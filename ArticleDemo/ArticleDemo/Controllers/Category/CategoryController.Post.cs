using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleDemo.Models.Dal;
using ArticleDemo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleDemo.Controllers.Category
{
    public partial class CategoryController : ControllerBase
    {
        /// <summary>
        /// 新增分類
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPost("/Category/Create")]
        public async Task<dynamic> CreateCateogry(TCategoryModel para)
        {
            return await _CategoryService.CreateCategory(para.f_category_name);
        }
    }
}