using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity.Enumerator;

namespace BradescoNext.Lib.Models
{
    public class ReportIndicadoPublicoModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CidadeNome { get; set; }
        public string EstadoUF { get; set; }
        public string CidadePartNome { get; set; }
        public string EstadoPartUF { get; set; }
        public string DocumentoTipo { get; set; }
        public string DocumentoNumero { get; set; }
        public DateTime DataNascimentoDB { get; set; }
        public string DataNascimento
        {
            get
            {
                return DataNascimentoDB.ToString("dd/MM/yyyy");
            }
        }
        public bool CondutorDB { get; set; }
        public string Condutor
        {
            get
            {
                if (CondutorDB)
                    return "Sim";
                return "Não";
            }
        }
        public string Galeria
        {
            get
            {
                if (RemoverGaleriaDB)
                {
                    return "Sim";
                }
                return "Não";
            }
        }
        public string AlteracaoOrigemDB { get; set; }
        public string AlteracaoOrigem
        {
            get
            {
                try
                {
                    return EnumExtensions.FromChar<enumAlteracaoOrigem>(AlteracaoOrigemDB).Description();
                }
                catch
                {
                    return enumAlteracaoOrigem.Indicacao.Description();
                }
            }

        }
        public bool RemoverGaleriaDB { get; set; }
        public string CidadeParticipante
        {
            get
            {
                return EstadoPartUF + " - " + CidadePartNome;
            }
        }
        public string Cidade
        {
            get
            {
                return EstadoUF + " - " + CidadeNome;
            }
        }
        public string Usuario { get; set; }
        public string ResponsavelNome { get; set; }
        public string ResponsavelTelefone { get; set; }
        public string ResponsavelEmail { get; set; }
        public DateTime DataModificacaoDB { get; set; }
        public string DataModificacao
        {
            get
            {
                return DataModificacaoDB.ToString("dd/MM/yyyy");
            }
        }
        public string NotaGaleria { get; set; }
        public string Nota { get; set; }
        public DateTime DataCadastroDB { get; set; }
        public string DataCadastro
        {
            get
            {
                return DataCadastroDB.ToString("dd/MM/yyyy");
            }
        }
        public string Genero { get; set; }
        public string Email { get; set; }
    }
}
