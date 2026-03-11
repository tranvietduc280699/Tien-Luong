using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.Model
{
    [Table("pa_organization")]
    public class Organization
    {
        /// <summary>
        /// đơn vị tổ chức
        /// </summary>
        [Key]
        [Column("organization_unit_id")]
        public Guid OrganizationUnitId { get; set; }
        /// <summary>
        /// đơn vị cha
        /// </summary>
        [Column("parent_id")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// mã đơn vị tổ chức
        /// </summary>
        [Column("organization_unit_code")]
        public string OrganizationUnitCode { get; set; }
        /// <summary>
        /// tên đơn vị tổ chức
        /// </summary>
        [Column("organization_unit_name")]
        public string OrganizationUnitName { get; set; }
        /// <summary>
        /// mã misa
        /// </summary>
        [Column("misa_code")]
        public string MisaCode { get; set; }
        /// <summary>
        /// thứ tự hiển thị
        /// </summary>
        [Column("sort_order")]
        public int SortOrder { get; set; }
        /// <summary>
        /// ngừng sử dụng
        /// </summary>
        [Column("inactive")]
        public bool Inactive { get; set; }
        /// <summary>
        /// đánh dấu xóa mềm
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// ngày tạo
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// ngày cập nhật
        /// </summary>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }


    }
}
