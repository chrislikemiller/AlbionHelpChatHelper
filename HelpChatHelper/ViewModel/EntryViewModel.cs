using HelpChatHelper.Util;
using HelpChatHelper.View;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Windows.Input;

namespace HelpChatHelper.ViewModel
{
    public class EntryViewModel : BaseViewModel
    {
        private string text;
        private string id;

        public EntryViewModel()
        {
            OpenCommand = new Command(() =>
            {
                var window = new SettingsWindow(Text);
                window.ShowDialog();
            });
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICommand OpenCommand { get; set; }
        
        public string ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
    }
}
