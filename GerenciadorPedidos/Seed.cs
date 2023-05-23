using GerenciadorPedidos.Context;
using GerenciadorPedidos.Models;

namespace GerenciadorPedidos
{
    public class Seed
    {
        private readonly Context.Context Context;
        public Seed(Context.Context context)
        {
            this.Context = context;
        }
        public void SeedContext()
        {
            if (!Context.PedidosProdutos.Any())
            {
                var pedidosProdutos = new List<PedidoProduto>()
                {
                    new PedidoProduto()
                    {
                        Pedido = new Pedido()
                        {
                            Descricao = "Escritório",
                            DataCadastro = new DateTime(1903,1,1),
                            QtdProduto = 2,
                            Preco = 10,
                            Fornecedor = new Fornecedor()
                            {
                                Nome = "Teste",
                                RazaoSocial = "Razão de Teste",
                                Email = "contato@teste.com",
                                UF = "CE",
                                CNPJ = "07.010.198/0001-33"
                            },
                            PedidosProdutos = new List<PedidoProduto>()
                            {
                                new PedidoProduto { 
                                    Produto = new Produto()
                                        {
                                           Descricao = "PC",
                                            DataCadastro = new DateTime(1903,1,1),
                                            Preco = 2000000
                                        }
                                }
                            }
                        },
                        Produto = new Produto()
                        {
                            Descricao = "Computador",
                            DataCadastro = new DateTime(1903,1,1),
                            Preco = 5000000
                        }
                    },
                };
                Context.PedidosProdutos.AddRange(pedidosProdutos);
                Context.SaveChanges();
            }

            if (!Context.Fornecedores.Any())
            {
                var fornecedor = new Fornecedor()
                {
                    CNPJ = "12345",
                    Email = "email@teste.com.br",
                    Id = 0,
                    Nome = "Nome Teste",
                    RazaoSocial = "Razão Teste",
                    UF = "CE",
                    Pedidos = new List<Pedido>()
                };
                Context.Fornecedores.Add(fornecedor);
                Context.SaveChanges();
            }
        }
    }
}
