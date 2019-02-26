using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
using BradescoNext.Lib.Models; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface IIndicadoInternoDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(IndicadoInterno entidade);
         void InserirLog(int id);
         void Atualizar(IndicadoInterno entidade); 
         IndicadoInterno Carregar(int id); 
         IList<IndicadoInterno> Listar(Int32 skip, Int32 take, string palavraChave, int cidadeParticipante, string documentoNumero, int condutor);
         IList<IndicadoInterno> ListarCidadeParticipante(int cidadeParticipanteID, bool diferente = false, bool condutor = false);
         void Realocar(int indicadoID, int cidadeParticipanteID, bool condutor);
         void SalvarRemoverGaleria(IndicadoInterno entidade);
    } 
} 
