using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterMenuServices
    {
        void AddMenu(Models.MasterMenu masterMenu);
        List<Models.MasterMenu> GetAllMenu(string cmpuuid, string envuuid);
        Models.MasterMenu GetByMenu(Guid uuid);
        void UpdateMenu(Models.MasterMenu master);
        void DeleteMenu(Guid uuid);
    }
}
