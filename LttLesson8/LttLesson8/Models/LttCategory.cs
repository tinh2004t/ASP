using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LttLesson8.Models
{
    /// <summary>
    /// Tao cau truc bang du lieu
    /// </summary>
    public class LttCategory
    {
        [Key]
        public int LttCategorId { get; set; }
        public string LttCategoryName { get; set; }


        //Thuoc tinh quan he
        public virtual ICollection<LttBook> LttBooks { get; set; }
    }
}