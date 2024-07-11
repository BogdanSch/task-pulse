using TaskPulse.ViewModel;

namespace TaskPulse;

public partial class TaskDetailPage : ContentPage
{
    private TaskDetailViewModel _taskDetailViewModel;
	public TaskDetailPage(TaskDetailViewModel taskDetailViewModel)
	{
		InitializeComponent();
        _taskDetailViewModel = taskDetailViewModel;
        BindingContext = taskDetailViewModel;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _taskDetailViewModel.InitializeTaskItem();
    }
}