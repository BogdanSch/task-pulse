using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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
    public async Task<TaskItem> GetTaskItemById(int TaskItemId = 0)
    {
        using (AppDatabaseContext databaseContext = new AppDatabaseContext())
        {
            return await databaseContext.TaskItems.SingleOrDefaultAsync(x => x.Id == TaskItemId);
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
    public async void UpdateTask(TaskItem updatedTaskItem)
    {
        using (AppDatabaseContext databaseContext = new AppDatabaseContext())
        {
            TaskItem existingTaskItem = await databaseContext.TaskItems.FindAsync(updatedTaskItem.Id);
            if (existingTaskItem != null)
            {
                existingTaskItem.Title = updatedTaskItem.Title;
                existingTaskItem.Note = updatedTaskItem.Note;
                existingTaskItem.IsDone = updatedTaskItem.IsDone;
                await databaseContext.SaveChangesAsync();
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
            databaseContext.TaskItems.Remove(taskItemToRemove);
            databaseContext.SaveChanges();
        }
    }
}
