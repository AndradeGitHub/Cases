using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using worker_WalmartLayoutParser.catalogoProdutoWalmart;
using worker_WalmartLayoutParser.xds_952;

using worker_WalmartLayoutParser.map.interfaces;

namespace worker_WalmartLayoutParser.map
{
    public class Map : IMap
    {
        public virtual Catalogo ParseCatalogo(RootObject objJsonWalmart)
        {
            throw new NotImplementedException();
        }

        public virtual void ParseProduto(ref Catalogo catalogoXML952, List<Product> objJsonWalmartProdutcs)      
        {
            throw new NotImplementedException();
        }

        public virtual List<Referencia> ParseReferencia(Product walmartProduct, Sku walmartSku)
        {
            throw new NotImplementedException();
        }

        public virtual List<Caracteristica> ParseCaracteristica(Product walmartProduct)
        {
            throw new NotImplementedException();
        }

        public virtual List<Imagem> ParseImagem(Sku walmartSku)
        {
            throw new NotImplementedException();
        }
    }
}
