using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    public class WeChatJssdk
    {
        private string jsapi_ticket;
        private string noncestr;
        private string timestamp;
        private string url;
        private string string1;
        private string signature;
        private string appId;
        public string Signature
        {
            get
            {
                return signature;
            }

            set
            {
                signature = value;
            }
        }

        public string String1
        {
            get
            {
                return string1;
            }

            set
            {
                string1 = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public string Timestamp
        {
            get
            {
                return timestamp;
            }

            set
            {
                timestamp = value;
            }
        }

        public string Noncestr
        {
            get
            {
                return noncestr;
            }

            set
            {
                noncestr = value;
            }
        }

        public string Jsapi_ticket
        {
            get
            {
                return jsapi_ticket;
            }

            set
            {
                jsapi_ticket = value;
            }
        }

        public string AppId
        {
            get
            {
                return appId;
            }

            set
            {
                appId = value;
            }
        }
    }
}