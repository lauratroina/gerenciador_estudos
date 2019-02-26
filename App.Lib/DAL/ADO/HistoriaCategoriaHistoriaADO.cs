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

    public class HistoriaCategoriaHistoriaADO : ADOSuper, IHistoriaCategoriaHistoriaDAL
    {
        /// <summary> 
        /// Inclui dados na base 
        /// </summary> 
        /// <param name="entidade"></param> 
        public void Inserir(int historiaID, int historiaCategoriaID)
        {
            string SQL = @" INSERT INTO HistoriaCategoriaHistoria
                (HistoriaID, HistoriaCategoriaID) VALUES (@HistoriaID, @HistoriaCategoriaID)";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL,
                    new
                    {
                        HistoriaID = historiaID,
                        HistoriaCategoriaID = historiaCategoriaID
                    });
                con.Close();
            }
        }

        public void Inserir(int historiaID, string listaHistoriaCategoriaID)
        {
            string SQL = string.Format(@"INSERT INTO HistoriaCategoriaHistoria (HistoriaID, HistoriaCategoriaID)
                                        (SELECT @HistoriaID, ID
                                         FROM HistoriaCategoria
                                         WHERE ID IN ({0}))", listaHistoriaCategoriaID);

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL,
                    new
                    {
                        HistoriaID = historiaID
                    });
                con.Close();
            }
        }

        /// <summary> 
        /// Exclui dados na base 
        /// </summary> 
        /// <param name="entidade"></param> 
        public void Excluir(int historiaID, int historiaCategoriaID)
        {
            string SQL = @" DELETE FROM HistoriaCategoriaHistoria
                            WHERE HistoriaID = @HistoriaID
                            AND HistoriaCategoriaID = @HistoriaCategoriaID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL,
                    new
                    {
                        HistoriaID = historiaID,
                        HistoriaCategoriaID = historiaCategoriaID
                    });
                con.Close();
            }
        }

        public void Excluir(int historiaID)
        {
            string SQL = @" DELETE FROM HistoriaCategoriaHistoria
                            WHERE HistoriaID = @HistoriaID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL,
                    new
                    {
                        HistoriaID = historiaID
                    });
                con.Close();
            }
        }

		 /// <summary> 
        /// Método que retorna todas as entidades. 
        /// </summary> 
        public IList<HistoriaCategoriaHistoria> Listar(int historiaID) 
        {
            IList<HistoriaCategoriaHistoria> list = null;
            string SQL = @"SELECT HCH.ID, HCH.HistoriaCategoriaID, HCH.HistoriaID,
                                  HC.ID, HC.Nome, HC.Inativo, HC.Ordem, HC.Valor, HC.CorLabel, HC.CorFont
                           FROM HistoriaCategoriaHistoria (NOLOCK) HCH
                           INNER JOIN HistoriaCategoria (NOLOCK) HC ON HCH.HistoriaCategoriaID = HC.ID
                           WHERE HCH.HistoriaID = @HistoriaID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<HistoriaCategoriaHistoria, HistoriaCategoria, HistoriaCategoriaHistoria>(SQL, (hch, hc) => {
                    hch.Categoria = hc;
                    return hch;
                }, new { HistoriaID = historiaID }).ToList();

                con.Close();

            }

            return list;
        }
		
    }
}