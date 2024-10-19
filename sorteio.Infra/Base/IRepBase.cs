using System.Linq.Expressions;

namespace sorteio.Infra.Base
{
    public interface IRepBase<TEnt>
        where TEnt : class
    {
        IQueryable<TEnt> Consulta { get; }
        Task Inserir(TEnt ent);
        Task InserirRange(List<TEnt> ent);
        IQueryable<TEnt> Listar();
        void SaveChanges();
        IQueryable<TEnt>? Where(Expression<Func<TEnt, bool>> func);
        TEnt? Find(int id);
        void Remove(TEnt ent);
        void RemoveRange(List<TEnt> ent);
        void CriarSequence(string sequence);
        int ReservarSequence(string sequence);
        bool Any();
        bool Any(Expression<Func<TEnt, bool>> func);
    }
}
