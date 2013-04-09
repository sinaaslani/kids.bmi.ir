using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Kids.EntitiesModel
{
    public static class Extension
    {
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>
  (
    this ObjectQuery<TEntity> query,
    Expression<Func<TEntity, TValue>> selector,
    IEnumerable<TValue> collection
  )
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (collection == null) throw new ArgumentNullException("collection");
            if (!collection.Any())
                return query.Where(t => false);

            ParameterExpression p = selector.Parameters.Single();

            IEnumerable<Expression> equals = collection.Select(value =>
               (Expression)Expression.Equal(selector.Body,
                    Expression.Constant(value, typeof(TValue))));

            Expression body = equals.Aggregate(Expression.Or);

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
        }

        //Optional - to allow static collection:
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>
          (
            this ObjectQuery<TEntity> query,
            Expression<Func<TEntity, TValue>> selector,
            params TValue[] collection
          )
        {
            return WhereIn(query, selector, (IEnumerable<TValue>)collection);
        }

        public static IQueryable<T> OrderBy2<T>(this IQueryable<T> items, string propertyName)
        {
            var typeOfT = typeof(T);
            var parameter = Expression.Parameter(typeOfT, "parameter");
            var propertyType = typeOfT.GetProperty(propertyName).PropertyType;
            var propertyAccess = Expression.PropertyOrField(parameter, propertyName);
            var orderExpression = Expression.Lambda(propertyAccess, parameter);

            var expression = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { typeOfT, propertyType }, items.Expression, Expression.Quote(orderExpression));
            return items.Provider.CreateQuery<T>(expression);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<string> Sort)
        {
            if (Sort == null)
                return source as IOrderedQueryable<T>;
            foreach (string s in Sort)
            {
                var sortItem = s.Split(' ');

                var col = sortItem[0];
                var dir = "asc";
                if (sortItem.Length > 1)
                    dir = sortItem[1];

                if (dir.ToLower() == "asc")
                    source = source.OrderBy(col);
                else if (dir.ToLower() == "desc")
                    source = source.OrderByDescending(col);
                else throw new InvalidOperationException();
            }
            return source as IOrderedQueryable<T>;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}
