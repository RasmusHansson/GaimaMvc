using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

using GaimaMvc.Models;

namespace GaimaMvc.Controllers
{
    public class SearchController : Controller
    {
        SearchHandler handler = new SearchHandler();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimpleSearch(string tbxSearch)
        {
            if (String.IsNullOrWhiteSpace(tbxSearch))
                return View("Index");

            var results = handler.NameSearch(tbxSearch);

            return View("Index", GetModelList(results));
        }

        public ActionResult Advanced()
        {
            return View(handler.PopulateCategoryLists());
        }

        [HttpPost]
        public ActionResult Advanced(SearchParameters parameters)
        {
            List<tbGame> gameList = null;

            if (parameters.Categories == null)
                gameList = handler.GetAllGames();

            else 
            { 
                foreach (var category in parameters.Categories)
                {
                    var categoryId = category.Split('_');
                    gameList = handler.CategorySearch(int.Parse(categoryId[1]), categoryId[0], gameList);              
                }
            }

            return PartialView("_GameList", GetModelList(gameList, parameters.Score));
        }

        public ActionResult PlatformSearch(int id)
        {
            List<tbGame> gameList = null;

            var results = handler.CategorySearch(id, "platform", gameList);

            return View("Index", GetModelList(results));
        }

        public ActionResult GenreSearch(int id)
        {
            List<tbGame> gameList = null;

            var results = handler.CategorySearch(id, "genre", gameList);

            return View("Index", GetModelList(results));
        }

        public ActionResult GraphicStyleSearch(int id)
        {
            List<tbGame> gameList = null;

            var results = handler.CategorySearch(id, "graphicStyle", gameList);

            return View("Index", GetModelList(results));
        }

        public ActionResult ThemeSearch(int id)
        {
            List<tbGame> gameList = null;

            var results = handler.CategorySearch(id, "theme", gameList);

            return View("Index", GetModelList(results));
        }

        public ActionResult GameInfo(int id)
        {
            return View(handler.GetGameInfo(id));
        }

        public ActionResult ScoreChart(int id)
        {
            var game = handler.GetGameInfo(id);

            var chart = new Chart(width: 350, height: 250)
                .AddTitle("Game Focus")
                .AddSeries(
                    name: "Score",
                    chartType: "Radar",
                    xValue: new[] { "Strategic Difficulty", "Replay Value", "Mechanical Difficulty", "Ease of Access", "Atmosphere" },
                    yValues: new[] { 
                        game.Score.iStrategy.ToString(), 
                        game.Score.iLongevity.ToString(), 
                        game.Score.iMechanics.ToString(), 
                        game.Score.iEase.ToString(), 
                        game.Score.iAtmosphere.ToString() 
                    });
            chart.Write();

            return null;
        }

        private List<GameInfoView> GetModelList(List<tbGame> gameList, int[] score)
        {
            List<GameInfoView> modelList = GetModelList(gameList);

            bool searchScore = false;

            for (int i = 0; i < score.Length; i++)
            {
                if (score[i] != 0)
                    searchScore = true;
            }

            if (searchScore == false)
                return modelList;
                
            foreach (GameInfoView model in modelList)
            {
                List<int> scoreList = new List<int>();

                if (score[0] != 0)
                    scoreList.Add(Math.Abs(model.Score.iStrategy.Value - score[0]));
                if (score[1] != 0)
                    scoreList.Add(Math.Abs(model.Score.iMechanics.Value - score[1]));
                if (score[2] != 0)
                    scoreList.Add(Math.Abs(model.Score.iEase.Value - score[2]));
                if (score[3] != 0)
                    scoreList.Add(Math.Abs(model.Score.iAtmosphere.Value - score[3]));
                if (score[4] != 0)
                    scoreList.Add(Math.Abs(model.Score.iLongevity.Value - score[4]));

                double amount = scoreList.Count();
                double similarity = 0;

                foreach (var i in scoreList)
	            {
		            similarity += i;
	            }

                model.ScoreSimilarity = 10 - Math.Round((similarity * 4) / amount, MidpointRounding.AwayFromZero) / 2;
            }

            return modelList.OrderByDescending(x => x.ScoreSimilarity).ToList();
        }

        private List<GameInfoView> GetModelList(List<tbGame> gameList)
        {
            var modelList = new List<GameInfoView>();

            foreach (var game in gameList)
            {
                modelList.Add(handler.GetGameInfo(game.Id));
            }

            return modelList;
        }
    }
}