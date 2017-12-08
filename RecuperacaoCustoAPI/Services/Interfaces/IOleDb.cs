using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacaoCustoAPI.Services.Interfaces
{
    public interface IOleDb
    {
        void Conectar(string filaName, bool headers);
        void Desconectar();
        int ExecuteNonQuery(string sql);
        DataSet ExecuteQuery(string sql); 

    }
}
