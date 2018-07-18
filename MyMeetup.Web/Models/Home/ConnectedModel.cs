using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Models.Home
{
    public class ConnectedModel
    {
        public ConnectedModel()
        {
        }

        public MyMeetupUser CurrentUser { get; set; }
        public string NextMeetupText { get; set; }

        public ConnectedModel(MyMeetupUser user)
        {
            CurrentUser = user;
        }

    }
}
