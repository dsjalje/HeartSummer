using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.Model
{
    public class CustomMenu
    {

        private string type;
        private string key;
        private string name;
        private string url;
        private List<CustomSubMenu> sub_button;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

 

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        public List<CustomSubMenu> Sub_button
        {
            get
            {
                return sub_button;
            }

            set
            {
                sub_button = value;
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
    }
}
