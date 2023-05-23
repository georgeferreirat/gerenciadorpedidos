using GerenciadorPedidos.Dto;
using GerenciadorPedidos.Models;

namespace GerenciadorPedidos.Interfaces
{
    public interface IProdutoRepository
    {
        ICollection<Produto> getALL();
        Produto get(int id);
        bool Create(Produto produto);
        bool Update(Produto produto);
        bool Delete(int id);
        bool Save();

        ICollection<Produto> getProduto(int produtoId);
        
        bool Exists(int Id);
    }
}
