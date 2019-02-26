using System;
using System.Text;
using System.Collections.Generic;
using BradescoNext.Lib.Entity.Enumerator;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class HistoriaMidia
    {
        private int _ID;
        private int _HistoriaID;
        private string _ArquivoNome;
        private DateTime _DataCadastro;
        private bool _Inativo;
        private string _AdicionadoOrigem;
        private string _InativoOrigem;
        private int? _UsuarioID;
        private string _ArquivoTipo;
        private Usuario _Usuario;



        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }


        public int HistoriaID
        {
            set { _HistoriaID = value; }
            get { return _HistoriaID; }
        }

        public string ArquivoNome
        {
            set { _ArquivoNome = value; }
            get { return _ArquivoNome; }
        }


        public DateTime DataCadastro
        {
            set { _DataCadastro = value; }
            get { return _DataCadastro; }
        }


        public bool Inativo
        {
            set { _Inativo = value; }
            get { return _Inativo; }
        }

        public bool Ativo
        {
            set { _Inativo = !value; }
            get { return !_Inativo; }
        }


        public string AdicionadoOrigemDB
        {
            set { _AdicionadoOrigem = value; }
            get { return _AdicionadoOrigem; }
        }
        public enumAlteracaoOrigem AdicionadoOrigem
        {
            set { _AdicionadoOrigem = value.ValueAsString(); }
            get { try { return EnumExtensions.FromChar<enumAlteracaoOrigem>(_AdicionadoOrigem); } catch { return enumAlteracaoOrigem.Indicacao; } }
        }
        public enumAlteracaoOrigem InativoOrigem
        {
            set { _InativoOrigem = value.ValueAsString(); }
            get { try { return EnumExtensions.FromChar<enumAlteracaoOrigem>(_InativoOrigem); } catch { return enumAlteracaoOrigem.Indicacao; } }
        }
        public string InativoOrigemDB
        {
            set { _InativoOrigem = value; }
            get { return _InativoOrigem; }
        }


        public int? UsuarioID
        {
            set { _UsuarioID = value; }
            get { return _UsuarioID; }
        }

        public enumArquivoTipo ArquivoTipo
        {
            set { _ArquivoTipo = value.ValueAsString(); }
            get { try { return EnumExtensions.FromChar<enumArquivoTipo>(_ArquivoTipo); } catch { return enumArquivoTipo.Imagem; } }
        }
        public string ArquivoTipoDB
        {
            set { _ArquivoTipo = value; }
            get { return _ArquivoTipo; }
        }
        public Usuario Usuario
        {
            set { _Usuario = value; }
            get { return _Usuario; }
        }


    }
}