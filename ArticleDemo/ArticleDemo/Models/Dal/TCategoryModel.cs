using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Models.Dal
{
    public class TCategoryModel
    {
        /// <summary>
        /// 分類ID
        /// </summary>
        [Key]
        public Guid f_category_id { get; set; }
        /// <summary>
        /// 分類名稱
        /// </summary>
        public string f_category_name { get; set; }

        public List<TArticleModel> articles { get; set; }
    }
}
