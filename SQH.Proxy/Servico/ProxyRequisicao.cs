using SQH.Entities.Requisitores;
using SQH.Entities.Response;
using SQH.Proxy.Contrato;
using SQH.Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using SQH.Shared.Enums;

namespace SQH.Proxy.Servico
{
    public class ProxyRequisicao : IProxyRequisicao
    {
        public AutenticacaoResponse ValidaLoginUsuario(LoginRequisitor requisitor)
        {
            AutenticacaoResponse retorno = new AutenticacaoResponse();

            HttpWebRequest _requisitor = null;
            String result = Post(Parametros.ExtremidadeDeAutenticacao, requisitor.ToSerializeBytes(), out _requisitor);

            if (result.Contains("timesheetColaborador"))
            {
                retorno.Autenticado = true;
                retorno.CookieContainer = GetAllCookies(_requisitor.CookieContainer);
            }
            return retorno;
        }

        public String CarregaProjetoMotivos(ProjetoRequisitor requisitor, CookieContainer cookieContainer)
        {
            string result = string.Empty;
            string uri = string.Concat(Parametros.ExtremidadeDeSolicitacaoDeAlteracaoDeHoras, "?data=", DateTime.Parse(requisitor.dataRequisicao).ToString("M/dd/yyyy"));

            // Cria a web request com os parâmetros necessários.
            var _requisitor = CreateWebRequest(uri, "GET", null, cookieContainer);

            using (var resposta = (HttpWebResponse)_requisitor.GetResponse())
            {
                using (var leitor = new StreamReader(resposta.GetResponseStream(), Encoding.GetEncoding("iso-8859-1")))
                {
                    result = leitor.ReadToEnd();
                }
            }
            return result;
        }

        public String CarregaPapeis(PapeisRequisitor requisitor, CookieContainer cookieContainer)
        {
            return Post(Parametros.ExtremidadeDeCarregamentoSubProjetosEPapeis, requisitor.ToSerializeBytes(), cookieContainer);
        }

        public String CarregaSubProjeto(SubProjetoRequisitor requisitor, CookieContainer cookieContainer)
        {
            return Post(Parametros.ExtremidadeDeCarregamentoSubProjetosEPapeis, requisitor.ToSerializeBytes(), cookieContainer);
        }

        public String BuscarRespostaApontamento(ApontamentoRequisitor requisitor, CookieContainer cookieContainer)
        {
            return Post(Parametros.ExtremidadeDeGravacaoDeSolicitacao, requisitor.ToSerializeBytes(), cookieContainer);
        }

        #region Métodos Privados
        private string Post(string uri, byte[] formdata, CookieContainer container = null)
        {
            String result = String.Empty;
            var _requisitor = CreateWebRequest(uri, "POST", formdata, container);

            using (var resposta = (HttpWebResponse)_requisitor.GetResponse())
            {
                using (var leitor = new StreamReader(resposta.GetResponseStream(), Encoding.GetEncoding("iso-8859-1")))
                    result = leitor.ReadToEnd();
            }
            return result;
        }

        private string Post(string uri, byte[] formdata, out HttpWebRequest request)
        {
            String result = String.Empty;
            request = CreateWebRequest(uri, "POST", formdata, null);

            using (var resposta = (HttpWebResponse)request.GetResponse())
            {
                using (var leitor = new StreamReader(resposta.GetResponseStream(), Encoding.GetEncoding("iso-8859-1")))
                    result = leitor.ReadToEnd();
            }
            return result;
        }

        private List<CookieResponse> GetAllCookies(CookieContainer container)
        {
            var allCookies = new List<CookieResponse>();
            var domainTableField = container.GetType().GetRuntimeFields().FirstOrDefault(x => x.Name == "m_domainTable");
            var domains = (IDictionary)domainTableField.GetValue(container);

            foreach (var val in domains.Values)
            {
                var type = val.GetType().GetRuntimeFields().First(x => x.Name == "m_list");
                var values = (IDictionary)type.GetValue(val);
                foreach (CookieCollection cookies in values.Values)
                {
                    foreach (Cookie cookie in cookies)
                    {
                        CookieResponse c = new CookieResponse();
                        c.Domain = cookie.Domain;
                        c.Expires = cookie.Expires;
                        c.Name = cookie.Name;
                        c.Path = cookie.Path;
                        c.TimeStamp = cookie.TimeStamp;
                        c.Value = cookie.Value;
                        allCookies.Add(c);
                    }
                }
            }
            return allCookies;
        }

        private HttpWebRequest CreateWebRequest(string extremidade, string metodo = "GET", byte[] dadosDePostagem = null, CookieContainer container = null)
        {
            var url = string.Concat("http:", Parametros.CaminhoDoServidorSqHoras, extremidade);

            HttpWebRequest _requisitor = WebRequest.Create(url) as HttpWebRequest;
            _requisitor.UserAgent = Parametros.Agente;
            _requisitor.CookieContainer = container == null ? new CookieContainer() : container;
            _requisitor.Method = metodo.ToUpper();
            _requisitor.ContentType = _requisitor.Method.Equals("GET") ? Parametros.TipoDeConteudoGET : Parametros.TipoDeContetudoPOST;

            if (_requisitor.Method.Equals("POST"))
            {
                _requisitor.ContentLength = dadosDePostagem.Length;

                using (var escritor = _requisitor.GetRequestStream())
                {
                    escritor.Write(dadosDePostagem, 0, dadosDePostagem.Length);
                }
            }

            return _requisitor;
        }
        #endregion
    }
}
