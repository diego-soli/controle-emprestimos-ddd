using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Application.DTOs
{
    public class EmprestimoDTO
    {
        
        public int Id { get;  set; }
        public int IdCliente { get;  set; }
        public int IdLivro { get;  set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataEntrega { get; set; }
        public bool Entregue { get; set; }

        public ClienteDTO ClienteDTO { get; set; }
        public LivroDTO LivroDTO { get; set; }


    }
}
