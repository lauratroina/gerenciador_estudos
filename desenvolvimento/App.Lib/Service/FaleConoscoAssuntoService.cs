using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Lib.Service
{
    public class FaleConoscoAssuntoService
    {
        private IFaleConoscoAssuntoDAL dal = new FaleConoscoAssuntoADO();

        /// <summary>
        /// Método que retorna uma lista de assuntos
        /// </summary>
        /// <returns></returns>
        public IList<FaleConoscoAssunto> Listar()
        {
            return dal.Listar();
        }

        /// <summary>
        /// Método que carrega um assunto 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comDestinos"></param>
        /// <returns></returns>
        public FaleConoscoAssunto Carregar(int id, bool comDestinos = true)
        {
            if (comDestinos)
            {
                return dal.CarregarComDestinos(id);
            }
            else
            {
                return dal.Carregar(id);
            }
            
        }
    }
}
