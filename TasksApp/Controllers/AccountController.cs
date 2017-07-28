using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using TasksApp.Models;
using TasksApp.ViewModels;

namespace TasksApp.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        protected readonly AppUserManager userManager;
        protected readonly IAuthenticationManager authenticationManager;
        protected readonly TasksContext dbContext;
        protected readonly IMapper Mapper;

        public AccountController(AppUserManager userManager,
                                IAuthenticationManager authenticationManager,
                                TasksContext dbContext,
                                IMapper mapper)
        {
            this.userManager = userManager;
            this.authenticationManager = authenticationManager;
            this.dbContext = dbContext;
            Mapper = mapper;
        }

        [Route("", Name = "GetUser")]
        [Authorize]
        public async Task<IHttpActionResult> GetUser()
        {
            var userId = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserInfoViewModel>(user));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<AppUser>(model);

                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result.Errors);                    
                }
                else
                {
                    var locationHeader = Url.Link("GetUser", new { });
                    await SignInAsync(user);

                    return Created(locationHeader, Mapper.Map<UserInfoViewModel>(user));
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong email and/or password");
                }
                else
                {
                    await SignInAsync(user, model.RememberMe);

                    return Ok(Mapper.Map<UserInfoViewModel>(user));
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            authenticationManager.SignOut();

            return Ok();
        }

        [NonAction]
        private async System.Threading.Tasks.Task SignInAsync(AppUser user, bool rememberMe = true)
        {
            ClaimsIdentity claim = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut();
            authenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = rememberMe
                }, 
                claim);
        }
    }
}
