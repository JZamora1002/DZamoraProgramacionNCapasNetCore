﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Dependiente
{
    public int IdDependiente { get; set; }

    public string? NumeroEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public DateTime FechaDeNacimiento { get; set; }

    public string? EstadoCivil { get; set; }

    public string? Genero { get; set; }

    public string Telefono { get; set; } = null!;

    public string? Rfc { get; set; }

    public int? IdDependienteTipo { get; set; }

    public string? TipoDependiente { get; set; }
    public virtual DependienteTipo? IdDependienteTipoNavigation { get; set; }

    public virtual Empleado? NumeroEmpleadoNavigation { get; set; }
}
