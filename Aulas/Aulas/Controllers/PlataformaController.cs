using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aulas.Models;

namespace Aulas.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PlataformaController
    {
        private Menu _menuState;
        private ArtigoController _artigoController;
        
        public PlataformaController()
        {
            _artigoController = new ArtigoController();
        }

        public void InicializarPlataformaController()
        {
            while (_menuState != Menu.Sair)
            {
                Console.Clear();
                Console.WriteLine("Escolha uma opcao");
                Console.WriteLine(((int)Menu.InserirArtigo) + " Para inserir artigos");
                Console.WriteLine(((int)Menu.EliminarArtigo) + " Para eliminar artigos");
                Console.WriteLine(((int)Menu.ListarArtigos) + " Para listar artigos");
                Console.WriteLine(((int)Menu.Sair)+" Para saír");
                string opcao = Console.ReadLine();

                if (!Menu.TryParse(opcao, out _menuState))
                {
                    _menuState = Menu.Invalido;
                }
                #region Menu
                switch (_menuState)
                {
                    case Menu.InserirArtigo:
                        Console.WriteLine("Insira o nome do artigo");
                        string nomeArtigoInserir = Console.ReadLine();
                        Console.WriteLine("Insira o preço do artigo");
                        float precoArtigoInserir;
                        string valorArtigoInserir;
                        do
                        {
                            valorArtigoInserir = Console.ReadLine();
                        } while (!float.TryParse(
                            valorArtigoInserir, 
                            NumberStyles.Any, 
                            new CultureInfo("PT-pt"),
                            out precoArtigoInserir));
                        Artigo artigoInserir = new Artigo(nomeArtigoInserir, precoArtigoInserir, _artigoController.GetArtigos().Count);
                        _artigoController.InserirArtigo(artigoInserir);
                        break;
                    case Menu.EliminarArtigo:
                        Console.WriteLine("Introduza um nome ou id do artigo a eliminar ");
                        string nome = Console.ReadLine();
                        int id;
                        if (int.TryParse(nome, out id))
                        {
                            if (_artigoController.RemoverArtigo(id))
                            {
                                Console.WriteLine("Removeu o artigo com sucesso");
                            }
                            else
                            {
                                Console.WriteLine("Artigo não encontrado");
                            }
                        }
                        else
                        {
                            if (_artigoController.RemoverArtigo(nome))
                            {
                                Console.WriteLine("Removeu o artigo com sucesso");
                            }
                            else
                            {
                                Console.WriteLine("Artigo não encontrado");
                            }
                        }
                        break;
                    case Menu.ListarArtigos:
                        foreach (Artigo artigo in _artigoController.GetArtigos())
                        {
                            Console.WriteLine(
                                string.Concat(
                                    artigo.Identificador,
                                    "     ", 
                                    artigo.Nome, 
                                    "     ", 
                                    artigo.Preco));
                        }
                        break;
                        case Menu.Sair:
                        break;
                    case Menu.Invalido:
                    default:
                        Console.WriteLine("Opcao invalida");
                        break;
                }
                #endregion

                Console.ReadLine();
            }
            
        }
    }
}
