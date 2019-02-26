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

namespace BradescoNext.Lib.DAL.ADO
{

    public class HistoriaModeracaoADO : ADOSuper, IHistoriaModeracaoDAL
    {
        /// <summary> 
        /// Inclui dados na base 
        /// </summary> 
        /// <param name="entidade"></param> 
        public int Inserir(HistoriaModeracao entidade)
        {
            string SQL = @" INSERT INTO HistoriaModeracao
                                (HistoriaID, UsuarioID, DataInicioAvaliacao) 
                            VALUES 
                                (@HistoriaID, @UsuarioID, @DataInicioAvaliacao);
                            SELECT ID = SCOPE_IDENTITY()";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade.ID = con.Query<int>(SQL, entidade).FirstOrDefault();
                con.Close();
            }

            return entidade.ID;
        }

        public void Atualizar(HistoriaModeracao entidade)
        {
            string SQL = @"UPDATE HistoriaModeracao SET
                                  Nota = @Nota,
                                  NotaPonderada = @NotaPonderada,
                                  DataFimAvaliacao = @DataFimAvaliacao
                           WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public int QtdeModeracaoRealizada(int historiaID, int usuarioID)
        {
            int id = 0;
            string SQL = @"SELECT COUNT(1)
                           FROM HistoriaModeracao (NOLOCK)
                           WHERE HistoriaID = @HistoriaID
                           AND UsuarioID <> @UsuarioID
                           AND ((@DataAtual < (SELECT DATEADD(MINUTE, ModeracaoMinutos, DataInicioAvaliacao) FROM CONFIGURACAO))
                            OR DataFimAvaliacao is not null
                           )";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                id = con.Query<int>(SQL,
                    new
                    {
                        HistoriaID = historiaID,
                        UsuarioID = usuarioID,
                        DataAtual = DateTime.Now
                    }).FirstOrDefault();
                con.Close();
            }

            return id;
        }

        public int QtdeModeracaoRealizada(int historiaID)
        {
            int id = 0;
            string SQL = @"SELECT COUNT(1)
                           FROM HistoriaModeracao (NOLOCK)
                           WHERE HistoriaID = @HistoriaID
                           AND DataFimAvaliacao is not null";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                id = con.Query<int>(SQL,
                    new
                    {
                        HistoriaID = historiaID,
                    }).FirstOrDefault();
                con.Close();
            }

            return id;
        }

        public bool ModeracaoRealizada(int historiaID, int usuarioID)
        {
            bool moderacaoRealizada = false;
            string SQL = @"SELECT COUNT(1)
                           FROM HistoriaModeracao (NOLOCK)
                           WHERE HistoriaID = @HistoriaID
                           AND UsuarioID = @UsuarioID
                           AND DataFimAvaliacao IS NOT NULL";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                moderacaoRealizada = con.Query<int>(SQL,
                    new
                    {
                        HistoriaID = historiaID,
                        UsuarioID = usuarioID
                    }).FirstOrDefault() > 0;
                con.Close();
            }

            return moderacaoRealizada;
        }

        public int BuscaHistoriaEmAnalise(int historiaID, int usuarioID)
        {
            int id = 0;
            string SQL = @"SELECT TOP 1 ID
                           FROM HistoriaModeracao (NOLOCK)
                           WHERE HistoriaID = @HistoriaID
                           AND UsuarioID = @UsuarioID
                           AND DataFimAvaliacao IS NULL
		                   AND (@DataAtual < (SELECT DATEADD(MINUTE, ModeracaoMinutos, DataInicioAvaliacao) FROM CONFIGURACAO (NOLOCK)))";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                id = con.Query<int>(SQL,
                    new
                    {
                        HistoriaID = historiaID,
                        UsuarioID = usuarioID,
                        DataAtual = DateTime.Now
                    }).FirstOrDefault();
                con.Close();
            }

            return id;
        }
    }
}