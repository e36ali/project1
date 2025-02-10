using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Set up ChromeOptions for headless mode
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); // Run in headless mode
        options.AddArgument("--disable-gpu"); // Disable GPU acceleration

        // Define the log file path
        string logFilePath = @"C:\Users\U1\Desktop\pro1\LogsAndScreenshots\Log.txt";

        // Create a StreamWriter to write to the log file
        using (StreamWriter logWriter = new StreamWriter(logFilePath, append: true))
        {
            // Redirect Console.WriteLine to the log file
            Console.SetOut(logWriter);

            using (IWebDriver driver = new ChromeDriver(options)) // Pass options to ChromeDriver
            {
                try
                {
                    Console.WriteLine("***** Starting Test Execution *****");
                    Console.WriteLine($"Timestamp: {DateTime.Now}");

                    driver.Navigate().GoToUrl("https://e-commerce-kib.netlify.app/");
                    Thread.Sleep(8000);

                    // Take a screenshot after navigating to the HomePage
                    TakeScreenshot(driver, "1-HomePage.png");

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                    // Test 1: Add a new product
                    AddProductTest(driver, wait);
            
                    // Test 2: Edit the product
                    EditProductTest(driver, wait);
                    
                    // Test 3: Delete the product
                    DeleteProductTest(driver, wait);

                    //Test 4: Search for products
                    SearchProductTest(driver, wait);

                    Console.WriteLine("***** Test Execution Completed Successfully *****");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Take a screenshot if an error occurs
                    TakeScreenshot(driver, "Error.png");
                }
                finally
                {
                    // Close the browser
                    driver.Quit();
                }
            }
        }
    }

    static void AddProductTest(IWebDriver driver, WebDriverWait wait)
    {
        Console.WriteLine("--- Running Add Product Test ---");
        Thread.Sleep(5000);

        var a1 = driver.FindElement(By.XPath("/html/body/div/div/header/div[2]/div/a"));
        a1.Click();
        Console.WriteLine("--- Navigating to the Add Product page ---");
        Thread.Sleep(3000);

        // Take a screenshot after navigating to the Add Product page
        TakeScreenshot(driver, "2-AddProductPage.png");

        IWebElement fileInput = driver.FindElement(By.Name("file"));
        string filePath = @"C:\Users\U1\Desktop\pro1\p1\iphone.jpg";
        fileInput.SendKeys(filePath);
        Console.WriteLine("--- Photo has been uploaded ---");

        // Take a screenshot after uploading the photo
        TakeScreenshot(driver, "3-PhotoAdded.png");

        Console.WriteLine("--- Adding Title, Price, and Description ---");
        var title = driver.FindElement(By.Name("title"));
        title.SendKeys("iPhone 16 pro");

        var price = driver.FindElement(By.Name("price"));
        price.Click();
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].value = '';", price); // clear the field before adding a value
        price.SendKeys("799.9");

        var description = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div/form/div[2]/div[2]/input"));
        description.SendKeys("Dark blue pro - 256 GB - with Airpods");
        Thread.Sleep(3000);

        // Take a screenshot after filling in the field
        TakeScreenshot(driver, "4-PageFilled.png");

        var submit = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div/form/button"));
        submit.Click();

        Thread.Sleep(5000);

        // Take a screenshot after adding a new product
        TakeScreenshot(driver, "5-NewProduct.png");

        Console.WriteLine("--- Product has been added successfully ---");
    }

     static void EditProductTest(IWebDriver driver, WebDriverWait wait)
    {
        Thread.Sleep(3000);
        Console.WriteLine("--- Running Edit Product Test ---");
        var EditButton = driver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div[2]/div[2]/div[4]/div[2]/button[1]"));
        EditButton.Click();
        Console.WriteLine("--- Navigating to the Edit Product page ---");
        Thread.Sleep(3000);

        // Take a screenshot after navigating to the Edit Product page
        TakeScreenshot(driver, "6-EditProductPage.png");
         IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        Console.WriteLine("--- Editing Title, Price, and Description ---");
        var title = driver.FindElement(By.Name("title"));
        title.Click();
        js.ExecuteScript("arguments[0].value = '';", title); 
        title.SendKeys("iPhone 14 pro");

        var price = driver.FindElement(By.Name("price"));
        price.Click();
        js.ExecuteScript("arguments[0].value = '';", price);
        price.SendKeys("650");

        var description = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div/form/div[2]/div[2]/input"));
        description.Click();
        js.ExecuteScript("arguments[0].value = '';", description);
        description.SendKeys("Black - 128 GB - with Magsafe charger");
        Thread.Sleep(3000);

        // Take a screenshot after filling in the field
        TakeScreenshot(driver, "7-PageEdited.png");

        var submit = driver.FindElement(By.XPath("/html/body/div[1]/div/main/div/form/button"));
        submit.Click();

        Thread.Sleep(5000);

        // Take a screenshot after adding a new product
        TakeScreenshot(driver, "8-EditedProduct.png");

        Console.WriteLine("--- Product has been Edited successfully ---");
    }

    static void DeleteProductTest(IWebDriver driver, WebDriverWait wait)
    {
        Thread.Sleep(3000);
        Console.WriteLine("--- Running Delete Product Test ---");
        var DeleteButton = driver.FindElement(By.XPath("/html/body/div/div/main/div/div/div/div[2]/div[2]/div[4]/div[2]/button[2]"));
        DeleteButton.Click();
        Console.WriteLine("--- Deleting the Product ---");
        Thread.Sleep(8000);
        TakeScreenshot(driver, "9-ProductDeleted.png");
        Console.WriteLine("--- Product has been Deleted successfully ---");
    }

     static void SearchProductTest(IWebDriver driver, WebDriverWait wait)
     {
        Console.WriteLine("--- Running Search Product Test ---");
        Thread.Sleep(5000);

            // Locate the search input field
            var searchInput = driver.FindElement(By.XPath("/html/body/div/div/header/div[2]/input"));
            Console.WriteLine("Search input field found.");

            // Enter a search keyword
            string searchKeyword = "iPhone";
            searchInput.SendKeys(searchKeyword);
            Console.WriteLine($"Entered search keyword: {searchKeyword}");
            TakeScreenshot(driver, "10-SearchKeyword.png");
            Thread.Sleep(5000);

            // counter to check the results
            int counter = 0;
                    string[] xpaths = {
            "/html/body/div/div/main/div/div/div/div[2]/div[1]/div[4]/div[2]/button[2]",
            "/html/body/div/div/main/div/div/div/div[2]/div[2]/div[4]/div[2]/button[2]",
            "/html/body/div/div/main/div/div/div/div[2]/div[3]/div[4]/div[2]/button[2]",
            "/html/body/div/div/main/div/div/div/div[2]/div[4]/div[4]/div[2]/button[2]"
        };
        for (int i = 0; i < xpaths.Length; i++)
        {
            try
            {
                // Check if the element exists
                var element = driver.FindElement(By.XPath(xpaths[i]));
                if (element != null)
                {
                    counter++; // Increment the counter if the element is found
                    
                }
            }
            catch (NoSuchElementException)
            {
                break;
            }

        }
                Console.WriteLine($"--- Search result: {counter} ---");
                TakeScreenshot(driver, "11-Searchresult.png");

     }

    static void TakeScreenshot(IWebDriver driver, string fileName)
    {
        try
        {
            // Cast the driver to ITakesScreenshot
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

            // Capture the screenshot
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            // Save the screenshot to a file
            string screenshotPath = $@"C:\Users\U1\Desktop\pro1\LogsAndScreenshots\{fileName}";
            screenshot.SaveAsFile(screenshotPath);

            Console.WriteLine($"Screenshot saved: {screenshotPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to take screenshot: {ex.Message}");
        }
    }
}
