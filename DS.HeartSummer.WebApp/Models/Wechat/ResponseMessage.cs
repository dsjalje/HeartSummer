using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    public class ResponseMessage : MessageBase
    {

        public string CreateTime
        {
            get
            {
                return new WeChatHelper().GetTime().ToString();
            }
        }
        public ResponseMsgType MsgType { get; set; }

        /// <summary>
        /// text
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// news
        /// </summary>
        public int ArticleCount { get; set; }
        //多条图文信息
        public Item[] Articles { get; set; }
        //非必须	图文消息标题
        public string Title { get; set; }
        //非必须	图文消息描述
        public string Description { get; set; }
        // 否	图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200 
        public string PicUrl { get; set; }
        //	否	点击图文消息跳转链接
        public string Url { get; set; }

        /// <summary>
        /// music
        /// </summary>
        public string MusicURL { get; set; }
        //Title	否	音乐标题
        //Description 否   音乐描述
        //否   高质量音乐链接，WIFI环境优先使用该链接播放音乐
        public string HQMusicUrl { get; set; }
        //    否 缩略图的媒体id，通过素材管理接口上传多媒体文件，得到的id
        public string ThumbMediaId { get; set; }

    }
}