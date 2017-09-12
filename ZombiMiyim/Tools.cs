using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Data;
using System.Reflection;

namespace ZombiMiyim
{
    public class Tools
    {
        public static bool MailGonder(string emailadresi,string subject,string body)
        {
           
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("tohumyazilim@gmail.com");
            try
            {
                ePosta.To.Add(emailadresi);
            }
            catch
            {   }
       
            //ePosta.Attachments.Add(new Attachment(@"C:\deneme.txt"));   //
            ePosta.Subject = subject;
            ePosta.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("tohumyazilim@gmail.com", "Tohum_Yazilim"); // Mail Şifresi
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta);
            }
            catch 
            {
                kontrol = false;
            }
            return kontrol;
        }

      public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable();
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }
}