using EmprestimoLivros.Domain.Entities;
using EmprestimoLivros.Domain.Interfaces;
using EmprestimosLivros.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Alterar(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Excluir(int id)
        {
            var usuario = await _context.Usuario.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return usuario;

        }

        public async Task<bool> ExisteUsuarioAsync()
        {
            return await _context.Usuario.AnyAsync();
        }

        public async Task<Usuario> Incluir(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }


        public async Task<Usuario> SelecionarAsync(int id)
        {
            var usuario = await _context.Usuario.Where(x => x.Id == id).FirstOrDefaultAsync();
            return usuario;
        }


        public async Task<IEnumerable<Usuario>> SelecionarTodosAsync()
        {
            return await _context.Usuario.ToListAsync();
        }
    }
}
