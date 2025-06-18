using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Model;

namespace TaskPulse.Interfaces
{
    public interface ITaskRepository
    {
        ObservableCollection<TaskItem> GetAll();
        Task<TaskItem?> GetByIdAsync(int taskItemId);
        Task AddAsync(TaskItem newTaskItem);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> UpdateAsync(TaskItem taskItemToUpdate);
        Task<bool> SaveAsync();
    }
}
