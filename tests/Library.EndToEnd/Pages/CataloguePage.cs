using Microsoft.Playwright;

namespace Library.EndToEnd.Pages;

public class CataloguePage
{
    private readonly IPage _page;
    private readonly ILocator _checkinButton;
    private readonly ILocator _reserveButton;
    private readonly ILocator _checkoutButton;

    public CataloguePage(IPage page)
    {
        _page = page;
        
        _checkinButton = _page.GetByRole(AriaRole.Button, new() { NameString = "Checkin" });
        _reserveButton = _page.GetByRole(AriaRole.Button, new() { NameString = "Reserve" });
        _checkoutButton = _page.GetByRole(AriaRole.Button, new() { NameString = "Checkout" });
    }
    
    public ILocator CheckoutPanel => _page.GetByTestId("checkout-panel");
    public ILocator ReservationsPanel => _page.GetByTestId("reservations-panel");
    
    public async Task CheckinBook()
    {
        await _checkinButton.Nth(0).ClickAsync();
    }
    
    public async Task ReserveBook(string cardNumber)
    {
        void ReserveDialog_EventHandler(object sender, IDialog dialog)
        {
            Console.WriteLine($"Dialog message: {dialog.Message}");
            dialog.AcceptAsync(cardNumber);
            _page.Dialog -= ReserveDialog_EventHandler;
        }

        _page.Dialog += ReserveDialog_EventHandler;
        
        await _reserveButton.Nth(0).ClickAsync();
    }
    
    public async Task CheckoutBook(string cardNumber)
    {
        void CheckoutDialog_EventHandler(object sender, IDialog dialog)
        {
            Console.WriteLine($"Dialog message: {dialog.Message}");
            dialog.AcceptAsync(cardNumber);
            _page.Dialog -= CheckoutDialog_EventHandler;
        }

        _page.Dialog += CheckoutDialog_EventHandler;
        
        await _checkoutButton.Nth(0).ClickAsync();
    }
}