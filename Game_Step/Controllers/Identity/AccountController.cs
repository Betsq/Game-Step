using System.IO;
using System.Linq;
using Game_Step.IdentityViewModels;
using Game_Step.Models;
using Game_Step.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Game_Step.Controllers.IdentityControllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _db;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, ApplicationContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        [Authorize]
        public async Task<IActionResult> Partner() => View(await SetProfileViewModel());

        [Authorize]
        public async Task<IActionResult> Purchase()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var orders = _db.OrderNumbers
                .Include(item => item.Orders)
                .Where(item => item.Email == user.Email)
                .ToList();

            var model = new PurchaseViewModel()
            {
                Orders = orders,
                Avatar = user.Avatar,
                Name = user.Name
            };

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (UserIsAuthenticated())
                return RedirectToAction("Profile", "Account");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (UserIsAuthenticated())
                return RedirectToAction("Profile", "Account");

            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
            };

            //Add users
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);

            var emailService = new EmailService();

            await emailService.SendEmailAsync(model.Email, "Confirm Email",
                $"Confirm registration by clicking on the link:" +
                $" <a href='{callbackUrl}'>link</a>");

            return Content("To complete registration, check your email and" +
                           " follow the link provided in the letter");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null || UserIsAuthenticated())
                return View("Error");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (UserIsAuthenticated())
                return RedirectToAction("Profile", "Account");

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (UserIsAuthenticated())
                return RedirectToAction("Profile", "Account");

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null)
                return View(model);

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "You don't confirm your email");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email,
                model.Password, model.RememberMe, false);

            if (result.Succeeded)
                return RedirectToAction("Profile", "Account");


            ModelState.AddModelError("", "Invalid username and (or) password");
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //Delete authentication cookies
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            if (UserIsAuthenticated())
                return RedirectToAction("Profile", "Account");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (UserIsAuthenticated())
                return RedirectToAction("Profile", "Account");

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null && !await _userManager.IsEmailConfirmedAsync(user))
                return Content("To reset your password, follow the link in the letter" +
                               " sent to your email.");

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

            var emailService = new EmailService();

            await emailService.SendEmailAsync(model.Email, "Reset Password",
                $"To reset your password follow the link:" +
                $" <a href='{callbackUrl}'>Reset Password</a>");

            return Content("To reset your password, follow the link in the letter" +
                           " sent to your email.");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (UserIsAuthenticated())
                return RedirectToAction("Profile", "Account");

            return code != null ? View() : View("Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (UserIsAuthenticated())
                return RedirectToAction("Profile", "Account");

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return View("ResetPasswordConfirmation");

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
                return View("ResetPasswordConfirmation");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View("ResetPasswordConfirmation");
        }

        [Authorize]
        public async Task<IActionResult> Profile() => View(await SetProfileViewModel());

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar(ProfileViewModel model)
        {
            if (model.AvatarFormFile == null)
                return RedirectToAction("Profile", "Account");

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(model.AvatarFormFile.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)model.AvatarFormFile.Length);
            }

            var user = await CurrentUserAsync();

            user.Avatar = imageData;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return RedirectToAction("Profile", "Account");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateNickname(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Profile", await SetProfileViewModel(model));


            if (string.IsNullOrEmpty(model.Name))
                return View("Profile", await SetProfileViewModel(model));

            var user = await CurrentUserAsync();

            user.Name = model.Name;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return View("Profile");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ProfileViewModel model)
        {
            TempData["UnSuccessChangePassword"] = "Пароль не был изменен:(";

            if (!ModelState.IsValid)
                return View("Profile", await SetProfileViewModel(model));

            if (string.IsNullOrEmpty(model.OldPassword))
                return View("Profile", await SetProfileViewModel(model));

            var user = await CurrentUserAsync();

            var result = await _userManager
                .ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessChangePassword"] = "Пароль успешно изменен";
                TempData["UnSuccessChangePassword"] = null;
                return RedirectToAction("Profile");
            }
                

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View("Profile", await SetProfileViewModel(model));
        }

        private bool UserIsAuthenticated() => User.Identity.IsAuthenticated;
        public Task<User> CurrentUserAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var user = _userManager.FindByIdAsync(userId);

            return user;
        }
        public async Task<ProfileViewModel> SetProfileViewModel(ProfileViewModel pvm = null)
        {
            var user = await CurrentUserAsync();

            if (pvm != null)
            {
                pvm.Avatar = user.Avatar;
                pvm.Name = user.Name;
                pvm.Email = user.Email;
                return pvm;
            }

            var profile = new ProfileViewModel()
            {
                Name = user.Name,
                Avatar = user.Avatar,
                Email = user.Email,
            };
            return profile;
        }
    }
}
