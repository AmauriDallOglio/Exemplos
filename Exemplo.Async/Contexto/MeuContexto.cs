using AutoFixture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemplo.Async.Contexto
{
    public class MeuContexto : DbContext
    {
        public DbSet<ClienteDto> ClienteDto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AppDatabase");
        }

        /// <summary>
        /// Este método faz a criação de dados do banco em memória
        /// </summary>
        public void AdicionarCliente()
        {
            ClienteDto.RemoveRange(ClienteDto);
            SaveChanges();
            Fixture fixture = new Fixture(); //é uma classe que ajuda a criar dados de teste ou dados fictícios
            IEnumerable<ClienteDto> pessoas = fixture.CreateMany<ClienteDto>(5);
            ClienteDto.AddRange(pessoas);
            SaveChanges();
        }
    }
}
