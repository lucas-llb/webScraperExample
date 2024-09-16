using WebScraper.Drivers;

var webScraper = new WebScrapper();
var playwright = new WebScrapperPlayWright();

var laptopsPlaywright = await playwright.GetDataAsync("https://webscraper.io/test-sites/e-commerce/static/computers/laptops");
var laptops = webScraper.GetData("https://webscraper.io/test-sites/e-commerce/static/computers/laptops");

webScraper.CreateCsvFile(laptops);
