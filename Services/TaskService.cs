using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TaskPulse.Model;

namespace TaskPulse.Services;
public class TaskService
{
    public ObservableCollection<TaskItem> GetAllTasks()
    {
        using (AppDatabaseContext databaseContext = new AppDatabaseContext())
        {
            return databaseContext.TaskItems.ToObservableCollection();
        }
    }

    public TaskItem GetTaskItemById(int taskItemId)
    {
        using (AppDatabaseContext databaseContext = new AppDatabaseContext())
        {
            return databaseContext.TaskItems.SingleOrDefault(task => task.Id == taskItemId);
        }
    }

    public void RewriteTasks(ObservableCollection<TaskItem> updatedTaskItems)
    {
        using (AppDatabaseContext databaseContext = new AppDatabaseContext())
        {
            databaseContext.TaskItems.RemoveRange(databaseContext.TaskItems);
            databaseContext.TaskItems.AddRange(updatedTaskItems);
            databaseContext.SaveChanges();
        }
    }

    public void AddTask(TaskItem updatedTaskItem)
    {
        using (AppDatabaseContext databaseContext = new AppDatabaseContext())
        {
            databaseContext.TaskItems.Add(updatedTaskItem);
            databaseContext.SaveChanges();
        }
    }

    public void UpdateTask(TaskItem updatedTaskItem)
    {
        using (AppDatabaseContext databaseContext = new AppDatabaseContext())
        {
            TaskItem existingTaskItem = databaseContext.TaskItems.Find(updatedTaskItem.Id);
            if (existingTaskItem != null)
            {
                existingTaskItem.Title = updatedTaskItem.Title;
                existingTaskItem.Note = updatedTaskItem.Note;
                existingTaskItem.IsDone = updatedTaskItem.IsDone;
                databaseContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"TaskItem with ID {updatedTaskItem.Id} not found.");
            }
        }
    }

    public void RemoveTask(TaskItem taskItemToRemove)
    {
        using (AppDatabaseContext databaseContext = new AppDatabaseContext())
        {
            var existingTask = databaseContext.TaskItems.Find(taskItemToRemove.Id);
            if (existingTask != null)
            {
                databaseContext.TaskItems.Remove(existingTask);
                databaseContext.SaveChanges();
            }
            else
            {
                Debug.WriteLine($"TaskItem with ID {taskItemToRemove.Id} not found for removal.");
            }
        }
    }
}
