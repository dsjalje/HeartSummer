using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    [Serializable]
    public class RequestMessage : MessageBase
    {
        public RequestMsgType MsgType { get; set; }
        public string Content { get; set; }

        public string CreateTime { get; set; }
        public long MsgId { get; set; }
        public Event Event { get; set; }
        public string EventKey { get; set; }
    }
}