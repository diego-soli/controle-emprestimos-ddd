using EmprestimoLivros.Domain.Entities;
using EmprestimoLivros.Domain.Interfaces;
using EmprestimoLivros.Domain.Pagination;
using EmprestimosLivros.Infra.Data.Context;
using EmprestimosLivros.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;
        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> Alterar(Cliente cliente)
        {            
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Excluir(int id)
        {
            var cliente = await _context.Cliente.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return cliente;
                        
        }

        public async Task<Cliente> Incluir(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }


        public async Task<Cliente> SelecionarAsync(int id)
        {
            var cliente = await _context.Cliente.Where(x => x.Id == id).FirstOrDefaultAsync();
            return cliente;
        }


        public async Task<PagedList<Cliente>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            var query = _context.Cliente.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
            
        }

    }
}
