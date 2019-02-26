using System;
using System.Text;
using System.Collections.Generic;
using BradescoNext.Lib.Models;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class CidadeParticipante 
    {
        private int _ID;
        private int _CidadeID;
        private int _QuantidadeLugares;
        private bool _ExibeMapa;
        private DateTime _DataModificacao;
        private int _UsuarioID;
        private bool _Inativo;
        private int _QuantidadeLugaresInterno;
        private bool _RealocacaoPendente;
        private bool _RealocacaoPendenteInterno;
        private int _QuantidadeIndicados;
        private int _QuantidadeIndicadosInterno;
        private Cidade _Cidade;
        private int _SlotsOcupados;
        private int _Menor18;
        private int _Entre18_35;
        private int _Entre35_60;
        private int _Maior60;
        private int _Homens;
        private int _Mulheres;
        private int _SlotsOcupadosInterno;
        private decimal _NotaMedia;
        private int _NaoCondutores;

        public decimal NotaMedia
        {
            get { return _NotaMedia; }
            set { _NotaMedia = value; }
        }
        public int NaoCondutores
        {
            get { return _NaoCondutores; }
            set { _NaoCondutores = value; }
        }
        public int SlotsOcupados
        {
            get { return _SlotsOcupados; }
            set { _SlotsOcupados = value; }
        }
        public int SlotsOcupadosInterno
        {
            get { return _SlotsOcupadosInterno; }
            set { _SlotsOcupadosInterno = value; }
        }
        public int Mulheres
        {
            get { return _Mulheres; }
            set { _Mulheres = value; }
        }
        public int Homens
        {
            get { return _Homens; }
            set { _Homens = value; }
        }
        public int Menor18
        {
            get { return _Menor18; }
            set { _Menor18 = value; }
        }
        public int Entre18_35
        {
            get { return _Entre18_35; }
            set { _Entre18_35 = value; }
        }
        public int Entre35_60
        {
            get { return _Entre35_60; }
            set { _Entre35_60 = value; }
        }
        public int Maior60
        {
            get { return _Maior60; }
            set { _Maior60 = value; }
        }


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        
        public int CidadeID
        {
            get { return _CidadeID; }
            set { _CidadeID = value; }
        }
      
        public int QuantidadeLugares
        {
            get { return _QuantidadeLugares; }
            set { _QuantidadeLugares = value; }
        }

        public bool ExibeMapa
        {
            get { return _ExibeMapa; }
            set { _ExibeMapa = value; }
        }

        public DateTime DataModificacao
        {
            get { return _DataModificacao; }
            set { _DataModificacao = value; }
        }

        public int UsuarioID
        {
            get { return _UsuarioID; }
            set { _UsuarioID = value; }
        }

        public bool Inativo
        {
            get { return _Inativo; }
            set { _Inativo = value; }
        }

        public bool Ativo
        {
            get { return !_Inativo; }
            set { _Inativo = !value; }
        }

        public int QuantidadeLugaresInterno
        {
            get { return _QuantidadeLugaresInterno; }
            set { _QuantidadeLugaresInterno = value; }
        }

        public bool RealocacaoPendente
        {
            get { return _RealocacaoPendente; }
            set { _RealocacaoPendente = value; }
        }

        public bool RealocacaoPendenteInterno
        {
            get { return _RealocacaoPendenteInterno; }
            set { _RealocacaoPendenteInterno = value; }
        }

        public int QuantidadeTotal
        {
            get { return _QuantidadeLugares + QuantidadeLugaresInterno; }
        }

        public int QuantidadeIndicados
        {
            get { return _QuantidadeIndicados; }
            set { _QuantidadeIndicados= value; }
        }

        public int QuantidadeIndicadosInterno
        {
            get { return _QuantidadeIndicadosInterno; }
            set { _QuantidadeIndicadosInterno = value; }
        }

        public Cidade Cidade
        {
            get { return _Cidade; }
            set { _Cidade = value; }
        }
        
    }
}