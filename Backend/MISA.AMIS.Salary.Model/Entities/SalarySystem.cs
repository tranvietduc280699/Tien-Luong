using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.Model.Entities
{
    [Table("pa_salary_composition_system")]
    public class SalarySystem
    {
       /// <summary>
       /// khóa chính tự tăng
       /// </summary>
        [Key]
        [Column("salary_composition_system_id")]
        public int SalaryCompositionSystemId { get; set; }
        /// <summary>
        /// mã thành phần
        /// </summary>
        [Column("salary_composition_system_code")]
        public string SalaryCompositionSystemCode { get; set; }
        /// <summary>
        /// tên thành phần
        /// </summary>
        [Column("salary_composition_system_name")]
        public string SalaryCompositionSystemName { get; set; }
        /// <summary>
        /// loại thành phần
        /// </summary>
        [Column("component_type")]
        public int ComponentType { get; set; }
        /// <summary>
        /// tính chất
        /// </summary>
        [Column("nature")]
        public int Nature { get; set; }
        /// <summary>
        /// kiểu giá trị
        /// </summary>
        [Column("salary_value_type")]
        public int SalaryValueType { get; set; }
        /// <summary>
        /// giá trị
        /// </summary>
        [Column("salary_value")]
        public string SalaryValue { get; set; }
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
