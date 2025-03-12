using Live_ConsultingKSP.Models;

public interface IDesignationService
{
    void AddDesignation(MasterDesignation designation);
    List<MasterDesignation> GetAllDesignations(string cmpuuid, string envuuid);
    MasterDesignation GetDesignationByUuid(Guid uuid); // Ensure the correct return type.
    void UpdateDesignation(MasterDesignation designation);
    void DeleteDesignation(Guid uuid);
    List<MasterDesignation> GetAllEmployeeDesignations();
}
