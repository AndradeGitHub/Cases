using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioEnquadramento : Repositorio<Enquadramento>
    {
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        private readonly string connStr;

        public RepositorioEnquadramento(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;

            this.connStr = dbQC.Database.Connection.ConnectionString;
        }

        #region Public Methods
        public override int Alterar(List<Enquadramento> enquadramento)
        {            
            SqlConnection conn = new SqlConnection(connStr);            

            SqlCommand cmd = new SqlCommand("SP_LIBERAR_INCONSISTENCIA_CARTEIRA ", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 5000;

            cmd.Parameters.Add("@CD_USUARIO", SqlDbType.Int).Value = enquadramento[0].codUsuario;
            cmd.Parameters.Add("@CD_PROCESSAMENTO", SqlDbType.Int).Value = enquadramento[0].codProcessamento;
            cmd.Parameters.Add("@CD_CARTEIRA", SqlDbType.VarChar, 15).Value = enquadramento[0].codCarteira;
            cmd.Parameters.Add("@LISTA_CD_SUBTIPO_FILTRO", SqlDbType.VarChar).Value = enquadramento[0].codListaSubTipo;            

            conn.Open();

            var ret = cmd.ExecuteNonQuery();

            conn.Close();

            return ret;
        }

        public override List<Enquadramento> SelecionarRegistro(Enquadramento enquadramento)
        {
            Enquadramento dadosPeriodo = VerificarPeriodo(enquadramento);
            
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("SP_ENQUADRAMENTO ", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 5000;

            cmd.Parameters.Add("@DATA", SqlDbType.VarChar, 10).Value = dadosPeriodo.dtReferenciaMensal.ToString("d/M/yyyy");
            cmd.Parameters.Add("@IN_PERIODO", SqlDbType.VarChar, 1).Value = dadosPeriodo.inPeriodoProcessamento;

            conn.Open();

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Enquadramento> lstEnquadramento = new List<Enquadramento>();
            while (rdr.Read())
            {
                Enquadramento enquadramentoRet = new Enquadramento();

                enquadramentoRet.codCarteira = rdr["CD_CARTEIRA"].ToString();
                enquadramentoRet.dtPosicao = Convert.ToDateTime(rdr["DT_POSICAO"]);
                enquadramentoRet.dtResultado = Convert.ToDateTime(rdr["DT_RESULTADO"]);
                enquadramentoRet.codTipoFiltroPai = Convert.ToInt32(rdr["CD_TIPO_FILTRO_PAI"]);
                enquadramentoRet.codTipoFiltro = Convert.ToInt32(rdr["CD_TIPO_FILTRO"]);
                enquadramentoRet.codProcessamento = Convert.ToInt32(rdr["CD_PROCESSAMENTO"]);
                enquadramentoRet.inEnquadrado = rdr["IN_ENQUADRADO"].ToString();               

                lstEnquadramento.Add(enquadramentoRet);
            }

            conn.Close();            

            return lstEnquadramento;
        }

        //public override List<Enquadramento> SelecionarRegistro(Enquadramento enquadramento)
        //{
        //    string inPeriodoProcessamento = "D";
        //    DateTime dataReferenciaMensal = enquadramento.dtResultado;

        //    List<DateTime> lstDataReferenciaMensal = null;

        //    //Query para descobrir data do processamento mensal do período
        //    if (enquadramento.inDiarioMensal.ToUpper().Equals("MENSAL"))
        //    {                
        //        lstDataReferenciaMensal = (from p in dbQC.ProcessamentoDB
        //                                   where
        //                                    p.dtReferencia.Month.Equals(enquadramento.dtResultado.Month) &&
        //                                    p.dtReferencia.Year.Equals(enquadramento.dtResultado.Year) &&
        //                                    p.inPeriodoProcessamento.Equals("M")
        //                                   select (p.dtReferencia)
        //                                   ).ToList<DateTime>();

        //        if (lstDataReferenciaMensal.Count > 0)
        //            dataReferenciaMensal = lstDataReferenciaMensal.Max();                

        //        inPeriodoProcessamento = "M";
        //    }

        //    var lstFrom = RetornaFrom(dataReferenciaMensal, inPeriodoProcessamento);

        //    var lstLeftJoin = RetornaLeftJoin(dataReferenciaMensal, inPeriodoProcessamento);

        //    var lstRet = RetornaAgrupamento(lstFrom, lstLeftJoin);

        //    if (enquadramento.inDiarioMensal.ToUpper().Equals("DIARIO"))
        //    {
        //        lstRet = lstRet.Where(p => p.dtResultado.Equals(enquadramento.dtResultado)).ToList<Enquadramento>();

        //        dataReferenciaMensal = enquadramento.dtResultado;
        //    }
        //    else if (enquadramento.inDiarioMensal.ToUpper().Equals("MENSAL"))
        //    {
        //        if (lstDataReferenciaMensal.Count == 0)
        //            lstRet.Clear();
        //        else
        //            lstRet = lstRet.Where(p => p.dtResultado.Equals(dataReferenciaMensal)).ToList<Enquadramento>();
        //    }

        //    var lstEnquadramentoRet = RetornaInEnquadrado(dataReferenciaMensal, lstRet);

        //    lstEnquadramentoRet = lstEnquadramentoRet.OrderBy(o => o.codCarteira).ThenBy(o => o.codTipoFiltroPai).ThenBy(o => o.codTipoFiltro).ToList<Enquadramento>();

        //    return lstEnquadramentoRet;
        //}

        public override List<Enquadramento> SelecionarRegistroDetalhe(Enquadramento enquadramento)
        {            
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("SP_LISTA_INCONSISTENCIA_DETALHE ", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 5000;

            cmd.Parameters.Add("@CD_PROCESSAMENTO", SqlDbType.Int).Value = enquadramento.codProcessamento;
            cmd.Parameters.Add("@CD_CARTEIRA", SqlDbType.VarChar).Value = enquadramento.codCarteira;

            conn.Open();

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Enquadramento> lstEnquadramento = new List<Enquadramento>();
            while (rdr.Read())
            {
                Enquadramento enquadramentoRet = new Enquadramento();

                enquadramentoRet.codCarteira = rdr["CD_CARTEIRA"].ToString();
                enquadramentoRet.codTipoFiltroPai = Convert.ToInt32(rdr["CD_TIPO_FILTRO_PAI"]);
                enquadramentoRet.codSubTipoFiltro = Convert.ToInt32(rdr["CD_SUBTIPO_FILTRO"]);
                enquadramentoRet.nomeTipoFiltro = rdr["NO_TIPO_FILTRO"].ToString();
                enquadramentoRet.dsCausaInconsistencia = rdr["DS_CAUSA_INCONSISTENCIA"].ToString();
                enquadramentoRet.codProcessamento = Convert.ToInt32(rdr["CD_PROCESSAMENTO"]);
                enquadramentoRet.inLiberado = rdr["IN_LIBERADO"].ToString();

                lstEnquadramento.Add(enquadramentoRet);
            }

            conn.Close();

            return lstEnquadramento;
        }
        #endregion Public Methods

        #region Private Methods
        private Enquadramento VerificarPeriodo(Enquadramento enquadramento)
        {
            string inPeriodoProcessamento = "D";
            DateTime dataReferenciaMensal = enquadramento.dtResultado;

            List<DateTime> lstDataReferenciaMensal = null;

            //Query para descobrir data do processamento mensal do período
            if (enquadramento.inDiarioMensal.ToUpper().Equals("MENSAL"))
            {
                lstDataReferenciaMensal = (from p in dbQC.ProcessamentoDB
                                           where
                                            p.dtReferencia.Month.Equals(enquadramento.dtResultado.Month) &&
                                            p.dtReferencia.Year.Equals(enquadramento.dtResultado.Year) &&
                                            p.inPeriodoProcessamento.Equals("M")
                                           select (p.dtReferencia)
                                           ).ToList<DateTime>();

                if (lstDataReferenciaMensal.Count > 0)
                    dataReferenciaMensal = lstDataReferenciaMensal.Max();

                inPeriodoProcessamento = "M";
            }

            enquadramento.inPeriodoProcessamento = inPeriodoProcessamento;
            enquadramento.dtReferenciaMensal = dataReferenciaMensal;

            return enquadramento;
        }

        //private List<Enquadramento> RetornaFrom(DateTime dataReferenciaMensal, string inPeriodoProcessamento)
        //{
        //    var lstFrom = (from re in dbQC.ResultadoEnquadramento
        //                  join p in dbQC.ProcessamentoDB on re.codProcessamento equals p.codProcessamento
        //                  from tf in dbQC.TipoFiltro
        //                  where
        //                   tf.codTipoFiltro.Equals(tf.codTipoFiltro) &&
        //                   !tf.codTipoFiltro.Equals(tf.codTipoFiltroPai) &&
        //                   re.dtResultado.Equals(dataReferenciaMensal) &&
        //                   p.inPeriodoProcessamento.Equals(inPeriodoProcessamento) &&
        //                   ((int)re.codProcessamento).Equals(
        //                                                    ((int)(from reMax in dbQC.ResultadoEnquadramento
        //                                                           join pMax in dbQC.ProcessamentoDB on reMax.codProcessamento equals pMax.codProcessamento
        //                                                           where
        //                                                            re.codCarteira.Equals(reMax.codCarteira) &&
        //                                                            re.dtResultado.Equals(reMax.dtResultado) &&
        //                                                            pMax.inPeriodoProcessamento.Equals(inPeriodoProcessamento)
        //                                                           select reMax.codProcessamento).Max())
        //                                                   ) &&
        //                   ((DateTime)re.dtPosicao).Equals(
        //                                                    ((DateTime)(from reMax in dbQC.ResultadoEnquadramento
        //                                                                join pMax in dbQC.ProcessamentoDB on reMax.codProcessamento equals pMax.codProcessamento
        //                                                                where
        //                                                                 ((int)re.codProcessamento).Equals((int)reMax.codProcessamento) &&
        //                                                                 re.codCarteira.Equals(reMax.codCarteira) &&
        //                                                                 pMax.inPeriodoProcessamento.Equals(inPeriodoProcessamento)
        //                                                                select reMax.dtPosicao).Max())
        //                                                   )
        //                   select new Enquadramento
        //                   {
        //                         codCarteira = re.codCarteira,
        //                         codTipoFiltro = tf.codTipoFiltro,
        //                         codTipoFiltroPai = tf.codTipoFiltroPai,
        //                         codSubTipoFiltro = re.codSubTipoFiltro,
        //                         dtResultado = re.dtResultado,
        //                         codProcessamento = (int)re.codProcessamento,
        //                         dtPosicao = re.dtPosicao
        //                   }
        //                   )
        //                   .GroupBy(group => new
        //                   {
        //                        group.codCarteira,
        //                        group.codTipoFiltro,
        //                        group.codTipoFiltroPai,
        //                        group.codSubTipoFiltro,   
        //                        group.dtResultado,                                
        //                        group.codProcessamento,                                
        //                        group.dtPosicao
        //                   })
        //                   .Select(group => new Enquadramento
        //                   {
        //                        codCarteira = group.Key.codCarteira,
        //                        codTipoFiltro = group.Key.codTipoFiltro,
        //                        codTipoFiltroPai = group.Key.codTipoFiltroPai,
        //                        codSubTipoFiltro = group.Key.codSubTipoFiltro,
        //                        dtResultado = group.Key.dtResultado,                                
        //                        codProcessamento = group.Key.codProcessamento,                                
        //                        dtPosicao = group.Key.dtPosicao
        //                   })
        //                   .Distinct<Enquadramento>()
        //                   .ToList<Enquadramento>();  

        //    return lstFrom;
        //}

        //private List<Enquadramento> RetornaLeftJoin(DateTime dataReferenciaMensal, string inPeriodoProcessamento)
        //{
        //    var lstLeftJoin = (from re in dbQC.ResultadoEnquadramento
        //                       join p in dbQC.ProcessamentoDB on re.codProcessamento equals p.codProcessamento
        //                       where
        //                        re.dtResultado.Equals(dataReferenciaMensal) &&
        //                        p.inPeriodoProcessamento.Equals(inPeriodoProcessamento) &&
        //                        ((int)re.codProcessamento).Equals(
        //                                                          ((int)(from reMax in dbQC.ResultadoEnquadramento
        //                                                                 join pMax in dbQC.ProcessamentoDB on reMax.codProcessamento equals pMax.codProcessamento
        //                                                                 where
        //                                                                  re.codCarteira.Equals(reMax.codCarteira) &&
        //                                                                  re.dtResultado.Equals(reMax.dtResultado) &&
        //                                                                  pMax.inPeriodoProcessamento.Equals(inPeriodoProcessamento)
        //                                                                 select reMax.codProcessamento).Max())
        //                                                         )
        //                       select new Enquadramento
        //                       {
        //                           codCarteira = re.codCarteira,                                   
        //                           codTipoFiltro = re.codSubTipoFiltro,
        //                           codTipoFiltroPai = re.codTipoFiltro,
        //                           dtResultado = re.dtResultado,
        //                           inEnquadrado = (re.inLiberado.Equals("S") ? "S" : re.inEnquadrado), 
        //                           codProcessamento = (int)re.codProcessamento
        //                       }
        //                      ).Distinct<Enquadramento>()
        //                       .ToList<Enquadramento>();

        //    return lstLeftJoin;
        //}

        //private List<Enquadramento> RetornaAgrupamento(List<Enquadramento> lstFrom, List<Enquadramento> lstLeftJoin)
        //{
        //    var lstGroupDistinct = (from re in lstFrom
        //                            from tf in lstLeftJoin.Where(w => w.codTipoFiltro.Equals(re.codTipoFiltroPai) &&
        //                                                         w.codCarteira.Equals(re.codCarteira)).DefaultIfEmpty() //Left Join                                                                                                         
        //                            select new Enquadramento
        //                            {
        //                                codCarteira = re.codCarteira.Trim(),
        //                                codTipoFiltroPai = re.codTipoFiltroPai,
        //                                codTipoFiltro = re.codTipoFiltro,
        //                                dtResultado = re.dtResultado,
        //                                //codSubTipoFiltro = re.codSubTipoFiltro,   
        //                                codProcessamento = re.codProcessamento,
        //                                inEnquadrado = (tf != null ? "NULL" : string.Empty),
        //                                dtPosicao = re.dtPosicao
        //                            }
        //                            )
        //                            .GroupBy(group => new
        //                            {
        //                                group.codCarteira,
        //                                group.codTipoFiltroPai,
        //                                group.codTipoFiltro,
        //                                group.dtResultado,
        //                                //group.codSubTipoFiltro,   
        //                                group.codProcessamento,
        //                                group.inEnquadrado,
        //                                group.dtPosicao
        //                            })
        //                            .Select(group => new Enquadramento
        //                            {
        //                                codCarteira = group.Key.codCarteira,
        //                                codTipoFiltroPai = group.Key.codTipoFiltroPai,
        //                                codTipoFiltro = group.Key.codTipoFiltro,
        //                                dtResultado = group.Key.dtResultado,
        //                                //codSubTipoFiltro = group.Key.codSubTipoFiltro,
        //                                codProcessamento = group.Key.codProcessamento,
        //                                inEnquadrado = group.Key.inEnquadrado,
        //                                dtPosicao = group.Key.dtPosicao
        //                            }).Distinct<Enquadramento>()
        //                              .ToList<Enquadramento>();            

        //    return lstGroupDistinct;
        //}

        //private List<Enquadramento> RetornaInEnquadrado(DateTime dataReferenciaMensal, List<Enquadramento> lstRet)
        //{
        //    var lstEnquadramento = (from reEnquadrado in dbQC.ResultadoEnquadramento
        //                            where
        //                             reEnquadrado.dtResultado.Equals(dataReferenciaMensal)
        //                            select new Enquadramento
        //                            {
        //                                codCarteira = reEnquadrado.codCarteira,
        //                                codTipoFiltroPai = reEnquadrado.codSubTipoFiltro,
        //                                codTipoFiltro = reEnquadrado.codTipoFiltro,
        //                                dtResultado = reEnquadrado.dtResultado,
        //                                codProcessamento = (int)reEnquadrado.codProcessamento,
        //                                inEnquadrado = (reEnquadrado.inLiberado.Equals("S") ? "S" : reEnquadrado.inEnquadrado)                                      
        //                            }).ToList<Enquadramento>();

        //    List<Enquadramento> lstEnquadramentoRet = new List<Enquadramento>();
        //    foreach (Enquadramento enquadramentoFor in lstRet)
        //    {
        //        Enquadramento enquadramentoInt = new Enquadramento();

        //        enquadramentoInt.codCarteira = enquadramentoFor.codCarteira;
        //        enquadramentoInt.codTipoFiltroPai = enquadramentoFor.codTipoFiltroPai;
        //        enquadramentoInt.codTipoFiltro = enquadramentoFor.codTipoFiltro;
        //        enquadramentoInt.dtResultado = enquadramentoFor.dtResultado;
        //        enquadramentoInt.codProcessamento = enquadramentoFor.codProcessamento;
        //        enquadramentoInt.inEnquadrado = string.Empty;
        //        enquadramentoInt.dtPosicao = enquadramentoFor.dtPosicao;

        //        foreach (Enquadramento enquadramentoForInt in lstEnquadramento)
        //        {
        //            if (enquadramentoFor.codCarteira.Trim().Equals(enquadramentoForInt.codCarteira.Trim()) &&
        //                enquadramentoFor.codTipoFiltro.Equals(enquadramentoForInt.codTipoFiltroPai) &&
        //                enquadramentoFor.codTipoFiltroPai.Equals(enquadramentoForInt.codTipoFiltro) &&
        //                enquadramentoFor.codProcessamento.Equals(enquadramentoForInt.codProcessamento) &&
        //                enquadramentoFor.dtResultado.Equals(enquadramentoForInt.dtResultado))
        //            {
        //                enquadramentoInt.inEnquadrado = enquadramentoForInt.inEnquadrado;
        //            }
        //        }

        //        lstEnquadramentoRet.Add(enquadramentoInt);
        //    }

        //    //lstRet.ForEach(item =>
        //    //{
        //    //    var teste = (from reEnquadrado in dbQC.ResultadoEnquadramento
        //    //                 where
        //    //                   reEnquadrado.codCarteira.Trim().Equals(item.codCarteira.Trim()) &&
        //    //                   reEnquadrado.codSubTipoFiltro.Equals(item.codTipoFiltro) &&
        //    //                   reEnquadrado.codTipoFiltro.Equals(item.codTipoFiltroPai) &&
        //    //                   ((int)(reEnquadrado.codProcessamento)).Equals(item.codProcessamento)
        //    //                 select (reEnquadrado.inEnquadrado)).Take(1).ToList<string>();

        //    //    if (teste.Count > 0)
        //    //        item.inEnquadrado = teste.Single();
        //    //});   

        //    return lstEnquadramentoRet;
        //}
        #endregion Private Methods
    }
}
