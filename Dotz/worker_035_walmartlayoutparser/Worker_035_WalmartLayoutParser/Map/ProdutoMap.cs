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
    public class ProdutoMap : Map
    {
        private IMap _referenciaFactoryMap;
                
        public ProdutoMap()
        {
            _referenciaFactoryMap = MapFactory.CreateMap<ReferenciaMap>();
        }

        public override void ParseProduto(ref Catalogo catalogoXML952, List<Product> objJsonWalmartProdutcs)
        {            
            catalogoXML952.PRODUTOS = new Produtos();            
            catalogoXML952.PRODUTOS.PRODUTO = new List<Produto>();

            foreach (Product walmartProduct in objJsonWalmartProdutcs)
            {
                if (walmartProduct.skus != null)
                {
                    foreach (Sku wallmartSku in walmartProduct.skus)
                    {
                        Produto produto = new Produto();

                        produto.PRODUTOID = walmartProduct.productId;
                        produto.NOMEPRODUTO = walmartProduct.name;
                        produto.DESCRICAO = walmartProduct.description;
                        produto.URL = null;
                        produto.PALAVRASCHAVE = null;
                        produto.REFERENCIAS = _referenciaFactoryMap.ParseReferencia(walmartProduct, wallmartSku);

                        if (walmartProduct.categories != null)
                        {
                            foreach (Category category in walmartProduct.categories)
                                produto.CATEGORIA += produto.CATEGORIA == null ? category.name : string.Concat(" / ", category.name);
                        }
                        else
                            produto.CATEGORIA = null;

                        catalogoXML952.PRODUTOS.PRODUTO.Add(produto);
                    }
                }
            }        
        }
    }
}
