using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TaskPulse.Data;
using TaskPulse.Interfaces;
using TaskPulse.Model;

namespace TaskPulse.Repositories;
public class TaskRepository(ApplicationDbContext context) : ITaskRepository
{
    public const int MAX_ITEMS = 100;
    private readonly ApplicationDbContext _context = context;
    public ObservableCollection<TaskItem> GetAll()
    {
        return _context.TaskItems.AsNoTracking().Take(MAX_ITEMS).ToObservableCollection();
    }
    public async Task<TaskItem?> GetByIdAsync(int taskItemId) => await _context.TaskItems.AsNoTracking().FirstOrDefaultAsync(task => task.Id == taskItemId);
    public async Task AddAsync(TaskItem newTaskItem)
    {
        await _context.TaskItems.AddAsync(newTaskItem);
        await SaveAsync();
    }
    public async Task<bool> DeleteByIdAsync(int id)
    {
        TaskItem? targetTaskItem = _context.TaskItems.FirstOrDefault(task => task.Id == id);

        if(targetTaskItem is not null)
        {
            _context.TaskItems.Remove(targetTaskItem);
            return await SaveAsync();
        }

        return false;
    }
    public async Task<bool> UpdateAsync(TaskItem taskItemToUpdate)
    {
        _context.TaskItems.Update(taskItemToUpdate);
        return await SaveAsync();
    }
    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
