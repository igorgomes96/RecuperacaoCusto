using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Implementations;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Repository.Implementations;
using RecuperacaoCustoAPI.Repository.Interfaces;
using RecuperacaoCustoAPI.Services.Implementations;
using RecuperacaoCustoAPI.Services.Interfaces;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace RecuperacaoCustoAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();


            container.RegisterType<DbContext, Contexto>();

            //Mappers
            container.RegisterType<ISingleMapper<CategoriaCR, CategoriaCRDTO>, CategoriaCRMapper>();
            container.RegisterType<ISingleMapper<TransferenciaReceita, TransferenciaReceitaDTO>, TransferenciaReceitaMapper>();
            container.RegisterType<ISingleMapper<CR, CRDTO>, CRMapper>();
            container.RegisterType<ISingleMapper<Usuario, UsuarioDTO>, UsuarioMapper>();
            container.RegisterType<ISingleMapper<RecuperacaoCusto, RecuperacaoCustoDTO>, RecuperacaoCustoMapper>();
            container.RegisterType<ISingleMapper<MesCiclo, MesCicloDTO>, MesCicloMapper>();
            container.RegisterType<ISingleMapper<MesBloqueadoTransfReceita, MesBloqueadoTransfReceitaDTO>, MesBloqueadoTransfReceitaMapper>();
            container.RegisterType<ISingleMapper<RecuperacaoCustoMes, RecuperacaoCustoMesDTO>, RecuperacaoCustoMesMapper>();
            container.RegisterType(typeof(IMapper<,>), typeof(Mapper<,>));

            //Repository
            container.RegisterType<IRelatorioTransfReceitaRepository, RelatorioTransfReceitaRepository>();
            container.RegisterType(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
            container.RegisterType<IAuthRepository, AuthRepository>();

            //Services
            container.RegisterType<IOleDb, OleDb>();
            container.RegisterType<ITransferenciaReceitaService, TransferenciaReceitaService>();
            container.RegisterType<IUsuariosService, UsuariosService>();
            container.RegisterType<IMesesBloqueadosService, MesesBloqueadosService>();
            container.RegisterType<ICRService, CRService>();
            container.RegisterType<ICategoriaCRService, CategoriaCRService>();
            container.RegisterType<IRecuperacoesService, RecuperacoesService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}