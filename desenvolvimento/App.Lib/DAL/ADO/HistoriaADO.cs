using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.Entity;
using Dapper;
using System.Linq;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Models;


namespace BradescoNext.Lib.DAL.ADO
{

    public class HistoriaADO : ADOSuper, IHistoriaDAL
    {

        private Int32 _totalRegistros = 0;
        private List<HistoriaReportModel> historiasReportadas = null;
        private IList<HistoriaVizualizaModel> historiasComMidias = null;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public void InserirLog(int id)
        {
            string SQL = @" INSERT INTO LogHistoria (HistoriaID
                          ,IndicadoID
                          ,IndicadorFacebookID
                          ,IndicadorFacebookToken
                          ,IndicadorNome
                          ,IndicadorEmailCrt
                          ,IndicadoNome
                          ,IndicadoEmailCrt
                          ,IndicadoAprovacao
                          ,TriagemAprovacao
                          ,TriagemAprovacaoNormal
                          ,TriagemAprovacaoSupervisor
                          ,Titulo
                          ,Texto
                          ,HistoriaCategoriaID
                          ,CodigoIndicado
                          ,CodigoIndicadoResponsavel
                          ,DataCadastro
                          ,DataModificacao
                          ,UsuarioID
                          ,TriagemAprovacaoAbuso
                          ,QuantidadeAbuso
                          ,AlteracaoOrigem
                          ,ComentarioIndicadoOriginal
                          ,ComentarioIndicadoResponsavel
                          ,ComentarioIndicado
                          ,ComentarioTriagemNormal
                          ,ComentarioTriagemSupervisor
                          ,ComentarioTriagemAbuso
                          ,ComentarioTriagem
                          ,IndicadoAprovacaoOriginal
                          ,IndicadoAprovacaoResponsavel
                          ,IndicadorFotoArquivoNome
                          ,DataEmailAguardandoAprovacao
                          ,QuantidadeEmailAguardandoAprovacao
                          ,Nota
                          ,ModeracaoEncerrada
                          ,EmailGaleria) 
                (SELECT ID as HistoriaID,IndicadoID
                          ,IndicadorFacebookID
                          ,IndicadorFacebookToken
                          ,IndicadorNome
                          ,IndicadorEmailCrt
                          ,IndicadoNome
                          ,IndicadoEmailCrt
                          ,IndicadoAprovacao
                          ,TriagemAprovacao
                          ,TriagemAprovacaoNormal
                          ,TriagemAprovacaoSupervisor
                          ,Titulo
                          ,Texto
                          ,HistoriaCategoriaID
                          ,CodigoIndicado
                          ,CodigoIndicadoResponsavel
                          ,DataCadastro
                          ,DataModificacao
                          ,UsuarioID
                          ,TriagemAprovacaoAbuso
                          ,QuantidadeAbuso
                          ,AlteracaoOrigem
                          ,ComentarioIndicadoOriginal
                          ,ComentarioIndicadoResponsavel
                          ,ComentarioIndicado
                          ,ComentarioTriagemNormal
                          ,ComentarioTriagemSupervisor
                          ,ComentarioTriagemAbuso
                          ,ComentarioTriagem
                          ,IndicadoAprovacaoOriginal
                          ,IndicadoAprovacaoResponsavel
                          ,IndicadorFotoArquivoNome
                          ,DataEmailAguardandoAprovacao
                          ,QuantidadeEmailAguardandoAprovacao
                          ,Nota
                          ,ModeracaoEncerrada
                          ,EmailGaleria 
                FROM Historia
                WHERE ID = @ID) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        /// <summary> 
        /// Inclui dados na base 
        /// </summary> 
        /// <param name="entidade"></param> 
        public int Inserir(Historia entidade)
        {
            string SQL = @" INSERT INTO Historia     
                (IndicadoID,
                IndicadorFacebookID,
                IndicadorFacebookToken,
                IndicadorFotoArquivoNome,
                IndicadorNome,
                IndicadorEmailCrt,
                IndicadoNome,
                IndicadoEmailCrt,
                IndicadoAprovacao,
                TriagemAprovacao,
                TriagemAprovacaoNormal,
                TriagemAprovacaoSupervisor,
                Titulo,
                Texto,
                ComentarioIndicadoOriginal,
                ComentarioIndicadoResponsavel,
                ComentarioIndicado,
                ComentarioTriagemNormal,
                ComentarioTriagemAbuso,
                ComentarioTriagemSupervisor,
                ComentarioTriagem,
                HistoriaCategoriaID,
                CodigoIndicado,
                CodigoIndicadoResponsavel,
                DataCadastro,
                DataModificacao,
                UsuarioID,
                TriagemAprovacaoAbuso, 
                IndicadoAprovacaoOriginal,
                IndicadoAprovacaoResponsavel,
                AlteracaoOrigem, 
                QuantidadeAbuso ,
                DataEmailAguardandoAprovacao,
                QuantidadeEmailAguardandoAprovacao,
                Nota,
                ModeracaoEncerrada,
                EmailGaleria) 
                VALUES    
                (@IndicadoID,@IndicadorFacebookID,@IndicadorFacebookToken, @IndicadorFotoArquivoNome,
                @IndicadorNome," + Criptografar(entidade.IndicadorEmail) + @",
                @IndicadoNome," + Criptografar(entidade.IndicadoEmail) + @",@IndicadoAprovacaoDB,@TriagemAprovacaoDB,@TriagemAprovacaoNormalDB,
                @TriagemAprovacaoSupervisorDB,
                @Titulo,@Texto,@ComentarioIndicadoOriginal,@ComentarioIndicadoResponsavel,@ComentarioIndicado,
                @ComentarioTriagemNormal,@ComentarioTriagemAbuso,@ComentarioTriagemSupervisor,@ComentarioTriagem,@HistoriaCategoriaID,
                @CodigoIndicado,
                @CodigoIndicadoResponsavel,@DataCadastro,@DataModificacao,@UsuarioID,
                @TriagemAprovacaoAbusoDB, @IndicadoAprovacaoOriginalDB,
                @IndicadoAprovacaoResponsavelDB, @AlteracaoOrigemDB, @QuantidadeAbuso,
                @DataEmailAguardandoAprovacao,@QuantidadeEmailAguardandoAprovacao,@Nota,@ModeracaoEncerrada,@EmailGaleria); " +
                " SELECT ID = SCOPE_IDENTITY()";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade.ID = con.Query<int>(SQL, entidade).FirstOrDefault();
                con.Close();
            }
            return entidade.ID;
        }


