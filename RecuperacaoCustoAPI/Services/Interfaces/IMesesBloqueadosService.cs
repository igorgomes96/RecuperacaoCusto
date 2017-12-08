using RecuperacaoCustoAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Services.Interfaces
{
    public interface IMesesBloqueadosService
    {
        ICollection<MesBloqueadoTransfReceitaDTO> List();
        void Delete(DateTime mes);
        void BloquearMes(DateTime mes);
        bool EstaBloqueado(DateTime mes);
        ICollection<int> GetAnos();
    }
}
