using Microsoft.Playwright;
using WebScraper.Models;
namespace WebScraper.Drivers;

public class WebScrapperPlayWright
{
    public async Task<IEnumerable<Item>> GetDataAsync(string url)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync();

        var page = await (await browser.NewContextAsync(new() { IgnoreHTTPSErrors = true })).NewPageAsync();

        page.Console += (sender, e) =>
        {
            Console.WriteLine($"Console message: {e.Text}");
        };
        await page.GotoAsync(url);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        var items = new List<Item>();

        var elements = await page.QuerySelectorAllAsync(".card.thumbnail");

        foreach (var element in elements)
        {
            var item = new Item();
            item.Title = await (await element.QuerySelectorAsync(".title")).GetAttributeAsync("title");
            item.Price = await (await element.QuerySelectorAsync(".price")).InnerTextAsync();
            item.Description = await (await element.QuerySelectorAsync(".description")).InnerTextAsync();

            items.Add(item);
        }

        await browser.CloseAsync();

        return items;
    }
}
