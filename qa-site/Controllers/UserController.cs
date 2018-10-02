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
using QASite;
using QASite.Models;
using QASite.Helper;
using Microsoft.AspNet.Identity;

namespace QASite.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiController
    {
        private AuthRepository _repo = null;

        public UserController()
        {
            _repo = new AuthRepository();
        }

        [AllowAnonymous]
        [Route("register"), HttpPost]
        [ResponseType(typeof(UserRegisterDTO))]
        public IHttpActionResult RegisterUser(UserRegisterDTO registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = _repo.RegisterUser(registrationData);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        //[Route("login"), HttpPost]
        //[ResponseType(typeof(UserProfileDTO))]
        //public IHttpActionResult LoginUser(UserRegisterDTO loginData)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    User user = db.Users.Where(u => u.Email == loginData.Email)
        //        .Include( u => u.Questions )
        //        .Include( u => u.Answers )
        //        .Include( u => u.Comments )
        //        .SingleOrDefault();

        //    if( user == null )
        //    {
        //        return NotFound();
        //    }

        //    if( !PasswordEncryptor.VerifyHash(loginData.Password, user.Password) )
        //    {
        //        return BadRequest("Password is wrong.");
        //    }

        //    user.LastLogin = DateTime.Now;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return BadRequest("There was a problem. Please try again.");
        //    }

        //    UserProfileDTO userProfile = new UserProfileDTO
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        RegisterDate = user.RegisterDate,
        //        LastLogin = user.LastLogin,
        //        Questions = user.Questions,
        //        Answers = user.Answers,
        //        Comments = user.Comments
        //    };

        //    return Ok(userProfile);
        //}

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}