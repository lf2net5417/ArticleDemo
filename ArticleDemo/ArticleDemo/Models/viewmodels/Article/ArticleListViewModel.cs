using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Models.viewmodels.Article
{
    public class ArticleListViewModel
    {
        public List<data> datas { get; set; }
        public class data
        {
            public Guid article_id { get; set; }
            public string category_name { get; set; }
            public string name { get; set; }
            public string content { get; set; }
        }
    }
}
