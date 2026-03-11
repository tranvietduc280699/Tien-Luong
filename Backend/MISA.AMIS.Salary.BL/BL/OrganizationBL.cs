using MISA.AMIS.Salary.DL;
using MISA.AMIS.Salary.BL.IBL;
using MISA.AMIS.Salary.Model;
namespace MISA.AMIS.Salary.BL
{
    public class OrganizationBL: BaseBL<Organization>, IOrganizationBL
    {
        public OrganizationBL(IDatabaseService connection) : base(connection)
        {

        }

    }
}
