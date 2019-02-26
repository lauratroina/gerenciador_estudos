using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
namespace BradescoNext.Lib.DAL
{
    public interface IModeracaoPerguntaDAL
    {
        Int32 TotalRegistros { get; set; }
        ModeracaoPergunta Carregar(int id);
        IList<ModeracaoPergunta> Listar();
    }
}
