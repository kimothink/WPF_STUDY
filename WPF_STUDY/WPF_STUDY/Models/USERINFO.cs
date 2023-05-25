using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_STUDY.Models
{
    class USERINFO
    {
		private string username;
        private string userimg;
        private int userage;

        public string USERNAME
        {
			get { return username; }
			set { username = value; }
		}


		public string USERIMG
		{
			get { return userimg; }
			set { userimg = value; }
		}


		public int USERAGE
		{
			get { return userage; }
			set { userage = value; }
		}


	}
}
