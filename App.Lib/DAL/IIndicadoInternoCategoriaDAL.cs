using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
namespace BradescoNext.Lib.DAL
{
    public interface IIndicadoInternoCategoriaDAL
    {
        IList<IndicadoInternoCategoria> Listar();

        IndicadoInternoCategoria Carregar(int id);
    }
}
