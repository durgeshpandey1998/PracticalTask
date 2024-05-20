using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBaseTable
{
    public class TblCity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("State")]
        public int StateId { get; set; }
        public virtual TblState State { get; set; }

        [Required]
        public string CityName { get; set; }
    }
}
