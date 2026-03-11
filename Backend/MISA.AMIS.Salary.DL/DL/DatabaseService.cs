using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace MISA.AMIS.Salary.DL
{
    /// <summary>
    /// Interface định nghĩa chức năng tạo kết nối db
    /// </summary>
    public interface IDatabaseService
    {
        IDbConnection CreateConnection();
    }
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;
        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// mở db cho toàn hệ thống
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            // Lấy connection string trong appsettings.json 
            return new MySqlConnection(_configuration.GetConnectionString("DBConnectionString"));
        }
    }
}
