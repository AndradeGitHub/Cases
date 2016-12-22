using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using worker_WalmartLayoutParser.catalogoProdutoWalmart;
using worker_WalmartLayoutParser.xds_952;

namespace worker_WalmartLayoutParser.map.interfaces
{
    public interface IMap
    {
        Catalogo ParseCatalogo(RootObject objJsonWalmart);
        void ParseProduto(ref Catalogo catalogoXML952, List<Product> objJsonWalmartProdutcs);
        List<Referencia> ParseReferencia(Product walmartProduct, Sku walmartSku);
        List<Caracteristica> ParseCaracteristica(Product walmartProduct);
        List<Imagem> ParseImagem(Sku walmartSku);
    }
}
