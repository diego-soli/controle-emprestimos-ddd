using EmprestimosLivros.Application.DTOs;
using EmprestimosLivros.Application.Interfaces;
using EmprestimosLivros.Application.Services;
using EmprestimosLivros.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    [Authorize]
    public class LivroController : Controller
    {
        private readonly ILivroService _livroService;
      //  private readonly IUsuarioService _usuarioService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }
        //public LivroController(ILivroService livroService, IUsuarioService usuarioService)
        //{
        //    _livroService = livroService;
        //    _usuarioService = usuarioService;
        //}


        [HttpPost]
        public async Task<ActionResult> Incluir(LivroDTO livroDTO)
        {
            var livroDTOIncluido = await _livroService.Incluir(livroDTO);
            if (livroDTOIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o livro");
            }
            return Ok("Livro incluído com sucesso.");
        }

        [HttpPut]
        public async Task<ActionResult> Alterar(LivroDTO livroDTO)
        {
            var livroAlterado = await _livroService.Alterar(livroDTO);
            if(livroAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o livro.");
            }
            return Ok("Livro alterado com suscesso");
        }

        [HttpDelete]
        public async Task<ActionResult> Excluir(int id)
        {
            //var userId = User.GetId();
            //var usuario = await _usuarioService.SelecionarAsync(userId);
            //if (!usuario.IsAdmin) return Unauthorized("Você não tem permissão para excluir livros.");

            var livroDTOExcluido = await _livroService.Excluir(id);
            if(livroDTOExcluido == null)
            {
                return BadRequest("Ocorreu um erro ao excluir o livro.");
            }
            return Ok("Livro excluído com sucesso.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Selecionar(int id)
        {
            var livroDTO = await _livroService.SelecionarAsync(id);
            if(livroDTO == null)
            {
                return NotFound("Livro não encontrado");
            }
            return Ok(livroDTO);
        }
        
        [HttpGet]
        public async Task<ActionResult> SelecionarTodos()
        {
            var livroDTO = await _livroService.SelecionarTodosAsync();
            return Ok(livroDTO);
        }



    }
}
