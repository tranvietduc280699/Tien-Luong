using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Salary.DL;
using MISA.AMIS.Salary.Model.BaseModel;
using MISA.AMIS.Salary.Model.Entities;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.Salary.DL.DatabaseHelper
{
    public class DatabaseHelper<T>
    {
        private readonly IDatabaseService _connectionString;
        public DatabaseHelper(IDatabaseService config)
        {
            _connectionString = config;
        }
        //using (var conn = new MySqlConnection()) ;
        public IDbConnection getconn()
        {
            return _connectionString.CreateConnection();
        }


        /// <summary>
        /// lấy tất cả bản ghi 
        /// (stored procedure)
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAll(string procedureName)
        {
            using var conn = getconn();
            return await conn.QueryAsync<T>(procedureName, commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// lấy bản ghi theo id
        /// (stored procedure)
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="p_id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetById<T>(string procedureName, string id)
        {
            using var conn = getconn();
            var param = new DynamicParameters();
            param.Add("p_id", id);
            return await conn.QueryFirstAsync<T>(procedureName, param , commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// tìm kiếm từ khóa ( stored procedure )
        /// ( stored procedure )
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetByKeyWord<T>(string procedureName, string KeyWord)
        {
            using var conn = getconn();
            var param = new DynamicParameters();
            param.Add("p_keyWord", KeyWord);
            return await conn.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// xóa bản ghi theo id
        /// ( stored procedure )
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteById(string procedureName, string id)
        {
            using var conn = getconn();
            var param = new DynamicParameters();
            param.Add("p_id", id);
            return await conn.ExecuteAsync(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        ///<summary>
        ///xóa nhiều bản ghi theo ids
        /// </summary>
        public virtual async Task<int> DeleteMany(string tableName, string keyColumn, List<int> ids)
        {
            using var conn = getconn();
            string sql = $"DELETE FROM {tableName} WHERE {keyColumn} in @Ids";
            return await conn.ExecuteAsync(sql, new { Ids = ids });
        }
        /// <summary>
        ///  cập nhật cột theo ids
        /// </summary>
        /// <param name="tableName"> tên bảng</param>
        /// <param name="column">tên cột muốn cập nhật</param>
        /// <param name="keyColumn"> khóa chính lấy vị trí</param>
        /// <param name="ids"> list id muốn cập nhật</param>
        /// <param name="value"> giá trị truyền vào</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateStatusMany(string tableName,string column,
            string keyColumn, List<int> ids, int value)
        {
            using var conn = getconn();
            string sql = $"UPDATE {tableName} SET {column} = @Value WHERE {keyColumn} IN @Ids";
            return await conn.ExecuteAsync(sql,new {Value=value,Ids = ids});
        }
        /// <summary>
        /// thêm bản ghi mới
        /// ( stored procedure )
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<int> Insert(string procedureName, T obj)
        {
            using var conn= getconn();
            var param = new DynamicParameters();
            foreach (var prop in typeof(T).GetProperties())
            {
                string name = prop.Name;
                object inputVallue = prop.GetValue(obj);
                param.Add("p_"+ name, inputVallue);
            }
            return await conn.ExecuteScalarAsync<int>(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// cập nhập bản ghi
        /// ( stored procedure )
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> Update(string procedureName, T obj)
        {
            using var conn = getconn();
            var param = new DynamicParameters();
          
            foreach (var prop in typeof(T).GetProperties())
            {
                string name = prop.Name;
                object inputValue = prop.GetValue(obj);
                param.Add("p_" + name, inputValue);
            }
            var result = await conn.ExecuteScalarAsync<int>(procedureName, param, commandType: CommandType.StoredProcedure );
            return result;
        }
        /// <summary>
        /// cập nhật trạng thái (procedure)
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateStatus(string procedureName,int id, int status)
        {
            using var conn = getconn();
            var param = new DynamicParameters();
            param.Add("p_id", id);
            param.Add("p_Status", status);
            return await conn.ExecuteAsync(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// cập nhật giá trị trong cột theo id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <param name="keyColumn"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateSingleField (string tableName, string columnName, string keyColumn, int fieldValue, int id)
        {
            using var conn= getconn();
            var param = new DynamicParameters();
            param.Add("@FieldValue", fieldValue);
            param.Add("@id", id);
            string sql = $"UPDATE {tableName} SET {columnName} = @FieldValue WHERE {keyColumn} = @id ; ";
            return await conn.ExecuteAsync(sql, param);
        }

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public virtual async Task<PagedResult<T>> GetPaging(string tableName, PagingParam paging)
        {
            //tính số trang
            if (paging.PageIndex < 1) paging.PageIndex = 1;
            int offset = (paging.PageIndex - 1) * paging.PageSize;
            //dùng object của Dapper
            var parameters = new DynamicParameters();
            var where = " WHERE 1=1";
            // lọc theo trạng thái 
            if (paging.Status != null)
            {
                where = where + " AND status = @status";
                parameters.Add("@status", paging.Status);

            }
            // tìm kiếm theo nhiều cột
            //salary_composition_code LIKE @Keyword OR salary_composition_name LIKE @Keyword";
            if (!string.IsNullOrEmpty(paging.KeyWord) && !string.IsNullOrWhiteSpace(paging.Columns))
            {
                var list = paging.Columns.Split(";");
                if (list.Any())
                {
                    where += " AND (";
                    for (int i = 0; i < list.Count(); i++)
                    {
                        var item = list[i];
                        if (i > 0)
                        {
                            where += " OR ";
                        }

                        where += $"{list[i]} LIKE @keyWord";
                    }
                    where += " )";
                }

            }
            // tìm kiếm theo nhiều từ khóa
            //table where @columns like @keyOrg111 OR @columns like @keyOrg2222 ;
            if (paging.KeyOrganization != null && !string.IsNullOrWhiteSpace(paging.ColumnOrganization))
            {
                var list = paging.KeyOrganization;
                if (list.Any()) 
                {
                    where += " AND ( ";
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if(i < list.Count() && i !=0 )
                        {
                            where += " OR ";
                        }
                        where += $"{paging.ColumnOrganization} LIKE '%{list[i]}%' ";
                       
                    }
                    where += ")";
                }
            }

           
            parameters.Add("@pageIndex", paging.PageIndex);
            parameters.Add("@pageSize", paging.PageSize);
            parameters.Add("@keyWord", $"%{paging.KeyWord}%");
            // sinh câu lệnh sql
            string sql = $@" SELECT * FROM {tableName} {where} ORDER BY {paging.OrderBy} LIMIT {paging.PageSize} OFFSET {offset};
                            SELECT COUNT(*) FROM {tableName} {where};";
            using var conn = getconn();
            var multi = await conn.QueryMultipleAsync(sql, parameters);
            // trả về danh sách
            var items = (await multi.ReadAsync<T>()).ToList();
            //lấy tổng bản ghi
            var total = await multi.ReadSingleAsync<int>();
            //  tổng số trang
            int totalPages = (int)Math.Ceiling((double)total / paging.PageSize);
            return new PagedResult<T>
            {
                Items = items,
                TotalRecords = total,
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalPages = totalPages,
            };

        }
    }
}
