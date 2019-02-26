using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;
namespace BradescoNext.Lib.DAL
{
    public interface ICidadeParticipanteDAL
    {
        Int32 TotalRegistros { get; set; }
        void Inserir(CidadeParticipante entidade);
        void InserirLog(int id);
        void Atualizar(CidadeParticipante entidade);
        CidadeParticipante Carregar(int id, bool condutor = false);
        CidadeParticipante CarregarPorCidade(int cidadeID, bool retornarQuantidades, bool condutor = false);
        IList<CidadeParticipante> Listar();
        IList<CidadeParticipante> ListarAtivas();
        IList<CidadeParticipante> Listar(Int32 skip, Int32 take, string palavraChave, bool realocacaoPendente, bool realocacaoPendenteInterno);
        CidadeRealocacaoPendente QuantidadeRealocacaoPendente();
        bool CidadeParticipanteCadastrada(int id, int cidadeID);
        IList<CidadeParticipante> ListarDecisaoPublica(Int32 skip, Int32 take, string palavraChave, int NotaMin);
        IList<CidadeParticipante> ListarDecisaoInterna(Int32 skip, Int32 take, string palavraChave);
        IList<CidadeParticipante> ListarNumerosConsolidados(Int32 skip, Int32 take, string palavraChave);
        InfoSlotsDecisaoModel CarregarTotaisVisaoPublica();
        InfoSlotsDecisaoModel CarregarTotaisVisaoInterna();
        InfoSlotsDecisaoModel CarregarTotaisVisaoConsolidada();
    }
}
