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
        [HttpPut("/Category/Edit")]
        public async Task<dynamic> EditCategory(TCategoryModel para)
        {
            return await _CategoryService.EditCategory(para);
        }
        [HttpPut("/Category/Delete")]
        public async Task<dynamic> DeleteCategory(TCategoryModel para)
        {
            return await _CategoryService.DeleteCategory(para.f_category_id);
        }
    }
}