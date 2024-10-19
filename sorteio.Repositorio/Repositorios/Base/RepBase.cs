using Microsoft.EntityFrameworkCore;
using sorteio.Infra.Base;
using sorteio.Repositorio.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace sorteio.Repositorio.Repositorios.Base
{
    public class RepBase<TEnt>
        where TEnt : Identificador
    {

        public required ContextoBanco _Db;
    public required DbSet<TEnt> _DbSet;
    public IQueryable<TEnt> Consulta => _DbSet;

    public RepBase(ContextoBanco contexto)
    {
        _Db = contexto;
        _DbSet = _Db.Set<TEnt>();
    }

    public virtual async Task Inserir(TEnt ent)
    {
        try
        {

            await _DbSet.AddAsync(ent);
            _Db.SaveChanges();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public virtual async Task InserirRange(List<TEnt> ent)
    {
        try
        {

            await _DbSet.AddRangeAsync(ent);
            _Db.SaveChanges();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public virtual IQueryable<TEnt> Listar()
    {
        var ret = _DbSet.AsQueryable();

        return ret;
    }
    public virtual void SaveChanges()
    {
        _Db.SaveChanges();
    }
    public virtual IQueryable<TEnt>? Where(Expression<Func<TEnt, bool>> func)
    {
        var ret = _DbSet.Where(func);

        return ret;
    }
    public virtual TEnt? Find(int id)
    {
        var ent = Where(p => p.Id == id);

        if (ent == null)
            return null;
        var ret = ent.FirstOrDefault();

        return ret;
    }
    public virtual void Remove(TEnt ent)
    {
        _DbSet.Remove(ent);
        _Db.SaveChanges();
    }
    public virtual void RemoveRange(List<TEnt> ent)
    {
        _DbSet.RemoveRange(ent);
        _Db.SaveChanges();
    }
    public int ReservarSequence(string sequence)
    {
        var sqlGeraReserva = MontarSqlGeraReserva(sequence);
        var sql = MontarSqlBuscaSequence(sequence);
        try
        {
            Executar_Reserva(sqlGeraReserva);
            int.TryParse(Executar(sql), out var result);
            _Db.SaveChanges();
            return result;
        }
        catch (Exception)
        {
            CriarSequence(sequence);
            var ret = _Db.Database.ExecuteSqlRaw(sql);
            _Db.SaveChanges();
            return ret;
        }
    }
    public void CriarSequence(string sequence)
    {
        var sql = $"CREATE TABLE {sequence} (id INT NOT NULL);";

        _Db.Database.ExecuteSqlRaw(sql);
        _Db.SaveChanges();
    }

    public bool Any()
    {
        return ((IQueryable<TEnt>)_DbSet).Any();
    }

    public bool Any(Expression<Func<TEnt, bool>> exp)
    {
        return ((IQueryable<TEnt>)_DbSet).Any(exp);
    }


    private string MontarSqlBuscaSequence(string sequence)
    {
        return $"select id from {sequence}";

    }
    private string MontarSqlGeraReserva(string sequence)
    {
        return $"UPDATE {sequence} SET id=LAST_INSERT_ID(id+1)";
    }
    private string Executar(string sql)
    {
        List<object> list = new List<object>();
        using DbCommand comm = _Db.Database.GetDbConnection().CreateCommand();
        comm.CommandText = sql;
        _Db.Database.OpenConnection();
        try
        {
            //using DbDataReader dbDataReader = comm.ExecuteReader();
            //while (dbDataReader.Read())
            //{
            //    IDictionary<string, object> dictionay = new ExpandoObject();
            //    for(int i = 0; i< dbDataReader.FieldCount; i++)
            //    {
            //        dictionay.Add(dbDataReader.GetName(i), dbDataReader[i]);
            //    }

            //    list.Add(dictionay);
            //}
            //return list;
            return comm.ExecuteScalar().ToString();
        }
        finally
        {

            if (comm.Connection.State == System.Data.ConnectionState.Open)
                comm.Connection.Close();
        }
    }
    private void Executar_Reserva(string sql)
    {
        List<object> list = new List<object>();
        using DbCommand comm = _Db.Database.GetDbConnection().CreateCommand();
        comm.CommandText = sql;
        _Db.Database.OpenConnection();
        try
        {
            //using DbDataReader dbDataReader = comm.ExecuteReader();
            //while (dbDataReader.Read())
            //{
            //    IDictionary<string, object> dictionay = new ExpandoObject();
            //    for(int i = 0; i< dbDataReader.FieldCount; i++)
            //    {
            //        dictionay.Add(dbDataReader.GetName(i), dbDataReader[i]);
            //    }

            //    list.Add(dictionay);
            //}
            //return list;
            comm.ExecuteNonQuery();
            _Db.SaveChanges();
        }
        finally
        {

            if (comm.Connection.State == System.Data.ConnectionState.Open)
                comm.Connection.Close();
        }
    }
}
}
