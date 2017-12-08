using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Repository.Interfaces;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Exceptions;

namespace RecuperacaoCustoAPI.Services.Implementations
{
    public class RecuperacoesService : IRecuperacoesService
    {
        private readonly IGenericRepository<int, RecuperacaoCusto, RecuperacaoCustoDTO> _recRep;
        private readonly IGenericRepository<object[], RecuperacaoCustoMes, RecuperacaoCustoMesDTO> _recMesRep;

        public RecuperacoesService(IGenericRepository<int, RecuperacaoCusto, RecuperacaoCustoDTO> recRep,
            IGenericRepository<object[], RecuperacaoCustoMes, RecuperacaoCustoMesDTO> recMesRep)
        {
            _recRep = recRep;
            _recMesRep = recMesRep;
        }

        public RecuperacaoCustoDTO CancelarRecuperacao(int id)
        {
            RecuperacaoCustoDTO recuperacao = _recRep.Find(id);
            if (recuperacao == null)
                throw new NaoEncontradoException<RecuperacaoCusto>("Recuperação de Custo não encontrada!");

            _recMesRep.Delete(x => x.CodRecuperacao == id);
            _recRep.Delete(id);
            return recuperacao;
        }
    }
}