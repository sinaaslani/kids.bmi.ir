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
    [KnownType(typeof(KidsUser))]
    [KnownType(typeof(Wish))]
    public partial class Kids_Wishes: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public long KidsWisheId
        {
            get { return _kidsWisheId; }
            set
            {
                if (_kidsWisheId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'KidsWisheId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _kidsWisheId = value;
                    OnPropertyChanged("KidsWisheId");
                }
            }
        }
        private long _kidsWisheId;
    
        [DataMember]
        public long KidsUserId
        {
            get { return _kidsUserId; }
            set
            {
                if (_kidsUserId != value)
                {
                    ChangeTracker.RecordOriginalValue("KidsUserId", _kidsUserId);
                    if (!IsDeserializing)
                    {
                        if (KidsUser != null && KidsUser.KidsUserId != value)
                        {
                            KidsUser = null;
                        }
                    }
                    _kidsUserId = value;
                    OnPropertyChanged("KidsUserId");
                }
            }
        }
        private long _kidsUserId;
    
        [DataMember]
        public int WishId
        {
            get { return _wishId; }
            set
            {
                if (_wishId != value)
                {
                    ChangeTracker.RecordOriginalValue("WishId", _wishId);
                    if (!IsDeserializing)
                    {
                        if (Wish != null && Wish.WishId != value)
                        {
                            Wish = null;
                        }
                    }
                    _wishId = value;
                    OnPropertyChanged("WishId");
                }
            }
        }
        private int _wishId;
    
        [DataMember]
        public System.DateTime CreateDateTime
        {
            get { return _createDateTime; }
            set
            {
                if (_createDateTime != value)
                {
                    _createDateTime = value;
                    OnPropertyChanged("CreateDateTime");
                }
            }
        }
        private System.DateTime _createDateTime;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public KidsUser KidsUser
        {
            get { return _kidsUser; }
            set
            {
                if (!ReferenceEquals(_kidsUser, value))
                {
                    var previousValue = _kidsUser;
                    _kidsUser = value;
                    FixupKidsUser(previousValue);
                    OnNavigationPropertyChanged("KidsUser");
                }
            }
        }
        private KidsUser _kidsUser;
    
        [DataMember]
        public Wish Wish
        {
            get { return _wish; }
            set
            {
                if (!ReferenceEquals(_wish, value))
                {
                    var previousValue = _wish;
                    _wish = value;
                    FixupWish(previousValue);
                    OnNavigationPropertyChanged("Wish");
                }
            }
        }
        private Wish _wish;

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
            KidsUser = null;
            Wish = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupKidsUser(KidsUser previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Kids_Wishes.Contains(this))
            {
                previousValue.Kids_Wishes.Remove(this);
            }
    
            if (KidsUser != null)
            {
                if (!KidsUser.Kids_Wishes.Contains(this))
                {
                    KidsUser.Kids_Wishes.Add(this);
                }
    
                KidsUserId = KidsUser.KidsUserId;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("KidsUser")
                    && (ChangeTracker.OriginalValues["KidsUser"] == KidsUser))
                {
                    ChangeTracker.OriginalValues.Remove("KidsUser");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("KidsUser", previousValue);
                }
                if (KidsUser != null && !KidsUser.ChangeTracker.ChangeTrackingEnabled)
                {
                    KidsUser.StartTracking();
                }
            }
        }
    
        private void FixupWish(Wish previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Kids_Wishes.Contains(this))
            {
                previousValue.Kids_Wishes.Remove(this);
            }
    
            if (Wish != null)
            {
                if (!Wish.Kids_Wishes.Contains(this))
                {
                    Wish.Kids_Wishes.Add(this);
                }
    
                WishId = Wish.WishId;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Wish")
                    && (ChangeTracker.OriginalValues["Wish"] == Wish))
                {
                    ChangeTracker.OriginalValues.Remove("Wish");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Wish", previousValue);
                }
                if (Wish != null && !Wish.ChangeTracker.ChangeTrackingEnabled)
                {
                    Wish.StartTracking();
                }
            }
        }

        #endregion
    }
}
