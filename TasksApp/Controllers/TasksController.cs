using AutoMapper;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TasksApp.Models;
using TasksApp.ViewModels;

namespace TasksApp.Controllers
{
    [RoutePrefix("api/tasks")]
    [Authorize]
    public class usersController : BaseApiController
    {
        protected readonly AppUserManager userManager;
        protected readonly TasksContext dbContext;
        protected readonly IMapper Mapper;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public usersController(AppUserManager userManager,
                                TasksContext dbContext,
                                IMapper mapper)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            Mapper = mapper;
        }

        [Route("")]       
        public async Task<IHttpActionResult> GetUserTasks()
        {
            var userId = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            var model = user.Tasks.OrderBy(t => t.CreationDate).Select(t => Mapper.Map<TaskViewModel>(t));

            return Ok(model);
        }

        [Route("{id:guid}", Name = "GetTask")]
        //Detailed infos
        public async Task<IHttpActionResult> GetTask(Guid id)
        {
            var userId = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            var task = user.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            return Ok(Mapper.Map<TaskDetailsViewModel>(task));
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUserTask(CreateTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            try
            {
                if (model.SelectedTags == null)
                    model.SelectedTags = new List<TagViewModel>();

                var newTask = Mapper.Map<Models.Task>(model);
                newTask.User = user;
                newTask.RelatedTags = dbContext.Tags
                    .ToList()
                    .Where
                    (tag =>
                        model.SelectedTags.Any(tm => tm.Id == tag.Id.ToString())
                    ).ToList();

                user.Tasks.Add(newTask);
                await dbContext.SaveChangesAsync();

                var location = Url.Link("GetTask", new { id = newTask.Id });
                return Created(location, Mapper.Map<TaskViewModel>(newTask));
            }
            catch (Exception e)
            {
                logger.Error($"CreateUserTask: {e.Message}");
                return InternalServerError();
            }
        }

        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateUserTask(UpdateTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            var task = user.Tasks.FirstOrDefault(t => t.Id == new Guid(model.Id));
            if (task == null)
                return NotFound();

            try
            {
                if (model.RelatedTags == null)
                    model.RelatedTags = new List<TagViewModel>();

                task.Title = model.Title;
                task.Content = model.Content;
                task.IsDone = model.IsDone;
                task.LastModificationDate = DateTime.UtcNow;
                task.ExpirationDate = model.ExpirationDate;
                task.RelatedTags.Clear();

                task.RelatedTags = dbContext.Tags
                    .ToList()
                    .Where
                    (tag =>
                        model.RelatedTags.Any(tm => tm.Id == tag.Id.ToString())
                    )
                    .ToList();

                await dbContext.SaveChangesAsync();

                return Ok(Mapper.Map<TaskViewModel>(task));
            }
            catch (Exception e)
            {
                logger.Error($"UpdateUserTask: {e.Message}");
                return InternalServerError();
            }            
        }

        [Route("{id:guid}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUserTask(Guid id)
        {
            var userId = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            var task = user.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            try
            {
                dbContext.Entry(task).State = System.Data.Entity.EntityState.Deleted;
                await dbContext.SaveChangesAsync();

                return Ok();
            }
            catch(Exception e)
            {
                logger.Error($"DeleteUserTask: {e.Message}");
                return InternalServerError();
            }
        }
    }
}
