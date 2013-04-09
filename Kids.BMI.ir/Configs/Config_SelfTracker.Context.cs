﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Kids.Common
{
    public partial class BMIKidsEntities : ObjectContext
    {
        public const string ConnectionString = "name=BMIKidsEntities";
        public const string ContainerName = "BMIKidsEntities";
    
        #region Constructors
    
        public BMIKidsEntities()
            : base(ConnectionString, ContainerName)
        {
            Initialize();
        }
    
        public BMIKidsEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            Initialize();
        }
    
        public BMIKidsEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            Initialize();
        }
    
        private void Initialize()
        {
            // Creating proxies requires the use of the ProxyDataContractResolver and
            // may allow lazy loading which can expand the loaded graph during serialization.
            ContextOptions.ProxyCreationEnabled = false;
            ObjectMaterialized += new ObjectMaterializedEventHandler(HandleObjectMaterialized);
        }
    
        private void HandleObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var entity = e.Entity as IObjectWithChangeTracker;
            if (entity != null)
            {
                bool changeTrackingEnabled = entity.ChangeTracker.ChangeTrackingEnabled;
                try
                {
                    entity.MarkAsUnchanged();
                }
                finally
                {
                    entity.ChangeTracker.ChangeTrackingEnabled = changeTrackingEnabled;
                }
                this.StoreReferenceKeyValues(entity);
            }
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<Config> Configs
        {
            get { return _configs  ?? (_configs = CreateObjectSet<Config>("Configs")); }
        }
        private ObjectSet<Config> _configs;

        #endregion
    }
}