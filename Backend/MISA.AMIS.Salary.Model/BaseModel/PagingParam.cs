using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.Model.BaseModel
{
    public class PagingParam
    {
        ///<summary>
        ///trang thứ mấy
        /// </summary>\
        public int PageIndex { get; set; }
        ///<summary>
        ///kích thước trang
        /// </summary>
        public int PageSize { get; set; }
   
        ///<summary>
        ///trạng thái (1 , 0 , null)
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// danh sách cột "salary_composition_code;salary_composition_name"
        /// </summary>
        public string? Columns { get; set; }
        /// <summary>
        /// từ khóa tìm kiếm
        /// </summary>
        public string? KeyWord { get; set; }
        /// <summary>
        /// tìm theo trường cơ cấu tổ chức
        /// </summary>
        public List<Guid>? KeyOrganization { get; set; }
        /// <summary>
        ///  cột "organization_unit_data "
        /// </summary>
        public string? ColumnOrganization { get; set; }
        /// <summary>
        /// trường để sắp xếp giảm dần 
        /// </summary>
        public string? OrderBy { get; set; } = "created_at DESC";
    }
}
