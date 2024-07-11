using TaskPulse.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskPulse.Services;
using System.Collections.ObjectModel;

namespace TaskPulse.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<TaskItem> _taskItems;
    [ObservableProperty]
    private string _inputText;

    private readonly TaskService _taskService = new TaskService();

    public MainViewModel()
    {
        LoadTasks();
        Shell.Current.NavigatedTo += OnNavigatedTo;
    }

    ~MainViewModel()
    {
        Shell.Current.NavigatedTo -= OnNavigatedTo;
    }

    private void LoadTasks()
    {
        TaskItems = _taskService.GetAllTasks();
    }

    private void SaveTasks()
    {
        _taskService.RewriteTasks(TaskItems);
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
