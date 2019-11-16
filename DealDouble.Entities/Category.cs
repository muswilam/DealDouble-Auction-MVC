using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [MinLength(10), MaxLength(125)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        //nav props
        public List<Auction> Auctions { get; set; }
    }
}
