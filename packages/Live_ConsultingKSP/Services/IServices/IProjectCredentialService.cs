using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IProjectCredentialService
    {

        void AddProjectCredential(ProjectProjectCrendential credential);
        List<ProjectProjectCrendential> GetAllProjectCredentials(string cmpuuid, string envuuid);
        ProjectProjectCrendential GetByProjectCredential(Guid uuid);
        void UpdateProjectCredential(ProjectProjectCrendential credential);
        void DeleteProjectCredential(Guid uuid);



    }
}
