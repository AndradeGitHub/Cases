using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using RestSharp;

using Dotz.Core.ExternalServices.SupplierIntegration.AbstractFactory;
using Dotz.Core.ExternalServices.SupplierIntegration.AbstractFactory.Views;
using Dotz.Core.ExternalServices.SupplierIntegration.Supplier.Walmart.Objects;

using worker_WalmartLayoutParser.infrastructure;
using worker_WalmartLayoutParser.catalogoProdutoWalmart;

namespace worker_WalmartLayoutParser.consumer
{
    internal class SupplierWalmart : ISupplierIntegration
    {
        public event Action<string, string, string> HttpErro;
        private readonly string baseUrl;

        public SupplierWalmart(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {            
            var client = new RestClient();
            client.BaseUrl = new Uri(this.baseUrl);
            client.AddDefaultHeader("content-Type", "application/json");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
            (sender, certificate, chain, sslPolicyErrors) => true;

            var response = client.Execute<T>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (this.HttpErro != null)
                {
                    #region Levanta Evento Erro Http
                    var codigoErro = response.StatusCode.ToString();
                    var descricaoErro = response.StatusDescription;
                    var dadoTransacao = response.Data.ToString();

                    this.HttpErro(codigoErro, descricaoErro, dadoTransacao);
                    #endregion
                }
            }

            if (response.ErrorException != null)
            {
                #region Levanta Exception Erro
                string message = "Erro. Verificar detalhes.";

                var walmartApiException = new ApplicationException(message, response.ErrorException);

                throw walmartApiException;
                #endregion
            }

            return response.Data;
        }

        public ResulltAutenticarVw Autenticar(string clientId, string clientSecret, string grantType)
        {
            var request = new RestRequest()
            {
                Resource = "/authenticate",
                RequestFormat = DataFormat.Json,
                Method = Method.POST,
                RootElement = "Authentication"
            };

            request.AddJsonBody(
                new
                {
                    client_id = clientId,
                    client_secret = clientSecret,
                    grant_type = grantType
                }
            );

            var response = Execute<WalmartAuthenticateResponse>(request);

            var resultVw = new ResulltAutenticarVw();

            resultVw.AccessToken = response.AccessToken;
            resultVw.ExpiresIn = response.ExpiresIn;

            this.HttpErro += (codigo, erro, dadoTransacao) => {

                resultVw.InfoErro.Codigo = codigo;
                resultVw.InfoErro.Descricao = erro;
            };

            return resultVw;
        }
        
        public byte[] GetCatalog(string access_token, string tipoCatalogo, int qtCatalogConsumer, ref int tentativas)
        {                      
            var client = new RestClient();
            client.BaseUrl = new Uri(this.baseUrl);            

            var request = new RestRequest()
            {
                Resource = string.Concat("/catalogs/", tipoCatalogo), /*partial ou full*/
                RequestFormat = DataFormat.Json,
                Method = Method.GET                
            };

            request.AddHeader("Authorization", access_token);                        
            request.AddHeader("Accept", "application/zip");

            var ret = new byte[byte.MaxValue];
            for (int i=1; i < qtCatalogConsumer; i++)
            {
                tentativas = i;

                ret = client.DownloadData(request);
                if (ret != null)
                    return ret;
            }

            return ret;
        }

        public ResultConfirmaPedidoVw ConfirmarPedido(int pedidoId, string sku)
        {
            throw new NotImplementedException();
        }

        public ResultConsultaDisponibilidadeVw ConsultarDisponibilidade(string sku, DetalheProdutoVw detalheProduto = null)
        {
            throw new NotImplementedException();
        }

        public ResultConsultaFreteVw ConsultarFrete(int pedidoId)
        {

            throw new NotImplementedException();
        }

        public ResultConsultaPedidoVw ConsultarPedido(int pedidoId)
        {
            throw new NotImplementedException();
        }

        public ResultConsultaProdutoVw ConsultarProduto(string sku)
        {
            throw new NotImplementedException();
        }

        public ResultConsultaTrackingVw ConsultarTracking(string nroPedidoFornecedor)
        {
            throw new NotImplementedException();
        }

        public ResultCriarPedidoVw CriarPedido(DadosCriacaoPedidoVw dadosCriacaoPedido)
        {
            throw new NotImplementedException();
        }

        public ResultReservaPedidoVw ReservarPedido(DadosCriacaoPedidoVw dadosCriacaoPedido)
        {
            throw new NotImplementedException();
        }
    }
}
