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
    public class EditorialBlocoADO : ADOSuper, IEditorialBlocoDAL
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public int Inserir(EditorialBloco entidade)
        {
            string SQL = @"INSERT 
                             INTO EditorialBloco
                                  (EditorialID,
                                   Tipo,
                                   Disposicao,
                                   Ordem,
                                   Nome,
                                   Texto,
                                   TextoResumido,
                                   Alinhamento,
                                   NomeArquivo,
                                    NomeArquivo1366,
                                    NomeArquivo1280,
                                    NomeArquivo1024,
                                    NomeArquivo640,
                                    NomeArquivo480,
                                   BotaoTitulo,
                                   BotaoLink,
                                   UsuarioID,
                                   DataCadastro,
                                   DataModificacao,
                                   Inativo)
                           VALUES
                                  (@EditorialID,
                                   @Tipo,
                                   @Disposicao,
                                   @Ordem,
                                   @Nome,
                                   @Texto,
                                   @TextoResumido,
                                   @Alinhamento,
                                   @NomeArquivo,
                                    @NomeArquivo1366,
                                    @NomeArquivo1280,
                                    @NomeArquivo1024,
                                    @NomeArquivo640,
                                    @NomeArquivo480,
                                   @BotaoTitulo,
                                   @BotaoLink,
                                   @UsuarioID,
                                   GetDate(),
                                   GetDate(),
                                   @Inativo);
                            SELECT SCOPE_IDENTITY();";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade.ID = Convert.ToInt32(con.ExecuteScalar(SQL, entidade));
                con.Close();
            }

            return entidade.ID;
        }

        public void Atualizar(EditorialBloco entidade)
        {

            string SQL = @"UPDATE EditorialBloco
                              SET EditorialID        = @EditorialID,
                                  Tipo               = @Tipo,
                                  Disposicao         = @Disposicao ,
                                  Ordem              = @Ordem,
                                  Nome               = @Nome,
                                  Texto              = @Texto,
                                  TextoResumido      = @TextoResumido,
                                  Alinhamento        = @Alinhamento,
                                  NomeArquivo        = @NomeArquivo,
                                    NomeArquivo1366  = @NomeArquivo1366,
                                    NomeArquivo1280  = @NomeArquivo1280,
                                    NomeArquivo1024  = @NomeArquivo1024,
                                    NomeArquivo640   = @NomeArquivo640,
                                    NomeArquivo480   = @NomeArquivo480,
                                  BotaoTitulo        = @BotaoTitulo,
                                  BotaoLink          = @BotaoLink,
                                  UsuarioID          = @UsuarioID,
                                  DataModificacao    = GetDate(),
                                  Inativo            = @Inativo
                            WHERE ID = @ID;";
            
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void Inativar(EditorialBloco entidade)
        {
            string SQL = @"UPDATE EditorialBloco
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

        public EditorialBloco Carregar(int id)
        {
            EditorialBloco entidadeRetorno = null;

            string SQL = @"SELECT TOP 1
                                  ID,
                                  EditorialID,
                                  Tipo,
                                  Disposicao,
                                  Ordem,
                                  Nome,
                                  Texto,
                                  TextoResumido,
                                  Alinhamento,
                                  NomeArquivo,
                                    NomeArquivo1366,
                                    NomeArquivo1280,
                                    NomeArquivo1024,
                                    NomeArquivo640,
                                    NomeArquivo480,
                                  BotaoTitulo,
                                  BotaoLink,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo
                             FROM EditorialBloco
                            WHERE ID = @ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<EditorialBloco>(SQL, new { ID = id }).FirstOrDefault();
                con.Close();
            }
            return entidadeRetorno;
        }

        public IList<EditorialBloco> Listar()
        {

            IList<EditorialBloco> entidadeRetorno = null;

            string SQL = @"SELECT ID,
                                  EditorialID,
                                  Tipo,
                                  Disposicao,
                                  Ordem,
                                  Nome,
                                  Texto,
                                  TextoResumido,
                                  Alinhamento,
                                  NomeArquivo,
                                    NomeArquivo1366,
                                    NomeArquivo1280,
                                    NomeArquivo1024,
                                    NomeArquivo640,
                                    NomeArquivo480,
                                  BotaoTitulo,
                                  BotaoLink,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo
                             FROM EditorialBloco
                            ORDER BY Inativo ASC, Ordem ASC;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<EditorialBloco>(SQL).ToList();
                con.Close();
            }
            return entidadeRetorno;
        }

        public IList<EditorialBloco> Listar(int editorialID, bool apenasAtivos, Int32 skip, Int32 take, string palavraChave)
        {

            IList<EditorialBloco> list = null;
            
            string SQLCount = @"SELECT COUNT(*) 
                                  FROM EditorialBloco 
                                 WHERE Nome LIKE @PalavraChave 
                                   AND EditorialID = @editorialID;";

            string SQL = @"SELECT EB.ID,
                                  EB.EditorialID,
                                  EB.Tipo,
                                  EB.Disposicao,
                                  EB.Ordem,
                                  EB.Nome,
                                  EB.Texto,
                                  EB.TextoResumido,
                                  EB.Alinhamento,
                                  EB.NomeArquivo,
                                    EB.NomeArquivo1366,
                                    EB.NomeArquivo1280,
                                    EB.NomeArquivo1024,
                                    EB.NomeArquivo640,
                                    EB.NomeArquivo480,
                                  EB.BotaoTitulo,
                                  EB.BotaoLink,
                                  EB.UsuarioID,
                                  EB.DataCadastro,
                                  EB.DataModificacao,
                                  EB.Inativo,
                                  EBA.ID,
                                  EBA.EditorialBlocoID,
                                  EBA.Tipo,
                                  EBA.Descricao,
                                  EBA.NomeArquivo,
                                  EBA.NomeVideo,
                                  EBA.YoutubeID,
                                  EBA.UsuarioID,
                                  EBA.DataCadastro,
                                  EBA.DataModificacao,
                                  EBA.Inativo,
                                  EBA.Ordem
                             FROM EditorialBloco (nolock) EB
                        LEFT JOIN EditorialBlocoArquivo (nolock) EBA ON EB.ID = EBA.EditorialBlocoID
                            WHERE EB.Nome LIKE @PalavraChave 
                              AND EB.EditorialID = @editorialID
                            ORDER BY EB.Inativo ASC, EB.Ordem ASC, EBA.Ordem ASC
                           OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

            if (apenasAtivos)
            {
                SQLCount = SQLCount.Replace("WHERE ", "WHERE Inativo = 0 AND ");
                SQL = SQL.Replace("WHERE ", "WHERE EB.Inativo = 0 AND ");
            }

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                
                TotalRegistros = con.Query<int>(SQLCount, new
                {
                    EditorialID = editorialID,
                    PalavraChave = "%" + palavraChave + "%"
                }).FirstOrDefault();

                list = new List<EditorialBloco>();
                con.Query<EditorialBloco, EditorialBlocoArquivo, int>(SQL, (bloco, arquivo) => {

                    EditorialBloco item = list.FirstOrDefault(t => t.ID == bloco.ID);
                    if (item == null)
                    {
                        item = bloco;
                        item.ListaArquivos = new List<EditorialBlocoArquivo>();
                        list.Add(item);

                    }
                    if (arquivo != null)
                    {
                        item.ListaArquivos.Add(arquivo);
                    }
                    return 1;
                }, new
                {
                    EditorialID = editorialID,
                    Skip = skip,
                    Take = take,
                    PalavraChave = "%" + palavraChave + "%"
                }).ToList();

                con.Close();
            }
            return list;
        }

        public IList<EditorialBloco> Listar(Int32 skip, Int32 take)
        {
            IList<EditorialBloco> list = new List<EditorialBloco>();

            string SQLCount = @"SELECT COUNT(*) FROM EditorialBloco;";

            string SQL = @"SELECT ID,
                                  EditorialID,
                                  Tipo,
                                  Disposicao,
                                  Ordem,
                                  Nome,
                                  Texto,
                                  TextoResumido,
                                  Alinhamento,
                                  NomeArquivo,
                                    NomeArquivo1366,
                                    NomeArquivo1280,
                                    NomeArquivo1024,
                                    NomeArquivo640,
                                    NomeArquivo480,
                                  BotaoTitulo,
                                  BotaoLink,
                                  UsuarioID,
                                  DataCadastro,
                                  DataModificacao,
                                  Inativo
                             FROM EditorialBloco
                            ORDER BY Inativo ASC, Ordem ASC
                           OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount).FirstOrDefault();
                list = con.Query<EditorialBloco>(SQL, new
                {
                    Skip = skip,
                    Take = take
                }).ToList();
                con.Close();
            }

            return list;
        }
    }
}
