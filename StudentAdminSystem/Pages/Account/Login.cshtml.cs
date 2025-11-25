using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/* NEW FILE - Demo login code-behind (keeps session simple) */
public class AccountLoginModel : PageModel
{
    [BindProperty]
    public string Username { get; set; } = "";

    public IActionResult OnPost()
    {
        if (!string.IsNullOrWhiteSpace(Username))
        {
            // Minimal demo approach: set TempData, you can replace with real auth later
            TempData["Message"] = $"Logged in as {Username} (demo)";
            return RedirectToPage("/Index");
        }

        ModelState.AddModelError("", "Please enter a username.");
        return Page();
    }
}
