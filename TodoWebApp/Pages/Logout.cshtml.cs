using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TodoWebApp.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            // Overwrite the cookie with an expired one to ensure deletion
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
                Path = "/",
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("jwt", "", cookieOptions);

            return RedirectToPage("/Login");
        }
    }
}