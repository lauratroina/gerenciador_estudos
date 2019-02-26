using System;
using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Models;
namespace BradescoNext.Lib.DAL
{
    public interface IEditorialDAL
    {
        Int32 TotalRegistros { get; set; }

        int Inserir(Editorial entidade);

        void Atualizar(Editorial entidade);
        
        void Inativar(Editorial entidade);

        Editorial Carregar(int id);
        Editorial Carregar(string url);

        IList<Editorial> Listar();

        IList<Editorial> Listar(Int32 skip, Int32 take, string palavraChave);

        void AtualizarCache(Editorial entidade);

        int ContarPublicados();

        Editorial CarregarUltimaPublicada(DateTime data, int id = 0);

        IList<Editorial> ListarPublicados();

        bool UrlLivre(int id, string url);

        IList<string> getListUrls();
    }
}
