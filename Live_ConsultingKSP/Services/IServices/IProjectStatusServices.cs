using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IProjectStatusServices
    {
        Task<IEnumerable<ProjectProjectStatus>> GetAllProjectStatusAsync(string cmpuuid, string envuuid);

        Task<ProjectProjectStatus?> GetProjectStatusByUuidAsync(string uuid);
        Task AddProjectStatusAsync(ProjectProjectStatus model);
        Task UpdateProjectStatusAsync(ProjectProjectStatus model);
        void DeleteProjectStatus(string uuid);
    }
}
