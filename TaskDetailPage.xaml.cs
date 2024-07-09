using TaskPulse.ViewModel;

namespace TaskPulse;

public partial class TaskDetailPage : ContentPage
{
	public TaskDetailPage(TaskDetailViewModel taskDetailViewModel)
	{
		InitializeComponent();
		BindingContext = taskDetailViewModel;
	}
}