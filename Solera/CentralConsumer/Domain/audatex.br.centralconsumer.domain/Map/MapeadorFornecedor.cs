
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.domain.Map
{
    public class MapeadorFornecedor
    {
        public object CriarFornecedor(dynamic Pedido)
        {
            var FORNECEDOR = new
            {
                CNPJ = "02144891000185",
                CODIGO = 0,
                TIPO = "",
                APELIDO = "AUDATEX",
                NOME = "AUDATEX",
                ENDERECO = "",
                BAIRRO = "",
                CEP = "",
                CIDADE = "",
                UF = "",
                TELEFONES = new
                {
                    DDD1 = "",
                    NUMERO1 = "",
                    RAMAL1 = "",

                    DDD2 = "",
                    NUMERO2 = "",
                    RAMAL2 = ""
                },
                FAX = new
                {
                    DDD = "",
                    NUMERO = "",
                    RAMAL = ""
                },
                IE = "",
                IM = "",
                CONTATO = "",
                EMAIL = ""
            };

            return FORNECEDOR;
        }
    }
}
