using AutoMapper;
using GerenciadorPedidos.Context;
using GerenciadorPedidos.Dto;
using GerenciadorPedidos.Interfaces;
using GerenciadorPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly Context.Context _context;
        private readonly IMapper _mapper;

        public PedidoRepository(Context.Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<Pedido> getALL()
        {
           var pedidos = _context.Pedidos
           .Include("Fornecedor")
           .OrderBy(p => p.Id).ToList();
                
            return pedidos;
        }

        public PedidoDto get(int Id)
        {   

            var pedidosN = _context.Pedidos
            .Where(p => p.Id == Id)
            .Include("Fornecedor")
            .Include("PedidosProdutos.Produto")
            .FirstOrDefault();

            // converte produtos
            List<Produto> produtosN = new List<Produto>();
            foreach(PedidoProduto p in pedidosN.PedidosProdutos)
            {
               produtosN.Add(p.Produto);
            }
            var produtos =  _mapper.Map<List<ProdutoDto>>(produtosN);


            var pedidoMap = _mapper.Map<PedidoDto>(pedidosN);
            pedidoMap.Produtos = produtos;
            return pedidoMap;
        }

        public bool Create(int fornecedorId, List<Produto> produtos, Pedido newPedido)
        {
            newPedido.FornecedorId = fornecedorId;
            _context.Add(newPedido);
            _context.SaveChanges();
            
            foreach(Produto p in produtos)
            {
                var pedidosProdutos = new PedidoProduto()
                {
                    
                    ProdutoId = p.Id,
                    PedidoId = newPedido.Id,
                };

                _context.Add(pedidosProdutos);
            }

            return Save();
        }

        public bool Update(Pedido pedido)
        {
            _context.Update(pedido);
            return Save();
        }

        public bool Delete(int pedidoId)
        {
            var pedidos = _context.Pedidos.Find(pedidoId);
            _context.Remove(pedidos);
            
            var pedidosProdutos = _context.PedidosProdutos.Where(a => a.PedidoId == pedidoId).ToList();
            _context.RemoveRange(pedidosProdutos);

            return Save();
        }

        public bool Save()
        {
            try { var saved = _context.SaveChanges(); return true; }
            catch { return false; }
        }

        public bool Exists(int Id)
        {
            return _context.Pedidos.Any(p => p.Id == Id);
        }

        public bool AddProduto(Produto produto, Pedido pedido)
        {
            return false;
        }

        public bool RemoveProduto(Produto produto, Pedido pedido)
        {
            return false;
        }
    }
}