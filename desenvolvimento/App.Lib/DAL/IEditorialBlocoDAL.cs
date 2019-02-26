using System;
using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Models;
namespace BradescoNext.Lib.DAL
{
    public interface IEditorialBlocoDAL
    {
        Int32 TotalRegistros { get; set; }

        int Inserir(EditorialBloco entidade);

        void Atualizar(EditorialBloco entidade);
        
        void Inativar(EditorialBloco entidade);

        EditorialBloco Carregar(int id);

        IList<EditorialBloco> Listar();
        IList<EditorialBloco> Listar(int editorialID, bool apenasAtivos, Int32 skip, Int32 take, string palavraChave);

        IList<EditorialBloco> Listar(Int32 skip, Int32 take);
    }
}
