using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GaimaMvc.Models
{
    public class AdvancedSearchView
    {
        [Display(Name = "Graphic Styles")]
        public IEnumerable<SelectListItem> SelectGraphicStyle { get; set; }
        public int SelectGraphicStyleID { get; set; }

        [Display(Name = "Genres")]
        public IEnumerable<SelectListItem> SelectGenre { get; set; }
        public int SelectGenreID { get; set; }

        [Display(Name = "Platforms")]
        public IEnumerable<SelectListItem> SelectPlatform { get; set; }
        public int SelectPlatformID { get; set; }

        [Display(Name = "Themes")]
        public IEnumerable<SelectListItem> SelectTheme { get; set; }
        public int SelectThemeID { get; set; }
    }
}
