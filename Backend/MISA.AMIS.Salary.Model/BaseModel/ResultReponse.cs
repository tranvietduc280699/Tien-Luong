using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.Model.BaseModel
{
    public class ResultReponse
    {
        /// <summary>
        /// thành công hay thất bại : true / false
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// lỗi cho người dùng biết
        /// </summary>
        public string? UserMsg { get; set; }
        /// <summary>
        /// mã lỗi
        /// </summary>
        public string? ErrorCode { get; set; }
        /// <summary>
        /// dữ liệu trả về
        /// </summary>
        public object? Data { get; set; }
    }
}
