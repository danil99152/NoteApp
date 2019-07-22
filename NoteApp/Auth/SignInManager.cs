using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using NoteApp.Models.Models;

namespace NoteApp.Auth
{
    public class SignInManager : SignInManager<User, long>
    {
        public SignInManager(UserManager<User, long> userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = UserManager.FindByName(userName);
            if (user != null && !user.IsEnabled)
            {
                return Task.FromResult(SignInStatus.LockedOut);
            }
            return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }
    }
}