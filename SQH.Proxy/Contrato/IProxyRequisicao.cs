using SQH.Entities.Requisitores;
using SQH.Entities.Response;
using System;
using System.Net;

namespace SQH.Proxy.Contrato
{
    public interface IProxyRequisicao
    {
        /// <summary>
        /// Método responsável por autenticar o usuário no SQHoras.
        /// </summary>
        /// <param name="requisitor">Dados da requisição.</param>
        /// <returns>Retorna se usuário autenticado ou não.</returns>
        AutenticacaoResponse ValidaLoginUsuario(LoginRequisitor requisitor);

        /// <summary>
        /// Carrega os dados de projeto e motivos.
        /// </summary>
        /// <param name="requisitor">Dados da requisição.</param>
        /// <param name="cookieContainer">Cookie container.</param>
        /// <returns>Retorna html com os dados da requisição.</returns>
        String CarregaProjetoMotivos(ProjetoRequisitor requisitor, CookieContainer cookieContainer);


        /// <summary>
        /// Método responsável por carregar os dados de sub projetos.
        /// </summary>
        /// <param name="requisitor">Dados do requisitor.</param>
        /// <param name="cookieContainer">Cookie container.</param>
        /// <returns>Retorna uma lista de subprojetos.</returns>
        String CarregaSubProjeto(SubProjetoRequisitor requisitor, CookieContainer cookieContainer);

        /// <summary>
        /// Método responsável por carregar os dados de papel.
        /// </summary>
        /// <param name="requisitor">Dados do requisitor.</param>
        /// <param name="cookieContainer">Cookie container.</param>
        /// <returns>Retorna uma lista de papeis.</returns>
        String CarregaPapeis(PapeisRequisitor requisitor, CookieContainer cookieContainer);

        /// <summary>
        /// Método responsável por enviar apontamento de horas.
        /// </summary>
        /// <param name="requisitor">Dados do requisitor.</param>
        /// <param name="cookieContainer">Cookie container.</param>
        /// <returns>Retorna resposta do apontamento.</returns>
        String BuscarRespostaApontamento(ApontamentoRequisitor requisitor, CookieContainer cookieContainer);
    }
}
