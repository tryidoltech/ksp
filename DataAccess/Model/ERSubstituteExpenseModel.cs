using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Model;

namespace DataAccess.Model
{
    public class ERSubstituteExpenseModel
    {
        public ER_ExpenseType CM { get; set; }
        public ER_SubstituteExpense CTY { get; set; }

        public string Primary_ExpenseType_Name { get; set; }

        public string Secoundary_ExpenseType_Name { get; set; }
        public string UUID { get; set; }



        public ERSubstituteExpenseModel()
        {
            CM = new ER_ExpenseType();
            CTY = new ER_SubstituteExpense();

        }
    }
}