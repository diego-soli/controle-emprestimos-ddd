using EmprestimoLivros.API.Models;
using EmprestimoLivros.Domain.Account;
using EmprestimosLivros.Application.DTOs;
using EmprestimosLivros.Application.Interfaces;
using EmprestimosLivros.Application.Services;
using EmprestimosLivros.Infra.Ioc;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IAuthenticate _authenticateService;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService, IAuthenticate authenticateService)
        {
            _usuarioService = usuarioService;
            _authenticateService = authenticateService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Incluir(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null) return BadRequest("Dados inálidos");

            var emailExiste = await _authenticateService.UserExists(usuarioDTO.Email);
            if (emailExiste) return BadRequest("Este e-mail já possui um cadastro");

            var existeUsuario = await _usuarioService.ExisteUsuarioAsync();
            if (!existeUsuario)
            {
                usuarioDTO.IsAdmin = true;
            } 
            else
            {
                if (User.FindFirst("id") == null) return Unauthorized();
            }
            var userId = User.GetId();
            var user = await _usuarioService.SelecionarAsync(userId);
            if (!user.IsAdmin) 
            {
                return Unauthorized("Você não tem permissão para incluir usuários.");
            }

            var usuario = await _usuarioService.Incluir(usuarioDTO);
            if (usuario == null) return BadRequest("Ocorreu um erro ao cadastrar");

            var token = _authenticateService.GenerateToken(usuario.Id, usuario.Email);
            return new UserToken
            {
                Token = token
            };

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Selecionar(LoginModel loginModel)
        {
            var existe = await _authenticateService.UserExists(loginModel.Email);
            if (!existe) return Unauthorized("Usuário não existe.");

            var result = await _authenticateService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (!result) return Unauthorized("Usuário inválido.");

            var usuario =  await _authenticateService.GetUserByEmail(loginModel.Email);
            var token = _authenticateService.GenerateToken(usuario.Id, usuario.Email);

            return new UserToken { Token = token };
        }

    }
}
