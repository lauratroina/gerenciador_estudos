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
    public class EditorialBlocoArquivoADO : ADOSuper, IEditorialBlocoArquivoDAL
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public int Inserir(EditorialBlocoArquivo entidade)
        {
            string SQL = @"INSERT 
                             INTO EditorialBlocoArquivo
                                  (EditorialBlocoID,
                                   Tipo,
                                   Descricao,
                                   NomeArquivo,
                                   NomeVideo,
                                   YoutubeID,
                                   UsuarioID,
                                   DataCadastro,
                                   DataModificacao,
                                   Inativo,
                                   Ordem)
                           VALUES
                                  (@EditorialBlocoID,
                                   @Tipo,
                                   @Descricao,
                                   @NomeArquivo,
                                   @NomeVideo,
                                   @YoutubeID,
                                   @UsuarioID,
                                   GetDate(),
                                   GetDate(),
                                   0,
                                   @Ordem);
                            SELECT SCOPE_IDENTITY();";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade.ID = Convert.ToInt32(con.ExecuteScalar(SQL, entidade));
                con.Close();
            }

            return entidade.ID;
        }

        public void Atualizar(EditorialBlocoArquivo entidade)
        {

            string SQL = @"UPDATE EditorialBlocoArquivo
                              SET EditorialBlocoID = @EditorialBlocoID,
                                  Tipo             = @Tipo,
                                  Descricao        = @Descricao,
                                  NomeArquivo      = @NomeArquivo,
                                  NomeVideo        = @NomeVideo,
                                  YoutubeID        = @YoutubeID,
                                  UsuarioID        = @UsuarioID,
                                  DataModificacao  = GetDate(),
                                  Inativo          = @Inativo,
                                  Ordem            = @Ordem
                            WHERE ID = @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void Inativar(EditorialBlocoArquivo entidade)
        {
            string SQL = @"UPDATE EditorialBlocoArquivo
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

        public EditorialBlocoArquivo Carregar(int id)
        {
            EditorialBlocoArquivo entidadeRetorno = null;

            string SQL = @"SELECT TOP 1
                                  ID,
                                  EditorialBlocoID,
                                  Tipo,
                                  Descricao,
                                  NomeArquivo,
                                  NomeVideo,
                                  YoutubeID,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                  Ordem
                             FROM EditorialBlocoArquivo
                            WHERE ID = @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<EditorialBlocoArquivo>(SQL, new { ID = id }).FirstOrDefault();
                con.Close();
            }
            return entidadeRetorno;
        }

        public IList<EditorialBlocoArquivo> Listar()
        {

            IList<EditorialBlocoArquivo> entidadeRetorno = null;

            string SQL = @"SELECT ID,
                                  EditorialBlocoID,
                                  Tipo,
                                  Descricao,
                                  NomeArquivo,
                                  NomeVideo,
                                  YoutubeID,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                  Ordem
                             FROM EditorialBlocoArquivo
                            ORDER BY Ordem ASC;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<EditorialBlocoArquivo>(SQL).ToList();
                con.Close();
            }
            return entidadeRetorno;
        }

        public IList<EditorialBlocoArquivo> Listar(Int32 skip, Int32 take)
        {
            IList<EditorialBlocoArquivo> list = new List<EditorialBlocoArquivo>();

            string SQLCount = @"SELECT COUNT(*) FROM EditorialBlocoArquivo;";

            string SQL = @"SELECT ID,
                                  EditorialBlocoID,
                                  Tipo,
                                  Descricao,
                                  NomeArquivo,
                                  NomeVideo,
                                  YoutubeID,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                  Ordem
                             FROM EditorialBlocoArquivo
                            ORDER BY Ordem ASC
                           OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount).FirstOrDefault();
                list = con.Query<EditorialBlocoArquivo>(SQL, new
                {
                    Skip = skip,
                    Take = take
                }).ToList();
                con.Close();
            }

            return list;
        }


        public IList<EditorialBlocoArquivo> ListarPorBlocoID(int blocoID, bool apenasAtivos)
        {
            IList<EditorialBlocoArquivo> entidadeRetorno = null;

            string SQL = @"SELECT ID,
                                  EditorialBlocoID,
                                  Tipo,
                                  Descricao,
                                  NomeArquivo,
                                  NomeVideo,
                                  YoutubeID,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo,
                                  Ordem
                             FROM EditorialBlocoArquivo
                            WHERE EditorialBlocoID = @BlocoID
                            ORDER BY Ordem ASC;";


            if (apenasAtivos)
            {
                SQL = SQL.Replace("WHERE ", "WHERE Inativo = 0 AND ");
            }

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidadeRetorno = con.Query<EditorialBlocoArquivo>(SQL, new { BlocoID = blocoID }).ToList();
                con.Close();
            }
            return entidadeRetorno;
        }
        public void Excluir(int id)
        {
            string SQL = @"DELETE From EditorialBlocoArquivo
                            WHERE ID = @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }

        }
    }
}
