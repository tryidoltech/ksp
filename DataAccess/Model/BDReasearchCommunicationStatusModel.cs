using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class BDReasearchCommunicationStatusModel
    {
        public BD_CommunicationHistory CM { get; set; }
      
        public BD_ReasearchCommunicationStatus CTY { get; set; }

        public string History_Type_Name { get; set; }

        public string UUID { get; set; }


        public BDReasearchCommunicationStatusModel()
        {
            CM = new BD_CommunicationHistory();
           
            CTY = new BD_ReasearchCommunicationStatus();

        }
    }
}
