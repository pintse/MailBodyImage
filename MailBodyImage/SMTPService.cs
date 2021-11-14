using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace MailBodyImage {
    public class SMTPService {
        public void SendMail () {
            try {
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage ();
                msg.From = new MailAddress ("ryan@gZZZ.com", "RYAN", System.Text.Encoding.UTF8); //參數分別是發件人地址，發件人名稱，編碼
                msg.Subject = "Mail body include image"; //郵件標題
                msg.SubjectEncoding = System.Text.Encoding.UTF8; //郵件標題編碼

                msg.To.Add ("ryan.chen@ZZZ.com"); //正本收件人，可以發送給多人
                //msg.CC.Add("XXX@ZZZ.com"); //副本收件人，可以發送給多人
                //msg.Bcc.Add ("YYY@ZZZ.com"); //密件副本收件人，可以發送給多人

                msg.AlternateViews.Add (MailBody ()); //郵件內容，有插圖的信件，使用此方法
                // msg.Body = ReadHTMLFile (); //郵件內容，沒插圖的信件，使用此方法
                msg.BodyEncoding = System.Text.Encoding.UTF8; //郵件內容編碼 
                // msg.Attachments.Add (new Attachment (@"D:\temp\test.docx")); //附件
                msg.IsBodyHtml = true; //是否是HTML郵件 
                //msg.Priority = MailPriority.High;//郵件優先級 

                SmtpClient client = new SmtpClient ();
                // client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential ("ryan@ZZZ.com", "password"); //這裡要填正確的帳號跟密碼
                client.Host = "smtp.gmail.com"; //設定smtp Server
                client.Port = 25; //設定Port
                client.EnableSsl = true; //gmail預設開啟驗證
                client.Send (msg); //寄出信件
                client.Dispose ();
                msg.Dispose ();
                Console.WriteLine ("郵件寄送成功！");
            } catch (Exception ex) {
                Console.WriteLine ("Exception::" + ex.Message);

            }
        }

        public string ReadHTMLFile () {

            string path = @"mail_template.html";
            string readText = File.ReadAllText (path);
            Console.WriteLine (readText);

            return readText;

        }

        private AlternateView MailBody () {
            string path = @"logo.png";
            LinkedResource Img = new LinkedResource (path, MediaTypeNames.Image.Jpeg);
            Img.ContentId = "logo.png";

            string str = ReadHTMLFile ();
            AlternateView AV =
                AlternateView.CreateAlternateViewFromString (str, null, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add (Img);
            return AV;
        }
    }
}