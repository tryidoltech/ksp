using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;

namespace Live_ConsultingKSP.Services
{
    public class MasterEmployeeServices : IMasterEmployeeServices
    {
        private readonly KsperpDbContext _context;
        public MasterEmployeeServices(KsperpDbContext context) 
        { 
            _context = context;
        }
        public async Task<bool> Exists(string uuid)
        {
            return await _context.MasterEmployees.AnyAsync(e => e.Uuid == uuid);
        }


        public async Task<Login> GetEmployeeForLoginAsync(string username, string password)
        {
           /* var employeeWithRights = await _context.MasterEmployees
                .Where(e => e.Username == username && e.Password == password && e.IsLoginActive == true && e.IsActive == true)
                .Select(e => new
                {
                    e.Username,
                    e.Password,
                    e.IsLoginActive,
                    e.MasterRolesUuid,
                    MenuRights = _context.MasterUserMenuRights
                        .Where(mr => mr.UserRoleUuid == e.MasterRolesUuid && mr.IsActive == true)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (employeeWithRights != null)
            {
                return new Login
                {
                    UserName = employeeWithRights.Username,
                    Password = employeeWithRights.Password,
                    IsLoginActive = employeeWithRights.IsLoginActive ?? false
                };
            }
*/
            return null;
        }



        /*  public async Task<Login> GetEmployeeForLoginAsync(string username, string password)
          {
              var employee = await _context.MasterEmployees
                  .FirstOrDefaultAsync(e => e.Username == username && e.Password == password && e.IsLoginActive == true);

              if (employee != null)
              {
                  return new Login
                  {
                      UserName = employee.Username,
                      Password = employee.Password,
                      IsLoginActive = (bool)employee.IsLoginActive
                  };
              }

              return null;
          }*/
        public async Task<List<MasterEmployee>> GetAllEmployees(string cmpuuid, string envuuid)
        {
            return await _context.MasterEmployees
                .Where(c => c.IsActive == true && c.MasterCompanyUuid == cmpuuid
            && c.MasterEnvironmentUuid == envuuid)
                .Select(e => new MasterEmployee
                {
                    Id = e.Id,
                    Uuid = e.Uuid ?? string.Empty,
                    MasterPrefixUuid = e.MasterPrefixUuid ?? string.Empty,
                    FirstName = e.FirstName ?? string.Empty,
                    MiddleName = e.MiddleName ?? string.Empty,
                    LastName = e.LastName ?? string.Empty,
                    ProfilePic = e.ProfilePic ?? string.Empty,
                    MasterBloodGroupUuid = e.MasterBloodGroupUuid ?? string.Empty,
                    MasterDepartmentUuid = e.MasterDepartmentUuid ?? string.Empty,
                    MasterGenderUuid = e.MasterGenderUuid ?? string.Empty,
                    EmployeeCode = e.EmployeeCode ?? string.Empty,
                    ExpLimitDesignation = e.ExpLimitDesignation ?? string.Empty,
                    ExpWorkflowDesignation = e.ExpWorkflowDesignation ?? string.Empty,
                    ReportingDesignation = e.ReportingDesignation ?? string.Empty,
                    MasterRolesUuid = e.MasterRolesUuid ?? string.Empty,
                    Username = e.Username ?? string.Empty,
                    Password = e.Password ?? string.Empty,
                    IsLoginActive = e.IsLoginActive,
                    PersonalEmail = e.PersonalEmail ?? string.Empty,
                    Mobile = e.Mobile ?? string.Empty,
                    Landline = e.Landline ?? string.Empty,
                    AltMobile = e.AltMobile ?? string.Empty,
                    CompanyEmail = e.CompanyEmail ?? string.Empty,
                    CompanyContactNo = e.CompanyContactNo ?? string.Empty,
                    BuildingNo = e.BuildingNo ?? string.Empty,
                    StreetAddress = e.StreetAddress ?? string.Empty,
                    StreetAddress2 = e.StreetAddress2 ?? string.Empty,
                    PostCode = e.PostCode ?? string.Empty,
                    MasterCityUuid = e.MasterCityUuid ?? string.Empty,
                    MasterCityName = e.MasterCityName ?? string.Empty,
                    MasterStateUuid = e.MasterStateUuid ?? string.Empty,
                    MasterStateName = e.MasterStateName ?? string.Empty,
                    MasterCountryUuid = e.MasterCountryUuid ?? string.Empty,
                    MasterCountryName = e.MasterCountryName ?? string.Empty,
                    CurrentBuildingNo = e.CurrentBuildingNo ?? string.Empty,
                    CurrentStreetAddress = e.CurrentStreetAddress ?? string.Empty,
                    CurrentStreetAddress2 = e.CurrentStreetAddress2 ?? string.Empty,
                    CurrentPostCode = e.CurrentPostCode ?? string.Empty,
                    CurrentMasterCityUuid = e.CurrentMasterCityUuid ?? string.Empty,
                    CurrentMasterCityName = e.CurrentMasterCityName ?? string.Empty,
                    CurrentMasterStateUuid = e.CurrentMasterStateUuid ?? string.Empty,
                    CurrentMasterStateName = e.CurrentMasterStateName ?? string.Empty,
                    CurrentMasterCountryUuid = e.CurrentMasterCountryUuid ?? string.Empty,
                    CurrentMasterCountryName = e.CurrentMasterCountryName ?? string.Empty,
                    CurrentLandmark = e.CurrentLandmark ?? string.Empty,
                    Landmark = e.Landmark ?? string.Empty,
                    BirthDate = e.BirthDate ?? default(DateOnly),
                    IsLibraryAllowed = e.IsLibraryAllowed,
                    IsTreeViewSearchAllowed = e.IsTreeViewSearchAllowed,
                    IsDisplay = e.IsDisplay
                   
                })
                .ToListAsync();
        }


