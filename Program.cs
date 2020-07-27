/*
 Compile notes:

    Update for dotnet core 2.0: the file appname.runtimeconfig.json (for both debug and release 
    onfiguration) is needed in the same path as appname.dll.

    In solution folder - 
    dotnet publish -c Debug -r win10-x64     
     
 */

using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.Collections.Generic;

namespace DynamicsStressTest
{
    class Program
    {
        public static String DSAHBOARD = "https://DYNAMICS365_URL_GOES_HERE.com/?cmp=Jisc&mi=DefaultDashboard";
        public static double responseTimeSum;
        public static double errorRateSum;
        public static double errorRateTotalAvg;
        public static double responseTimeAvg;
        public static int RECURSIONS = 10;
        public static int TIMEOUTTASK = 30;
        public static int fails;
        public static long taskExecTimeSum;
        public static long taskExecAvg;
        public static String DATETIME = DateTime.Now.ToString("dd/MM/yyyy HH:mm");


        static void Main(string[] args)
        {
            Console.Clear();
            Brand brand = new Brand();
            brand.Jisc();
            Console.WriteLine("Testing on : " + DATETIME);
            Console.WriteLine("\n\nPerformace test for Dynamics F and O\n\nEnter an internal IP for benchmarking > ");
            String remoteServer = Console.ReadLine();

            SeleniumController SelTest1 = new SeleniumController();
            SelTest1.SetupTest(20, "Starting stress test 1, nav Dashboard",
            DSAHBOARD, false);

            SeleniumController SelTest2 = new SeleniumController();
            SelTest2.SetupTest(5, "Starting stress test 2, nav Dashboard",
            DSAHBOARD, true);

            SeleniumController SelTest3 = new SeleniumController();
            SelTest3.SetupTest(5, "Starting stress test 3, nav Dashboard",
            DSAHBOARD, true);

            SeleniumController SelTest4 = new SeleniumController();
            SelTest4.SetupTest(5, "Starting stress test 4, nav Dashboard",
            DSAHBOARD, true);

            SeleniumController SelTest5 = new SeleniumController();
            SelTest5.SetupTest(5, "Starting stress test 5, nav Dashboard",
            DSAHBOARD, true);
                     
            SeleniumController SelTest6 = new SeleniumController();
            SelTest6.SetupTest(5, "Starting stress test 6, nav Dashboard",
            DSAHBOARD, true);

            SeleniumController SelTest7 = new SeleniumController();
            SelTest7.SetupTest(5, "Starting stress test 7, nav Dashboard",
            DSAHBOARD, true);

            SeleniumController SelTest8 = new SeleniumController();
            SelTest8.SetupTest(5, "Starting stress test 8, nav Dashboard",
            DSAHBOARD, true);

            SeleniumController SelTest9 = new SeleniumController();
            SelTest9.SetupTest(5, "Starting stress test 9, nav Dashboard",
            DSAHBOARD, true);

            SeleniumController SelTest10 = new SeleniumController();
            SelTest10.SetupTest(5, "Starting stress test 10, nav Dashboard",
            DSAHBOARD, true);
            
            //Loop through test case
            Pinger pinger = new Pinger();

            for (int i = 0; i < RECURSIONS; i++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                //Launch Lambda/threads for Selenium tests
                Task task1 = Task.Factory.StartNew(() => SelTest1.StartUserTest());
                Task task2 = Task.Factory.StartNew(() => SelTest2.StartUserTest());
                Task task3 = Task.Factory.StartNew(() => SelTest3.StartUserTest());
                Task task4 = Task.Factory.StartNew(() => SelTest4.StartUserTest());
                Task task5 = Task.Factory.StartNew(() => SelTest5.StartUserTest());
                Task task6 = Task.Factory.StartNew(() => SelTest6.StartUserTest());
                Task task7 = Task.Factory.StartNew(() => SelTest7.StartUserTest());
                Task task8 = Task.Factory.StartNew(() => SelTest8.StartUserTest());
                Task task9 = Task.Factory.StartNew(() => SelTest9.StartUserTest());
                Task task10 = Task.Factory.StartNew(() => SelTest10.StartUserTest());

                Task.WaitAll(task1, task2, task3, task4, task5, task6, task7, task8, task9, task10);
                watch.Stop();
                timeTest(watch.ElapsedMilliseconds);

                //Ping operations
                int[] pingReult = new int[2];
                pingReult = pinger.SendPing(remoteServer, 5000); //Setup Ping     "10.100.12.254" 
                responseTimeSum += pingReult[0];
                errorRateSum += pingReult[1];
                Console.Clear();
                Console.WriteLine("Completed " + i + " of " + RECURSIONS);
            }

            //Close tests
            SelTest1.StopTest();
            SelTest2.StopTest();
            SelTest3.StopTest();
            SelTest4.StopTest();
            SelTest5.StopTest();
            SelTest6.StopTest();
            SelTest7.StopTest();
            SelTest8.StopTest();
            SelTest9.StopTest();
            SelTest10.StopTest();

            //reporting calcs
            responseTimeAvg = (responseTimeSum / RECURSIONS);
            errorRateTotalAvg = ((errorRateSum / RECURSIONS) * 100);
            taskExecAvg = (taskExecTimeSum / RECURSIONS);
            fails = ((fails / RECURSIONS) * 100);

            Csv csv = new Csv();
            csv.InitCsv();
            csv.AppendCsv(DATETIME, responseTimeAvg, responseTimeAvg, taskExecAvg, fails);

            Console.Clear();
            Console.WriteLine("\n\n" + DATETIME);
            Console.WriteLine("Avg. Ping time : " + responseTimeAvg + "ms");
            Console.WriteLine("Ping success rate : " + errorRateTotalAvg + "%");
            Console.WriteLine("\n\nAvg. Exec time : " + taskExecAvg + " seconds.");
            Console.WriteLine("Failed : " + fails + "%");
        }  
        
        public static void timeTest(long completionMs)
        {
            long _time = (completionMs / 1000);
            taskExecTimeSum += _time;

            if (_time >= TIMEOUTTASK)
            {
                fails++;
            }
        }
    }
}
