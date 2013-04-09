﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3053.
// 
#pragma warning disable 1591

namespace BMISSOClientHelper.BMISSOService
{


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "BMISSOServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class BMISSOServiceWse : Microsoft.Web.Services3.WebServicesClientProtocol
    {

        private System.Threading.SendOrPostCallback AuthenticateOperationCompleted;

        private System.Threading.SendOrPostCallback GetUserInfoOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public BMISSOServiceWse()
        {
            this.Url = global::BMISSOClientHelper.Properties.Settings.Default.BMISSOClientHelper_BMISSOService_BMISSOService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event AuthenticateCompletedEventHandler AuthenticateCompleted;

        /// <remarks/>
        public event GetUserInfoCompletedEventHandler GetUserInfoCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Authenticate", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UserProfile Authenticate(string UserID, string Password)
        {
            object[] results = this.Invoke("Authenticate", new object[] {
                        UserID,
                        Password});
            return ((UserProfile)(results[0]));
        }

        /// <remarks/>
        public void AuthenticateAsync(string UserID, string Password)
        {
            this.AuthenticateAsync(UserID, Password, null);
        }

        /// <remarks/>
        public void AuthenticateAsync(string UserID, string Password, object userState)
        {
            if ((this.AuthenticateOperationCompleted == null))
            {
                this.AuthenticateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenticateOperationCompleted);
            }
            this.InvokeAsync("Authenticate", new object[] {
                        UserID,
                        Password}, this.AuthenticateOperationCompleted, userState);
        }

        private void OnAuthenticateOperationCompleted(object arg)
        {
            if ((this.AuthenticateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenticateCompleted(this, new AuthenticateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUserInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UserProfile GetUserInfo(string UserID)
        {
            object[] results = this.Invoke("GetUserInfo", new object[] {
                        UserID});
            return ((UserProfile)(results[0]));
        }

        /// <remarks/>
        public void GetUserInfoAsync(string UserID)
        {
            this.GetUserInfoAsync(UserID, null);
        }

        /// <remarks/>
        public void GetUserInfoAsync(string UserID, object userState)
        {
            if ((this.GetUserInfoOperationCompleted == null))
            {
                this.GetUserInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserInfoOperationCompleted);
            }
            this.InvokeAsync("GetUserInfo", new object[] {
                        UserID}, this.GetUserInfoOperationCompleted, userState);
        }

        private void OnGetUserInfoOperationCompleted(object arg)
        {
            if ((this.GetUserInfoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserInfoCompleted(this, new GetUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "BMISSOServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class BMISSOService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback AuthenticateOperationCompleted;

        private System.Threading.SendOrPostCallback GetUserInfoOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public BMISSOService()
        {
            this.Url = global::BMISSOClientHelper.Properties.Settings.Default.BMISSOClientHelper_BMISSOService_BMISSOService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event AuthenticateCompletedEventHandler AuthenticateCompleted;

        /// <remarks/>
        public event GetUserInfoCompletedEventHandler GetUserInfoCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Authenticate", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UserProfile Authenticate(string UserID, string Password)
        {
            object[] results = this.Invoke("Authenticate", new object[] {
                        UserID,
                        Password});
            return ((UserProfile)(results[0]));
        }

        /// <remarks/>
        public void AuthenticateAsync(string UserID, string Password)
        {
            this.AuthenticateAsync(UserID, Password, null);
        }

        /// <remarks/>
        public void AuthenticateAsync(string UserID, string Password, object userState)
        {
            if ((this.AuthenticateOperationCompleted == null))
            {
                this.AuthenticateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenticateOperationCompleted);
            }
            this.InvokeAsync("Authenticate", new object[] {
                        UserID,
                        Password}, this.AuthenticateOperationCompleted, userState);
        }

        private void OnAuthenticateOperationCompleted(object arg)
        {
            if ((this.AuthenticateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenticateCompleted(this, new AuthenticateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUserInfo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UserProfile GetUserInfo(string UserID)
        {
            object[] results = this.Invoke("GetUserInfo", new object[] {
                        UserID});
            return ((UserProfile)(results[0]));
        }

        /// <remarks/>
        public void GetUserInfoAsync(string UserID)
        {
            this.GetUserInfoAsync(UserID, null);
        }

        /// <remarks/>
        public void GetUserInfoAsync(string UserID, object userState)
        {
            if ((this.GetUserInfoOperationCompleted == null))
            {
                this.GetUserInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserInfoOperationCompleted);
            }
            this.InvokeAsync("GetUserInfo", new object[] {
                        UserID}, this.GetUserInfoOperationCompleted, userState);
        }

        private void OnGetUserInfoOperationCompleted(object arg)
        {
            if ((this.GetUserInfoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserInfoCompleted(this, new GetUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class UserProfile
    {

        private string userIDField;

        private string passwordField;

        private string personalNoField;

        private string idNumberField;

        private string nationalCodeField;

        private string nameField;

        private bool isBMIEmployeeField;
        
        private string fatherNameField;

        private string emailField;

        private string phoneNumberField;

        private string faxNumberField;

        private string mobileField;

        private string birthdateField;

        private string addressField;

        private System.DateTime lastLoginTimeField;

        private System.DateTime currentLoginTimeField;

        private Genders genderField;

        /// <remarks/>
        public string UserID
        {
            get
            {
                return this.userIDField;
            }
            set
            {
                this.userIDField = value;
            }
        }

        /// <remarks/>
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }

        /// <remarks/>
        public string PersonalNo
        {
            get
            {
                return this.personalNoField;
            }
            set
            {
                this.personalNoField = value;
            }
        }

        /// <remarks/>
        public string IdNumber
        {
            get
            {
                return this.idNumberField;
            }
            set
            {
                this.idNumberField = value;
            }
        }

        /// <remarks/>
        public string NationalCode
        {
            get
            {
                return this.nationalCodeField;
            }
            set
            {
                this.nationalCodeField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public bool IsBMIEmployee {
            get {
                return this.isBMIEmployeeField;
            }
            set {
                this.isBMIEmployeeField = value;
            }
        }
        
        /// <remarks/>
        public string FatherName {
            get {
                return this.fatherNameField;
            }
            set
            {
                this.fatherNameField = value;
            }
        }

        /// <remarks/>
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }

        /// <remarks/>
        public string FaxNumber
        {
            get
            {
                return this.faxNumberField;
            }
            set
            {
                this.faxNumberField = value;
            }
        }

        /// <remarks/>
        public string Mobile
        {
            get
            {
                return this.mobileField;
            }
            set
            {
                this.mobileField = value;
            }
        }

        /// <remarks/>
        public string Birthdate
        {
            get
            {
                return this.birthdateField;
            }
            set
            {
                this.birthdateField = value;
            }
        }

        /// <remarks/>
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public System.DateTime LastLoginTime
        {
            get
            {
                return this.lastLoginTimeField;
            }
            set
            {
                this.lastLoginTimeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime CurrentLoginTime
        {
            get
            {
                return this.currentLoginTimeField;
            }
            set
            {
                this.currentLoginTimeField = value;
            }
        }

        /// <remarks/>
        public Genders Gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public enum Genders
    {

        /// <remarks/>
        Male,

        /// <remarks/>
        Female,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void AuthenticateCompletedEventHandler(object sender, AuthenticateCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthenticateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal AuthenticateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UserProfile Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UserProfile)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetUserInfoCompletedEventHandler(object sender, GetUserInfoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetUserInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UserProfile Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UserProfile)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591