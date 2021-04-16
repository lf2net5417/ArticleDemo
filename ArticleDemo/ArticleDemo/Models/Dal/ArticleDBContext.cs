using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleDemo.Models.Dal
{
    public class ArticleDBContext : DbContext
    {
        public ArticleDBContext(DbContextOptions<ArticleDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<TArticleModel> t_article { get; set; }
        public DbSet<TCategoryModel> t_category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //用分類ID跟文章牽關聯
            modelBuilder.Entity<TArticleModel>()
                .HasOne(p => p.TCategoryModel)
                .WithMany(b => b.articles)
                .HasForeignKey(p => p.f_category_id)
                .OnDelete(DeleteBehavior.NoAction); //沒加DeleteBehavior.NoAction的話，其他表格有這個foreign key的資料也會被刪掉(預設是Cascade)

            //創立種子資料
            modelBuilder.Entity<TCategoryModel>().HasData(
                new TCategoryModel() { f_category_id = new Guid("6ca72fa3-c617-4a33-9ffd-3227c970dd60"), f_category_name = "熱血" },
                new TCategoryModel() { f_category_id = new Guid("d89d0be7-aa5e-46d3-9b8d-f760c453de74"), f_category_name = "戀愛" },
                new TCategoryModel() { f_category_id = new Guid("e516fe0e-ac89-4996-b68e-61d973cd1f1f"), f_category_name = "校園" }
            );

            modelBuilder.Entity<TArticleModel>().HasData(
                new TArticleModel() { f_article_id = Guid.NewGuid(), f_category_id = new Guid("6ca72fa3-c617-4a33-9ffd-3227c970dd60"), f_article_name = "鬼滅之刃", f_content = "《鬼滅之刃》（日語：鬼滅の刃），簡稱《鬼滅》，是日本漫畫家吾峠呼世晴所創作的奇幻漫畫作品，描述主角炭治郎為了尋求讓被變成鬼的妹妹復原的方法，踏上斬鬼之旅的和風刀劍奇譚[1]。於2016年2月15日至2020年5月18日在《週刊少年Jump》連載[2]，全205話。漫畫改編電視動畫於2019年4月至9月播放，並於2020年10月16日上映續集電影《鬼滅之刃劇場版 無限列車篇》[3]。 " },
                new TArticleModel() { f_article_id = Guid.NewGuid(), f_category_id = new Guid("d89d0be7-aa5e-46d3-9b8d-f760c453de74"), f_article_name = "澄路", f_content = "《古靈精怪》（日語：きまぐれオレンジ☆ロード）是日本漫畫家松本泉創作的日本漫畫作品。於《週刊少年Jump》1984年15號至1987年42號期間進行連載。單行本全18卷。" },
                new TArticleModel() { f_article_id = Guid.NewGuid(), f_category_id = new Guid("e516fe0e-ac89-4996-b68e-61d973cd1f1f"), f_article_name = "驚爆危機校園篇", f_content = "2003年8月開始在富士電視台播出。骨幹是將短篇小說動畫化，因此嚴肅的戲份減少，成為校園喜劇作品。由於其搞笑和惡搞情節出色，跳脫熱血激戰的框架，吸引不少軍事迷或機器人迷以外的收視群，成為另一種接觸本系列作的入口。製作公司由原本的GONZO Digimation改為京都動畫。 " }
            );
        }
    }
}
