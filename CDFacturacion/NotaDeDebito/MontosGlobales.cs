﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.NotaDeDebito
{
    public class MontosGlobales
    {
        public Decimal TVVOperacionesGravadas { get; set; }
        public Decimal TVVOperacionesInafectas { get; set; }
        public Decimal TVVOperacionesExoneradas { get; set; }
        public Decimal SumatoriaIGV { get; set; }
        public Decimal SumatoriaISC { get; set; }
        public Decimal SumatoriaOtrosTributos { get; set; }
        public Decimal SumatorioOtrosCargos { get; set; }
        public Decimal TotalDescuentos { get; set; }
        public Decimal ImporteTotalVenta { get; set; }
    }
}
