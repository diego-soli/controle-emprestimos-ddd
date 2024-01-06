using EmprestimoLivros.Domain.Entities;
using EmprestimosLivros.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> Incluir(UsuarioDTO usuario);
        Task<UsuarioDTO> Alterar(UsuarioDTO usuario);
        Task<UsuarioDTO> Excluir(int id);
        Task<UsuarioDTO> SelecionarAsync(int id);
        Task<IEnumerable<UsuarioDTO>> SelecionarTodosAsync();
        Task<bool> ExisteUsuarioAsync();
    }
}
