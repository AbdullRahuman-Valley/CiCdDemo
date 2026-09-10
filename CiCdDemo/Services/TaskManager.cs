using CiCdDemo.Models;

namespace CiCdDemo.Services;

public class TaskManager
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public IReadOnlyList<TaskItem> Tasks => _tasks;

    public TaskItem AddTask(string title, string description)
    {
        var task = new TaskItem
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            IsCompleted = false,
        };

        _tasks.Add(task);
        return task;
    }

    public bool CompleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null || task.IsCompleted)
        {
            return false;
        }

        task.IsCompleted = true;
        return true;
    }
}
