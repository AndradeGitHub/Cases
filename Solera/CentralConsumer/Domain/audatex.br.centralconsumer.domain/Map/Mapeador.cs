

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.domain.Map
{
   public class Mapeador : Operation
    {
        public override object MapearConfPedidos01(dynamic Pedido)
        {
            MapeadorChamada MapeadorChamada = new MapeadorChamada();
            var ConfPedidos01 = new
            {
                CHAMADA = MapeadorChamada.CriarChamada(Pedido),
                PEDIDOS = new
                {
                    PEDIDO = new
                    {
                        IDPEDIDO = ValidarIdPedido(Pedido),
                    }
                }
            };
            return ConfPedidos01;
        }



        public override object MapearConfPedidos03(dynamic Seguradora, dynamic Pedido)
        {

            var ConfPedidos03 = new
            {
                CHAMADA = new
                {
                    CNPJ = Seguradora.CNPJ ?? "",
                    CHAMADOR = Seguradora.CHAMADOR ?? "",
                    PERFIL = Seguradora.PERFIL ?? "",
                    SENHA = Seguradora.SENHA ?? "",
                    SERIE = Seguradora.SERIE ?? "",
                    HD = Seguradora.HD ?? "",
                    CPF = Seguradora.CPF ?? "",
                    PERITO = Seguradora.PERITO ?? ""

                }, 

                DESTINATARIO = new
                {
                    CNPJ = Seguradora.CNPJDest ?? "",
                    CPF  = Seguradora.CNPJDest ?? ""

                },

                PEDIDOS = new
                {
                    PEDIDO = new
                    {
                        IDPEDIDO = ValidarIdPedido(Pedido),
                        NUMERO = ValidarNumeroPedido(Pedido),
                        DATAABERTURA = ValidarDataAberturaPedido(Pedido)

                    }
                }
            };
            return ConfPedidos03;
        }

        private dynamic ValidarIdPedido(dynamic Pedido)
        {
            if(Pedido.PEDIDOS != null)
            {
                if(Pedido.PEDIDOS.PEDIDO != null)
                {
                    return Pedido.PEDIDOS.PEDIDO.IDPEDIDO ?? "";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }
        private dynamic ValidarNumeroPedido(dynamic Pedido)
        {
            if (Pedido.PEDIDOS != null)
            {
                if (Pedido.PEDIDOS.PEDIDO != null)
                {
                    return Pedido.PEDIDOS.PEDIDO.NUMERO ?? "";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }
        private dynamic ValidarDataAberturaPedido(dynamic Pedido)
        {
            if (Pedido.PEDIDOS != null)
            {
                if (Pedido.PEDIDOS.PEDIDO != null)
                {
                    return Pedido.PEDIDOS.PEDIDO.DATAABERTURA ?? "";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }

        public override object MapearPpecEnviado01(dynamic Pedido)
        {
            MapeadorChamada MapeadorChamada = new MapeadorChamada();
            MapeadorDestinatario MapeadorDestinatario = new MapeadorDestinatario();
            MapeadorOficina MapeadorOficina = new MapeadorOficina();
            MapeadorFornecedor MapeadorFornecedor = new MapeadorFornecedor();
            MapeadorPedido MapeadorPedido = new MapeadorPedido();

            var PpecEnviado01 = new
            {
                CHAMADA = MapeadorChamada.CriarChamada(Pedido),
                DESTINATARIO = MapeadorDestinatario.CriarDestinatario(Pedido),
                OFICINAS = new
                {
                    OFICINA = MapeadorOficina.CriarOficina(Pedido),
                },
                FORNECEDORES = new
                {
                    FORNECEDOR = MapeadorFornecedor.CriarFornecedor(Pedido)
                },
                PEDIDOS = new
                {
                    PEDIDO = MapeadorPedido.CriarPedido(Pedido)
                }
            };
            return PpecEnviado01;
        }
    }
}
