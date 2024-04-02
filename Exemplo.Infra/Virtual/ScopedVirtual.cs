using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemplo.Infra.Virtual
{
    public sealed class ScopedVirtual
    {
        private static ScopedVirtual instance = null;
        public Guid Id { get; } = Guid.NewGuid();

    }
}
