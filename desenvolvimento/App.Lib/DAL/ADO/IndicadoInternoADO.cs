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
using BradescoNext.Lib.Models;


namespace BradescoNext.Lib.DAL.ADO
{

    public class IndicadoInternoADO : ADOSuper, IIndicadoInternoDAL
    {

        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public void InserirLog(int id)
        {
            string SQL = @"INSERT INTO LogIndicadoInterno     
                               (IndicadoInternoID,
                                PrimeiroNome,
                                NomeMeio,
                                Sobrenome,
                                NomeMae,
                                DataNascimento,
                                Genero,
                                DocumentoTipoID,
                                DocumentoNumeroCrt,
                                DocumentoOrgaoEmissor,
                                DocumentoEstadoID,
                                DocumentoDataExpedicao,
                                EnderecoCrt,
                                EnderecoComplementoCrt,
                                EnderecoPaisID,
                                EnderecoCidadeID,
                                EnderecoCidadeEstadoNaoBR,
                                EnderecoCEP,
                                EnderecoBairro,
                                EnderecoPais,
                                TelefoneResidencialCrt, 
                                TelefoneComercialCrt,
                                EmailCrt,
                                CidadeParticipanteID,
                                Condutor,
                                HistoriaTitulo,
                                HistoriaCategoriaID,
                                HistoriaTexto,
                                HistoriaArquivoNome,
                                UsuarioID,
                                DataModificacao,
                                DataCadastro,
                                Inativo,
                                IndicadoInternoCategoriaID,
                                RemoverGaleria) 
                        (SELECT @ID,
                                PrimeiroNome,
                                NomeMeio,
                                Sobrenome,
                                NomeMae,
                                DataNascimento,
                                Genero,
                                DocumentoTipoID,
                                DocumentoNumeroCrt,
                                DocumentoOrgaoEmissor,
                                DocumentoEstadoID,
                                DocumentoDataExpedicao,
                                EnderecoCrt,
                                EnderecoComplementoCrt,
                                EnderecoPaisID,
                                EnderecoCidadeID,
                                EnderecoCidadeEstadoNaoBR,
                                EnderecoCEP,
                                EnderecoBairro,
                                EnderecoPais,
                                TelefoneResidencialCrt, 
                                TelefoneComercialCrt,
                                EmailCrt,
                                CidadeParticipanteID,
                                Condutor,
                                HistoriaTitulo,
                                HistoriaCategoriaID,
                                HistoriaTexto,
                                HistoriaArquivoNome,
                                UsuarioID,
                                DataModificacao,
                                DataCadastro,
                                Inativo,
                                IndicadoInternoCategoriaID,
                                RemoverGaleria
                FROM IndicadoInterno
                WHERE ID = @ID) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        /// <summary> 
        /// Inclui dados na base 
        /// </summary> 
        /// <param name="entidade"></param> 
        public void Inserir(IndicadoInterno entidade)
        {
            string SQL = @"INSERT INTO IndicadoInterno     
                                (PrimeiroNome,
                                NomeMeio,
                                Sobrenome,
                                NomeMae,
                                DataNascimento,
                                Genero,
                                DocumentoTipoID,
                                DocumentoNumeroCrt,
                                DocumentoOrgaoEmissor,
                                DocumentoEstadoID,
                                DocumentoDataExpedicao,
                                EnderecoCrt,
                                EnderecoComplementoCrt,
                                EnderecoPaisID,
                                EnderecoCidadeID,
                                EnderecoCidadeEstadoNaoBR,
                                EnderecoCEP,
                                EnderecoBairro,
                                EnderecoPais,
                                TelefoneResidencialCrt, 
                                TelefoneComercialCrt,
                                EmailCrt,
                                CidadeParticipanteID,
                                Condutor,
                                HistoriaTitulo,
                                HistoriaCategoriaID,
                                HistoriaTexto,
                                HistoriaArquivoNome,
                                UsuarioID,
                                DataModificacao,
                                DataCadastro,
                                Inativo,
                                IndicadoInternoCategoriaID,
                                RemoverGaleria) 
                            VALUES
                                (@PrimeiroNome,
                                @NomeMeio,
                                @Sobrenome,
                                @NomeMae,
                                @DataNascimento,
                                @Genero,
                                @DocumentoTipoID,
                                " + Criptografar(entidade.DocumentoNumero) + @",
                                @DocumentoOrgaoEmissor,
                                @DocumentoEstadoID,
                                @DocumentoDataExpedicao,
                                " + Criptografar(entidade.Endereco) + @",
                                " + Criptografar(entidade.EnderecoComplemento) + @",
                                @EnderecoPaisID,
                                @EnderecoCidadeID,
                                @EnderecoCidadeEstadoNaoBR,
                                @EnderecoCEP,
                                @EnderecoBairro,
                                @EnderecoPais,
                                " + Criptografar(entidade.TelefoneResidencial) + @",
                                " + Criptografar(entidade.TelefoneComercial) + @",
                                " + Criptografar(entidade.Email) + @",
                                @CidadeParticipanteID,
                                @Condutor,
                                @HistoriaTitulo,
                                @HistoriaCategoriaID,
                                @HistoriaTexto,
                                @HistoriaArquivoNome,
                                @UsuarioID,
                                @DataModificacao,
                                @DataCadastro,
                                @Inativo,
                                @IndicadoInternoCategoriaID,
                                @RemoverGaleria) ";

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
        public void Atualizar(IndicadoInterno entidade)
        {
            string SQL = @"UPDATE IndicadoInterno SET  
                            PrimeiroNome=@PrimeiroNome,
                                NomeMeio=@NomeMeio,
                                Sobrenome=@Sobrenome,
                                NomeMae=@NomeMae,
                                DataNascimento=@DataNascimento,
                                Genero=@Genero,
                                DocumentoTipoID=@DocumentoTipoID,
                                DocumentoNumeroCrt=" + Criptografar(entidade.DocumentoNumero) + @",
                                DocumentoOrgaoEmissor=@DocumentoOrgaoEmissor,
                                DocumentoEstadoID=@DocumentoEstadoID,
                                DocumentoDataExpedicao=@DocumentoDataExpedicao,
                                EnderecoCrt=" + Criptografar(entidade.Endereco) + @",
                                EnderecoComplementoCrt=" + Criptografar(entidade.EnderecoComplemento) + @",
                                EnderecoPaisID=@EnderecoPaisID,
                                EnderecoCidadeID=@EnderecoCidadeID,
                                EnderecoCidadeEstadoNaoBR=@EnderecoCidadeEstadoNaoBR,
                                EnderecoCEP=@EnderecoCEP,
                                EnderecoBairro=@EnderecoBairro,
                                EnderecoPais=@EnderecoPais,
                                TelefoneResidencialCrt=" + Criptografar(entidade.TelefoneResidencial) + @", 
                                TelefoneComercialCrt=" + Criptografar(entidade.TelefoneComercial) + @",
                                EmailCrt=" + Criptografar(entidade.Email) + @",
                                CidadeParticipanteID=@CidadeParticipanteID,
                                Condutor=@Condutor,
                                HistoriaTitulo=@HistoriaTitulo,
                                HistoriaCategoriaID=@HistoriaCategoriaID,
                                HistoriaTexto=@HistoriaTexto,
                                HistoriaArquivoNome=@HistoriaArquivoNome,
                                UsuarioID=@UsuarioID,
                                DataModificacao=@DataModificacao,
                                Inativo=@Inativo,
                                IndicadoInternoCategoriaID=@IndicadoInternoCategoriaID,
                                RemoverGaleria=@RemoverGaleria
                        WHERE ID=@ID ";

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
        public IndicadoInterno Carregar(int id)
        {
            IndicadoInterno entidadeRetorno = null;

            string SQL = @"SELECT I.ID,
                                I.PrimeiroNome,
                                I.NomeMeio,
                                I.Sobrenome,
                                I.NomeMae,
                                I.DataNascimento,
                                I.Genero,
                                I.DocumentoTipoID,
                                " + Decriptografar("I.DocumentoNumeroCrt") + @" DocumentoNumero,
                                I.DocumentoOrgaoEmissor,
                                I.DocumentoEstadoID,
                                I.DocumentoDataExpedicao,
                                " + Decriptografar("I.EnderecoCrt") + @" Endereco,
                                " + Decriptografar("I.EnderecoComplementoCrt") + @" EnderecoComplemento,
                                I.EnderecoPaisID,
                                I.EnderecoCidadeID,
                                I.EnderecoCidadeEstadoNaoBR,
                                I.EnderecoCEP,
                                I.EnderecoBairro,
                                I.EnderecoPais,
                                " + Decriptografar("I.TelefoneResidencialCrt") + @" TelefoneResidencial, 
                                " + Decriptografar("I.TelefoneComercialCrt") + @" TelefoneComercial,
                                " + Decriptografar("I.EmailCrt") + @" Email, 
                                I.CidadeParticipanteID,
                                I.Condutor,
                                I.HistoriaTitulo,
                                I.HistoriaCategoriaID,
                                I.HistoriaTexto,
                                I.HistoriaArquivoNome,
                                I.UsuarioID,
                                I.DataModificacao,
                                I.Inativo,
                                i.IndicadoInternoCategoriaID,
                                i.RemoverGaleria,
                                C.ID, C.Nome, 
                                E.ID, E.UF, E.Nome, E.PaisID,
                                HC.ID, HC.Nome, HC.CorFont, HC.CorLabel,
                                CP.ID, CP.CidadeID,
                                CPC.ID, CPC.Nome, 
                                CPE.ID, CPE.UF, CPE.Nome, CPE.PaisID
							FROM IndicadoInterno (NOLOCK) I
                            INNER JOIN CidadeParticipante (NOLOCK) CP ON I.CidadeParticipanteID = CP.ID
                            INNER JOIN Cidade (NOLOCK) CPC ON CP.CidadeID = CPC.ID
                            INNER JOIN Estado (NOLOCK) CPE ON CPC.EstadoID = CPE.ID
                            INNER JOIN IndicadoInternoCategoria (nolock) IIC ON i.IndicadoInternoCategoriaID = IIC.ID
                            LEFT JOIN Cidade (NOLOCK) C ON I.EnderecoCidadeID = C.ID
                            LEFT JOIN Estado (NOLOCK) E ON C.EstadoID = E.ID
                            LEFT JOIN HistoriaCategoria (NOLOCK) HC ON I.HistoriaCategoriaID = HC.ID                            
							WHERE I.ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidadeRetorno = con.Query<IndicadoInterno, Cidade, Estado, HistoriaCategoria, CidadeParticipante, Cidade, Estado, IndicadoInterno>(SQL,
                    (indicadoInterno, cidade, estado, categoria, cp, cpcidade, cpestado) =>
                    {
                        indicadoInterno.HistoriaCategoria = categoria;
                        indicadoInterno.Cidade = cidade;
                        if (cidade != null)
                            indicadoInterno.Cidade.Estado = estado;
                        indicadoInterno.CidadeParticipante = cp;
                        indicadoInterno.CidadeParticipante.Cidade = cpcidade;
                        indicadoInterno.CidadeParticipante.Cidade.Estado = cpestado;
                        return indicadoInterno;
                    },
                    new { ID = id })
                    .FirstOrDefault();
                con.Close();
            }
            return entidadeRetorno;
        }

        
        /// <summary> 
        /// Método que retorna todas as entidades. 
        /// SELECT * FROM IndicadoInterno ORDER BY ID DESC 
        /// </summary> 
        public IList<IndicadoInterno> Listar(Int32 skip, Int32 take, string palavraChave, int cidadeParticipanteID, string documentoNumero, int condutor)
        {
            IList<IndicadoInterno> entidadeRetorno = null;

            string strWhere = @" ";

            if (cidadeParticipanteID != 0)
                strWhere += " AND CidadeParticipanteID = @cidadeParticipanteID";

            if (documentoNumero != "")
                strWhere += " AND " + Decriptografar("DocumentoNumeroCrt") + " = @DocumentoNumero";

            if (condutor != 0)
                strWhere += " AND I.Condutor = @Condutor";

            string SQL = @"SELECT 
                        I.ID,
                        I.PrimeiroNome, I.NomeMeio, I.SobreNome, 
                        " + Decriptografar("I.EmailCrt") + @" Email, I.Condutor, i.RemoverGaleria,
                        C.ID, C.Nome, E.ID, E.UF
                    FROM IndicadoInterno I, CidadeParticipante CP, Cidade C, Estado E
                    WHERE I.CidadeParticipanteID = CP.ID
                    AND I.Inativo = 0
                    AND CP.CidadeID = C.ID
                    AND C.EstadoID = E.ID
                    AND (I.PrimeiroNome like @palavraChave
                         OR I.NomeMeio like @palavraChave
                         OR I.Sobrenome like @palavraChave) " + 
                    strWhere + @"
                   ORDER BY I.Condutor DESC, E.UF, C.Nome, I.PrimeiroNome
                  OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";

            string SQLCount = @"SELECT COUNT(1) 
                                  FROM IndicadoInterno I, CidadeParticipante CP, Cidade C, Estado E
                                  WHERE I.CidadeParticipanteID = CP.ID
                                  AND I.Inativo = 0
                                  AND CP.CidadeID = C.ID
                                  AND C.EstadoID = E.ID
                                  AND (I.PrimeiroNome like @palavraChave
                                       OR I.NomeMeio like @palavraChave
                                       OR I.Sobrenome like @palavraChave) " +
                                  strWhere ;


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount,
                    new
                    {
                        palavraChave = '%' + palavraChave + '%',
                        DocumentoNumero = documentoNumero,
                        CidadeParticipanteID = cidadeParticipanteID,
                        Condutor = (condutor == 1)
                    }).FirstOrDefault();

                entidadeRetorno = con.Query<IndicadoInterno, Cidade, Estado, IndicadoInterno>(SQL,
                    (indicadoInterno, cidade, estado) =>
                        {
                            indicadoInterno.Cidade = cidade;
                            indicadoInterno.Cidade.Estado = estado;
                            return indicadoInterno;
                        },
                    new
                    {
                        Skip = skip,
                        Take = take,
                        palavraChave = '%' + palavraChave + '%',
                        DocumentoNumero = documentoNumero,
                        CidadeParticipanteID = cidadeParticipanteID,
                        Condutor = (condutor == 1)
                    }).ToList();
                con.Close();
            }

            return entidadeRetorno;
        }


