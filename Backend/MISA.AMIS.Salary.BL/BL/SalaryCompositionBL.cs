using MISA.AMIS.Salary.DL;
using MISA.AMIS.Salary.Model.BaseModel;
using MISA.AMIS.Salary.Model.Entities;

namespace MISA.AMIS.Salary.BL
{
    public class SalaryCompositionBL : BaseBL<SalaryComposition>, ISalaryCompositionBL
    {
        public SalaryCompositionBL(IDatabaseService connection) : base(connection)
        {
        }
        /// <summary>
        /// validate thành phần lương
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override async Task<ResultReponse> Validate(SalaryComposition obj)
        {
            var response = new ResultReponse();
            //validate tên thành phần không được để trống và quá 255 ký tự
            if (string.IsNullOrEmpty(obj.SalaryCompositionName))
            {
                response.Success = false;
                response.UserMsg = "Tên thành phần lương không được để trống";
                return response;
            }
            else if (obj.SalaryCompositionName.Length > 255)
            {
                response.Success = false;
                response.UserMsg = "Tên thành phần không được vượt quá 255 ký tự.";
                return response;
            }
            // validate mã thành phần không được để trống và quá 255 ký tự
            if (string.IsNullOrEmpty(obj.SalaryCompositionCode))
            {
                response.Success = false;
                response.UserMsg = "Mã thành phần lương không được để trống";
                return response;
            }
            else if (obj.SalaryCompositionCode.Length > 255)
            {
                response.Success = false;
                response.UserMsg = "Mã thành phần không được vượt quá 255 ký tự.";
                return response;
            }
            //validate mã thành phần lương không được trùng
            var duplicateData = await GetByKeyWord(obj.SalaryCompositionCode);
            if (duplicateData.Any())
            {
                response.Success = false;
                response.UserMsg = "Mã thành phần lương đã tồn tại";
                return response;
            }
            // validate loại thành phần
            if(obj.ComponentType == null)
            {
                response.Success = false;
                response.UserMsg = "Loại thành phần lương không được để trống";
                return response;
            }
            //validate tính chất thành phần
            if(obj.Nature == null)
            {
                response.Success = false;
                response.UserMsg = "Tính chất thành phần lương không được để trống";
                return response;
            }

            response.Success = true;
            return await Task.FromResult(response);
        }
        /// <summary>
        /// tìm kiếm bản ghi theo từ khóa 
        /// ( stored procedure )
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SalaryComposition>> GetByKeyWord(string keyWord)
        {
            var proc = GetProcedureName("GetByKeyWord");
            return await GetByKeyWord<SalaryComposition>(
                proc,
                keyWord
            );
        }
        ///<summary>
        ///xóa nhiều theo ids
        /// </summary>
        public async Task<int> DeleteMany(List<int> ids)
        {
            return await DeleteMany("pa_salary_composition", "salary_composition_id", ids);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keyColumn"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<int> UpdateStatusMany(List<int> ids, int status)
        {
            var column = "status";
            var keyColumn = "salary_composition_id";
            return await UpdateStatusMany("pa_salary_composition", column, keyColumn, ids, status);
        }
        /// <summary>
        /// cập nhật trạng thái theo id( procedure)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<int> UpdateStatus(int id, int status)
        {
            var proc = GetProcedureName("UpdateStatus");
            return await UpdateStatus(proc, id, status);
        }
        /// <summary>
        /// cập nhật  trạng thái theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<int> UpdateSingleField(int id, int fieldValue)
        {
            var column = "status";
            var keyColum = "salary_composition_id";
            return await UpdateSingleField("pa_salary_composition", column, keyColum, fieldValue, id);
        }


    }
}
