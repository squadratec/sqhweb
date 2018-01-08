using HtmlAgilityPack;
using Newtonsoft.Json;
using SQH.Business.Contrato;
using SQH.Entities.Requisitores;
using SQH.Entities.Response;
using SQH.Proxy.Contrato;
using SQH.Shared.Enums;
using SQH.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace SQH.Business.Servico
{
    public class Requisicao : IRequisicao
    {
        private IProxyRequisicao repository;

        public Requisicao(IProxyRequisicao _repo)
        {
            repository = _repo;
        }

        public AuthRetorno ValidaLoginUsuario(LoginRequisitor requisitor)
        {
            AuthRetorno retorno = new AuthRetorno();
            AutenticacaoResponse response = repository.ValidaLoginUsuario(requisitor);

            retorno.authenticated = response.Autenticado;

            if (response.Autenticado)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                   new GenericIdentity(requisitor.usuario, "Login"),
                   new[] {
                        new Claim("CookieContainer", JsonConvert.SerializeObject(response.CookieContainer), ClaimValueTypes.String, "")
                   }
               );

                retorno.Claims = identity;
                retorno.message = "OK";
            }
            else
            {
                retorno.message = "Usuário e/ou senha não encontrados.";
            }

            return retorno;
        }

        public SubProjetoResponse CarregaSubProjeto(SubProjetoRequisitor requisitor, CookieContainer cookieContainer)
        {
            SubProjetoResponse retorno = new SubProjetoResponse();
            HtmlDocument doc = new HtmlDocument();
            HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("option");

            if (String.IsNullOrEmpty(requisitor.opcao))
                requisitor.opcao = "1";

            requisitor.data = DateTime.Parse(requisitor.data).ToString("M/dd/yyyy");

            string HTML = repository.CarregaSubProjeto(requisitor, cookieContainer);

            if (!String.IsNullOrEmpty(HTML))
            {
                doc.LoadHtml(HTML);

                retorno.subprojetos = doc.ToListItem(Parametros.FiltroSeletorDeSubProjetos);

                if (retorno.subprojetos.Count == 0)
                    retorno.msg = "Não existe subprojeto para apropriar para esta data.";
            }
            return retorno;
        }

        public List<ApontamentoResponse> BuscarRespostaApontamento(ApontamentoRequisitor requisitor, IEnumerable<DateTime> datasApontamento, CookieContainer cookieContainer)
        {
            var listaResposta = new List<ApontamentoResponse>();

            datasApontamento.ToList().ForEach(data =>
            {
                requisitor.ARRAY_SOLICITACAO = String.Format(Parametros.ArraySolicitacao,
                                                             data.ToString("M/dd/yyyy"),
                                                             "08:00",
                                                             "12:00",
                                                             "14:00",
                                                             "18:00");
                ApontamentoResponse response = new ApontamentoResponse();
                response.data = data;

                HtmlDocument doc = new HtmlDocument();
                string HTML = repository.BuscarRespostaApontamento(requisitor, cookieContainer);

                if (!String.IsNullOrEmpty(HTML))
                {
                    doc.LoadHtml(HTML);

                    var falha = doc.DocumentNode.SelectSingleNode(Parametros.FiltroErroRespostaApontamento);

                    if (falha != null)
                    {
                        string msg = falha.Attributes["value"].Value.Trim();

                        if (!String.IsNullOrEmpty(msg))
                        {
                            response.mensagem = msg;
                            response.sucesso = false;
                        }
                    }
                    else
                    {
                        var sucesso = doc.DocumentNode.SelectSingleNode(Parametros.FiltroMensagemRespostaApontamento);

                        if (sucesso != null)
                        {
                            string msg = sucesso.Attributes["value"].Value.Trim();

                            if (!String.IsNullOrEmpty(msg))
                            {
                                response.mensagem = msg;
                                response.sucesso = true;
                            }
                        }
                    }
                }

                listaResposta.Add(response);
            });
           
            return listaResposta;
        }

        public PapelResponse CarregarPapeis(PapeisRequisitor requisitor, CookieContainer cookieContainer)
        {
            PapelResponse retorno = new PapelResponse();
            HtmlDocument doc = new HtmlDocument();
            HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("option");

            if (String.IsNullOrEmpty(requisitor.opcao))
                requisitor.opcao = "2";

            requisitor.data = DateTime.Parse(requisitor.data).ToString("M/dd/yyyy");

            string HTML = repository.CarregaPapeis(requisitor, cookieContainer);

            if (!String.IsNullOrEmpty(HTML))
            {
                doc.LoadHtml(HTML);

                retorno.papeis = doc.ToListItem(Parametros.FiltroSeletorDePapeis);
            }
            return retorno;
        }

        public ProjetoResponse CarregaProjetoMotivos(ProjetoRequisitor requisitor, CookieContainer cookieContainer)
        {
            HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("option");

            ProjetoResponse retorno = new ProjetoResponse();
            HtmlDocument doc = new HtmlDocument();
            string HTML = repository.CarregaProjetoMotivos(requisitor, cookieContainer);


            if (!String.IsNullOrEmpty(HTML))
            {
                doc.LoadHtml(HTML);

                retorno.idColaborador = Convert.ToInt32(doc.DocumentNode.SelectSingleNode(Parametros.FiltroSeletorIdDoColaborador).Attributes["value"].Value);
                retorno.projetos = doc.ToListItem(Parametros.FiltroSeletorDeProjetos);
                retorno.motivos = doc.ToListItem(Parametros.FiltroSeletorDeMotivos);
            }
            return retorno;
        }
    }
}
