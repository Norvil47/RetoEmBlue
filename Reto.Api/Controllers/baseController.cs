using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace Reto.Api.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class baseController : ControllerBase
    {
        private IMediator mediator_;

        protected IMediator _mediator => mediator_ ?? (mediator_ = HttpContext.RequestServices.GetService<IMediator>());
    }
}
