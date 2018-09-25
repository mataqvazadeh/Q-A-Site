using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using stackoverflow;
using stackoverflow.Models;
using stackoverflow.Helper;

namespace stackoverflow.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiController
    {
        private QAContext db = new QAContext();

        [Route("register"), HttpPost]
        [ResponseType(typeof(UserSignUpOrSignInDTO))]
        public IHttpActionResult RegisterUser(UserSignUpOrSignInDTO registerdUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if( registerdUser.ConfirmPassword == null || registerdUser.Password != registerdUser.ConfirmPassword)
            {
                return BadRequest("Password does not match the confirm password.");
            }

            User user = db.Users.Where(u => u.Email == registerdUser.Email).SingleOrDefault();

            if( user != null )
            {
                return BadRequest("Email Address Duplicated.");
            }

            user = new User();
            user.Email = registerdUser.Email;
            user.Password = PasswordEncryptor.ComputeHash(registerdUser.Password);
            user.RegisterDate = DateTime.Now;

            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return BadRequest("There was a problem. Please try again.");
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}