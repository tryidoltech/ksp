using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Repository
{
    public interface IMasterRole
    {
        void AddRole(MasterUserRole role);
        List<MasterUserRole> GetAllRoles(string cmpuuid, string envuuid);
        MasterUserRole GetRoleByUUID(Guid uuid);
        void UpdateRoles(MasterUserRole role);
        void DeleteRoles(Guid uuid);
    }
}
