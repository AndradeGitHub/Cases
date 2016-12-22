using audatex.br.centralpublisher.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralpublisher.domain.map
{
    public class Mapeador : Operation
    {
        public override object Mapear_PedePedidos01(dynamic Seguradora)
        {
           
            var PedePedidos01 = new
            { 
                CHAMADA = new  
                {
                    CNPJ = Seguradora.CNPJDest,
                    Chamador = Seguradora.Chamador,
                    Perfil = Seguradora.Perfil,
                    Senha = Seguradora.Senha,
                    Serie = Seguradora.Serie,
                    HD = Seguradora.HD,
                    CPF = Seguradora.CPFDest,
                    Perito = Seguradora.Perito,
                    Qtde = Seguradora.Qtde
                }
            };
            return PedePedidos01;
        }
        public override object Mapear_ConfRetorno01(dynamic Seguradora, dynamic ConfRetorno01)
        {
            var objConfRetorno01 = new
            {
                CONFRETORNO01 = new
                {
                    PEDIDOS = new
                    {
                        PEDIDO = new
                        {
                            IDPEDIDO = ConfRetorno01.IdPedido,
                            OPERACAO = ConfRetorno01.Operacao
                        }
                    }
                }
            };
            return objConfRetorno01;
        }
       
        public override object Mapear_PecRetorno01(dynamic Seguradora, dynamic PedidoStatus)
        {
            var objPecRetorno01 = new
            {
                PPECRETORNO01 = new
                {
                    OFICINAS = new
                    {
                        OFICINA = new
                        {
                            CNPJOFI = PedidoStatus.CnpjOficina
                        }
                    },
                    FORNECEDORES = new
                    {
                        FORNECEDOR = new
                        {
                            CNPJFOR = PedidoStatus.CnpjFornecedor
                        }
                    },
                    PEDIDOS = new
                    {
                        PEDIDO = new
                        {
                            IDPEDIDO = PedidoStatus.IdPedido,
                            OPERACAO = PedidoStatus.Operacao
                        }
                    }
                }
            };
            return objPecRetorno01;
        }

        public override object Mapear_ConfPedidos03(dynamic Seguradora, dynamic PecRetorno01)
        {

            var objConfPedidos03 = new
            {
                CONFPEDIDOS03 = new
                {
                    CHAMADA = new
                    {
                        CNPJ = Seguradora.CNPJ,
                        Chamador = Seguradora.Chamador,
                        Perfil = Seguradora.Perfil,
                        Senha = Seguradora.Senha,
                        Serie = Seguradora.Serie,
                        HD = Seguradora.HD,
                        CPF = Seguradora.CPF,
                        Perito = Seguradora.Perito,
                        Qtde = Seguradora.Qtde
                    },
                    DESTINATARIO = new
                    {
                        CNPJ = PecRetorno01.DESTINATARIO.CNPJ,
                        CPF = PecRetorno01.DESTINATARIO.CPF
                    },
                    PEDIDOS = new
                    {
                        PEDIDO = new
                        {
                            IDPEDIDO = PecRetorno01.PEDIDOS.PEDIDO.IDPEDIDO,
                            NUMERO = PecRetorno01.PEDIDOS.PEDIDO.NUMERO,
                            DATAABERTURA = PecRetorno01.PEDIDOS.PEDIDO.DATAABERTURA,
                        }
                    }
                }
            };
            return objConfPedidos03;
        }

    
    }
}
 