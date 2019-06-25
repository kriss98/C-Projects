namespace Torshia.Services.Contracts
{
    using System.Collections.Generic;

    using Torshia.Models.Enums;
    using Torshia.ViewModels.Tasks;

    public interface ITasksService
    {
        ICollection<TaskViewModel> AllNonReported();

        void CreateTask(
            string title,
            string description,
            string dueDate,
            string participants,
            ICollection<string> affectedSectors);

        ICollection<Sector> GetAffectedSectors(int taskId);

        TaskDetailsViewModel GetTaskById(int id);
    }
}