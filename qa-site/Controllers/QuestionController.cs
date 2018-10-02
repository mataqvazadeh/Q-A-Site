using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QASite;
using QASite.Models;

namespace QASite.Controllers
{
    [RoutePrefix("api/v1/qusetion")]
    public class QuestionController : ApiController
    {
        private QAContext db = new QAContext();

        [Route(""), HttpPost]
        [ResponseType(typeof(Question))]
        public IHttpActionResult AskQusetion(QuestionFormDTO questionForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Question question = new Question
            {
                Title = questionForm.Title,
                Body = questionForm.Body,
                CreateDate = DateTime.Now
            };

            db.Questions.Add(question);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = question.Id }, question);
        }

        // GET: api/Question
        public IQueryable<Question> GetQuestions()
        {
            return db.Questions;
        }

        // GET: api/Question/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult GetQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Question/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.Id)
            {
                return BadRequest();
            }

            db.Entry(question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Question/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            db.Questions.Remove(question);
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(int id)
        {
            return db.Questions.Count(e => e.Id == id) > 0;
        }
    }
}