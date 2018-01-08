using Microsoft.Extensions.DependencyInjection;
using SQH.Business.Contrato;
using SQH.Business.Servico;
using SQH.Proxy.Contrato;
using SQH.Proxy.Servico;

namespace SQH.IoC
{
    public class Injector
    {
        public static void ConfigureIoC(IServiceCollection services)
        {
            services.AddSingleton<IRequisicao, Requisicao>();
            services.AddSingleton<IProxyRequisicao, ProxyRequisicao>();
        }
    }
}
