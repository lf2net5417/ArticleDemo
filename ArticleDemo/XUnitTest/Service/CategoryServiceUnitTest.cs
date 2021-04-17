using ArticleDemo.Models.Dal;
using ArticleDemo.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitTest.Environment;

namespace XUnitTest.Service
{
    public class CategoryServiceUnitTest
    {
        /// <summary>
        /// 新增分類
        /// </summary>
        [Fact]
        public void CreateCategory()
        {
            var CategoryService = DbFixture.GetRequiredService<CategoryService>();
            try
            {
                //Arrange 初始化
                string category_name = "恐怖";
                //Act 行為
                var test = CategoryService.CreateCategory(category_name);
                //Assert 驗證結果
                Assert.True(test.Result == "success", "測試通過");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 輸入重複的名稱
        /// </summary>
        [Fact]
        public void DuplicateName()
        {
            var CategoryService = DbFixture.GetRequiredService<CategoryService>();
            try
            {
                //Arrange 初始化
                string category_name = "熱血";
                //Act 行為
                var test = CategoryService.CreateCategory(category_name);
                //Assert 驗證結果
                Assert.True(test.Result == "已有相同名稱的分類", "測試通過");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
