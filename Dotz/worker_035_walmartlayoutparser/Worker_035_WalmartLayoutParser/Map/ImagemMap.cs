using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using worker_WalmartLayoutParser.catalogoProdutoWalmart;
using worker_WalmartLayoutParser.xds_952;

using worker_WalmartLayoutParser.map.interfaces;

namespace worker_WalmartLayoutParser.map
{
    public class ImagemMap : Map
    {
        public override List<Imagem> ParseImagem(Sku walmartSku)
        {            
            List<Imagem> lstImagem = new List<Imagem>();
            lstImagem.Add(new Imagem());

            List<ImagemItem> lstImagemItem = new List<ImagemItem>();
            ImagemItem imagem = new ImagemItem();

            if (walmartSku.images != null)
            {
                int i = 0;
                foreach (Image walmarSkuImage in walmartSku.images)
                {
                    ImagemItem imagemItem = new ImagemItem();
                    imagemItem.URL = walmarSkuImage.url;

                    if (i.Equals(0))
                        imagemItem.PRINCIPAL = "TRUE";

                    lstImagemItem.Add(imagemItem);

                    i++;
                }
            }                                                

            lstImagem[0].IMAGEM = lstImagemItem;

            return lstImagem;
        }
    }
}
