using EmprestimoLivros.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoLivros.Domain.Entities
{
    public class Livro
    {
        public int Id { get; private set; }
        public string NomeLivro { get; private set; }
        public string Autor { get; private set; } 
        public string Editora { get; private set; }
        public DateTime AnoPublicacao { get; private set; }
        public string Edicao { get; private set; }
        public ICollection<Emprestimo> Emprestimos { get; set; }

        public Livro(int id, string nomeLivro, string autor, string editora, DateTime anoPublicacao, string edicao)
        {

            DomainExceptionValidation.When(Id < 0, "O Id do livro deve ser maior que zero.");
            Id = id;
            ValidateDomain(nomeLivro, autor, editora, anoPublicacao, edicao);

        }
        public Livro(string nomeLivro, string autor, string editora, DateTime anoPublicacao, string edicao)
        {   
            ValidateDomain(nomeLivro, autor, editora, anoPublicacao, edicao);
        }
        public void Update(string nomeLivro, string autor, string editora, DateTime anoPublicacao, string edicao)
        {   
            ValidateDomain(nomeLivro, autor, editora, anoPublicacao, edicao);
        }



        private void ValidateDomain(string nomeLivro, string autor, string editora, DateTime anoPublicacao, string edicao)
        {
            DomainExceptionValidation.When(nomeLivro.Length > 50, "Nome do livro deve ter no maximo 50 caracteres.");
            DomainExceptionValidation.When(autor.Length > 200, "Nome do autor deve ter no maximo 200 caracteres.");
            DomainExceptionValidation.When(editora.Length > 100, "Nome da editora no maximo de 100 caracteres.");
            DomainExceptionValidation.When(edicao.Length > 50, "A edição do livro deve ter no máximo 50 caracteres.");

            NomeLivro = nomeLivro;
            Autor = autor;
            Editora = editora;
            AnoPublicacao = anoPublicacao;
            Edicao = edicao;
        }
    }
}
