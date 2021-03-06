﻿using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Exceptions;
using RecuperacaoCustoAPI.Filters;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace RecuperacaoCustoAPI.Excel
{
    public class ImportacaoOleDB
    {
        private OleDbConnection conexao;
        private OleDbDataAdapter adapter;
        private Contexto db;
        private IPrincipal user = null;
        public Ciclo CicloAtual { get; set; }
        public string FileName { get; set; }

        public ImportacaoOleDB() { }

        public ImportacaoOleDB(string fileName, Ciclo ciclo, IPrincipal user)
        {
            FileName = fileName;
            CicloAtual = ciclo;
            this.user = user;
        }

        private void Conectar()
        {
            conexao = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source=" + FileName + ";Extended Properties='Excel 12.0 Xml; HDR = YES';");
        }

        private void Desconectar()
        {
            if (conexao.State == ConnectionState.Open) conexao.Close();
        }

        public void ExecutarImportacao()
        {
            DataSet ds;
            Conectar();
            db = new Contexto();
            string tab = "";

            try
            {
                conexao.Open();

                tab = "Template";
                adapter = new OleDbDataAdapter("select * from[" + tab + "$B7:Z2000]", conexao);
                ds = new DataSet() { DataSetName = tab };
                try
                {
                    adapter.Fill(ds);
                    ImportacaoRecuperacoes(ds);
                }
                catch (OleDbException e)
                {
                    if (e.Message.Split(new char[] { '.' })[0] != "'" + tab + "' is not a valid name") throw;
                }

            }
            catch (CROrigemNaoEncontradoException e)
            {
                throw new Exception("CR de origem não encontrado na base! (CR " + e.CR + "; Linha " + e.Linha + ")");
            }
            catch (CROrigemNaoAutorizadoException e)
            {
                throw new Exception("CR de origem não autorizado para o usuário '" + ((CustomIdentity)user.Identity).Usuario.Nome + "'! (CR " + e.CR + "; Linha " + e.Linha + ")");
            }
            catch (CRDestinoNaoEncontradoException e)
            {
                throw new Exception("CR de destino não encontrado na base! (CR " + e.CR + "; Linha " + e.Linha + ")");
            }
            catch (TipoRecuperacaoNaoEncontradoException e)
            {
                throw new Exception("Tipo de recuperação inválido! (Tipo: " + e.Tipo + "; Linha " + e.Linha + ")");
            }
            catch (OleDbException e)
            {
                throw new Exception("Erro ao salvar os dados: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro inesperado! " + e.Message);
            }
            finally
            {
                Desconectar();
            }


            db.Dispose();
        }

        public void ImportacaoRecuperacoes(DataSet ds)
        {
            int l = 8;
            Dictionary<string, IEnumerable<RecuperacaoCusto>> recuperacoes = new Dictionary<string, IEnumerable<RecuperacaoCusto>>();
            try
            {
                RecuperacaoCusto rec;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //Condição de parada
                    if (r[0] == null || r[0].ToString() == "")
                        break;

                    CR crOrigem = db.CR.Find(r[0].ToString());
                    if (crOrigem == null) throw new CROrigemNaoEncontradoException(r[0].ToString(), ds.DataSetName, l);

                    CR crDestino = db.CR.Find(r[1].ToString());
                    if (crDestino == null) throw new CRDestinoNaoEncontradoException(r[1].ToString(), ds.DataSetName, l);

                    string tipoNome = r[2].ToString();
                    TipoRecuperacao tipo = db.TipoRecuperacao.Where(x => x.Tipo == tipoNome).FirstOrDefault();
                    if (tipo == null) throw new TipoRecuperacaoNaoEncontradoException(r[2].ToString(), ds.DataSetName, l);

                    rec = new RecuperacaoCusto
                    {
                        CROrigem = crOrigem.Codigo,
                        CRDestino = crDestino.Codigo,
                        TipoRecuperacaoCod = tipo.Codigo,
                        DataHora = DateTime.Now,
                        CodCiclo = CicloAtual.Codigo,
                        Motivo = r[3].ToString(),
                        LoginEnvio = ((CustomIdentity)user.Identity).Usuario.Login
                    };

                    db.RecuperacaoCusto.Add(rec);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    int col = 4; //Coluna onde se iniciam os valores da recuperação.

                    foreach (MesCiclo m in CicloAtual.MesesCiclos.OrderBy(x => x.Mes))
                    {
                        if (r[col] != null && r[col].ToString() != "")
                        {
                            float valor;
                            if (float.TryParse(r[col].ToString(), out valor))
                            {
                                if (valor != 0)
                                    db.RecuperacaoCustoMes.Add(new RecuperacaoCustoMes
                                    {
                                        CodRecuperacao = rec.Codigo,
                                        CodMesCiclo = m.Codigo,
                                        Valor = valor
                                    });
                            }
                        }
                        col++;
                    }

                    try
                    {
                        db.SaveChanges();
                        if (recuperacoes.ContainsKey(rec.Destino.Responsavel.Email))
                            ((List<RecuperacaoCusto>)recuperacoes[rec.Destino.Responsavel.Email]).Add(rec);
                        else
                        {
                            IEnumerable<RecuperacaoCusto> temp = new List<RecuperacaoCusto>();
                            ((List<RecuperacaoCusto>)temp).Add(rec);
                            recuperacoes.Add(rec.Destino.Responsavel.Email, temp);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    l++;
                }

                foreach (string email in recuperacoes.Keys)
                {
                    Thread t = new Thread(SendEmail.Send);
                    t.Start(new EmailDTO(new[] { email }, "Recuperações de Custos", recuperacoes[email]));
                }


            }
            catch (CROrigemNaoEncontradoException e)
            {
                throw e;
            }
            catch (CROrigemNaoAutorizadoException e)
            {
                throw e;
            }
            catch (CRDestinoNaoEncontradoException e)
            {
                throw e;
            }
            catch (TipoRecuperacaoNaoEncontradoException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExcelException(e.Message, ds.DataSetName, l);
            }


        }
    }
}