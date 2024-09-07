using AEPersonalTrainerApp.Models;
using AEPersonalTrainerApp.ViewModels.TrainingViewModels;

namespace AEPersonalTrainerApp.Pages.TrainingPages;

public partial class TrainingPageList : ContentPage
{
	public TrainingPageList()
	{
		InitializeComponent();
        BindingContext = new TrainingsViewModel();
    }

    private void OnAddTrainingClicked(object sender, EventArgs e)
    {
        (BindingContext as TrainingsViewModel)?.AddTrainingCommand.Execute(null);
    }

    private void OnTrainingSelected(object sender, SelectionChangedEventArgs e)
    {
        var training = e.CurrentSelection.FirstOrDefault() as Training;
        (BindingContext as TrainingsViewModel)?.TrainingSelectedCommand.Execute(training);
    }
}