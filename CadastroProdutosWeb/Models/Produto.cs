using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroProdutosWeb.Models
{
    public class Produto
    {
        [Required]
        public Guid CodigoProduto { get; set; }
        
        [Required(ErrorMessage = "Favor informar a descrição do produto", AllowEmptyStrings =false)]
        public string DescricaoProduto { get; set; }
        
        [Required(ErrorMessage = "Favor informar o tipo do produto", AllowEmptyStrings = false)]
        public string TipoProduto { get; set; }
        
        [Required(ErrorMessage = "Favor informar a data de lançamento do produto", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataLancamento { get; set; }
        
        [Required(ErrorMessage = "Favor informar o valor do produto", AllowEmptyStrings = false)]
        public decimal Valor { get; set; }
    }

    public enum TipoProduto
    {
        Celular = 1,
        Tablet = 2,
        Notebook = 3
    }
}
