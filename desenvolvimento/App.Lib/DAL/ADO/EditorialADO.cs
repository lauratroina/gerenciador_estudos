using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.Entity;
using Dapper;
using System.Linq;
using System.Data.Common;
using System;


namespace BradescoNext.Lib.DAL.ADO
{
    public class EditorialADO : ADOSuper, IEditorialDAL
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public int Inserir(Editorial entidade)
        {
            string SQL = @"INSERT 
                             INTO Editorial
                                  (IndicadoNome,
                                   IndicadoFoto,
                                   DataExibicao,
                                   TopoTitulo,
                                   TopoTexto,
                                   TopoBotaoYouTubeID,
                                   BaseTexto,
                                   UsuarioID,
                                   DataCadastro,
                                   DataModificacao,
                                   Inativo,
                                    TopoImagem1920,
                                    TopoImagem1366,
                                    TopoImagem1280,
                                    TopoImagem1024,
                                    TopoImagem640,
                                    TopoImagem480,
                                    TopoVideo,
                                    TopoBotaoVideo,
                                    BaseImagem1920,
                                    BaseImagem1366,
                                    BaseImagem1280,
                                    BaseImagem1024,
                                    BaseImagem640,
                                    BaseImagem480,
                                        Url,
                                        MetaDescription,
                                        MetaKeywords,
                                        ImagemRedesSociais,
                                        FacebookMetaTitle,
                                        FacebookMetaDescription,
                                        TwitterMetaTitle,
                                        TwitterMetaDescription)
                           VALUES
                                  (@IndicadoNome,
                                   @IndicadoFoto,
                                   @DataExibicao,
                                   @TopoTitulo,
                                   @TopoTexto,
                                   @TopoBotaoYouTubeID,
                                   @BaseTexto,
                                   @UsuarioID,
                                   GetDate(),
                                   GetDate(),
                                   1,
                                    @TopoImagem1920,
                                    @TopoImagem1366,
                                    @TopoImagem1280,
                                    @TopoImagem1024,
                                    @TopoImagem640,
                                    @TopoImagem480,
                                    @TopoVideo,
                                    @TopoBotaoVideo,
                                    @BaseImagem1920,
                                    @BaseImagem1366,
                                    @BaseImagem1280,
                                    @BaseImagem1024,
                                    @BaseImagem640,
                                    @BaseImagem480,
                                        @Url,
                                        @MetaDescription,
                                        @MetaKeywords,
                                        @ImagemRedesSociais,
                                        @FacebookMetaTitle,
                                        @FacebookMetaDescription,
                                        @TwitterMetaTitle,
                                        @TwitterMetaDescription);
                            SELECT SCOPE_IDENTITY();";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade.ID = Convert.ToInt32(con.ExecuteScalar(SQL, entidade));
                con.Close();
            }