        public async Task<MasterEmployee> GetEmployeeByUUID(string uuid)
        {
            return await _context.MasterEmployees.FirstOrDefaultAsync(e => e.Uuid == uuid);
        }

        public async Task AddEmployee(MasterEmployee employee)
        {
            bool isDuplicate = _context.MasterEmployees.Any(c =>
         c.EmployeeCode == employee.EmployeeCode ||
         c.PersonalEmail == employee.PersonalEmail ||
         c.Mobile == employee.Mobile);

            if (isDuplicate)
            {
                throw new Exception("Data Already Exists!");
            }
            employee.IsLoginActive = employee.IsLoginActive;
            employee.IsActive = true;
            employee.IsAddedOn = DateTime.Now;
            employee.IsAddedBy = "1"; 
            _context.MasterEmployees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(MasterEmployee employee)
        {
            var existingEmployee = await _context.MasterEmployees.FindAsync(employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.ProfilePic = employee.ProfilePic;
                existingEmployee.MasterPrefixUuid = employee.MasterPrefixUuid;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.MiddleName = employee.MiddleName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.MasterBloodGroupUuid = employee.MasterBloodGroupUuid;
                existingEmployee.MasterDepartmentUuid = employee.MasterDepartmentUuid;
                existingEmployee.MasterGenderUuid = employee.MasterGenderUuid;
                existingEmployee.EmployeeCode = employee.EmployeeCode;
                existingEmployee.ExpLimitDesignation = employee.ExpLimitDesignation;
                existingEmployee.PersonalEmail = employee.PersonalEmail;
                existingEmployee.Mobile = employee.Mobile;
                existingEmployee.ExpWorkflowDesignation = employee.ExpWorkflowDesignation;
                existingEmployee.ReportingDesignation = employee.ReportingDesignation;
                existingEmployee.MasterRolesUuid = employee.MasterRolesUuid;
                existingEmployee.Username = employee.Username;
                existingEmployee.Password = employee.Password;
                existingEmployee.IsLoginActive = employee.IsLoginActive;
                existingEmployee.Landline = employee.Landline;

                existingEmployee.IsUpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmployee(string uuid)
        {
            var employee = await _context.MasterEmployees.FirstOrDefaultAsync(e => e.Uuid == uuid);
            if (employee != null)
            {
                employee.IsActive = false;
                employee.IsDeletedOn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }

}
