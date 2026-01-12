using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        // This Is The List Where We Store Tasks In.
        private readonly List<TaskItem> tasks = new();

        // This Is Used To Generate Unique Ids For New Tasks Instead Of Identity Columns In Databases.
        private int nextId = 1;

        // Implementation Of ITaskRepository Interface Methods.

        // Get All Tasks
        public IEnumerable<TaskItem> GetAll() => tasks;

        // Get Task By Id
        public TaskItem? GetById(int id) => tasks.FirstOrDefault(T => T.Id == id);


        // Add New Task
        public void Add(TaskItem task)
        {
            // Assign A Unique Id To The New Task Which Is Auto-Incremented.
            task.Id = nextId++;
            tasks.Add(task);
        }

        public void Update(TaskItem task)
        {
            // Find The Existing Task By Id
            var existing = GetById(task.Id);
            if(existing == null)
                return;

            // Update The Properties Of The Task Already Exists
            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.IsCompleted = task.IsCompleted;

        }

        public void Delete(int id)
        {
            tasks.RemoveAll(T => T.Id == id);

            // We Can Also Use This But I Prefer The Above For Simplicity.
            //var TaskToDelete = GetById(id);
            //if(TaskToDelete == null)
            //    return;
            //tasks.Remove(TaskToDelete);

        }
    }
}

