using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using worker_WalmartLayoutParser.catalogoProdutoWalmart;
using worker_WalmartLayoutParser.xds_952;

using worker_WalmartLayoutParser.infrastructure;
using worker_WalmartLayoutParser.map.interfaces;

namespace worker_WalmartLayoutParser.map
{
    public class ReferenciaMap : Map
    {
        private IMap _caracteristicaMap;
        private IMap _imagemMap;

        public ReferenciaMap()
        {
            _caracteristicaMap = MapFactory.CreateMap<CaracteristicaMap>();
            _imagemMap = MapFactory.CreateMap<ImagemMap>();
        }

        public override List<Referencia> ParseReferencia(Product walmartProduct, Sku walmartSku)
        {
            List<Referencia> lstReferencia = new List<Referencia>();
            lstReferencia.Add(new Referencia());

            List<ReferenciaItem> lstReferenciaItem = new List<ReferenciaItem>();
            ReferenciaItem referencia = new ReferenciaItem();                              
                  
            referencia.PRODUTOIDREFERENCIA = walmartSku.id;
            referencia.ATIVO = !walmartSku.active? Convert.ToByte(walmartSku.active) : byte.MinValue;
            referencia.PRECODE = walmartSku.offers != null ? walmartSku.offers[0].listPrice.BRL : 0;
            referencia.PRECOPOR = walmartSku.offers != null ? walmartSku.offers[0].discountPrice.BRL : 0;
            referencia.FRETEMEDIO = 0;
            referencia.DISPONIVEL = walmartSku.offers != null ? Convert.ToByte(walmartSku.offers[0].available) : byte.MinValue;
            referencia.SALDO = 0;

            if (walmartProduct.attributes != null)
            {
                foreach (var attribute in walmartProduct.attributes)
                {
                    if (attribute.name.ToLower().Equals("tipo"))
                    {
                        foreach (string value in attribute.values)
                            referencia.TIPOPRODUTO += referencia.TIPOPRODUTO == null ? value : string.Concat("-", value);
                    }
                }
            }

            referencia.CODIGOEAN = walmartSku.eans.Count > 0 ? walmartSku.eans[0] : null;
            
            referencia.CARACTERISTICAS = _caracteristicaMap.ParseCaracteristica(walmartProduct);
            
            referencia.IMAGENS = _imagemMap.ParseImagem(walmartSku);

            lstReferenciaItem.Add(referencia);

            lstReferencia[0].REFERENCIA = lstReferenciaItem;

            return lstReferencia;
        }
    }
}
