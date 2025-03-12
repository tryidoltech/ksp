using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class CreateProjectServices : ICreateProjectServices
    {
        private readonly KsperpDbContext _context;
        public CreateProjectServices(KsperpDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProjectCreateProject>> GetAllCreateProjectAsync(string cmpuuid, string envuuid)
        {
            return await _context.ProjectCreateProjects.Where(x => x.IsActive == true && x.MasterCompanyUuid == cmpuuid
            && x.MasterEnvironmentUuid == envuuid).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ProjectCreateProject?> GetCreateProjectByUuidAsync(string uuid)
        {
            return await _context.ProjectCreateProjects.FirstOrDefaultAsync(c => c.Uuid == uuid.ToString());
        }

        public async Task AddCreateProjectAsync(ProjectCreateProject result)
        {
            bool isDuplicate = _context.ProjectCreateProjects.Any(x => x.ProjectTitle == result.ProjectTitle);
            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            result.IsActive = true;
            result.Uuid = Guid.NewGuid().ToString();
            result.IsAddedOn = DateTime.Now;
            result.IsAddedBy = "1";
            _context.ProjectCreateProjects.Add(result);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateCreateProjectAsync(ProjectCreateProject model)
        {
            var result = await _context.ProjectCreateProjects.FirstOrDefaultAsync(b => b.Uuid == model.Uuid.ToString());

            if (result != null)
            {
                result.ProjectTitle = model.ProjectTitle;
                result.CustomerUuid = model.CustomerUuid;
                result.EmployeeUuid = model.EmployeeUuid;
                result.ProjectDescription = model.ProjectDescription;
                result.StartDate = model.StartDate;
                result.EndDate = model.EndDate;
                result.ExpectedTotalHours = model.ExpectedTotalHours;
                result.ProjectCost = model.ProjectCost;
                result.IsDisplay = model.IsDisplay;
                result.IsUpdatedBy = "1";
                result.IsUpdatedOn = DateTime.Now;
                _context.ProjectCreateProjects.Update(result);
                await _context.SaveChangesAsync();

            }
        }
        public void DeleteProject(string uuid)
        {
            var result = _context.ProjectCreateProjects.FirstOrDefault(c => c.Uuid == uuid.ToString());
            if (result != null)
            {
                result.IsActive = false;
                result.IsDeletedBy = "1";
                result.IsDeletedOn = DateTime.Now;
                _context.ProjectCreateProjects.Update(result);
                _context.SaveChanges();
            }
        }
    }
}
