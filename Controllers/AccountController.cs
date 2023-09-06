using AlphaBeratung.Data;
using AlphaBeratung.Models;
using AlphaBeratung.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace AlphaBeratung.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        public AccountController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(Users user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //TempData["Message"] = "Please Fill in all required inputs";
                    return View(user);
                }
                else
                {

                    var found = context.Users.FirstOrDefault(x => x.Email == user.Email);
                    if (found != null)
                    {
                        TempData["found"] = "This Email aready exist, try a new one";
                        return View(user);
                    }


                    var new_user = new IdentityUser()
                    {
                        Email = user.Email,
                        UserName= user.Email
                    };

                    IdentityRole VRole = context.Roles.FirstOrDefault(a => a.Name == "Visitor");
                    
                    var result = await userManager.CreateAsync(new_user, user.Password);
                    if (result.Succeeded)
                    {
                        if (VRole != null)
                        {
                            await userManager.AddToRoleAsync(new_user, "Visitor");
                        }
                        return RedirectToAction("Index", "Clients");
                    }
                    else
                    {
                        TempData["Error"] = "There was an Error, please try again";
                        return View(user);
                    }
                }
            }
            catch
            {
                TempData["Message"] = "An Error Occured, Please try again later";
                return View();
            }
            
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = "Invalid Email or Password, Please try again.";
                    return View(user);
                }
                else
                {
                    // Ahmed Madher - This is Login code for user of type 
                    var result = await signInManager.PasswordSignInAsync(user.Email, user.Password,user.RememberMe,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Clients");
                    }

                    else
                    {
                        TempData["Message"] = "Invalid Email or Password, Please try again.";
                        //ModelState.AddModelError(string.Empty, "Invalid Email or Password, Please try again.");
                        return View(user);
                    }
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Invalid Email or Password,";
                return View(user);
            }


        }


        public IActionResult Reset_Password()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset_Password(Password_Recovery recover)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var user = await userManager.FindByEmailAsync(recover.Email);
                if (user == null)
                {
                    TempData["Error"]= "This Email was not found, please enter a valid email";
                    return View(recover);
                }
                string newPassword = GenerateRandomPassword();
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, newPassword);
                if (result.Succeeded)
                {

                    SendPassword(newPassword, recover.Email);

                    TempData["recover"] = "Your new password has been sent to Email, Please Check it ";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Error"] = "An Error occured, please try again later";
                }
                return View();
            }
            
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var randomString = new char[8];

            for (int i = 0; i < 5; i++)
            {
                randomString[i] = chars[random.Next(chars.Length)];
            }
            randomString[6] = '-';
            randomString[7] = '1';

            return new string(randomString);
        }

        public void SendPassword(string password,string email)
        {
            var anything = "System  : ";
            var content = $@"
                <style type=""text/css"">
                    body {{ font-family: Arial; font-size: 10pt; }}
                </style>
                <body>
                    <h4>{DateTime.Now:dddd, MMMM d, yyyy h:mm:ss tt}</h4>
                    <h3>  {anything} </h3>
                    <h3> كلمة السر الجديدة هي : {password}  </h3>
                    
                    
                    
                </body>
                ";

            var view = AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, MediaTypeNames.Text.Html);

            var fromSender = "ahmed_madher_new@hotmail.com"; //  ايميل المرسل
            var pass = "Ahmedmadher123"; //  كلمة السر لايميل المرسل
            using (var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromSender, pass)
            })

            using (MailMessage message = new MailMessage()
            {
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                Subject = "Password Recovery",
                SubjectEncoding = Encoding.UTF8,
            })
            {
                message.AlternateViews.Add(view);
                message.From = new MailAddress(fromSender);
                message.To.Add(new MailAddress("ahmedmadher101@gmail.com")); // ايميل المستقبل
                client.Send(message);
                
            }
            //=========================================
            


        }
    }

    }

