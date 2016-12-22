
using audatex.br.centralconsumer.infrastructure.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.domain.Map
{
    public class MapeadorChamada
    {
        public object CriarChamada(dynamic Pedido)
        {
            var CHAMADA = new
            {
                CNPJ = Pedido.CHAMADA.CNPJ ?? "",
                CHAMADOR = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.CHAMADA.CHAMADOR ?? ""),
                PERFIL = Pedido.CHAMADA.PERFIL ?? "",
                SENHA = Pedido.CHAMADA.SENHA ?? "",
                SERIE = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.CHAMADA.SERIE ?? ""),
                HD = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.CHAMADA.HD ?? ""),
                CPF = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.CHAMADA.CPF ?? ""),
                PERITO = TratarCaracteres.RemoverCaracteresEspeciais(Pedido.CHAMADA.PERITO ?? "")
            };

            return CHAMADA;
        }
    }
}
