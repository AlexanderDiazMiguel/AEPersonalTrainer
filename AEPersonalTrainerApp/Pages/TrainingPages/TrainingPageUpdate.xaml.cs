using AEPersonalTrainerApp.ViewModels.TrainingViewModels;

namespace AEPersonalTrainerApp.Pages.TrainingPages;

public partial class TrainingPageUpdate : ContentPage
{
	public TrainingPageUpdate()
	{
		InitializeComponent();
	}

	private void SaveCommand(object sender, EventArgs e)
	{
        TrainingViewModel vm = new TrainingViewModel(App.Database);
		vm.Name = NameEntry.Text;
		vm.Description = DescriptionEntry.Text;

		vm.SaveTrainingCommand.Execute(null);
    }

}