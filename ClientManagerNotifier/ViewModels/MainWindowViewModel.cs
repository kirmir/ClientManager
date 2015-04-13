using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ClientManagerNotifier.Models;
using ClientManagerNotifier.Models.DataTransfer;
using ClientManagerNotifier.Views;
using Microsoft.Practices.Prism.Commands;

namespace ClientManagerNotifier.ViewModels
{
    /// <summary>
    /// The main window view model.
    /// </summary>
    public class MainWindowViewModel
    {
        private readonly DataTransferModel _dataTransfer;
        private readonly IconCreator _icon;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            _dataTransfer = new DataTransferModel();
            
            // Setup commands
            CheckLetters = new DelegateCommand(() => { if (CloseEventHandler != null) CheckLettersHandler(); });
            SendLetters = new DelegateCommand(() => { if (CloseEventHandler != null) SendLetterHandler(); });
            UserData = new DelegateCommand(() => { (new UserDataEditor()).ShowDialog(); CheckLetters.Execute(); });
            CloseCommand = new DelegateCommand(() => { if (CloseEventHandler != null) CloseEventHandler(); });

            // Setup icon services
            CurrentIcon = new CurrentIconBindingObject();
            _icon = new IconCreator(new Icon(Properties.Resources.Icon, new System.Drawing.Size(16, 16)),
                        Properties.Settings.Default.IconColor);
            CurrentIcon.CurrentIcon = _icon.IconSource;

            // Setup timer
            MailCheckTimer = new Timer(obj => CheckLettersHandler(), null, 1000, (long)Properties.Settings.Default.CheckInterval.TotalMilliseconds);
        }

        /// <summary>
        /// Gets or sets the mail check timer.
        /// </summary>
        /// <value>
        /// The mail check timer.
        /// </value>
        public Timer MailCheckTimer { get; set; }

        /// <summary>
        /// Gets or sets the current icon.
        /// </summary>
        /// <value>
        /// The current icon.
        /// </value>
        public CurrentIconBindingObject CurrentIcon { get; set; }

        /// <summary>
        /// Gets or sets the check letters.
        /// </summary>
        /// <value>
        /// The check letters.
        /// </value>
        public DelegateCommand CheckLetters { get; set; }

        /// <summary>
        /// Gets or sets the send letters.
        /// </summary>
        /// <value>
        /// The send letters.
        /// </value>
        public DelegateCommand SendLetters { get; set; }

        /// <summary>
        /// Gets or sets the user data.
        /// </summary>
        /// <value>
        /// The user data.
        /// </value>
        public DelegateCommand UserData { get; set; }

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        /// <value>
        /// The close command.
        /// </value>
        public DelegateCommand CloseCommand { get; set; }

        /// <summary>
        /// Occurs when [close event handler].
        /// </summary>
        public Action CloseEventHandler { get; set; }

        /// <summary>
        /// Call's action in native context.
        /// </summary>
        public Action<Action> CallWithDispatcher { get; set; }

        /// <summary>
        /// Show's balloon message.
        /// </summary>
        public Action<UserControl, int> ShowMessageBaloon { get; set; }

        /// <summary>
        /// Sends the letter handler.
        /// </summary>
        private void SendLetterHandler()
        {
            if(Properties.Settings.Default.LettersUrl != string.Empty)
                Process.Start(new ProcessStartInfo(Properties.Settings.Default.LettersUrl));
        }

        /// <summary>
        /// Checks the letters.
        /// </summary>
        private void CheckLettersHandler()
        {
            int count;
            try
            {
                count = _dataTransfer.GetNumberOfUnreadMessages();
            }
            catch(DataTransferFailToLogOnException ex)
            {
                CallWithDispatcher(() => ShowBalloonErrorMessages(ex.Message));
                UserData.Execute();
                return;                
            }
            catch (Exception ex)
            {
                CallWithDispatcher(() => ShowBalloonErrorMessages(ex.Message));
                return;
            }
            
            if (_icon.ProcessImage(count))
                CurrentIcon.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action(() => CurrentIcon.CurrentIcon = _icon.ProcessedIcon));

            if (count == 0) return;

            CallWithDispatcher(() => ShowBalloonMessages("Check web-site!", count));
        }

        /// <summary>
        /// Shows the balloon messages.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="count">The count.</param>
        private void ShowBalloonMessages(string text, int count)
        {
            var mailBalloon = new MessageBalloon
                                  {
                                      BalloonText = text, 
                                      BalloonTitle = string.Format("You have {0} messages that's wait for send!", count)
                                  };

            ShowMessageBaloon(mailBalloon, (int)Properties.Settings.Default.NotifyTimeOut.TotalMilliseconds);
        }

        /// <summary>
        /// Shows the balloon error messages.
        /// </summary>
        /// <param name="text">The text.</param>
        private void ShowBalloonErrorMessages(string text)
        {
            var balloon = new ErrorBalloon
                              {
                                  BalloonTitle = "Processing mail fail:", 
                                  BalloonText = text
                              };
            ShowMessageBaloon(balloon, (int)Properties.Settings.Default.NotifyTimeOut.TotalMilliseconds);
        }

        /// <summary>
        /// The binding object for current icon.
        /// </summary>
        public class CurrentIconBindingObject : DependencyObject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Windows.DependencyObject"/> class. 
            /// </summary>
            public CurrentIconBindingObject()
            {
                CurrentIconProperty = DependencyProperty.Register(
                    "CurrentIcon",
                    typeof(Icon),
                    typeof(CurrentIconBindingObject));
            }

            /// <summary>
            /// The binding property for current icon.
            /// </summary>
            public DependencyProperty CurrentIconProperty { get; set; }

            /// <summary>
            /// Gets or sets the current icon.
            /// </summary>
            /// <value>
            /// The current icon.
            /// </value>
            public Icon CurrentIcon
            {
                get { return (Icon)GetValue(CurrentIconProperty); }
                set { SetValue(CurrentIconProperty, value); }
            }
        }
    }
}
