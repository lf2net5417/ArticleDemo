using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleDemo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ArticleDemo.Controllers.Article
{
    public partial class ArticleController : ControllerBase
    {
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        [HttpGet("/Article/List")]
        public async Task<dynamic> GetList([FromQuery] Guid category_id)
        {
            return await _ArticleService.ArticleList(category_id);
        }
        /// <summary>
        /// 文章詳細
        /// </summary>
        /// <param name="article_id"></param>
        /// <returns></returns>
        [HttpGet("/Article/Detail")]
        public async Task<dynamic> GetDetail([FromQuery] string article_id)
        {
            #region 驗證欄位
            if (string.IsNullOrEmpty(article_id))
            {
                return "請輸入文章ID";
            }
            else
            {
                if(!Guid.TryParse(article_id, out Guid id))
                {
                    return "id格式不對";
                }
            }
            #endregion

            return await _ArticleService.ArticleDetail(new Guid(article_id));
        }
    }
}