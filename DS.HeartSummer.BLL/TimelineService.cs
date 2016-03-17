using DS.HeartSummer.IBLL;
using DS.HeartSummer.Model; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.BLL
{
    public partial class TimelineService : BaseService<Timeline>, ITimelineService
    {
        public bool AddTimeLine(Timeline tl)
        {
            Color[] colors = Enum.GetValues(typeof(Color)) as Color[];
            Random random = new Random();
            Color color = colors[random.Next(0, colors.Length)];
            string colorss = color.ToString();
            int fontid = new Random().Next(0, 865);
            string font = this.DbSession.FontAwesomeDAL.LoadEntities(c => c.id == fontid).FirstOrDefault().value;

            tl.condate = DateTime.Now.Month + "月" + DateTime.Now.Day + "日";
            tl.concolor = colorss + "-bg";
            tl.confa = font;
            DbSession.TimelineDAL.AddEntity(tl);
            return DbSession.SaveChanges() > 0;
        }
    }
}