            return entidade.ID;
        }

        public void Atualizar(Editorial entidade)
        {

            string SQL = @"UPDATE Editorial
                              SET IndicadoNome       = @IndicadoNome,
                                  IndicadoFoto       = @IndicadoFoto,
                                  DataExibicao       = @DataExibicao,
                                  TopoTitulo         = @TopoTitulo,
                                  TopoTexto          = @TopoTexto,
                                  TopoBotaoYouTubeID = @TopoBotaoYouTubeID,
                                  BaseTexto          = @BaseTexto,
                                  UsuarioID          = @UsuarioID,
                                  DataModificacao    = GetDate(),
                                  Inativo            = @Inativo,
                                  TopoImagem1920     = @TopoImagem1920,
                                  TopoImagem1366     = @TopoImagem1366,
                                  TopoImagem1280     = @TopoImagem1280,
                                  TopoImagem1024     = @TopoImagem1024,
                                  TopoImagem640      = @TopoImagem640,
                                  TopoImagem480      = @TopoImagem480,
                                  TopoVideo          = @TopoVideo,
                                  TopoBotaoVideo     = @TopoBotaoVideo,
                                  BaseImagem1920     = @BaseImagem1920,
                                  BaseImagem1366     = @BaseImagem1366,
                                  BaseImagem1280     = @BaseImagem1280,
                                  BaseImagem1024     = @BaseImagem1024,
                                  BaseImagem640      = @BaseImagem640,
                                  BaseImagem480      = @BaseImagem480,
                                    Url 					= @Url,
                                    MetaDescription 		= @MetaDescription,
                                    MetaKeywords 			= @MetaKeywords,
                                    ImagemRedesSociais 		= @ImagemRedesSociais,
                                    FacebookMetaTitle 		= @FacebookMetaTitle,
                                    FacebookMetaDescription = @FacebookMetaDescription,
                                    TwitterMetaTitle 		= @TwitterMetaTitle,
                                    TwitterMetaDescription 	= @TwitterMetaDescription
                            WHERE ID = @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void Inativar(Editorial entidade)
        {
            string SQL = @"UPDATE Editorial
                              SET UsuarioID           = @UsuarioID,
                                  DataModificacao     = GetDate(),
                                  Inativo             = @Inativo
                            WHERE ID = @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void AtualizarCache(Editorial entidade)
        {
            string SQL = @"UPDATE Editorial
                              SET UsuarioID       = @UsuarioID,
                                  DataModificacao = GetDate(),
                                  ConteudoHtml    = @ConteudoHtml
                            WHERE ID = @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public Editorial Carregar(int id)
        {
            Editorial entidadeRetorno = null;

            string SQL = @"SELECT TOP 1
                                  ID,
                                  IndicadoNome,
                                  IndicadoFoto,
                                  DataExibicao,
                                  TopoTitulo,
                                  TopoTexto,
                                  TopoBotaoYouTubeID,
                                  BaseTexto,
                                  ConteudoHtml,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                    TopoImagem1920,
                                    TopoImagem1366,
                                    TopoImagem1280,
                                    TopoImagem1024,
                                    TopoImagem640,
                                    TopoImagem480,
                                    TopoVideo,
                                    TopoBotaoVideo,
                                    BaseImagem1920,
                                    BaseImagem1366,
                                    BaseImagem1280,
                                    BaseImagem1024,
                                    BaseImagem640,
                                    BaseImagem480,
                                        Url,
                                        MetaDescription,
                                        MetaKeywords,
                                        ImagemRedesSociais,
                                        FacebookMetaTitle,
                                        FacebookMetaDescription,
                                        TwitterMetaTitle,
                                        TwitterMetaDescription
                             FROM Editorial
                            WHERE ID = @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Editorial>(SQL, new { ID = id }).FirstOrDefault();
                con.Close();
            }
            return entidadeRetorno;
        }

        public Editorial Carregar(string url)
        {
            Editorial entidadeRetorno = null;

            string SQL = @"SELECT TOP 1
                                  ID,
                                  IndicadoNome,
                                  IndicadoFoto,
                                  DataExibicao,
                                  TopoTitulo,
                                  TopoTexto,
                                  TopoBotaoYouTubeID,
                                  BaseTexto,
                                  ConteudoHtml,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                    TopoImagem1920,
                                    TopoImagem1366,
                                    TopoImagem1280,
                                    TopoImagem1024,
                                    TopoImagem640,
                                    TopoImagem480,
                                    TopoVideo,
                                    TopoBotaoVideo,
                                    BaseImagem1920,
                                    BaseImagem1366,
                                    BaseImagem1280,
                                    BaseImagem1024,
                                    BaseImagem640,
                                    BaseImagem480,
                                        Url,
                                        MetaDescription,
                                        MetaKeywords,
                                        ImagemRedesSociais,
                                        FacebookMetaTitle,
                                        FacebookMetaDescription,
                                        TwitterMetaTitle,
                                        TwitterMetaDescription
                             FROM Editorial
                            WHERE Url = @Url;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Editorial>(SQL, new { Url = url }).FirstOrDefault();
                con.Close();
            }
            return entidadeRetorno;
        }

        public IList<Editorial> Listar()
        {

            IList<Editorial> entidadeRetorno = null;

            string SQL = @"SELECT ID,
                                  IndicadoNome,
                                  IndicadoFoto,
                                  DataExibicao,
                                  TopoTitulo,
                                  TopoTexto,
                                  TopoBotaoYouTubeID,
                                  BaseTexto,
                                  ConteudoHtml,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                    TopoImagem1920,
                                    TopoImagem1366,
                                    TopoImagem1280,
                                    TopoImagem1024,
                                    TopoImagem640,
                                    TopoImagem480,
                                    TopoVideo,
                                    TopoBotaoVideo,
                                    BaseImagem1920,
                                    BaseImagem1366,
                                    BaseImagem1280,
                                    BaseImagem1024,
                                    BaseImagem640,
                                    BaseImagem480,
                                        Url,
                                        MetaDescription,
                                        MetaKeywords,
                                        ImagemRedesSociais,
                                        FacebookMetaTitle,
                                        FacebookMetaDescription,
                                        TwitterMetaTitle,
                                        TwitterMetaDescription
                             FROM Editorial
                            ORDER BY DataExibicao DESC;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<Editorial>(SQL).ToList();
                con.Close();
            }
            return entidadeRetorno;
        }

        public IList<Editorial> Listar(Int32 skip, Int32 take, string palavraChave)
        {
            IList<Editorial> list = new List<Editorial>();

            string SQLCount = @"SELECT COUNT(*) FROM Editorial WHERE IndicadoNome LIKE @PalavraChave;";

            string SQL = @"SELECT ID,
                                  IndicadoNome,
                                  IndicadoFoto,
                                  DataExibicao,
                                  TopoTitulo,
                                  TopoTexto,
                                  TopoBotaoYouTubeID,
                                  BaseTexto,
                                  ConteudoHtml,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                    TopoImagem1920,
                                    TopoImagem1366,
                                    TopoImagem1280,
                                    TopoImagem1024,
                                    TopoImagem640,
                                    TopoImagem480,
                                    TopoVideo,
                                    TopoBotaoVideo,
                                    BaseImagem1920,
                                    BaseImagem1366,
                                    BaseImagem1280,
                                    BaseImagem1024,
                                    BaseImagem640,
                                    BaseImagem480,
                                        Url,
                                        MetaDescription,
                                        MetaKeywords,
                                        ImagemRedesSociais,
                                        FacebookMetaTitle,
                                        FacebookMetaDescription,
                                        TwitterMetaTitle,
                                        TwitterMetaDescription
                             FROM Editorial
                            WHERE IndicadoNome LIKE @PalavraChave
                            ORDER BY DataExibicao DESC
                           OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new
                {
                    PalavraChave = "%" + palavraChave + "%"
                }).FirstOrDefault();

                list = con.Query<Editorial>(SQL, new
                {
                    Skip = skip,
                    Take = take,
                    PalavraChave = "%" + palavraChave + "%"
                }).ToList();

                con.Close();
            }

            return list;
        }

        public int ContarPublicados()
        {
            int retorno = 0;

            string SQL = @"SELECT count(1)
                             FROM Editorial
                            WHERE DataExibicao <= getdate()
                              AND Inativo = 0;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                retorno = con.Query<int>(SQL).FirstOrDefault();
                con.Close();
            }
            return retorno;
        }

        public Editorial CarregarUltimaPublicada(DateTime data, int id = 0)
        {
            Editorial entidadeRetorno = null;

            string SQL = @"SELECT TOP 1
                                  ID,
                                  IndicadoNome,
                                  IndicadoFoto,
                                  DataExibicao,
                                  TopoTitulo,
                                  TopoTexto,
                                  TopoBotaoYouTubeID,
                                  BaseTexto,
                                  ConteudoHtml,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                    TopoImagem1920,
                                    TopoImagem1366,
                                    TopoImagem1280,
                                    TopoImagem1024,
                                    TopoImagem640,
                                    TopoImagem480,
                                    TopoVideo,
                                    TopoBotaoVideo,
                                    BaseImagem1920,
                                    BaseImagem1366,
                                    BaseImagem1280,
                                    BaseImagem1024,
                                    BaseImagem640,
                                    BaseImagem480,
                                        Url,
                                        MetaDescription,
                                        MetaKeywords,
                                        ImagemRedesSociais,
                                        FacebookMetaTitle,
                                        FacebookMetaDescription,
                                        TwitterMetaTitle,
                                        TwitterMetaDescription
                             FROM Editorial
                            WHERE DataExibicao <= @Data
                              AND Inativo = 0
                              AND ID <> @ID
                            ORDER BY DataExibicao DESC, ID DESC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidadeRetorno = con.Query<Editorial>(SQL, new { ID = id, Data = data }).FirstOrDefault();
                con.Close();
            }
            return entidadeRetorno;
        }

        public IList<Editorial> ListarPublicados()
        {

            IList<Editorial> entidadeRetorno = null;

            string SQL = @"SELECT ID,
                                  IndicadoNome,
                                  IndicadoFoto,
                                  DataExibicao,
                                  TopoTitulo,
                                  TopoTexto,
                                  TopoBotaoYouTubeID,
                                  BaseTexto,
                                  ConteudoHtml,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                    TopoImagem1920,
                                    TopoImagem1366,
                                    TopoImagem1280,
                                    TopoImagem1024,
                                    TopoImagem640,
                                    TopoImagem480,
                                    TopoVideo,
                                    TopoBotaoVideo,
                                    BaseImagem1920,
                                    BaseImagem1366,
                                    BaseImagem1280,
                                    BaseImagem1024,
                                    BaseImagem640,
                                    BaseImagem480,
                                        Url,
                                        MetaDescription,
                                        MetaKeywords,
                                        ImagemRedesSociais,
                                        FacebookMetaTitle,
                                        FacebookMetaDescription,
                                        TwitterMetaTitle,
                                        TwitterMetaDescription
                             FROM Editorial
                            WHERE DataExibicao <= getdate()
                              AND Inativo = 0
                            ORDER BY DataExibicao DESC;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidadeRetorno = con.Query<Editorial>(SQL).ToList();
                con.Close();
            }
            return entidadeRetorno;
        }

        public bool UrlLivre(int id, string url)
        {
            int retorno = 0;

            string SQL = @"SELECT count(1)
                             FROM Editorial
                            WHERE Url = @Url
                              AND ID <> @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                retorno = con.Query<int>(SQL, new { ID = id, Url = url}).FirstOrDefault();
                con.Close();
            }
            return retorno == 0;
        }


        public IList<string> getListUrls()
        {

            IList<string> listaRetorno = null;

            string SQL = @"SELECT Url
                             FROM Editorial
                            WHERE DataExibicao <= getdate()
                              AND Inativo = 0
                            ORDER BY DataExibicao DESC;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                listaRetorno = con.Query<string>(SQL).ToList();
                con.Close();
            }
            return listaRetorno;
        }
    }
}
