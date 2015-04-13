using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;

namespace ClientManagerNotifier.Views
{
    /// <summary>
    /// Interaction logic for ErrorBalloon.xaml
    /// </summary>
    public partial class ErrorBalloon
    {
        /// <summary>
        /// Contain's balloon text.
        /// </summary>
        public static readonly DependencyProperty BalloonTextProperty =
            DependencyProperty.Register("BalloonText",
                                        typeof(string),
                                        typeof(ErrorBalloon));

        /// <summary>
        /// Contain's balloon title.
        /// </summary>
        public static readonly DependencyProperty BalloonTitleProperty =
            DependencyProperty.Register("BalloonTitle",
                                        typeof(string),
                                        typeof(ErrorBalloon));

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorBalloon"/> class.
        /// </summary>
        public ErrorBalloon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A property wrapper for the <see cref="BalloonTextProperty"/>
        /// dependency property:<br/>
        /// Description
        /// </summary>
        public string BalloonText
        {
            get { return (string)GetValue(BalloonTextProperty); }
            set { SetValue(BalloonTextProperty, value); }
        }

        /// <summary>
        /// A property wrapper for the <see cref="BalloonTextProperty"/>
        /// dependency property:<br/>
        /// Description
        /// </summary>
        public string BalloonTitle
        {
            get { return (string)GetValue(BalloonTitleProperty); }
            set { SetValue(BalloonTitleProperty, value); }
        }

        /// <summary>
        /// Controls the mouse down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ControlMouseDown(object sender, MouseButtonEventArgs e)
        {
            var taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.CloseBalloon();
        }
    }
}
