using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace RecuperacaoCustoAPI.Services.Implementations
{
    public class OleDb : IOleDb
    {
        private string FileName;
        private OleDbConnection conexao;
        
        public void Conectar(string fileName, bool headers)
        {
            this.FileName = fileName;
            conexao = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source=" + FileName + ";Extended Properties='Excel 12.0 Xml;HDR = YES';");
            conexao.Open();
        }

        public void Desconectar()
        {
            conexao.Close();
        }

        public int ExecuteNonQuery(string sql)
        {
            OleDbCommand command = new OleDbCommand
            {
                Connection = conexao,
                CommandText = sql
            };
            return command.ExecuteNonQuery();
        }

        public DataSet ExecuteQuery(string sql)
        {
            throw new NotImplementedException();
        }
    }
}