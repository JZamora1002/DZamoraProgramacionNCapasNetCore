using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getAllQuery = context.Dependientes.FromSqlRaw($"DependienteGetAll").ToList();
                    if (getAllQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in getAllQuery)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.Empleado = new ML.Empleado();

                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaDeNacimiento = obj.FechaDeNacimiento.ToString("dd-MM-yyyy");
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo.Value;

                            result.Objects.Add(dependiente);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetById(ML.Dependiente dependienteGetById)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getByIdQuery = context.Dependientes.FromSqlRaw($"DependienteGetById '{dependienteGetById.Empleado.NumeroEmpleado}'").ToList();
                    if (getByIdQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in getByIdQuery)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.Empleado = new ML.Empleado();

                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaDeNacimiento = obj.FechaDeNacimiento.ToString("dd-MM-yyyy");
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo.Value;

                            result.Objects.Add(dependiente);
                            
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Delete(ML.Dependiente dependienteDelete)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var deleteQuery = context.Database.ExecuteSqlRaw($"DependienteDelete {dependienteDelete.IdDependiente}");
                    if (deleteQuery >0 )
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Add(ML.Dependiente dependienteAdd)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var addQuery = context.Database.ExecuteSqlRaw($"DependienteAdd '{dependienteAdd.Empleado.NumeroEmpleado}', '{dependienteAdd.Nombre}', '{dependienteAdd.ApellidoPaterno}', '{dependienteAdd.ApellidoMaterno}', '{dependienteAdd.FechaDeNacimiento}', '{dependienteAdd.EstadoCivil}', '{dependienteAdd.Genero}', '{dependienteAdd.Telefono}', '{dependienteAdd.RFC}', {dependienteAdd.DependienteTipo.IdDependienteTipo}");
                    if (addQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Dependiente dependienteUpdate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var updateQuery = context.Database.ExecuteSqlRaw($"DependienteUpdate {dependienteUpdate.IdDependiente}, '{dependienteUpdate.Empleado.NumeroEmpleado}', '{dependienteUpdate.Nombre}', '{dependienteUpdate.ApellidoPaterno}', '{dependienteUpdate.ApellidoMaterno}', '{dependienteUpdate.FechaDeNacimiento}', '{dependienteUpdate.EstadoCivil}', '{dependienteUpdate.Genero}', '{dependienteUpdate.Telefono}', '{dependienteUpdate.RFC}', {dependienteUpdate.DependienteTipo.IdDependienteTipo}");
                    if (updateQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
