using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HelpChatHelper.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly string _originalText;

        public SettingsWindow(string originalText_)
        {
            InitializeComponent();
            _originalText = originalText_;
            _nameTextField.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(string.IsNullOrWhiteSpace(_nameTextField.Text) 
                ? _originalText 
                : $"@{_nameTextField.Text} {_originalText}");
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
