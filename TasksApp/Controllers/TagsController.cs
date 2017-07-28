using AutoMapper;
using System.Linq;
using System.Web.Http;
using TasksApp.Models;
using TasksApp.ViewModels;

namespace TasksApp.Controllers
{
    [Authorize]
    public class TagsController : BaseApiController
    {
        protected readonly AppUserManager userManager;
        protected readonly TasksContext dbContext;
        protected readonly IMapper Mapper;

        public TagsController(AppUserManager userManager,
                                TasksContext dbContext,
                                IMapper mapper)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            Mapper = mapper;
        }

        [Route("api/tags")]
        public IHttpActionResult GetTags()
        {
            var model = dbContext.Tags
                .OrderBy(t => t.Name)
                .AsEnumerable()
                .Select(tag => Mapper.Map<TagViewModel>(tag))
                .ToList();

            return Ok(model);
        }
    }
}
