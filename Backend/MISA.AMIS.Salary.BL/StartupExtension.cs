using Microsoft.Extensions.DependencyInjection;
using MISA.AMIS.Salary.BL;
using MISA.AMIS.Salary.BL.IBL;
using MISA.AMIS.Salary.BL.IBL.IBaseBL;
using MISA.AMIS.Salary.DL;
using MISA.AMIS.Salary.Model;
using MISA.AMIS.Salary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.BL
{
    /// <summary>
    /// hợp đồng (Dependency Injection)
    /// </summary>
    public static class StartupExtension
    {
        public static void UseSalaryBL(this IServiceCollection services)
        {
            services.UseSalaryDL();
            // mỗi request gọi thì khởi tạo 1 lần
            services.AddTransient<ISalaryCompositionBL, SalaryCompositionBL>();
            services.AddTransient<IOrganizationBL, OrganizationBL>();
            services.AddTransient<ISalarySystemBL, SalarySystemBL>();
        }
    }
}
