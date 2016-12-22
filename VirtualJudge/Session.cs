using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualJudge
{
    public class Session
    {
        private static string UserID;
        private static string UserName;
        private static string Name;

        public static string getLoggedName()
        {
            return Session.UserID;
        }

        public static void setUserName(string UserID)
        {
            Session.UserID = UserID;
        }

        public static void GetUserName(string UserID)
        {
            Session.UserID = UserID;
        }
    }
    
}
