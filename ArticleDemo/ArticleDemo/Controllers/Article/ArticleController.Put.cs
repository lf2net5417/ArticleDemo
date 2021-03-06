using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleDemo.Models.Dal;
using ArticleDemo.Models.viewmodels.Article;
using ArticleDemo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ArticleDemo.Controllers.Article
{
    public partial class ArticleController : ControllerBase
    {
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPut("/Article/Edit")]
        public async Task<dynamic> EditArticle(ArticleViewModel_para para)
        {
            return await _ArticleService.EditArticle(para);
        }
        /// <summary>
        /// 刪除文章
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPut("/Article/Delete")]
        public async Task<dynamic> DeleteArticle(TArticleModel para)
        {
            return await _ArticleService.DeleteArticle(para.f_article_id);
        }
    }
}