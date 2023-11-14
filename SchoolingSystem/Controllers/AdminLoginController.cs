using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolingSystem.Dtos;
using SchoolingSystem.Models;

namespace SchoolingSystem.Controllers
{
    [Route("api/[controller]")]
    public class AdminLoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AdminLoginController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

            signInManager.SignOutAsync();
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(Login login)
        {
            var user = await signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent: false, false);
            if (user.Succeeded)
                return Ok("User Successfully login");

            return NotFound("User not found");
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register(Login login)
        {
            
            var user = new IdentityUser { UserName = login.UserName, Email = login.UserName };
            var result = await userManager.CreateAsync(user, login.Password);

            if (result.Succeeded) 
                return Ok("Account Created Successfully");

            return BadRequest("Something went wrong");
        }

    }
}
