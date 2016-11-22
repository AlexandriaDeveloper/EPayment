using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using AlexFacultyOfMed.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SendGrid;

namespace AlexFacultyOfMed
{
    public class EmailFormModel
    {
        [Required]
        [Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required]
        [Display(Name = "Your email")]
        [EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string Message { get; set; }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return ConfigureSendMailAsync(message);

            // return SendGridAsync(message);
            // return ConfigureSendMailHypirdAsync(message);
            // Plug in your email service here to send an email.
        }

        private static Task ConfigureSendMailHypirdAsync(IdentityMessage message)
        {
            var email = new MailMessage
            {
                From = new MailAddress("EPayment@Facultyofmed.com", "وحدة الدفع الألكترونى كلية الطب "),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };
            email.To.Add(message.Destination);
            try
            {
                var client = new SmtpClient();
                client.EnableSsl = true;
                return client.SendMailAsync(email);
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        private static Task ConfigureSendMailAsync(IdentityMessage message)
        {
            var email = new MailMessage
            {
                From = new MailAddress("mcit.alex85@gmail.com", "وحدة الدفع الألكترونى كلية الطب "),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };
            email.To.Add(message.Destination);
            try
            {
                var client = new SmtpClient();
                client.EnableSsl = true;
                return client.SendMailAsync(email);
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        public async Task SendGridAsync(IdentityMessage message)
        {
            await ConfigSendGridAsync(message);
        }

        private async Task ConfigSendGridAsync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new MailAddress("EPayment@Facultyofmed.com", "وحدة الدفع الألكترونى كلية الطب ");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["mailAccount"],
                ConfigurationManager.AppSettings["mailPassword"]);

            var transportweb = new Web(credentials);

            if (transportweb != null)
                try
                {
                    await transportweb.DeliverAsync(myMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            else
                await Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<AspNetUser>
    {
        public ApplicationUserManager(IUserStore<AspNetUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<AspNetUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AspNetUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(30);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<AspNetUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AspNetUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AspNetUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            return manager;
        }


        public async Task<AspNetUser> GetUserByNationalIdAsync(string NationalId)
        {
            var context = ApplicationDbContext.Create();
            var user = await context.Users.FirstAsync(x => x.NationalId == NationalId);
            return user;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<AspNetUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AspNetUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class ApplictaionRoleManager : RoleManager<IdentityRole>
    {
        public ApplictaionRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }

        public static ApplictaionRoleManager Create(IdentityFactoryOptions<ApplictaionRoleManager> options,
            IOwinContext context)
        {
            var Store = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());
            return new ApplictaionRoleManager(Store);
        }
    }
}