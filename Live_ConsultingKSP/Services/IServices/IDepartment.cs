using Live_ConsultingKSP.Models;
using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Services
{
    public interface IDepartment
    {
        void AddDepartment(MasterDepartment department);
        List<MasterDepartment> GetAllDepartments(string cmpuuid, string envuuid);
        MasterDepartment GetDepartmentByUUID(Guid uuid);
        void UpdateDepartment(MasterDepartment department);
        void DeleteDepartment(Guid uuid);
    }
}
