using System;
using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Models;
namespace BradescoNext.Lib.DAL
{
    public interface IEditorialBlocoArquivoDAL
    {
        Int32 TotalRegistros { get; set; }

        int Inserir(EditorialBlocoArquivo entidade);

        void Atualizar(EditorialBlocoArquivo entidade);

        void Inativar(EditorialBlocoArquivo entidade);

        EditorialBlocoArquivo Carregar(int id);

        IList<EditorialBlocoArquivo> Listar();

        IList<EditorialBlocoArquivo> Listar(Int32 skip, Int32 take);

        IList<EditorialBlocoArquivo> ListarPorBlocoID(int blocoID, bool apenasAtivos);
        void Excluir(int id);
    }
}
