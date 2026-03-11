using MISA.AMIS.Salary.DL;
using MISA.AMIS.Salary.BL.IBL.IBaseBL;
using MISA.AMIS.Salary.Model.BaseModel;
using MISA.AMIS.Salary.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.Salary.DL.DatabaseHelper;

namespace MISA.AMIS.Salary.BL
{
    public  class BaseBL<T> : DatabaseHelper<T>, IBaseBL<T>
    {
        public BaseBL(IDatabaseService connection) : base(connection)
        {

        }
        /// <summary>
        /// GetAll 
        /// ( stored procedure )
        /// </summary>
        /// <returns> lấy tất cả bản ghi</returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            var proc = GetProcedureName("GetAll");

            return await GetAll(proc);
        }
        /// <summary>
        /// lấy bản ghi theo id 
        /// ( stored procedure)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<T> GetById(string? id)
        {
            var proc = GetProcedureName("GetById");
            
            return await GetById<T>(proc, id);
        }
        /// <summary>
        /// xóa bản ghi theo id 
        /// (stored procedure)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteById(string? id)
        {
            var proc = GetProcedureName("DeleteById");
            return await DeleteById(
               proc,
               id.ToString()
           );
        }

        /// <summary>
        /// thêm mới bản ghi 
        /// (stored procedure)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<ResultReponse> Insert(T obj)
        {
            var response = new ResultReponse();
            //validate dữ liệu
            var validateResult = await Validate(obj);
            if(validateResult.Success == false)
            {
                return validateResult;
            }
            //lấy tên procedure
            var functionNam = GetProcedureName("Insert");
            //gọi hàm thêm mới
            var result = await Insert(functionNam, obj);
            if (result > 0)
            {
                response.Success = true;
                response.UserMsg = "Thêm mới thành công.";
            }
            else
            {
                response.Success = false;
                response.UserMsg = "Thêm mới thất bại.";
                response.ErrorCode = "500";
            }
            return response;
        }
        /// <summary>
        /// cập nhật bản ghi theo id
        /// stored procedure
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultReponse> Update(T obj)
        {
            var response = new ResultReponse();
            var proc = GetProcedureName("Update");
            // gọi hàm cập nhật
            var result = await Update(proc, obj);
            if (result > 0)
            {
                response.Success = true;
                response.UserMsg = "Cập nhật thành công.";

            }
            else
            {
                response.Success = false;
                response.UserMsg = "Cập nhật thất bại.";
                response.ErrorCode = "500";
            }
            return response;


        }

        ///<summary>
        ///phân trang
        /// </summary>
        public async Task<PagedResult<T>> GetPaging(PagingParam paging)
        {
            // kiểm tra nếu có tổ chức có giá trị thì thêm cột tổ chức vào để phân quyền
            if (paging.KeyOrganization != null && paging.KeyOrganization.Any())
            {
                paging.ColumnOrganization = "organization_unit_data";
            }

            // lấy tên bảng table(entity)
            var type = typeof(T);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            var tableName = tableAttr?.Name;

            // loại bỏ pa_ để sinh ra tên trường
            string tableShort = tableName.Replace("pa_", "");

            // lấy trường tên thành phần và mã thành phần ( pading)
            // để sinh ra "salary_composition_code;salary_composition_name";
            string columns = $"{tableShort}_code;{tableShort}_name";
            paging.Columns = columns;
            return await GetPaging(tableName, paging);
        }

        /// <summary>
        /// method sinh tên stored procedure theo tên class và tên method
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns>tên procedure trên db</returns>
        public string GetProcedureName(string methodName)
        {
            // lấy tên bảng table(entity)
            var type = typeof(T);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            var tableName = tableAttr?.Name;

            return "procedure_" + tableName + "_" + methodName;

        }
        /// <summary>
        /// Hàm validate dữ liệu và cho phép override lại để validate theo từng đối tượng cụ thể
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<ResultReponse> Validate(T obj)
        {
            return await Task.FromResult(new ResultReponse());
        }

    }
}
