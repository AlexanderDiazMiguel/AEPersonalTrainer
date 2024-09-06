using AEPersonalTrainerApp.Data;
using AEPersonalTrainerApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AEPersonalTrainerApp.ViewModels.TrainingViewModels
{
    public class TrainingViewModel : BaseViewModel
    {
        public ObservableCollection<Training> Trainings { get; set; } = new ObservableCollection<Training>();
        public ICommand LoadTrainingsCommand { get; }
        public ICommand SaveTrainingCommand { get; }
        public ICommand DeleteTrainingCommand { get; }

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

        public TrainingViewModel() { }

        public TrainingViewModel(Database database)
        {
            _database = database;

            LoadTrainingsCommand = new Command(async () => await LoadTrainingsAsync());
            SaveTrainingCommand = new Command(async () => await SaveTrainingAsync());
            DeleteTrainingCommand = new Command<Training>(async (training) => await DeleteTrainingAsync(training));

            Task.Run(LoadTrainingsAsync);
        }

        private async Task LoadTrainingsAsync()
        {
            var trainingsFromDb = await _database.GetTrainingsAsync();
            Trainings.Clear();
            foreach (var training in trainingsFromDb)
            {
                Trainings.Add(training);
            }
        }

        private async Task SaveTrainingAsync()
        {
            var newTraining = new Training { Name = Name, Description = Description };
            await _database.SaveTrainingAsync(newTraining);

            Name = string.Empty;
            Description = string.Empty;

            await LoadTrainingsAsync();
        }

        private async Task DeleteTrainingAsync(Training training)
        {
            if (training != null)
            {
                await _database.DeleteTrainingAsync(training);
                await LoadTrainingsAsync();
            }
        }
    }
}