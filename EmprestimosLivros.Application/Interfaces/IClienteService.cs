using EmprestimoLivros.Domain.Entities;
using EmprestimoLivros.Domain.Pagination;
using EmprestimosLivros.Application.DTOs;

namespace EmprestimosLivros.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDTO> Incluir(ClienteDTO clienteDTO);
        Task<ClienteDTO> Alterar(ClienteDTO clienteDTO);
        Task<ClienteDTO> Excluir(int id);
        Task<ClienteDTO> SelecionarAsync(int id);
        //Task<IEnumerable<ClienteDTO>> SelecionarTodosAsync();
        Task<PagedList<ClienteDTO>> SelecionarTodosAsync(int pageNumber, int pageSize);


    }
}
