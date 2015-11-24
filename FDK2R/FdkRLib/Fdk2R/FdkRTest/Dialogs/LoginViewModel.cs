using System.Globalization;
using RHost.Shared;

namespace FdkRTest.Dialogs
{
    public class LoginViewModel : NotificationViewModel
    {
        private string _address;
        private long _login;
        private string _password;

        public LoginViewModel()
        {
            Address = "tp.dev.soft-fx.eu";
            Login = 100106;
            Password = "123qwe123";
            Wrapper = new FdkWrapper();
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                Changed();
            }
        }

        public long Login
        {
            get { return _login; }
            set
            {
                _login = value; 
                Changed();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value; 
                Changed();
            }
        }

        public FdkWrapper Wrapper { get; set; }

        public bool Connect()
        {   
            Wrapper.Address = Address;
            Wrapper.Login = Login.ToString(CultureInfo.InvariantCulture);
            Wrapper.Password = Password;
            return Wrapper.Connect();

        }
    }
}