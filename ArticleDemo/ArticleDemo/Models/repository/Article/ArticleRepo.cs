using ArticleDemo.Models.Dal;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Models.repository.Article
{
    public class ArticleRepo
    {
        private readonly DBList _dbList;

        public ArticleRepo(IOptions<DBList> dbList)
        {
            _dbList = dbList.Value;
        }
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public async Task<int> CreateArticle(TArticleModel para)
        {

            var sqlQuery = $@"INSERT INTO t_article
                                        (f_article_id
                                        ,f_category_id
                                        ,f_article_name
                                        ,f_content)
                            VALUES
                                        (@f_article_id
                                        ,@f_category_id
                                        ,@f_article_name
                                        ,@f_content)";


            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.ExecuteAsync(sqlQuery, para);
            }
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public async Task<int> UpdateArticle(TArticleModel para)
        {
            var sqlQuery = $@"UPDATE t_article
                             SET f_category_id = @f_category_id
                                ,f_article_name = @f_article_name
                                ,f_content = @f_content
                            WHERE f_article_id = @f_article_id";
            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.ExecuteAsync(sqlQuery, para);
            }
        }
        #region 文章列表DB
        public class ArticleListDB
        {
            public Guid article_id { get; set; }
            public Guid category_id { get; set; }
            public string category_name { get; set; }
            public string article_name { get; set; }
            public string article_content { get; set; }
        }
        #endregion
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="f_category_id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ArticleListDB>> GetArticleList(Guid f_category_id = new Guid())
        {
            var sqlParam = new DynamicParameters();
            var sqlWhere = string.Empty;
            if (f_category_id != Guid.Empty)
            {
                sqlParam.Add("f_category_id", f_category_id);
                sqlWhere += " and f_category_id = @f_category_id";
            }
            var sqlQuery = $@"select f_article_id article_id
                                , ta.f_category_id category_id
                                , f_category_name category_name
                                , f_article_name article_name
                                , f_content article_content
                              from t_article as ta, t_category as tc
                              where 1 = 1 AND ta.f_category_id = tc.f_category_id
                                {sqlWhere}";

            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.QueryAsync<ArticleListDB>(sqlQuery, sqlParam);
            }
        }
        /// <summary>
        /// 文章詳細
        /// </summary>
        /// <param name="f_article_id"></param>
        /// <returns></returns>
        public async Task<ArticleListDB> GetById(Guid f_article_id)
        {
            var sqlQuery = $@"select f_article_id article_id
                                , ta.f_category_id category_id
                                , f_category_name category_name
                                , f_article_name article_name
                                , f_content article_content
                              from t_article as ta, t_category as tc
                              where ta.f_article_id = @f_article_id";

            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.QueryFirstOrDefaultAsync<ArticleListDB>(sqlQuery, new { f_article_id = f_article_id });
            }
        }
        /// <summary>
        /// 刪除文章
        /// </summary>
        /// <param name="f_article_id"></param>
        /// <returns></returns>
        public async Task<int> DeleteArticle(Guid f_article_id)
        {
            var sqlQuery = @" Delete FROM t_article WHERE f_article_id = @f_article_id";

            using (var conn = new SqlConnection(_dbList.Article))
            {
                return await conn.ExecuteAsync(sqlQuery, new { f_article_id = f_article_id });
            }
        }
    }
}
