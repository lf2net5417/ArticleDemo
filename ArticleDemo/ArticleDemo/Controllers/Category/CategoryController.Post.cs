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
        [HttpPost("/Category/Create")]
        public async Task<dynamic> CreateCateogry(TCategoryModel para)
        {
            return await _CategoryService.CreateCategory(para.f_category_name);
        }
    }
}