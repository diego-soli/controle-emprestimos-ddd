using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Application.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(11)]
        [MinLength(11, ErrorMessage = "O CPF deve ter no mínimo 14 caracteres.")]
        public string Cpf { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "O Nome deve ter no máximo 200 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "O Endereco deve ter no máximo 200 caracteres.")]
        public string Endereco { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "A Cidade deve ter no máximo 100 caracteres.")]
        public string Cidade { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O Bairro deve ter no máximo 100 caracteres.")]
        public string Bairro { get; set; }
        [Required]
        [StringLength(50)]
        public string Numero { get; set; }
        [Required]
        [StringLength(14, ErrorMessage = "O Celular deve ter no máximo 14 caracteres.")]
        public string Celular { get; set; }
        [Required]
        [StringLength(13, ErrorMessage = "O Telefone deve ter no máximo 13 caracteres.")]
        public string TelefoneFixo { get; set; }

    }
}
