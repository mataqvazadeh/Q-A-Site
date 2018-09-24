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
        [ResponseType(typeof(UserRegisterDTO))]
        public IHttpActionResult RegisterUser(UserRegisterDTO registerdUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(registerdUser.Password != registerdUser.PasswordConfirm)
            {
                return BadRequest("Password and Confirmation Mismatched!");
            }

            User user = new User();
            user.Email = registerdUser.Email;
            user.Password = PasswordEncryptor.ComputeHash(registerdUser.Password);
            user.RegisterDate = DateTime.Now;

            db.Users.Add(user);
            db.SaveChanges();

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