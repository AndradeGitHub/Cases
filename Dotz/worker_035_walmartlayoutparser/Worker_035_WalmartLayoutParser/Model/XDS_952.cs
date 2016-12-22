using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace worker_WalmartLayoutParser.xds_952
{    
    public class Catalogo
    {        
        public Produtos PRODUTOS { get; set; }       
    }
    
    public class Produtos
    {        
        public List<Produto> PRODUTO { get; set; }
    }
    
    public class Produto
    {      
        public long? PRODUTOID { get; set; }
        public string NOMEPRODUTO { get; set; }
        public string DESCRICAO { get; set; }
        public string URL { get; set; }
        public List<Palavra> PALAVRASCHAVE { get; set; }
        public List<Referencia> REFERENCIAS { get; set; }
        public string CATEGORIA { get; set; }        
    }
    
    public class Palavra {
        public string DESCRICAO { get; set; }
    }

    public class Referencia
    {
        public List<ReferenciaItem> REFERENCIA;
    }

    public class ReferenciaItem
    {
        public long? PRODUTOIDREFERENCIA;
        public byte ATIVO;
        public float PRECODE;
        public float PRECOPOR;
        public float FRETEMEDIO;
        public byte DISPONIVEL;
        public short SALDO;
        public string TIPOPRODUTO;
        public string CODIGOEAN;
        public List<Caracteristica> CARACTERISTICAS;
        public List<Imagem> IMAGENS;
    }
    
    public class Caracteristica
    {
        public List<CaracteristicaItem> CARACTERISTICA;
    }

    public class CaracteristicaItem
    {
        public string NOME;
        public string VALOR;
    }

    public class Imagem
    {
        public List<ImagemItem> IMAGEM;        
    }

    public class ImagemItem
    {
        public string URL;
        public string PRINCIPAL;
    }   
}
