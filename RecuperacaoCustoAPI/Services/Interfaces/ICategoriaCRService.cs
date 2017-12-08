using RecuperacaoCustoAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Services.Interfaces
{
    public interface ICategoriaCRService
    {
        ICollection<CategoriaCRDTO> List();
        CategoriaCRDTO Save(CategoriaCRDTO categoria);
        CategoriaCRDTO Delete(string categoria);
    }
}
