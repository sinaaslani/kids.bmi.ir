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
    [KnownType(typeof(Festival_Pictures))]
    public partial class Festival_Pictures_Poll: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public long PicPollId
        {
            get { return _picPollId; }
            set
            {
                if (_picPollId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'PicPollId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _picPollId = value;
                    OnPropertyChanged("PicPollId");
                }
            }
        }
        private long _picPollId;
    
        [DataMember]
        public int PicId
        {
            get { return _picId; }
            set
            {
                if (_picId != value)
                {
                    ChangeTracker.RecordOriginalValue("PicId", _picId);
                    if (!IsDeserializing)
                    {
                        if (Festival_Pictures != null && Festival_Pictures.PicId != value)
                        {
                            Festival_Pictures = null;
                        }
                    }
                    _picId = value;
                    OnPropertyChanged("PicId");
                }
            }
        }
        private int _picId;
    
        [DataMember]
        public long PollUserId
        {
            get { return _pollUserId; }
            set
            {
                if (_pollUserId != value)
                {
                    _pollUserId = value;
                    OnPropertyChanged("PollUserId");
                }
            }
        }
        private long _pollUserId;
    
        [DataMember]
        public string PollDescription
        {
            get { return _pollDescription; }
            set
            {
                if (_pollDescription != value)
                {
                    _pollDescription = value;
                    OnPropertyChanged("PollDescription");
                }
            }
        }
        private string _pollDescription;
    
        [DataMember]
        public string CreatDateTime
        {
            get { return _creatDateTime; }
            set
            {
                if (_creatDateTime != value)
                {
                    _creatDateTime = value;
                    OnPropertyChanged("CreatDateTime");
                }
            }
        }
        private string _creatDateTime;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Festival_Pictures Festival_Pictures
        {
            get { return _festival_Pictures; }
            set
            {
                if (!ReferenceEquals(_festival_Pictures, value))
                {
                    var previousValue = _festival_Pictures;
                    _festival_Pictures = value;
                    FixupFestival_Pictures(previousValue);
                    OnNavigationPropertyChanged("Festival_Pictures");
                }
            }
        }
        private Festival_Pictures _festival_Pictures;

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
            Festival_Pictures = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupFestival_Pictures(Festival_Pictures previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Festival_Pictures_Poll.Contains(this))
            {
                previousValue.Festival_Pictures_Poll.Remove(this);
            }
    
            if (Festival_Pictures != null)
            {
                if (!Festival_Pictures.Festival_Pictures_Poll.Contains(this))
                {
                    Festival_Pictures.Festival_Pictures_Poll.Add(this);
                }
    
                PicId = Festival_Pictures.PicId;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Festival_Pictures")
                    && (ChangeTracker.OriginalValues["Festival_Pictures"] == Festival_Pictures))
                {
                    ChangeTracker.OriginalValues.Remove("Festival_Pictures");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Festival_Pictures", previousValue);
                }
                if (Festival_Pictures != null && !Festival_Pictures.ChangeTracker.ChangeTrackingEnabled)
                {
                    Festival_Pictures.StartTracking();
                }
            }
        }

        #endregion
    }
}
