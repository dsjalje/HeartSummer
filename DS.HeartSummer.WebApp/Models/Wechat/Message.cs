using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    public class Message
    {
        private string toUserName;
        private string fromUserName;
        private string createTime;
        private RequestMsgType msgType;
        private string content;
        private Int64 msgId;
        private int articleCount;
        private Item[] articles;

        public string ToUserName
        {
            get
            {
                return toUserName;
            }

            set
            {
                toUserName = value;
            }
        }

        public string FromUserName
        {
            get
            {
                return fromUserName;
            }

            set
            {
                fromUserName = value;
            }
        }

        public string CreateTime
        {
            get
            {
                return createTime;
            }

            set
            {
                createTime = value;
            }
        }

        public RequestMsgType MsgType
        {
            get
            {
                return msgType;
            }

            set
            {
                msgType = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        public long MsgId
        {
            get
            {
                return msgId;
            }

            set
            {
                msgId = value;
            }
        }

        public int ArticleCount
        {
            get
            {
                return articleCount;
            }

            set
            {
                articleCount = value;
            }
        }

        public Item[] Articles
        {
            get
            {
                return articles;
            }

            set
            {
                articles = value;
            }
        }
    }
}