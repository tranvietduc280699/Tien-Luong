using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.Model.Entities
{
    /// <summary>
    /// máp dữ liệu từ db
    /// 
    /// </summary>
    [Table("pa_salary_composition")]
    public class SalaryComposition
    {
        /// <summary>
        /// Khóa chính tự tăng
        /// DB: salary_composition_id
        /// </summary>
        [Key]
        [Column("salary_composition_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? SalaryCompositionId { get; set; }

        /// <summary>
        /// Mã thành phần
        /// DB: salary_composition_code
        /// </summary>
        [Column("salary_composition_code")]
        [Required(ErrorMessage = "Mã thành thành phần không được để trống")]
        [MaxLength(255, ErrorMessage = "Không vượt quá 255 ký tự")]
        public string SalaryCompositionCode { get; set; }

        /// <summary>
        /// Tên thành phần
        /// DB: salary_composition_name
        /// </summary>
        [Column("salary_composition_name")]
        [Required(ErrorMessage = "Tên thành thành phần không được để trống")]
        [MaxLength(255, ErrorMessage = "Không vượt quá 255 ký tự")]
        public string SalaryCompositionName { get; set; }
        /// <summary>
        /// Đơn vị áp dụng
        /// DB: organization_unit_data
        /// </summary>
        [Column("organization_unit_data")]
        public string? OrganizationUnitData { get; set; }


        /// <summary>
        /// Loại thành phần
        /// DB: component_type
        /// </summary>
        [Column("component_type")]
        [Required(ErrorMessage = "Loại thành phần không được để trống")]
        public int? ComponentType { get; set; }

        /// <summary>
        /// Tính chất
        /// DB: nature
        /// </summary>
        [Column("nature")]
        [Required(ErrorMessage = "Tính chất không được để trống")]
        public int? Nature { get; set; }

        /// <summary>
        /// Chịu thuế
        /// DB: taxable_type
        /// </summary>
        [Column("taxable_type")]
        public int TaxableType { get; set; }

        /// <summary>
        /// Giảm trừ khi tính thuế
        /// DB: is_deducted_on_tax
        /// </summary>
        [Column("is_deducted_on_tax")]
        public bool IsDeductedOnTax { get; set; }

        /// <summary>
        /// Định mức
        /// DB: quota
        /// </summary>
        [Column("quota")]
        public string? Quota { get; set; }

        /// <summary>
        /// Cho phép vượt định mức
        /// DB: is_exceed_quota
        /// </summary>
        [Column("is_exceed_quota")]
        public bool? IsExceedQuota { get; set; }

        /// <summary>
        /// Kiểu giá trị
        /// DB: salary_value_type
        /// </summary>
        [Column("salary_value_type")]
        public int? SalaryValueType { get; set; }

        /// <summary>
        /// Giá trị
        /// DB: salary_value
        /// </summary>
        [Column("salary_value")]
        public string SalaryValue { get; set; }
        /// <summary>
        /// gợi ý công thức và tham số
        /// DB: salary_value
        /// </summary>
        [Column("recipe_suggestion")]
        public string? RecipeSuggestion { get; set; }
        /// <summary>
        /// Phạm vi giá tri
        /// DB: salary_value
        /// </summary>
        [Column("value_arrgegatiton_scope")]
        public string? ValueArrgegatitonScope { get; set; }
        /// <summary>
        /// Cộng giá trị
        /// DB: salary_value
        /// </summary>
        [Column("sum_value")]
        public string SumValue { get; set; }
        /// <summary>
        /// Mô tả
        /// DB: salary_description
        /// </summary>
        [Column("salary_description")]
        public string? SalaryDescription { get; set; }

        /// <summary>
        /// Hiển thị trên phiếu lương
        /// DB: show_on_payslip
        /// </summary>
        [Column("show_on_payslip")]
        public int ShowOnPayslip { get; set; }

        /// <summary>
        /// Nguồn tạo
        /// DB: source
        /// </summary>
        [Column("source")]
        public string? Source { get; set; }

        /// <summary>
        /// Trạng thái
        /// DB: status
        /// </summary>
        [Column("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Ngày tạo
        /// DB: created_at
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// DB: updated_at
        /// </summary>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

    }
}
