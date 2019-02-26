using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;
using Dapper;
using System.Data.Common;

namespace BradescoNext.Lib.DAL.ADO
{
    public class HistoriaReporteAbusoADO : ADOSuper, IHistoriaReporteAbusoDAL
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public int Inserir(HistoriaReporteAbuso entidade)
        {
            string SQL = @" INSERT INTO HistoriaReporteAbuso     
                (Nome,
                HistoriaID,
                Inativo,
                Mensagem,
                DataCadastro,
                EmailCrt) 
                VALUES    
                (@Nome,
                @HistoriaID,
                @Inativo,
                @Mensagem,
                @DataCadastro,
                " + Criptografar(entidade.Email) + @"); " +
                " SELECT ID = SCOPE_IDENTITY()";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade.ID = con.Query<int>(SQL, entidade).FirstOrDefault();
                con.Close();
            }
            return entidade.ID;
        }
        public void Atualizar(HistoriaReporteAbuso entidade)
        {
            string SQL = @"UPDATE HistoriaReporteAbuso SET  
                    (Nome = @Nome
                  ,EmailCrt=" + Criptografar(entidade.Email) + @"
                  ,DataCadastro=@DataCadastro
                  ,Inativo=@Inativo
                  ,Mensagem=@Mensagem
                  ,HistoriaID=@HistoriaID)
                    WHERE ID=@ID ";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public IList<HistoriaReporteAbuso> ListarReportesHistoria(int historiaID)
        {
            IList<HistoriaReporteAbuso> list = null;

            string SQL = @"SELECT ID
                    ,Nome
                    ," + Decriptografar("EmailCrt") + @" as Email
                    ,HistoriaID
                    ,Inativo
                    ,Mensagem
                    ,DataCadastro
                FROM 
                    HistoriaReporteAbuso
                WHERE (HistoriaID = @historiaID)";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                list = con.Query<HistoriaReporteAbuso>(SQL, new { historiaID = historiaID }).ToList();
                TotalRegistros = list.Count;
                con.Close();
            }

            return list;
        }
        public void InativarReporte(HistoriaReporteAbuso entidade)
        {
            string SQL = @"UPDATE HistoriaReporteAbuso SET  
                    Inativo=@Inativo
                    WHERE ID=@ID ";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public HistoriaReporteAbuso Carregar(int historiaID, string email)
        {
            HistoriaReporteAbuso entidade = null;
            string SQL = @"SELECT TOP 1 ID
                    ,Nome
                    ," + Decriptografar("EmailCrt") + @" as Email
                    ,HistoriaID
                    ,Inativo
                    ,Mensagem
                    ,DataCadastro
                FROM 
                    HistoriaReporteAbuso
                WHERE HistoriaID = @HistoriaID AND " + Decriptografar("EmailCrt") + @" = @Email";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<HistoriaReporteAbuso>(SQL, new
                {
                    Email = email,
                    HistoriaID = historiaID
                }).FirstOrDefault();

                con.Close();
            }

            return entidade;
        }
    }
}
