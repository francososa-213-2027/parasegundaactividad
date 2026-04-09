using System;
using System.Collections.Generic;
using System.Linq;

namespace Taskflow
{
    public class TaskService
    {
        private List<TaskItem> _tasks;

        public TaskService()
        {
            _tasks = FileManager.LoadTasks();
        }

        public void CreateTask(string title, string description, string responsible)
        {
            int nextId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
            var newTask = new TaskItem
            {
                Id = nextId,
                Title = title,
                Description = description,
                Responsible = responsible,
                Status = TaskStatus.Pendiente, // Siempre inicial en Pendiente [cite: 1]
                CreatedAt = DateTime.Now
            };
            _tasks.Add(newTask);
            FileManager.SaveTasks(_tasks);
        }

        public List<TaskItem> ListTasks(TaskStatus? filter = null)
        {
            if (filter == null) return _tasks; // Todas las tareas [cite: 1]
            return _tasks.Where(t => t.Status == filter).ToList(); // Filtradas [cite: 2]
        }

        public bool UpdateStatus(int id, TaskStatus newStatus)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Status = newStatus;
                task.UpdatedAt = DateTime.Now; // Registro de modificación [cite: 4]
                FileManager.SaveTasks(_tasks);
                return true;
            }
            return false;
        }

        public bool UpdateResponsible(int id, string newResponsible)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Responsible = newResponsible;
                task.UpdatedAt = DateTime.Now; // [cite: 4]
                FileManager.SaveTasks(_tasks);
                return true;
            }
            return false;
        }

        public bool DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                FileManager.SaveTasks(_tasks); // No debe quedar rastro en JSON 
                return true;
            }
            return false;
        }
    }
}