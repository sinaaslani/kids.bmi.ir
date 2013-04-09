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
    [KnownType(typeof(FAQCategory))]
    public partial class FAQ: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public long FAQId
        {
            get { return _fAQId; }
            set
            {
                if (_fAQId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'FAQId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _fAQId = value;
                    OnPropertyChanged("FAQId");
                }
            }
        }
        private long _fAQId;
    
        [DataMember]
        public long CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (_categoryId != value)
                {
                    ChangeTracker.RecordOriginalValue("CategoryId", _categoryId);
                    if (!IsDeserializing)
                    {
                        if (FAQCategory != null && FAQCategory.CategoryId != value)
                        {
                            FAQCategory = null;
                        }
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
        public string Summary
        {
            get { return _summary; }
            set
            {
                if (_summary != value)
                {
                    _summary = value;
                    OnPropertyChanged("Summary");
                }
            }
        }
        private string _summary;
    
        [DataMember]
        public string Body
        {
            get { return _body; }
            set
            {
                if (_body != value)
                {
                    _body = value;
                    OnPropertyChanged("Body");
                }
            }
        }
        private string _body;
    
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
        public string LargePic
        {
            get { return _largePic; }
            set
            {
                if (_largePic != value)
                {
                    _largePic = value;
                    OnPropertyChanged("LargePic");
                }
            }
        }
        private string _largePic;
    
        [DataMember]
        public string ExtraFile
        {
            get { return _extraFile; }
            set
            {
                if (_extraFile != value)
                {
                    _extraFile = value;
                    OnPropertyChanged("ExtraFile");
                }
            }
        }
        private string _extraFile;
    
        [DataMember]
        public int Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        private int _status;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public FAQCategory FAQCategory
        {
            get { return _fAQCategory; }
            set
            {
                if (!ReferenceEquals(_fAQCategory, value))
                {
                    var previousValue = _fAQCategory;
                    _fAQCategory = value;
                    FixupFAQCategory(previousValue);
                    OnNavigationPropertyChanged("FAQCategory");
                }
            }
        }
        private FAQCategory _fAQCategory;

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
            FAQCategory = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupFAQCategory(FAQCategory previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.FAQs.Contains(this))
            {
                previousValue.FAQs.Remove(this);
            }
    
            if (FAQCategory != null)
            {
                if (!FAQCategory.FAQs.Contains(this))
                {
                    FAQCategory.FAQs.Add(this);
                }
    
                CategoryId = FAQCategory.CategoryId;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("FAQCategory")
                    && (ChangeTracker.OriginalValues["FAQCategory"] == FAQCategory))
                {
                    ChangeTracker.OriginalValues.Remove("FAQCategory");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("FAQCategory", previousValue);
                }
                if (FAQCategory != null && !FAQCategory.ChangeTracker.ChangeTrackingEnabled)
                {
                    FAQCategory.StartTracking();
                }
            }
        }

        #endregion
    }
}
