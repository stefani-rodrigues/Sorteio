using FrozenForge.Apis;
using Microsoft.AspNetCore.Mvc;
using sorteio.Infra.Base;
using System.Net;

namespace Sorteio.Controllers.Base
{
    public class ResponceControllerBase : ApiResponse
    {
        public bool Success { get; set; }

        public IActionResult RetSucesso()
        {
            StatusCode = HttpStatusCode.OK;
            Success = true;
            return new ControllerListaBase().Ok(this);
        }

        public IActionResult RetSucesso(object content)
        {
            StatusCode = HttpStatusCode.OK;
            Body = content;
            Success = true;
            return new ControllerListaBase().Ok(this);
        }

        public IActionResult RetSucesso(object content, string? msg)
        {
            StatusCode = HttpStatusCode.OK;
            Body = content;
            ReasonPhrase = msg;
            Success = true;
            return new ControllerListaBase().Ok(this);
        }

        public IActionResult RetAlerta(string? msg)
        {
            StatusCode = HttpStatusCode.FailedDependency;
            ReasonPhrase = msg;
            Success = false;
            return new ControllerListaBase().Ok(this);
        }

        public IActionResult RetAlerta(string? msg, object content)
        {
            StatusCode = HttpStatusCode.FailedDependency;
            ReasonPhrase = msg;
            Body = content;
            Success = false;
            return new ControllerListaBase().BadRequest(this);
        }

        public IActionResult RetError(string? msg)
        {
            StatusCode = HttpStatusCode.ExpectationFailed;
            ReasonPhrase = msg;
            Success = false;
            return new ControllerListaBase().BadRequest(this);
        }

        public IActionResult RetError()
        {
            StatusCode = HttpStatusCode.ExpectationFailed;
            ReasonPhrase = "";
            Success = false;
            return new ControllerListaBase().BadRequest(this);
        }

        public IActionResult RetErrorLogin(string? msg)
        {
            StatusCode = HttpStatusCode.BadRequest;
            ReasonPhrase = msg;
            Success = false;
            return new ControllerListaBase().BadRequest(this);
        }
    }
}
