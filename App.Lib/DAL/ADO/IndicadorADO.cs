using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.DAL.ADO
{
    public class IndicadorADO : ADOSuper, IIndicadorDAL
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
        public void Inserir(Indicador entidade)
        {
            string SQL = @" INSERT INTO Indicador (FacebookID, FacebookToken, DataEnvioEmailContinuacao, QuantidadeEmailContinuacao, DataCadastro, DataModificacao, Nome, EmailCrt) 
                                 VALUES (@FacebookID, @FacebookToken, @DataEnvioEmailContinuacao, @QuantidadeEmailContinuacao, @DataCadastro, @DataModificacao, @Nome, " + Criptografar(entidade.Email) + ") ";

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
        public void Atualizar(Indicador entidade)
        {
            string SQL = @"UPDATE Indicador SET  
                               FacebookID = @FacebookID
                              ,FacebookToken = @FacebookToken
                              ,DataEnvioEmailContinuacao = @DataEnvioEmailContinuacao
                              ,QuantidadeEmailContinuacao = @QuantidadeEmailContinuacao
                              ,DataCadastro = @DataCadastro
                              ,DataModificacao = @DataModificacao
                              ,Nome = @Nome
                              ,EmailCrt = " + Criptografar(entidade.Email) + @"
                          WHERE ID=@ID ";


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary>
        /// Retorna o Indicador por e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Indicador Carregar(string email)
        {
            Indicador entidade = null;
            string SQL = @"SELECT TOP 1 
                               Indicador.ID
                              ,Indicador.FacebookID
                              ,Indicador.FacebookToken
                              ,Indicador.DataEnvioEmailContinuacao
                              ,Indicador.QuantidadeEmailContinuacao
                              ,Indicador.DataCadastro
                              ,Indicador.DataModificacao
                              ,Indicador.Nome
                              ," + Decriptografar("EmailCrt") + @" as Email
                            FROM Indicador (NOLOCK)
                           WHERE " + Decriptografar("EmailCrt") + " = @Email";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<Indicador>(SQL, new { Email = email }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }


        public void AtualizarContinuacao(Indicador indicador)
        {
            string SQL = @"UPDATE Indicador SET 
                                  DataEnvioEmailContinuacao=@DataEnvioEmailContinuacao
                                 ,DataModificacao=@DataModificacao
                                 ,QuantidadeEmailContinuacao=@QuantidadeEmailContinuacao
                            WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, indicador);
                con.Close();
            }
        }

        public IList<Indicador> ListarPendentesRoboContinuacao(DateTime dataAtual, int horasEmailContinuacao, int quantidadeEmailContinuacao)
        {
            IList<Indicador> list = new List<Indicador>();

            string SQL = @"SELECT Indicador.ID
              ,Indicador.FacebookID
              ,Indicador.FacebookToken
              ,Indicador.DataEnvioEmailContinuacao
              ,Indicador.QuantidadeEmailContinuacao
              ,Indicador.Nome
              ," + Decriptografar("Indicador.EmailCrt") + @" as Email
            FROM Indicador (NOLOCK)
            WHERE (SELECT COUNT(*) FROM Historia WHERE " + Decriptografar("Historia.IndicadorEmailCrt") + " = " + Decriptografar("Indicador.EmailCrt") + ") = 0 AND DATEDIFF(HOUR, DataEnvioEmailContinuacao, @DataEmailContinuacao) >= @RoboEmailQtdHoras AND QuantidadeEmailContinuacao < @QuantidadeEmailContinuacaoRobo";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Indicador>(SQL, new 
                {
                    QuantidadeEmailContinuacaoRobo = quantidadeEmailContinuacao,
                    RoboEmailQtdHoras = horasEmailContinuacao,
                    DataEmailContinuacao = dataAtual
                }).ToList();

                con.Close();
            }
            return list;
        }

    }
}

