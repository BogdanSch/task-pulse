using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskPulse.Model;
using TaskPulse.Repositories;

namespace TaskPulse.ViewModel;

[QueryProperty(nameof(TaskItemId), nameof(TaskItemId))]
public partial class TaskDetailViewModel : ObservableObject
{
    private TaskItem _taskItem;
    private TaskRepository _taskService;

    [ObservableProperty]
    private int _taskItemId;

    [ObservableProperty]
    private string _title;
    [ObservableProperty]
    private string _note;
    [ObservableProperty]
    private bool _isDone;

    public TaskDetailViewModel()
    {
        _taskService = new TaskRepository();
        Shell.Current.NavigatedFrom += OnNavigatedFrom;
    }

    ~TaskDetailViewModel()
    {
        Shell.Current.NavigatedFrom -= OnNavigatedFrom;
    }

    private void OnNavigatedFrom(object? sender, NavigatedFromEventArgs e)
    {
        if (_taskItem != null)
        {
            _taskItem.Title = Title;
            _taskItem.Note = Note;
            _taskItem.IsDone = IsDone;
            _taskService.UpdateTask(_taskItem);
        }
    }

    public void InitializeTaskItem()
    {
        if (TaskItemId > 0)
        {
            _taskItem = GetTargetTaskItem();
            if (_taskItem != null)
            {
                Title = _taskItem.Title;
                Note = _taskItem.Note;
                IsDone = _taskItem.IsDone;
            }
        }
    }

    private TaskItem GetTargetTaskItem()
    {
        return _taskService.GetTaskItemById(TaskItemId);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        if (_taskItem != null)
        {
            _taskItem.Title = Title;
            _taskItem.Note = Note;
            _taskItem.IsDone = IsDone;
            _taskService.UpdateTask(_taskItem);
        }
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task RemoveTask()
    {
        if (_taskItem != null)
        {
            _taskService.RemoveTask(_taskItem);
        }
        await GoBack();
    }
}
