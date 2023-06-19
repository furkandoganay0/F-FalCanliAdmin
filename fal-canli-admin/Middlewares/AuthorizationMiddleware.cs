using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace fal_canli_admin.Middlewares
{
    public class AuthorizationMiddleware : ActionFilterAttribute
    {
        //private IConfiguration _configuration;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //_configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            //_service = new CustomRestService(_configuration);

            bool isAuthorized = false;

            ISession _session = context.HttpContext.Session;

            var userId = _session.GetInt32("UserId");
            //var expiration =Convert.ToDateTime(_session.GetString("Expiration"));

            if (userId > 0)
            {
                //if (expiration < DateTime.Now)
                //{
                //    var _service = new CustomRestService(_configuration);

                //    var tokenForLoginResponseDto = _service.Login(new UserForLoginDto() { Email = email, Password = password });
                //}

                isAuthorized = true;
            }
                

            if (!isAuthorized)
            {
                //throw new NotAuthorizedException();
                context.Result = new RedirectResult("/Login");
                return;
            }
            else
            {
                base.OnActionExecuting(context);
            }

        }
    }
}
