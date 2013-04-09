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
    [KnownType(typeof(Kids_Wishes))]
    public partial class Wish: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int WishId
        {
            get { return _wishId; }
            set
            {
                if (_wishId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'WishId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _wishId = value;
                    OnPropertyChanged("WishId");
                }
            }
        }
        private int _wishId;
    
        [DataMember]
        public string WishName
        {
            get { return _wishName; }
            set
            {
                if (_wishName != value)
                {
                    _wishName = value;
                    OnPropertyChanged("WishName");
                }
            }
        }
        private string _wishName;
    
        [DataMember]
        public long WishAmount
        {
            get { return _wishAmount; }
            set
            {
                if (_wishAmount != value)
                {
                    _wishAmount = value;
                    OnPropertyChanged("WishAmount");
                }
            }
        }
        private long _wishAmount;
    
        [DataMember]
        public string WiShPicSmall
        {
            get { return _wiShPicSmall; }
            set
            {
                if (_wiShPicSmall != value)
                {
                    _wiShPicSmall = value;
                    OnPropertyChanged("WiShPicSmall");
                }
            }
        }
        private string _wiShPicSmall;
    
        [DataMember]
        public string WishPic
        {
            get { return _wishPic; }
            set
            {
                if (_wishPic != value)
                {
                    _wishPic = value;
                    OnPropertyChanged("WishPic");
                }
            }
        }
        private string _wishPic;
    
        [DataMember]
        public string WishDescription
        {
            get { return _wishDescription; }
            set
            {
                if (_wishDescription != value)
                {
                    _wishDescription = value;
                    OnPropertyChanged("WishDescription");
                }
            }
        }
        private string _wishDescription;
    
        [DataMember]
        public bool IsCustome
        {
            get { return _isCustome; }
            set
            {
                if (_isCustome != value)
                {
                    _isCustome = value;
                    OnPropertyChanged("IsCustome");
                }
            }
        }
        private bool _isCustome;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<Kids_Wishes> Kids_Wishes
        {
            get
            {
                if (_kids_Wishes == null)
                {
                    _kids_Wishes = new TrackableCollection<Kids_Wishes>();
                    _kids_Wishes.CollectionChanged += FixupKids_Wishes;
                }
                return _kids_Wishes;
            }
            set
            {
                if (!ReferenceEquals(_kids_Wishes, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_kids_Wishes != null)
                    {
                        _kids_Wishes.CollectionChanged -= FixupKids_Wishes;
                    }
                    _kids_Wishes = value;
                    if (_kids_Wishes != null)
                    {
                        _kids_Wishes.CollectionChanged += FixupKids_Wishes;
                    }
                    OnNavigationPropertyChanged("Kids_Wishes");
                }
            }
        }
        private TrackableCollection<Kids_Wishes> _kids_Wishes;

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
            Kids_Wishes.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupKids_Wishes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Kids_Wishes item in e.NewItems)
                {
                    item.Wish = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Kids_Wishes", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Kids_Wishes item in e.OldItems)
                {
                    if (ReferenceEquals(item.Wish, this))
                    {
                        item.Wish = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Kids_Wishes", item);
                    }
                }
            }
        }

        #endregion
    }
}