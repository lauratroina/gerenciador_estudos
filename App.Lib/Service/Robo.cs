using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Lib.DAL;
using App.Lib.DAL.ADO;
using App.Lib.Entity;
using App.Lib.Enumerator;
using App.Lib.Models;
using App.Lib.Util;

namespace App.Lib.Service
{
    public class Robo
    {
        private DateTime _dataStartServico = System.DateTime.Now;
        private readonly CancellationTokenSource _serviceCancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _serviceCancellationToken;
        private Task _syncEmailResponsavelPendente;
        
        //decimal balance;        
        private Object thisLockLog = new Object();
        private Object thisLockError = new Object();


        public Robo()
        {
            //TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        public void Start()
        {


            _serviceCancellationToken = _serviceCancellationTokenSource.Token;
            //LogServiceInfo("Iniciando Serviço");

            LaunchIndicadoAguardandoResponsavel();
        }




        /// <summary>
        /// Dispara a task.
        /// </summary>
        private void LaunchIndicadoAguardandoResponsavel()
        {
            if (_syncEmailResponsavelPendente != null)
            {
                _syncEmailResponsavelPendente.Dispose();
            }
            //_syncEmailResponsavelPendente = new Task(EnvioEmail, null, _serviceCancellationToken, TaskCreationOptions.LongRunning);
            //_syncEmailResponsavelPendente.Start();

            //SyncEmailAlertaStream(null);
        }

        /// <summary>
        /// Task responsável por enviar emails de alerta do stream
        /// </summary>
        /// <param name="objectState"></param>
        
        private void ExecutaEnvio()
        {
            //int contador = 0;
            DateTime dataAtual = System.DateTime.Now;

            LogUtil.Debug(string.Format("{0}-{1}", DateTime.Now, "Iniciou Envio de e-mails de Streem !"));
            
        }


    }
}

