using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webservice.Models
{
    public class HistoryModel : LinkedResourceModel
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime Access { get; set; }
    }
}
