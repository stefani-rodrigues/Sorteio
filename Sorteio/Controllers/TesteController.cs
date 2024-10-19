using Microsoft.AspNetCore.Mvc;
using sorteio.Aplic.Testes;
using Sorteio.Controllers.Base;

namespace Sorteio.Controllers
{
    [ApiController]
    [Route("api/Teste")]
    public class TesteController(IAplicTeste _aplicTeste) : ControllerBase
    {
        [HttpGet]
        [Route("Testar/{nome}")]
        public IActionResult AlterarPedido([FromRoute] string nome)
        {
            try
            {
                var ret = _aplicTeste.Testar(nome);

                return new ResponceControllerBase().RetSucesso(content: ret);
            }
            catch (Exception ex)
            {
                return new ResponceControllerBase().RetError(ex.Message);
            }
        }
    }
}
