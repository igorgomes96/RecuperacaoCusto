using RecuperacaoCustoAPI.Exceptions;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Repository.Implementations
{
    public class GenericRepository<TKey, TEntity, TDto> : IGenericRepository<TKey, TEntity, TDto> where TEntity : class
    {
        private readonly IMapper<TEntity, TDto> _mapper;
        private readonly DbContext _db;

        public GenericRepository(IMapper<TEntity, TDto> mapper, DbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public TDto Delete(TKey chave)
        {
            TEntity entity = _db.Set<TEntity>().Find(chave);
            if (entity != null)
                _db.Set<TEntity>().Remove(entity);
            else
                throw new NaoEncontradoException<TEntity>();

            _db.SaveChanges();
            return _mapper.Map(entity);

        }

        public ICollection<TDto> Query(Func<TEntity, bool> predicate)
        {
            return _mapper.Map(_db.Set<TEntity>().Where(predicate).ToList());
        }

        public TDto Find(TKey chave)
        {
            TEntity entidade = _db.Set<TEntity>().Find(chave);
            if (entidade == null) throw new NaoEncontradoException<TEntity>();

            return _mapper.Map(entidade);
        }

        public ICollection<TDto> List()
        {
            return _mapper.Map(_db.Set<TEntity>().ToList());
        }

        public TDto Save(TDto entidade)
        {
            TEntity entidadeSalva;
            entidadeSalva = _db.Set<TEntity>().Add(_mapper.Map(entidade));
            _db.SaveChanges();

            return _mapper.Map(entidadeSalva);
        }

        public TDto Update(TDto entidade)
        {
            try
            {
                TEntity entidadeDB = _mapper.Map(entidade);
                _db.Entry(entidadeDB).State = EntityState.Detached;
                _db.Entry(entidadeDB).State = EntityState.Modified;
                _db.SaveChanges();
            } catch (DbUpdateConcurrencyException)
            {
                throw new NaoEncontradoException<TEntity>();
            }
            return entidade;

        }

        public bool Existe(TKey chave)
        {
            return _db.Set<TEntity>().Find(chave) != null;
        }
    }
}