using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.Model.BaseModel
{
    public class PagedResult<T>
    {
        ///<summary>
        ///dữ liệu hiển thị
        /// </summary>
        public IEnumerable<T> Items { get; set; }
        ///<summary>
        ///tổng sau khi lọc ( trạng thái)
        /// </summary>
        public int TotalRecords { get; set; }
        ///<summary>
        ///trang thứ mấy
        /// </summary>\
        public int PageIndex { get; set; }
        ///<summary>
        ///kích thước trang
        /// </summary>
        public int PageSize { get; set; }
        ///<summary>
        ///tổng số trang
        /// </summary>
        public int TotalPages { get; set; }


    }
}
