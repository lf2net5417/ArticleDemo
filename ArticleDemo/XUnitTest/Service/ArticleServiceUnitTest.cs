using ArticleDemo.Models.viewmodels.Article;
using ArticleDemo.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitTest.Environment;

namespace XUnitTest.Service
{
    public class ArticleServiceUnitTest
    {
        /// <summary>
        /// 新增文章
        /// </summary>
        [Fact]
        public void CreateArticle()
        {
            var ArticleService = DbFixture.GetRequiredService<ArticleService>();
            try
            {
                //Arrange 初始化
                var para = new ArticleViewModel_para()
                {
                    category_id = "6ca72fa3-c617-4a33-9ffd-3227c970dd60",
                    article_name = "超人",
                    article_content = "空白"
                };
                //Act 行為
                var test = ArticleService.CreateArticle(para);
                //Assert 驗證結果
                Assert.True(test.Result == "success", "測試通過");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 輸入不存在的分類
        /// </summary>
        [Fact]
        public void WrongCatId()
        {
            var ArticleService = DbFixture.GetRequiredService<ArticleService>();
            try
            {
                //Arrange 初始化
                var para = new ArticleViewModel_para()
                {
                    category_id = "9ca72fa3-c617-4a33-9ffd-3227c970dd60",
                    article_name = "超人",
                    article_content = "空白"
                };
                //Act 行為
                var test = ArticleService.CreateArticle(para);
                //Assert 驗證結果
                Assert.True(test.Result == "分類不存在", "測試通過");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 沒輸入文章名稱
        /// </summary>
        [Fact]
        public void NoTitle()
        {
            var ArticleService = DbFixture.GetRequiredService<ArticleService>();
            try
            {
                //Arrange 初始化
                var para = new ArticleViewModel_para()
                {
                    category_id = "6ca72fa3-c617-4a33-9ffd-3227c970dd60",
                    article_name = "",
                    article_content = "空白"
                };
                //Act 行為
                var test = ArticleService.CreateArticle(para);
                //Assert 驗證結果
                Assert.True(test.Result == "文章名稱沒填", "測試通過");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
