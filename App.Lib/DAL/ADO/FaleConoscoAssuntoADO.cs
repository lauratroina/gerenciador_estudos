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

    public class FaleConoscoAssuntoADO : ADOSuper, IFaleConoscoAssuntoDAL
    {

		private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public void Inserir(FaleConoscoAssunto entidade)
        {
            throw new NotImplementedException();
        }
        public void Atualizar(FaleConoscoAssunto entidade)
        {
            throw new NotImplementedException();
        }
        public FaleConoscoAssunto Carregar(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retorna um assunto do fale conosco carregado com a Lista de Destinos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FaleConoscoAssunto CarregarComDestinos(int id)
        {
            FaleConoscoAssunto entidade = null;

            string SQL = @"SELECT 
                              fca.ID, fca.Nome, fca.Ordem, fca.Inativo,
                              fcd.ID, fcd.Email, fcd.Nome
                    FROM FaleConoscoAssunto (NOLOCK) fca
              INNER JOIN FaleConoscoAssuntoDestino (NOLOCK) fcad ON fcad.FaleConoscoAssuntoID = fca.ID
              INNER JOIN FaleConoscoDestino (NOLOCK) fcd ON fcd.ID = fcad.FaleConoscoDestinoID
                   WHERE fca.ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                con.Query<FaleConoscoAssunto, FaleConoscoDestino, int>(SQL, (assunto, destino) =>
                {
                    if (entidade == null)
                    {
                        entidade = assunto;
                        entidade.Destinos = new List<FaleConoscoDestino>();
                    }
                    entidade.Destinos.Add(destino);
                    return destino.ID;
                }, new
                {
                    ID = id
                }).ToList();

                con.Close();
            }

            return entidade;
        }

        /// <summary>
        /// Método que retorna a lista de assuntos do FaleConosco
        /// </summary>
        /// <returns></returns>
        public IList<FaleConoscoAssunto> Listar(bool somenteAtivos = true)
        {
            IList<FaleConoscoAssunto> list = null;

            List<string> lstWhere = new List<string>();
            if (somenteAtivos)
            {
                lstWhere.Add("Inativo = 0");
            }
            string where = (lstWhere.Count == 0) ? "" : " WHERE " + string.Join(" AND ", lstWhere);

            string SQL = @"SELECT ID, Nome, Ordem, Inativo FROM FaleConoscoAssunto " + where + " ORDER BY Ordem";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<FaleConoscoAssunto>(SQL).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }

        /// <summary>
        /// Método que retorna a lista de FaleConoscoAssunto de uma determinada página
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="totalRegPorPagina"></param>
        /// <returns></returns>
        public IList<FaleConoscoAssunto> Listar(Int32 skip, Int32 take, bool somenteAtivos = true)
        {

            List<string> lstWhere = new List<string>();
            if (somenteAtivos)
            {
                lstWhere.Add("Inativo = 0");
            }
            string where = (lstWhere.Count == 0) ? "" : " WHERE " + string.Join(" AND ", lstWhere);

            IList<FaleConoscoAssunto> list = null;

            string SQLCount = @"SELECT COUNT(*) FROM FaleConoscoAssunto";
            string SQL = @"SELECT 
                        ID,
                        Nome, 
                        Ordem, 
                        Inativo
                    FROM FaleConoscoAssunto " + where + " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount).FirstOrDefault();
                list = con.Query<FaleConoscoAssunto>(SQL, new { Skip = skip, Take = take }).ToList();

                con.Close();


            }

            return list;
        }

    }
}