using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemplo.Infra.Singleton
{
    public sealed class SingletonVirtual
    {
        private static SingletonVirtual instance = null;
        public Guid Id { get; } = Guid.NewGuid();

        private SingletonVirtual()
        {
        }

        public static SingletonVirtual Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonVirtual();
                }
                return instance;
            }
        }
    }
}