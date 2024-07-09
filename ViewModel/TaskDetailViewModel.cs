using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskPulse.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using TaskPulse.Services;

namespace TaskPulse.ViewModel;

[QueryProperty("TaskItemId", "TaskItemId")]
public partial class TaskDetailViewModel : ObservableObject
{
    private TaskItem _taskItem;
    private TaskService _taskService;

    [ObservableProperty]
    private int _taskItemId;

    public TaskDetailViewModel(MainViewModel mainViewModel) 
    {
        _taskService = new TaskService();
        InitializeAsync();
    }
    private async Task InitializeAsync()
    {
        _taskItem = await GetTargetTaskItemAsync();
        Title = _taskItem.Title;
        Note = _taskItem.Note;
        IsDone = _taskItem.IsDone;
    }

    [ObservableProperty]
    private string _title;
    [ObservableProperty]
    private string _note;
    [ObservableProperty]
    private bool _isDone;

    private async Task<TaskItem> GetTargetTaskItemAsync()
    {
        return await _taskService.GetTaskItemById(_taskItemId);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        if (_taskItem != null)
        {
            _taskService.UpdateTask(_taskItem);
        }
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task RemoveTask()
    {
        _taskService.RemoveTask(_taskItem);
        _taskItem = null;
        await GoBack();
    }
}
