using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos.Komponent
{
    public interface IVMKomponente
    {
        string Name { get;  }
        string Author { get; }

    }

    public class vmKomponente : IVMKomponente
    {
        public string Name { get; private set; }
        public string Author { get; private set; }

        public vmKomponente(string name, string autor)
        {
            this.Name = name;
            this.Author = autor;
        }

        
    }
}
