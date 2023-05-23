using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using GerenciadorPedidos.Dto;
using GerenciadorPedidos.Interfaces;
using GerenciadorPedidos.Models;

namespace GerenciadorPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoPedidoController : ControllerBase
    {   
        private readonly IProdutoRepository _produtosRepository;
        private readonly IMapper _mapper;

        public ProdutoPedidoController(
            IProdutoRepository produtosRepository,
            IMapper mapper)
        {
            _produtosRepository = produtosRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PedidoProdutoDto>))]
        public IActionResult getALL(){

            var produtos = _mapper.Map<ICollection<PedidoProdutoDto>>(_produtosRepository.getALL());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(produtos);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(PedidoProdutoDto))]
        [ProducesResponseType(400)]
        public IActionResult get(int Id)
        {
            if (!_produtosRepository.Exists(Id))
                return NotFound();
            var produto = _produtosRepository.get(Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(produto);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] PedidoProdutoDto produtoCreate)
        {

            if (produtoCreate == null)
                return BadRequest(ModelState);

            var produtoMap = _mapper.Map<Produto>(produtoCreate);

            if (!_produtosRepository.Create(produtoMap))
            {
                ModelState.AddModelError("", "Ocorreu um erro ao salvar!");
                return BadRequest(ModelState);
            }

            return Ok("Criado com sucesso");
        }


        [HttpPut("{produtoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int produtoId, [FromBody] PedidoProdutoDto produtoUpdate)
        {

            if (!_produtosRepository.Exists(produtoId))
                return NotFound();

            var produtoMap = _mapper.Map<Produto>(produtoUpdate);

            produtoMap.Id = produtoId;

            if (!_produtosRepository.Update(produtoMap))
            {
                ModelState.AddModelError("", "Ocorreu um erro ao salvar!");
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{produtoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int produtoId)
        {
            if (!_produtosRepository.Exists(produtoId))
            {
                return NotFound();
            }

            if (!_produtosRepository.Delete(produtoId))
            {
                ModelState.AddModelError("", "Ocorreu um erro ao salvar!");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}