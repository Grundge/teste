﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aulas.Models;
using Newtonsoft.Json;

namespace Aulas.Controllers
{
    public class ArtigoController
    {
        private List<Artigo> _artigoList;

        public ArtigoController(string listaArtigos = "")
        {
            if (string.IsNullOrEmpty(listaArtigos) || string.IsNullOrWhiteSpace(listaArtigos))
            {
                _artigoList = new List<Artigo>();
            }
            else
            {
                _artigoList = JsonConvert.DeserializeObject<List<Artigo>>(listaArtigos);
            }
            
        }

        public void InserirArtigo(string nomeArtigoInserir, float precoArtigoInserir, int aritgoControllerCont)
        {
            Artigo artigoInserir = new Artigo(nomeArtigoInserir, precoArtigoInserir, aritgoControllerCont);
            _artigoList.Add(artigoInserir);
        }

        /// <summary>
        /// Remove um artigo da lista pelo nome do artigo
        /// </summary>
        /// <param name="nomeArtigo">Nome do artigo</param>
        /// <returns>Devolve true se encontrar e remover o artigo</returns>
        public bool RemoverArtigo(string nomeArtigo)
        {

            /*Artigo art = _artigoList.FirstOrDefault(a => string.Equals(a.Nome, nomeArtigo));
            _artigoList.Remove(art);*/

            Artigo artigoRemover = null;
            foreach (Artigo artigo in _artigoList)
            {
                if (artigo.Nome == nomeArtigo)
                {
                    artigoRemover = artigo;
                    break;
                }
            }
            if (artigoRemover != null)
            {
                _artigoList.Remove(artigoRemover);
                return true;
            }
            return false;

        }

        /// <summary>
        /// Remove um artigo da lista pelo id do artigo
        /// </summary>
        /// <param name="idArtigo">Id do artigo</param>
        /// <returns>Devolve true se encontrar e remover artigo</returns>
        public bool RemoverArtigo(int idArtigo)
        {
            Artigo artigoRemover = null;
            foreach (Artigo artigo in _artigoList)
            {
                if (artigo.Identificador == idArtigo)
                {
                    artigoRemover = artigo;
                    break;
                }
            }
            if (artigoRemover != null)
            {
                _artigoList.Remove(artigoRemover);
                return true;
            }
            return false;
        }

        public Artigo GetArtigo(string nome)
        {
            return _artigoList.FirstOrDefault(a => a.Nome == nome);
        }

        public Artigo GetArtigo(int id)
        {
            //return _artigoList.FirstOrDefault(artigo => artigo.Identificador == id);
            foreach (Artigo artigo in _artigoList)
            {
                if (artigo.Identificador == id)
                {
                    return artigo;
                }
            }
            return null;
        }

        public List<string> GetArtigos()
        {
            List<string> artigoStringList = new List<string>();
            foreach (Artigo artigo in _artigoList)
            {
                artigoStringList.Add(
                    string.Concat(
                        artigo.Identificador,
                        "     ",
                        artigo.Nome,
                        "     ",
                        artigo.Preco));
            }
            return artigoStringList;
        }

        public List<Artigo> GetArtigosList()
        {
            return _artigoList;
        }

        public void LimparLista()
        {
            _artigoList.Clear();
        }
    }
}
