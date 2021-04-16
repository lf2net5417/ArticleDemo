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
        [HttpGet("/Category/List")]
        public async Task<dynamic> GetList()
        {
            return await _CategoryService.CategoryList();
        }
        [HttpGet("/Category/Detail")]
        public async Task<dynamic> GetDetail([FromQuery] Guid category_id)
        {
            return await _CategoryService.GetSingleCategory(category_id);
        }
    }
}