using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClientManagerNotifier.ViewModels;

namespace ClientManagerNotifier.Views
{
    /// <summary>
    /// Interaction logic for UserDataEditor.xaml
    /// </summary>
    public partial class UserDataEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataEditor"/> class.
        /// </summary>
        public UserDataEditor()
        {
            DataContext = new UserDataViewModel();
            ((UserDataViewModel)DataContext).CloseWindowEvent += Close;
            InitializeComponent();
        }

        /// <summary>
        /// Passwords the box password changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            // Need's to bind password property
            ((UserDataViewModel)DataContext).UserPass = ((PasswordBox)sender).Password;
        }

        /// <summary>
        /// Windows the mouse left button down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void WindowMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Need's to drag window
            DragMove();
        }
    }
}
