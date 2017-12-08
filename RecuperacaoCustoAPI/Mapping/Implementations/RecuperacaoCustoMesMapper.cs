using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Mapping.Implementations
{
    public class RecuperacaoCustoMesMapper : ISingleMapper<RecuperacaoCustoMes, RecuperacaoCustoMesDTO>
    {
        private readonly IMapper<MesCiclo, MesCicloDTO> _mesMapper;

        public RecuperacaoCustoMesMapper(IMapper<MesCiclo, MesCicloDTO> mesMapper)
        {
            _mesMapper = mesMapper;
        }

        public RecuperacaoCustoMesDTO Map(RecuperacaoCustoMes source)
        {

            return source == null ? null : new RecuperacaoCustoMesDTO
            {
                CodRecuperacao = source.CodRecuperacao,
                CodMesCiclo = source.CodMesCiclo,
                Valor = source.Valor,
                Mes = _mesMapper.Map(source.MesCiclo)
            };
        }

        public RecuperacaoCustoMes Map(RecuperacaoCustoMesDTO destination)
        {
            return destination == null ? null : new RecuperacaoCustoMes
            {
                CodRecuperacao = destination.CodRecuperacao,
                CodMesCiclo = destination.CodMesCiclo,
                Valor = destination.Valor
            };
        }
    }
}