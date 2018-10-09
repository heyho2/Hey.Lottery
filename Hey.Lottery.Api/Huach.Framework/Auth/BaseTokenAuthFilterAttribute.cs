using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Huach.Framework.Models
{
    public abstract class BaseTokenAuthFilterAttribute : BaseAuthFilterAttribute
    {
        protected virtual string TokenKeyName
        {
            get
            {
                return "Authorization";
            }
        }
        protected virtual string GetToken(HttpActionContext actionContext)
        {
            return actionContext.Request.Headers.GetValues(TokenKeyName).FirstOrDefault();
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (IsAllowAnonymous(actionContext))
            {
                return;
            }
            if (IsAuthenticated(GetToken(actionContext)))
            {
                base.OnAuthorization(actionContext);
                return;
            }
            HandleUnauthenticatedRequest(actionContext, null);
        }
        protected abstract bool IsAuthenticated(string token);
    }
}
