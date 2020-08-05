using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace HelpChatHelper.Util
{
    public class Config<T> where T : class
    {
        private const string FILENAME = "config.json";
        private static readonly string DIRPATH = Path.Combine(Path.GetTempPath(), "HelpchatHelper");
        private static readonly string PATH = Path.Combine(DIRPATH, FILENAME);
        private ObservableCollection<T> _items;
        private IComparer<T> _customComparer;
        
        public Config(IComparer<T> customComparer = null)
        {
            _customComparer = customComparer;
        }

        public ObservableCollection<T> Values
        {
            get
            {
                if (_items == null)
                {
                    if (!File.Exists(PATH))
                    {
                        if (!Directory.Exists(DIRPATH))
                        {
                            Directory.CreateDirectory(DIRPATH);
                        }
                        using var _ = File.CreateText(PATH); // quick opening and closing of filestream
                    }

                    _items = new ObservableCollection<T>();
                    foreach (var line in File.ReadAllLines(PATH))
                    {
                        _items.Add(JsonConvert.DeserializeObject<T>(line) as T);
                    }
                }

                if (_customComparer != null)
                {
                    SortWithComparer();
                }
                return _items;
            }
        }

        private void SortWithComparer()
        {
            var temp = _items.OrderBy(x => x, _customComparer ?? Comparer<T>.Default).ToList();
            _items.Clear();
            foreach (var item in temp)
            {
                _items.Add(item);
            }
        }

        public void AddConfigItem(T item)
        {
            App.Current.Dispatcher.Invoke(() => _items.Add(item));
            PersistConfig();
        }

        public void RemoveConfigItem(T item)
        {
            App.Current.Dispatcher.Invoke(() => _items.Remove(item));
            PersistConfig();
        }

        public void ReplaceConfigItem(T oldItem, T newItem)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _items.Remove(oldItem);
                _items.Add(newItem);
            });
            PersistConfig();
        }

        private void PersistConfig() => File.WriteAllLines(PATH, _items.Select(x => JsonConvert.SerializeObject(x)));
    }
}