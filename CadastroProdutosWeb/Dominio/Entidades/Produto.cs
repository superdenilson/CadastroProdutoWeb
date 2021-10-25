using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutosWeb.Dominio.Entidades
{
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string TipoProduto { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal Valor { get; set; }
    }
}
