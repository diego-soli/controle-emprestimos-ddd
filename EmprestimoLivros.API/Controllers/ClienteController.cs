﻿using EmprestimoLivros.API.Extensions;
using EmprestimoLivros.API.Models;
using EmprestimosLivros.Application.DTOs;
using EmprestimosLivros.Application.Interfaces;
using EmprestimosLivros.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IUsuarioService _usuarioService;

        public ClienteController(IClienteService clienteService, IUsuarioService usuarioService)
        {
            _clienteService = clienteService;
            _usuarioService = usuarioService;
        }

        [HttpPost]     
        public async Task<ActionResult> Incluir(ClienteDTO clienteDTO)
        {
            var clienteDTOIncluido = await _clienteService.Incluir(clienteDTO);
            if (clienteDTOIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o cliente");
            }
            return Ok("Cliente incluído com sucesso.");
        }

        [HttpPut]
        public async Task<ActionResult> Alterar(ClienteDTO clienteDTO)
        {
            var clienteDTOAlterado = await _clienteService.Alterar(clienteDTO);
            if (clienteDTOAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o cliente");
            }
            return Ok("Cliente alterado com sucesso.");
        }

        [HttpDelete]
        public async Task<ActionResult> Excluir(int id)
        {
            var userId = User.GetId();
            var usuario = await _usuarioService.SelecionarAsync(userId);
            
            if (!usuario.IsAdmin) return Unauthorized("Você não tem permissao para excluir clientes.");

            var clienteDTOExcluido = await _clienteService.Excluir(id);
            if (clienteDTOExcluido == null)
            {
                return BadRequest("Ocorreu um erro ao excluir o cliente");
            }
            return Ok("Cliente excluido com sucesso.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Selecionar(int id)
        {
            var clienteDTO = await _clienteService.SelecionarAsync(id);
            if (clienteDTO == null)
            {
                return NotFound("Cliente não encontrado.");
            }
            return Ok(clienteDTO);
        }

        [HttpGet]
        public async Task<ActionResult> SelecionarTodos([FromQuery]PaginationParams paginationParams)
        {
            var clientesDTO = await _clienteService.SelecionarTodosAsync(paginationParams.PageNumber,paginationParams.PageSize);
            Response.AddPaginationHeader(new PaginationHeader(clientesDTO.CurrentPage, clientesDTO.PageSize, clientesDTO.TotalCount, clientesDTO.TotalPages));
            return Ok(clientesDTO);
        }
    }
}
