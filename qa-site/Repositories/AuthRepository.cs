using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QASite.Models
{
    public class AuthRepository : IDisposable
    {
        private QAContext _ctx;

        private UserManager<User> _userManager;

        public AuthRepository()
        {
            _ctx = new QAContext();
            _userManager = new UserManager<User>(new UserStore<User>(_ctx));
        }

        public IdentityResult RegisterUser(UserRegisterDTO userModel)
        {
            User user = new User
            {
                UserName = userModel.Email,
                Email = userModel.Email             
            };

            var result = _userManager.Create(user, userModel.Password);

            return result;
        }

        public User FindUser(string userName, string password)
        {
            User user = _userManager.Find(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }

    }
}