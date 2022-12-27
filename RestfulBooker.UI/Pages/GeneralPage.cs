using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace RestfulBooker.UI.Pages
{
    /** Abstract class with the logic shared between the
     * pages
     */
    public abstract class GeneralPage
    {
        protected readonly IWebDriver driver;

        protected WebDriverWait wait;

        /*
         * List to keep track of the dom events when the DOM mutation
         * listener is enabled
         */
        private List<string> domEventsList;

        /**
         * Variables to keep track of the network requests
         * when the network interception is enabled
         */
        private string networkUrl = "";
        private string networkMethod = "";
        private string currentNetworkId = "";
        protected bool networkResponseReceived = false;
        private NetworkResponseReceivedEventArgs? receivedResponse;

        public GeneralPage(IWebDriver driver)
        {
            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            domEventsList = new List<string>();
        }

        public abstract string Url
        {
            get;
        }

        /**
         * Method to start listening for DOM mutation
         * events
         * //https://www.selenium.dev/documentation/webdriver/bidirectional/bidi_api/#mutation-observation
         */
        protected async Task EnableMutationListener()
        {
            IJavaScriptEngine monitor = new JavaScriptEngine(driver);

            monitor.DomMutated += HandleDomMutatedEvent;

            await monitor.StartEventMonitoring();
            await monitor.EnableDomMutationMonitoring();
            Console.WriteLine("DOM MUTATION LISTENER ADDED");
        }

        /**
         * Method to handle the DomMutated event
         */
        private void HandleDomMutatedEvent(object sender, DomMutatedEventArgs e)
        {
            Console.WriteLine("\nDOM MUTATED");
            var mutatedData = e.AttributeData;

            Console.WriteLine("Sender: {0}", sender.ToString());
            Console.WriteLine("Event: {0}", e.ToString());

            Console.WriteLine("Attribute name: {0}", mutatedData.AttributeName);
            Console.WriteLine("Attribute value: {0}", mutatedData.AttributeValue);
            Console.WriteLine("Original value: {0}", mutatedData.AttributeOriginalValue);
            Console.WriteLine("Target Id: {0}", mutatedData.TargetId);

            domEventsList.Add(mutatedData.AttributeName);
        }

        /**
         * Method to disable the DOM mutation listener
         * and stop monitoring this event
         */
        protected async Task DisableMutationListener()
        {
            IJavaScriptEngine monitor = new JavaScriptEngine(driver);
            monitor.StopEventMonitoring();
            await monitor.DisableDomMutationMonitoring();
            monitor.DomMutated -= HandleDomMutatedEvent;
            domEventsList.Clear();
            Console.WriteLine("DOM MUTATION LISTENER DISABLED");
        }

        /**
         * Method to wait until a DOM mutation event
         * is detected
         */
        protected void waitUntiDomMutates()
        {
            Console.WriteLine("Waiting until DOM mutates");
            DefaultWait < List<string> > domWait = new DefaultWait<List<string>>(domEventsList);

            domWait.Timeout = TimeSpan.FromSeconds(60);
            domWait.PollingInterval = TimeSpan.FromMilliseconds(500);

            domWait.Until(list => {
                Console.WriteLine("Inside the Until {0}",list.Count);
                return list.Count > 0;
            });

            Console.WriteLine("Wait completed, clearing the list of events");

            domEventsList.Clear();
            Console.WriteLine("Event list count {0}", domEventsList.Count);
        }


        //NETWORK PROOF OF CONCEPT
        /**
         * Method to simulate a slow coneection
         */
        protected void SetSlowConnection()
        {
            if (driver is ChromeDriver)
            {
                var chromeDriver = driver as ChromeDriver;

                chromeDriver.NetworkConditions =
                    new OpenQA.Selenium.Chromium.ChromiumNetworkConditions()
                    {
                        //DownloadThroughput = 500,
                        UploadThroughput = 500,
                        Latency = TimeSpan.FromMilliseconds(1000)
                    };
            }
        }

        /**
         * Method to reset the connection speed to normal
         */
        protected void ResetConnectionSpeed()
        {
            if (driver is ChromeDriver)
            {
                var chromeDriver = driver as ChromeDriver;

                chromeDriver.ClearNetworkConditions();
            }
        }

        /**
        * Method to start intercepting the network requests
        * that match with the given URL and HTTP method
        */
        protected async Task InterceptNetworkRequest(string matchingUrl, string method)
        {
            this.networkUrl = matchingUrl;
            this.networkMethod = method;
            driver.Manage().Network.NetworkRequestSent += RequestSent;
            driver.Manage().Network.NetworkResponseReceived += ResponseReceived;
            await driver.Manage().Network.StartMonitoring();
        }

        /**
         * Method to stop intercepting the network requests
         */
        protected async Task StopInterceptingRequests()
        {
            this.networkUrl = "";
            this.networkMethod = "";
            driver.Manage().Network.NetworkRequestSent -= RequestSent;
            driver.Manage().Network.NetworkResponseReceived -= ResponseReceived;
            await driver.Manage().Network.StopMonitoring();
        }

        /**
         * Method to handle the RequestSent event
         */
        private void RequestSent(object? sender, NetworkRequestSentEventArgs e)
        {
            Console.WriteLine("HTTP Request intercepted: {0}", e.RequestUrl);

            if (e.RequestUrl.Contains(networkUrl) && e.RequestMethod.Equals(networkMethod))
            {
                Console.WriteLine("Id: {0} \nMethod: {1}", e.RequestId, e.RequestMethod );
                this.currentNetworkId = e.RequestId;
            }
            else
            {
                Console.WriteLine("Request to be ignored");
            }
        }

        /*
         * Method to handle the Response received event
         */
        private void ResponseReceived(object? sender, NetworkResponseReceivedEventArgs e)
        {
            Console.WriteLine("HTTP response received: {0}", e.ResponseUrl);

            if (e.RequestId == this.currentNetworkId)
            {
                Console.WriteLine("Id: {0} \nStatus: {1}", e.RequestId, e.ResponseStatusCode);
                this.networkResponseReceived = true;
                this.receivedResponse = e;
            }
            else
            {
                Console.WriteLine("Response to be ignored");
            }
        }

        /**
         * Method to wait until an intercepted url reponse is received
         */
        protected void waitUntilApiResponds()
        {
            Console.WriteLine("Waiting until the API responds");
            DefaultWait<bool> networkWait = new DefaultWait<bool>( networkResponseReceived );
            networkWait.Timeout = TimeSpan.FromSeconds(20);

            networkWait.Until(response => networkResponseReceived);
            Console.WriteLine("API Responded");
            networkResponseReceived = false;
        }
    }
}

