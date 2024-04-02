using Exemplo.Async.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemplo.Async.Classe
{
    public class Cliente
    {
        private MeuContexto _contexto;
        public Cliente()
        {
            _contexto = new MeuContexto();
            _contexto.Database.EnsureCreated();
            _contexto.AdicionarCliente();
        }

        public async Task ListarClientesAsync()
        {
            List<ClienteDto> clientes = await _contexto.ClienteDto.ToListAsync();
            foreach (var cliente in clientes)
            {
                Console.WriteLine(cliente);
            }

        }
    }
}
