using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleDemo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleDemo.Controllers.Category
{
    public partial class CategoryController : ControllerBase
    {
        [HttpPost("/Category/Create")]
        public async Task<dynamic> CreateCateogry(string category_name)
        {
            return await _CategoryService.CreateCategory(category_name);
        }
    }
}