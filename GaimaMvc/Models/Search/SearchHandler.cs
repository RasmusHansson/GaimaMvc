using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GaimaMvc.Models
{
    public class SearchHandler
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public List<tbGame> GetAllGames()
        {
            return db.tbGames.Select(x => x).ToList();
        }

        public List<tbGame> NameSearch(string searchString)
        {
            var sList = searchString.Split(' ').OrderBy(x => x);

            List<tbGame> results = new List<tbGame>();

            foreach (var term in sList)
            {
                results.AddRange(from g in db.tbGames
                                 where g.sTitle.Contains(term)
                                 select g);
            }

            return results;         
        }

        public List<tbGame> CategorySearch(int id, string type, List<tbGame> gameList)
        {
            List<tbGame> results = new List<tbGame>();

            if (gameList == null)
            {
                gameList = db.tbGames.Select(x => x).ToList();
            }

            switch (type)
            {
                case "platform":
                    var gamePlatformIds = db.tbGamePlatforms
                        .Where(x => x.iPlatform == id)
                        .Select(x => x.iGame);

                    foreach (var gameId in gamePlatformIds)
	                {
                        if (gameList.Exists(x => x.Id == gameId))
                            results.Add(gameList.Where(x => x.Id == gameId)
                                .Select(x => x).First());
	                }
                    break;
                case "genre":
                    var gameGenreIds = db.tbGameGenres
                        .Where(x => x.iGenre == id)
                        .Select(x => x.iGame);

                    foreach (var gameId in gameGenreIds)
	                {
                        if(gameList.Exists(x => x.Id == gameId))
                            results.Add(gameList.Where(x => x.Id == gameId)
                                .Select(x => x).First());
	                }
                    break;
                case "graphicStyle":
                    var gameGraphicStyleIds = db.tbGameGraphicStyles
                        .Where(x => x.iGraphicStyle == id)
                        .Select(x => x.iGame);

                    foreach (var gameId in gameGraphicStyleIds)
	                {
                        if (gameList.Exists(x => x.Id == gameId))
                            results.Add(gameList.Where(x => x.Id == gameId)
                                .Select(x => x).First());
	                }
                    break;
                case "theme":
                    var gameThemeIds = db.tbGameThemes
                        .Where(x => x.iTheme == id)
                        .Select(x => x.iGame);

                    foreach (var gameId in gameThemeIds)
	                {
                        if (gameList.Exists(x => x.Id == gameId))
                            results.Add(gameList.Where(x => x.Id == gameId)
                                .Select(x => x).First());
	                }
                    break;
            }

            return results;
        }

        public GameInfoView GetGameInfo(int gameId)
        {
            var game = (from g in db.tbGames
                       where g.Id == gameId
                       select g).First();

            var genres = db.vwGenres
                .Where(x => x.iGame == gameId)
                .Select(x => x)
                .ToList();

            var themes = db.vwThemes
                .Where(x => x.iGame == gameId)
                .Select(x => x)
                .ToList();

            var graphicStyles = db.vwGraphicStyles
                .Where(x => x.iGame == gameId)
                .Select(x => x)
                .ToList();

            var platforms = db.vwPlatforms
                .Where(x => x.iGame == gameId)
                .Select(x => x)
                .ToList();

            var images = db.tbImages
                .Where(x => x.iGame == gameId)
                .Select(x => x)
                .First();

            var scores = db.vwScores
                .Where(x => x.iGame == gameId)
                .Select(x => x)
                .First();

            return new GameInfoView()
                {
                    ID = game.Id,
                    Title = game.sTitle,
                    Description = game.sDescription,
                    Genres = genres,
                    Themes = themes,
                    GraphicStyles = graphicStyles,
                    Platforms = platforms,
                    Images = images,
                    Score = scores
                };
        }

        public AdvancedSearchView PopulateCategoryLists()
        {
            AdvancedSearchView model = new AdvancedSearchView();

            model.SelectGenre = db.tbGenres
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.sName });
            model.SelectGraphicStyle = db.tbGraphicStyles
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.sName });
            model.SelectTheme = db.tbThemes
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.sName });
            model.SelectPlatform = db.tbPlatforms
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.sName });

            return model;

        }
    }
}
