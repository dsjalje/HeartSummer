using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    public class Item
    {
        private string title;
        private string description;
        private string picUrl;
        private string url;

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

        public string PicUrl
        {
            get
            {
                return picUrl;
            }

            set
            {
                picUrl = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }




        ////<Title><![CDATA[title1]]></Title> 
        //    <Description><![CDATA[description1]]></Description>
        //    <PicUrl><![CDATA[picurl]]></PicUrl>
        //    <Url><![CDATA[url]]></Url>
    }
}