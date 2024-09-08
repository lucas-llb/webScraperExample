using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using WebScraper.Models;

namespace WebScraper.Drivers;

public class WebScrapper
{
    private IWebDriver _driver;

    public WebScrapper()
    {
        _driver = new ChromeDriver();
    }
    public IEnumerable<Item> GetData(string url)
    {
        var items = new List<Item>();

        _driver.Navigate().GoToUrl(url);

        var cardElement = _driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div[2]/div[1]")).FindElements(By.ClassName("thumbnail"));

        foreach (var element in cardElement)
        {
            var item = new Item();
            item.Title = element.FindElement(By.ClassName("title")).GetAttribute("title");
            item.Price = element.FindElement(By.ClassName("price")).Text;
            item.Description = element.FindElement(By.ClassName("description")).Text;

            items.Add(item);
        }

        return items;
    }

    public void CreateCsvFile(IEnumerable<Item> items)
    {
        var file = new StringBuilder();
        file.Append("Name;Description;Price");

        foreach (var item in items)
        {
            file.AppendLine($"{item.Title};{item.Description};{item.Price}");
        }

        // Check file in .\bin\Debug\net8.0
        File.WriteAllText("items.csv", file.ToString());
    }
}
