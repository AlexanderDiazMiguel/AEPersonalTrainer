using AEPersonalTrainerApp.Data;
using AEPersonalTrainerApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AEPersonalTrainerApp.ViewModels.TrainingViewModels
{
    public class TrainingItemViewModel : BaseViewModel
    {
        public ObservableCollection<TrainingItem> Items { get; set; } = new ObservableCollection<TrainingItem>();
        public ICommand LoadItemsCommand { get; }
        public ICommand SaveItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand AddItemToTrainingCommand { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private readonly Database _database;
        private int _trainingId;

        public TrainingItemViewModel(Database database, int trainingId)
        {
            _database = database;
            _trainingId = trainingId;

            LoadItemsCommand = new Command(async () => await LoadItemsAsync());
            SaveItemCommand = new Command(async () => await SaveItemAsync());
            DeleteItemCommand = new Command<TrainingItem>(async (item) => await DeleteItemAsync(item));
            AddItemToTrainingCommand = new Command<TrainingItem>(async (item) => await AddItemToTrainingAsync(item));

            Task.Run(LoadItemsAsync);
        }

        private async Task LoadItemsAsync()
        {
            var itemsFromDb = await _database.GetItemsByTrainingAsync(_trainingId);
            Items.Clear();
            foreach (var item in itemsFromDb)
            {
                Items.Add(item);
            }
        }

        private async Task SaveItemAsync()
        {
            var newItem = new TrainingItem { Name = Name, Description = Description };
            await _database.SaveTrainingItemAsync(newItem);

            Name = string.Empty;
            Description = string.Empty;

            await LoadItemsAsync();
        }

        private async Task DeleteItemAsync(TrainingItem item)
        {
            if (item != null)
            {
                await _database.DeleteTrainingItemAsync(item);
                await LoadItemsAsync();
            }
        }

        private async Task AddItemToTrainingAsync(TrainingItem item)
        {
            if (item != null)
            {
                await _database.AddTrainingItemToTrainingAsync(_trainingId, item.Id);
                await LoadItemsAsync();
            }
        }
    }
}