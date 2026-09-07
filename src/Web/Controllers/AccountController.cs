using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ignite.Web.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password, string? returnUrl)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return LocalRedirect(string.IsNullOrWhiteSpace(returnUrl) ? "/admin" : returnUrl);
        }

        return Redirect("/admin/login?error=1");
    }

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
}
