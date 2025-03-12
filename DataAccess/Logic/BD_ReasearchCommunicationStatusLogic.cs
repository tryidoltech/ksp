using DataAccess.Entities;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class BD_ReasearchCommunicationStatusLogic : BD_ReasearchCommunicationStatusRepository
    {
        private AppDbContext _context;
        public BD_ReasearchCommunicationStatusLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<BDReasearchCommunicationStatusModel> getBDCommunicationStatusSubModel()
        {
            List<BDReasearchCommunicationStatusModel> BDCS = (from c in _context.BD_ReasearchCommunicationStatus.Where(q => q.IsActive == true)
                                                  join s in _context.BD_CommunicationHistory.Where(s => s.IsActive == true)
                                                      on c.History_UUID equals s.UUID

                                                  select new BDReasearchCommunicationStatusModel
                                                  {
                                                      CM = s,
                                                      CTY = c,
                                                      UUID = s.UUID,
                                                      History_Type_Name = s.History_Type_Name,
                                                  }).ToList();
            return BDCS;
        }
    }
}

    

