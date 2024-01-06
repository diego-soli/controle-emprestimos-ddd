using EmprestimoLivros.Domain.Entities;
using EmprestimosLivros.Application.DTOs;
using EmprestimosLivros.Application.Interfaces;
using EmprestimosLivros.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmprestimoController : Controller
    {
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimoController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpPost]
        public async Task<ActionResult> Incluir(EmprestimoPostDTO emprestimoPostDTO)
        {

            var disponivel = await _emprestimoService.VerificaDisponibilidadeAsync(emprestimoPostDTO.IdLivro);
            if (!disponivel) return BadRequest("O livro não está disponível para empréstimo.");

            emprestimoPostDTO.DataEmprestimo = DateTime.Now;
            emprestimoPostDTO.Entregue = false;

            var emprestimoDTOIncluido = await _emprestimoService.Incluir(emprestimoPostDTO);
            if (emprestimoDTOIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o emprestimo.");
            }
            return Ok("Emprestimo incluído com sucesso.");
        }

        [HttpPut]
        public async Task<ActionResult> Alterar(EmprestimoPutDTO emprestimoPutDTO)
        {
            var emprestimoDTO = await _emprestimoService.SelecionarAsync(emprestimoPutDTO.Id);
            if (emprestimoDTO == null) return NotFound("Empréstimo não encontrado.");

            emprestimoDTO.DataEntrega = emprestimoPutDTO.DataEntrega;
            emprestimoDTO.Entregue = emprestimoPutDTO.Entregue;

            var emprestimo = await _emprestimoService.Alterar(emprestimoDTO);
            if (emprestimo == null)  return BadRequest("Ocorreu um erro ao alterar o emprestimo.");

            return Ok("Cliente alterado com sucesso.");
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Excluir(int Id)
        {
            //var emprestimo = await _emprestimoService.SelecionarAsync(Id);
            var emprestimoExcluido = await _emprestimoService.Excluir(Id);
            if (emprestimoExcluido == null) return BadRequest("Ocorreu um erro ao localizar o emprestimo.");
            return Ok("Emprestimo excluido com sucesso.");

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Selecionar(int Id)
        {
            var dtoEmprestimo = await _emprestimoService.SelecionarAsync(Id);
            if (dtoEmprestimo == null) return NotFound("Emprestimo não encontrado.");
            return Ok(dtoEmprestimo);
        }

        [HttpGet]
        public async Task<ActionResult> SelecionarTodos()
        {
            var dtoEmprestimo = await _emprestimoService.SelecionarTodosAsync();
            return Ok(dtoEmprestimo);
        }


    }
}
