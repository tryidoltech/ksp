using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterStateServices
    {
        IEnumerable<MasterState> GetAllStates(string cmpuuid, string envuuid);
        void AddState(MasterState state);
       
        MasterState GetStatesByUUID(Guid uuid);
        void UpdateState(MasterState state);
        void DeleteState(Guid uuid);
        IEnumerable<MasterState> GetStatesByCountry(string countryUuid);
    }
}
