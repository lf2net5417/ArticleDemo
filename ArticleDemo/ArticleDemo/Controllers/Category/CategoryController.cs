using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleDemo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleDemo.Controllers.Category
{
    [Route("")]
    [ApiController]
    public partial class CategoryController : ControllerBase
    {
        private readonly CategoryService _CategoryService;

        public CategoryController(CategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }
    }
}