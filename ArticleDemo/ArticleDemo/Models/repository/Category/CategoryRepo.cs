using ArticleDemo.Models.Dal;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Models.repository.Category
{
    public class CategoryRepo
    {
        private readonly DBList _dbList;

        public CategoryRepo(IOptions<DBList> dbList)
        {
            _dbList = dbList.Value;
        }
        /// <summary>
        /// 新增分類
        /// </summary>
        /// <returns></returns>
        public async Task<int> CreateCategory(TCategoryModel para)
        {

            var sqlQuery = $@"INSERT INTO t_category
                                        (f_category_id
                                        ,f_category_name)
                            VALUES
                                        (@f_category_id
                                        ,@f_category_name)";


            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.ExecuteAsync(sqlQuery, para);
            }

        }
        /// <summary>
        /// 更新分類
        /// </summary>
        /// <returns></returns>
        public async Task<int> UpdateCategory(TCategoryModel para)
        {
            var sqlQuery = $@"UPDATE t_category
                             SET f_category_name = @f_category_name
                            WHERE f_category_id = @f_category_id";
            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.ExecuteAsync(sqlQuery, para);
            }
        }
        /// <summary>
        /// 分類列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TCategoryModel>> GetCategoryList()
        {
            var sqlQuery = $@"select * 
                              from t_category
                              where 1 = 1
                                ";

            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.QueryAsync<TCategoryModel>(sqlQuery);
            }
        }
        /// <summary>
        /// 單筆分類
        /// </summary>
        /// <param name="f_category_id"></param>
        /// <returns></returns>
        public async Task<TCategoryModel> GetById(Guid f_category_id)
        {
            var sqlQuery = $@"select * from t_category where f_category_id = @f_category_id";

            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.QueryFirstOrDefaultAsync<TCategoryModel>(sqlQuery, new { f_category_id = f_category_id });
            }
        }
        /// <summary>
        /// 刪除分類
        /// </summary>
        /// <param name="f_category_id"></param>
        /// <returns></returns>
        public async Task<int> DeleteCategory(Guid f_category_id)
        {
            var sqlQuery = $@"DELETE FROM t_category
                            WHERE f_category_id = @f_category_id";
            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.ExecuteAsync(sqlQuery, new { f_category_id = f_category_id });
            }
        }
    }
}
