using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /// 新增文章
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost("/Article/Create")]
        public async Task<dynamic> CreateArticle(ArticleViewModel_para para)
        {
            return await _ArticleService.CreateArticle(para);
        }
    }
}