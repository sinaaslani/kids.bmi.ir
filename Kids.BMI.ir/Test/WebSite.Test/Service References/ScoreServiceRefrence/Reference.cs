﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSite.Test.ScoreServiceRefrence {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TempUser", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class TempUser : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FamilyField;
        
        private bool SexField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Family {
            get {
                return this.FamilyField;
            }
            set {
                if ((object.ReferenceEquals(this.FamilyField, value) != true)) {
                    this.FamilyField = value;
                    this.RaisePropertyChanged("Family");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public bool Sex {
            get {
                return this.SexField;
            }
            set {
                if ((this.SexField.Equals(value) != true)) {
                    this.SexField = value;
                    this.RaisePropertyChanged("Sex");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ScoreServiceRefrence.ScoreServiceSoap")]
    public interface ScoreServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddGameScore", ReplyAction="*")]
        void AddGameScore(int GameId, int ScoreId, long Value);
        
        // CODEGEN: Generating message contract since element name TempUserId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddGameScoreForTempUser", ReplyAction="*")]
        WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserResponse AddGameScoreForTempUser(WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserRequest request);
        
        // CODEGEN: Generating message contract since element name TempUserId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IsValidUser", ReplyAction="*")]
        WebSite.Test.ScoreServiceRefrence.IsValidUserResponse IsValidUser(WebSite.Test.ScoreServiceRefrence.IsValidUserRequest request);
        
        // CODEGEN: Generating message contract since element name TempUserId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/KeepAliveTempUser", ReplyAction="*")]
        WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserResponse KeepAliveTempUser(WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddGameScoreForTempUserRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddGameScoreForTempUser", Namespace="http://tempuri.org/", Order=0)]
        public WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserRequestBody Body;
        
        public AddGameScoreForTempUserRequest() {
        }
        
        public AddGameScoreForTempUserRequest(WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AddGameScoreForTempUserRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string TempUserId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int GameId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int ScoreId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long Value;
        
        public AddGameScoreForTempUserRequestBody() {
        }
        
        public AddGameScoreForTempUserRequestBody(string TempUserId, int GameId, int ScoreId, long Value) {
            this.TempUserId = TempUserId;
            this.GameId = GameId;
            this.ScoreId = ScoreId;
            this.Value = Value;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddGameScoreForTempUserResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddGameScoreForTempUserResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserResponseBody Body;
        
        public AddGameScoreForTempUserResponse() {
        }
        
        public AddGameScoreForTempUserResponse(WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class AddGameScoreForTempUserResponseBody {
        
        public AddGameScoreForTempUserResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class IsValidUserRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="IsValidUser", Namespace="http://tempuri.org/", Order=0)]
        public WebSite.Test.ScoreServiceRefrence.IsValidUserRequestBody Body;
        
        public IsValidUserRequest() {
        }
        
        public IsValidUserRequest(WebSite.Test.ScoreServiceRefrence.IsValidUserRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class IsValidUserRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string TempUserId;
        
        public IsValidUserRequestBody() {
        }
        
        public IsValidUserRequestBody(string TempUserId) {
            this.TempUserId = TempUserId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class IsValidUserResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="IsValidUserResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebSite.Test.ScoreServiceRefrence.IsValidUserResponseBody Body;
        
        public IsValidUserResponse() {
        }
        
        public IsValidUserResponse(WebSite.Test.ScoreServiceRefrence.IsValidUserResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class IsValidUserResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public WebSite.Test.ScoreServiceRefrence.TempUser IsValidUserResult;
        
        public IsValidUserResponseBody() {
        }
        
        public IsValidUserResponseBody(WebSite.Test.ScoreServiceRefrence.TempUser IsValidUserResult) {
            this.IsValidUserResult = IsValidUserResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class KeepAliveTempUserRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="KeepAliveTempUser", Namespace="http://tempuri.org/", Order=0)]
        public WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserRequestBody Body;
        
        public KeepAliveTempUserRequest() {
        }
        
        public KeepAliveTempUserRequest(WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class KeepAliveTempUserRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string TempUserId;
        
        public KeepAliveTempUserRequestBody() {
        }
        
        public KeepAliveTempUserRequestBody(string TempUserId) {
            this.TempUserId = TempUserId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class KeepAliveTempUserResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="KeepAliveTempUserResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserResponseBody Body;
        
        public KeepAliveTempUserResponse() {
        }
        
        public KeepAliveTempUserResponse(WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class KeepAliveTempUserResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool KeepAliveTempUserResult;
        
        public KeepAliveTempUserResponseBody() {
        }
        
        public KeepAliveTempUserResponseBody(bool KeepAliveTempUserResult) {
            this.KeepAliveTempUserResult = KeepAliveTempUserResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ScoreServiceSoapChannel : WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ScoreServiceSoapClient : System.ServiceModel.ClientBase<WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap>, WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap {
        
        public ScoreServiceSoapClient() {
        }
        
        public ScoreServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ScoreServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ScoreServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ScoreServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddGameScore(int GameId, int ScoreId, long Value) {
            base.Channel.AddGameScore(GameId, ScoreId, Value);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserResponse WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap.AddGameScoreForTempUser(WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserRequest request) {
            return base.Channel.AddGameScoreForTempUser(request);
        }
        
        public void AddGameScoreForTempUser(string TempUserId, int GameId, int ScoreId, long Value) {
            WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserRequest inValue = new WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserRequest();
            inValue.Body = new WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserRequestBody();
            inValue.Body.TempUserId = TempUserId;
            inValue.Body.GameId = GameId;
            inValue.Body.ScoreId = ScoreId;
            inValue.Body.Value = Value;
            WebSite.Test.ScoreServiceRefrence.AddGameScoreForTempUserResponse retVal = ((WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap)(this)).AddGameScoreForTempUser(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebSite.Test.ScoreServiceRefrence.IsValidUserResponse WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap.IsValidUser(WebSite.Test.ScoreServiceRefrence.IsValidUserRequest request) {
            return base.Channel.IsValidUser(request);
        }
        
        public WebSite.Test.ScoreServiceRefrence.TempUser IsValidUser(string TempUserId) {
            WebSite.Test.ScoreServiceRefrence.IsValidUserRequest inValue = new WebSite.Test.ScoreServiceRefrence.IsValidUserRequest();
            inValue.Body = new WebSite.Test.ScoreServiceRefrence.IsValidUserRequestBody();
            inValue.Body.TempUserId = TempUserId;
            WebSite.Test.ScoreServiceRefrence.IsValidUserResponse retVal = ((WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap)(this)).IsValidUser(inValue);
            return retVal.Body.IsValidUserResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserResponse WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap.KeepAliveTempUser(WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserRequest request) {
            return base.Channel.KeepAliveTempUser(request);
        }
        
        public bool KeepAliveTempUser(string TempUserId) {
            WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserRequest inValue = new WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserRequest();
            inValue.Body = new WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserRequestBody();
            inValue.Body.TempUserId = TempUserId;
            WebSite.Test.ScoreServiceRefrence.KeepAliveTempUserResponse retVal = ((WebSite.Test.ScoreServiceRefrence.ScoreServiceSoap)(this)).KeepAliveTempUser(inValue);
            return retVal.Body.KeepAliveTempUserResult;
        }
    }
}
