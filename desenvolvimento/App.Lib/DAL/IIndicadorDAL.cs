using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.DAL
{
    public interface IIndicadorDAL
    {
        Int32 TotalRegistros { get; set; }
        Indicador Carregar(string email);
        void Inserir(Indicador entidade);
        void Atualizar(Indicador entidade);
        IList<Indicador> ListarPendentesRoboContinuacao(DateTime dataAtual, int horasEmailContinuacao, int quantidadeEmailContinuacao);
        void AtualizarContinuacao(Indicador indicador);
    }
}
