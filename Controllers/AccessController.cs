using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AuthProject.Models;
using AuthProject.Data;

namespace AuthProject.Controllers
{
    

    public class AccessController : Controller
    {  private readonly ApplicationDbContext _context;
        public AccessController(ApplicationDbContext context)
        {
            _context = context;
           
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
   Boolean login=false;
            // _context.Users.
          User res = _context.Users.FirstOrDefault(u => u.Email == modelLogin.Email);
         
           if(res ==null){
             Console.WriteLine("null");
             
            }else {
             if( res.Password == modelLogin.PassWord){
                  login=true;
                 }
             }

            if (login)
            { 
                List<Claim> claims = new List<Claim>() { 
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties","Example Role")
                
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme );

                AuthenticationProperties properties = new AuthenticationProperties() { 
                
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
            
                return RedirectToAction("Index", "Home");
            }



            ViewData["ValidateMessage"] = "email or password not found";
            return View();
        }
    }
}
