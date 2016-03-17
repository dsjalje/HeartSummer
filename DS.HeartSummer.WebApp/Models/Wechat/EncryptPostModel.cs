using DS.HeartSummer.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    /// <summary>
    /// 接收加密信息统一基类
    /// </summary>
    public class EncryptPostModel
    {
        private string signature;
        private string timestamp;
        private string nonce;
        private string echostr;
        private string token;

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

        public string Nonce
        {
            get
            {
                return nonce;
            }

            set
            {
                nonce = value;
            }
        }

        public string Echostr
        {
            get
            {
                return echostr;
            }

            set
            {
                echostr = value;
            }
        }

        public string Token
        {
            get
            {
                return new SettingsService().LoadEntities(c => c.Name == "token").FirstOrDefault().value;
            }
        }
    }
}
