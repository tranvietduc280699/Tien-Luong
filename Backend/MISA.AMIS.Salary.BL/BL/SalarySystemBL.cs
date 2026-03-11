using MISA.AMIS.Salary.DL;
using MISA.AMIS.Salary.BL.IBL;
using MISA.AMIS.Salary.Model;
using MISA.AMIS.Salary.Model.BaseModel;
using MISA.AMIS.Salary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.BL
{
    public class SalarySystemBL: BaseBL<SalarySystem>, ISalarySystemBL
    {
        public SalarySystemBL(IDatabaseService connection): base(connection)

        {

        }
       
    }
}
