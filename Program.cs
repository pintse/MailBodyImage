using System;

namespace MailBodyImage {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World!");
            SMTPService sMTPService = new SMTPService ();
            sMTPService.SendMail ();

        }
    }
}