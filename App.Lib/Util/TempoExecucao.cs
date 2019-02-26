using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using App.Lib.Util.Models;

namespace App.Lib.Util
{
    public class TempoExecucao
    {
        private const string MensagemInicio = "INI";
        private static readonly bool EnableTempoExecucao;
        private readonly List<TempoExecucaoModel> _TempoExecucao;
        private int _GeralMs;
        private Stopwatch _TimerTrace;
        private string _UtilmaMensagem;

        static TempoExecucao()
        {
            EnableTempoExecucao = ConfigurationManager.AppSettings["EnableTempoExecucao"] != null;
        }

        public TempoExecucao()
        {
            if (!EnableTempoExecucao)
                return;
            _UtilmaMensagem = MensagemInicio;
            _TimerTrace = Stopwatch.StartNew();
            _TempoExecucao = new List<TempoExecucaoModel>();
        }

        public void trace(string mensagem)
        {
            if (!EnableTempoExecucao)
                return;
            _TimerTrace.Stop();
            var item = new TempoExecucaoModel();
            item.DiferencaMs = _TimerTrace.Elapsed.Milliseconds;
            _GeralMs += item.DiferencaMs;
            item.DiferencaInicioMs = _GeralMs;
            item.Inicio = _UtilmaMensagem;
            item.Fim = mensagem;
            _UtilmaMensagem = mensagem;
            _TempoExecucao.Add(item);
            _TimerTrace = Stopwatch.StartNew();
        }

        public List<string> result(string mensagemFim)
        {
            if (!EnableTempoExecucao)
                return null;
            trace(mensagemFim);
            if (_TempoExecucao.Count > 0)
                return _TempoExecucao.Select(t => "Entre [" + t.Inicio + "] e [" + t.Fim + "] = " + t.DiferencaMs + "ms (" + Math.Round(Convert.ToDecimal(t.DiferencaMs)/_GeralMs*100, 2) + "%) | " + t.DiferencaInicioMs + "ms").ToList();
            return null;
        }
    }
}