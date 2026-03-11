
using MISA.AMIS.Salary.BL.IBL.IBaseBL;
using MISA.AMIS.Salary.Model.BaseModel;
using MISA.AMIS.Salary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.BL
{
    public interface ISalaryCompositionBL : IBaseBL<SalaryComposition>
    {
        // tìm kiếm theo từ khóa
        Task<IEnumerable<SalaryComposition>> GetByKeyWord(string keyWord);
        // xóa danh sách theo danh sách list id
        Task<int> DeleteMany(List<int> ids);
        // cập nhật trạng thái theo danh sách list id
        Task<int> UpdateStatusMany(List<int> ids, int status);
        // cập nhật trạng thái theo id (procedure)
        Task<int> UpdateStatus(int id, int status);
        // cập nhật trạng thái theo id
        Task<int> UpdateSingleField(int id, int FieldValue);
    }
}
