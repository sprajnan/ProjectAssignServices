using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WcfService3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        SqlConnection con = new SqlConnection("Data Source=tcp:g0h94rjgzp.database.windows.net,1433;Initial Catalog=HackDuke;Persist Security Info=True;User ID=sprajnan@g0h94rjgzp;Password=Hackduke2013");
        SqlCommand cmd, cmd1, cmd2;
        SqlDataReader dr, dr1;
        SqlDataAdapter da;
        Random r = new Random();
        public List<Name> GetData(string value)
        {
            con.Open();
            int id = r.Next(1000);
            cmd = new SqlCommand("insert into DemoTable values("+id+",'"+value+"')",con);
            int res = cmd.ExecuteNonQuery();
            if (res == 1)
            {
                con.Close();
                Name n = new Name();
                n.my_name = value;
                List<Name> l = new List<Name>();
                l.Add(n);
                return l;
            }
            else
            {
                con.Close();
                Name n = new Name();
                n.my_name = "fail";
                List<Name> l = new List<Name>();
                l.Add(n);
                return l;
            } 
           
        }
        // Verification code generator
        private string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        // Registration of user Service method
        public string UserRegistration(string email, string password)
        {
            string user_email = email;
            string user_password = password;
            string code = null;
            con.Open();
            // Check for user id first
            cmd1 = new SqlCommand("select count(*) from Users where user_email='"+user_email+"'",con);
            dr = cmd1.ExecuteReader();
            dr.Read();
            if (int.Parse(dr.GetValue(0).ToString()) == 0)
            {
                // New user registration
                dr.Close();
                cmd = new SqlCommand("insert into Users(user_email,user_password) values('" + user_email + "','" + user_password + "')", con);
                int i = cmd.ExecuteNonQuery();
                // insert into user preferences table
                // first retreive user_id from Users table
                cmd2 = new SqlCommand("select user_id from Users where user_email='"+user_email+"'",con);
                dr = cmd2.ExecuteReader();
                dr.Read();
                int user_id = int.Parse(dr.GetValue(0).ToString());
                dr.Close();

                cmd1 = new SqlCommand("insert into UserPreference values("+user_id+",3,7,7)",con);
                int j = cmd1.ExecuteNonQuery();
                if (i == 1)
                {

                    // Verification Code

                    code = RandomString(7);
                    // Mail
                    try
                    {
                        MailMessage mail = new MailMessage();
                        mail.To.Add(user_email);
                        mail.To.Add("assignme2013@gmail.com");
                        mail.From = new MailAddress("assignme2013@gmail.com");
                        mail.Subject = "Welcome to Assign me!";

                        string Body = "Hi , \n You have been successfully registered with Assign Me. Your Verification Code is :"+code;
                        mail.Body = Body;

                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                        smtp.Credentials = new System.Net.NetworkCredential
                             ("assignme2013@gmail.com", "assignme@2013");
                        //Or your Smtp Email ID and Password
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        // SMS 
                        // HttpWebRequest request = WebRequest.Create("http://alerts.adeeptechnologies.com/api/web2sms.php?username=nstgmc&password=nstgmc123&to=" +q[2]+ "&sender=NSTGMC&message=Hey "+q[1]+"Welcome to School Pandit. You have been successfully registered. Subject to approval. Your password has been sent to your email id : "+q[3]+"") as HttpWebRequest;
                        // HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        return code;
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }

                }
                else
                    return "Failure";
            }
            else
            { 
                // User already exists
                dr.Close();
                return "User already exists";
            }
        }

        // Login Service method
       public string UserLogin(string email, string password)
        {
            string user_email = email;
            string user_entered_password = password;
            con.Open();
            cmd = new SqlCommand("select user_password from Users where user_email='"+user_email+"'",con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                // User exists , check password
                string user_password = dr.GetValue(0).ToString();
                dr.Close();
                if (user_password.Equals(user_entered_password))
                {
                       // Password correct
                    return "Success"; 
                }
                else
                { 
                        //Password incorrect 
                    return "Password Incorrect";
                }
            }
            else
            {
                // User Email doesnt exist
                dr.Close();
                return "User doesnt exist";
            }
             
        } 

        // Forgot Password Service method
       public string ForgotPassword(string email)
       {
           string user_email = email;
           con.Open();
           cmd = new SqlCommand("select user_password from Users where user_email='"+user_email+"'",con);
           dr = cmd.ExecuteReader();
           if (dr.Read())
           {
            // User exists so send password to email
               string password = dr.GetValue(0).ToString();
               dr.Close();
               // Mail
               try
               {
                   MailMessage mail = new MailMessage();
                   mail.To.Add(user_email);
                   mail.To.Add("assignme2013@gmail.com");
                   mail.From = new MailAddress("assignme2013@gmail.com");
                   mail.Subject = "Forgot Password : Assign Me!";

                   string Body = "Hi , Your password is : "+password;
                   mail.Body = Body;

                   mail.IsBodyHtml = true;
                   SmtpClient smtp = new SmtpClient();
                   smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                   smtp.Credentials = new System.Net.NetworkCredential
                        ("assignme2013@gmail.com", "assignme@2013");
                   //Or your Smtp Email ID and Password
                   smtp.EnableSsl = true;
                   smtp.Send(mail);
                   // SMS 
                   // HttpWebRequest request = WebRequest.Create("http://alerts.adeeptechnologies.com/api/web2sms.php?username=nstgmc&password=nstgmc123&to=" +q[2]+ "&sender=NSTGMC&message=Hey "+q[1]+"Welcome to School Pandit. You have been successfully registered. Subject to approval. Your password has been sent to your email id : "+q[3]+"") as HttpWebRequest;
                   // HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                   return "Success";
               }
               catch (Exception e)
               {
                   return e.ToString();
               }

           }
           else
           { 
            // user doesnt exist
               dr.Close();
               return "Failure";
           }
       }
        // New task service method
       public string NewTask(string email, string category, string title, string description, string date, string priority)
       { 
        // retr user id from email
           con.Open();
           cmd = new SqlCommand("select user_id from Users where user_email='"+email+"'",con);
           dr = cmd.ExecuteReader();
           dr.Read();
           int user_id = int.Parse(dr.GetValue(0).ToString());
           dr.Close();
           cmd = new SqlCommand("insert into Tasks(user_id,task_category,task_title,task_description,task_deadline,task_status,task_priority) values("+user_id+",'"+category+"','"+title+"','"+description+"','"+date+"',1,"+int.Parse(priority)+")",con);
           int i =cmd.ExecuteNonQuery();
           if (i == 1)
           {

               return "Success";
           }
           else
           {
               return "Failure";
           }
       }

        // Service Method to retrieve all tasks
       public List<Task> getAllTasks(string email) 
       {
           con.Open();
           cmd = new SqlCommand("select user_id from Users where user_email='" + email + "'", con);
           dr = cmd.ExecuteReader();
           dr.Read();
           int user_id = int.Parse(dr.GetValue(0).ToString());
           dr.Close();
           cmd = new SqlCommand("select task_id,task_title,task_category,task_deadline from Tasks where user_id="+user_id+" and task_status=1 order by task_deadline,task_priority",con);
           da = new SqlDataAdapter();
           da.SelectCommand = cmd;
           DataSet myDataSet = new DataSet();
           da.Fill(myDataSet);
           List<Task> list = new List<Task>();
           foreach (DataTable table in myDataSet.Tables)
           {
               foreach (DataRow row in table.Rows)
               {
                 
                   Task t = new Task();
                   t.task_id = int.Parse(row["task_id"].ToString());
                   t.task_title = row["task_title"].ToString();
                   t.task_category = row["task_category"].ToString();
                   DateTime now = DateTime.Now;

                   string current_date = now.ToString("u");
                   string deadline = row["task_deadline"].ToString();
                   DateTime enteredDate = DateTime.Parse(deadline);
                  // Date dt = Date.ParseExact(deadline,"yyyy-dd-mm",CultureInfo.InvariantCulture);
                   int diff = (enteredDate.Date - now.Date).Days;
                   t.no_of_days = diff;
                   list.Add(t);


               }
           }

           return list;
       
       }
       // Service Method to retrieve all tasks
       public List<Task> getAllProjects(string email)
       {
           con.Open();
           cmd = new SqlCommand("select user_id from Users where user_email='" + email + "'", con);
           dr = cmd.ExecuteReader();
           dr.Read();
           int user_id = int.Parse(dr.GetValue(0).ToString());
           dr.Close();
           cmd = new SqlCommand("select task_id,task_title,task_category,task_deadline from Tasks where user_id=" + user_id + " and task_status=1 and task_category='Projects' order by task_deadline,task_priority", con);
           da = new SqlDataAdapter();
           da.SelectCommand = cmd;
           DataSet myDataSet = new DataSet();
           da.Fill(myDataSet);
           List<Task> list = new List<Task>();
           foreach (DataTable table in myDataSet.Tables)
           {
               foreach (DataRow row in table.Rows)
               {

                   Task t = new Task();
                   t.task_id = int.Parse(row["task_id"].ToString());
                   t.task_title = row["task_title"].ToString();
                   t.task_category = row["task_category"].ToString();
                   DateTime now = DateTime.Now;

                   string current_date = now.ToString("u");
                   string deadline = row["task_deadline"].ToString();
                   DateTime enteredDate = DateTime.Parse(deadline);
                   // Date dt = Date.ParseExact(deadline,"yyyy-dd-mm",CultureInfo.InvariantCulture);
                   int diff = (enteredDate.Date - now.Date).Days;
                   t.no_of_days = diff;
                   list.Add(t);


               }
           }

           return list;

       }

       public List<Task> getAllAssignments(string email)
       {
           con.Open();
           cmd = new SqlCommand("select user_id from Users where user_email='" + email + "'", con);
           dr = cmd.ExecuteReader();
           dr.Read();
           int user_id = int.Parse(dr.GetValue(0).ToString());
           dr.Close();
           cmd = new SqlCommand("select task_id,task_title,task_category,task_deadline from Tasks where user_id=" + user_id + " and task_status=1 and task_category='Assignments' order by task_deadline,task_priority", con);
           da = new SqlDataAdapter();
           da.SelectCommand = cmd;
           DataSet myDataSet = new DataSet();
           da.Fill(myDataSet);
           List<Task> list = new List<Task>();
           foreach (DataTable table in myDataSet.Tables)
           {
               foreach (DataRow row in table.Rows)
               {

                   Task t = new Task();
                   t.task_id = int.Parse(row["task_id"].ToString());
                   t.task_title = row["task_title"].ToString();
                   t.task_category = row["task_category"].ToString();
                   DateTime now = DateTime.Now;

                   string current_date = now.ToString("u");
                   string deadline = row["task_deadline"].ToString();
                   DateTime enteredDate = DateTime.Parse(deadline);
                   // Date dt = Date.ParseExact(deadline,"yyyy-dd-mm",CultureInfo.InvariantCulture);
                   int diff = (enteredDate.Date - now.Date).Days;
                   t.no_of_days = diff;
                   list.Add(t);


               }
           }

           return list;

       }

       public List<Task> getAllHomeworks(string email)
       {
           con.Open();
           cmd = new SqlCommand("select user_id from Users where user_email='" + email + "'", con);
           dr = cmd.ExecuteReader();
           dr.Read();
           int user_id = int.Parse(dr.GetValue(0).ToString());
           dr.Close();
           cmd = new SqlCommand("select task_id,task_title,task_category,task_deadline from Tasks where user_id=" + user_id + " and task_status=1 and task_category='Homeworks' order by task_deadline,task_priority", con);
           da = new SqlDataAdapter();
           da.SelectCommand = cmd;
           DataSet myDataSet = new DataSet();
           da.Fill(myDataSet);
           List<Task> list = new List<Task>();
           foreach (DataTable table in myDataSet.Tables)
           {
               foreach (DataRow row in table.Rows)
               {

                   Task t = new Task();
                   t.task_id = int.Parse(row["task_id"].ToString());
                   t.task_title = row["task_title"].ToString();
                   t.task_category = row["task_category"].ToString();
                   DateTime now = DateTime.Now;

                   string current_date = now.ToString("u");
                   string deadline = row["task_deadline"].ToString();
                   DateTime enteredDate = DateTime.Parse(deadline);
                   // Date dt = Date.ParseExact(deadline,"yyyy-dd-mm",CultureInfo.InvariantCulture);
                   int diff = (enteredDate.Date - now.Date).Days;
                   t.no_of_days = diff;
                   list.Add(t);


               }
           }

           return list;

       }

       public List<MyTask> getTask(string id)
       {
           int task_id = int.Parse(id);
           con.Open();
           cmd = new SqlCommand("select task_title,task_description,task_category,task_deadline,task_priority from Tasks where task_id ="+task_id+"",con);
           dr = cmd.ExecuteReader();
           dr.Read();
           MyTask m = new MyTask();
           m.task_title = dr.GetValue(0).ToString();
           m.task_description = dr.GetValue(1).ToString();
           m.task_category = dr.GetValue(2).ToString();
           string date = dr.GetValue(3).ToString();
         DateTime enteredDate = DateTime.Parse(date);
         m.due_date = enteredDate.ToString("D");
           m.task_priority = int.Parse(dr.GetValue(4).ToString());
           List<MyTask> list = new List<MyTask>();
           list.Add(m);
           return list;
       
       }

       public string deleteTask(string id)
       {
           int task_id = int.Parse(id);
           con.Open();
           cmd = new SqlCommand("update Tasks set task_status=0 where task_id="+task_id+"",con);
           cmd.ExecuteNonQuery();
           return "Success";
       }





        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
