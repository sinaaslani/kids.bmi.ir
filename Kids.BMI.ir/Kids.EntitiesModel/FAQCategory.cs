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
    [KnownType(typeof(FAQ))]
    public partial class FAQCategory: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public long CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (_categoryId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'CategoryId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _categoryId = value;
                    OnPropertyChanged("CategoryId");
                }
            }
        }
        private long _categoryId;
    
        [DataMember]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        private string _title;
    
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
        public string SmallPic
        {
            get { return _smallPic; }
            set
            {
                if (_smallPic != value)
                {
                    _smallPic = value;
                    OnPropertyChanged("SmallPic");
                }
            }
        }
        private string _smallPic;
    
        [DataMember]
        public int Lang
        {
            get { return _lang; }
            set
            {
                if (_lang != value)
                {
                    _lang = value;
                    OnPropertyChanged("Lang");
                }
            }
        }
        private int _lang;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<FAQ> FAQs
        {
            get
            {
                if (_fAQs == null)
                {
                    _fAQs = new TrackableCollection<FAQ>();
                    _fAQs.CollectionChanged += FixupFAQs;
                }
                return _fAQs;
            }
            set
            {
                if (!ReferenceEquals(_fAQs, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_fAQs != null)
                    {
                        _fAQs.CollectionChanged -= FixupFAQs;
                    }
                    _fAQs = value;
                    if (_fAQs != null)
                    {
                        _fAQs.CollectionChanged += FixupFAQs;
                    }
                    OnNavigationPropertyChanged("FAQs");
                }
            }
        }
        private TrackableCollection<FAQ> _fAQs;

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
            FAQs.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupFAQs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (FAQ item in e.NewItems)
                {
                    item.FAQCategory = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("FAQs", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (FAQ item in e.OldItems)
                {
                    if (ReferenceEquals(item.FAQCategory, this))
                    {
                        item.FAQCategory = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("FAQs", item);
                    }
                }
            }
        }

        #endregion
    }
}