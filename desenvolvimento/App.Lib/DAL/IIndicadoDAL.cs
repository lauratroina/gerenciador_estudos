using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Enumerator;
namespace BradescoNext.Lib.DAL
{
    public interface IIndicadoDAL
    {
        Int32 TotalRegistros { get; set; }
        int Inserir(Indicado entidade);
        void InserirLog(int id);
        void Atualizar(Indicado entidade);
        void AtualizarIndicadoModeracao(int indicadoID, int usuarioID);
        void AtualizarConfirmarIndicacao(Indicado entidade, bool menor);
        void AtualizarConfirmarIndicacaoResponsavel(Indicado entidade);
        Indicado Carregar(int id);
        Indicado Carregar(string documentoNumero, int documentoTipoID);
        int CarregaIDUltimaHistoriaCadastrada(int indicadoID);
        int CarregaIDUltimaHistoriaModerada(int indicadoID);
        IList<ReportIndicadoPublicoModel> ListarExcel();
        IList<Indicado> Listar(Int32 skip, Int32 take, bool mostraTodos, string palavraChave, int cidadeParticipanteID, string documentoNumero, int condutor, decimal nota);
        void AtualizarDataQuantidadeAguardandoAprovacao(Indicado entidade);
        void AtualizaIDUltimaHistoriaModeracao(int indicadoID, int historiaID);
        bool PossuiHistoriaPendente(int indicadoID);
        IList<Indicado> ListarCidadeParticipante(int cidadeParticipanteID, bool diferente = false, bool condutor = false);
        void Realocar(int indicadoID, int cidadeParticipanteID, bool condutor);
        IList<Indicado> ListarIndicadosComReports(Int32 skip, Int32 take, string palavraChave);
        void AtualizarIndicadoReportadoAbuso(Indicado entidade);
        int QtdeHistorias(int indicadoID);
        int QtdeHistoriasAprovadas(int indicadoID);
        int QtdeHistoriasNaoAprovadas(int indicadoID);
        int QtdeHistoriasModeradas(int indicadoID);
        int QtdeReportsAbuso(int indicadoID);
        bool EstaNaGaleria(int indicadoID, int historiaID);
        void SalvarCondutor(Indicado entidade);
        void SalvarRemoverGaleria(Indicado entidade);
        void AtualizarCidadeParticipante(Indicado entidade);
        void RemoverGaleriaAbuso(int indicadoID);
        List<Indicado> BuscarGaleria(Int32 skip, Int32 take, string busca = "", int nota = 0, enumTipoBuscaIndicado tipo = enumTipoBuscaIndicado.tudo);
        Indicado CarregarGaleria(int id);
        IList<ExportaIndicadoModel> ExportaIndicados();

    }
}

