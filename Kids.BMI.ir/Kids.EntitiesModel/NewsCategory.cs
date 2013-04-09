//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace Kids.EntitiesModel
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(News))]
    public partial class NewsCategory: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int NewsCategoryId
        {
            get { return _newsCategoryId; }
            set
            {
                if (_newsCategoryId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'NewsCategoryId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _newsCategoryId = value;
                    OnPropertyChanged("NewsCategoryId");
                }
            }
        }
        private int _newsCategoryId;
    
        [DataMember]
        public string NewsCategoryName
        {
            get { return _newsCategoryName; }
            set
            {
                if (_newsCategoryName != value)
                {
                    _newsCategoryName = value;
                    OnPropertyChanged("NewsCategoryName");
                }
            }
        }
        private string _newsCategoryName;
    
        [DataMember]
        public string NewsCategoryDescription
        {
            get { return _newsCategoryDescription; }
            set
            {
                if (_newsCategoryDescription != value)
                {
                    _newsCategoryDescription = value;
                    OnPropertyChanged("NewsCategoryDescription");
                }
            }
        }
        private string _newsCategoryDescription;
    
        [DataMember]
        public int SortOrderId
        {
            get { return _sortOrderId; }
            set
            {
                if (_sortOrderId != value)
                {
                    _sortOrderId = value;
                    OnPropertyChanged("SortOrderId");
                }
            }
        }
        private int _sortOrderId;
    
        [DataMember]
        public bool IsVisibleCategory
        {
            get { return _isVisibleCategory; }
            set
            {
                if (_isVisibleCategory != value)
                {
                    _isVisibleCategory = value;
                    OnPropertyChanged("IsVisibleCategory");
                }
            }
        }
        private bool _isVisibleCategory;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<News> News
        {
            get
            {
                if (_news == null)
                {
                    _news = new TrackableCollection<News>();
                    _news.CollectionChanged += FixupNews;
                }
                return _news;
            }
            set
            {
                if (!ReferenceEquals(_news, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_news != null)
                    {
                        _news.CollectionChanged -= FixupNews;
                    }
                    _news = value;
                    if (_news != null)
                    {
                        _news.CollectionChanged += FixupNews;
                    }
                    OnNavigationPropertyChanged("News");
                }
            }
        }
        private TrackableCollection<News> _news;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            News.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupNews(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (News item in e.NewItems)
                {
                    if (!item.NewsCategories.Contains(this))
                    {
                        item.NewsCategories.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("News", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (News item in e.OldItems)
                {
                    if (item.NewsCategories.Contains(this))
                    {
                        item.NewsCategories.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("News", item);
                    }
                }
            }
        }

        #endregion
    }
}
