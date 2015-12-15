using RDotNet;
using RHost.Shared;

namespace FdkRTest.Dialogs
{
    public class MainViewModel : NotificationViewModel
    {
        private string _resultScript;
        private string _scriptToRun;
        public FdkWrapper Wrapper { get; set; }

        public MainViewModel()
        {
            REngine.SetEnvironmentVariables();
            // There are several options to initialize the engine, but by default the following suffice:
            Engine = REngine.GetInstance();
        }

        public REngine Engine { get; set; }

        public string ScriptToRun
        {
            get { return _scriptToRun; }
            set
            {
                _scriptToRun = value; 
                Changed();
            }
        }

        public string LineOfScript { get; set; }

        public string ResultScript
        {
            get { return _resultScript; }
            set
            {
                _resultScript = value;
                Changed();
            }
        }
    }
}