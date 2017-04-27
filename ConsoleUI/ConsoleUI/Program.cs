using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:log4net.Config.XmlConfigurator(Watch =true)]

namespace ConsoleUI
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!!!");
            //log.Debug("Debug");
            //log.Info("Info");
            //log.Warn("Warn");
            // int i = 0;
            //try
            //{
            //    var x = 10 / i;
            //}
            //catch (DivideByZeroException ex)
            //{
            //    log.Error("ERROR",ex);
            //}
            Counter count = new Counter();
            count.LoopCounter++;
            string name = (System.Reflection.MethodBase.GetCurrentMethod().Name).ToString();
            string parentClass = (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).ToString();
            log4net.GlobalContext.Properties["Counter"] = parentClass + "/"+ name + "  hitCount("+ count + ")";

            log.Fatal("Fatal");
            //while (i < 1000)
            //{
            //    count.LoopCounter++;
            //    log.Error("Error Encountered");
            //    log4net.GlobalContext.Properties["Counter"] = parentClass + "/" + name + "  hitCount(" + count + ")";
            //}
            Console.ReadLine();
        }
    }
}
