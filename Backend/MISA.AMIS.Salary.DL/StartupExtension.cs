using Microsoft.Extensions.DependencyInjection;
namespace MISA.AMIS.Salary.DL
{
    /// <summary>
    /// hợp đồng (Dependency Injection)
    /// </summary>
    public static class StartupExtension
    {
        public static void UseSalaryDL(this IServiceCollection services)
        {
            // 1 request gọi vào thì khởi tạo, request tiếp theo thì ko khởi tạo
            services.AddScoped<IDatabaseService, DatabaseService>();
        }
    }
}
