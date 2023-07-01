using SistemaStock.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Repositorio.IRepositorio
{
    public interface IEconomatoRepositorio :IRepositorio <Economato>
    {
        void Update(Economato economato);
    }
}
