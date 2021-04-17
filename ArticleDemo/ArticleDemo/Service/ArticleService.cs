using ArticleDemo.Models.Dal;
using ArticleDemo.Models.repository.Article;
using ArticleDemo.Models.repository.Category;
using ArticleDemo.Models.viewmodels.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Service
{
    public class ArticleService
    {
        private readonly ArticleRepo _articleRepo;
        private readonly CategoryService _categoryService;
        private readonly CategoryRepo _categoryRepo;
        public ArticleService(ArticleRepo articleRepo, CategoryService categoryService, CategoryRepo categoryRepo)
        {
            _articleRepo = articleRepo;
            _categoryService = categoryService;
            _categoryRepo = categoryRepo;
        }
        public async Task<dynamic> ArticleVerify(ArticleViewModel_para para)
        {
            if (string.IsNullOrEmpty(para.category_id) || !Guid.TryParse(para.category_id, out Guid id))
            {
                return "分類沒填";
            }
            if (string.IsNullOrEmpty(para.article_name))
            {
                return "文章名稱沒填";
            }
            if (string.IsNullOrEmpty(para.article_content))
            {
                return "文章內容沒填";
            }
            var catID = await _categoryRepo.GetById(id);
            if (catID == null)
            {
                return "分類不存在";
            }
            return "success";
        }
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="category_id"></param>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<dynamic> CreateArticle(ArticleViewModel_para para)
        {
            #region 驗證
            var check = await ArticleVerify(para);
            if (check != "success")
            {
                return check;
            }
            #endregion
            var NewArticle = new TArticleModel()
            {
                f_article_id = Guid.NewGuid(),
                f_category_id = new Guid(para.category_id),
                f_article_name = para.article_name,
                f_content = para.article_content
            };
            await _articleRepo.CreateArticle(NewArticle);

            return "success";
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public async Task<string> EditArticle(ArticleViewModel_para para)
        {
            #region 驗證
            if (string.IsNullOrEmpty(para.article_id) || !Guid.TryParse(para.article_id, out Guid id))
            {
                return "請選擇文章";
            }
            var GetArticle = await _articleRepo.GetById(id);
            if(GetArticle == null)
            {
                return "該文章不存在";
            }
            var check = await ArticleVerify(para);
            if(check != "success")
            {
                return check;
            }
            #endregion
            var UpdateArticle = new TArticleModel()
            {
                f_article_id = id,
                f_category_id = new Guid(para.category_id),
                f_article_name = para.article_name,
                f_content = para.article_content
            };
            var update = await _articleRepo.UpdateArticle(UpdateArticle);
            return "success";
        }
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public async Task<dynamic> ArticleList(Guid category_id)
        {
            var GetArticle = await _articleRepo.GetArticleList(category_id);

            var result = GetArticle.Select(x => new ArticleListViewModel.data()
            {
                article_id = x.article_id,
                category_name = x.category_name,
                name = x.article_name,
                content = x.article_content,
            }).ToList();

            return result;
        }

        /// <summary>
        /// 文章詳細
        /// </summary>
        /// <param name="article_id"></param>
        /// <returns></returns>
        public async Task<ArticleViewModel> ArticleDetail(Guid article_id)
        {
            var detail = await _articleRepo.GetById(article_id);
            var result = new ArticleViewModel()
            {
                article_id = detail.article_id,
                category_id = detail.category_id,
                category_name = detail.category_name,
                name = detail.article_name,
                content = detail.article_content
            };
            return result;
        }
        /// <summary>
        /// 刪除文章
        /// </summary>
        /// <param name="article_id"></param>
        /// <returns></returns>
        public async Task<dynamic> DeleteArticle(Guid article_id)
        {
            var GetArticle = await _articleRepo.GetById(article_id);
            if (GetArticle == null)
            {
                return "該文章不存在";
            }

            await _articleRepo.DeleteArticle(article_id);
            return "success";
        }
    }
}
