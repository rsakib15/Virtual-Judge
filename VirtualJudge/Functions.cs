using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualJudge
{
    class Functions
    {
        public static string calculateDuration(string length)
        {
            string duration = "";
            long sec = Convert.ToInt64(length);

            if (sec < 0)
            {
                sec = sec * -1;
            }
            long minutes = sec / 60;
            long hours = minutes / 60;
            minutes = minutes % 60;

            sec = sec % 60;

            if (hours < 10)
            {
                duration = ("0" + hours.ToString() + " Hours " + minutes.ToString() + " Minutes");
            }

            if (minutes < 10)
            {
                duration = (hours.ToString() + " Hours " + "0" + minutes.ToString() + " Minutes");
            }

            if (minutes < 10 && hours < 10)
            {
                duration = ("0" + hours.ToString() + " Hours " + "0" + minutes.ToString() + " Minutes");
            }
            else
            {
                duration = (hours.ToString() + " Hours " + minutes.ToString() + " Minutes");
            }

            return duration;
        }



        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}
