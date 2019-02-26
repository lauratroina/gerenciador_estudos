using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{
    public interface IHistoriaModeracaoDAL 
    {
        int Inserir(HistoriaModeracao entidade);
        void Atualizar(HistoriaModeracao entidade);
        int BuscaHistoriaEmAnalise(int historiaID, int usuarioID);
        int QtdeModeracaoRealizada(int historiaID, int usuarioID);
        int QtdeModeracaoRealizada(int historiaID);
        bool ModeracaoRealizada(int historiaID, int usuarioID);
    } 
} 
