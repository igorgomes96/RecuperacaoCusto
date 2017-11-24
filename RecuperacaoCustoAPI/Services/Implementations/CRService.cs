using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Repository.Interfaces;
using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Services.Implementations
{
    public class CRService : ICRService
    {
        private readonly IGenericRepository<string, CR, CRDTO> _crRep;

        public CRService(IGenericRepository<string, CR, CRDTO> crRep)
        {
            _crRep = crRep;
        }

        bool ICRService.CRExiste(string CR)
        {
            return _crRep.Existe(CR);
        }

    }
}