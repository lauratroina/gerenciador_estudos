using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
namespace BradescoNext.Lib.DAL
{
    public interface IModeracaoRespostaDAL
    {
        Int32 TotalRegistros { get; set; }
        ModeracaoResposta Carregar(int id);
        IList<ModeracaoResposta> Listar(int moderacaoPerguntaID);
    }
}
