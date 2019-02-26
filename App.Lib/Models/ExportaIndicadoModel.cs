using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Models
{
    public class ExportaIndicadoModel
    {
        public string Interno { get; set; }
        public string EstadoParticipante { get; set; }
        public string CidadeParticipante { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string DocumentoTipo { get; set; }
        public string DocumentoNumero { get; set; }
        public string DocumentoNumeroComplemento { get; set; }
        public DateTime DataNascimento { get; set; }
        public string ResponsavelNome { get; set; }
        public string ResponsavelTelefone { get; set; }
        public string ResponsavelEmail { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string EnderecoCidadeEstadoNaoBR { get; set; }
        public string TelefoneResidencial { get; set; }
        public string TelefoneComercial { get; set; }
        public string Email { get; set; }

        public string HistoriaTitulo { get; set; }
        public string HistoriaNomeacao { get; set; }
        public string HistoriaCategoria { get; set; }
        public string NotaHistoria { get; set; }
        public string PrimeiroNome
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Nome))
                    return "";
                else
                {
                    string[] retorno = Nome.Split(' ');

                    return retorno[0];
                }
            }
        }
        public string SobreNome
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Nome))
                    return "";
                else
                {
                    string[] retorno = Nome.Split(' ');

                    return retorno[retorno.Length - 1];
                }
            }
        }
        public string RespPrimeiroNome
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResponsavelNome))
                    return "";
                else
                {
                    string[] retorno = ResponsavelNome.Split(' ');

                    return retorno[0];
                }
            }
        }
        public string RespSobreNome
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResponsavelNome))
                    return "";
                else
                {
                    string[] retorno = ResponsavelNome.Split(' ');

                    return retorno[retorno.Length - 1];
                }
            }
        }

        public string IndicadoInternoCategoria { get; set; }
    }
}
