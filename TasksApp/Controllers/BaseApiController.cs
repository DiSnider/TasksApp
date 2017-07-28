using System.Collections.Generic;
using System.Web.Http;

namespace TasksApp.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IHttpActionResult GetErrorResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                return InternalServerError();
            }

            foreach (string error in errors)
            {
                ModelState.AddModelError("", error);
            }

            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
