using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace ubs.brasil.qualitycontrol.comum.globals
{
    public static class Utils
    {
        //Retorna o Description do Enum de Tipo de Filtro
        public static string GetEnumDescriptionTipoFiltro(int value)
        {            
            string descricao = string.Empty;            
            TiposDeFiltro en = (TiposDeFiltro)value;            
            FieldInfo fi = en.GetType().GetField(en.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            
            if (attributes.Length > 0)                
                descricao = attributes[0].Description;
            else                
                descricao = Enum.GetName(typeof(TiposDeFiltro), value);

            return descricao;        
        }

        //Retorna o Description do Enum de Log Carga Desc Ordem
        public static string GetEnumDescriptionLogCargaOrdem(int value)
        {
            string descricao = string.Empty;
            LogCargaDescOrdem en = (LogCargaDescOrdem)value;
            FieldInfo fi = en.GetType().GetField(en.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                descricao = attributes[0].Description;
            else
                descricao = Enum.GetName(typeof(LogCargaDescOrdem), value);

            return descricao;
        }
    }
}
