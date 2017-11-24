using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Repository.Interfaces;
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Exceptions;

namespace RecuperacaoCustoAPI.Services.Implementations
{
    public class TransferenciaReceitaService : ITransferenciaReceitaService
    {
        private readonly IGenericRepository<int, TransferenciaReceita, TransferenciaReceitaDTO> _repository;
        private readonly ICRService _crService;

        public TransferenciaReceitaService(IGenericRepository<int, TransferenciaReceita, TransferenciaReceitaDTO> repository, ICRService crService)
        {
            _repository = repository;
            _crService = crService;
        }

        public TransferenciaReceitaDTO Delete(int codigo)
        {
            return _repository.Delete(codigo);
        }

        public TransferenciaReceitaDTO Find(int codigo)
        {
            return _repository.Find(codigo);
        }

        public ICollection<TransferenciaReceitaDTO> List()
        {
            return _repository.List();
        }

        public ICollection<TransferenciaReceitaDTO> List(Func<TransferenciaReceita, bool> predicate)
        {
            return _repository.Query(predicate);
        }

        public TransferenciaReceitaDTO Save(TransferenciaReceitaDTO transf)
        {
            transf.DataHora = DateTime.Now;
            transf.ISS = transf.ISS != 0 ? transf.ISS / 100 : 0;

            try { 
                return _repository.Save(transf);
            } catch (Exception e)
            {
                if (!_crService.CRExiste(transf.CROrigem))
                    throw new NaoEncontradoException<string>("CR de Origem não encontrado!");

                if (!_crService.CRExiste(transf.CRDestino))
                    throw new NaoEncontradoException<string>("CR de Destino não encontrado!");

                if (!_crService.CRExiste(transf.RegimeTributacao))
                    throw new NaoEncontradoException<string>("Regime de Tributação não encontrado!");

                throw e;
            }
        }

        public TransferenciaReceitaDTO Update(TransferenciaReceitaDTO transf)
        {
            transf.DataHora = DateTime.Now;
            transf.ISS = transf.ISS != 0 ? transf.ISS / 100 : 0;

            return _repository.Update(transf);
        }
    }
}