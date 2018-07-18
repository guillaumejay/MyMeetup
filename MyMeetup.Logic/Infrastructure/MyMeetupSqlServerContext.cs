using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyMeetUp.Logic.Infrastructure
{
    //Add-Migration InitialCreate -Context MyMeetupSqlServerContext -OutputDir Migrations\SqlServerMigrations
    public class MyMeetupSqlServerContext:MyMeetupContext
    {
        public MyMeetupSqlServerContext(DbContextOptions options) : base(options)
        {
        }
    }
}
