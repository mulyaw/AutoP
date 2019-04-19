using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.Timers;

namespace autocons
{
    class Program
    {
        private static IWebDriver driver = null;
        static void Main(string[] args)
        {
            Console.Title = ("AutoPro");
            login();
            promoted();            
        }

        static void login()
        {
            ChromeDriverService servis = ChromeDriverService.CreateDefaultService();
            servis.HideCommandPromptWindow = true;
            ChromeOptions opt = new ChromeOptions();
            opt.AddArgument("--silent-launch");
            driver = new ChromeDriver(servis);
            while (true)
                try
                {
                    Console.WriteLine("Loading Page...");
                    driver.Navigate().GoToUrl("https://accounts.tokopedia.com/authorize?client_id=1001&p=https%3A%2F%2Fwww.tokopedia.com&redirect_uri=https%3A%2F%2Fwww.tokopedia.com%2Fappauth%2Fcode&response_type=code&state=eyJyZWYiOiJodHRwczovL3d3dy50b2tvcGVkaWEuY29tIiwidXVpZCI6ImQ3NzkzYzNkLTE3ZTQtNDA4OS04ZDFlLTEyZjA5ODFhMDMyMCJ9");
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                    var email = driver.FindElement(By.XPath("//input[@name='email']"));
                    Console.Clear();
                    Console.WriteLine("Silahkan Masukan Email...");
                    string temail = Console.ReadLine();
                    email.SendKeys(temail);

                    var pass = driver.FindElement(By.XPath("//input[@name='password']"));
                    Console.WriteLine("Silahkan Masukan Password...");
                    //hide pass text//////////////////
                    var fc = Console.ForegroundColor;
                    Console.ForegroundColor = Console.BackgroundColor;
                    var tpass = Console.ReadLine();
                    Console.ForegroundColor = fc;
                    /////////////////////////////////
                    pass.SendKeys(tpass);
                    
                    Console.Clear();
                    Console.WriteLine("Loading Page...");

                    var login = driver.FindElement(By.XPath("//button[@id='login-submit']"));
                    login.Click();
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("Login Succeed...");
                    Thread.Sleep(3000);
                    Console.Clear();

                    break;
                }
                catch
                {
                    continue;
                }
        }
        static void promoted()
        {            
            Console.WriteLine("Starting...");
            string link;
            string file = "link.txt";
            StreamReader srfile = new StreamReader(file);

            while (srfile.EndOfStream == false)
            {
                link = srfile.ReadLine();
                //Console.WriteLine(link);
                driver.Navigate().GoToUrl(Convert.ToString(link));

                Thread.Sleep(TimeSpan.FromSeconds(5));
                var klik = driver.FindElement(By.XPath("//*[@id='dink-it']"));//("//span[contains(.,'Promo Perjam')]"));
                klik.Click();
                Thread.Sleep(TimeSpan.FromSeconds(5));
                var ok = driver.FindElement(By.XPath("//button[contains(.,'Ok')]"));
                ok.Click();

                Console.Clear();
                Console.WriteLine(DateTime.Now.ToString("HH:mm dddd, dd MMMM yyyy") + " Product | " + link + " | Has Succesfully Promoted...");

                Console.WriteLine("Please Wait for 60 minutes before next product promoted...");//("Time remaining for next product promoted " + timer + " Minutes");
                Thread.Sleep(TimeSpan.FromHours(1));                
            }
            //Console.ReadLine();
            //srfile.Close();
        }
    }
}
