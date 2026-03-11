using MISA.AMIS.Salary.Model.BaseModel;
using MISA.AMIS.Salary.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Salary.BL.IBL.IBaseBL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// lấy tấy cả bản ghi (stored procedure)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// lấy bản ghi theo id (stored procedure)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(string? id);
        /// <summary>
        /// xóa theo id (stored procedure)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteById(string id);
    
        /// <summary>
        /// thêm mới 1 bản ghì (stored procedure)
        /// </summary>
        /// <param name="newob"></param>
        /// <returns></returns>
        Task<ResultReponse> Insert(T obj);
        /// <summary>
        /// cập nhật theo id  (stored procedure)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultReponse> Update(T obj);
        
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<PagedResult<T>> GetPaging(PagingParam paging);
    }
}
