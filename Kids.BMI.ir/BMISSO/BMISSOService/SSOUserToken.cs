namespace BMISSOService
{
    public class SSOUserToken
    {
        private string _uname = "";
        private string _Pass = "";


        public string SSOUserName
        {
            get { return _uname; }
            set { _uname = value; }
        }

        public string SSOPassword
        {
            get { return _Pass; }
            set { _Pass = value; }
        }
    }
}
