using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using ClientManagerNotifier.ViewModels;

namespace ClientManagerNotifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            var model = new MainWindowViewModel();

            DataContext = model;
            InitializeComponent();
            Hide();

            // Setup handlers 
            model.CloseEventHandler += Close;
            model.ShowMessageBaloon += ShowBalloonHandler;
            model.CallWithDispatcher += act => _taskbarIcon.Dispatcher
                .BeginInvoke(DispatcherPriority.Normal, act);
            
            // Setup tray icon
            _taskbarIcon.TrayMouseDoubleClick += (sender, args) => model.SendLetters.Execute();
            _taskbarIcon.Icon = model.CurrentIcon.CurrentIcon;

            // When I try to realize it with binding, it's fail so I find another way.
            var prop = DependencyPropertyDescriptor.FromProperty(model.CurrentIcon.CurrentIconProperty, 
                typeof(MainWindowViewModel.CurrentIconBindingObject));
            prop.AddValueChanged(model.CurrentIcon, IconValueChanged);
        }

        /// <summary>
        /// Icons the value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void IconValueChanged(object sender, EventArgs e)
        {
            _taskbarIcon.Icon = ((MainWindowViewModel.CurrentIconBindingObject)sender).CurrentIcon;
        }

        /// <summary>
        /// Shows the balloon messages.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="timeout">The timeout.</param>
        private void ShowBalloonHandler(UserControl control, int timeout)
        {
            _taskbarIcon.ShowCustomBalloon(control, PopupAnimation.Slide, timeout);
        }
    }
}
