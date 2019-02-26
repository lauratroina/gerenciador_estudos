using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;
using Dapper;
using System.Data.Common;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Enumerator;

namespace BradescoNext.Lib.DAL.ADO
{

    public class IndicadoADO : ADOSuper, IIndicadoDAL
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }


        /// <summary> 
        /// Inclui dados na base 
        /// </summary> 
        /// <param name="entidade"></param> 
        public int Inserir(Indicado entidade)
        {
            string SQL = @" INSERT INTO Indicado     
                    (Nome
                  ,EmailCrt
                  ,DocumentoTipoID
                  ,DocumentoNumeroCrt
                  ,DocumentoOrgaoEmissor
                  ,DocumentoEstadoID
                  ,DocumentoDataExpedicao
                  ,Genero
                  ,DataNascimento
                  ,FotoArquivoNome
                  ,ParentescoID
                  ,ResponsavelNome
                  ,ResponsavelEmailCrt
                  ,ResponsavelTelefoneCrt
                  ,CidadeParticipanteID
                  ,CidadeID
                  ,Condutor
                  ,UsuarioID
                  ,DataModificacao
                  ,AlteracaoOrigem
                  ,DataCadastro
                  ,RemoverGaleria
                  ,DocumentoNumeroComplementoCrt
                  ,HistoriaID
                  ,HistoriaIDConcluido)
                    VALUES 
                  (@Nome
                  ," + Criptografar(entidade.Email) + @"
                  ,@DocumentoTipoID
                  ," + Criptografar(entidade.DocumentoNumero) + @"
                  ,@DocumentoOrgaoEmissor
                  ,@DocumentoEstadoID
                  ,@DocumentoDataExpedicao
                  ,@Genero
                  ,@DataNascimento
                  ,@FotoArquivoNome
                  ,@ParentescoID
                  ,@ResponsavelNome
                  ," + Criptografar(entidade.ResponsavelEmail) + @"
                  ," + Criptografar(entidade.ResponsavelTelefone) + @"
                  ,@CidadeParticipanteID
                  ,@CidadeID
                  ,@Condutor
                  ,@UsuarioID
                  ,@DataModificacao
                  ,@AlteracaoOrigemDB
                  ,@DataCadastro
                  ,@RemoverGaleria
                  ," + Criptografar(entidade.DocumentoNumeroComplemento) + @"
                  ,@HistoriaID
                  ,@HistoriaIDConcluido);
            SELECT ID = SCOPE_IDENTITY()";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade.ID = con.Query<int>(SQL, entidade).FirstOrDefault();
                con.Close();
            }
            return entidade.ID;
        }

        public void InserirLog(int id)
        {
            string SQL = @" INSERT INTO LogIndicado (IndicadoID
                          ,Nome
                          ,EmailCrt
                          ,DocumentoTipoID
                          ,DocumentoNumeroCrt
                          ,DocumentoOrgaoEmissor
                          ,DocumentoEstadoID
                          ,DocumentoDataExpedicao
                          ,Genero
                          ,DataNascimento
                          ,FotoArquivoNome
                          ,ParentescoID
                          ,ResponsavelNome
                          ,ResponsavelEmailCrt
                          ,ResponsavelTelefoneCrt
                          ,CidadeParticipanteID
                          ,CidadeID
                          ,Condutor
                          ,UsuarioID
                          ,DataModificacao
                          ,AlteracaoOrigem
                          ,DataCadastro
                          ,RemoverGaleria
                          ,DocumentoNumeroComplementoCrt
                          ,HistoriaID
                          ,HistoriaIDConcluido) 
                (SELECT ID as IndicadoID
                           ,Nome
                          ,EmailCrt
                          ,DocumentoTipoID
                          ,DocumentoNumeroCrt
                          ,DocumentoOrgaoEmissor
                          ,DocumentoEstadoID
                          ,DocumentoDataExpedicao
                          ,Genero
                          ,DataNascimento
                          ,FotoArquivoNome
                          ,ParentescoID
                          ,ResponsavelNome
                          ,ResponsavelEmailCrt
                          ,ResponsavelTelefoneCrt
                          ,CidadeParticipanteID
                          ,CidadeID
                          ,Condutor
                          ,UsuarioID
                          ,DataModificacao
                          ,AlteracaoOrigem
                          ,DataCadastro
                          ,RemoverGaleria
                          ,DocumentoNumeroComplementoCrt
                          ,HistoriaID
                          ,HistoriaIDConcluido
                FROM Indicado
                WHERE ID = @ID) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        /// <summary> 
        /// Método que atualiza os dados entidade. NÃO ATUALIZA STATUS nem ultima historiaid
        /// </summary> 
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        public void AtualizarConfirmarIndicacao(Indicado entidade, bool menor)
        {
            string SQL = @"UPDATE Indicado SET 
                   Nome=@Nome
                  ,EmailCrt=" + Criptografar(entidade.Email) + @"
                  ,DocumentoTipoID=@DocumentoTipoID
                  ,DocumentoNumeroCrt=" + Criptografar(entidade.DocumentoNumero) + @"
                  ,DocumentoOrgaoEmissor=@DocumentoOrgaoEmissor
                  ,DocumentoEstadoID=@DocumentoEstadoID
                  ,DocumentoDataExpedicao=@DocumentoDataExpedicao
                  ,Genero=@Genero
                  ,DataNascimento=@DataNascimento
                  ,FotoArquivoNome=@FotoArquivoNome
                  ,CidadeParticipanteID=@CidadeParticipanteID
                  ,CidadeID=@CidadeID
                  ,DataModificacao=@DataModificacao
                  ,UsuarioID=NULL
                  ,AlteracaoOrigem=@AlteracaoOrigemDB
                  ,DocumentoNumeroComplementoCrt=" + Criptografar(entidade.DocumentoNumeroComplemento);
            if (menor)
            {
                SQL += @",ParentescoID=@ParentescoID
                      ,ResponsavelNome=@ResponsavelNome
                      ,ResponsavelEmailCrt=" + Criptografar(entidade.ResponsavelEmail) + @"
                      ,ResponsavelTelefoneCrt=" + Criptografar(entidade.ResponsavelTelefone);
            }

            SQL += @" WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void AtualizarConfirmarIndicacaoResponsavel(Indicado entidade)
        {
            string SQL = @"UPDATE Indicado SET 
                                  FotoArquivoNome=@FotoArquivoNome
                                 ,DataModificacao=@DataModificacao
                                 ,UsuarioID=NULL
                                 ,AlteracaoOrigem=@AlteracaoOrigemDB
                            WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }
      
        /// <summary> 
        /// Método que atualiza os dados entidade.
        /// </summary> 
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        public void Atualizar(Indicado entidade)
        {
            string SQL = @"UPDATE Indicado SET  
                    (Nome = @Nome
                  ,EmailCrt=" + Criptografar(entidade.Email) + @"
                  ,DocumentoTipoID=@DocumentoTipoID
                  ,DocumentoNumeroCrt=@" + Criptografar(entidade.DocumentoNumero) + @"
                  ,DocumentoOrgaoEmissor=@DocumentoOrgaoEmissor
                  ,DocumentoEstadoID=@DocumentoEstadoID
                  ,DocumentoDataExpedicao=@DocumentoDataExpedicao
                  ,Genero=@Genero
                  ,DataNascimento=@DataNascimento
                  ,FotoArquivoNome=@FotoArquivoNome
                  ,ParentescoID=@ParentescoID
                  ,ResponsavelNome=@ResponsavelNome
                  ,ResponsavelEmailCrt=@" + Criptografar(entidade.ResponsavelEmail) + @"
                  ,ResponsavelTelefoneCrt=@" + Criptografar(entidade.ResponsavelTelefone) + @"
                  ,CidadeParticipanteID=@CidadeParticipanteID
                  ,CidadeID=@CidadeID
                  ,Condutor=@Condutor
                  ,UsuarioID=@UsuarioID
                  ,DataModificacao=@DataModificacao
                  ,AlteracaoOrigem=@AlteracaoOrigemDB
                  ,DataCadastro=@DataCadastro
                  ,RemoverGaleria=@RemoverGaleria
                  ,HistoriaID=@HistoriaID
                  ,HistoriaIDConcluido=@HistoriaIDConcluido
                  ,DocumentoNumeroComplementoCrt=@" + Criptografar(entidade.DocumentoNumeroComplemento) + @")
                    WHERE ID=@ID ";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void AtualizarIndicadoModeracao(int indicadoID, int usuarioID)
        {

            string SQLHistoriaID = @"(
                SELECT TOP 1 H.ID
                FROM Historia H
            WHERE H.IndicadoID = Indicado.ID AND (H.TriagemAprovacao = @TriagemAprovado OR H.TriagemAprovacao = @TriagemAprovacaoComRessalva) AND H.Nota IS NOT NULL
            ORDER BY H.Nota desc, H.DataCadastro desc
            )";
            string SQLHistoriaIDConcluido = @"(
                SELECT TOP 1 H.ID
                FROM Historia H
            WHERE H.IndicadoID = Indicado.ID AND (H.TriagemAprovacao = @TriagemAprovado OR H.TriagemAprovacao = @TriagemAprovacaoComRessalva) AND H.Nota IS NOT NULL AND H.ModeracaoEncerrada = 1
            ORDER BY H.Nota desc, H.DataCadastro desc
            )";
            string SQL = @"UPDATE Indicado SET 
                                HistoriaID = " + SQLHistoriaID + @"
                              , HistoriaIDConcluido = " + SQLHistoriaIDConcluido + @"
                              , UsuarioID=@UsuarioID
                              , DataModificacao=@DataModificacao
                              , AlteracaoOrigem=@AlteracaoOrigemDB
                            WHERE ID = @IndicadoID 
                             AND (     (HistoriaID IS NULL AND " + SQLHistoriaID + @" IS NOT NULL) 
                                    OR  HistoriaID <> " + SQLHistoriaID + @"
                                    OR (HistoriaIDConcluido IS NULL AND " + SQLHistoriaIDConcluido + @" IS NOT NULL) 
                                    OR  HistoriaIDConcluido <> " + SQLHistoriaIDConcluido + @"
                                )";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new
                {
                    IndicadoID = indicadoID, 
                    UsuarioID = usuarioID, 
                    DataModificacao = DateTime.Now,
                    AlteracaoOrigemDB = enumAlteracaoOrigem.Moderacao.ValueAsString(),
                    TriagemAprovado = enumAprovacao.aprovado.ValueAsString(),
                    TriagemAprovacaoComRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString()
                });
                con.Close();
            }
        }

        
        /// <summary>
        /// Atualização da data de aguardando Aprovação
        /// e quantidade de email aguardando aprovação
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarDataQuantidadeAguardandoAprovacao(Indicado entidade)
        {
            string SQL = @"UPDATE Indicado SET DataEmailAguardandoAprovacao=@DataEmailAguardandoAprovacao, QuantidadeEmailAguardandoAprovacao=@QuantidadeEmailAguardandoAprovacao
                                    WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public Indicado Carregar(string documentoNumero, int documentoTipoID)
        {
            //TODO: Considerar TIPO ID
            Indicado entidadeRetorno = null;
            string SQL = @"SELECT Indicado.ID
                ,Indicado.Nome
                ," + Decriptografar("Indicado.EmailCrt") + @" as Email
                ,Indicado.DocumentoTipoID
                ," + Decriptografar("Indicado.DocumentoNumeroCrt") + @" as DocumentoNumero 
                ,Indicado.DocumentoOrgaoEmissor
                ,Indicado.DocumentoEstadoID
                ,Indicado.DocumentoDataExpedicao
                ,Indicado.Genero
                ,Indicado.DataNascimento
                ,Indicado.FotoArquivoNome
                ,Indicado.ParentescoID
                ,Indicado.ResponsavelNome
                ," + Decriptografar("Indicado.ResponsavelEmailCrt") + @" as ResponsavelEmail  
                ," + Decriptografar("Indicado.ResponsavelTelefoneCrt") + @" as ResponsavelTelefone
                ,Indicado.CidadeParticipanteID
                ,Indicado.CidadeID
                ,Indicado.Condutor
                ,Indicado.UsuarioID
                ,Indicado.DataModificacao
                ,Indicado.AlteracaoOrigem as AlteracaoOrigemDB
                ,Indicado.DataCadastro
                ,Indicado.RemoverGaleria
                ,Indicado.HistoriaID
                ,Indicado.HistoriaIDConcluido
                ,Cidade.ID
                ,Cidade.Nome
                ,Cidade.Slug
                ,Cidade.CEPFinal
                ,Cidade.CEPInicial
                ,Cidade.Longitude
                ,Cidade.Latitude
                ,Cidade.EstadoID
                ,Estado.ID
                ,Estado.Nome
                ,Estado.UF
                ,Estado.PaisID
                FROM Indicado as Indicado
                INNER JOIN Cidade as Cidade 
                ON Cidade.ID = Indicado.CidadeID
                INNER JOIN Estado as Estado 
                ON Estado.ID = Cidade.EstadoID
                WHERE " + Decriptografar("DocumentoNumeroCrt") + @"=@documento AND DocumentoTipoID = @tipoID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Indicado, Cidade, Estado, Indicado>(SQL, (indicado, cidade, estado) =>
                {
                    indicado.Cidade = cidade;
                    indicado.Estado = estado;
                    return indicado;
                }, new { documento = documentoNumero, tipoID = documentoTipoID }).FirstOrDefault();

                con.Close();
            }
            return entidadeRetorno;
        }

        /// <summary> 
        /// Método que carrega uma entidade. 
        /// </summary> 
        /// <param name="entidade">Entidade a ser carregada (somente o identificador é necessário).</param> 
        /// <returns></returns> 
        public Indicado Carregar(int id)
        {
            Indicado entidadeRetorno = null;

            string SQL = @"SELECT Indicado.ID
            ,Indicado.Nome
            ," + Decriptografar("Indicado.EmailCrt") + @" as Email
            ,Indicado.DocumentoTipoID
            ," + Decriptografar("Indicado.DocumentoNumeroCrt") + @" as DocumentoNumero 
            ,Indicado.DocumentoOrgaoEmissor
            ,Indicado.DocumentoEstadoID
            ,Indicado.DocumentoDataExpedicao
            ,Indicado.Genero
            ,Indicado.DataNascimento
            ,Indicado.FotoArquivoNome
            ,Indicado.ParentescoID
            ,Indicado.ResponsavelNome
            ," + Decriptografar("Indicado.ResponsavelEmailCrt") + @" as ResponsavelEmail  
            ," + Decriptografar("Indicado.ResponsavelTelefoneCrt") + @" as ResponsavelTelefone
            ,Indicado.CidadeParticipanteID
            ,Indicado.CidadeID
            ,Indicado.Condutor
            ,Indicado.UsuarioID
            ,Indicado.DataModificacao
            ,Indicado.AlteracaoOrigem as AlteracaoOrigemDB
            ,Indicado.DataCadastro
            ,Indicado.RemoverGaleria
            ,Indicado.HistoriaID
            ,Indicado.HistoriaIDConcluido
            ,Cidade.ID
            ,Cidade.Nome
            ,Cidade.Slug
            ,Cidade.CEPFinal
            ,Cidade.CEPInicial
            ,Cidade.Longitude
            ,Cidade.Latitude
            ,Cidade.EstadoID
            ,Estado.ID
            ,Estado.Nome
            ,Estado.UF
            ,Estado.PaisID
            ,Estado.Slug
            ,Usuario.ID
            ,Usuario.Nome
            ,Usuario.Login
            ,CPart.ID
            ,CPart.Nome
            ,EPart.ID
            ,EPart.UF
            FROM Indicado (NOLOCK) 
            INNER JOIN Cidade (NOLOCK) ON Cidade.ID = Indicado.CidadeID
            INNER JOIN Estado (NOLOCK) ON Estado.ID = Cidade.EstadoID
            INNER JOIN CidadeParticipante (NOLOCK) CP ON CP.ID = Indicado.CidadeParticipanteID
            INNER JOIN Cidade (NOLOCK) CPart ON CPart.ID = CP.CidadeID
            INNER JOIN Estado (NOLOCK) EPart ON EPart.ID = CPart.EstadoID
            LEFT JOIN Usuario (NOLOCK) ON Indicado.UsuarioID = Usuario.ID
			WHERE Indicado.ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Indicado, Cidade, Estado, Usuario, Cidade, Estado, Indicado>(SQL, (indicado, cidade, estado, usuario, cidadePart, estadoPart) =>
                {
                    indicado.Cidade = cidade;
                    indicado.Estado = estado;
                    indicado.Usuario = usuario;
                    indicado.CidadeParticipante = new CidadeParticipante();
                    indicado.CidadeParticipante.ID = indicado.CidadeParticipanteID;
                    indicado.CidadeParticipante.Cidade = cidadePart;
                    indicado.CidadeParticipante.Cidade.Estado = estadoPart;
                    return indicado;
                }, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            
            return entidadeRetorno;
        }

        /// <summary> 
        /// Método que retorna todas as entidades. 
        /// SELECT * FROM Indicado ORDER BY ID DESC 
        /// </summary> 
        public IList<ReportIndicadoPublicoModel> ListarExcel()
        {
            string SQL = @"SELECT Indicado.ID 
            ,Indicado.Nome 
            ," + Decriptografar("Indicado.EmailCrt") + @" as Email
            ," + Decriptografar("Indicado.DocumentoNumeroCrt") + @" as DocumentoNumero 
            ,Indicado.Genero
            ,Indicado.DataNascimento as DataNascimentoDB
            ,Indicado.ParentescoID
            ,Indicado.ResponsavelNome 
            ," + Decriptografar("Indicado.ResponsavelEmailCrt") + @" as ResponsavelEmail  
            ," + Decriptografar("Indicado.ResponsavelTelefoneCrt") + @" as ResponsavelTelefone
            ,Indicado.Condutor as CondutorDB
            ,Indicado.DataModificacao as DataModificacaoDB
            ,Indicado.AlteracaoOrigem as AlteracaoOrigemDB
            ,Indicado.DataCadastro as DataCadastroDB
            ,Indicado.RemoverGaleria as RemoverGaleriaDB
            ,galeria.Nota as NotaGaleria
            ,moderada.Nota as Nota
            ,Cidade.Nome as CidadeNome
            ,Estado.UF as EstadoUF
            ,CPart.Nome as CidadePartNome
            ,EPart.UF as EstadoPartUF
            ,doc.Nome as DocumentoTipo
            ,Usuario.Nome as Usuario
            FROM Indicado (NOLOCK) 
            INNER JOIN Cidade (NOLOCK) ON Cidade.ID = Indicado.CidadeID
            INNER JOIN Estado (NOLOCK) ON Estado.ID = Cidade.EstadoID
            INNER JOIN CidadeParticipante (NOLOCK) CP ON CP.ID = Indicado.CidadeParticipanteID
            INNER JOIN Cidade (NOLOCK) CPart ON CPart.ID = CP.CidadeID
            INNER JOIN Estado (NOLOCK) EPart ON EPart.ID = CPart.EstadoID
            INNER JOIN DocumentoTipo (NOLOCK) doc ON doc.ID = Indicado.DocumentoTipoID
            LEFT JOIN Usuario (NOLOCK) ON Indicado.UsuarioID = Usuario.ID
            LEFT JOIN Historia (NOLOCK) galeria ON Indicado.HistoriaID = galeria.ID
            LEFT JOIN Historia (NOLOCK) moderada ON Indicado.HistoriaIDConcluido = moderada.ID";
        

            IList<ReportIndicadoPublicoModel> list = new List<ReportIndicadoPublicoModel>();

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<ReportIndicadoPublicoModel>(SQL).ToList();
                /*
                 * 
                    ReportIndicadoPublicoModel retorno = new ReportIndicadoPublicoModel();
                    retorno.DocumentoNumero = indicado.DocumentoNumero;
                    retorno.DataNascimento = indicado.DataNascimento.ToString("dd-MM-yyyy");
                    retorno.Nome = indicado.Nome;
                    retorno.ID = indicado.ID;
                    retorno.DocumentoTipo = tipo.Nome;
                    retorno.Condutor = "Não";
                    if (indicado.Condutor)
                        retorno.Condutor = "Sim";
                    retorno.Galeria = "Não";
                    if (!indicado.RemoverGaleria)
                        retorno.Galeria = "Sim";

                    retorno.AlteracaoOrigem = indicado.AlteracaoOrigem.Description();
                    retorno.ResponsavelEmail = indicado.ResponsavelEmail;
                    retorno.ResponsavelNome = indicado.ResponsavelNome;
                    retorno.ResponsavelTelefone = indicado.ResponsavelTelefone;
                    if(usuario != null)
                    {
                        retorno.Usuario = usuario.Nome;
                    }

                    retorno.Cidade = estado.UF + " - " + cidade.Nome;
                    retorno.CidadeParticipante = estadoPart.UF + " - " + cidadePart.Nome;

                    retorno.DataModificacao = indicado.DataModificacao.ToString("dd-MM-yyyy");
                    retorno.DataCadastro = indicado.DataCadastro.ToString("dd-MM-yyyy");
                    retorno.Genero = indicado.Genero;
                    retorno.NotaGaleria = indicado.MaiorNota + "";
                    retorno.Nota = indicado.MaiorNotaConcluida + "";
                    retorno.Email = indicado.Email;

                    return retorno;
                 */
                TotalRegistros = list.Count;

                con.Close();
            }
            return list;
        }

        /// <summary> 
        /// Método que retorna todas as entidades. 
        /// SELECT * FROM Indicado ORDER BY ID DESC 
        /// </summary> 
        public IList<Indicado> Listar(Int32 skip, Int32 take, bool mostraTodos, string palavraChave, int cidadeParticipanteID, string documentoNumero, int condutor, decimal nota)
        {
            IList<Indicado> list = new List<Indicado>();

            string strWhere = @" ";

            if (cidadeParticipanteID != 0)
                strWhere += " AND Indicado.CidadeParticipanteID = @cidadeParticipanteID ";

            if (documentoNumero != "")
                strWhere += " AND " + Decriptografar("Indicado.DocumentoNumeroCrt") + " = @DocumentoNumero ";

            if (condutor != 0)
                strWhere += " AND Indicado.Condutor = @Condutor ";

            if (nota > 0)
                strWhere += " AND HistConcluida.Nota >= @Nota ";

            
            string SQLCount = @"SELECT COUNT(1) 
                                FROM Indicado (NOLOCK)
                                INNER JOIN Cidade (NOLOCK) Cidade ON Cidade.ID = Indicado.CidadeID
                                INNER JOIN Estado (NOLOCK) Estado ON Estado.ID = Cidade.EstadoID
                                INNER JOIN CidadeParticipante (NOLOCK) CP ON CP.ID = Indicado.CidadeParticipanteID
                                INNER JOIN Cidade (NOLOCK) CPart ON CPart.ID = CP.CidadeID
                                INNER JOIN Estado (NOLOCK) EPart ON EPart.ID = CPart.EstadoID
                                LEFT JOIN Historia (NOLOCK) HistMaior ON HistMaior.ID = Indicado.HistoriaID
                                LEFT JOIN Historia (NOLOCK) HistConcluida ON HistConcluida.ID = Indicado.HistoriaIDConcluido
                                WHERE (Indicado.Nome like @palavraChave OR
                                    EPart.UF +'-'+ CPart.Nome like @palavrachave OR " + Decriptografar("Indicado.EmailCrt") + " like @palavrachave)";
            
            string SQL = @"SELECT Indicado.ID
                                ,Indicado.Nome
                                ," + Decriptografar("Indicado.EmailCrt") + @" as Email
                                ,Indicado.DocumentoTipoID
                                ," + Decriptografar("Indicado.DocumentoNumeroCrt") + @" as DocumentoNumero 
                                ,Indicado.DocumentoOrgaoEmissor
                                ,Indicado.DocumentoEstadoID
                                ,Indicado.DocumentoDataExpedicao
                                ,Indicado.Genero
                                ,Indicado.DataNascimento
                                ,Indicado.FotoArquivoNome
                                ,Indicado.ParentescoID
                                ,Indicado.ResponsavelNome
                                ," + Decriptografar("Indicado.ResponsavelEmailCrt") + @" as ResponsavelEmail  
                                ," + Decriptografar("Indicado.ResponsavelTelefoneCrt") + @" as ResponsavelTelefone
                                ,Indicado.CidadeParticipanteID
                                ,Indicado.CidadeID
                                ,Indicado.Condutor
                                ,Indicado.UsuarioID
                                ,Indicado.DataModificacao
                                ,Indicado.AlteracaoOrigem as AlteracaoOrigemDB
                                ,Indicado.DataCadastro
                                ,Indicado.RemoverGaleria
                                ,Indicado.HistoriaID
                                ,Indicado.HistoriaIDConcluido
                                ,ISNULL(HistMaior.Nota, 0) as MaiorNota
                                ,ISNULL(HistConcluida.Nota, 0) as MaiorNotaConcluida";
            if (!mostraTodos)
            {
                SQL += ", (SELECT COUNT(1) FROM Historia h WHERE h.IndicadoID = Indicado.ID AND h.ModeracaoEncerrada = 1) as TotalHistorias";
            }
            else
            {
                SQL += ", (SELECT COUNT(1) FROM Historia h WHERE h.IndicadoID = Indicado.ID) as TotalHistorias";
            }
            SQL += @",Cidade.ID
                                ,Cidade.Nome
                                ,Cidade.Slug
                                ,Cidade.CEPFinal
                                ,Cidade.CEPInicial
                                ,Cidade.Longitude
                                ,Cidade.Latitude
                                ,Cidade.EstadoID
                                ,Estado.ID
                                ,Estado.Nome
                                ,Estado.UF
                                ,Estado.PaisID
                                ,Estado.Slug
                                ,CPart.ID
                                ,CPart.Nome
                                ,EPart.ID
                                ,EPart.UF
                                FROM Indicado (NOLOCK) Indicado
                                INNER JOIN Cidade (NOLOCK) Cidade ON Cidade.ID = Indicado.CidadeID
                                INNER JOIN Estado (NOLOCK) Estado ON Estado.ID = Cidade.EstadoID
                                INNER JOIN CidadeParticipante (NOLOCK) CP ON CP.ID = Indicado.CidadeParticipanteID
                                INNER JOIN Cidade (NOLOCK) CPart ON CPart.ID = CP.CidadeID
                                INNER JOIN Estado (NOLOCK) EPart ON EPart.ID = CPart.EstadoID
                    LEFT JOIN Historia (NOLOCK) HistMaior ON HistMaior.ID = Indicado.HistoriaID
                    LEFT JOIN Historia (NOLOCK) HistConcluida ON HistConcluida.ID = Indicado.HistoriaIDConcluido
                    WHERE (Indicado.Nome like @palavraChave OR
                          EPart.UF +'-'+ CPart.Nome like @palavrachave OR " + Decriptografar("Indicado.EmailCrt") + " like @palavrachave)";
            if (!mostraTodos)
            {
                SQLCount += " AND Indicado.HistoriaIDConcluido is not NULL ";
                SQL += " AND Indicado.HistoriaIDConcluido is not NULL ";
            }

            SQLCount += strWhere;
            SQL += strWhere;

            SQL += @" ORDER BY " + ((mostraTodos) ? "MaiorNota DESC, " : "") + @"Indicado.Condutor DESC, MaiorNotaConcluida DESC, EPart.UF, CPart.Nome, Indicado.Nome
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount,
                    new
                    {
                        palavraChave = "%" + palavraChave + "%",
                        DocumentoNumero = documentoNumero,
                        CidadeParticipanteID = cidadeParticipanteID,
                        Nota = nota,
                        Condutor = (condutor == 1)
                    }).FirstOrDefault();

                list = con.Query<Indicado, Cidade, Estado, Cidade, Estado, Indicado>(SQL, (indicado, cidade, estado, cidadePart, estadoPart) =>
                {
                    indicado.Cidade = cidade;
                    indicado.Estado = estado;
                    indicado.CidadeParticipante = new CidadeParticipante();
                    indicado.CidadeParticipante.ID = indicado.CidadeParticipanteID;
                    indicado.CidadeParticipante.Cidade = cidadePart;
                    indicado.CidadeParticipante.Cidade.Estado = estadoPart;
                    return indicado;
                },
                    new
                    {
                        Skip = skip,
                        Take = take,
                        palavraChave = "%" + palavraChave + "%",
                        DocumentoNumero = documentoNumero,
                        CidadeParticipanteID = cidadeParticipanteID,
                        Nota = nota,
                        Condutor = (condutor == 1)
                    }).ToList();
                con.Close();
                
            }
            return list;
        }

        public int CarregaIDUltimaHistoriaCadastrada(int indicadoID)
        {
            int id = 0;

            string SQL = @"SELECT TOP 1 UltimaHistoriaIDCadastrada
            FROM Indicado 
            WHERE ID=@Codigo";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                id = con.Query<int>(SQL,
                    new
                    {
                        Codigo = indicadoID
                    }).FirstOrDefault();

                con.Close();
            }

            return id;
        }

        public int CarregaIDUltimaHistoriaModerada(int indicadoID)
        {
            int id = 0;

            string SQL = @"SELECT TOP 1 ISNULL(UltimaHistoriaIDModerada, 0)
            FROM Indicado 
            WHERE ID=@Codigo";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                id = con.Query<int>(SQL,
                    new
                    {
                        Codigo = indicadoID
                    }).FirstOrDefault();

                con.Close();
            }

            return id;
        }

        public void AtualizaIDUltimaHistoriaModeracao(int indicadoID, int historiaID)
        {
            string SQL = @"UPDATE Indicado SET UltimaHistoriaIDModerada = @HistoriaID
                    WHERE ID=@IndicadoID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { IndicadoID = indicadoID, HistoriaID = historiaID });
                con.Close();
            }
        }

        public bool PossuiHistoriaPendente(int indicadoID)
        {

            bool retorno = false;
            string SQL = @"SELECT COUNT(1) QTDE
                           FROM Historia
                           WHERE IndicadoID=@IndicadoID
                           AND TriagemAprovacao = @TriagemAprovacaoPendente";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                retorno = (con.Query<int>(SQL,
                    new
                    {
                        IndicadoID = indicadoID,
                        TriagemAprovacaoPendente = enumAprovacao.pendente.ValueAsString()
                    }).FirstOrDefault() > 0);

                con.Close();
            }
            return retorno;
        }

        public int QtdeHistorias(int indicadoID)
        {
            int count = 0;

            string SQL = @"SELECT COUNT(1) QTDE
                           FROM Historia
                           WHERE IndicadoID = @IndicadoID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                count = con.Query<int>(SQL,
                    new
                    {
                        IndicadoID = indicadoID
                    }).FirstOrDefault();

                con.Close();
            }

            return count;
        }

        public int QtdeHistoriasAprovadas(int indicadoID)
        {
            int count = 0;

            string SQL = @"SELECT COUNT(1) QTDE
                           FROM Historia
                           WHERE IndicadoID = @IndicadoID
                           AND (TriagemAprovacao = @TriagemAprovacao
                             OR TriagemAprovacao = @TriagemAprovacaoComRessalva)";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                count = con.Query<int>(SQL,
                    new
                    {
                        IndicadoID = indicadoID,
                        TriagemAprovacao = enumAprovacao.aprovado.ValueAsString(),
                        TriagemAprovacaoComRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString()
                    }).FirstOrDefault();

                con.Close();
            }

            return count;
        }

        public int QtdeHistoriasNaoAprovadas(int indicadoID)
        {
            int count = 0;

            string SQL = @"SELECT COUNT(1) QTDE
                           FROM Historia
                           WHERE IndicadoID = @IndicadoID
                           AND TriagemAprovacao = @TriagemNaoAprovada";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                count = con.Query<int>(SQL,
                    new
                    {
                        IndicadoID = indicadoID,
                        TriagemNaoAprovada = enumAprovacao.naoAprovado.ValueAsString()
                    }).FirstOrDefault();

                con.Close();
            }

            return count;
        }

        public int QtdeHistoriasModeradas(int indicadoID)
        {
            int count = 0;

            string SQL = @"SELECT COUNT(1) QTDE
                           FROM Historia (NOLOCK) 
                           WHERE IndicadoID = @IndicadoID
                           AND ModeracaoEncerrada = 1";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                count = con.Query<int>(SQL,
                    new
                    {
                        IndicadoID = indicadoID
                    }).FirstOrDefault();

                con.Close();
            }

            return count;
        }

        public bool EstaNaGaleria(int indicadoID, int historiaID)
        {
            bool retorno = false;

            string SQL = @"SELECT 
	                            CASE WHEN Nota >= (SELECT TOP 1 GaleriaNotaCorte FROM Configuracao) THEN
		                            1
	                            ELSE
		                            0
	                            END AS naGaleria
                            FROM
	                            IndicadoModeracao
                            WHERE IndicadoID = @IndicadoID
                            AND HistoriaID = @HistoriaID
                            AND DataFimAvaliacao IS NOT NULL";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                retorno = (con.Query<int>(SQL,
                    new
                    {
                        IndicadoID = indicadoID,
                        HistoriaID = historiaID
                    }).FirstOrDefault() == 1);

                con.Close();
            }

            return retorno;
        }

        public IList<Indicado> ListarCidadeParticipante(int cidadeParticipanteID, bool diferente = false, bool condutor = false)
        {
            string SQL = @"SELECT I.ID, I.Nome, I.Condutor, I.CidadeParticipanteID, I.CidadeID,
                                  C.ID, C.Nome,
                                  E.ID, E.Nome, E.UF,
                                  CP.ID,
                                  (SELECT COUNT(*) FROM Indicado (NOLOCK) WHERE CidadeParticipanteID = cp.ID " + (condutor ? "AND Condutor = 1" : "") + @") as QuantidadeIndicados,
                                  (SELECT COUNT(*) FROM IndicadoInterno (NOLOCK) WHERE CidadeParticipanteID = cp.ID AND Inativo = 0 " + (condutor ? "AND Condutor = 1" : "") + @") as QuantidadeIndicadosInterno,
                                  CCP.ID, CCP.Nome, 
                                  ECP.ID, ECP.Nome, ECP.UF
                           FROM Indicado I
                           INNER JOIN Cidade C ON I.CidadeID = C.ID 
                           INNER JOIN Estado E ON C.EstadoID = E.ID
                           INNER JOIN CidadeParticipante CP ON I.CidadeParticipanteID = CP.ID
                           INNER JOIN Cidade CCP ON CP.CidadeID = CCP.ID
                           INNER JOIN Estado ECP ON CCP.EstadoID  = ECP.ID
                           WHERE I.CidadeParticipanteID " + (diferente ? "<>" : "=") + @" @CidadeParticipanteID
                           ORDER BY CCP.Nome, I.Nome";

            IList<Indicado> list = new List<Indicado>();

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Indicado, Cidade, Estado, CidadeParticipante, Cidade, Estado, Indicado>(SQL,
                    (indicado, cidade, estado, cidadeParticipante, cidadeParticipanteCidade, cidadeParticipanteEstado) =>
                    {
                        indicado.Cidade = cidade;
                        indicado.Cidade.Estado = estado;
                        indicado.CidadeParticipante = cidadeParticipante;
                        indicado.CidadeParticipante.Cidade = cidadeParticipanteCidade;
                        indicado.CidadeParticipante.Cidade.Estado = cidadeParticipanteEstado;
                        return indicado;
                    },
                new
                {
                    CidadeParticipanteID = cidadeParticipanteID
                }).ToList();
                TotalRegistros = list.Count;

                con.Close();
            }
            return list;
        }

        public void Realocar(int indicadoID, int cidadeParticipanteID, bool condutor)
        {
            string SQL = @"UPDATE Indicado SET 
                   CidadeParticipanteID=@CidadeParticipanteID,
                   Condutor=@Condutor
                   WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new
                {
                    CidadeParticipanteID = cidadeParticipanteID,
                    Condutor = condutor,
                    ID = indicadoID
                });
                con.Close();
            }
        }

        
        public IList<Indicado> ListarIndicadosComReports(Int32 skip, Int32 take, string palavraChave)
        {
            IList<Indicado> list = new List<Indicado>();
            string SQLCount = @"SELECT COUNT(DISTINCT IndicadoID)
                                FROM 
                                    HistoriaReporteAbuso as report
                                    INNER JOIN Historia as Historia ON report.HistoriaID = Historia.ID
                                    INNER JOIN Indicado as Indicado ON Historia.IndicadoID = Indicado.ID
                                WHERE report.Inativo = 0 AND Historia.TriagemAprovacaoAbuso = 'P' AND Indicado.Nome like @palavraChave";

            string SQL = @"SELECT 
                    Indicado.ID
                    ,Indicado.Nome
                    ,Indicado.Genero
                    ,Indicado.DataNascimento
                    ,Indicado.CidadeParticipanteID
                    ,Indicado.CidadeID
                    ," + Decriptografar("Indicado.EmailCrt") + @" as Email
                    ,COUNT(report.ID) as NumeroReportsAbuso
                    ,MAX(report.DataCadastro) as UltimoReportAbuso
                FROM 
                    HistoriaReporteAbuso as report
                    INNER JOIN Historia as Historia ON report.HistoriaID = Historia.ID
                    INNER JOIN Indicado as Indicado ON Historia.IndicadoID = Indicado.ID
                WHERE report.Inativo = 0 AND Historia.TriagemAprovacaoAbuso = 'P' AND Indicado.Nome like @palavraChave
                GROUP BY 
                    Indicado.ID
                    ,Indicado.Nome
                    ,Indicado.EmailCrt
                    ,Indicado.Genero
                    ,Indicado.DataNascimento
                    ,Indicado.CidadeParticipanteID
                    ,Indicado.CidadeID
                ORDER BY UltimoReportAbuso ASC
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                list = con.Query<Indicado>(SQL, new
                {
                    Skip = skip,
                    Take = take,
                    palavraChave = "%" + palavraChave + "%"
                }).ToList();
                TotalRegistros = list.Count;
                con.Close();
            }

            return list;
        }
        public void AtualizarIndicadoReportadoAbuso(Indicado entidade)
        {

            string SQLHistoriaID = @"(
                SELECT TOP 1 H.ID
                FROM Historia H
            WHERE H.IndicadoID = Indicado.ID AND H.TriagemAprovacao IN ('" + enumAprovacao.aprovado.ValueAsString() + "', '" + enumAprovacao.aprovadoComRessalva.ValueAsString() + @"') AND H.Nota IS NOT NULL
            ORDER BY H.Nota desc, H.DataCadastro desc
            )";
            string SQLHistoriaIDConcluido = @"(
                SELECT TOP 1 H.ID
                FROM Historia H
            WHERE H.IndicadoID = Indicado.ID AND H.TriagemAprovacao IN ('" + enumAprovacao.aprovado.ValueAsString() + "', '" + enumAprovacao.aprovadoComRessalva.ValueAsString() + @"') AND H.Nota IS NOT NULL AND H.ModeracaoEncerrada = 1
            ORDER BY H.Nota desc, H.DataCadastro desc
            )";

            string SQL = @"UPDATE Indicado SET  
                                HistoriaID = " + SQLHistoriaID + @"
                              , HistoriaIDConcluido = " + SQLHistoriaIDConcluido + @"
                              , UsuarioID=@UsuarioID
                              , DataModificacao=@DataModificacao
                              , AlteracaoOrigem=@AlteracaoOrigemDB
                              , RemoverGaleria=@RemoverGaleria
                            WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }

        }
        public void SalvarCondutor(Indicado entidade)
        {
            string SQL = @"UPDATE Indicado SET  
                    UsuarioID = @usuarioID
                    ,DataModificacao = @data
                    ,Condutor = @condutor
                    WHERE ID = @indicadoID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new 
                { 
                    indicadoID = entidade.ID,
                    condutor = entidade.Condutor,
                    usuarioID = entidade.UsuarioID,
                    data = DateTime.Now
                });
                con.Close();
            }
        }
        public void SalvarRemoverGaleria(Indicado entidade)
        {
            string SQL = @"UPDATE Indicado SET  
                    UsuarioID = @usuarioID
                    ,DataModificacao = @data
                    ,RemoverGaleria = @remover
                    WHERE ID = @indicadoID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new
                {
                    indicadoID = entidade.ID,
                    remover = entidade.RemoverGaleria,
                    usuarioID = entidade.UsuarioID,
                    data = DateTime.Now
                });
                con.Close();
            }
        }

        public List<Indicado> BuscarGaleria(Int32 skip, Int32 take, string busca = "", int nota = 0, enumTipoBuscaIndicado tipo = enumTipoBuscaIndicado.tudo)
        {
            List<Indicado> indicados = null;
            List<string> lstWhere = new List<string>();

            if (!string.IsNullOrEmpty(busca))
            {
                string campo = "HTitulo + ' ' + Nome + ' ' + CNome + ' - ' + EUF + ' ' + ENome + ' ' + HCNome";
                switch (tipo)
                {
                    
                    case enumTipoBuscaIndicado.titulo:
                        campo = "HTitulo";
                        break;
                    case enumTipoBuscaIndicado.indicado:
                        campo = "Nome";
                        break;
                    case enumTipoBuscaIndicado.cidade:
                        campo = "CNome + ' - ' + EUF + ' ' + ENome";
                        break;
                    case enumTipoBuscaIndicado.categoria:
                        campo = "HCNome";
                        break;
                }
                lstWhere.Add("(" + campo + ") like @PalavraChave");
            }
            if(nota > 0)
            {
                lstWhere.Add("HNota >= @Nota");
            }
            string where = (lstWhere.Count == 0) ? "" : " WHERE " + string.Join(" AND ", lstWhere);
            /*
            string SQL = @"SELECT   I.ID, I.Nome, I.FotoArquivoNome,
                                    C.ID, C.Nome,
                                    E.ID, E.UF,
                                    H.ID, H.Titulo, H.Nota, H.DataCadastro,
                                    HC.ID, HC.Nome, HC.CorFont, HC.CorLabel,
                                    HM.ID, HM.ArquivoTipo AS ArquivoTipoDB, HM.ArquivoNome
                                    FROM Indicado (NOLOCK) I
                            INNER JOIN Cidade (NOLOCK) C ON C.ID = I.CidadeID
                            INNER JOIN Estado (NOLOCK) E ON E.ID = C.EstadoID
                            INNER JOIN Historia (NOLOCK) H ON H.ID = I.HistoriaID
                            INNER JOIN HistoriaCategoria (NOLOCK) HC ON HC.ID = H.HistoriaCategoriaID
                            LEFT JOIN (
                                    SELECT MAX(HM_AUX.ID) as HistoriaMidiaID, HM_AUX.HistoriaID
                                    FROM HistoriaMidia (NOLOCK) HM_AUX
                                    WHERE HM_AUX.ArquivoTipo = @ArquivoTipoImagem AND HM_AUX.Inativo = 0
                                GROUP BY HM_AUX.HistoriaID
                            ) HM_UNICO ON HM_UNICO.HistoriaID = H.ID
                            LEFT JOIN HistoriaMidia (NOLOCK) HM ON HM_UNICO.HistoriaMidiaID = HM.ID" + 
                            where + @" ORDER BY H.Nota DESC, H.DataCadastro DESC 
                            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";
             */
            string SQL = @" SELECT  ID, Nome, FotoArquivoNome, Interno,
                                    CID as ID, CNome as Nome,
                                    EID as ID, EUF as UF, ENome as Nome,
                                    HID as ID, HTitulo as Titulo, HNota as Nota, HDataCadastro as DataCadastro,
                                    HCID as ID, HCNome as Nome, HCCorFont as CorFont, HCCorLabel as CorLabel,
                                    HMID as ID, HMArquivoTipoDB as ArquivoTipoDB, HMArquivoNome as ArquivoNome
                                    FROM (
                                        SELECT   I.ID, I.Nome, I.FotoArquivoNome, 0 as Interno,
                                                C.ID as CID, C.Nome as CNome,
                                                E.ID as EID, E.UF as EUF, E.Nome as ENome,
                                                H.ID as HID, H.Titulo as HTitulo, H.Nota as HNota, H.DataCadastro as HDataCadastro,
                                                HC.ID as HCID, HC.Nome as HCNome, HC.CorFont as HCCorFont, HC.CorLabel as HCCorLabel,
                                                HM.ID as HMID, HM.ArquivoTipo AS HMArquivoTipoDB, HM.ArquivoNome as HMArquivoNome
                                                FROM Indicado (NOLOCK) I
                                        INNER JOIN Cidade (NOLOCK) C ON C.ID = I.CidadeID
                                        INNER JOIN Estado (NOLOCK) E ON E.ID = C.EstadoID
                                        INNER JOIN Historia (NOLOCK) H ON H.ID = I.HistoriaID
                                        INNER JOIN HistoriaCategoria (NOLOCK) HC ON HC.ID = H.HistoriaCategoriaID
                                        LEFT JOIN (
                                                SELECT MAX(HM_AUX.ID) as HistoriaMidiaID, HM_AUX.HistoriaID
                                                FROM HistoriaMidia (NOLOCK) HM_AUX
                                                WHERE HM_AUX.ArquivoTipo = @ArquivoTipoImagem AND HM_AUX.Inativo = 0
                                            GROUP BY HM_AUX.HistoriaID
                                        ) HM_UNICO ON HM_UNICO.HistoriaID = H.ID
                                        LEFT JOIN HistoriaMidia (NOLOCK) HM ON HM_UNICO.HistoriaMidiaID = HM.ID
                                        WHERE I.RemoverGaleria = 0

                                        UNION ALL

                                        SELECT   I.ID, I.PrimeiroNome + ' ' + I.NomeMeio + ' ' + I.Sobrenome as Nome, null as FotoArquivoNome, 1 as Interno,
                                                C.ID as CID, C.Nome as CNome,
                                                E.ID as EID, E.UF as EUF, E.Nome as ENome,
                                                0 as HID, I.HistoriaTitulo as HTitulo, (ASCII(LEFT(I.PrimeiroNome, 1))%(10-(SELECT TOP 1 GaleriaNotaCorte FROM Configuracao)))+(SELECT TOP 1 GaleriaNotaCorte FROM Configuracao) as HNota, I.DataCadastro as HDataCadastro,
                                                HC.ID as HCID, HC.Nome as HCNome, HC.CorFont as HCCorFont, HC.CorLabel as HCCorLabel,
                                                0 as HMID, 'I' AS HMArquivoTipoDB, I.HistoriaArquivoNome as HMArquivoNome
                                                FROM IndicadoInterno (NOLOCK) I
                                        INNER JOIN CidadeParticipante (NOLOCK) CP ON I.CidadeParticipanteID = CP.ID
                                        INNER JOIN Cidade (NOLOCK) C ON C.ID = ISNULL(I.EnderecoCidadeID, CP.CidadeID)
                                        INNER JOIN Estado (NOLOCK) E ON E.ID = C.EstadoID
                                        INNER JOIN HistoriaCategoria (NOLOCK) HC ON HC.ID = I.HistoriaCategoriaID
                                        WHERE I.RemoverGaleria = 0 AND I.Inativo = 0 AND I.HistoriaTitulo is not null AND RTRIM(LTRIM(I.HistoriaTitulo)) <> '' AND I.HistoriaTexto is not null AND RTRIM(LTRIM(I.HistoriaTexto)) <> ''
                                    ) U " +
                                    where + @" ORDER BY HNota DESC, HDataCadastro DESC 
                                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY "; 
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                indicados = con.Query<Indicado, Cidade, Estado, Historia, HistoriaCategoria, HistoriaMidia, Indicado>(SQL, (indicado, cidade, estado, historia, categoria, midia) => {

                    indicado.Cidade = cidade;
                    indicado.Cidade.Estado = estado;
                    indicado.Historia = historia;
                    indicado.Historia.HistoriaCategoria = categoria;
                    indicado.Historia.Midia = midia;

                    return indicado;
                }, new
                {
                    Skip = skip,
                    Take = take,
                    Nota = nota,
                    PalavraChave = "%" + busca + "%",
                    ArquivoTipoImagem = enumArquivoTipo.Imagem.ValueAsString()
                }).ToList();
                con.Close();
            }
            return indicados;
        }

        public Indicado CarregarGaleria(int id)
        {
            Indicado indicado = null;
            
            string SQL = @" SELECT I.ID, I.Nome, I.FotoArquivoNome, I.DataModificacao, 
                                   C.ID, C.Nome,
                                   E.ID, E.UF,
                                   H.ID, H.Titulo, H.Texto, H.Nota, H.DataCadastro, H.IndicadorNome, H.ComentarioIndicado,
                                   HC.ID, HC.Nome, HC.CorFont, HC.CorLabel,
                                   HM.ID, HM.ArquivoTipo AS ArquivoTipoDB, HM.ArquivoNome
                                  FROM Indicado (NOLOCK) I
                            INNER JOIN Cidade (NOLOCK) C ON C.ID = I.CidadeID
                            INNER JOIN Estado (NOLOCK) E ON E.ID = C.EstadoID
                            INNER JOIN Historia (NOLOCK) H ON H.IndicadoID = I.ID
                            INNER JOIN HistoriaCategoria (NOLOCK) HC ON HC.ID = H.HistoriaCategoriaID
                            LEFT JOIN HistoriaMidia (NOLOCK) HM ON H.ID = HM.HistoriaID AND HM.Inativo = 0
                                 WHERE I.ID = @ID AND (H.TriagemAprovacao = @TriagemAprovado OR H.TriagemAprovacao = @TriagemAprovacaoComRessalva) AND H.Nota IS NOT NULL
                              ORDER BY H.Nota DESC, H.DataCadastro DESC, HM.ArquivoTipo";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                con.Query<Indicado, Cidade, Estado, Historia, HistoriaCategoria, HistoriaMidia, string>(SQL, (ind, cidade, estado, historia, categoria, midia) =>
                {
                    if (indicado == null)
                    {
                        indicado = ind;
                        indicado.Cidade = cidade;
                        indicado.Cidade.Estado = estado;
                        indicado.Historias = new List<Historia>();
                    }
                    Historia historiaEncontrada = indicado.Historias.FirstOrDefault(t => t.ID == historia.ID);
                    if (historiaEncontrada == null)
                    {
                        historiaEncontrada = historia;
                        historiaEncontrada.Midias = new List<HistoriaMidia>();
                        historiaEncontrada.HistoriaCategoria = categoria;
                        indicado.Historias.Add(historiaEncontrada);
                    }
                    if (midia != null)
                    {
                        historiaEncontrada.Midias.Add(midia);
                        return historiaEncontrada.ID.ToString() + "-" + midia.ID.ToString();
                    }
                    else
                    {
                        return historiaEncontrada.ID.ToString() + "-0";
                    }
                    
                }, new
                {
                    ID = id,
                    TriagemAprovado = enumAprovacao.aprovado.ValueAsString(),
                    TriagemAprovacaoComRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString()
                }).ToList();
                con.Close();
            }
            return indicado;
        }

        public void AtualizarCidadeParticipante(Indicado entidade)
        {
            string SQL = @"UPDATE Indicado SET  
                    CidadeParticipanteID = @cidadeParticipanteID
                    ,UsuarioID = @usuarioID
                    ,DataModificacao = @data
                    ,AlteracaoOrigem = @origem
                    WHERE ID = @indicadoID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new
                {
                    indicadoID = entidade.ID,
                    cidadeParticipanteID = entidade.CidadeParticipanteID,
                    usuarioID = entidade.UsuarioID,
                    data = DateTime.Now,
                    origem = entidade.AlteracaoOrigem.ValueAsString()
                });
                con.Close();
            }
        }

        public int QtdeReportsAbuso(int indicadoID)
        {
            #region query
            string query = @"SELECT 
                               COUNT(report.ID) as NumeroReportsAbuso
                            FROM 
                                HistoriaReporteAbuso(NOLOCK) as report
                                INNER JOIN Historia(NOLOCK) as Historia ON report.HistoriaID = Historia.ID
                            WHERE
	                            report.Inativo = 0
	                            AND Historia.IndicadoID = @ID
                                AND (Historia.TriagemAprovacao = @TriagemAprovado OR Historia.TriagemAprovacao = @TriagemAprovacaoComRessalva)
                                AND Historia.Nota IS NOT NULL";
            #endregion
            int qtd = 0;
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                qtd = con.ExecuteScalar<int>(query, new
                {
                    ID = indicadoID,
                    TriagemAprovado = enumAprovacao.aprovado.ValueAsString(),
                    TriagemAprovacaoComRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString()
                });
                con.Close();
            }
            return qtd;
        }

        public void RemoverGaleriaAbuso(int indicadoID)
        {
            #region query
            string query = @"UPDATE 
                                Indicado
                            SET
                                RemoverGaleria = 1
                                ,UsuarioID = NULL
                                ,AlteracaoOrigem = @AlteracaoOrigem
                            WHERE 
	                            ID = @ID";
            #endregion
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(query, new { ID = indicadoID, AlteracaoOrigem = enumAlteracaoOrigem.ReportAbuso.ValueAsString() });
                con.Close();
            }
        }
        public IList<ExportaIndicadoModel> ExportaIndicados()
        {
            IList<ExportaIndicadoModel> lista = null;
            string SQL = @"SELECT '' Interno, E.UF EstadoParticipante, C.Nome CidadeParticipante, 
		                            I.Nome, I.Genero, 
                                    CASE DT.ID
                                      WHEN 3 THEN 'RNE' 
                                      ELSE DT.Descricao
                                    END as DocumentoTipo,
                                    " + Decriptografar("I.DocumentoNumeroCrt") + @" DocumentoNumero, 
                                    " + Decriptografar("I.DocumentoNumeroComplementoCrt") + @" DocumentoNumeroComplemento, 
                                    I.DataNascimento, 
		                            I.ResponsavelNome, " + Decriptografar("I.ResponsavelTelefoneCrt") + @" ResponsavelTelefone, 
                                    " + Decriptografar("I.ResponsavelEmailCrt") + @" ResponsavelEmail, 
		                            E2.UF, C2.Nome Cidade, 
		                            '' Endereco,
		                            '' EnderecoCidadeEstadoNaoBR,
		                            '' TelefoneResidencial, '' TelefoneComercial, 
		                            " + Decriptografar("I.EmailCrt") + @" Email,
                                    h.Titulo   HistoriaTitulo,
                                    h.Texto    HistoriaNomeacao,
                                    'Público'  HistoriaCategoria,
                                    cast (h.Nota as varchar)     NotaHistoria,
                                    ''         IndicadoInternoCategoria
                            FROM Indicado I
                            INNER JOIN CidadeParticipante CP ON CP.ID = I.CidadeParticipanteID
                            INNER JOIN Cidade C ON C.ID = CP.CidadeID
                            INNER JOIN Estado E ON E.ID = C.EstadoID
                            INNER JOIN Cidade C2 ON C2.ID = I.CidadeID
                            INNER JOIN Estado E2 ON E2.ID = C2.EstadoID
                            INNER JOIN DocumentoTipo DT ON DT.ID = I.DocumentoTipoID
                            LEFT JOIN Historia h ON h.id = i.HistoriaIDConcluido
                            WHERE I.Condutor = @Condutor
                            AND CP.Inativo = @Inativo

                            UNION ALL

                            SELECT 'X' Interno, E.UF EstadoParticipante, C.Nome CidadeParticipante, 
		                            I.PrimeiroNome + ' ' + I.NomeMeio + ' ' + I.Sobrenome as Nome, 
		                            I.Genero, 
                                    CASE DT.ID
                                      WHEN 3 THEN 'RNE' 
                                      ELSE DT.Descricao
                                    END as DocumentoTipo,

                                    " + Decriptografar("I.DocumentoNumeroCrt") + @" DocumentoNumero, 

                                    CASE DT.ID
                                      WHEN 3 THEN " + Decriptografar("I.DocumentoNumeroCrt") + @"
                                      ELSE ''
                                    END as DocumentoNumeroComplemento,
                                    I.DataNascimento, 
		                            I.NomeMae ResponsavelNome, '' ResponsavelTelefone, '' ResponsavelEmail,
		                            E2.UF, C2.Nome Cidade, 
		                            " + Decriptografar("I.EnderecoCrt") + @" + ' - ' + " + Decriptografar("I.EnderecoComplementoCrt") + @" as Endereco,
		                            I.EnderecoCidadeEstadoNaoBR,
		                            " + Decriptografar("I.TelefoneResidencialCrt") + @" TelefoneResidencial, 
                                    " + Decriptografar("I.TelefoneComercialCrt") + @" TelefoneComercial, 
		                            " + Decriptografar("I.EmailCrt") + @" Email,
                                    i.HistoriaTitulo,
                                    i.HistoriaTexto    HistoriaNomeacao,
                                    'Interno'          HistoriaCategoria,
                                    ''                 NotaHistoria,
                                    IIC.Nome           IndicadoInternoCategoria
                            FROM IndicadoInterno I
                            INNER JOIN CidadeParticipante CP ON CP.ID = I.CidadeParticipanteID
                            INNER JOIN Cidade C ON C.ID = CP.CidadeID
                            INNER JOIN Estado E ON E.ID = C.EstadoID
                            LEFT JOIN Cidade C2 ON C2.ID = I.EnderecoCidadeID
                            LEFT JOIN Estado E2 ON E2.ID = C2.EstadoID
                            INNER JOIN DocumentoTipo DT ON DT.ID = I.DocumentoTipoID
                            INNER JOIN IndicadoInternoCategoria IIC ON IIC.ID = I.IndicadoInternoCategoriaID
                            WHERE I.Condutor = @Condutor
                            AND CP.Inativo = @Inativo
                            AND I.Inativo = 0 

                            ORDER BY Interno, EstadoParticipante, CidadeParticipante, Nome, UF, Cidade";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                lista = con.Query<ExportaIndicadoModel>(SQL, 
                    new {
                        Condutor = true,
                        Inativo = false
                    }).ToList();
                con.Close();
            }

            return lista;
        }
      
    }
}