using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Models;
namespace BradescoNext.Lib.DAL
{
    public interface IHistoriaDAL
    {
        Int32 TotalRegistros { get; set; }
        int Inserir(Historia entidade);
        void InserirLog(int id);
        void Atualizar(Historia entidade);
        void AtualizarTriagemModeracao(Historia entidade, enumPerfilNome perfil, enumAprovacao operacao = enumAprovacao.semNecessidade);
        Historia Carregar(int id);
        Historia Carregar(string codigo);
        Historia CarregarTriagemModeracao(int id);
        bool TriagemBloqueada(int id, enumPerfilNome perfil);
        void BloquearTriagem(int id, int usuarioID, enumPerfilNome perfil);
        Historia CarregarUltimaHistoria(int indicadoID);
        int CarregaIDUltimaHistoriaModerada(int? indicadoID);
        IList<Historia> ListarAprovadasTriagemNaoModeradas(int ultimaHistoriaModeradaID, int indicadoID);
        IList<Historia> Listar(int indicadoID);
        IList<ExportaHistoriaModel> ListarExcel();
        IList<HistoriaVizualizaModel> ListarHistoriasComMidias(int indicadoID, bool mostraTodos);
        IList<Historia> Listar(Int32 skip, Int32 take, int indicadoID);
        int CheckCodigo(string codigoIndicado);
        Historia CarregaPenultimaHistoria(int ultimaHistoriaID);
        void AtualizarTriagemAbuso(Historia entidade);
        void RemoverIndicadorFoto(int id);
        void AtualizarAguardandoAprovacao(Historia entidade);
        void AtualizarGaleria(int ID);
        void AtualizarConfirmacaoResponsavel(Historia entidade);
        void AdicionarAbuso(int historiaId);
        IList<Historia> CarregaHistoriasAguardandoResponsavel(DateTime dataAtual, int roboEmailQtdDiasEnvioAguardandoResp, int roboEmailQtdEnvioAguardandoResp);
        IList<Historia> CarregaHistoriasIndicado(int indicadoID);
        IList<Historia> CarregaHistoriasAguardandoIndicado(DateTime dataAtual, int roboEmailQtdDiasEnvioAguardando, int roboEmailQtdEnvioAguardando);
        IList<Historia> CarregaHistoriasAguardandoGaleria();
        IList<Historia> Listar(Int32 skip, Int32 take, int usuarioID, enumPerfilNome perfil, string palavraChave);
        IList<HistoriaReportModel> ListarHistoriasReportadas(int indicadoID);
        OverviewModel CarregarDadosOverview();
        
    }
}
