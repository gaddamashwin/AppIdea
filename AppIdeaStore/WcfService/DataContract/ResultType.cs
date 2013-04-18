using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.DataContract
{
    public class ResultType
    {
        public int appId { get; set; }
        public int techId { get; set; }
        public int sectorId { get; set; }
        public int commentId { get; set; }
        public int imageId { get; set; }
        public int userId { get; set; }
    }
}