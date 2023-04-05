using System.Security.Claims;
using System.Text;
using System.Text.Json;
using API.DTOs;
using API.Infrastructure;
using API.Services;
using Entities;
using Entities.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using static API.Services.Security;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly LocalEmailSender _emailSender;
        public AccountController(ITokenService tokenService, UserManager<User> userManager,
                SignInManager<User> signInManager, LocalEmailSender emailSender)
        {
            _emailSender = emailSender;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid email");

            if (!user.EmailConfirmed) return Unauthorized("Email not confirmed");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Problem with login");

            return await CreateUserObject(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.NormalizedEmail == registerDto.Email.ToUpper()))
            {

                ModelState.AddModelError("email", "This email is taken. Try to remeber password option");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.NormalizedUserName == registerDto.UserName.ToUpper()))
            {
                ModelState.AddModelError("UserName", "This username is taken");
                return ValidationProblem();
            }

            var user = new User
            {
                UserName = registerDto.UserName.ToLower(),
                FirstName = registerDto.FirstName.ToLower(),
                LastName = registerDto.LastName.ToLower(),
                Email = registerDto.Email.ToLower()
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

           // var origin = Request.Headers["origin"];
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            
            var callback = Url.Action(nameof(VerifyEmail), "Account", new {token=token, email=user.Email}, Request.Scheme);
            var verifyUrl = $"{callback}/account/verifyEmail?token={token}&email={user.Email}";
            var message = $"<p>Kliknij poniższy link aby potwierdzić rejestrację konta</p><a href='{verifyUrl}'>Potwierdź email</a>";

            await _emailSender.SendEmailAsync(user.Email, "Please verify email", message);

            return Ok(JsonSerializer.Serialize("Register Successfully. Chek your email inbox and confirm to finish"));
            //return await CreateUserObject(user);
        }

        [AllowAnonymous]
        [HttpPost("verifyEmail")]
        public async Task<ActionResult> VerifyEmail(EmailDto email)
        {

            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            var user = await _userManager.FindByEmailAsync(email.Email);
            if (user == null || email.Token == null) return Unauthorized();

            var decodedTokenBytes = WebEncoders.Base64UrlDecode(email.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded) return BadRequest("Could not verify email address");

            return Ok(JsonSerializer.Serialize("Email confirmed - you can login now"));
        }

        [AllowAnonymous]
        [HttpPost("resendEmailConfirmationLink")]
        public async Task<IActionResult> ResendEmailConfirmationLink(EmailDto email)
        {

            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            var user = await _userManager.FindByEmailAsync(email.Email);
            if (user == null) return Unauthorized();

           // var origin = Request.Headers["origin"];
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            //var verifyUrl = $"{origin}/account/verifyEmail?token={token}&email={user.Email}";
            var callback = Url.Action(nameof(VerifyEmail), "Account", new {token=token, email=user.Email}, Request.Scheme);

            var message = $"<p>Kliknij poniższy link aby potwierdzić rejestrację konta</p><a href='{callback}'>Potwierdź email</a>";

            await _emailSender.SendEmailAsync(user.Email, "Please verify email", message);
            return Ok(JsonSerializer.Serialize("Resended token again"));
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(EmailDto email)
        {

            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            var user = await _userManager.FindByEmailAsync(email.Email);
            if (user == null)
                return Unauthorized();

            // var origin = Request.Headers["Host"];
            // Console.WriteLine(origin);
            var tokenToSend = await _userManager.GeneratePasswordResetTokenAsync(user);
            tokenToSend = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenToSend));

            //var verifyUrl = $"{origin}/account/ResetPassword?token={token}&email={user.Email}";
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token = tokenToSend, email = user.Email }, Request.Scheme);
            // response url to app password change mode
            var message = $"<p>Kliknij poniższy link do resetu hasła: </p><a href='{callback}'>resetuj hasło</a>";

            await _emailSender.SendEmailAsync(user.Email, "WMService password reset", message);

            return Ok(JsonSerializer.Serialize("Reset password token was send to your email- check your inbox"));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return BadRequest();
            }

            return Ok(new  { Token = token, Email = email }); // Zwróć url strony do resetu hasła
        }
        
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null) return Unauthorized();

            var decodedTokenBytes = WebEncoders.Base64UrlDecode(resetPassword.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);


            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPassword.Password);

            if (!result.Succeeded) return BadRequest("Could not reset your password");

            return Ok(JsonSerializer.Serialize("Your password has been reset"));
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

             var user = await _userManager.GetUserAsync(User);

            if (user == null) return Unauthorized();

            var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Description);
            }

            return Ok(JsonSerializer.Serialize("Your password has been reset"));
 }

        [Authorize]
        //[ValidateAntiForgeryToken]
        [HttpGet("GetUserProfile")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) return Unauthorized();

            UserProfileDto userDto = new UserProfileDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return userDto;
        }

        private async Task<UserDto> CreateUserObject(User user)
        {
            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }
    }
}
