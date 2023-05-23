using GerenciadorPedidos.Context;
using GerenciadorPedidos.Dto;
using GerenciadorPedidos.Interfaces;
using GerenciadorPedidos.Models;

namespace GerenciadorPedidos.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly Context.Context _context;

        public FornecedorRepository(Context.Context context)
        {
            _context = context;
        }

        public ICollection<Fornecedor> getALL()
        {
            return _context.Fornecedores.OrderBy(f => f.Id).ToList();
        }

        public Fornecedor get(int Id)
        {   

            var fornecedor = _context.Fornecedores.Find(Id);
           _context
                .Entry(fornecedor)
                .Collection("Pedidos")
                .Load();

            return fornecedor;
        }
        
        public bool Create(Fornecedor fornecedor)
        {
            _context.Add(fornecedor);
            return Save();
        }
        public bool Update(Fornecedor fornecedor)
        {
           _context.Update(fornecedor);
            return Save();
        }
        public bool Delete(int Id)
        {   
            var fornecedor = _context.Fornecedores.Find(Id);
            _context.Remove(fornecedor);
            return Save();
        }
        public bool Save()
        {
            try { var saved = _context.SaveChanges(); return true; }
            catch { return  false; }
        }

        public bool Exists(int Id)
        {
            return _context.Fornecedores.Any(p => p.Id == Id);
        }
    }
}