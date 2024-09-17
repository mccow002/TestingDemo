using System.Data.Common;
using Library.EndToEnd.Pages;
using Microsoft.Data.SqlClient;
using Microsoft.Playwright;
using Respawn;

namespace Library.EndToEnd.Features;

[TestClass]
public class CatalogueTests
{
    private Respawner _respawn;
    private DbConnection _dbConnection;

    [TestInitialize]
    public async Task Setup()
    {
        _dbConnection = new SqlConnection("Server=localhost,1434;Database=Library;User ID=sa;Password=Test123!;TrustServerCertificate=True;");
        await _dbConnection.OpenAsync();
        _respawn = await Respawner.CreateAsync(_dbConnection, new()
        {
            TablesToInclude = [
                "Reservation",
                "Checkout"
            ]
        });   
    }
    
    [TestCleanup]
    public async Task Teardown()
    {
        await _respawn.ResetAsync(_dbConnection);
        await _dbConnection.CloseAsync();
    }
    
    [TestMethod]
    public async Task ShouldAutomaticallyCheckoutNextReservation_WhenBookIsCheckedIn()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });
        var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();

        await page.GotoAsync("http://localhost:4200/");

        await page.GotoAsync("http://localhost:4200/catalogue");

        void page_Dialog1_EventHandler(object sender, IDialog dialog)
        {
            Console.WriteLine($"Dialog message: {dialog.Message}");
            dialog.AcceptAsync("1234");
            page.Dialog -= page_Dialog1_EventHandler;
        }

        page.Dialog += page_Dialog1_EventHandler;
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { NameString = "Checkout" }).Nth(0).ClickAsync();

        await page.GetByTestId("checkout-panel").WaitForAsync(new() { State = WaitForSelectorState.Visible });

        void page_Dialog2_EventHandler(object sender, IDialog dialog)
        {
            Console.WriteLine($"Dialog message: {dialog.Message}");
            dialog.AcceptAsync("5678");
            page.Dialog -= page_Dialog2_EventHandler;
        }

        page.Dialog += page_Dialog2_EventHandler;
        await page.GetByRole(AriaRole.Button, new() { NameString = "Reserve" }).Nth(0).ClickAsync();
        
        await page.GetByTestId("reservations-panel").WaitForAsync(new() { State = WaitForSelectorState.Visible });

        await page.GetByRole(AriaRole.Button, new() { NameString = "Checkin" }).Nth(0).ClickAsync();
    }
    
    [TestMethod]
    public async Task ShouldAutomaticallyCheckoutNextReservation_WhenBookIsCheckedIn_UsePageObject()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });
        var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();

        await page.GotoAsync("http://localhost:4200/");
        await page.GotoAsync("http://localhost:4200/catalogue");
        
        var cataloguePage = new CataloguePage(page);
        await cataloguePage.CheckoutBook("1234");
        await cataloguePage.CheckoutPanel.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        await cataloguePage.ReserveBook("5678");
        await cataloguePage.ReservationsPanel.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        await cataloguePage.CheckinBook();
    }
}