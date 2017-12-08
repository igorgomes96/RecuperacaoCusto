using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Repository.Interfaces;
using RecuperacaoCustoAPI.Exceptions;

namespace RecuperacaoCustoAPI.Services.Implementations
{
    public class MesesBloqueadosService : IMesesBloqueadosService
    {
        private readonly IGenericRepository<DateTime, MesBloqueadoTransfReceita, MesBloqueadoTransfReceitaDTO> _rep;

        public MesesBloqueadosService(IGenericRepository<DateTime, MesBloqueadoTransfReceita, MesBloqueadoTransfReceitaDTO> rep)
        {
            _rep = rep;
        }

        public void BloquearMes(DateTime mes)
        {
            DateTime novoMes = new DateTime(mes.Year, mes.Month, 1);
            try { 
                _rep.Save(new MesBloqueadoTransfReceitaDTO { Mes = novoMes });
            } catch (Exception e)
            {
                if (_rep.Existe(novoMes))
                    throw new ConflitoException<MesBloqueadoTransfReceita>();
                throw e;
            }
        }


        public void Delete(DateTime mes)
        {
            DateTime novoMes = new DateTime(mes.Year, mes.Month, 1);
            _rep.Delete(novoMes);
        }

        public bool EstaBloqueado(DateTime mes)
        {
            return _rep.List().Count(x => x.Mes.Year == mes.Year && x.Mes.Month == mes.Month) > 0;
        }

        public ICollection<int> GetAnos()
        {
            return List().Select(x => x.Mes.Year).Distinct().ToList();
        }

        public ICollection<MesBloqueadoTransfReceitaDTO> List()
        {
            return _rep.List();
        }
    }
}