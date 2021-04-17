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
        /// 修改分類
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPut("/Category/Edit")]
        public async Task<dynamic> EditCategory(TCategoryModel para)
        {
            return await _CategoryService.EditCategory(para);
        }
        /// <summary>
        /// 刪除分類
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [HttpPut("/Category/Delete")]
        public async Task<dynamic> DeleteCategory(TCategoryModel para)
        {
            return await _CategoryService.DeleteCategory(para.f_category_id);
        }
    }
}