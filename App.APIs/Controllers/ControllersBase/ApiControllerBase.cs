using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers.BaseController
{
    [Route("api/[controller]")]
    [ApiController]     
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Token Business
        /// </summary>

        /// <summary>
        /// Response Handler
        /// </summary>

        /// <inheritdoc />
        public ApiControllerBase()
        {
        }

        /// <inheritdoc />
        private readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException();
        }

        protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
            return await _mediator.Send(query);
        }

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null) return NotFound();
            return Ok(data);
        }

        protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
        {
            return await _mediator.Send(command);
        }

    }
}
