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

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<DbContext, Contexto>();
            container.RegisterType<ISingleMapper<TransferenciaReceita, TransferenciaReceitaDTO>, TransferenciaReceitaMapper>();
            container.RegisterType<ISingleMapper<CR, CRDTO>, CRMapper>();
            container.RegisterType(typeof(IMapper<,>), typeof(Mapper<,>));
            container.RegisterType(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
            container.RegisterType<ITransferenciaReceitaService, TransferenciaReceitaService>();
            container.RegisterType<ICRService, CRService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}