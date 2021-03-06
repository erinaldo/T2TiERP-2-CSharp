using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FolhaClient.ServicoFolhaReference;
using SearchWindow;
using FolhaClient.Model;
using FolhaClient.Command;

namespace FolhaClient.ViewModel.Folha
{
	/// 
	/// The MIT License
	///
	/// Copyright: Copyright (C) 2010 T2Ti.COM
	///
	/// Permission is hereby granted, free of charge, to any person
	/// obtaining a copy of this software and associated documentation
	/// files (the "Software"), to deal in the Software without
	/// restriction, including without limitation the rights to use,
	/// copy, modify, merge, publish, distribute, sublicense, and/or sell
	/// copies of the Software, and to permit persons to whom the
	/// Software is furnished to do so, subject to the following
	/// conditions:
	///
	/// The above copyright notice and this permission notice shall be
	/// included in all copies or substantial portions of the Software.
	///
	/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	/// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
	/// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
	/// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
	/// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
	/// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
	/// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
	/// OTHER DEALINGS IN THE SOFTWARE.
	///
	///        The author may be contacted at:
	///            t2ti.com@gmail.com
	///
	/// Autor: Albert Eije (t2ti.com@gmail.com)
	/// Version: 1.0
    public class FolhaLancamentoCabecalhoViewModel : ERPViewModelBase
    {
        public ObservableCollection<FolhaLancamentoCabecalhoDTO> ListaFolhaLancamentoCabecalho { get; set; }
        private FolhaLancamentoCabecalhoDTO _FolhaLancamentoCabecalhoSelected;
        public FolhaLancamentoDetalheDTO FolhaLancamentoDetalheSelected { get; set; }
        private int primeiroResultado;
        protected ICommand seguinteCommand;
        protected ICommand anteriorCommand;
        private bool _isEditar { get; set; }


        public FolhaLancamentoCabecalhoViewModel()
        {
            try
            {
                ListaFolhaLancamentoCabecalho = new ObservableCollection<FolhaLancamentoCabecalhoDTO>();
                primeiroResultado = 0;
                this.atualizarListaFolhaLancamentoCabecalho(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarColaborador()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(ColaboradorDTO),
                    typeof(ServicoFolha));

                if (searchWindow.ShowDialog() == true)
                {
                    FolhaLancamentoCabecalhoSelected.Colaborador = (ColaboradorDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("FolhaLancamentoCabecalhoSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void pesquisarFolhaEvento()
        {
            try
            {
                SearchWindowApp searchWindow = new SearchWindowApp(typeof(FolhaEventoDTO),
                    typeof(ServicoFolha));

                if (searchWindow.ShowDialog() == true)
                {
                    FolhaLancamentoDetalheSelected.FolhaEvento = (FolhaEventoDTO)searchWindow.itemSelecionado;
                    notifyPropertyChanged("FolhaLancamentoDetalheSelected");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FolhaLancamentoCabecalhoDTO FolhaLancamentoCabecalhoSelected
        {
            get { return _FolhaLancamentoCabecalhoSelected; }
            set
            {
                _FolhaLancamentoCabecalhoSelected = value;
                notifyPropertyChanged("FolhaLancamentoCabecalhoSelected");
            }
        }

        public ICommand paginaSeguinteCommand
        {
            get
            {
                if (seguinteCommand == null)
                {
                    seguinteCommand = new RelayCommand
                    (
                        param =>
                        {
                            this.atualizarListaFolhaLancamentoCabecalho(1);
                        },
                        param =>
                        {
                            return true;
                        }
                    );
                }
                return seguinteCommand;
            }
        }

        public ICommand paginaAnteriorCommand
        {
            get
            {
                if (anteriorCommand == null)
                {
                    anteriorCommand = new RelayCommand
                    (
                        param =>
                        {
                            this.atualizarListaFolhaLancamentoCabecalho(-1);
                        },
                        param =>
                        {
                            return true;
                        }
                    );
                }
                return anteriorCommand;
            }
        }

        public void salvarAtualizarFolhaLancamentoCabecalho()
        {
            try
            {
                using (ServicoFolhaClient serv = new ServicoFolhaClient())
                {
                    serv.salvarAtualizarFolhaLancamentoCabecalho(FolhaLancamentoCabecalhoSelected);
                    FolhaLancamentoCabecalhoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void atualizarListaFolhaLancamentoCabecalho(int pagina)
        {
            try
            {
                using (ServicoFolhaClient serv = new ServicoFolhaClient())
                {
                    if (pagina == 0)
                        primeiroResultado = 0;
                    else if (pagina > 0)
                        primeiroResultado += QUANTIDADE_PAGINA;
                    else if (pagina < 0)
                        primeiroResultado -= QUANTIDADE_PAGINA;

                    List<FolhaLancamentoCabecalhoDTO> listaServ = serv.selectFolhaLancamentoCabecalhoPagina(primeiroResultado, QUANTIDADE_PAGINA, new FolhaLancamentoCabecalhoDTO());

                    ListaFolhaLancamentoCabecalho.Clear();

                    foreach (FolhaLancamentoCabecalhoDTO objAdd in listaServ)
                    {
                        ListaFolhaLancamentoCabecalho.Add(objAdd);
                    }
                    FolhaLancamentoCabecalhoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void excluirFolhaLancamentoCabecalho()
        {
            try
            {
                using (ServicoFolhaClient serv = new ServicoFolhaClient())
                {
                    serv.deleteFolhaLancamentoCabecalho(FolhaLancamentoCabecalhoSelected);
                    FolhaLancamentoCabecalhoSelected = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsEditar
        {
            get { return _isEditar; }
            set
            {
                _isEditar = value;
                notifyPropertyChanged("IsEditar");
                notifyPropertyChanged("IsListar");
            }
        }

        public bool IsListar
        {
            get { return !_isEditar; }
            set
            {
                _isEditar = !value;
                notifyPropertyChanged("IsEditar");
                notifyPropertyChanged("IsListar");
            }
        }
    }
}
