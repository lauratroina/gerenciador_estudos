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
using System.Linq;
using Dapper;

namespace BradescoNext.Lib.DAL.ADO
{

    public class HistoriaMidiaADO : ADOSuper, IHistoriaMidiaDAL
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
        public void Inserir(HistoriaMidia entidade)
        {
            string SQL = @" INSERT INTO HistoriaMidia     
             (HistoriaID,ArquivoNome,DataCadastro,Inativo,AdicionadoOrigem,InativoOrigem,UsuarioID,ArquivoTipo) 
              VALUES    
             (@HistoriaID,@ArquivoNome,@DataCadastro,@Inativo,@AdicionadoOrigemDB,@InativoOrigemDB,@UsuarioID,@ArquivoTipoDB) ";

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
        public void Atualizar(HistoriaMidia entidade)
        {
            string SQL = @"UPDATE HistoriaMidia SET HistoriaID=@HistoriaID,ArquivoNome=@ArquivoNome,
        DataCadastro=@DataCadastro,Inativo=@Inativo,AdicionadoOrigem=@AdicionadoOrigemDB,
        InativoOrigem=@InativoOrigemDB,UsuarioID=@UsuarioID, ArquivoTipo=@ArquivoTipoDB
        WHERE ID=@ID";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary> 
        /// Método que carrega uma entidade. 
        /// </summary> 
        /// <param name="entidade">Entidade a ser carregada (somente o identificador é necessário).</param> 
        /// <returns></returns> 
        public HistoriaMidia Carregar(int id)
        {
            HistoriaMidia entidadeRetorno = null;

            string SQL = @"SELECT ID
              ,HistoriaID
              ,ArquivoNome
              ,DataCadastro
              ,Inativo
              ,AdicionadoOrigem as AdicionadoOrigemDB
              ,InativoOrigem as InativoOrigemDB
              ,UsuarioID
              ,ArquivoTipo as ArquivoTipoDB
            FROM HistoriaMidia WHERE ID=@ID ";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidadeRetorno = con.Query<HistoriaMidia>(SQL, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidadeRetorno;
        }

        /// <summary> 
        /// Método que retorna todas as entidades. 
        /// SELECT * FROM HistoriaMidia ORDER BY ID DESC 
        /// </summary> 
        public IList<HistoriaMidia> Listar(int historiaID, bool somenteAtivos = true)
        {
            IList<HistoriaMidia> list = null;

            string SQL = @"SELECT 
                            HM.ID
                          , HM.HistoriaID
                          , HM.ArquivoNome
                          , HM.DataCadastro
                          , HM.Inativo
                          , HM.AdicionadoOrigem as AdicionadoOrigemDB
                          , HM.InativoOrigem as InativoOrigemDB
                          , HM.UsuarioID
                          , HM.ArquivoTipo as ArquivoTipoDB
                          , U.ID, U.Nome, U.Login
                FROM HistoriaMidia (NOLOCK) HM 
           LEFT JOIN Usuario (NOLOCK) U ON HM.UsuarioID = U.ID
               WHERE HM.HistoriaID = @HistoriaID " + (somenteAtivos ? " AND HM.Inativo = 0 " : "") + 
          " ORDER BY HM.ArquivoTipo, HM.DataCadastro DESC";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<HistoriaMidia, Usuario, HistoriaMidia>(SQL, (hm, usuario) =>
                {
                    hm.Usuario = usuario;
                    return hm;
                }, new { HistoriaID = historiaID }).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }

    }
}