        public IList<IndicadoInterno> ListarCidadeParticipante(int cidadeParticipanteID, bool diferente = false, bool condutor = false)
        {
            string SQL = @"SELECT I.ID, I.PrimeiroNome, I.NomeMeio, I.Sobrenome, I.Condutor, 
                                  I.CidadeParticipanteID, I.EnderecoCidadeID, i.RemoverGaleria,
                                  C.ID, C.Nome,
                                  E.ID, E.Nome, E.UF,
                                  CP.ID,
                                  (SELECT COUNT(*) FROM Indicado (NOLOCK) WHERE CidadeParticipanteID = cp.ID  " + (condutor ? "AND Condutor = 1" : "") + @") as QuantidadeIndicados,
                                  (SELECT COUNT(*) FROM IndicadoInterno (NOLOCK) WHERE CidadeParticipanteID = cp.ID AND Inativo = 0 " + (condutor ? "AND Condutor = 1" : "") + @") as QuantidadeIndicadosInterno,
                                  CCP.ID, CCP.Nome, 
                                  ECP.ID, ECP.Nome, ECP.UF
                           FROM IndicadoInterno (NOLOCK) I
                           LEFT JOIN Cidade (NOLOCK) C ON I.EnderecoCidadeID = C.ID 
                           LEFT JOIN Estado (NOLOCK) E ON C.EstadoID = E.ID
                           INNER JOIN CidadeParticipante (NOLOCK) CP ON I.CidadeParticipanteID = CP.ID
                           INNER JOIN Cidade (NOLOCK) CCP ON CP.CidadeID = CCP.ID
                           INNER JOIN Estado (NOLOCK) ECP ON CCP.EstadoID  = ECP.ID
                           WHERE I.CidadeParticipanteID " + (diferente ? "<>" : "=") + @" @CidadeParticipanteID
                           ORDER BY CCP.Nome, I.PrimeiroNome";

            IList<IndicadoInterno> list = new List<IndicadoInterno>();

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<IndicadoInterno, Cidade, Estado, CidadeParticipante, Cidade, Estado, IndicadoInterno>(SQL,
                    (indicado, cidade, estado, cidadeParticipante, cidadeParticipanteCidade, cidadeParticipanteEstado) =>
                    {
                        if (cidade != null)
                        {
                            indicado.Cidade = cidade;
                            indicado.Cidade.Estado = estado;
                        }

                        indicado.CidadeParticipante = cidadeParticipante;
                        indicado.CidadeParticipante.Cidade = cidadeParticipanteCidade;
                        indicado.CidadeParticipante.Cidade.Estado = cidadeParticipanteEstado;
                        return indicado;
                    },
                new
                {
                    CidadeParticipanteID = cidadeParticipanteID
                }).ToList();
                TotalRegistros = list.Count;

                con.Close();
            }
            return list;
        }

        public void Realocar(int indicadoID, int cidadeParticipanteID, bool condutor)
        {
            string SQL = @"UPDATE IndicadoInterno SET 
                   CidadeParticipanteID=@CidadeParticipanteID,
                   Condutor=@Condutor
                   WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new
                {
                    CidadeParticipanteID = cidadeParticipanteID,
                    Condutor = condutor,
                    ID = indicadoID
                });
                con.Close();
            }
        }
        public void SalvarRemoverGaleria(IndicadoInterno entidade)
        {
            string SQL = @"UPDATE IndicadoInterno SET  
                    UsuarioID = @usuarioID
                    ,DataModificacao = @data
                    ,RemoverGaleria = @remover
                    WHERE ID = @indicadoID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new
                {
                    indicadoID = entidade.ID,
                    remover = entidade.RemoverGaleria,
                    usuarioID = entidade.UsuarioID,
                    data = DateTime.Now
                });
                con.Close();
            }
        }

    }
}