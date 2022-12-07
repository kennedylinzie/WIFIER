using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFIER
{
    class host_model
    {

        private string hostname;
        private string password;


        public void setname(string name)
        {

            hostname = name;

        }

        public void setpasssword(string pass)
        {

            password = pass;

        }

        public string get_name_
        {

            set { value = hostname; }
            get { return hostname; }

        }
        public string get_pass_
        {

            set { value = password; }
            get { return password; }

        }

    }
}
