using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Repository.Interfaces
{
    public interface IRelatorioTransfReceitaRepository
    {
        ICollection<RelatorioTransfReceita> List(DateTime mes);
    }
}
