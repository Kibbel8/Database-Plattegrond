using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Models
{
    public class DatasetDetail
    {
        public Dataset Dataset { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Relevant> Links { get; set; }
        public string NewCommentText { get; set; }
    }
}