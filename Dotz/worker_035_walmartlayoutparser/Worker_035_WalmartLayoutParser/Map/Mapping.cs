using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

using worker_WalmartLayoutParser.catalogoProdutoWalmart;
using worker_WalmartLayoutParser.xds_952;

using worker_WalmartLayoutParser.infrastructure;
using worker_WalmartLayoutParser.map.interfaces;

namespace worker_WalmartLayoutParser.map
{
    public class Mapping : Map
    {        
        private Catalogo _catalogoXML952;
        private IMap _produtoMap;

        public Mapping()
        {            
            _catalogoXML952 = new Catalogo();

            _produtoMap = MapFactory.CreateMap<ProdutoMap>();
        }

        public override Catalogo ParseCatalogo(RootObject objJsonWalmart)
        {            
            if (objJsonWalmart?.products != null)
                _produtoMap.ParseProduto(ref _catalogoXML952, objJsonWalmart.products);                                                      

            return _catalogoXML952;
        }       
    }
}
