using TaskPulse.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TaskPulse.Repositories;
using TaskPulse.Interfaces;

namespace TaskPulse.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<TaskItem> _taskItems;
    [ObservableProperty]
    private string _inputText;

    private readonly ITaskRepository _taskRepository;

    public MainViewModel(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        //LoadTasks();
        //Shell.Current.NavigatedTo += OnNavigatedTo;
    }
    //~MainViewModel()
    //{
    //    Shell.Current.NavigatedTo -= OnNavigatedTo;
    //}

    private async Task LoadTasksAsync()
    {
        TaskItems = _taskService.GetAllTasks();
    }
    [RelayCommand]
    private void AddTask()
    {
        if (!string.IsNullOrWhiteSpace(InputText))
        {
            TaskItems.Add(new TaskItem(InputText));
            InputText = string.Empty;
            SaveTasks();
        }
    }

    [RelayCommand]
    private void RemoveTask(TaskItem taskItem)
    {
        if (TaskItems.Contains(taskItem))
        {
            TaskItems.Remove(taskItem);
            SaveTasks();
        }
    }

    [RelayCommand]
    private async Task TaskTap(TaskItem taskItem)
    {
        if (TaskItems.Contains(taskItem))
        {
            await Shell.Current.GoToAsync($"{nameof(TaskDetailPage)}?TaskItemId={taskItem.Id}");
        }
    }
}
