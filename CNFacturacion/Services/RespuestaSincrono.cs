﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Services
{
    public class RespuestaSincrono
    {
        public string ConstanciaDeRecepcion { get; set; }
        public bool Exito { get; set; }
        public string MensajeError { get; set; }
    }
}