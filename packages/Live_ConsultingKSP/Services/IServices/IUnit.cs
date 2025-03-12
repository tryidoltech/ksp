using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IUnit
    {
        void AddUnit(AcUnit acUnit);
        List<AcUnit> GetAllUnit(string cmpuuid, string envuuid);
        AcUnit GetByUnit(Guid uuid);
        void UpdateUnit(AcUnit unit);
        void DeleteUnit(Guid uuid);
    }
}
