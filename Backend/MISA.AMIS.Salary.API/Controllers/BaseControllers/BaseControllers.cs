using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Salary.BL;
using MISA.AMIS.Salary.BL.IBL.IBaseBL;
using MISA.AMIS.Salary.Model.BaseModel;
using MISA.AMIS.Salary.Model.Entities;

namespace MISA.AMIS.Salary.API.Controllers.BaseControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseControllers<T> : Controller
    {
        private readonly IBaseBL<T> _baseBL;
        public BaseControllers(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        /// <returns> lấy tất cả bản ghi </returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _baseBL.GetAll());
        }
        /// <summary>
        /// tìm kiếm theo id
        /// </summary>
        /// <param name="p_id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _baseBL.GetById(id));
        }
       
        /// <summary>
        /// xóa bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            return Ok(await _baseBL.DeleteById(id));
        }
 
        /// <summary>
        /// thêm mới bản ghi
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] T obj)
        {
            var result = await _baseBL.Insert(obj);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        /// <summary>
        /// cập nhật bản ghi theo id
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] T obj)
        {
            var result = await _baseBL.Update(obj);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        ///<summary>
        ///phân trang
        /// </summary>
        [HttpPost("Paging")]
        public async Task<PagedResult<T>> GetPaging([FromBody] PagingParam paging)
        {
            return await _baseBL.GetPaging(paging);
        }

    }
}
