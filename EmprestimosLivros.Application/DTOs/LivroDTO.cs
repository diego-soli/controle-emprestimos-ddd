using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Application.DTOs
{
    public class LivroDTO
    {
        public int Id { get;  set; }

        [MaxLength(50,ErrorMessage ="O nome do livro não pode ultrapassar de 50 caracteres.")]
        [Required(ErrorMessage ="O campo nome é obrigatório.")]
        public string NomeLivro { get;  set; }

        [MaxLength(200,ErrorMessage = "O nome do autor não pode ultrapassar de 200 caracteres.")]
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Autor { get;  set; }

        [MaxLength(50,ErrorMessage = "O nome da editora não pode ultrapassar de 50 caracteres.")]
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Editora { get;  set; }
        public DateTime AnoPublicacao { get;  set; }
        
        [MaxLength(50,ErrorMessage = "A edição não pode ultrapassar 50 caracteres.")]
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Edicao { get;  set; }
    }
}
