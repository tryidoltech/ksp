using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface ICreateProjectServices
    {
        Task<IEnumerable<ProjectCreateProject>> GetAllCreateProjectAsync(string cmpuuid, string envuuid);
        Task<ProjectCreateProject?> GetCreateProjectByUuidAsync(string uuid);
        Task AddCreateProjectAsync(ProjectCreateProject result);
        Task UpdateCreateProjectAsync(ProjectCreateProject model);
        void DeleteProject(string uuid);
    }
}
