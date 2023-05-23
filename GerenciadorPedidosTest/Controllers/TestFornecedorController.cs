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
    public class TestFornecedorController : DbTest
    {
        private FornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public TestFornecedorController()
        {

            _fornecedorRepository = new FornecedorRepository(context);

            _mapper = new Mapper(new MapperConfiguration(cfg =>
                 cfg.AddProfile(new MappingProfiles())));
        }


        [Fact]
        public void TestgetALL_Return200()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.getALL();

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<FornecedorDto>>();
        }

        [Fact]
        public void Testget_Return200()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.get(1);

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<Fornecedor>();
        }

        [Fact]
        public void Testget_Return400()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.get(0);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestCreate_Return200()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.Create(new FornecedorDto()
            {
                CNPJ = "12345",
                Email = "email@teste.com.br",
                Id = 0,
                Nome = "Nome Teste",
                RazaoSocial = "Razão Teste",
                UF = "CE",
                Pedidos = new List<PedidoDto>()
            });

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<string>();
        }

        [Fact]
        public void TestCreate_Return400()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.Create(new FornecedorDto()
            {
                CNPJ = "12345",
                Email = "email@teste.com.br",
                Id = 1,
                Nome = "Nome Teste",
                RazaoSocial = "Razão Teste",
                UF = "CE",
                Pedidos = new List<PedidoDto>()
            });

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestUpdate_Return204()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.Update(1, new FornecedorDto()
            {
                CNPJ = "54321",
                Email = "teste@email.com.br",
                Id = 1,
                Nome = "Teste Nome",
                RazaoSocial = "Teste Razão",
                UF = "UF",
                Pedidos = new List<PedidoDto>()
            });

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void TestUpdate_Return404()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.Update(0, new FornecedorDto()
            {
                CNPJ = "54321",
                Email = "teste@email.com.br",
                Id = 0,
                Nome = "Teste Nome",
                RazaoSocial = "Teste Razão",
                UF = "UF",
                Pedidos = new List<PedidoDto>()
            });

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void TestDelete_Return204()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.Delete(1);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void TestDelete_Return404()
        {

            var controller = new FornecedorController(_fornecedorRepository, _mapper);

            var result = controller.Delete(0);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
