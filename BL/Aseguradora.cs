using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using DL;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Aseguradora
    {

        //EF
        public static ML.Result AddEF(ML.Aseguradora aseguradoraAdd)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var addQuery = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradoraAdd.Nombre}', {aseguradoraAdd.Usuario.IdUsuario}");
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


        public static ML.Result UpdateEF(ML.Aseguradora aseguradoraUpdate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var updateQuery = context.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradoraUpdate.IdAseguradora}, '{aseguradoraUpdate.Nombre}', {aseguradoraUpdate.Usuario.IdUsuario}");
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

        public static ML.Result DeleteEF(ML.Aseguradora aseguradoraDelete)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var deleteQuery = context.Database.ExecuteSqlRaw($"AseguradoraDelete {aseguradoraDelete.IdAseguradora}");
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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getAllQuery = context.Aseguradoras.FromSql($"AseguradoraGetAll").ToList();

                    result.Objects = new List<object>();
                    if (getAllQuery != null)
                    {
                        foreach (var obj in getAllQuery)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();
                            aseguradora.Usuario.IdUsuario = Int32.Parse(obj.IdUsuario.ToString());
                            aseguradora.Usuario.Nombre = obj.NombreUsuario;
                            result.Objects.Add(aseguradora);
                        }
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

        public static ML.Result GetByIdEF(ML.Aseguradora aseguradoraGetById)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getByIdQuery = context.Aseguradoras.FromSql($"AseguradoraGetById {aseguradoraGetById.IdAseguradora}").AsEnumerable().FirstOrDefault();

                    
                    if (getByIdQuery != null)
                    {
                        
                            aseguradoraGetById.Usuario = new ML.Usuario();

                            aseguradoraGetById.IdAseguradora = getByIdQuery.IdAseguradora;
                            aseguradoraGetById.Nombre = getByIdQuery.Nombre;
                            aseguradoraGetById.FechaCreacion = getByIdQuery.FechaCreacion.ToString();
                            aseguradoraGetById.FechaModificacion = getByIdQuery.FechaModificacion.ToString();
                            

                            aseguradoraGetById.Usuario.IdUsuario = Int32.Parse(getByIdQuery.IdUsuario.ToString());
                        aseguradoraGetById.Usuario.Nombre = getByIdQuery.NombreUsuario;
                            result.Object=aseguradoraGetById;
                        
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                result.Correct = true;
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
