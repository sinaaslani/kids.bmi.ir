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
    [KnownType(typeof(SystemRole))]
    public partial class SystemUser: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public long UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'UserId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        private long _userId;
    
        [DataMember]
        public string SSOUserName
        {
            get { return _sSOUserName; }
            set
            {
                if (_sSOUserName != value)
                {
                    _sSOUserName = value;
                    OnPropertyChanged("SSOUserName");
                }
            }
        }
        private string _sSOUserName;
    
        [DataMember]
        public string BranchCode
        {
            get { return _branchCode; }
            set
            {
                if (_branchCode != value)
                {
                    _branchCode = value;
                    OnPropertyChanged("BranchCode");
                }
            }
        }
        private string _branchCode;
    
        [DataMember]
        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active != value)
                {
                    _active = value;
                    OnPropertyChanged("Active");
                }
            }
        }
        private bool _active;
    
        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;
    
        [DataMember]
        public string Family
        {
            get { return _family; }
            set
            {
                if (_family != value)
                {
                    _family = value;
                    OnPropertyChanged("Family");
                }
            }
        }
        private string _family;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<SystemRole> SystemRoles
        {
            get
            {
                if (_systemRoles == null)
                {
                    _systemRoles = new TrackableCollection<SystemRole>();
                    _systemRoles.CollectionChanged += FixupSystemRoles;
                }
                return _systemRoles;
            }
            set
            {
                if (!ReferenceEquals(_systemRoles, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_systemRoles != null)
                    {
                        _systemRoles.CollectionChanged -= FixupSystemRoles;
                    }
                    _systemRoles = value;
                    if (_systemRoles != null)
                    {
                        _systemRoles.CollectionChanged += FixupSystemRoles;
                    }
                    OnNavigationPropertyChanged("SystemRoles");
                }
            }
        }
        private TrackableCollection<SystemRole> _systemRoles;

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
            SystemRoles.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupSystemRoles(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (SystemRole item in e.NewItems)
                {
                    if (!item.SystemUsers.Contains(this))
                    {
                        item.SystemUsers.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("SystemRoles", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (SystemRole item in e.OldItems)
                {
                    if (item.SystemUsers.Contains(this))
                    {
                        item.SystemUsers.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("SystemRoles", item);
                    }
                }
            }
        }

        #endregion
    }
}
