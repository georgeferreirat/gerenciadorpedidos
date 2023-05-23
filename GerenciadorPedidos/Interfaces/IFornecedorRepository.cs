using GerenciadorPedidos.Dto;
using GerenciadorPedidos.Models;

namespace GerenciadorPedidos.Interfaces
{
    public interface IFornecedorRepository
    {
        ICollection<Fornecedor> getALL();
        Fornecedor get(int id);
        bool Create(Fornecedor fornecedor);
        bool Update(Fornecedor fornecedor);
        bool Delete(int pedidoId);
        bool Save();

        bool Exists(int Id);
    }
}
