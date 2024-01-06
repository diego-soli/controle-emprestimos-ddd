using EmprestimoLivros.Domain.Entities;
using EmprestimoLivros.Domain.Interfaces;
using EmprestimosLivros.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Infra.Data.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly ApplicationDbContext _context;

        public EmprestimoRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Emprestimo> Alterar(Emprestimo emprestimo)
        {
            _context.Emprestimo.Update(emprestimo);
            await _context.SaveChangesAsync();
            return emprestimo;
        }

        public async Task<Emprestimo> Excluir(int id)
        {
            var emprestimo = await _context.Emprestimo.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Remove(emprestimo);
            _context.SaveChangesAsync();
            return emprestimo;
        }

        public async Task<Emprestimo> Incluir(Emprestimo emprestimo)
        {
            _context.Emprestimo.Add(emprestimo);
            await _context.SaveChangesAsync();
            return(emprestimo);
        }

        public async Task<Emprestimo> SelecionarAsync(int id)
        {
            var emprestimo = await _context.Emprestimo.Include(x => x.Cliente).Include(x => x.Livro).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return emprestimo;
        }

        public async Task<IEnumerable<Emprestimo>> SelecionarTodosAsync()
        {
            return await _context.Emprestimo.Include( x => x.Cliente).Include(x => x.Livro).ToListAsync();
        }

        public async Task<bool> VerificaDisponibilidadeAsync(int idLivro)
        {
            var exiteEmprestimo = await _context.Emprestimo.Where(x => x.IdLivro == idLivro && x.Entregue == false).AnyAsync();
            return !exiteEmprestimo;
        }
        
    }
}
