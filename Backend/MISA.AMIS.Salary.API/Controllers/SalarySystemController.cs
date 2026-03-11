using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Salary.API.Controllers.BaseControllers;
using MISA.AMIS.Salary.BL;
using MISA.AMIS.Salary.BL.IBL;
using MISA.AMIS.Salary.BL.IBL.IBaseBL;
using MISA.AMIS.Salary.Model;
using MISA.AMIS.Salary.Model.BaseModel;
using MISA.AMIS.Salary.Model.Entities;

namespace MISA.AMIS.Salary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalarySystemController : BaseControllers<SalarySystem>
    {
        public SalarySystemController(ISalarySystemBL baseBL) : base(baseBL)
        {
        }
    }

}
