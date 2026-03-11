using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Salary.BL;
using MISA.AMIS.Salary.Model.Entities;
using MISA.AMIS.Salary.API.Controllers.BaseControllers;
namespace MISA.AMIS.Salary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryCompositionController : BaseControllers<SalaryComposition>
    {
        private readonly ISalaryCompositionBL _iSalarycompositionBL;
        public SalaryCompositionController(ISalaryCompositionBL iSalarycompositionBL) : base(iSalarycompositionBL)
        {
            _iSalarycompositionBL = iSalarycompositionBL;

        }
        /// <summary>   
        /// tìm kiếm theo từ khóa
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HttpGet("GetByKeyWord")]
        public async Task<IActionResult> GetByKeyWord([FromQuery] string keyWord)
        {
            return Ok(await _iSalarycompositionBL.GetByKeyWord(keyWord));
        }
        /// <summary>
        /// xóa nhiều theo ids
        /// </summary>
        [HttpPost("DeleteManyID")]
        public async Task<IActionResult> DeleteMany([FromBody] List<int> ids)
        {
            return Ok(await _iSalarycompositionBL.DeleteMany(ids));
        }
        /// <summary>
        /// cập nhật trạng thái theo danh sách list id 
        /// </summary>
        /// <param name="ids"> list id</param>
        /// <param name="status"> giá trị cần cập nhật</param>
        /// <returns></returns>
        [HttpPost("UpdateStatusMany")]
        public async Task<IActionResult> UpdateStatusMany([FromQuery] List<int> ids, [FromQuery] int status)
        {
            return Ok(await _iSalarycompositionBL.UpdateStatusMany(ids, status));
        }
        /// <summary>
        /// cập nhật trạng thái theo id (procedure)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int id, int status)
        {
            return Ok(await _iSalarycompositionBL.UpdateStatus(id, status));
        }
        /// <summary>
        /// cập nhật trạng thái theo id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("UpdateSingleField")]
        public async Task<IActionResult> UpdateSingleField(int id, int FieldValue)
        {
            return Ok(await _iSalarycompositionBL.UpdateSingleField(id, FieldValue));
        }
    }
}
