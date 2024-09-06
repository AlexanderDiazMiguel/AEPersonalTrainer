using AEPersonalTrainerApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AEPersonalTrainerApp.ViewModels.TrainingViewModels
{
    public class TrainingsViewModel : BaseViewModel
    {
        public ObservableCollection<Training> Trainings { get; set; }
        public ICommand AddTrainingCommand { get; }
        public ICommand TrainingSelectedCommand { get; }

        public TrainingsViewModel()
        {
            Trainings = new ObservableCollection<Training>();
            AddTrainingCommand = new Command(OnAddTraining);
            TrainingSelectedCommand = new Command<Training>(OnTrainingSelected);

            // Load trainings from database
            LoadTrainings();
        }

        private void LoadTrainings()
        {
            // Load trainings from the database and add to Trainings collection
        }

        private async void OnAddTraining()
        {
            await Shell.Current.GoToAsync("AddPage");
        }

        private async void OnTrainingSelected(Training training)
        {
            if (training == null)
                return;

            // Navigate to the details page
            await Shell.Current.GoToAsync($"DetailsPage?trainingId={training.Id}");
        }
    }
}