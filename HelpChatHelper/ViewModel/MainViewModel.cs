using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HelpChatHelper.Util;
using HelpChatHelper.View;

namespace HelpChatHelper.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private EntryViewModel _selectedEntry;
        private string _filterText;

        public MainViewModel()
        {
            Config = new Config<EntryViewModel>();
            AddNewEntryCommand = new Command(() =>
            {
                var window = new AddNewEntryWindow();
                window.ShowDialog();
                if (window.NewEntry != null)
                {
                    Config.AddConfigItem(window.NewEntry);
                    OnPropertyChanged(nameof(Entries));
                }
            });
            DeleteEntryCommand = new Command(() =>
            {
                if (SelectedEntry != null)
                {
                    Config.RemoveConfigItem(SelectedEntry);
                    OnPropertyChanged(nameof(Entries));
                }
            });
        }

        public ICommand AddNewEntryCommand { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public Config<EntryViewModel> Config { get; }

        public IEnumerable<EntryViewModel> Entries
        {
            get
            {
                return (from item in Config.Values
                        where item.ID.Contains(_filterText ?? string.Empty) || item.Text.Contains(_filterText)
                        orderby item.ID
                        select item).ToList();
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Entries));
            }
        }

        public EntryViewModel SelectedEntry
        {
            get => _selectedEntry;
            set
            {
                _selectedEntry = value;
                OnPropertyChanged();
            }
        }
    }
}
