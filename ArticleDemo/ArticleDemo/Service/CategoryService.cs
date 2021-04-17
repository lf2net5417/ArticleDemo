using ArticleDemo.Models.Dal;
using ArticleDemo.Models.repository.Category;
using ArticleDemo.Models.viewmodels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Service
{
    public class CategoryService
    {
        private readonly CategoryRepo _categoryRepo;
        public CategoryService(CategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        /// <summary>
        /// 新增分類
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<dynamic> CreateCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "分類名稱沒填";
            }
            var getCategory = await _categoryRepo.GetByName(name);
            if(getCategory != null)
            {
                return "已有相同名稱的分類";
            }
            var NewCategory = new TCategoryModel()
            {
                f_category_id = Guid.NewGuid(),
                f_category_name = name
            };
            var create = await _categoryRepo.CreateCategory(NewCategory);

            return "success";
        }
        /// <summary>
        /// 更新分類
        /// </summary>
        /// <param name="category_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<string> EditCategory(TCategoryModel para)
        {
            var GetCategory = _categoryRepo.GetById(para.f_category_id);
            if(GetCategory == null)
            {
                return "此分類不存在";
            }
            var UpdateCategory = new TCategoryModel()
            {
                f_category_id = para.f_category_id,
                f_category_name = para.f_category_name
            };
            var update = await _categoryRepo.UpdateCategory(UpdateCategory);
            return "success";
        }
        /// <summary>
        /// 刪除分類
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public async Task<string> DeleteCategory(Guid category_id)
        {
            var update = await _categoryRepo.DeleteCategory(category_id);
            return "success";
        }
        /// <summary>
        /// 分類列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryListViewModel>> CategoryList()
        {
            var GetCategory = await _categoryRepo.GetCategoryList();
            var result = GetCategory.Select(x => new CategoryListViewModel()
            {
                category_id = x.f_category_id,
                category_name = x.f_category_name
            });
            return result;
        }
        /// <summary>
        /// 單一分類詳細資訊
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public async Task<dynamic> GetSingleCategory(Guid category_id)
        {
            var GetSingle = await _categoryRepo.GetById(category_id);
            if(GetSingle == null)
            {
                return "查無此分類";
            }
            var result = new CategoryViewModel()
            {
                category_id = GetSingle.f_category_id,
                category_name = GetSingle.f_category_name
            };
            return result;
        }
    }
}
