using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ExpenseSubTypeModel
    {
        public ER_ExpenseType CM { get; set; }
        public ER_ExpenseSubType CTY { get; set; }

        public string ExpenseType_Name { get; set; }
        public string ExpenseSubType_Name { get; set; }

        public string UUID { get; set; }

        public ExpenseSubTypeModel()
        {
            CM = new ER_ExpenseType();
            CTY = new ER_ExpenseSubType();
        }

    }
}
