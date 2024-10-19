using sorteio.Dominio.Base;
using sorteio.Repositorio.Contexto;

namespace sorteio.Repositorio.Repositorios.Base
{
    public class RepTeste(ContextoBanco contexto) : RepBase<Teste>(contexto), IRepTeste
    {
    }
}
