using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IProjectMeetingServices
    {
        IEnumerable<ProjectProjectMeeting> GetAllProjectMeeting(string cmpuuid, string envuuid);
        Task<ProjectProjectMeeting?> GetProjectMeetingByUuid(string uuid);
        void AddProjectMeeting(ProjectProjectMeeting model);
        Task UpdateProjectMeeting(ProjectProjectMeeting model);
        void DeleteProjectMeeting(string uuid);
    }
}
