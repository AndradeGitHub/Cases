
using audatex.br.centralconsumer.infrastructure.common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.domain.Map
{
    public class MapeadorPedido
    {
        public object CriarPedido(dynamic Pedido)
        {

            if (Pedido.PEDIDOS.PEDIDO != null)
            {

                var PEDIDO = new
                {
                    IDPEDIDO = Pedido.PEDIDOS.PEDIDO.IDPEDIDO ?? "",
                    NUMERO = Pedido.PEDIDOS.PEDIDO.NUMERO ?? "",
                    DATAABERTURA = Pedido.PEDIDOS.PEDIDO.DATAABERTURA ?? "",
                    ORCAMENTO = CriarOrcamento(Pedido),
                    CGCS = CriarCGC(Pedido),
                    CLIENTE = CriarCliente(Pedido),
                    VEICULO = CriarVeiculo(Pedido),
                    ITENSPEDIDO = new
                    {
                        ITEMPEDIDO = (IEnumerable<dynamic>)CriarItemPedido(Pedido)
                    }
                };
                return PEDIDO;
            }
            else
                return null;
        }

        private object CriarCliente(dynamic Pedido)
        {
            var CLIENTE = new
            {
                NOME = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.NOME ?? ""),
                TIPO = ValidarTipoCliente(Pedido.PEDIDOS.PEDIDO.TIPO ?? ""),
                CGC = ValidarTipoCliente(TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.CPF ?? "")),
                CPF = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.CPF ?? ""),
                ENDERECO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.ENDERECO ?? ""),
                BAIRRO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.BAIRRO ?? ""),
                CEP = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.CEP ?? ""), 
                CIDADE = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.CIDADE ?? ""),
                UF = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.UF ?? ""),
                TELEFONES = new
                {
                    DDD1 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.DDD1 ?? ""),
                    NUMERO1 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.NUMERO1 ?? ""),
                    RAMAL1 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.RAMAL1 ?? ""),

                    DDD2 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.DDD2 ?? ""),
                    NUMERO2 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.NUMERO2 ?? ""),
                    RAMAL2 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.RAMAL2 ?? "")
                }
            };
            return CLIENTE;
        }
        private object CriarVeiculo(dynamic Pedido)
        {
            var VEICULO = new
            {
                PLACA = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.PLACA ?? ""),
                DESCRICAO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.DESCRICAO ?? ""),
                COR = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.COR ?? ""),
                KM = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.KM ?? "0"),
                OBS_VEIC = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.OBS_VEIC ?? ""),
                COD_FUN = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.COD_FUN ?? ""),
                COD_MEC = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.COD_MEC ?? ""),
                COD_FMC = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.COD_FMC ?? ""),
                ANOMODELO = ValidarAnoModelo(TratarCaracteres.RemoverCaracteresEspeciais(Pedido.PEDIDOS.PEDIDO.VEICULO.ANOMODELO ?? "")) //<--verificar
            };
            return VEICULO;
        }     
        private object CriarCGC(dynamic Pedido)
        {
            var CGCS = new
            {
                OFICINA = Pedido.PEDIDOS.PEDIDO.CGCS.OFICINA ?? "",
                SEGURADORA = Pedido.PEDIDOS.PEDIDO.CGCS.SEGURADORA ?? "",
                SUCURSAL = ValidarSucursal(Pedido.PEDIDOS.PEDIDO.CGCS.SUCURSAL.ToString(), Pedido.PEDIDOS.PEDIDO.CGCS.REGULADORA.ToString()),
                REGULADORA = Pedido.PEDIDOS.PEDIDO.CGCS.REGULADORA ?? "",
                MEDIADORA = Pedido.PEDIDOS.PEDIDO.CGCS.MEDIADORA ?? ""
            };
            return CGCS;
        }
        private object CriarOrcamento(dynamic Pedido)
        {
            var ORCAMENTO = new
            {
                NUMERO = Pedido.PEDIDOS.PEDIDO.ORCAMENTO.NUMERO ?? "",
                COMPLEMENTO = "0",
                VERSAO = Pedido.PEDIDOS.PEDIDO.ORCAMENTO.VERSAO != null ? Pedido.PEDIDOS.PEDIDO.ORCAMENTO.VERSAO : 0,
                CONCLUSAO = "0",
                SINISTRO = Pedido.PEDIDOS.PEDIDO.ORCAMENTO.SINISTRO ?? "",
                APOLICE = Pedido.PEDIDOS.PEDIDO.ORCAMENTO.APOLICE ?? "",
                DATAVISTORIA = Pedido.PEDIDOS.PEDIDO.ORCAMENTO.DATAVISTORIA
            };
            return ORCAMENTO;
        }
        private dynamic CriarItemPedido(dynamic Pedido)
        {
            List<dynamic> ItensPedido = new List<dynamic>();

                foreach(var item in Pedido.PEDIDOS.PEDIDO.ITENSPEDIDO.ITEMPEDIDO)
                {
                    
                    ItensPedido.Add(new
                    {

                        CODIGOPEDIDO = item.CODIGOPEDIDO ?? "",
                        CODIGO = item.CODIGO ?? "",
                        CODIGOCORR = item.CODIGOCORR ?? "",
                        COD_PAI = item.COD_PAI ?? "",
                        DESCRICAO = item.NOME ?? "",
                        QTDE = item.QTDE,
                        PRECO_CON = item.PRECO_CON,
                        PRECO_ALT = item.PRECO_ALT,
                        CNPJ = item.CGC_FOR ?? "",
                        FLAG_PRECO = ValidarFlagPreco(item.FLAG_PRECOC, item.FLAG_PRECOA),
                        NUM_SEQ = item.NUMSEQ,
                        STATUSITEMPEDIDO = item.STATUSITEMPEDIDO,
                        DT_PREVISAO = item.DT_PREVISAO,
                        DT_ENTREGA = item.DT_ENTREGA,
                        DT_FATURAMENTO = item.DT_FATURAMENTO,
                        CODTIPOPECA = item.CODTIPOPECA ?? ""
                    });
                }              
                   
            return ItensPedido;
        }

        public string ValidarFlagPreco(string Flag_PrecoC, string Flag_PrecoA)
        {
            if (!string.IsNullOrEmpty(Flag_PrecoC))
                if (Flag_PrecoC.Equals("T"))
                    return "C";
            if (!string.IsNullOrEmpty(Flag_PrecoA))
                if (Flag_PrecoA.Equals("T"))
                    return "A";
            return "";
        }
        public string ValidarTipoCliente(string Valor)
        {
            if (string.IsNullOrEmpty(Valor))
                return "";

            if (Valor.Length == 11)
                return "F";
            else if (Valor.Length == 14)
                return "J";
            else
                return "";
        }
        public int ValidarAnoModelo(string Valor)
        {
            if (!string.IsNullOrEmpty(Valor) && Valor.Trim().Length > 0)
                return 1920 + Convert.ToInt32(Valor.ToCharArray()[0]);
            else
                return 0;
        }
        public string ValidarSucursal(string CGCSucursal, string CGCSeguradora)
        {
            string CGC = "";
            if (!string.IsNullOrEmpty(CGCSucursal))
            {
                CGC = CGCSucursal;
            }
            else
            {
                if(CGCSucursal == "" || CGCSucursal == "11111111111111" || CGCSucursal == "22222222222222" || CGCSucursal == "33333333333333")
                {
                    CGC = CGCSeguradora;
                }                
            }
            return CGC;
        } 
    }
}
