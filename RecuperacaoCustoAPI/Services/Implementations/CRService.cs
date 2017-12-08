using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Exceptions;
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

        public CRDTO Delete(string codigo)
        {
            return _crRep.Delete(codigo);
        }

        public CRDTO Find(string codigo)
        {
            return _crRep.Find(codigo);
        }

        public ICollection<CRDTO> List(string search)
        {
            if (search != null && search.Length > 0) {
                search = search.Trim().ToLower();
                return _crRep.Query(x => x.Codigo.Contains(search) || x.Descricao.ToLower().Contains(search) || x.ResponsavelLogin.ToLower().Contains(search));
            }
            return _crRep.List();
        }

        public CRDTO Save(CRDTO cr)
        {
            try { 
                return _crRep.Save(cr);
            } catch (Exception e)
            {
                if (_crRep.Existe(cr.Codigo))
                {
                    return _crRep.Update(cr.Codigo, cr);
                }
                else throw e;
            }
        }


        bool ICRService.CRExiste(string CR)
        {
            return _crRep.Existe(CR);
        }

    }
}