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
    public class LivroRepository : ILivroRepository
    {
        private readonly ApplicationDbContext _context;

        public LivroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Livro> Alterar(Livro livro)
        {
            _context.Livro.Update(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> Excluir(int id)
        {
            var livro = await _context.Livro.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Livro.Remove(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> Incluir(Livro livro)
        {
            _context.Livro.Add(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> SelecionarAsync(int id)
        {
            var Livro = await _context.Livro.Where(x => x.Id == id).FirstOrDefaultAsync();
            return Livro;
        }

        public async Task<IEnumerable<Livro>> SelecionarTodosAsync()
        {
            return await _context.Livro.ToListAsync();
        }
    }
}
