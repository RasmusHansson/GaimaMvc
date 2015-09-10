using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaimaMvc.Models
{
    public class GameInfoView
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<vwGenre> Genres { get; set; }
        public List<vwTheme> Themes { get; set; }
        public List<vwGraphicStyle> GraphicStyles { get; set; }
        public List<vwPlatform> Platforms { get; set; }
        public tbImage Images { get; set; }
        public vwScore Score { get; set; }
        public double? ScoreSimilarity { get; set; }

        //public int[5] Scores { get; set; }
    }
}
