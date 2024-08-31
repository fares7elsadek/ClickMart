// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Loader;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ClickMart.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IWebHostEnvironment webHostEnvironment,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string avatar {  get; set; }

       
        

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "First Name")]
            [MaxLength(120)]
            public string FirstName { get; set;}

            [Required]
            [Display(Name = "Last Name")]
            [MaxLength(120)]
            public string LastName { get; set; }
        }

        

        private async Task LoadAsync(User user)
        {
            var userData = await _userManager.GetUserAsync(User);
            

            Username = userData.UserName;
            FirstName = userData.FirstName;
            LastName = userData.LastName;
            PhoneNumber = userData.PhoneNumber;
            Email = userData.Email;
            avatar = userData.avatar;

           

            Input = new InputModel
            {
                PhoneNumber = userData.PhoneNumber,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            bool updateNeeded = false;

            // FirstName
            if (user.FirstName != Input.FirstName)
            {
                user.FirstName = Input.FirstName;
                updateNeeded = true;
            }

            // LastName
            if (user.LastName != Input.LastName)
            {
                user.LastName = Input.LastName;
                updateNeeded = true;
            }

            // Avatar upload
            if (Request.Form.Files.Count > 0)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var file = Request.Form.Files.FirstOrDefault();
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string avatarPath = Path.Combine(wwwRootPath, "Images", "User", "avatar");

                // Create directory if it doesn't exist
                if (!Directory.Exists(avatarPath))
                {
                    Directory.CreateDirectory(avatarPath);
                }

                // Delete old avatar if exists
                if (!string.IsNullOrEmpty(user.avatar))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, user.avatar);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Save new avatar
                using (var fileStream = new FileStream(Path.Combine(avatarPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                user.avatar = Path.Combine("Images", "User", "avatar", fileName).Replace("\\", "/");
                updateNeeded = true;
            }

            

            // Update user data if necessary
            if (updateNeeded)
            {
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to update the user.";
                    return RedirectToPage();
                }
            }

            

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
