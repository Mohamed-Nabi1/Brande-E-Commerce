using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL
{
    public class FilterDto
    {
        public List<Guid>? ParentId { get; set; }
        public List<Guid>?  ChildId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<string>? Color { get; set; }   
        public List<decimal>? Rate { get; set; }
        public List<string>? Size { get; set; }
    }
}
