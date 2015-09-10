using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaimaMvc.Models
{
    public class SearchParameters
    {
        public int[] Score { get; set; }
        public List<string> Categories { get; set; } 

        //public int RatingStrat { get; set; }
        //public int RatingMech { get; set; }
        //public int RatingEase { get; set; }
        //public int RatingLong { get; set; }
        //public int RatingAtmo { get; set; }
    }
}
