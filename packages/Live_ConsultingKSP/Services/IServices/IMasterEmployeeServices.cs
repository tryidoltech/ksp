using Live_ConsultingKSP.Models;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IMasterEmployeeServices

    {
        Task<bool> Exists(string uuid);
       
        Task<Login> GetEmployeeForLoginAsync(string username, string password);
        Task<List<MasterEmployee>> GetAllEmployees(string cmpuuid, string envuuid);
        Task<MasterEmployee> GetEmployeeByUUID(string uuid);
        Task AddEmployee(MasterEmployee employee);
        Task UpdateEmployee(MasterEmployee employee);
        Task DeleteEmployee(string uuid);
    }
}
