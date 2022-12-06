using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getAllQuery = context.Empleados.FromSqlRaw($"EmpleadoGetAll").ToList();
                    if (getAllQuery !=null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in getAllQuery)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.Empresa = new ML.Empresa();

                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaDeNacimiento = obj.FechaDeNacimiento.ToString("dd-MM-yyyy");
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.ToString("dd-MM-yyyy");
                            empleado.Foto = obj.Foto;
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.NombreEmpresa = obj.NombreEmpresa;
                            result.Objects.Add(empleado);
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

        public static ML.Result GetById(ML.Empleado empleadoGetById)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getByIdQuery = context.Empleados.FromSqlRaw($"EmpleadoGetById '{empleadoGetById.NumeroEmpleado}'").AsEnumerable().FirstOrDefault();
                    if (getByIdQuery != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.Empresa = new ML.Empresa();

                        empleado.NumeroEmpleado = getByIdQuery.NumeroEmpleado;
                        empleado.RFC = getByIdQuery.Rfc;
                        empleado.Nombre = getByIdQuery.Nombre;
                        empleado.ApellidoPaterno = getByIdQuery.ApellidoPaterno;
                        empleado.ApellidoMaterno = getByIdQuery.ApellidoMaterno;
                        empleado.Email = getByIdQuery.Email;
                        empleado.Telefono = getByIdQuery.Telefono;
                        empleado.FechaDeNacimiento = getByIdQuery.FechaDeNacimiento.ToString("dd-MM-yyyy");
                        empleado.NSS = getByIdQuery.Nss;
                        empleado.FechaIngreso = getByIdQuery.FechaIngreso.ToString("dd-MM-yyyy");
                        empleado.Foto = getByIdQuery.Foto;
                        empleado.Empresa.IdEmpresa = getByIdQuery.IdEmpresa.Value;
                        empleado.Empresa.NombreEmpresa = getByIdQuery.NombreEmpresa;
                        
                        result.Object = empleado;

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

        public static ML.Result Delete(ML.Empleado empleadoDelete)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var deleteQuery = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{empleadoDelete.NumeroEmpleado}'");
                    if (deleteQuery > 0)
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

        public static ML.Result Add(ML.Empleado empleadoAdd)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var addQuery = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleadoAdd.NumeroEmpleado}', '{empleadoAdd.RFC}', '{empleadoAdd.Nombre}', '{empleadoAdd.ApellidoPaterno}', '{empleadoAdd.ApellidoMaterno}', '{empleadoAdd.Email}', '{empleadoAdd.Telefono}', '{empleadoAdd.FechaDeNacimiento}', '{empleadoAdd.NSS}', '{empleadoAdd.FechaIngreso}', '{empleadoAdd.Foto}', {empleadoAdd.Empresa.IdEmpresa}");
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

        public static ML.Result Update(ML.Empleado empleadoUpdate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var updateQuery = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleadoUpdate.NumeroEmpleado}', '{empleadoUpdate.RFC}', '{empleadoUpdate.Nombre}', '{empleadoUpdate.ApellidoPaterno}', '{empleadoUpdate.ApellidoMaterno}', '{empleadoUpdate.Email}', '{empleadoUpdate.Telefono}', '{empleadoUpdate.FechaDeNacimiento}', '{empleadoUpdate.NSS}', '{empleadoUpdate.FechaIngreso}', '{empleadoUpdate.Foto}', {empleadoUpdate.Empresa.IdEmpresa}");
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
