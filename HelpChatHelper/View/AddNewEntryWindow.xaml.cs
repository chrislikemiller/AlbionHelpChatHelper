using HelpChatHelper.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace HelpChatHelper.View
{
    /// <summary>
    /// Interaction logic for AddNewEntryWindow.xaml
    /// </summary>
    public partial class AddNewEntryWindow : Window
    {
        public AddNewEntryWindow()
        {
            InitializeComponent();
            _idTextField.Focus();
        }

        public EntryViewModel NewEntry { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewEntry = new EntryViewModel()
            {
                ID = _idTextField.Text,
                Text = _nameTextField.Text
            };
            Close();
        }

        private void _nameTextField_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, null);
            }
        }
    }
}
