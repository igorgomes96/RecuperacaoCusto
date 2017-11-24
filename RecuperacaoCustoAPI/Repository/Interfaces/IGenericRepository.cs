using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Repository.Interfaces
{
    public interface IGenericRepository<TKey, TEntity, TDto>
    {
        ICollection<TDto> List();
        TDto Find(TKey chave);
        TDto Save(TDto entidade);
        TDto Update(TDto entidade);
        TDto Delete(TKey chave);
        ICollection<TDto> Query(Func<TEntity, bool> predicate);
        bool Existe(TKey chave);
    }
}
