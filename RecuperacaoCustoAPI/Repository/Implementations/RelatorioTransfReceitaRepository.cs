using RecuperacaoCustoAPI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecuperacaoCustoAPI.Models;
using System.Data.Entity;

namespace RecuperacaoCustoAPI.Repository.Implementations
{
    public class RelatorioTransfReceitaRepository : IRelatorioTransfReceitaRepository
    {
        private DbContext _db;
        public RelatorioTransfReceitaRepository(DbContext db)
        {
            _db = db;
        }
        public ICollection<RelatorioTransfReceita> List(DateTime mes)
        {
            return _db.Set<RelatorioTransfReceita>()
                .Where(x => x.DataEmissao.Year == mes.Year && x.DataEmissao.Month == mes.Month)
                .OrderBy(x => x.CR).ThenBy(x => x.NF).ThenBy(x => x.Conta_Contabil)
                .ThenBy(x => x.Tipo).ThenBy(x => x.Debito).ThenBy(x => x.Credito)
                .ToList();
        }
    }
}