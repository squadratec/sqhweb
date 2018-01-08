using SQH.Entities.Requisitores;
using SQH.Entities.Response;
using System;
using System.Collections.Generic;
using System.Net;

namespace SQH.Business.Contrato
{
    public interface IRequisicao
    {
        /// <summary>
        /// Método responsável por autenticar o usuário no SQHoras.
        /// </summary>
        /// <param name="requisitor">Dados da requisição.</param>
        /// <returns>Retorna o resultado com o token válido.</returns>
        AuthRetorno ValidaLoginUsuario(LoginRequisitor requisitor);

        /// <summary>
        /// Carrega os dados de projeto e motivos.
        /// </summary>
        /// <param name="requisitor">Dados da requisição.</param>
        /// <param name="cookieContainer">Container da primeira requisição.</param>
        /// <returns>Retorna html com os dados da requisição.</returns>
        ProjetoResponse CarregaProjetoMotivos(ProjetoRequisitor requisitor, CookieContainer cookieContainer);

        /// <summary>
        /// Método responsável por carregar os dados de sub projetos.
        /// </summary>
        /// <param name="requisitor">Dados do requisitor.</param>
        /// <param name="cookieContainer">Cookie container.</param>
        /// <returns>Retorna uma lista de subprojetos.</returns>
        SubProjetoResponse CarregaSubProjeto(SubProjetoRequisitor requisitor, CookieContainer cookieContainer);

        /// <summary>
        /// Método responsável por carregar os dados de papeis.
        /// </summary>
        /// <param name="requisitor">Dados do requisitor.</param>
        /// <param name="cookieContainer">Cookie container.</param>
        /// <returns>Retorna uma lista de papeis.</returns>
        PapelResponse CarregarPapeis(PapeisRequisitor requisitor, CookieContainer cookieContainer);

        /// <summary>
        /// Método responsável por realizar apontamento de horas.
        /// </summary>
        /// <param name="requisitor">Dados do requisitor.</param>
        /// <param name="datasApontamento">Datas do apontamento</param>
        /// <param name="cookieContainer">Cookie container.</param>
        /// <returns>Retorna resultado do apontamento.</returns>
        List<ApontamentoResponse> BuscarRespostaApontamento(ApontamentoRequisitor requisitor, IEnumerable<DateTime> datasApontamento, CookieContainer cookieContainer);
    }
}
