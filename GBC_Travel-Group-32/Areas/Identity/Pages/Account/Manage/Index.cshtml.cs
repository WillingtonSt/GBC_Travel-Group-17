// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GBC_Travel_Group_32.Data;
using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GBC_Travel_Group_32.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
      

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager
          )

        {
            _userManager = userManager;
            _signInManager = signInManager;
       
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

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


            [Display(Name = "Username")]
            public string Username { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }


            [Display(Name = "Last Name")]
            public string LastName { get; set; }



            [Display(Name = "Frequent Flyer Number")]
            public string FrequentFlyerNumber { get; set; }


            [Display(Name = "Hotel Loyalty ID")]
            public string HotelLoyaltyID { get; set; }


            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var profilePicture = user.ProfilePicture;
            var frequentFlyerNum = user.FrequentFlyerNumber;
            var hotelLoyaltyID = user.HotelLoyaltyID;
            

            Username = userName;

        

            Input = new InputModel
            {
                Username = userName,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicture = profilePicture,
                FrequentFlyerNumber = frequentFlyerNum,
                HotelLoyaltyID = hotelLoyaltyID,
                PhoneNumber = phoneNumber
                
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


            if(Input.Username != user.UserName) {

                var userNameExists = await _userManager.FindByNameAsync(user.UserName);
                if (userNameExists != null) {
                    StatusMessage = "Error: username not available. Please enter a new username";
                    return RedirectToPage();
                }

            }

            var setUserName = await _userManager.SetUserNameAsync(user, Input.Username);
            if(!setUserName.Succeeded) {
                StatusMessage = "Error: Unexpected error when attempting to update username";
                return RedirectToPage();
            } else {
                user.UserName = Input.Username;
                await _userManager.UpdateAsync(user);
            }
            

            var firstName = user.FirstName;
            if (Input.FirstName != firstName) {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }


            var lastName = user.LastName;
            if (Input.LastName != lastName) {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }


            var frequentFlyerNum = user.FrequentFlyerNumber;
            if(Input.FrequentFlyerNumber != frequentFlyerNum) {
                user.FrequentFlyerNumber = Input.FrequentFlyerNumber;
                await _userManager.UpdateAsync(user);
            }

            var hotelLoyaltyID = user.HotelLoyaltyID;
            if(Input.HotelLoyaltyID != hotelLoyaltyID) {
                user.HotelLoyaltyID= Input.HotelLoyaltyID;
                await _userManager.UpdateAsync(user);
            }

            if (Request.Form.Files.Count > 0) {

                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream()) {
                    file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

          
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
