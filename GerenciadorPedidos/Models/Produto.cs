using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorPedidos.Models;
using GerenciadorPedidos.Models;

public class Produto
{
    public int Id {get; set;}
    public string Descricao { get; set; }
    public DateTime DataCadastro { get; set; }
    public decimal Preco { get; set; }

    public virtual  ICollection<PedidoProduto> PedidosProdutos { get; set; }
}