        /// <summary> 
        /// Método que atualiza os dados entidade. 
        /// </summary> 
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        public void Atualizar(Historia entidade)
        {
            string SQL = @"UPDATE Historia SET  
                IndicadoID=@IndicadoID,
                IndicadorFacebookID=@IndicadorFacebookID,
                IndicadorFotoArquivoNome=@IndicadorFotoArquivoNome,
                IndicadorFacebookToken=@IndicadorFacebookToken,IndicadorNome=@IndicadorNome,
                IndicadorEmailCrt=" + Criptografar(entidade.IndicadorEmail) + @",
                IndicadoNome=@IndicadoNome,
                IndicadoEmailCrt=" + Criptografar(entidade.IndicadoEmail) + @",
                IndicadoAprovacao=@IndicadoAprovacaoDB,
                TriagemAprovacao=@TriagemAprovacaoDB,
                TriagemAprovacaoNormal=@TriagemAprovacaoNormalDB,
                TriagemAprovacaoSupervisor=@TriagemAprovacaoSupervisorDB,
                TriagemAprovacaoAbuso=@TriagemAprovacaoAbusoDB,
                QuantidadeAbuso=@QuantidadeAbuso,
                Titulo=@Titulo,
                Texto=@Texto,
                ComentarioIndicadoOriginal=@ComentarioIndicadoOriginal,
                ComentarioIndicadoResponsavel=@ComentarioIndicadoResponsavel,
                ComentarioIndicado=@ComentarioIndicado,
                ComentarioTriagemNormal=@ComentarioTriagemNormal,
                ComentarioTriagemSupervisor=@ComentarioTriagemSupervisor,
                ComentarioTriagem=@ComentarioTriagem,
                ComentarioTriagemAbuso=@ComentarioTriagemAbuso,
                HistoriaCategoriaID=@HistoriaCategoriaID,
                CodigoIndicado=@CodigoIndicado,
                CodigoIndicadoResponsavel=@CodigoIndicadoResponsavel,
                DataCadastro=@DataCadastro,
                DataModificacao=@DataModificacao,
                UsuarioID=@UsuarioID, 
                IndicadoAprovacaoOriginal=@IndicadoAprovacaoOriginalDB, 
                IndicadoAprovacaoResponsavel=@IndicadoAprovacaoResponsavelDB, 
                AlteracaoOrigem=@AlteracaoOrigemDB,
                DataEmailAguardandoAprovacao=@DataEmailAguardandoAprovacao,
                QuantidadeEmailAguardandoAprovacao=@QuantidadeEmailAguardandoAprovacao,
                Nota=@Nota,
                ModeracaoEncerrada=@ModeracaoEncerrada,
                EmailGaleria=@EmailGaleria
                WHERE ID=@ID ";

            

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void AtualizarConfirmacaoResponsavel(Historia entidade)
        {
            string SQL = @"UPDATE Historia SET  
                AlteracaoOrigem=@AlteracaoOrigemDB,
                IndicadoAprovacao=@IndicadoAprovacao,
                TriagemAprovacao=@TriagemAprovacao,
                ComentarioIndicadoResponsavel=@ComentarioIndicadoResponsavel,
                DataModificacao=@DataModificacao
    WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public Historia CarregaPenultimaHistoria(int ultimaHistoriaID)
        {
            Historia entidadeRetorno = null;

            string SQL = @"SELECT ID
              ,IndicadoID
              ,IndicadorFacebookID
              ,IndicadorFacebookToken
              ,IndicadorNome
              ," + Decriptografar("IndicadorEmailCrt") + @" as IndicadorEmail
              ,IndicadoNome
              ," + Decriptografar("IndicadoEmailCrt") + @" as IndicadoEmail
              ,IndicadoAprovacao as IndicadoAprovacaoDB
              ,TriagemAprovacao as TriagemAprovacaoDB
              ,TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,Titulo
              ,Texto
              ,HistoriaCategoriaID
              ,CodigoIndicado
              ,CodigoIndicadoResponsavel
              ,DataCadastro
              ,DataModificacao
              ,UsuarioID
              ,TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,QuantidadeAbuso
              ,AlteracaoOrigem as AlteracaoOrigemDB
              ,ComentarioIndicadoOriginal
              ,ComentarioIndicadoResponsavel
              ,ComentarioIndicado
              ,ComentarioTriagemNormal
              ,ComentarioTriagemSupervisor
              ,ComentarioTriagemAbuso
              ,ComentarioTriagem
              ,IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,IndicadorFotoArquivoNome
              ,DataEmailAguardandoAprovacao
              ,QuantidadeEmailAguardandoAprovacao
              ,Nota
              ,ModeracaoEncerrada
          FROM Historia (NOLOCK) WHERE IndicadoID = @ID";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Historia>(SQL, new { ID = ultimaHistoriaID }).FirstOrDefault();

                con.Close();
            }
            return entidadeRetorno;
        }
        public Historia CarregarUltimaHistoria(int indicadoId)
        {
            Historia entidadeRetorno = null;

            string SQL = @"SELECT ID
              ,IndicadoID
              ,IndicadorFacebookID
              ,IndicadorFacebookToken
              ,IndicadorNome
              ," + Decriptografar("IndicadorEmailCrt") + @" as IndicadorEmail
              ,IndicadoNome
              ," + Decriptografar("IndicadoEmailCrt") + @" as IndicadoEmail
              ,IndicadoAprovacao as IndicadoAprovacaoDB
              ,TriagemAprovacao as TriagemAprovacaoDB
              ,TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,Titulo
              ,Texto
              ,HistoriaCategoriaID
              ,CodigoIndicado
              ,CodigoIndicadoResponsavel
              ,DataCadastro
              ,DataModificacao
              ,UsuarioID
              ,TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,QuantidadeAbuso
              ,AlteracaoOrigem as AlteracaoOrigemDB
              ,ComentarioIndicadoOriginal
              ,ComentarioIndicadoResponsavel
              ,ComentarioIndicado
              ,ComentarioTriagemNormal
              ,ComentarioTriagemSupervisor
              ,ComentarioTriagemAbuso
              ,ComentarioTriagem
              ,IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,IndicadorFotoArquivoNome
              ,DataEmailAguardandoAprovacao
              ,QuantidadeEmailAguardandoAprovacao
              ,Nota
              ,ModeracaoEncerrada
          FROM Historia (NOLOCK) WHERE IndicadoID = @ID";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Historia>(SQL, new { ID = indicadoId }).FirstOrDefault();

                con.Close();
            }
            return entidadeRetorno;
        }
        /// <summary> 
        /// Método que carrega uma entidade. 
        /// </summary> 
        /// <param name="entidade">Entidade a ser carregada (somente o identificador é necessário).</param> 
        /// <returns></returns> 
        public Historia Carregar(int id)
        {
            Historia entidadeRetorno = null;

            string SQL = @"SELECT H.ID
              ,H.IndicadoID
              ,H.IndicadorFacebookID
              ,H.IndicadorFacebookToken
              ,H.IndicadorNome
              ," + Decriptografar("H.IndicadorEmailCrt") + @" as IndicadorEmail
              ,H.IndicadoNome
              ," + Decriptografar("H.IndicadoEmailCrt") + @" as IndicadoEmail
              ,H.IndicadoAprovacao as IndicadoAprovacaoDB
              ,H.TriagemAprovacao as TriagemAprovacaoDB
              ,H.TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,H.TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,H.Titulo
              ,H.Texto
              ,H.HistoriaCategoriaID
              ,H.CodigoIndicado
              ,H.CodigoIndicadoResponsavel
              ,H.DataCadastro
              ,H.DataModificacao
              ,H.UsuarioID
              ,H.TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,H.QuantidadeAbuso
              ,H.AlteracaoOrigem as AlteracaoOrigemDB
              ,H.ComentarioIndicadoOriginal
              ,H.ComentarioIndicadoResponsavel
              ,H.ComentarioIndicado
              ,H.ComentarioTriagemNormal
              ,H.ComentarioTriagemSupervisor
              ,H.ComentarioTriagemAbuso
              ,H.ComentarioTriagem
              ,H.IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,H.IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,H.IndicadorFotoArquivoNome
              ,H.DataEmailAguardandoAprovacao
              ,H.QuantidadeEmailAguardandoAprovacao
              ,H.Nota
              ,H.ModeracaoEncerrada
              ,H.EmailGaleria
          FROM Historia (NOLOCK) H 
            WHERE H.ID = @ID";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Historia>(SQL, new { ID = id }).FirstOrDefault();
                con.Close();
            }
            return entidadeRetorno;
        }
        public IList<Historia> ListarAprovadasTriagemNaoModeradas(int ultimaHistoriaModeradaID, int indicadoID)
        {
            IList<Historia> entidadeRetorno = null;

            string SQL = @"SELECT ID
              ,IndicadoID
              ,IndicadorFacebookID
              ,IndicadorFacebookToken
              ,IndicadorNome
              ," + Decriptografar("IndicadorEmailCrt") + @" as IndicadorEmail
              ,IndicadoNome
              ," + Decriptografar("IndicadoEmailCrt") + @" as IndicadoEmail
              ,IndicadoAprovacao as IndicadoAprovacaoDB
              ,TriagemAprovacao as TriagemAprovacaoDB
              ,TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,Titulo
              ,Texto
              ,HistoriaCategoriaID
              ,CodigoIndicado
              ,CodigoIndicadoResponsavel
              ,DataCadastro
              ,DataModificacao
              ,UsuarioID
              ,TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,QuantidadeAbuso
              ,AlteracaoOrigem as AlteracaoOrigemDB
              ,ComentarioIndicadoOriginal
              ,ComentarioIndicadoResponsavel
              ,ComentarioIndicado
              ,ComentarioTriagemNormal
              ,ComentarioTriagemSupervisor
              ,ComentarioTriagemAbuso
              ,ComentarioTriagem
              ,IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,IndicadorFotoArquivoNome
              ,DataEmailAguardandoAprovacao
              ,QuantidadeEmailAguardandoAprovacao
              ,Nota
              ,ModeracaoEncerrada
          FROM Historia (NOLOCK) as historia WHERE historia.DataCadastro > (SELECT TOP 1 DataCadastro FROM Historia WHERE ID = @UltimaHistoriaIDModerada) 
          AND historia.IndicadoID = @IndicadoID
          AND (historia.TriagemAprovacao = 'A' OR historia.TriagemAprovacao = 'R')";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Historia>(SQL, new { UltimaHistoriaIDModerada = ultimaHistoriaModeradaID, IndicadoID = indicadoID }).ToList();

                con.Close();
            }
            return entidadeRetorno;
        }
        public int CarregaIDUltimaHistoriaModerada(int? indicadoID)
        {
            int id = 0;

            string SQL = @"SELECT TOP 1 ID
            FROM Historia (NOLOCK) WHERE IndicadoID=@Codigo AND AlteracaoOrigem=@AlteracaoOrigem ORDER BY DataModificacao DESC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                id = con.Query<int>(SQL,
                    new
                    {
                        Codigo = indicadoID,
                        AlteracaoOrigem = enumAlteracaoOrigem.Moderacao
                    }).FirstOrDefault();

                con.Close();
            }

            return id;
        }

        /// <summary> 
        /// Método que carrega uma entidade a partir do codigoIndicado. 
        /// </summary> 
        /// <param name="codigoIndicado">Entidade a ser carregada (somente o identificador é necessário).</param> 
        /// <returns></returns> 
        public Historia Carregar(string codigo)
        {
            Historia entidadeRetorno = null;

            string SQL = @"SELECT ID
              ,IndicadoID
              ,IndicadorFacebookID
              ,IndicadorFacebookToken
              ,IndicadorNome
              ," + Decriptografar("IndicadorEmailCrt") + @" as IndicadorEmail
              ,IndicadoNome
              ," + Decriptografar("IndicadoEmailCrt") + @" as IndicadoEmail
              ,IndicadoAprovacao as IndicadoAprovacaoDB
              ,TriagemAprovacao as TriagemAprovacaoDB
              ,TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,Titulo
              ,Texto
              ,HistoriaCategoriaID
              ,CodigoIndicado
              ,CodigoIndicadoResponsavel
              ,DataCadastro
              ,DataModificacao
              ,UsuarioID
              ,TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,QuantidadeAbuso
              ,AlteracaoOrigem as AlteracaoOrigemDB
              ,ComentarioIndicadoOriginal
              ,ComentarioIndicadoResponsavel
              ,ComentarioIndicado
              ,ComentarioTriagemNormal
              ,ComentarioTriagemSupervisor
              ,ComentarioTriagemAbuso
              ,ComentarioTriagem
              ,IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,IndicadorFotoArquivoNome
              ,DataEmailAguardandoAprovacao
              ,QuantidadeEmailAguardandoAprovacao
              ,Nota
              ,ModeracaoEncerrada
              ,EmailGaleria
          FROM Historia (NOLOCK) WHERE CodigoIndicado=@Codigo OR CodigoIndicadoResponsavel=@Codigo";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Historia>(SQL, new { Codigo = codigo }).FirstOrDefault();

                con.Close();
            }
            return entidadeRetorno;
        }

        /// <summary> 
        /// Método que retorna todas as entidades. 
        /// SELECT * FROM Historia ORDER BY ID DESC 
        /// </summary> 
        public IList<Historia> Listar(int indicadoID)
        {
            IList<Historia> entidadeRetorno = new List<Historia>();

            string SQLDados = @"SELECT H.ID, H.Titulo, H.Texto, H.ComentarioTriagem, H.IndicadoID, HC.ID, HC.Nome
	                                FROM Historia (NOLOCK) H, HistoriaCategoria (NOLOCK) HC
                                    WHERE H.HistoriaCategoriaID = HC.ID
	                                AND H.IndicadoID = @IndicadoID
                                    AND (H.TriagemAprovacao = @TriagemAprovacao 
                                        OR H.TriagemAprovacao = @TriagemAprovacaoRessalva)";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidadeRetorno = con.Query<Historia, HistoriaCategoria, Historia>(SQLDados,
                    (historia, historiaCategoria) =>
                    {
                        historia.HistoriaCategoria = historiaCategoria;
                        return historia;
                    },
                    new
                    {
                        IndicadoID = indicadoID,
                        TriagemAprovacao = enumAprovacao.aprovado.ValueAsString(),
                        TriagemAprovacaoRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString()
                    }).ToList();
                con.Close();
            }

            return entidadeRetorno;
        }
        public IList<HistoriaVizualizaModel> ListarHistoriasComMidias(int indicadoID, bool mostraTodos)
        {
            historiasComMidias = new List<HistoriaVizualizaModel>();

            string SQLDados = @"SELECT 
                     Historia.ID
                    ,Historia.Titulo
                    ,Historia.Texto
                    ,Historia.QuantidadeAbuso
                    ,Historia.IndicadoAprovacao as IndicadoAprovacaoDB
                    ,Historia.TriagemAprovacao as TriagemAprovacaoDB
                    ,Historia.ModeracaoEncerrada
                    ,Historia.Nota
                    ,Historia.IndicadorFacebookID
                    ,Historia.IndicadorFotoArquivoNome
                    ,Historia.IndicadorNome
                    ," + Decriptografar("Historia.IndicadorEmailCrt") + @" as IndicadorEmail
                    ,Midia.ID
                    ,Midia.ArquivoNome
                    ,Midia.ArquivoTipo as ArquivoTipoDB
                    ,Categoria.ID
                    ,Categoria.Nome
                FROM 
                    Historia (NOLOCK) Historia
                    LEFT JOIN HistoriaMidia (NOLOCK) Midia ON (Midia.HistoriaID = Historia.ID AND Midia.Inativo = 0)
                    INNER JOIN HistoriaCategoria (NOLOCK) Categoria ON (Historia.HistoriaCategoriaID = Categoria.ID)
                WHERE Historia.IndicadoID = @indicadoID ";
            if (!mostraTodos)
            {
                SQLDados += " AND Historia.ModeracaoEncerrada = 1 ";
            }
            SQLDados += " ORDER BY Historia.Nota DESC, Historia.DataCadastro DESC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Query<Historia, HistoriaMidia, HistoriaCategoria, int>(SQLDados, addResultHistoriasIndicado,
                    new
                    {
                        IndicadoID = indicadoID
                    }).ToList();
                con.Close();
            }


            return historiasComMidias;
        }
        private int addResultHistoriasIndicado(Historia historia, HistoriaMidia midia, HistoriaCategoria categoria)
        {
            HistoriaVizualizaModel historiaAdicionada = historiasComMidias.Where(x => x.Historia.ID == historia.ID).FirstOrDefault();

            if (historiaAdicionada != null)
            {
                if (historiaAdicionada.Midias == null)
                {
                    historiaAdicionada.Midias = new List<HistoriaMidia>();
                }
                if (midia != null)
                {
                    historiaAdicionada.Midias.Add(midia);
                }
            }
            else
            {
                historiaAdicionada = new HistoriaVizualizaModel();
                historiaAdicionada.Historia = historia;
                historiaAdicionada.Historia.HistoriaCategoria = categoria;

                if (historiaAdicionada.Midias == null)
                {
                    historiaAdicionada.Midias = new List<HistoriaMidia>();
                }
                if (midia != null)
                {
                    historiaAdicionada.Midias.Add(midia);
                }
                historiasComMidias.Add(historiaAdicionada);
            }
            return 1;
        }
        public IList<ExportaHistoriaModel> ListarExcel()
        {
            IList<ExportaHistoriaModel> list = new List<ExportaHistoriaModel>();

            string SQL = @"SELECT H.ID
              ,H.IndicadoID
              ,H.IndicadorFacebookID
              ,H.IndicadorFacebookToken
              ,H.IndicadorNome
              ," + Decriptografar("H.IndicadorEmailCrt") + @" as IndicadorEmail
              ,H.IndicadoNome
              ," + Decriptografar("H.IndicadoEmailCrt") + @" as IndicadoEmail
              ,H.IndicadoAprovacao
              ,H.TriagemAprovacao 
              ,H.TriagemAprovacaoNormal 
              ,H.TriagemAprovacaoSupervisor 
              ,H.Titulo
              ,H.Texto
              ,H.HistoriaCategoriaID
              ,H.CodigoIndicado
              ,H.CodigoIndicadoResponsavel
              ,H.DataCadastro
              ,H.DataModificacao
              ,H.UsuarioID
              ,H.TriagemAprovacaoAbuso 
              ,H.QuantidadeAbuso
              ,H.AlteracaoOrigem 
              ,H.ComentarioIndicadoOriginal
              ,H.ComentarioIndicadoResponsavel
              ,H.ComentarioIndicado
              ,H.ComentarioTriagemNormal
              ,H.ComentarioTriagemSupervisor
              ,H.ComentarioTriagemAbuso
              ,H.ComentarioTriagem
              ,H.IndicadoAprovacaoOriginal 
              ,H.IndicadoAprovacaoResponsavel 
              ,H.IndicadorFotoArquivoNome
              ,H.DataEmailAguardandoAprovacao
              ,H.QuantidadeEmailAguardandoAprovacao
              ,H.Nota
              ,H.ModeracaoEncerrada
              ,H.EmailGaleria
              ,HC.Nome as CategoriaNome
              ,U.Nome as UsuarioNome
                FROM Historia (NOLOCK) H 
                INNER JOIN HistoriaCategoria (NOLOCK) HC ON HC.ID = H.HistoriaCategoriaID
                LEFT JOIN Usuario (NOLOCK) U ON U.ID = H.UsuarioID
                ORDER BY H.ID ASC";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<ExportaHistoriaModel>(SQL).ToList();
                con.Close();
            }
            return list;
        }
        /// <summary> 
        /// Método que retorna todas as entidades. 
        /// SELECT * FROM Historia ORDER BY ID DESC 
        /// </summary> 
        public IList<Historia> Listar(Int32 skip, Int32 take, int indicadoID)
        {
            IList<Historia> list = null;
            string SQLCount = @"SELECT COUNT(*) FROM Historia (NOLOCK) WHERE (IndicadoID =@IndicadoID)";

            string SQL = @"WITH Members AS (SELECT Historia.ID
            ,Historia.IndicadoID
            ,Historia.IndicadorFacebookID
            ,Historia.IndicadorFacebookToken
            ,Historia.IndicadorNome
            ,Historia.IndicadorEmailCrt
            ,Historia.IndicadoNome
            ,Historia.IndicadoEmailCrt
            ,Historia.IndicadoAprovacao
            ,Historia.TriagemAprovacao
            ,Historia.TriagemAprovacaoNormal
            ,Historia.TriagemAprovacaoSupervisor
            ,Historia.Titulo
            ,Historia.Texto
            ,Historia.ComentarioIndicadoOriginal
            ,Historia.ComentarioIndicadoResponsavel
            ,Historia.ComentarioIndicado
            ,Historia.ComentarioTriagemNormal
            ,Historia.ComentarioTriagemSupervisor
            ,Historia.ComentarioTriagem
            ,Historia.HistoriaCategoriaID
            ,Historia.CodigoIndicado
            ,Historia.CodigoIndicadoResponsavel
            ,Historia.DataCadastro
            ,Historia.DataModificacao
            ,Historia.UsuarioID
            ,Historia.Nota
            ,Historia.ModeracaoEncerrada
            ,Historia.EmailGaleria
            , ROW_NUMBER() OVER ( ORDER BY ID DESC) AS RowNumber  FROM Historia (NOLOCK)
            WHERE IndicadoID = @IndicadoID ";


            SQL += @"  )  SELECT	RowNumber, ID
            ,IndicadoID
            ,IndicadorFacebookID
            ,IndicadorFacebookToken
            ,IndicadorNome
            ,IndicadorEmailCrt
            ,IndicadoNome
            ,IndicadoEmailCrt
            ,IndicadoAprovacao
            ,TriagemAprovacao
            ,TriagemAprovacaoNormal
            ,TriagemAprovacaoSupervisor
            ,Titulo
            ,Texto
            ,ComentarioIndicadoOriginal
            ,ComentarioIndicadoResponsavel
            ,ComentarioIndicado
            ,ComentarioTriagemNormal
            ,ComentarioTriagemSupervisor
            ,ComentarioTriagem
            ,HistoriaCategoriaID
            ,CodigoIndicado
            ,CodigoIndicadoResponsavel
            ,DataCadastro
            ,DataModificacao
            ,UsuarioID
            FROM	Members
                WHERE RowNumber <= (1+(@paginaAtual-1)) * @totalRegPagina  AND  RowNumber >= 1+((@paginaAtual-1)* @totalRegPagina)           ORDER BY RowNumber ASC; ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { IndicadoID = indicadoID }).FirstOrDefault();

                list = con.Query<Historia>(SQL, new { Skip = skip, Take = take }).ToList();
                con.Close();
            }
            return list;
        }


        public int CheckCodigo(string codigoIndicado)
        {
            int result = 0;
            string SQL = @"SELECT COUNT(ID) 
								FROM Historia (NOLOCK)
								WHERE CodigoIndicado=@Codigo or CodigoIndicadoResponsavel=@Codigo";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                result = con.Query<int>(SQL, new { Codigo = codigoIndicado }).FirstOrDefault();

                con.Close();
            }
            return result;
        }

        public void RemoverIndicadorFoto(int id)
        {
            string SQL = @"UPDATE Historia SET  
                IndicadorFotoArquivoNome = NULL
                WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        public void AtualizarAguardandoAprovacao(Historia entidade)
        {

            string SQL = @"UPDATE Historia SET DataEmailAguardandoAprovacao=@DataEmailAguardandoAprovacao, QuantidadeEmailAguardandoAprovacao=@QuantidadeEmailAguardandoAprovacao
                                    WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void AtualizarGaleria(int id)
        {

            string SQL = @"UPDATE Historia SET EmailGaleria=1 WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        public IList<Historia> CarregaHistoriasAguardandoResponsavel(DateTime dataAtual, int roboEmailQtdDiasEnvioAguardando, int roboEmailQtdEnvioAguardando)
        {
            IList<Historia> list = new List<Historia>();

            string SQL = @"SELECT ID
              ,IndicadoID
              ,IndicadorFacebookID
              ,IndicadorFacebookToken
              ,IndicadorNome
              ," + Decriptografar("IndicadorEmailCrt") + @" as IndicadorEmail
              ,IndicadoNome
              ," + Decriptografar("IndicadoEmailCrt") + @" as IndicadoEmail
              ,IndicadoAprovacao as IndicadoAprovacaoDB
              ,TriagemAprovacao as TriagemAprovacaoDB
              ,TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,Titulo
              ,Texto
              ,HistoriaCategoriaID
              ,CodigoIndicado
              ,CodigoIndicadoResponsavel
              ,DataCadastro
              ,DataModificacao
              ,UsuarioID
              ,TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,QuantidadeAbuso
              ,AlteracaoOrigem as AlteracaoOrigemDB
              ,ComentarioIndicadoOriginal
              ,ComentarioIndicadoResponsavel
              ,ComentarioIndicado
              ,ComentarioTriagemNormal
              ,ComentarioTriagemSupervisor
              ,ComentarioTriagemAbuso
              ,ComentarioTriagem
              ,IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,IndicadorFotoArquivoNome
              ,DataEmailAguardandoAprovacao
              ,QuantidadeEmailAguardandoAprovacao
              ,Nota
              ,ModeracaoEncerrada
            FROM Historia (NOLOCK)
            WHERE ((DATEDIFF(DAY, DataEmailAguardandoAprovacao, @DataEmailAguardandoAprovacao) >= @RoboEmailQtdDiasEnvioAguardando/2 AND QuantidadeEmailAguardandoAprovacao < @RoboEmailQtdEnvioAguardando) 
                 OR DATEDIFF(DAY, DataEmailAguardandoAprovacao, @DataEmailAguardandoAprovacao) >= @RoboEmailQtdDiasEnvioAguardando)
            AND IndicadoAprovacaoResponsavel = 'P'  ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Historia>(SQL, new
                {
                    DataEmailAguardandoAprovacao = dataAtual,
                    roboEmailQtdDiasEnvioAguardando = roboEmailQtdDiasEnvioAguardando,
                    roboEmailQtdEnvioAguardando = roboEmailQtdEnvioAguardando
                }).ToList();

                con.Close();
            }
            return list;
        }

        public IList<Historia> CarregaHistoriasAguardandoIndicado(DateTime dataAtual, int roboEmailQtdDiasEnvioAguardando, int roboEmailQtdEnvioAguardando)
        {
            IList<Historia> list = new List<Historia>();

            string SQL = @"SELECT ID
              ,IndicadoID
              ,IndicadorFacebookID
              ,IndicadorFacebookToken
              ,IndicadorNome
              ," + Decriptografar("IndicadorEmailCrt") + @" as IndicadorEmail
              ,IndicadoNome
              ," + Decriptografar("IndicadoEmailCrt") + @" as IndicadoEmail
              ,IndicadoAprovacao as IndicadoAprovacaoDB
              ,TriagemAprovacao as TriagemAprovacaoDB
              ,TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,Titulo
              ,Texto
              ,HistoriaCategoriaID
              ,CodigoIndicado
              ,CodigoIndicadoResponsavel
              ,DataCadastro
              ,DataModificacao
              ,UsuarioID
              ,TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,QuantidadeAbuso
              ,AlteracaoOrigem as AlteracaoOrigemDB
              ,ComentarioIndicadoOriginal
              ,ComentarioIndicadoResponsavel
              ,ComentarioIndicado
              ,ComentarioTriagemNormal
              ,ComentarioTriagemSupervisor
              ,ComentarioTriagemAbuso
              ,ComentarioTriagem
              ,IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,IndicadorFotoArquivoNome
              ,DataEmailAguardandoAprovacao
              ,QuantidadeEmailAguardandoAprovacao
              ,Nota
              ,ModeracaoEncerrada
              ,EmailGaleria
            FROM Historia (NOLOCK)
            WHERE ((DATEDIFF(DAY, DataEmailAguardandoAprovacao, @DataEmailAguardandoAprovacao) >= @RoboEmailQtdDiasEnvioAguardando/2 AND QuantidadeEmailAguardandoAprovacao < @RoboEmailQtdEnvioAguardando)
                 OR DATEDIFF(DAY, DataEmailAguardandoAprovacao, @DataEmailAguardandoAprovacao) >= @RoboEmailQtdDiasEnvioAguardando)
            AND IndicadoAprovacaoOriginal = @IndicadoAprovacaoOriginal";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Historia>(SQL, new
                {
                    DataEmailAguardandoAprovacao = dataAtual,
                    RoboEmailQtdDiasEnvioAguardando = roboEmailQtdDiasEnvioAguardando,
                    RoboEmailQtdEnvioAguardando = roboEmailQtdEnvioAguardando,
                    IndicadoAprovacaoOriginal = enumAprovacao.pendente.ValueAsString()
                }).ToList();

                con.Close();

            }
            return list;
        }

        public IList<Historia> CarregaHistoriasAguardandoGaleria()
        {
            IList<Historia> list = new List<Historia>();

            string SQL = @"SELECT ID
              ,IndicadoID
              ,IndicadorFacebookID
              ,IndicadorFacebookToken
              ,IndicadorNome
              ," + Decriptografar("IndicadorEmailCrt") + @" as IndicadorEmail
              ,IndicadoNome
              ," + Decriptografar("IndicadoEmailCrt") + @" as IndicadoEmail
              ,IndicadoAprovacao as IndicadoAprovacaoDB
              ,TriagemAprovacao as TriagemAprovacaoDB
              ,TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,Titulo
              ,Texto
              ,HistoriaCategoriaID
              ,CodigoIndicado
              ,CodigoIndicadoResponsavel
              ,DataCadastro
              ,DataModificacao
              ,UsuarioID
              ,TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,QuantidadeAbuso
              ,AlteracaoOrigem as AlteracaoOrigemDB
              ,ComentarioIndicadoOriginal
              ,ComentarioIndicadoResponsavel
              ,ComentarioIndicado
              ,ComentarioTriagemNormal
              ,ComentarioTriagemSupervisor
              ,ComentarioTriagemAbuso
              ,ComentarioTriagem
              ,IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,IndicadorFotoArquivoNome
              ,DataEmailAguardandoAprovacao
              ,QuantidadeEmailAguardandoAprovacao
              ,Nota
              ,ModeracaoEncerrada
              ,EmailGaleria
            FROM Historia (NOLOCK)
            WHERE Nota IS NOT NULL AND EmailGaleria = 0";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Historia>(SQL).ToList();

                con.Close();

            }
            return list;
        }

        public bool TriagemBloqueada(int id, enumPerfilNome perfil)
        {
            string MontaWhere = "";
            switch (perfil)
            {
                case enumPerfilNome.triagemComum:
                    {
                        MontaWhere = @"AND H.TriagemAprovacaoNormal = @TriagemAprovacaoIniciada
	                                   AND @DataAtual < (SELECT dateadd(minute, TriagemNormalMinutos, H.DataModificacao) FROM Configuracao) ";
                        break;
                    }

                case enumPerfilNome.triagemSupervisor:
                    {
                        MontaWhere = @"AND H.TriagemAprovacaoSupervisor = @TriagemAprovacaoIniciada
	                                   AND @DataAtual < (SELECT dateadd(minute, TriagemSupervisorMinutos, H.DataModificacao) FROM Configuracao) ";
                        break;
                    }
            };

            string SQL = @"SELECT H.ID
	                       FROM Historia (NOLOCK) H, Indicado (NOLOCK) I
	                       WHERE H.IndicadoID = I.ID
	                       AND H.ID = @ID " + MontaWhere;


            using (DbConnection con = _db.CreateConnection())
            {

                con.Open();
                return (con.Query<Historia>(SQL,
                    new
                    {
                        ID = id,
                        TriagemAprovacaoIniciada = enumAprovacao.aprovacaoIniciada.ValueAsString(),
                        TriagemAprovacao = enumAprovacao.aprovado.ValueAsString(),
                        TriagemAprovacaoComRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString(),
                        DataAtual = DateTime.Now
                    }).Count() > 0);
            }
        }

        /// <summary> 
        /// Método que carrega uma entidade apenas para Triagem comum e supervisor. 
        /// </summary> 
        /// <param name="entidade">Entidade a ser carregada (somente o identificador é necessário).</param> 
        /// <returns></returns> 
        public Historia CarregarTriagemModeracao(int id)
        {
            Historia entidadeRetorno = null;

            string SQLDados = @"SELECT H.ID
                                     , H.Titulo
                                     , H.Texto
                                     , H.DataCadastro
                                     , H.DataModificacao
                                     , H.UsuarioID
                                     , H.ComentarioTriagem
                                     , H.ComentarioIndicado
                                     , H.IndicadoID, H.UsuarioID
                                     , H.HistoriaCategoriaID
                                     , H.AlteracaoOrigem as AlteracaoOrigemDB
                                     , H.TriagemAprovacao as TriagemAprovacaoDB
                                     , H.TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
                                     , H.IndicadoAprovacao as IndicadoAprovacaoDB
                                     , H.EmailGaleria
                                     , H.IndicadoNome
                                     , H.ModeracaoEncerrada
                                     , H.Nota
                                     , H.IndicadorNome
                                     , H.IndicadorFotoArquivoNome
                                     , " + Decriptografar("H.IndicadorEmailCrt") + @" as IndicadorEmail
                                     , H.IndicadorFacebookID
                                     , H.IndicadorFacebookToken
                                     , H.CodigoIndicado
                                     , H.CodigoIndicadoResponsavel
                                     , HC.ID
                                     , HC.Nome
                                     , U.ID
                                     , U.Nome
                                     , U.Login
	                                FROM Historia (NOLOCK) H
                                    INNER JOIN HistoriaCategoria (NOLOCK) HC ON H.HistoriaCategoriaID = HC.ID
                                    LEFT JOIN Usuario (NOLOCK) U ON H.UsuarioID = U.ID
                                    WHERE H.ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidadeRetorno = con.Query<Historia, HistoriaCategoria, Usuario, Historia>(SQLDados,
                    (historia, historiaCategoria, usuario) =>
                    {
                        historia.HistoriaCategoria = historiaCategoria;
                        historia.Usuario = usuario;
                        return historia;
                    },
                    new { ID = id }).FirstOrDefault();
                con.Close();
            }

            return entidadeRetorno;
        }

        public void BloquearTriagem(int id, int usuarioID, enumPerfilNome perfil)
        {
            string MontaUpdate = "";
            switch (perfil)
            {
                case enumPerfilNome.triagemComum:
                    {
                        MontaUpdate = @", TriagemAprovacaoNormal = @TriagemAprovacaoIniciada";
                        break;
                    }

                case enumPerfilNome.triagemSupervisor:
                    {
                        MontaUpdate = @", TriagemAprovacaoSupervisor = @TriagemAprovacaoIniciada";
                        break;
                    }
            };

            string SQL = @"UPDATE Historia SET                            
                            DataModificacao = @DataAtual, 
                            UsuarioID = @UsuarioID
                            " + MontaUpdate + @"
                        WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL,
                    new
                    {
                        ID = id,
                        TriagemAprovacaoIniciada = enumAprovacao.aprovacaoIniciada.ValueAsString(),
                        DataAtual = DateTime.Now,
                        UsuarioID = usuarioID
                    });
                con.Close();
            }
        }


        /// <summary> 
        /// Método que retorna todas as entidades. 
        /// SELECT * FROM Historia ORDER BY ID DESC 
        /// </summary> 
        public IList<Historia> Listar(Int32 skip, Int32 take, int usuarioID, enumPerfilNome perfil, string palavraChave)
        {
            IList<Historia> list = new List<Historia>();

            string MontaWhere = "";
            switch (perfil)
            {
                case enumPerfilNome.triagemComum:
                    {
                        MontaWhere = @"AND I.ID is not null
                                       AND H.IndicadoAprovacao = @IndicadoAprovacaoAprovado
                                       AND H.TriagemAprovacao = @TriagemAprovacaoPendente
                                       AND ((H.TriagemAprovacaoNormal = @TriagemAprovacaoPendente)
                                                OR
		                                    (H.TriagemAprovacaoNormal = @TriagemAprovacaoIniciado AND 
                                            (@DataAtual > (SELECT DATEADD(MINUTE, TriagemNormalMinutos, H.DataModificacao) FROM Configuracao)
                                                OR 
                                             H.UsuarioID = @UsuarioID))
                                            )";
                        break;
                    }

                case enumPerfilNome.triagemSupervisor:
                    {
                        MontaWhere = @"AND I.ID is not null
                                       AND H.IndicadoAprovacao = @IndicadoAprovacaoAprovado
                                       AND H.TriagemAprovacao = @TriagemAprovacaoPendente
                                       AND ((H.TriagemAprovacaoSupervisor = @TriagemAprovacaoPendente)
                                                OR
		                                    (H.TriagemAprovacaoSupervisor = @TriagemAprovacaoIniciado AND 
                                            (@DataAtual > (SELECT DATEADD(MINUTE, TriagemSupervisorMinutos, H.DataModificacao) FROM Configuracao)
                                                OR 
                                             H.UsuarioID = @UsuarioID))
                                            )";
                        break;
                    }
                case enumPerfilNome.moderadores:
                    {
                        // busca histórias aprovadas
                        // e que ainda não estão moderadas
                        // e que o usuário logado ainda não moderou
                        MontaWhere = @"AND I.ID is not null
                                       AND H.IndicadoAprovacao = @IndicadoAprovacaoAprovado
                                       AND (H.TriagemAprovacao = @TriagemAprovacao
                                            OR
                                            H.TriagemAprovacao = @TriagemAprovacaoComRessalva)

                                       AND (SELECT COUNT(1)
	                                        FROM HistoriaModeracao (NOLOCK) HM
	                                        WHERE HM.HistoriaID = H.ID
                                            AND HM.DataFimAvaliacao IS NOT NULL) < (SELECT ModeracaoQuantidade FROM Configuracao (NOLOCK))

                                       AND NOT EXISTS (SELECT H.ID
	                                                   FROM HistoriaModeracao (NOLOCK) HM
	                                                   WHERE HM.HistoriaID = H.ID
                                                       AND HM.UsuarioID = @UsuarioID AND Nota is not null)";
                        break;
                    }
            };

            if (palavraChave.ToLower() == "facebook")
            {
                MontaWhere = "H.IndicadorFacebookID is not null " + MontaWhere;
            }
            else
            {
                MontaWhere = @"(ISNULL(I.Nome, H.IndicadoNome) like @palavraChave
                                OR " + Decriptografar("ISNULL(I.EmailCrt, H.IndicadoEmailCrt)") + @"  like @palavraChave
                                OR H.IndicadorNome like @palavraChave
                                OR H.IndicadorFacebookID like @palavraChave
                                OR H.Titulo like @palavraChave) " + MontaWhere;
            }

            string SQLCount = @"SELECT COUNT(1) 
                                FROM Historia (NOLOCK) H
                                LEFT JOIN Indicado (NOLOCK) I ON H.IndicadoID = I.ID
	                            WHERE " + MontaWhere;

            string SQL = @"SELECT H.ID
                                , ISNULL(I.Nome, H.IndicadoNome) AS IndicadoNome
                                , " + Decriptografar("ISNULL(I.EmailCrt, H.IndicadoEmailCrt)") + @" AS IndicadoEmail
                                , H.CodigoIndicado
                                , H.CodigoIndicadoResponsavel
                                , H.IndicadorNome
                                , H.IndicadorFacebookID
                                , H.DataCadastro
                                , H.Titulo
                                , H.IndicadoAprovacao as IndicadoAprovacaoDB
                                , H.TriagemAprovacao as TriagemAprovacaoDB
                                , H.ModeracaoEncerrada
	                       FROM Historia (NOLOCK) H
                           LEFT JOIN Indicado (NOLOCK) I ON H.IndicadoID = I.ID
	                       WHERE " + MontaWhere +
                           @"ORDER BY H.DataCadastro ASC
                           OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount,
                    new
                    {
                        UsuarioID = usuarioID,
                        TriagemAprovacaoPendente = enumAprovacao.pendente.ValueAsString(),
                        TriagemAprovacaoIniciado = enumAprovacao.aprovacaoIniciada.ValueAsString(),
                        TriagemAprovacao = enumAprovacao.aprovado.ValueAsString(),
                        TriagemAprovacaoComRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString(),
                        IndicadoAprovacaoAprovado = enumAprovacao.aprovado.ValueAsString(),
                        DataAtual = DateTime.Now,
                        palavraChave = "%" + palavraChave + "%" 
                    }).FirstOrDefault();

                list = con.Query<Historia>(SQL,
                    new
                    {
                        Skip = skip,
                        Take = take,
                        UsuarioID = usuarioID,
                        TriagemAprovacaoPendente = enumAprovacao.pendente.ValueAsString(),
                        TriagemAprovacaoIniciado = enumAprovacao.aprovacaoIniciada.ValueAsString(),
                        TriagemAprovacao = enumAprovacao.aprovado.ValueAsString(),
                        TriagemAprovacaoComRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString(),
                        IndicadoAprovacaoAprovado = enumAprovacao.aprovado.ValueAsString(),
                        DataAtual = DateTime.Now,
                        palavraChave = "%" + palavraChave + "%" 
                    }).ToList();
                con.Close();
            }

            return list;
        }

        /// <summary> 
        /// Método que verifica o LOCK da Triagem, verifica se está no tempo configurado e se é o mesmo usuário
        /// Caso for(Quantidade > 0), o tempo da triagem está válido
        /// </summary> 
        public bool ValidaTempoTriagem(Historia entidade, enumPerfilNome perfil)
        {
            bool TempoValido = false;

            string MontaWhere = "";
            switch (perfil)
            {
                case enumPerfilNome.triagemComum:
                    {
                        MontaWhere = @"AND UsuarioID = @UsuarioID 
                        AND @DataAtual < (SELECT dateadd(minute, TriagemNormalMinutos, H.DATAMODIFICACAO) FROM CONFIGURACAO)";
                        break;
                    }
                case enumPerfilNome.triagemSupervisor:
                    {
                        MontaWhere = @"AND UsuarioID = @UsuarioID 
                        AND @DataAtual < (SELECT dateadd(minute, TriagemSupervisorMinutos, H.DATAMODIFICACAO) FROM CONFIGURACAO)";
                        break;
                    }
            };

            string SQL = @"SELECT COUNT(1) QTDE
	                        FROM Historia (NOLOCK) H
	                        WHERE ID = @ID " + MontaWhere;

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                TempoValido = (con.Query<int>(SQL,
                    new
                    {
                        ID = entidade.ID,
                        UsuarioID = entidade.UsuarioID,
                        DataAtual = DateTime.Now
                    }).FirstOrDefault() > 0);
                con.Close();
            }

            return TempoValido;
        }

        public void AtualizarTriagemAbuso(Historia entidade)
        {
            string SQL = @"UPDATE Historia 
                            SET 
                                AlteracaoOrigem = @AlteracaoOrigemDB,
                                DataModificacao = @DataModificacao,
                                UsuarioID = @UsuarioID,
                                ComentarioTriagemAbuso = @ComentarioTriagemAbuso,
                                ComentarioTriagem = @ComentarioTriagem,
                                TriagemAprovacaoAbuso = @TriagemAprovacaoAbusoDB,
                                TriagemAprovacao = @TriagemAprovacaoDB,
                                QuantidadeAbuso = @QuantidadeAbuso
                           WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void AtualizarTriagemModeracao(Historia entidade, enumPerfilNome perfil, enumAprovacao operacao = enumAprovacao.semNecessidade)
        {
            string MontaUpdate = "";

            switch (perfil)
            {
                case enumPerfilNome.triagemComum:
                    {
                        MontaUpdate = @"TriagemAprovacaoNormal = @Operacao, 
                                        ComentarioTriagem = @ComentarioTriagem,
                                        ComentarioTriagemNormal = @ComentarioTriagem,";

                        if (operacao == enumAprovacao.naoAprovado)
                            MontaUpdate += "TriagemAprovacaoSupervisor = @TriagemSupervisorPendente,";
                        else
                            MontaUpdate += "TriagemAprovacao = @Operacao, ";

                        break;
                    }

                case enumPerfilNome.triagemSupervisor:
                    {
                        MontaUpdate = @"TriagemAprovacaoSupervisor = @Operacao, 
                                        TriagemAprovacao = @Operacao, 
                                        ComentarioTriagem = @ComentarioTriagem,
                                        ComentarioTriagemSupervisor = @ComentarioTriagem,";
                        break;
                    }

                case enumPerfilNome.moderadores:
                    {
                        MontaUpdate = @"Titulo = @Titulo
                                      , Texto = @Texto
                                      , EmailGaleria = @EmailGaleria
                                      , ComentarioIndicado = @ComentarioIndicado
                                      , Nota=(SELECT AVG(Nota) as Nota FROM HistoriaModeracao (NOLOCK) WHERE HistoriaID = Historia.ID AND DataFimAvaliacao IS NOT NULL)
                                      , ModeracaoEncerrada = IIF((SELECT COUNT(1) FROM HistoriaModeracao HM WHERE HM.HistoriaID = Historia.ID AND HM.DataFimAvaliacao IS NOT NULL) >= (SELECT ModeracaoQuantidade FROM Configuracao), 1, 0) 
                                      ,";
                        break;
                    }
            };
            //atualizar dataModificacao, alteraçaoOrigem, usuarioID
            string SQL = @"UPDATE Historia SET " + MontaUpdate + @"
                            AlteracaoOrigem = @AlteracaoOrigem,
                            UsuarioID = @UsuarioID,
                            DataModificacao = @Data
                           WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL,
                    new
                    {
                        ID = entidade.ID,
                        Operacao = operacao.ValueAsString(),
                        TriagemSupervisorPendente = enumAprovacao.pendente.ValueAsString(), // apenas para Triagem comum
                        ComentarioTriagem = entidade.ComentarioTriagem,
                        Titulo = entidade.Titulo,
                        Texto = entidade.Texto,
                        Data = DateTime.Now,
                        UsuarioID = entidade.UsuarioID,
                        Nota = entidade.Nota,
                        AlteracaoOrigem = entidade.AlteracaoOrigem.ValueAsString(),
                        ModeracaoEncerrada = entidade.ModeracaoEncerrada,
                        EmailGaleria = entidade.EmailGaleria,
                        ComentarioIndicado = entidade.ComentarioIndicado
                    });
                con.Close();
            }

        }

        public IList<HistoriaReportModel> ListarHistoriasReportadas(int indicadoID)
        {
            historiasReportadas = new List<HistoriaReportModel>();

            string SQL = @"SELECT 
                     Historia.ID
                    ,Historia.Titulo
                    ,Historia.Texto
                    ,Historia.QuantidadeAbuso
                    ,Report.ID
                    ,Report.Nome
					,Report.Mensagem
					,Report.DataCadastro
                FROM 
                    Historia (NOLOCK) as Historia
                    INNER JOIN HistoriaReporteAbuso (NOLOCK) as Report ON (Report.HistoriaID = Historia.ID)
                WHERE (Report.Inativo = 0 AND Historia.IndicadoID = @indicadoID)
                ORDER BY Historia.ID ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Query<Historia, HistoriaReporteAbuso, int>(SQL, addResultHistoriasReportadas, new { indicadoID = indicadoID }).ToList();
                con.Close();
            }

            return historiasReportadas;
        }

        private int addResultHistoriasReportadas(Historia historia, HistoriaReporteAbuso reporte) 
        {
            HistoriaReportModel historiaAdicionada = historiasReportadas.Where(x => x.Historia.ID == historia.ID).FirstOrDefault();
            
            if (historiaAdicionada != null)
            {
                if (historiaAdicionada.Reportes == null)
                {
                    historiaAdicionada.Reportes = new List<HistoriaReporteAbuso>();
                }
                historiaAdicionada.Reportes.Add(reporte);
            }
            else
            {
                historiaAdicionada = new HistoriaReportModel();
                historiaAdicionada.Historia = historia;
                historiaAdicionada.AceitarHistoria = true;

                if (historiaAdicionada.Reportes == null)
                {
                    historiaAdicionada.Reportes = new List<HistoriaReporteAbuso>();
                }
                historiaAdicionada.Reportes.Add(reporte);
                historiasReportadas.Add(historiaAdicionada);
            }
            return 1;
        }

        public IList<Historia> CarregaHistoriasIndicado(int indicadoID)
        {
            List<Historia> list = new List<Historia>();

            string SQL = @"SELECT ID
              ,IndicadoID
              ,IndicadorFacebookID
              ,IndicadorFacebookToken
              ,IndicadorNome
              ," + Decriptografar("IndicadorEmailCrt") + @" as IndicadorEmail
              ,IndicadoNome
              ," + Decriptografar("IndicadoEmailCrt") + @" as IndicadoEmail
              ,IndicadoAprovacao as IndicadoAprovacaoDB
              ,TriagemAprovacao as TriagemAprovacaoDB
              ,TriagemAprovacaoNormal as TriagemAprovacaoNormalDB
              ,TriagemAprovacaoSupervisor as TriagemAprovacaoSupervisorDB
              ,Titulo
              ,Texto
              ,HistoriaCategoriaID
              ,CodigoIndicado
              ,CodigoIndicadoResponsavel
              ,DataCadastro
              ,DataModificacao
              ,UsuarioID
              ,TriagemAprovacaoAbuso as TriagemAprovacaoAbusoDB
              ,QuantidadeAbuso
              ,AlteracaoOrigem as AlteracaoOrigemDB
              ,ComentarioIndicadoOriginal
              ,ComentarioIndicadoResponsavel
              ,ComentarioIndicado
              ,ComentarioTriagemNormal
              ,ComentarioTriagemSupervisor
              ,ComentarioTriagemAbuso
              ,ComentarioTriagem
              ,IndicadoAprovacaoOriginal as IndicadoAprovacaoOriginalDB
              ,IndicadoAprovacaoResponsavel as IndicadoAprovacaoResponsavelDB
              ,IndicadorFotoArquivoNome
              ,DataEmailAguardandoAprovacao
              ,QuantidadeEmailAguardandoAprovacao
          FROM Historia (NOLOCK) WHERE IndicadoID = @indicadoID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Query<Historia>(SQL, new { indicadoID = indicadoID }).ToList();
                con.Close();
            }

            return list;
        }

        public void AdicionarAbuso(int historiaId)
        {
            #region query
            string query = @"UPDATE 
	                            Historia 
                            SET
	                            UsuarioID = NULL
	                            ,TriagemAprovacaoAbuso = @TriagemAprovacaoAbuso
	                            ,AlteracaoOrigem = @AlteracaoOrigem
	                            ,QuantidadeAbuso = (SELECT COUNT(ha.ID) FROM HistoriaReporteAbuso AS ha WHERE ha.Inativo = 0 AND ha.HistoriaID = Historia.ID)
                            WHERE
                                ID = @ID";
            #endregion
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Query(query, new { 
                    ID = historiaId, 
                    TriagemAprovacaoAbuso = enumAprovacao.pendente.ValueAsString(),
                    AlteracaoOrigem = enumAlteracaoOrigem.ReportAbuso.ValueAsString()
                });
                con.Close();
            }
        }
        public OverviewModel CarregarDadosOverview()
        {
            OverviewModel retorno = new OverviewModel();

            string SQL = @"SELECT count(*) as Quantidade, a.Data as DataStr
                        FROM (
                            SELECT
                            H.ID,
                            convert(VARCHAR(10), ISNULL((SELECT MIN(LH.DataModificacao)
                            FROM LogHistoria ( NOLOCK ) LH
                            WHERE LH.IndicadoAprovacao = 'A' AND LH.HistoriaID = H.ID),
                            H.DataModificacao), 111) AS Data
                            FROM Historia ( NOLOCK ) H
                            WHERE H.IndicadoAprovacao = 'A'
                        ) a
                        GROUP BY a.Data
                        ORDER BY a.Data";

            //se retornar nula a data, pega a ultima
            string SQL2 = @"SELECT count(*) as Quantidade, a.Data as DataStr
                        FROM (
                            SELECT
                            H.ID,
                            convert(VARCHAR(10), ISNULL((SELECT MAX(LH.DataModificacao)
                            FROM LogHistoria ( NOLOCK ) LH
                            WHERE LH.IndicadoAprovacao = 'A' AND LH.TriagemAprovacao IN ('A', 'R', 'N') AND LH.HistoriaID = H.ID),
                            H.DataModificacao), 111) AS Data
                            FROM Historia ( NOLOCK ) H
                            WHERE H.IndicadoAprovacao = 'A' AND H.TriagemAprovacao IN ('A', 'R', 'N')
                        ) a
                        GROUP BY a.Data
                        ORDER BY a.Data";

            string SQL3 = @"SELECT (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'A' AND TriagemAprovacao = 'A') as AprovadasTriagem,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'A' AND TriagemAprovacao = 'N') AS RecusadasTriagem,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'A' AND TriagemAprovacao = 'R' ) AS RessalvasTriagem,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'A' AND TriagemAprovacao = 'P' ) AS PendentesTriagem,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'A' AND TriagemAprovacao IN ('A', 'R') AND ModeracaoEncerrada = 1 ) AS Moderadas,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'A' AND TriagemAprovacao IN ('A', 'R') AND Nota is not null) AS Galeria,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'A' AND TriagemAprovacao IN ('A', 'R') AND Nota is null) AS PendentesModeracao,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'A') AS TotalHistoriasIndicados,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'N') AS TotalHistoriasRecusadas,
                            (SELECT COUNT(1) FROM Historia (NOLOCK) WHERE IndicadoAprovacao = 'P') AS TotalHistoriasPendentes,
                            (SELECT COUNT(1) FROM Historia (NOLOCK)) AS TotalHistorias,
                            (SELECT Min(DataModificacao) FROM LogHistoria (NOLOCK) WHERE IndicadoAprovacao = 'A') AS DataMin,
                            (SELECT COUNT(1) FROM Indicado (NOLOCK)) AS TotalIndicados,
                            
                            (SELECT COUNT(1) 
                                    FROM Usuario (NOLOCK) U
                                    INNER JOIN UsuarioPerfil (NOLOCK) UP ON UP.ID = U.UsuarioPerfilID
                                    WHERE (UP.Nome = @PerfilTriagem
                                        OR UP.Nome = @PerfilTriagemAbuso) ) as QtdeUsuarioTriagem,
                            (SELECT COUNT(1) 
                                    FROM Usuario (NOLOCK) U
                                    INNER JOIN UsuarioPerfil (NOLOCK) UP ON UP.ID = U.UsuarioPerfilID
                                    WHERE UP.Nome = @PerfilModerador ) as QtdeUsuarioModerador,
                            (SELECT COUNT(1) 
                                    FROM Usuario (NOLOCK) U
                                    INNER JOIN UsuarioPerfil (NOLOCK) UP ON UP.ID = U.UsuarioPerfilID
                                    WHERE UP.Nome = @PerfilTriagemSupervisor ) as QtdeUsuarioSupervisor";


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                retorno = con.Query<OverviewModel>(SQL3,
                    new
                    {
                        PerfilTriagem = enumPerfilNome.triagemComum.ToString(),
                        PerfilTriagemAbuso = enumPerfilNome.triagemAbuso.ToString(),
                        PerfilModerador = enumPerfilNome.moderadores.ToString(),
                        PerfilTriagemSupervisor = enumPerfilNome.triagemSupervisor.ToString()
                    }
                    ).FirstOrDefault();

                retorno.Historias = con.Query<DadoGraficoModel>(SQL).ToList();

                retorno.Triadas = con.Query<DadoGraficoModel>(SQL2).ToList();

                con.Close();
            }
            return retorno;
        }
    }
}