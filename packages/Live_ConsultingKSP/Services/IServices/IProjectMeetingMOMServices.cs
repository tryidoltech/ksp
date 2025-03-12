using Live_ConsultingKSP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IProjectMeetingMOMServices
    {
        IEnumerable<SelectListItem> GetEmployeesForDropdown();
        IEnumerable<ProjectProjectMom> GetAllProjectMeetingMOMAsync(string cmpuuid, string envuuid);
        ProjectProjectMom? GetProjectMeetingMOMByUuidAsync(string uuid);
        void AddProjectMeetingMOMAsync(ProjectProjectMom model);

        void UpdateProjectMeetingMOMAsync(ProjectProjectMom model);
        void DeleteProjectMeetingMOM(string uuid);
    }


}
