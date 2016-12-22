
using audatex.br.centralconsumer.infrastructure.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.domain.Map
{
    public class MapeadorOficina
    {
        public object CriarOficina(dynamic Pedido)
        {
            var OFICINA = new
            {
                PESSOA = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.PESSOA ?? ""),
                CGC = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.CGC.ToString() ?? ""),
                CPF = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.CGC.ToString() ?? ""),
                APELIDO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.APELIDO.ToString() ?? ""),
                NOME = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.NOME.ToString() ?? ""),
                ENDERECO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.ENDERECO.ToString() ?? ""),
                NUMERO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.NUMERO.ToString() ?? ""),
                BAIRRO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.BAIRRO.ToString() ?? ""),
                CIDADE = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.CIDADE.ToString() ?? ""),
                UF = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.UF.ToString() ?? ""),
                CEP = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.CEP.ToString() ?? ""),
                TELEFONES = new
                {
                    DDD1 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.TELEFONES.DDD1 ?? ""),
                    NUMERO1 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.TELEFONES.NUMERO1 ?? ""),
                    RAMAL1 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.TELEFONES.RAMAL1 ?? ""),

                    DDD2 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.TELEFONES.DDD2 ?? ""),
                    NUMERO2 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.TELEFONES.NUMERO2 ?? ""),
                    RAMAL2 = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.TELEFONES.RAMAL2 ?? "")
                },
                FAX = new
                {
                    DDD = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.FAX.DDD ?? ""),
                    NUMERO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.FAX.NUMERO ?? ""),
                    RAMAL = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.FAX.RAMAL ?? "")
                },
                IE = new
                {
                    CONTATO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.IE.CONTATO ?? ""),
                    REFERENCIA = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.IE.REFERENCIA ?? "")
                },
                IM = new
                {
                    CONTATO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.IM.CONTATO ?? ""),
                    REFERENCIA = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.OFICINAS.OFICINA.IM.REFERENCIA ?? "")
                },

            };

            return OFICINA;
        }
    }
}
