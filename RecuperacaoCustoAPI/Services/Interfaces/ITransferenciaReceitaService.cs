using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Services.Interfaces
{
    public interface ITransferenciaReceitaService
    {
        ICollection<TransferenciaReceitaDTO> List();
        ICollection<TransferenciaReceitaDTO> List(Func<TransferenciaReceita, bool> predicate);
        TransferenciaReceitaDTO Find(int codigo);
        TransferenciaReceitaDTO Save(TransferenciaReceitaDTO transf);
        TransferenciaReceitaDTO Update(TransferenciaReceitaDTO transf);
        TransferenciaReceitaDTO Delete(int codigo);
        void Report(string FileName, string Template, DateTime mes);
        ICollection<int> GetAnos();
    }
}
