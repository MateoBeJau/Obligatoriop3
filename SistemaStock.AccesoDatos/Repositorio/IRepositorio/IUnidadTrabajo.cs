﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo :IDisposable
    {
        IEconomatoRepositorio Economato { get; }
        ICategoriaRepositorio Categoria { get; }

        Task Save();
    }
}
