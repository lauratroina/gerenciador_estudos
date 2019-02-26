using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.DAL
{
    public interface IFaleConoscoDestinoDAL
    {
        Int32 TotalRegistros { get; set; }
        void Inserir(FaleConoscoDestino entidade);
        void Atualizar(FaleConoscoDestino entidade);
        FaleConoscoDestino Carregar(int id);
        IList<FaleConoscoDestino> Listar();
        IList<FaleConoscoDestino> Listar(Int32 skip, Int32 take);
    }
}
