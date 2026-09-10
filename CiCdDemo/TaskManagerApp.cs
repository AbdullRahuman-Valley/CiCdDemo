using CiCdDemo.Models;
using CiCdDemo.Services;

namespace CiCdDemo;

public class TaskManagerApp
{
    private readonly TaskManager _taskManager = new();

    public void Run()
    {
        Console.WriteLine("Developer Task Manager");
        Console.WriteLine("======================");

        while (true)
        {
            PrintMenu();

            string? choice = Console.ReadLine();
            if (choice is null)
            {
                // End of input (e.g. non-interactive/piped run): exit cleanly.
                Console.WriteLine("No more input. Goodbye!");
                return;
            }

            switch (choice.Trim())
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ListTasks();
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 4.");
                    break;
            }
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Menu:");
        Console.WriteLine("  1. Add Task");
        Console.WriteLine("  2. List Tasks");
        Console.WriteLine("  3. Complete Task");
        Console.WriteLine("  4. Exit");
        Console.Write("Select an option: ");
    }

    private void AddTask()
    {
        Console.Write("Title: ");
        string title = (Console.ReadLine() ?? string.Empty).Trim();
        if (title.Length == 0)
        {
            Console.WriteLine("Title cannot be empty. Task not added.");
            return;
        }

        Console.Write("Description: ");
        string description = (Console.ReadLine() ?? string.Empty).Trim();

        TaskItem task = _taskManager.AddTask(title, description);
        Console.WriteLine($"Added task #{task.Id}: {task.Title}");
    }

    private void ListTasks()
    {
        if (_taskManager.Tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet.");
            return;
        }

        Console.WriteLine("Tasks:");
        foreach (TaskItem task in _taskManager.Tasks)
        {
            string status = task.IsCompleted ? "x" : " ";
            Console.WriteLine($"  [{status}] #{task.Id} {task.Title}");
            if (task.Description.Length > 0)
            {
                Console.WriteLine($"        {task.Description}");
            }
        }
    }

    private void CompleteTask()
    {
        Console.Write("Enter task id to complete: ");
        string input = (Console.ReadLine() ?? string.Empty).Trim();

        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid id. Please enter a number.");
            return;
        }

        if (_taskManager.CompleteTask(id))
        {
            Console.WriteLine($"Task #{id} marked as complete.");
        }
        else
        {
            Console.WriteLine($"No incomplete task found with id #{id}.");
        }
    }
}
