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

    public class ParentescoADO : ADOSuper, IParentescoDAL
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
        public void Inserir(Parentesco entidade)
        {            
			         string SQL = @" INSERT INTO Parentesco     
         (Nome) 
          VALUES    
         (@Nome) "; 


            DbCommand command = _db.GetSqlStringCommand(SQL);

            _db.AddInParameter(command, "Nome", DbType.String, entidade.Nome); 


            // Executa a query. 
            _db.ExecuteNonQuery(command);
        }


        /// <summary> 
        /// Método que atualiza os dados entidade. 
        /// </summary> 
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        public void Atualizar(Parentesco entidade)
        {
                     string SQL = @"UPDATE Parentesco SET  
        Nome=@Nome
         WHERE ID=@ID "; 


            DbCommand command = _db.GetSqlStringCommand(SQL);

            _db.AddInParameter(command, "Nome", DbType.String, entidade.Nome); 
_db.AddInParameter(command, "ID", DbType.Decimal, entidade.ID); 


            _db.ExecuteNonQuery(command);
        }


        /// <summary> 
        /// Método que remove uma entidade do repositório. 
        /// </summary> 
        /// <param name="entidade">Entidade a ser excluída (somente o identificador é necessário).</param>		 
        public void Excluir(Parentesco entidade)
        {
            string SQL = @"DELETE FROM Parentesco WHERE ID=@ID  ";

            DbCommand command = _db.GetSqlStringCommand(SQL);

            _db.AddInParameter(command, "ID", DbType.Int32, entidade.ID);

            _db.ExecuteNonQuery(command);
        }


        /// <summary> 
        /// Método que carrega uma entidade. 
        /// </summary> 
        /// <param name="entidade">Entidade a ser carregada (somente o identificador é necessário).</param> 
        /// <returns></returns> 
        public Parentesco Carregar(Parentesco entidade)
        {
            Parentesco entidadeRetorno = null;

            string SQL = @"SELECT * 
								FROM Parentesco 
								WHERE ID=@ID ";

            DbCommand command = _db.GetSqlStringCommand(SQL);

            _db.AddInParameter(command, "ID", DbType.Int32, entidade.ID);

            IDataReader entidades = _db.ExecuteReader(command);

            if (entidades.Read())
            {
                entidadeRetorno = new Parentesco();
                Popula(entidades, entidadeRetorno);
            }
            entidades.Close();
            return entidadeRetorno;
        }

		 /// <summary> 
        /// Método que retorna todas as entidades. 
		/// SELECT * FROM Parentesco ORDER BY ID DESC 
        /// </summary> 
		public IList<Parentesco> Listar() 
        {
            IList<Parentesco> list = null;
            string SQL = @"SELECT * FROM Parentesco ORDER BY Ordem ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Parentesco>(SQL).ToList();
                TotalRegistros = list.Count;

                con.Close();


            }

            return list;
        }

        public IList<Parentesco> Listar(bool somenteAtivos)
        {
            IList<Parentesco> list = null;
            string SQL = @"SELECT * FROM Parentesco WHERE Inativo = @Inativo ORDER BY Ordem ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Parentesco>(SQL, new { Inativo = !somenteAtivos }).ToList();
                TotalRegistros = list.Count;

                con.Close();


            }

            return list;
        }
        
        /// <summary> 
        /// Método que retorna todas as entidades. 
		/// SELECT * FROM Parentesco ORDER BY ID DESC 
        /// </summary> 
        public IList<Parentesco> Listar(Int32 paginaAtual, Int32 totalRegPorPagina) 
        {
            Parentesco entidadeRetorno = null;

            string SQL = @"WITH Members AS (SELECT Parentesco.ID
,Parentesco.Nome
    , ROW_NUMBER() OVER ( ORDER BY ID DESC) AS RowNumber  FROM Parentesco
  WHERE 1=1 "; 
 

 SQL += @"  )  SELECT	RowNumber, ID
,Nome
 
                                    FROM	Members
                                     WHERE RowNumber <= (1+(@paginaAtual-1)) * @totalRegPagina  AND  RowNumber >= 1+((@paginaAtual-1)* @totalRegPagina)           ORDER BY RowNumber ASC; "; 

            DbCommand command = _db.GetSqlStringCommand(SQL);
			
			
			
			_db.AddInParameter(command, "paginaAtual", DbType.Int32, paginaAtual);
            _db.AddInParameter(command, "totalRegPagina", DbType.Int32, totalRegPorPagina);

            IDataReader entidades = _db.ExecuteReader(command);

            IList<Parentesco> list = new List<Parentesco>();

            while (entidades.Read())
            {
                entidadeRetorno = new Parentesco();
                Popula(entidades, entidadeRetorno);
                list.Add(entidadeRetorno);
            }
            entidades.Close();
            return list;
        }


        /// <summary> 
        /// Método que retorna popula uma entidade baseado nos dados de um DataReader. 
        /// </summary> 
        /// <param name="entidades">DataReader contendo os dados da entidade.</param> 
        /// <param name="entidade">Entidade a ser populada.</param> 
        public static void Popula(IDataReader entidades, Parentesco entidade)
        {
                        if (entidades["ID"] != DBNull.Value)  
                entidade.ID = Convert.ToInt32(entidades["ID"]); 
            if (entidades["Nome"] != DBNull.Value)  
                entidade.Nome = entidades["Nome"].ToString(); 

        }
		
    }
}