using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using OpenQA.Selenium;
using System.Data;
using WebScraper.Models;

namespace WebScraper.Drivers;

public class WebScrapper : Web
{
    public DataTable GetData(string url)
    {
        if(driver == null)
        {
            StartBrowser();
        }

        var items = new List<Item>();

        Navigate(url);

        var elements = GetValue(TypeElement.Xpath, "/html/body/div[1]/div[3]/div/div[2]/div[1]").element.FindElements(By.ClassName("thumbnail"));

        foreach(var element in elements)
        {
            var item = new Item();
            item.Title = element.FindElement(By.ClassName("title")).GetAttribute("title");
            item.Price = element.FindElement(By.ClassName("price")).Text;
            item.Description = element.FindElement(By.ClassName("description")).Text;

            items.Add(item);
        }

        return Base.ConvertTo(items);
    }

    public void CreateExcelFile(DataTable data, string sheetName)
    {
        var dataTableParams = new ParamsDataTable("Data", "C:/", new List<DataTables> { new DataTables(sheetName, data) });

        Base.GenerateExcel(dataTableParams);
    }
}
