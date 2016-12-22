using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using worker_WalmartLayoutParser.catalogoProdutoWalmart;
using worker_WalmartLayoutParser.xds_952;

namespace worker_WalmartLayoutParser.map
{
    public class CaracteristicaMap : Map
    {
        public override List<Caracteristica> ParseCaracteristica(Product walmartProduct)
        {
            List<Caracteristica> lstCaracteristica = new List<Caracteristica>();
            lstCaracteristica.Add(new Caracteristica());

            List<CaracteristicaItem> lstCaracteristicaItem = new List<CaracteristicaItem>();
            CaracteristicaItem catacteristica = new CaracteristicaItem();

            if (walmartProduct.attributes != null)
            {
                foreach (worker_WalmartLayoutParser.catalogoProdutoWalmart.Attribute attribute in walmartProduct.attributes)
                {
                    string name = attribute.name.Replace(@"\", "");
                    name = name.Replace(@":", "");
                    name = name.Replace(@".", "");

                    string valueCaracteristica = string.Empty;

                    foreach (string value in attribute.values)
                        valueCaracteristica += string.IsNullOrEmpty(valueCaracteristica) ? value : string.Concat("-", value);

                    CaracteristicaItem catacteristicaInt = new CaracteristicaItem();
                    catacteristicaInt.NOME = name;
                    catacteristicaInt.VALOR = valueCaracteristica;

                    lstCaracteristicaItem.Add(catacteristicaInt);                    
                }
            }

            lstCaracteristica[0].CARACTERISTICA = lstCaracteristicaItem;

            return lstCaracteristica;
        }

        //public override List<Caracteristica> ParseCaracteristica(Product walmartProduct, Sku walmartSku)
        //{
        //    List<Caracteristica> lstCaracteristica = new List<Caracteristica>();
        //    lstCaracteristica.Add(new Caracteristica());

        //    List<CaracteristicaItem> lstCaracteristicaItem = new List<CaracteristicaItem>();
        //    CaracteristicaItem catacteristica = new CaracteristicaItem();

        //    catacteristica.NOME = "MARCA";            
        //    catacteristica.VALOR = walmartProduct.brand?.name;

        //    lstCaracteristicaItem.Add(catacteristica);

        //    if (walmartSku.specializations != null)
        //    {
        //        foreach (Specialization specialization in walmartSku.specializations)
        //        {
        //            foreach (string value in specialization.values)
        //            {
        //                CaracteristicaItem catacteristicaInt = new CaracteristicaItem();                        
        //                catacteristicaInt.NOME = specialization.name;
        //                catacteristicaInt.VALOR = value;

        //                lstCaracteristicaItem.Add(catacteristicaInt);
        //            }
        //        }
        //    }            

        //    lstCaracteristica[0].CARACTERISTICA = lstCaracteristicaItem; 

        //    return lstCaracteristica;
        //}
    }
}
