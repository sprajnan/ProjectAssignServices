using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService3
{
    public class Task
    {
        public int task_id { get; set; }
        public string task_title { get; set; }
        public string task_category { get; set; }
        public int no_of_days { get; set; }
    }
}