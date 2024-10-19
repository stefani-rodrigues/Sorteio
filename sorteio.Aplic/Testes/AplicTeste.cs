using sorteio.Infra.Base;

namespace sorteio.Aplic.Testes
{
    public class AplicTeste : AplicBase, IAplicTeste
    {
        public string Testar(string nome)
        {
            return nome + ". Parabéns, está funfando hehe 😁😁";
        }
    }
}
