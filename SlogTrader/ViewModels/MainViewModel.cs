using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;

namespace SlogTrader.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _numOpenTradingWindows;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// 
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            // My initial effort:
            // OpenTradingWindowCommand = new ActionCommand( () => OpenTradingWindow(), () => true );

            // ReSharper 'Replace with method group' :
            OpenTradingWindowCommand = new ActionCommand(OpenTradingWindow, NotMoreThanFourTradingWindowsOpen);

            OpenOrderingWindowCommand = new ActionCommand(OpenOrderingWindow, () => true);
            OpenOrderingWindowCommand.Execute(null);

        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        // Commands
        public ICommand OpenTradingWindowCommand
        {
            get; 
            private set; 
        }

        public ICommand OpenOrderingWindowCommand
        {
            get;
            private set;
        }

        public void OpenTradingWindow()
        {
            _numOpenTradingWindows++;
        }

        public void OpenOrderingWindow()
        {
            MessageBox.Show("I just opened a new Ordering window");
        }

        public bool NotMoreThanFourTradingWindowsOpen()
        {
            return _numOpenTradingWindows <= 4;
        }


    }
}
