using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioProcessamentoResultado : Repositorio<Processamento>
    {
        private CultureInfo cultureBR = new CultureInfo("pt-BR");

        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        public RepositorioProcessamentoResultado(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;
        }

        #region Public Methods
        public override int Gravar(List<Processamento> processamento)
        {
            string lstAtivo = string.Empty;

            foreach (Processamento proc in processamento)
            {
                if (string.IsNullOrEmpty(lstAtivo))
                    lstAtivo = string.Concat(proc.codAtivo, "|", proc.codTipoAtivo);
                else
                    lstAtivo = string.Format("{0},{1}", lstAtivo, string.Concat(proc.codAtivo, "|", proc.codTipoAtivo));
            }

            SqlParameter[] parameters = new SqlParameter[4];

            SqlParameter codSubTipoFiltro = new SqlParameter("@CD_SUBTIPO_FILTRO", SqlDbType.Int);
            codSubTipoFiltro.Value = processamento[0].codSubTipoFiltro;
            parameters[0] = codSubTipoFiltro;

            SqlParameter dtResultado = new SqlParameter("@DT_RESULTADO", SqlDbType.DateTime);
            dtResultado.Value = processamento[0].dtResultado;
            parameters[1] = dtResultado;

            SqlParameter codUsuario = new SqlParameter("@CD_USUARIO", SqlDbType.Int);
            codUsuario.Value = processamento[0].codUsuario;
            parameters[2] = codUsuario;

            SqlParameter codListaAtivo = new SqlParameter("@LISTA_CD_ATIVO", SqlDbType.VarChar);
            codListaAtivo.Value = lstAtivo;
            parameters[3] = codListaAtivo;

            return dbQC.Database.ExecuteSqlCommand("exec SP_LIBERAR_INCONSISTENCIA_ATIVO @CD_SUBTIPO_FILTRO, @DT_RESULTADO, @CD_USUARIO, @LISTA_CD_ATIVO", parameters);
        }

        public override int Alterar(List<Processamento> processamento)
        {
            int ret = 0;
            
            foreach (Processamento proc in processamento)
            {
                EnquadramentoDB enquadramento = new EnquadramentoDB();

                if (proc.codSubTipoFiltro == null)
                    enquadramento.codSubTipoFiltro = 0;
                else
                    enquadramento.codSubTipoFiltro = (int)proc.codSubTipoFiltro;

                enquadramento.dtResultado  = proc.dtResultado;

                if (enquadramento.codSubTipoFiltro.Equals(16))
                {
                    if (proc.codAtivo != null)
                    {
                        enquadramento.codAtivo = proc.codAtivo;
                        enquadramento.codTipoAtivo = proc.codTipoAtivo;
                    }
                    else
                        enquadramento.codCarteira = proc.codCarteira.TrimStart().TrimEnd();
                }
                else
                    enquadramento.codCarteira = proc.codCarteira.TrimStart().TrimEnd();

                var lstEnqRet = SelecionarRegistroAlterar(enquadramento);

                if (lstEnqRet.Count > 0)
                {
                    lstEnqRet.ForEach(item => { item.inLiberado = proc.inLiberado; item.dtLiberacao = DateTime.Now; item.codUsuarioAlteracao = proc.codUsuario; });

                    lstEnqRet.ForEach(p => dbQC.Entry(p).State = EntityState.Modified);

                    ret += dbQC.SaveChanges();
                }                
            }

            return ret;
        }

        public override List<Processamento> SelecionarRegistro(Processamento processamento)
        {
            processamento.dtResultado = DateTime.ParseExact(processamento.dtResultadoPesq, "dd/MM/yyyy", cultureBR); 

            var lstProc = (from re in dbQC.ResultadoEnquadramento
                           join tf in dbQC.TipoFiltro on re.codSubTipoFiltro equals tf.codTipoFiltro                                             
                           join p in dbQC.ProcessamentoDB on re.codProcessamento equals p.codProcessamento
                           where
                            re.dtResultado.Equals(processamento.dtResultado) &&
                            tf.inAtivoInativo.Equals("A") &&
                            re.inEnquadrado.Equals("N") &&
                            re.inLiberado.Equals("N") &&
                            p.inFinalizado.Equals("S") &&                            
                            (p.codProcessamento).Equals(
                                                        ((int)(from proc in dbQC.ResultadoEnquadramento
                                                            where 
                                                                proc.dtResultado.Equals(processamento.dtResultado) &&                                                                
                                                                proc.codCarteira.Equals(re.codCarteira)
                                                            select proc.codProcessamento).Max())
                                                        )  
                           select new Processamento
                           {
                              dtResultado      = re.dtResultado,
                              codCarteira      = re.codCarteira,
                              codSubTipoFiltro = re.codSubTipoFiltro,
                              codTipoFiltroPai = tf.codTipoFiltroPai,
                              codTipoFiltro    = tf.codTipoFiltro,
                              nomeFiltroAbrev  = tf.nomeTipoFiltroAbrev,
                              nomeFiltro       = tf.nomeTipoFiltro,                              
                           }).ToList<Processamento>();

            var lstVlrPatrimonio = RecuperaValorPatrimonio(processamento.dtResultado);

            var lstLeftJoin = (from p in lstProc
                               from cc in lstVlrPatrimonio.Where(w => w.codCarteira.Trim().Equals(p.codCarteira.Trim()) && w.dtResultado.Equals(p.dtResultado)).DefaultIfEmpty() //Left Join                                                                                              
                               select new Processamento
                               {
                                  dtResultado      = p.dtResultado,
                                  codCarteira      = p.codCarteira,
                                  codSubTipoFiltro = (int)p.codSubTipoFiltro,
                                  codTipoFiltroPai = (int)p.codTipoFiltroPai,
                                  codTipoFiltro    = (int)p.codTipoFiltro,
                                  nomeFiltroAbrev  = p.nomeFiltroAbrev,
                                  nomeFiltro       = p.nomeFiltro,
                                  vlPatrimonial    = (double)(cc == null ? 0 : cc.vlPatrimonial)
                               }).ToList<Processamento>();

            //Atualiza o Valor Patrimonial            
            //lstProc.ForEach(item => { item.vlPatrimonial = lstVlrPatrimonio.Where(w => w.codCarteira.Trim().Equals(item.codCarteira.Trim())).Select(item1 => item1.vlPatrimonial).Sum(); });

            var lstRet = lstLeftJoin.GroupBy(group => new
                                              {                                
                                                group.nomeFiltroAbrev,
                                                group.nomeFiltro,
                                                group.codSubTipoFiltro,
                                                group.codTipoFiltroPai,
                                                group.codTipoFiltro
                                              })
                                              .Select(group => new Processamento
                                              {
                                                dtResultado      = Convert.ToDateTime(group.Select(s => s.dtResultado).First()),       
                                                nomeFiltroAbrev  = group.Key.nomeFiltroAbrev,
                                                nomeFiltro       = group.Key.nomeFiltro,
                                                codSubTipoFiltro = group.Key.codSubTipoFiltro,
                                                codTipoFiltroPai = group.Key.codTipoFiltroPai,
                                                codTipoFiltro    = group.Key.codTipoFiltro,
                                                qtdeCarteira     = group.Select(s => s.codCarteira).Distinct<string>().Count(),
                                                vlPatrimonial    = Math.Round((double)group.Sum(s => s.vlPatrimonial), 2)                                                
                                              })
                                              .OrderBy(o => o.codTipoFiltro)
                                              .ToList<Processamento>();

            return lstRet;
        }

        public override List<Processamento> SelecionarRegistroDetalhe(Processamento processamento)
        {            
            var lstProc = (from re in dbQC.ResultadoEnquadramento
                           join tf in dbQC.TipoFiltro on re.codSubTipoFiltro equals tf.codTipoFiltro
                           join p in dbQC.ProcessamentoDB on re.codProcessamento equals p.codProcessamento
                           where
                            tf.inAtivoInativo.Equals("A") &&     
                            re.dtResultado.Equals(processamento.dtResultado) &&
                            re.codSubTipoFiltro.Equals((int)processamento.codSubTipoFiltro) &&                                                                                
                            re.inEnquadrado.Equals("N") &&
                            re.inLiberado.Equals("N") &&
                            p.inFinalizado.Equals("S") &&
                            (p.codProcessamento).Equals(
                                                        ((int)(from proc in dbQC.ProcessamentoDB
                                                               where
                                                                   proc.dtReferencia.Equals(processamento.dtResultado) &&
                                                                   proc.inFinalizado.Equals("S")
                                                               select proc.codProcessamento).Max())
                                                        )                              
                           select new Processamento
                           {
                               codAtivo         = re.codAtivo,
                               codTipoAtivo     = re.codTipoAtivo,
                               codSubTipoFiltro = re.codSubTipoFiltro,  
                               dtResultado      = re.dtResultado,
                               dtPosicao        = (DateTime)re.dtPosicao,
                               codCarteira      = re.codCarteira,
                               inLiberado       = re.inLiberado,                               
                               nomeFiltroAbrev  = tf.nomeTipoFiltroAbrev                                   
                           })
                           .ToList<Processamento>();

            var lstVlrPatrimonio = RecuperaValorPatrimonioDetalhe();
            var lstAtivo = RecuperaAtivo();
            var lstPosicao = RecuperaPosicao();      
                      
            var lstLeftJoinProc = (from p in lstProc
                                   /*****************************************************************
                                    Left Join 
                                   *****************************************************************/
                                   join a in lstAtivo on
                                   new
                                   {
                                       codAtivo     = p.codAtivo,
                                       codTipoAtivo = p.codTipoAtivo,
                                   }
                                   equals
                                   new
                                   {
                                       codAtivo     = a.codAtivo,
                                       codTipoAtivo = a.codTipoAtivo                                      
                                   } into atJoinedLists  
                                   from at in atJoinedLists.DefaultIfEmpty()                               
                                   join c in lstVlrPatrimonio on
                                   new
                                   {
                                       codCarteira = p.codCarteira.Trim(),
                                       dtPosicao   = p.dtPosicao,
                                   }
                                   equals
                                   new
                                   {
                                       codCarteira = c.codCarteira.Trim(),
                                       dtPosicao   = c.dtResultado,                                       
                                   } into ccJoinedLists
                                   from cc in ccJoinedLists.DefaultIfEmpty()
                                   join pos in lstPosicao on
                                   new
                                   {
                                       codCarteira  = p.codCarteira.Trim(),
                                       dtPosicao    = p.dtPosicao,
                                       codAtivo     = p.codAtivo,
                                       codTipoAtivo = p.codTipoAtivo
                                   }
                                   equals
                                   new
                                   {
                                       codCarteira  = pos.codCarteira.Trim(),
                                       dtPosicao    = pos.dtPosicao,
                                       codAtivo     = pos.codAtivo,
                                       codTipoAtivo = pos.codTipoAtivo
                                   } into posJoinedLists
                                   from pos1 in posJoinedLists.DefaultIfEmpty()  
                                   /*****************************************************************
                                    Fim Left Join 
                                   *****************************************************************/                                                      
                                   select new Processamento
                                   {
                                       codAtivo         = p.codAtivo,
                                       codTipoAtivo     = p.codTipoAtivo,
                                       codSubTipoFiltro = p.codSubTipoFiltro,
                                       dtResultado      = p.dtResultado,
                                       codCarteira      = p.codCarteira,
                                       inLiberado       = p.inLiberado,
                                       nomeFiltroAbrev  = p.nomeFiltroAbrev,
                                       vlPatrimonial    = (double)(cc == null ? 0 : cc.vlPatrimonial),                                                                                                                  
                                       nomeAtivo        = (string)(p.codSubTipoFiltro.Equals(5) || p.codSubTipoFiltro.Equals(8) ? (at == null ? "" : at.nomeAtivo) : (p.codAtivo == null ? "" : p.codAtivo)),                                                                                                                                                      
                                       vlrPosicaoAtivo  = (double)(pos1 == null ? 0 : pos1.vlrSaldoBruto)                                                                                                                
                                   }).ToList<Processamento>();            

            var lstLeftProcUnion = lstLeftJoinProc.GroupBy(group => new
                                                        {
                                                            group.codAtivo,
                                                            group.codTipoAtivo,
                                                            group.codSubTipoFiltro,
                                                            group.nomeFiltroAbrev,
                                                            group.inLiberado,
                                                            group.nomeAtivo
                                                        })
                                                        .Select(group => new Processamento
                                                        {
                                                            dtResultado      = Convert.ToDateTime(group.Select(s => s.dtResultado).First()),
                                                            codSubTipoFiltro = group.Key.codSubTipoFiltro,
                                                            codAtivo         = group.Key.codAtivo,
                                                            nomeAtivo        = group.Key.nomeAtivo,
                                                            codTipoAtivo     = group.Key.codTipoAtivo,
                                                            nomeFiltroAbrev  = group.Key.nomeFiltroAbrev,
                                                            inLiberado       = group.Key.inLiberado,  
                                                            qtdeCarteira     = group.Select(s => s.codCarteira).Distinct<string>().Count(),
                                                            vlPatrimonial    = Math.Round((double)group.Sum(s => s.vlPatrimonial), 2),
                                                            vlrPosicaoAtivo  = Math.Round((double)group.Sum(s => s.vlrPosicaoAtivo), 2)
                                                        })
                                                        .OrderBy(o => o.codAtivo)
                                                        .ToList<Processamento>();

            //2ª Parte para fazer o Union            
            int[] lstCodSubTipoFiltro = { 11, 13, 14, 15 };

            var lstProcUnion = (from re in dbQC.ResultadoEnquadramento
                                join tf in dbQC.TipoFiltro on re.codSubTipoFiltro equals tf.codTipoFiltro
                                join p in dbQC.ProcessamentoDB on re.codProcessamento equals p.codProcessamento
                                where
                                 tf.inAtivoInativo.Equals("A") &&
                                 re.dtResultado.Equals(processamento.dtResultado) &&
                                 lstCodSubTipoFiltro.Contains(re.codTipoFiltro) &&                                 
                                 re.inEnquadrado.Equals("N") &&
                                 re.inLiberado.Equals("N") &&
                                 p.inFinalizado.Equals("S") &&
                                 (p.codProcessamento).Equals(
                                                             ((int)(from proc in dbQC.ProcessamentoDB
                                                                    where
                                                                        proc.dtReferencia.Equals(processamento.dtResultado) &&
                                                                        proc.inFinalizado.Equals("S")
                                                                    select proc.codProcessamento).Max())
                                                             )
                                select new Processamento
                                {
                                    codAtivo = re.codAtivo,
                                    codTipoAtivo = re.codTipoAtivo,
                                    codSubTipoFiltro = re.codSubTipoFiltro,
                                    dtResultado = re.dtResultado,
                                    codCarteira = re.codCarteira,
                                    inLiberado = re.inLiberado,
                                    nomeFiltroAbrev = tf.nomeTipoFiltroAbrev
                                }).ToList<Processamento>();

            var lstLeftJoiProcUnion = (from p in lstProcUnion
                                       from cc in lstVlrPatrimonio.Where(w => w.codCarteira.Trim().Equals(p.codCarteira.Trim()) && w.dtResultado.Equals(p.dtResultado)).DefaultIfEmpty() //Left Join                                                                    
                                       from pos in lstPosicao.Where(w => w.codCarteira.Trim().Equals(p.codCarteira.Trim()) && w.dtPosicao.Equals(p.dtPosicao) &&
                                                                    w.codAtivo.Equals(p.codAtivo) && w.codTipoAtivo.Equals(p.codTipoAtivo)).DefaultIfEmpty() //Left Join                                                                    
                                       select new Processamento
                                       {
                                           codAtivo         = p.codAtivo,
                                           codTipoAtivo     = p.codTipoAtivo,
                                           codSubTipoFiltro = p.codSubTipoFiltro,
                                           dtResultado      = p.dtResultado,
                                           codCarteira      = p.codCarteira,
                                           inLiberado       = p.inLiberado,
                                           nomeFiltroAbrev  = p.nomeFiltroAbrev,
                                           vlPatrimonial    = (double)(cc == null ? 0 : cc.vlPatrimonial),
                                           nomeAtivo        = p.nomeFiltro,
                                           vlrPosicaoAtivo  = (double)(pos == null ? 0 : pos.vlrSaldoBruto),                                                                                                                  
                                       }).ToList<Processamento>();

            var lstRightProcUnion = lstLeftJoiProcUnion.GroupBy(group => new
                                                {
                                                    group.codAtivo,
                                                    group.codTipoAtivo,
                                                    group.codSubTipoFiltro,
                                                    group.nomeFiltroAbrev,
                                                    group.inLiberado
                                                })
                                                .Select(group => new Processamento
                                                {
                                                    dtResultado = Convert.ToDateTime(group.Select(s => s.dtResultado).First()),
                                                    codSubTipoFiltro = group.Key.codSubTipoFiltro,
                                                    codAtivo = group.Key.codAtivo,
                                                    codTipoAtivo = group.Key.codTipoAtivo,
                                                    nomeFiltroAbrev = group.Key.nomeFiltroAbrev,
                                                    inLiberado = group.Key.inLiberado,
                                                    qtdeCarteira = group.Select(s => s.codCarteira).Count(),
                                                    vlPatrimonial = Math.Round((double)group.Sum(s => s.vlPatrimonial), 2),
                                                    vlrPosicaoAtivo = Math.Round((double)group.Sum(s => s.vlrPosicaoAtivo), 2)
                                                })
                                                .OrderBy(o => o.codAtivo)
                                                .ToList<Processamento>();

            var lstRet = lstLeftProcUnion.Union(lstRightProcUnion);

            return lstRet.ToList<Processamento>();
        }

        public override List<Processamento> SelecionarRegistroDetalheItem(Processamento processamento)
        {
            var lstAtivo = RecuperaAtivo();
            var lstCarteira = RecuperaCarteira();

            var lstProc = new List<Processamento>();
            var lstInnerProc = new List<Processamento>();

            if (processamento.codSubTipoFiltro.Equals(16)) //Bloqueio de Ativos
            {
                lstProc = (from re in dbQC.ResultadoEnquadramento
                           join tf in dbQC.TipoFiltro on re.codSubTipoFiltro equals tf.codTipoFiltro
                           where
                           tf.inAtivoInativo.Equals("A") &&
                           re.dtResultado.Equals(processamento.dtResultado) &&
                           re.codSubTipoFiltro.Equals((int)processamento.codSubTipoFiltro) &&
                           re.codAtivo.Equals(processamento.codAtivo) &&
                           re.codTipoAtivo.Equals(processamento.codTipoAtivo)
                           select new Processamento
                           {
                                codAtivo = re.codAtivo,
                                codTipoAtivo = re.codTipoAtivo,                                   
                                codCarteira = re.codCarteira,
                                codTipoFiltro = re.codTipoFiltro,
                                codSubTipoFiltro = re.codSubTipoFiltro,
                                dtResultado = re.dtResultado,
                                inLiberado = re.inLiberado
                           })
                           .Distinct<Processamento>()
                           .ToList<Processamento>();

                lstInnerProc = (from p in lstProc
                                from at in lstAtivo.Where(w => w.codAtivo.Equals(p.codAtivo) && w.codTipoAtivo.Equals(p.codTipoAtivo))
                                join c in lstCarteira on p.codCarteira.Trim() equals c.codCarteira.Trim()
                                select new Processamento
                                {
                                   codCarteira = c.codCarteira.Trim(),
                                   nomeCarteira = c.nomeCarteira,
                                   codTipoFiltro = p.codTipoFiltro,
                                   codSubTipoFiltro = p.codSubTipoFiltro,
                                   dtResultado = p.dtResultado,
                                   inLiberado = p.inLiberado

                                })
                                .OrderBy(o => o.codCarteira)
                                .ToList<Processamento>();
            }
            else
            {              
                lstProc = (from re in dbQC.ResultadoEnquadramento
                           join tf in dbQC.TipoFiltro on re.codSubTipoFiltro equals tf.codTipoFiltro
                           join proc in dbQC.ProcessamentoDB on re.codProcessamento equals proc.codProcessamento
                           where
                           tf.inAtivoInativo.Equals("A") &&
                           re.dtResultado.Equals(processamento.dtResultado) &&
                           re.inEnquadrado.Equals("N") &&
                           (proc.codProcessamento).Equals(
                                                         ((int)(from p in dbQC.ProcessamentoDB
                                                                where
                                                                    p.dtReferencia.Equals(processamento.dtResultado) &&
                                                                    p.inFinalizado.Equals("S")
                                                                select p.codProcessamento).Max())
                                                            ) &&
                           proc.inFinalizado.Equals("S") &&                                
                           re.codSubTipoFiltro.Equals((int)processamento.codSubTipoFiltro)
                           select new Processamento
                           {
                                codCarteira      = re.codCarteira,
                                codTipoFiltro    = re.codTipoFiltro,
                                codSubTipoFiltro = re.codSubTipoFiltro,
                                dtResultado      = re.dtResultado,
                                inLiberado       = re.inLiberado,
                                codProcessamento = proc.codProcessamento
                           })
                           .Distinct<Processamento>()
                           .ToList<Processamento>();

                lstInnerProc = (from p in lstProc                                
                                join c in lstCarteira on p.codCarteira.Trim() equals c.codCarteira.Trim()
                                select new Processamento
                                {
                                    codCarteira      = c.codCarteira.Trim(),
                                    nomeCarteira     = c.nomeCarteira,
                                    codTipoFiltro    = p.codTipoFiltro,
                                    codSubTipoFiltro = p.codSubTipoFiltro,
                                    dtResultado      = p.dtResultado,
                                    inLiberado       = p.inLiberado,
                                    codProcessamento = p.codProcessamento
                                })
                                .OrderBy(o => o.codCarteira)
                                .ToList<Processamento>();                
            }

            return lstInnerProc;
        }
        #endregion Public Methods

        #region Private Methods

        private List<EnquadramentoDB> SelecionarRegistroAlterar(EnquadramentoDB enquadramento)
        {
            var lstEnq = new List<EnquadramentoDB>();

            if (enquadramento.codSubTipoFiltro.Equals(16) && !string.IsNullOrEmpty(enquadramento.codAtivo)) //Bloqueio de Ativos
            {
                lstEnq = (from re in dbQC.ResultadoEnquadramento
                          where
                           re.dtResultado.Equals(enquadramento.dtResultado) &&
                           re.codSubTipoFiltro.Equals(enquadramento.codSubTipoFiltro) &&
                           re.codAtivo.Equals(enquadramento.codAtivo) &&
                           re.codTipoAtivo.Equals(enquadramento.codTipoAtivo)
                          select (re)
                         ).ToList<EnquadramentoDB>();
            }
            else
            {
                lstEnq = (from re in dbQC.ResultadoEnquadramento
                          where
                           re.dtResultado.Equals(enquadramento.dtResultado) &&
                           re.codSubTipoFiltro.Equals(enquadramento.codSubTipoFiltro) &&
                           re.codCarteira.Trim().Equals(enquadramento.codCarteira.Trim())                            
                          select (re)
                         ).ToList<EnquadramentoDB>();
            }

            return lstEnq;
        }

        //Recupera o valor dos patrimonios existentes pela data
        private List<Processamento> RecuperaValorPatrimonio(DateTime dtResultado)
        {
            var lstCodCarteira = from cc in dbWM_DB.CarteiraCota
                                 where cc.dtCota.Equals(dtResultado)
                                 select (cc.codCarteira.Trim());

            var lstVlrPatrimonio = (from cc in dbWM_DB.CarteiraCota                                    
                                    where
                                     cc.dtCota.Equals(dtResultado) &&                                     
                                     lstCodCarteira.Contains(cc.codCarteira.Trim())
                                    select new Processamento
                                    {
                                        codCarteira   = cc.codCarteira,
                                        dtResultado   = cc.dtCota,                                        
                                        vlPatrimonial = (cc.vlPatrimonio == null ? 0 : cc.vlPatrimonio)
                                    });

            return lstVlrPatrimonio.ToList<Processamento>(); 
        }

        //Recupera o valor dos patrimonios 
        private List<Processamento> RecuperaValorPatrimonioDetalhe()
        {
            var lstVlrPatrimonio = (from cc in dbWM_DB.CarteiraCota                                    
                                    select new Processamento
                                    {
                                        codCarteira = cc.codCarteira,
                                        dtResultado = cc.dtCota,
                                        vlPatrimonial = (cc.vlPatrimonio == null ? 0 : cc.vlPatrimonio)
                                    });

            return lstVlrPatrimonio.ToList<Processamento>();
        }

        private List<Processamento> RecuperaCarteira()
        {
            var lstCarteira = (from cc in dbWM_DB.Carteira                               
                               select new Processamento
                               {
                                    codCarteira = cc.codCarteira,
                                    nomeCarteira = cc.nomeCarteira
                               });

            return lstCarteira.ToList<Processamento>(); 
        }

        private List<Processamento> RecuperaAtivo()
        {            
            var lstAtivo = (from at in dbWM_DB.Ativo                            
                            select new Processamento
                            {
                                codAtivo     = at.codAtivo,
                                codTipoAtivo = at.codTipoAtivo,
                                nomeAtivo    = at.nomeAtivo
                            }).ToList<Processamento>(); 

            return lstAtivo;
        }

        private List<Processamento> RecuperaPosicao()
        {
            var lstPosicao = (from pos in dbWM_DB.Posicao                                                    
                              select new Processamento
                              {
                                  codCarteira = pos.codCarteira,
                                  dtPosicao = pos.dtPosicao,
                                  codAtivo = pos.codAtivo,
                                  codTipoAtivo = pos.codTipoAtivo,
                                  vlrSaldoBruto = (pos.vlrSaldoBruto == null ? 0 : pos.vlrSaldoBruto)                                   
                              }).ToList<Processamento>();

            return lstPosicao;
        }

        private List<Ativo> SelecionarAtivoDetalheItem(Processamento processamento)
        {
            var resAtivo =
                   from at in dbWM_DB.Ativo
                   where
                    at.codAtivo.Trim() == processamento.codAtivo.Trim() &&
                    at.codTipoAtivo.Trim() == processamento.codTipoAtivo.Trim()
                   select (at);

            return resAtivo.ToList<Ativo>();             
        }

        private List<Carteira> SelecionarCarteira(List<string> lstCodCarteira)
        {
            var resCC = from c in dbWM_DB.Carteira
                        where 
                         c.inAtivoInativo.Trim().ToUpper().Equals("A") &&
                         lstCodCarteira.Contains(c.codCarteira.Trim())
                        select (c);

            return resCC.ToList<Carteira>();          
        }
        #endregion Private Methods
    }
}