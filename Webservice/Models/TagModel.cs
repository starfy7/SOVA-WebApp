using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webservice.Models
{
    public class TagModel : LinkedResourceModel
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
    }
}
