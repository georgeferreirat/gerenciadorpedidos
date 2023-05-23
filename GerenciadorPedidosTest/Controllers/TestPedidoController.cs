using AutoMapper;
using FluentAssertions;
using GerenciadorPedidos;
using GerenciadorPedidos.Context;
using GerenciadorPedidos.Controllers;
using GerenciadorPedidos.Dto;
using GerenciadorPedidos.Helper;
using GerenciadorPedidos.Interfaces;
using GerenciadorPedidos.Models;
using GerenciadorPedidos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidosTest.Controllers
{
    public class TestPedidoController : DbTest
    {
        private PedidoRepository _pedidoRepository;
        private ProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public TestPedidoController()
        {

            _mapper = new Mapper(new MapperConfiguration(cfg =>
                 cfg.AddProfile(new MappingProfiles())));

            _pedidoRepository = new PedidoRepository(context, _mapper);
            _produtoRepository = new ProdutoRepository(context);
        }


        [Fact]
        public void TestgetALL_Return200()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.getALL();

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<PedidoDto>>();
        }

        [Fact]
        public void Testget_Return200()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.get(1);

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<Pedido>();
        }

        [Fact]
        public void Testget_Return400()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.get(0);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestCreate_Return200()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.Create(new PedidoDto()
            {
                DataCadastro = DateTime.Now,
                Descricao = "Descrição de Teste",
                Fornecedor = new FornecedorDto(),
                FornecedorId = 0,
                Id = 0,
                Preco = 1,
                Produtos = new List<ProdutoDto>(),
                QtdProduto = 0
            });

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<string>();
        }

        [Fact]
        public void TestCreate_Return400()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.Create(new PedidoDto()
            {
                DataCadastro = DateTime.Now,
                Descricao = "Descrição de Teste",
                Fornecedor = new FornecedorDto(),
                FornecedorId = 0,
                Id = 1,
                Preco = 1,
                Produtos = new List<ProdutoDto>(),
                QtdProduto = 0
            });

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestUpdate_Return204()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.Update(1, new PedidoDto()
            {
                DataCadastro = DateTime.Now,
                Descricao = "Descrição de Teste",
                Fornecedor = new FornecedorDto(),
                FornecedorId = 0,
                Id = 1,
                Preco = 1,
                Produtos = new List<ProdutoDto>(),
                QtdProduto = 0
            });

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void TestUpdate_Return404()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.Update(0, new PedidoDto()
            {
                DataCadastro = DateTime.Now,
                Descricao = "Descrição de Teste",
                Fornecedor = new FornecedorDto(),
                FornecedorId = 0,
                Id = 0,
                Preco = 1,
                Produtos = new List<ProdutoDto>(),
                QtdProduto = 0
            });

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestDelete_Return204()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.Delete(1);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void TestDelete_Return404()
        {

            var controller = new PedidoController(_pedidoRepository, _produtoRepository, _mapper);

            var result = controller.Delete(0);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
