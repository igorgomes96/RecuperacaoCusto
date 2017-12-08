using RecuperacaoCustoAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Services.Interfaces
{
    public interface IRecuperacoesService
    {
        RecuperacaoCustoDTO CancelarRecuperacao(int id);
    }
}
