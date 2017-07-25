using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentService.Controllers
{
    public class StudentsController : ApiController
    {
        [Authorize]
        public IEnumerable<Student> Get()
        {
            using (StudentDBEntities entities = new StudentDBEntities())
            {
                return entities.Students.ToList();
            }
        }
    }
}
