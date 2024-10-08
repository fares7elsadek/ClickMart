﻿using ClickMart.DataAccess.Data;
using ClickMart.Models.Models;
using ClickMart.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;
        public DbInitializer(UserManager<User> userManager
            , RoleManager<IdentityRole> roleManager
            , AppDbContext db
            , IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _configuration = configuration;
        }

        public void Initialize()
        {
            // Migrations if not applied
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                 _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                 _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

                string AdminUserName = _configuration["AdminInfo:UserName"];
                string AdminFirstName = _configuration["AdminInfo:FirstName"];
                string AdminLastName = _configuration["AdminInfo:LastName"];
                string AdminPhoneNumber = _configuration["AdminInfo:PhoneNumber"];
                string AdminPassword = _configuration["AdminInfo:Password"];
                string AdminEmail = _configuration["AdminInfo:Email"];

                _userManager.CreateAsync(new User
                {
                    UserName = AdminUserName,
                    Email= AdminEmail,
                    FirstName= AdminFirstName,
                    LastName= AdminLastName,
                    PhoneNumber= AdminPhoneNumber,
                    EmailConfirmed = true,

                },AdminPassword).GetAwaiter().GetResult();

                User user = _db.Users.FirstOrDefault( u => u.Email == AdminEmail);
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }

            var counries = _db.Countries.ToList();
            if(counries.Count == 0)
            {
                List<Country> _countryList = new List<Country>()
                {
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Afghanistan", ISO = "AF" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Albania", ISO = "AL" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Algeria", ISO = "DZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "American Samoa", ISO = "AS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Andorra", ISO = "AD" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Angola", ISO = "AO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Anguilla", ISO = "AI" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Antarctica", ISO = "AQ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Antigua and Barbuda", ISO = "AG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Argentina", ISO = "AR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Armenia", ISO = "AM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Aruba", ISO = "AW" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Australia", ISO = "AU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Austria", ISO = "AT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Azerbaijan", ISO = "AZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Bahamas", ISO = "BS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Bahrain", ISO = "BH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Bangladesh", ISO = "BD" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Barbados", ISO = "BB" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Belarus", ISO = "BY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Belgium", ISO = "BE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Belize", ISO = "BZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Benin", ISO = "BJ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Bermuda", ISO = "BM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Bhutan", ISO = "BT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Bosnia and Herzegovina", ISO = "BA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Botswana", ISO = "BW" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Bouvet Island", ISO = "BV" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Brazil", ISO = "BR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "British Indian Ocean Territory", ISO = "IO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Brunei Darussalam", ISO = "BN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Bulgaria", ISO = "BG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Burkina Faso", ISO = "BF" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Burundi", ISO = "BI" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Cambodia", ISO = "KH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Cameroon", ISO = "CM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Canada", ISO = "CA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Cape Verde", ISO = "CV" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Cayman Islands", ISO = "KY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Central African Republic", ISO = "CF" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Chad", ISO = "TD" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Chile", ISO = "CL" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "China", ISO = "CN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Christmas Island", ISO = "CX" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Cocos (Keeling) Islands", ISO = "CC" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Colombia", ISO = "CO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Comoros", ISO = "KM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Congo", ISO = "CG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Cook Islands", ISO = "CK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Costa Rica", ISO = "CR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Croatia", ISO = "HR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Cuba", ISO = "CU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Cyprus", ISO = "CY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Czech Republic", ISO = "CZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Denmark", ISO = "DK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Djibouti", ISO = "DJ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Dominica", ISO = "DM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Dominican Republic", ISO = "DO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Ecuador", ISO = "EC" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Egypt", ISO = "EG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "El Salvador", ISO = "SV" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Equatorial Guinea", ISO = "GQ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Eritrea", ISO = "ER" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Estonia", ISO = "EE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Ethiopia", ISO = "ET" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Falkland Islands (Malvinas)", ISO = "FK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Faroe Islands", ISO = "FO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Fiji", ISO = "FJ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Finland", ISO = "FI" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "France", ISO = "FR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "French Guiana", ISO = "GF" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "French Polynesia", ISO = "PF" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "French Southern Territories", ISO = "TF" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Gabon", ISO = "GA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Gambia", ISO = "GM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Georgia", ISO = "GE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Germany", ISO = "DE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Ghana", ISO = "GH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Gibraltar", ISO = "GI" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Greece", ISO = "GR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Greenland", ISO = "GL" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Grenada", ISO = "GD" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Guadeloupe", ISO = "GP" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Guam", ISO = "GU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Guatemala", ISO = "GT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Guernsey", ISO = "GG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Guinea", ISO = "GN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Guinea-Bissau", ISO = "GW" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Guyana", ISO = "GY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Haiti", ISO = "HT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Heard Island and McDonald Islands", ISO = "HM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Holy See (Vatican City State)", ISO = "VA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Honduras", ISO = "HN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Hong Kong", ISO = "HK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Hungary", ISO = "HU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Iceland", ISO = "IS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "India", ISO = "IN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Indonesia", ISO = "ID" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Iran", ISO = "IR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Iraq", ISO = "IQ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Ireland", ISO = "IE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Isle of Man", ISO = "IM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Italy", ISO = "IT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Jamaica", ISO = "JM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Japan", ISO = "JP" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Jersey", ISO = "JE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Jordan", ISO = "JO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Kazakhstan", ISO = "KZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Kenya", ISO = "KE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Kiribati", ISO = "KI" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Kuwait", ISO = "KW" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Kyrgyzstan", ISO = "KG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Lao Peoples Democratic Republic", ISO = "LA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Latvia", ISO = "LV" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Lebanon", ISO = "LB" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Lesotho", ISO = "LS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Liberia", ISO = "LR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Libya", ISO = "LY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Liechtenstein", ISO = "LI" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Lithuania", ISO = "LT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Luxembourg", ISO = "LU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Macao", ISO = "MO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Macedonia", ISO = "MK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Madagascar", ISO = "MG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Malawi", ISO = "MW" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Malaysia", ISO = "MY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Maldives", ISO = "MV" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Mali", ISO = "ML" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Malta", ISO = "MT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Marshall Islands", ISO = "MH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Martinique", ISO = "MQ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Mauritania", ISO = "MR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Mauritius", ISO = "MU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Mayotte", ISO = "YT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Mexico", ISO = "MX" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Micronesia", ISO = "FM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Moldova", ISO = "MD" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Monaco", ISO = "MC" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Mongolia", ISO = "MN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Montenegro", ISO = "ME" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Montserrat", ISO = "MS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Morocco", ISO = "MA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Mozambique", ISO = "MZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Myanmar", ISO = "MM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Namibia", ISO = "NA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Nauru", ISO = "NR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Nepal", ISO = "NP" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Netherlands", ISO = "NL" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "New Caledonia", ISO = "NC" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "New Zealand", ISO = "NZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Nicaragua", ISO = "NI" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Niger", ISO = "NE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Nigeria", ISO = "NG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Niue", ISO = "NU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Norfolk Island", ISO = "NF" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Northern Mariana Islands", ISO = "MP" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Norway", ISO = "NO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Oman", ISO = "OM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Pakistan", ISO = "PK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Palau", ISO = "PW" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Palestinian Territory", ISO = "PS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Panama", ISO = "PA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Papua New Guinea", ISO = "PG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Paraguay", ISO = "PY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Peru", ISO = "PE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Philippines", ISO = "PH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Pitcairn", ISO = "PN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Poland", ISO = "PL" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Portugal", ISO = "PT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Puerto Rico", ISO = "PR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Qatar", ISO = "QA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Reunion", ISO = "RE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Romania", ISO = "RO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Russian Federation", ISO = "RU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Rwanda", ISO = "RW" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Saint Barthelemy", ISO = "BL" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Saint Helena", ISO = "SH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Saint Kitts and Nevis", ISO = "KN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Saint Lucia", ISO = "LC" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Saint Martin", ISO = "MF" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Saint Pierre and Miquelon", ISO = "PM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Saint Vincent and the Grenadines", ISO = "VC" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Samoa", ISO = "WS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "San Marino", ISO = "SM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Sao Tome and Principe", ISO = "ST" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Saudi Arabia", ISO = "SA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Senegal", ISO = "SN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Serbia", ISO = "RS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Seychelles", ISO = "SC" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Sierra Leone", ISO = "SL" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Singapore", ISO = "SG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Slovakia", ISO = "SK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Slovenia", ISO = "SI" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Solomon Islands", ISO = "SB" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Somalia", ISO = "SO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "South Africa", ISO = "ZA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "South Georgia", ISO = "GS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "South Sudan", ISO = "SS" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Spain", ISO = "ES" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Sri Lanka", ISO = "LK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Sudan", ISO = "SD" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Suriname", ISO = "SR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Svalbard and Jan Mayen", ISO = "SJ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Swaziland", ISO = "SZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Sweden", ISO = "SE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Switzerland", ISO = "CH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Syrian Arab Republic", ISO = "SY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Taiwan", ISO = "TW" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Tajikistan", ISO = "TJ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Tanzania", ISO = "TZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Thailand", ISO = "TH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Timor-Leste", ISO = "TL" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Togo", ISO = "TG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Tokelau", ISO = "TK" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Tonga", ISO = "TO" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Trinidad and Tobago", ISO = "TT" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Tunisia", ISO = "TN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Turkey", ISO = "TR" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Turkmenistan", ISO = "TM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Turks and Caicos Islands", ISO = "TC" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Tuvalu", ISO = "TV" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Uganda", ISO = "UG" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Ukraine", ISO = "UA" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "United Arab Emirates", ISO = "AE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "United Kingdom", ISO = "GB" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "United States", ISO = "US" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Uruguay", ISO = "UY" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Uzbekistan", ISO = "UZ" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Vanuatu", ISO = "VU" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Venezuela", ISO = "VE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Viet Nam", ISO = "VN" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Western Sahara", ISO = "EH" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Yemen", ISO = "YE" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Zambia", ISO = "ZM" },
                    new Country { Id = Guid.NewGuid().ToString(), Name = "Zimbabwe", ISO = "ZW" }
                };
                _db.Countries.AddRange(_countryList);
                _db.SaveChanges();
            }

            var shippingMethods = _db.ShippingMethods.ToList();
            if(shippingMethods.Count ==0)
            {
                List<ShippingMethod> _shippingMethods = new List<ShippingMethod>()
                {
                    new ShippingMethod
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Standard Shipping",
                        Price = 5.99M,
                        Description = "Delivery within 5-7 business days.",
                        Default = true
                    },
                    new ShippingMethod
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Express Shipping",
                        Price = 12.99M,
                        Description = "Faster delivery within 2-3 business days."
                    },
                    new ShippingMethod
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Overnight Shipping",
                        Price = 24.99M,
                        Description = "Next-day delivery for urgent orders."
                    }
                };
                _db.ShippingMethods.AddRange(_shippingMethods);
                _db.SaveChanges();
            }

            

            

            

            return;
        }
    }
}
