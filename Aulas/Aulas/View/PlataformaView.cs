using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aulas.Controllers;
using Aulas.Models;
using Newtonsoft.Json;


namespace Aulas.View
{

    public class PlataformaView
    {
        private Menu _menuState;
        private ArtigoController _artigoController;
        private string _serializedDataModel;



        public PlataformaView()
        {

            if (System.IO.File.Exists("backup.txt"))
            {
                string backup = System.IO.File.ReadAllText("backup.txt");
                _artigoController = new ArtigoController(backup);
            }
            else
            {
                _artigoController = new ArtigoController();
            }
        }

        public void InicializarPlataformaView()
        {
            while (_menuState != Menu.Sair)
            {
                Console.Clear();
                Console.WriteLine("Escolha uma opcao");
                Console.WriteLine(((int)Menu.InserirArtigo) + " Para inserir artigos");
                Console.WriteLine(((int)Menu.EliminarArtigo) + " Para eliminar artigos");
                Console.WriteLine(((int)Menu.ListarArtigos) + " Para listar artigos");
                Console.WriteLine(((int)Menu.Guardar) + " Para guardar artigos");
                Console.WriteLine(((int)Menu.Sair) + " Para saír");
                string opcao = Console.ReadLine();

                if (!Menu.TryParse(opcao, out _menuState))
                {
                    _menuState = Menu.Invalido;
                }
                #region Menu
                switch (_menuState)
                {
                    case Menu.InserirArtigo:
                        OpcaoInserirArtigo();
                        break;
                    case Menu.EliminarArtigo:
                        OpcaoEliminarArtigo();
                        break;
                    case Menu.ListarArtigos:
                        OpcaoListArtigos();
                        break;
                    case Menu.Sair:
                        break;
                    case Menu.Invalido:
                    default:
                        OpcaoInvalidoDefault();
                        break;
                }
                #endregion

                Console.ReadLine();
            }

        }

        private void OpcaoInserirArtigo()
        {
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
            
            _artigoController.InserirArtigo(nomeArtigoInserir, precoArtigoInserir, _artigoController.GetArtigos().Count);
            OpcaoGurdar();
        }

        private void OpcaoEliminarArtigo()
        {
            Console.WriteLine("Introduza um nome ou id do artigo a eliminar ");
            string nome = Console.ReadLine();
            int id;
            if (int.TryParse(nome, out id))
            {
                if (_artigoController.RemoverArtigo(id))
                {
                    Console.WriteLine("Removeu o artigo com sucesso");
                    OpcaoGurdar();
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
                    OpcaoGurdar();
                }
                else
                {
                    Console.WriteLine("Artigo não encontrado");
                }
            }
        }

        private void OpcaoListArtigos()
        {
            foreach (string artigo in _artigoController.GetArtigos())
            {
                Console.WriteLine(artigo);
            }
        }

        private void OpcaoInvalidoDefault()
        {
            Console.WriteLine("Opcao invalida");
        }

        private void OpcaoGurdar()
        {
            _serializedDataModel = JsonConvert.SerializeObject(_artigoController.GetArtigosList());
            System.IO.File.WriteAllText("backup.txt",_serializedDataModel);
        }

    }
}
