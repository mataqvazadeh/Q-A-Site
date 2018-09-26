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
        public IHttpActionResult RegisterUser(UserSignUpOrSignInDTO registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if( registrationData.ConfirmPassword == null || registrationData.Password != registrationData.ConfirmPassword)
            {
                return BadRequest("Password does not match the confirm password.");
            }

            User user = db.Users.Where(u => u.Email == registrationData.Email).SingleOrDefault();

            if( user != null )
            {
                return BadRequest("Email Address Duplicated.");
            }

            user = new User();
            user.Email = registrationData.Email;
            user.Password = PasswordEncryptor.ComputeHash(registrationData.Password);
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

        [Route("login"), HttpPost]
        [ResponseType(typeof(UserProfileDTO))]
        public IHttpActionResult LoginUser(UserSignUpOrSignInDTO loginData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = db.Users.Where(u => u.Email == loginData.Email)
                .Include( u => u.Questions )
                .Include( u => u.Answers )
                .Include( u => u.Comments )
                .SingleOrDefault();

            if( user == null )
            {
                return NotFound();
            }

            if( !PasswordEncryptor.VerifyHash(loginData.Password, user.Password) )
            {
                return BadRequest("Password is wrong.");
            }

            user.LastLogin = DateTime.Now;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest("There was a problem. Please try again.");
            }

            UserProfileDTO userProfile = new UserProfileDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegisterDate = user.RegisterDate,
                LastLogin = user.LastLogin,
                Questions = user.Questions,
                Answers = user.Answers,
                Comments = user.Comments
            };

            return Ok(userProfile);
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