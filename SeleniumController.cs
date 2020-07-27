using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace DynamicsStressTest
{

    class SeleniumController
    {
        private IWebDriver driver;
        private String BASE_URL;

        /*
         Sets up the ChromeDriver instance
         wait       - String Wait until test sequence starts (wait for MFA)
         greeting   - String explains to the user what the wait is for
         baseUrl    - String base url for driver
             */
        public void SetupTest(int wait, String greeting, String baseUrl, bool headless)
        {
            if (headless == true)
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments(new List<string>() {
                "--silent-launch",
                "--no-startup-window",
                "no-sandbox",
                "headless",});

                BASE_URL = baseUrl;
                driver = new ChromeDriver(chromeOptions);
                driver.Navigate().GoToUrl(BASE_URL);
                Sleep(wait, greeting);
            }
            else
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments(new List<string>() {
                "--silent-launch",
                "--no-startup-window",
                "no-sandbox",
                "headless",});

                BASE_URL = baseUrl;
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl(BASE_URL);
                Sleep(wait, greeting);

            }
        }

        
        /*
         Starts the tes sequence/BPM for which the instance of the class is designed
         Returns the completition time of the test*/
        public int StartUserTest()
        {
            int completionTime = 0;
            WaitOnResponseByID("defaultdashboard_1_BankTreasurerWorkspace_text");
            completionTime = WaitOnResponseByID("BankTreasurerWorkspace_2_BankStatements_text");
            driver.Navigate().GoToUrl(BASE_URL);

            return completionTime;
        }


        /*
         Sleep function, forces a wait
         seconds    - int seconds to wait
         action     - string action (describes to user what the sleep is being used for
         */
        public void Sleep(int seconds, String action)
        {
            for (int i = 1; i < (seconds + 1); i++)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Wait " + i + " of " + seconds + "s");
            }
        }


        /*
         Waits for the click by id function to have the id it requires loaded on browser...
         Returns the time in seconds it took for the clickable id to be loaded on the page
         id         - string this is the DOM element
         */
        public int WaitOnResponseByID(String id)
        {
            bool loaded = false;
            int completionTime = 0;

            do
            {
                try
                {
                    driver.FindElement(By.Id(id)).Click();
                    loaded = true;
                }
                catch
                {
                    Sleep(1, "Loading...");
                    completionTime++;
                    loaded = false;

                    if (completionTime > Program.TIMEOUTTASK)    //Escape loading loop after n seconds
                    {
                        loaded = true;
                    }
                }
            } while (loaded == false);
            return completionTime;
        }

        public void StopTest()
        {
            driver.Close();
        }
    }    
}
