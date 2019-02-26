
using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Models; 
namespace BradescoNext.Lib.Service 
{ 
    public class CidadeParticipanteService 
    { 

		private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private ICidadeParticipanteDAL dal = new CidadeParticipanteADO(); 

        public void Salvar(CidadeParticipante entidade) 
        {
            if (entidade.ID > 0)
            {
                dal.InserirLog(entidade.ID);
                dal.Atualizar(entidade);
            }
            else
            {
                dal.Inserir(entidade); 
            }
        }

        /// <summary>
        /// Lista Cidades Participantes
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="palavraChave"></param>
        /// <param name="realocacaoPendente"></param>
        /// <param name="realocacaoPendenteInterno"></param>
        /// <returns></returns>
        public IList<CidadeParticipante> Listar(Int32 skip, Int32 take, string palavraChave, bool realocacaoPendente, bool realocacaoPendenteInterno)
        {
            IList<CidadeParticipante> lista = dal.Listar(skip, take, palavraChave, realocacaoPendente, realocacaoPendenteInterno);
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }
        public IList<CidadeParticipante> ListarAtivas()
        {
            IList<CidadeParticipante> lista = dal.ListarAtivas();
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }

        public IList<CidadeParticipante> Listar()
        {
            return dal.Listar();
        }


        public CidadeParticipante Carregar(int id, bool condutor = false)
        {
            return dal.Carregar(id, condutor);
        }


        public CidadeRealocacaoPendente QuantidadeRealocacaoPendente()
        {
            return dal.QuantidadeRealocacaoPendente();
        }

        public CidadeParticipante CarregarPorCidade(int cidadeID, bool retornarQuantidades = false, bool condutor = false)
        {
            return dal.CarregarPorCidade(cidadeID, retornarQuantidades, condutor);
        }

        public bool CidadeParticipanteCadastrada(int id, int cidadeID)
        {
            return dal.CidadeParticipanteCadastrada(id, cidadeID);
        }

        public IList<CidadeParticipante> ListarDecisaoPublica(int skip, int take, string palavraChave, int notaMin)
        {
            IList<CidadeParticipante> retorno = dal.ListarDecisaoPublica(skip, take, palavraChave, notaMin);
            TotalRegistros = dal.TotalRegistros;
            return retorno;
        }
        public IList<CidadeParticipante> ListarDecisaoInterna(int skip, int take, string palavraChave)
        {
            IList<CidadeParticipante> retorno = dal.ListarDecisaoInterna(skip, take, palavraChave);
            TotalRegistros = dal.TotalRegistros;
            return retorno;
        }
        public IList<CidadeParticipante> ListarNumerosConsolidados(int skip, int take, string palavraChave)
        {
            IList<CidadeParticipante> retorno = dal.ListarNumerosConsolidados(skip, take, palavraChave);
            TotalRegistros = dal.TotalRegistros;
            return retorno;
        }
        public InfoSlotsDecisaoModel CarregarNumerosTotaisDecisaoPublica()
        {
            return dal.CarregarTotaisVisaoPublica();
        }
        public InfoSlotsDecisaoModel CarregarNumerosTotaisDecisaoInterna()
        {
            return dal.CarregarTotaisVisaoInterna();
        }
        public InfoSlotsDecisaoModel CarregarNumerosTotaisDecisaoConsolidada()
        {
            return dal.CarregarTotaisVisaoConsolidada();
        }
    } 
} 
