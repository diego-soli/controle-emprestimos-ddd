using EmprestimoLivros.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoLivros.Domain.Entities
{
    public class Emprestimo
    {
        public int Id { get; private set; }
        public int IdCliente { get; private set; }
        public int IdLivro { get; private set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public bool Entregue { get; private set; }

        public Cliente Cliente { get;  set; }
        public Livro Livro { get; set; }

        public Emprestimo(int id, int idCliente, int idLivro, DateTime dataEmprestimo, DateTime dataEntrega, bool entregue)
        {
            DomainExceptionValidation.When(id < 0, "O Id do empresitmo deve ser maior que zero.");
            
            Id = id;
            ValidateDomain(idCliente,idLivro,dataEmprestimo,dataEntrega,entregue);           
        }
        public Emprestimo(int idCliente, int idLivro, DateTime dataEmprestimo, DateTime dataEntrega, bool entregue)
        {
            ValidateDomain(idCliente,idLivro,dataEmprestimo,dataEntrega,entregue);           
        }
        public void Update(int idCliente, int idLivro, DateTime dataEmprestimo, DateTime dataEntrega, bool entregue)
        {
            ValidateDomain(idCliente,idLivro,dataEmprestimo,dataEntrega,entregue);           
        }


        public void ValidateDomain(int idCliente, int idLivro, DateTime dataEmprestimo, DateTime dataEntrega, bool entregue)
        {
            DomainExceptionValidation.When(idCliente < 0, "O Id do cliente deve ser maior que zero.");
            DomainExceptionValidation.When(idLivro < 0, "O Id do cliente deve ser maior que zero.");

            IdCliente = idCliente;
            IdLivro = idLivro;
            DataEmprestimo = dataEmprestimo;
            DataEntrega = dataEntrega;
            Entregue = entregue;
        }
    }
}
