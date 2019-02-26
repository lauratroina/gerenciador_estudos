﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Identity;
using BradescoNext.Lib.Models; 
namespace BradescoNext.Lib.Service 
{ 
    public class HistoriaRecusaMotivoService 
    {
        private IHistoriaRecusaMotivoDAL dal = new HistoriaRecusaMotivoADO();

        public void Incluir(int historiaID, string listaRecusaMotivoID)
        {
            dal.Incluir(historiaID, listaRecusaMotivoID);
        }

        public IList<HistoriaRecusaMotivo> Listar(int historiaID)
        {
            return dal.Listar(historiaID);
        }
    } 
} 