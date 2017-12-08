using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Repository.Interfaces;
using RecuperacaoCustoAPI.Exceptions;

namespace RecuperacaoCustoAPI.Services.Implementations
{
    public class CategoriaCRService : ICategoriaCRService
    {
        private readonly IGenericRepository<string, CategoriaCR, CategoriaCRDTO> _rep;

        public CategoriaCRService(IGenericRepository<string, CategoriaCR, CategoriaCRDTO> rep)
        {
            _rep = rep;
        }

        public CategoriaCRDTO Delete(string categoria)
        {
            return _rep.Delete(categoria);
        }

        public ICollection<CategoriaCRDTO> List()
        {
            return _rep.List();
        }

        public CategoriaCRDTO Save(CategoriaCRDTO categoria)
        {
            try
            {
                _rep.Save(categoria);
            }
            catch (ConflitoException<CategoriaCR>)
            {
                _rep.Update(categoria.Categoria, categoria);
            }
            return categoria;
        }
    }
}