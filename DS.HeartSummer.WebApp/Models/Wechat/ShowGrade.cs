using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    public class ShowGrade
    {
        public string Term { get; set; }
        public List<GradeDate> GradeDates { get; set; }
    }
    public class GradeDate
    {
        public string GradeName { get; set; }
        public string Credit { get; set; }
        public string Grade { get; set; }
    }
}