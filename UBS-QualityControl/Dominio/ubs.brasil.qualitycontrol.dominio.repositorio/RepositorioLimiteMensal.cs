using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioLimiteMensal : Repositorio<LimitePerfilRisco>
    {        
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        public RepositorioLimiteMensal(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;            
        }

        #region Public Methods
        public override int Gravar(List<LimitePerfilRisco> limitePerfilRisco)
        {
            if (verificaRegistro(limitePerfilRisco))
                return -1;

            int codLimitePerfilRisco = RetornaUltimoRegistro() + 1;

            limitePerfilRisco.ForEach(item => item.codLimitePerfilRisco = codLimitePerfilRisco++);

            limitePerfilRisco.ForEach(p => dbQC.Entry(p).State = EntityState.Added);

            return dbQC.SaveChanges();                              
        }

        public override int Alterar(List<LimitePerfilRisco> limitePerfilRisco)
        {
            limitePerfilRisco.ForEach(p => dbQC.Entry(p).State = EntityState.Modified);                                

            return dbQC.SaveChanges();
        }

        public override int Apagar(List<LimitePerfilRisco> limitePerfilRisco)
        {
            string codPerfilRisco = string.Empty;
            string codigoPerfilRisco = string.Empty;
            foreach (LimitePerfilRisco perfilRisco in limitePerfilRisco)
            {
                if (codigoPerfilRisco != perfilRisco.codPerfilRisco)
                    codPerfilRisco += string.Concat(perfilRisco.codPerfilRisco, ",");

                codigoPerfilRisco = perfilRisco.codPerfilRisco;
            }

            DateTime dtIniVigencia = limitePerfilRisco[0].dtIniVigencia;
            DateTime dtFimVigencia = limitePerfilRisco[0].dtFimVigencia;
            int codTipoFiltro = limitePerfilRisco[0].codTipoFiltro;
            int[] codSubTipoFiltro = { 10, 12 };            

            var lstLimitePerfilRisco = (from lpr in dbQC.LimitePerfilRisco
                                        where 
                                          lpr.dtIniVigencia.Equals(dtIniVigencia) &&
                                          lpr.dtFimVigencia.Equals(dtFimVigencia) &&
                                          lpr.inExcecao.Equals("N") &&
                                          lpr.inDiarioMensal.Equals("M") &&
                                          lpr.codTipoFiltro.Equals(codTipoFiltro) &&
                                          codPerfilRisco.Contains(lpr.codPerfilRisco) &&
                                          codSubTipoFiltro.Contains(lpr.codSubTipoFiltro)                                          
                                        select lpr).ToList<LimitePerfilRisco>(); 

            lstLimitePerfilRisco.ForEach(p => dbQC.Entry(p).State = EntityState.Deleted);

            return dbQC.SaveChanges();
        }
        
        public override List<LimitePerfilRisco> SelecionarRegistro(LimitePerfilRisco limitePerfilRisco)
        {
            List<string> lstPerfilRisco;
            if (string.IsNullOrEmpty(limitePerfilRisco.codPerfilRisco))
            {
                lstPerfilRisco = (from pr in dbWM_DB.PerfilRisco
                                  where pr.inAtivoInativo.Equals("A")
                                  select pr.codPerfilRisco).ToList<string>(); 
            }
            else
            {
                lstPerfilRisco = (from pr in dbWM_DB.PerfilRisco
                                  where 
                                    pr.inAtivoInativo.Equals("A") &&
                                    pr.codPerfilRisco.ToUpper().Equals(limitePerfilRisco.codPerfilRisco.ToUpper())
                                  select pr.codPerfilRisco).ToList<string>();
            }

            int[] codSubTipoFiltro = { 10, 12 };

            var resQC = from lpr in dbQC.LimitePerfilRisco
                        join tf in dbQC.TipoFiltro on lpr.codTipoFiltro equals tf.codTipoFiltro
                        where 
                          lpr.inExcecao.Equals("N") &&
                          lpr.inDiarioMensal.Equals("M") &&
                          tf.inAtivoInativo.Equals("A") &&
                          lpr.codTipoFiltro.Equals(2) &&
                          lstPerfilRisco.Contains(lpr.codPerfilRisco) &&
                          codSubTipoFiltro.Contains(lpr.codSubTipoFiltro)                           
                        orderby lpr.dtIniVigencia, lpr.dtFimVigencia, lpr.codPerfilRisco
                        select lpr;

            if (limitePerfilRisco.dtInicial != null)
            {                
                resQC = resQC.Where(item => item.dtIniVigencia.Month == limitePerfilRisco.dtInicial.Value.Month);
                resQC = resQC.Where(item => item.dtFimVigencia.Month == limitePerfilRisco.dtInicial.Value.Month);
                resQC = resQC.Where(item => item.dtIniVigencia.Year == limitePerfilRisco.dtInicial.Value.Year);
                resQC = resQC.Where(item => item.dtFimVigencia.Year == limitePerfilRisco.dtInicial.Value.Year);
            }

            return resQC.ToList<LimitePerfilRisco>(); 
        }
        #endregion

        #region Private Methods
        private bool verificaRegistro(List<LimitePerfilRisco> limitePerfilRisco)
        {
            int count = 0;

            foreach (LimitePerfilRisco limitePerfil in limitePerfilRisco)
            {
                var resQC = from lpr in dbQC.LimitePerfilRisco
                            where
                              lpr.codPerfilRisco.Equals(limitePerfil.codPerfilRisco) &&
                              (lpr.dtIniVigencia.Month.Equals(limitePerfil.dtIniVigencia.Month) && lpr.dtIniVigencia.Year.Equals(limitePerfil.dtIniVigencia.Year) &&
                              lpr.dtFimVigencia.Month.Equals(limitePerfil.dtFimVigencia.Month) && lpr.dtFimVigencia.Year.Equals(limitePerfil.dtFimVigencia.Year)) &&
                              lpr.codTipoFiltro.Equals(limitePerfil.codTipoFiltro) &&
                              lpr.codSubTipoFiltro.Equals(limitePerfil.codSubTipoFiltro) &&
                              lpr.inExcecao.Equals(limitePerfil.inExcecao) &&
                              lpr.inDiarioMensal.Equals(limitePerfil.inDiarioMensal)
                            select lpr;

                count = resQC.ToList<LimitePerfilRisco>().Count;

                if (count > 0)
                    return true;
            }

            return false;
        }

        private int RetornaUltimoRegistro()
        {
            int codLimitePerfilRisco = 0;

            var resQCExist = from lpr in dbQC.LimitePerfilRisco
                             select lpr;

            if (resQCExist.Count() > 0)
            {
                var resQC = (from lpr in dbQC.LimitePerfilRisco
                             orderby lpr.codLimitePerfilRisco descending
                             select lpr).First();
                codLimitePerfilRisco = resQC.codLimitePerfilRisco + 1;
            }

            return codLimitePerfilRisco;
        }
        #endregion
    }
}
