using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBokningSystem
{
   public class LinqStatement
    {
        DataClasses1DataContext dcc = new DataClasses1DataContext();
        system ss = new system();

        public void insertStatement(string username, string pass, string date, string selectedTime)
        {            
            ss.name = username;
            ss.password = pass;
            ss.day = date;
            ss.time = selectedTime;
            dcc.systems.InsertOnSubmit(ss);
            dcc.SubmitChanges();
        }

        public void deleteStatement(string daySub, string timeSub)
        {
            ss = dcc.systems.SingleOrDefault(x => x.day == daySub && x.time == timeSub);
            dcc.systems.DeleteOnSubmit(ss);
            dcc.SubmitChanges();
        }

        public IEnumerable<system> compareTime(string date)
        {
            return
            from user in dcc.systems
                           where user.day == date
                           select user;                      
        }
    }
}
