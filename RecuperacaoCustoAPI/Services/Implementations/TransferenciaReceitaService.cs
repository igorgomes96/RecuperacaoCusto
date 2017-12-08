using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Repository.Interfaces;
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Exceptions;
using System.IO;
using System.Data.OleDb;
using System.Diagnostics;

namespace RecuperacaoCustoAPI.Services.Implementations
{
    public class TransferenciaReceitaService : ITransferenciaReceitaService
    {
        private readonly IGenericRepository<int, TransferenciaReceita, TransferenciaReceitaDTO> _repository;
        private readonly ICRService _crService;
        private readonly IRelatorioTransfReceitaRepository _relatorioTransfReceitaRepository;
        private readonly IOleDb _oleDb;
        private readonly IMesesBloqueadosService _mesesBloqueadosService;


        public TransferenciaReceitaService(IGenericRepository<int, TransferenciaReceita, TransferenciaReceitaDTO> repository, 
            ICRService crService, IRelatorioTransfReceitaRepository relatorioTransfReceitaRepository, IOleDb oleDb,
            IMesesBloqueadosService mesesBloqueadosService)
        {
            _repository = repository;
            _crService = crService;
            _relatorioTransfReceitaRepository = relatorioTransfReceitaRepository;
            _oleDb = oleDb;
            _mesesBloqueadosService = mesesBloqueadosService;
        }

        public TransferenciaReceitaDTO Delete(int codigo)
        {
            TransferenciaReceitaDTO transf = _repository.Find(codigo);

            if (_mesesBloqueadosService.EstaBloqueado(transf.DataEmissao))
                throw new MesBloqueadoException();

            return _repository.Delete(codigo);
        }

        public TransferenciaReceitaDTO Find(int codigo)
        {
            return _repository.Find(codigo);
        }

        public ICollection<int> GetAnos()
        {
            return List().Select(x => x.DataEmissao.Year).Distinct().ToList();
        }

        public ICollection<TransferenciaReceitaDTO> List()
        {
            return _repository.List();
        }

        public ICollection<TransferenciaReceitaDTO> List(Func<TransferenciaReceita, bool> predicate)
        {
            return _repository.Query(predicate);
        }

        public void Report(string FileName, string Template, DateTime mes)
        {
           
            File.Copy(Template, FileName);

            _oleDb.Conectar(FileName, false);
            foreach (RelatorioTransfReceita r in _relatorioTransfReceitaRepository.List(mes)) {
                string sql = string.Format(
                    "insert into [Planilha$B2:S10000] values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                    r.Empresa, r.UN, r.UN, r.Conta_Contabil, r.CR, r.Produto, r.Intercompany, r.UF, r.Cliente, r.Reserva1,
                    r.Reserva2, r.Debito, r.Credito, r.ItemFaturamento, r.Documento, r.NF, r.DataEmissao.ToString("dd/MM/yyyy"), r.Historico.Replace("\'", "")
                );

                _oleDb.ExecuteNonQuery(sql);
            }
            _oleDb.Desconectar();

        }


        public TransferenciaReceitaDTO Save(TransferenciaReceitaDTO transf)
        {

            if (_mesesBloqueadosService.EstaBloqueado(transf.DataEmissao))
                throw new MesBloqueadoException();

            transf.DataHora = DateTime.Now;
            transf.ISS = transf.ISS != 0 ? transf.ISS / 100 : 0;
            transf.CPRB = transf.CPRB != 0 ? transf.CPRB / 100 : 0;

            try { 
                return _repository.Save(transf);
            } catch (Exception e)
            {
                if (!_crService.CRExiste(transf.CROrigem))
                    throw new NaoEncontradoException<CR>("CR de Origem não encontrado!");

                if (!_crService.CRExiste(transf.CRDestino))
                    throw new NaoEncontradoException<CR>("CR de Destino não encontrado!");

                if (!_crService.CRExiste(transf.RegimeTributacao))
                    throw new NaoEncontradoException<CR>("Regime de Tributação não encontrado!");

                throw e;
            }
        }

        public TransferenciaReceitaDTO Update(TransferenciaReceitaDTO transf)
        {
            if (_mesesBloqueadosService.EstaBloqueado(transf.DataEmissao))
                throw new MesBloqueadoException();

            transf.DataHora = DateTime.Now;
            transf.ISS = transf.ISS != 0 ? transf.ISS / 100 : 0;

            return _repository.Update(transf.Codigo, transf);
        }
    }
}