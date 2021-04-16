using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Models.Dal
{
    public class TArticleModel
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        [Key]
        public Guid f_article_id { get; set; }
        /// <summary>
        /// 分類ID
        /// </summary>
        public Guid f_category_id { get; set; }
        public TCategoryModel TCategoryModel { get; set; }
        /// <summary>
        /// 文章名稱
        /// </summary>
        public string f_article_name { get; set; }
        /// <summary>
        /// 文章內容
        /// </summary>
        public string f_content { get; set; }
    }
}
