using Microsoft.AspNetCore.Mvc.RazorPages;

/* NEW FILE - Products code-behind (minimal) */
public class ProductsIndexModel : PageModel
{
    public void OnGet()
    {
        // Minimal: later use DI to get product list from your Repositories or DbContext.
    }
}
