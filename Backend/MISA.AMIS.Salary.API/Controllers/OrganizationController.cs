using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Salary.API.Controllers.BaseControllers;
using MISA.AMIS.Salary.BL;
using MISA.AMIS.Salary.BL.IBL;
using MISA.AMIS.Salary.BL.IBL.IBaseBL;
using MISA.AMIS.Salary.Model;

namespace MISA.AMIS.Salary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : BaseControllers<Organization>
    {
        public OrganizationController(IOrganizationBL baseBL) : base(baseBL)
        {
        }
    }
}
