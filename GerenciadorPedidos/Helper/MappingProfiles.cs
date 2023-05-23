using AutoMapper;
using GerenciadorPedidos.Context;
using GerenciadorPedidos.Models;
using GerenciadorPedidos.Dto;

namespace GerenciadorPedidos.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pedido, PedidoDto>();
            CreateMap<Fornecedor, FornecedorDto>();
            CreateMap<Produto, ProdutoDto>();

            CreateMap<PedidoDto, Pedido>();
            CreateMap<FornecedorDto, Fornecedor>();
            CreateMap<ProdutoDto, Produto>();
        }
    }
}
