using EmprestimoLivros.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Infra.Data.EntitiesConfiguration
{
    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NomeLivro).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Autor).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Editora).HasMaxLength(50).IsRequired();
            builder.Property(x => x.AnoPublicacao).IsRequired();
            builder.Property(x => x.Edicao).HasMaxLength(50).IsRequired();
        }
    }
}
