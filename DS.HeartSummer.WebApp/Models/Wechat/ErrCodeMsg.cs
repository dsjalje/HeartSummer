using DS.HeartSummer.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.WebApp.Models.Wechat
{
    public class ErrCodeMsg
    {
        private ReturnCode errcode;
        private string errmsg;
        private string access_token;
        private string expires_in;
        private string ticket;

        //simsi code
        private string response;
        private string result;

        public string Errmsg
        {
            get
            {
                return errmsg;
            }

            set
            {
                errmsg = value;
            }
        }

        public string Access_token
        {
            get
            {
                return access_token;
            }

            set
            {
                access_token = value;
            }
        }

        public string Expires_in
        {
            get
            {
                return expires_in;
            }

            set
            {
                expires_in = value;
            }
        }

        public ReturnCode Errcode
        {
            get
            {
                return errcode;
            }

            set
            {
                errcode = value;
            }
        }

        public string Ticket
        {
            get
            {
                return ticket;
            }

            set
            {
                ticket = value;
            }
        }

        public string Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

        public string Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value;
            }
        }
    }
}
