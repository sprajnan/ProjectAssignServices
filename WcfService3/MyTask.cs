using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService3
{
    public class MyTask
    {
       
        public string task_title { get; set; }
        public string task_category { get; set; }
        public string due_date { get; set; }
        public string task_description { get; set; }
        public int task_priority { get; set; }
    }
}