using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using CadastroProdutosWeb.Models;
using System.Text;

namespace CadastroProdutosWeb.Controllers
{
    public class ProdutoController : Controller
    {
        public async Task<IActionResult> Index()
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44356/api/");
            HttpResponseMessage httpResponse = await hc.GetAsync($"produtoCommand/GetAll");
            httpResponse.EnsureSuccessStatusCode();
             
            List<Produto> produtosModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Produto>>(httpResponse.Content.ReadAsStringAsync().Result);

            return View(produtosModel);
        }

        public IActionResult Create()
        {
            Produto produto = new Produto { CodigoProduto = Guid.NewGuid()};

            return View(produto);
        }

        public async Task<IActionResult> SalvarProdutoAsync(Produto produto)
        {

            produto.TipoProduto = ObterTipoProduto(produto.TipoProduto);
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44356/api/produtoCommand/AddProduto");

            var jsonRequest = JsonConvert.SerializeObject(produto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await hc.PostAsync(hc.BaseAddress.ToString(),content);
            postResponse.EnsureSuccessStatusCode();

            string jSonResult = postResponse.Content.ReadAsStringAsync().Result;

           return RedirectToAction("Index");
        }

        public async Task<IActionResult> AlterarProdutoAsync(Produto produto)
        {

            produto.TipoProduto = ObterTipoProduto(produto.TipoProduto);
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44356/api/produtoCommand/UpdateProduto");

            var jsonRequest = JsonConvert.SerializeObject(produto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await hc.PutAsync(hc.BaseAddress.ToString(), content);
            postResponse.EnsureSuccessStatusCode();

            string jSonResult = postResponse.Content.ReadAsStringAsync().Result;

            return RedirectToAction("Index");
        }

        private string ObterTipoProduto(string tipoProduto)
        {
            switch (tipoProduto){
                case "1": return "Celular"; 
                case "2": return "Tablet";
                case "3": return "Notebook";
                default: return "Não especificado";
            }
        }

        public async Task<IActionResult>Edit(Guid? id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(String.Format("https://localhost:44356/api/produtoCommand/GetById/{0}", id));
            HttpResponseMessage httpResponse = await hc.GetAsync(hc.BaseAddress.ToString());
            httpResponse.EnsureSuccessStatusCode();

            string jSonResult = httpResponse.Content.ReadAsStringAsync().Result;

            List<Produto> produtoModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Produto>>(httpResponse.Content.ReadAsStringAsync().Result);

            return View(produtoModel.FirstOrDefault());
        }

        public async Task<IActionResult> Delete(Guid? id)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(String.Format("https://localhost:44356/api/produtoCommand/GetById/{0}", id));
            HttpResponseMessage httpResponse = await hc.GetAsync(hc.BaseAddress.ToString());
            httpResponse.EnsureSuccessStatusCode();

            string jSonResult = httpResponse.Content.ReadAsStringAsync().Result;

            List<Produto> produtoModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Produto>>(httpResponse.Content.ReadAsStringAsync().Result);

            return View(produtoModel.FirstOrDefault());
        }

        [HttpPost(""), ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(String.Format("https://localhost:44356/api/produtoCommand/DeleteProduto/{0}", id));
            HttpResponseMessage httpResponse = await hc.DeleteAsync(hc.BaseAddress.ToString());
            httpResponse.EnsureSuccessStatusCode();

            string jSonResult = httpResponse.Content.ReadAsStringAsync().Result;

            // List<Produto> produtoModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Produto>>(httpResponse.Content.ReadAsStringAsync().Result);

            return RedirectToAction("Index");
        }
    }
}
