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
    [KnownType(typeof(PollResponseItem))]
    [KnownType(typeof(PollUserResponse))]
    public partial class PollQuestion: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public long QuestionId
        {
            get { return _questionId; }
            set
            {
                if (_questionId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'QuestionId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _questionId = value;
                    OnPropertyChanged("QuestionId");
                }
            }
        }
        private long _questionId;
    
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
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }
        private bool _isActive;
    
        [DataMember]
        public bool UsersCanViewResult
        {
            get { return _usersCanViewResult; }
            set
            {
                if (_usersCanViewResult != value)
                {
                    _usersCanViewResult = value;
                    OnPropertyChanged("UsersCanViewResult");
                }
            }
        }
        private bool _usersCanViewResult;
    
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
    
        [DataMember]
        public bool HasScore
        {
            get { return _hasScore; }
            set
            {
                if (_hasScore != value)
                {
                    _hasScore = value;
                    OnPropertyChanged("HasScore");
                }
            }
        }
        private bool _hasScore;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<PollResponseItem> PollResponseItems
        {
            get
            {
                if (_pollResponseItems == null)
                {
                    _pollResponseItems = new TrackableCollection<PollResponseItem>();
                    _pollResponseItems.CollectionChanged += FixupPollResponseItems;
                }
                return _pollResponseItems;
            }
            set
            {
                if (!ReferenceEquals(_pollResponseItems, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_pollResponseItems != null)
                    {
                        _pollResponseItems.CollectionChanged -= FixupPollResponseItems;
                    }
                    _pollResponseItems = value;
                    if (_pollResponseItems != null)
                    {
                        _pollResponseItems.CollectionChanged += FixupPollResponseItems;
                    }
                    OnNavigationPropertyChanged("PollResponseItems");
                }
            }
        }
        private TrackableCollection<PollResponseItem> _pollResponseItems;
    
        [DataMember]
        public TrackableCollection<PollUserResponse> PollUserResponses
        {
            get
            {
                if (_pollUserResponses == null)
                {
                    _pollUserResponses = new TrackableCollection<PollUserResponse>();
                    _pollUserResponses.CollectionChanged += FixupPollUserResponses;
                }
                return _pollUserResponses;
            }
            set
            {
                if (!ReferenceEquals(_pollUserResponses, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_pollUserResponses != null)
                    {
                        _pollUserResponses.CollectionChanged -= FixupPollUserResponses;
                    }
                    _pollUserResponses = value;
                    if (_pollUserResponses != null)
                    {
                        _pollUserResponses.CollectionChanged += FixupPollUserResponses;
                    }
                    OnNavigationPropertyChanged("PollUserResponses");
                }
            }
        }
        private TrackableCollection<PollUserResponse> _pollUserResponses;

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
            PollResponseItems.Clear();
            PollUserResponses.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupPollResponseItems(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (PollResponseItem item in e.NewItems)
                {
                    item.PollQuestion = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("PollResponseItems", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PollResponseItem item in e.OldItems)
                {
                    if (ReferenceEquals(item.PollQuestion, this))
                    {
                        item.PollQuestion = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("PollResponseItems", item);
                    }
                }
            }
        }
    
        private void FixupPollUserResponses(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (PollUserResponse item in e.NewItems)
                {
                    item.PollQuestion = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("PollUserResponses", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PollUserResponse item in e.OldItems)
                {
                    if (ReferenceEquals(item.PollQuestion, this))
                    {
                        item.PollQuestion = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("PollUserResponses", item);
                    }
                }
            }
        }

        #endregion
    }
}
