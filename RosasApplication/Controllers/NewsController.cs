using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.WebApi;
using Umbraco.Web.PublishedModels;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System;
using System.Linq;

namespace RosasApplication.Controllers
{

    public class NewsController : UmbracoApiController
    {
        public Home homePage;
        List<Article> articles;

        public NewsController()
        {

            this.homePage = (Home)Umbraco.Content(1104);
            this.articles = new List<Article>();

            foreach (var article in this.homePage.Articles)
            {
                articles.Add(new Article(article.Headline, article.Preamble, ("https://localhost:44359" + article.ArticleImage.Url.ToString()), article.DateCreated, article.DatePublished, Decimal.ToInt32(article.PoliticalSpectrum)));
            }
        }


        public class Article
        {
            public Article(string headline, string preamble, string imageURL, DateTime datePublished, DateTime dateCreated, int spectrumScore)
            {
                this.headline = headline;
                this.preamble = preamble;
                this.imageURL = imageURL;
                this.datePublished = datePublished;
                this.dateCreated = dateCreated;
                this.politicalSpectrum = spectrumScore;
            }

            public string headline { get; set; }
            public string preamble { get; set; }
            public string imageURL { get; set; }
            public DateTime datePublished { get; set; }
            public DateTime dateCreated { get; set; }
            public int politicalSpectrum { get; set; }
        }

        [HttpGet]
        public IHttpActionResult GetAllArticles()
        {

            return Ok(articles);
        }


        [HttpGet]
        public IHttpActionResult GetArticlesOrderByDatePublished()
        {
            return Ok(articles.OrderBy(x => x.datePublished));
        }

        [HttpGet]
        public IHttpActionResult GetArticlesOrderByDateCreated()
        {
            return Ok(articles.OrderBy(x => x.dateCreated));
        }

        [HttpGet]
        public IHttpActionResult GetArticlesOrderLeftToRight()
        {
            return Ok(articles.OrderBy(x => x.politicalSpectrum));
        }

        [HttpGet]
        public IHttpActionResult GetRightWingNews()
        {
            List<Article> politicalArticles = new List<Article>();

            foreach (var article in articles)
            {
                if (article.politicalSpectrum > 0)
                {
                    politicalArticles.Add(new Article(article.headline, article.preamble, article.imageURL, article.dateCreated, article.datePublished, article.politicalSpectrum));
                }
            }

            return Ok(politicalArticles.OrderBy(x => x.politicalSpectrum));
        }

        [HttpGet]
        public IHttpActionResult GetLeftWingNews()
        {
            List<Article> politicalArticles = new List<Article>();

            foreach (var article in articles)
            {
                if (article.politicalSpectrum < 0)
                {
                    politicalArticles.Add(new Article(article.headline, article.preamble, article.imageURL, article.dateCreated, article.datePublished, article.politicalSpectrum));
                }
            }

            return Ok(politicalArticles.OrderBy(x => x.politicalSpectrum));
        }


        [HttpGet]
        public IHttpActionResult GetNeutralNews()
        {
            List<Article> politicalArticles = new List<Article>();

            foreach (var article in articles)
            {
                if (article.politicalSpectrum >= -2 && article.politicalSpectrum <= 2)
                {
                    politicalArticles.Add(new Article(article.headline, article.preamble, article.imageURL, article.dateCreated, article.datePublished, article.politicalSpectrum));
                }
            }

            return Ok(politicalArticles.OrderBy(x => x.politicalSpectrum));
        }

        [HttpGet]
        public IHttpActionResult Polarise(int politicalSpectrum)
        {
            List<Article> sortedArticles = new List<Article>();

            var polar = (politicalSpectrum * -1);

            for (int i = 0; i < 21; i++)
            {
                List<Article> articleSample = articles.FindAll(x => (x.politicalSpectrum == polar - i || x.politicalSpectrum == polar + i));

                foreach (var article in articleSample)
                {
                    articles.Remove(article);
                    sortedArticles.Add(article);
                }
            }
            return Ok(sortedArticles);
        }



        [HttpGet]
        public IHttpActionResult PolariseWithinBounds(int politicalSpectrum)
        {
            List<Article> sortedArticles = new List<Article>();

            var polar = (politicalSpectrum * -1);
            var max = 21;

            if (polar >= 0)
            {
              max = (polar*2 + 1);
            } else
            {
              max = (politicalSpectrum * 2 + 1);
            }

            for (int i = 0; i < max; i++)
            {
                List<Article> articleSample = new List<Article>();
                if (polar >= 0)
                {
                    articleSample = articles.FindAll(x => (x.politicalSpectrum == polar - i));

                } else if (polar < 0)
                {
                   articleSample = articles.FindAll(x => (x.politicalSpectrum == polar + i));
                }

                foreach (var article in articleSample)
                {
                    articles.Remove(article);
                    sortedArticles.Add(article);
                }
            }
            return Ok(sortedArticles);
        }
    }
}