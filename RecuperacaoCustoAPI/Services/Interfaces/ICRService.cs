using RecuperacaoCustoAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Services.Interfaces
{
    public interface ICRService
    {
        ICollection<CRDTO> List(string search);
        CRDTO Find(string codigo);
        CRDTO Save(CRDTO cr);
        CRDTO Delete(string codigo);
        bool CRExiste(string CR);
    }
}
