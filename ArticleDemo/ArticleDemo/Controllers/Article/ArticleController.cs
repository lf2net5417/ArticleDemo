using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleDemo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleDemo.Controllers.Article
{
    [Route("")]
    [ApiController]
    public partial class ArticleController : ControllerBase
    {
        private readonly ArticleService _ArticleService;

        public ArticleController(ArticleService ArticleService)
        {
            _ArticleService = ArticleService;
        }
    }
}