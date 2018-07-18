using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyMeetUp.Logic.Infrastructure
{
  public  class MyMeetupSqlLiteContext:MyMeetupContext
    {
        //https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/provider
        public MyMeetupSqlLiteContext(DbContextOptions options) : base(options)
        {
        }
    }
}
