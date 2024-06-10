using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LttLesson8.Models
{
    /// <summary>
    /// Tao ra cau truc bang Book
    /// </summary>
    public class LttBook
    {
        [Key]
        public int LttBookId { get; set; }
        public string LttTitle { get; set; }
        public string LttAuthor { get; set; }
        public int LttYear { get; set;}
        public string LttPublisher { get; set;}
        public string LttPicture { get; set;}
        public int LttCategoryId { get; set; }

        // Thuoc tinh quan he
        public virtual LttCategory LttCategory { get; set; }
    }
}