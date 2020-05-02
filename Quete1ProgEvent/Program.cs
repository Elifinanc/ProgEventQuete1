using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quete1ProgEvent
{
    class Program
    {
        public event EventHandler<SendLogEventArgs> OnSendLog;

        static void Main(string[] args)
        {
            var program = new Program();
            var logger = new StandardOutputLogger();
            var logger1 = new FileStreamOutputLogger();
            logger.Subscribe(program);
            logger1.Subscribe(program);
            var eventArgs = new SendLogEventArgs("LogEvent published", DateTime.Now);
            if (program.OnSendLog != null)
            {
                program.OnSendLog(program, eventArgs);
            }
        }
    }

    class StandardOutputLogger
    {
        public void Subscribe(Program program)
        {
            program.OnSendLog += OnLogSend;
        }

        public void OnLogSend(object sender, SendLogEventArgs args)
        {
            Write(args.Message, args.DateTime);
        }

        public void Write(String message, DateTime? dateTime = null)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            String formattedMessage = String.Format("{0} - {1}", dateTime, message);
            Console.WriteLine(formattedMessage);
            Console.ReadLine();
        }
    }

    class SendLogEventArgs : EventArgs
    {
        public String Message;
        public DateTime DateTime;

        public SendLogEventArgs(String message, DateTime dateTime)
        {
            Message = message;
            DateTime = dateTime;
        }
    }

    class FileStreamOutputLogger
    {
        public void Subscribe(Program program)
        {
            program.OnSendLog += OnLogSend;
        }

        public void OnLogSend(object sender, SendLogEventArgs args)
        {
            Write(args.Message, args.DateTime);
        }

        public void Write(String message, DateTime? dateTime = null)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            String formattedMessage = String.Format("{0} - {1}", dateTime, message);
            string root = @"C:/Users/konuk/source/repos/Quete1ProgEvent/Quete1ProgEvent/bin/Debug";
            TextWriter TextWrite = new StreamWriter(root + "/textwriter.txt");
            TextWrite.Write(formattedMessage);
            TextWrite.Flush();
            TextWrite.Close();
            TextWrite = null;
        }
    }
}




 
  