using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Models.viewmodels.Article
{
    public class ArticleViewModel
    {
        public Guid article_id { get; set; }
        public Guid category_id { get; set; }
        public string category_name { get; set; }
        public string name { get; set; }
        public string content { get; set; }
    }
}
