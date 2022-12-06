using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result AddEF(ML.Empresa empresaAdd)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var addquery = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresaAdd.NombreEmpresa}', '{empresaAdd.Telefono}', '{empresaAdd.Email}', '{empresaAdd.DireccionWeb}', '{empresaAdd.Logo}'");

                    if (addquery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
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

        public static ML.Result UpdateEF(ML.Empresa empresaUpdate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var updateQuery = context.Database.ExecuteSqlRaw($"EmpresaUpdate {empresaUpdate.IdEmpresa}, '{empresaUpdate.NombreEmpresa}', '{empresaUpdate.Telefono}', '{empresaUpdate.Email}', '{empresaUpdate.DireccionWeb}', '{empresaUpdate.Logo}'"); 

                    if(updateQuery > 0)
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

        public static ML.Result DeleteEF(ML.Empresa empresaDelete)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var deleteQuery = context.Database.ExecuteSqlRaw($"EmpresaDelete {empresaDelete.IdEmpresa}");

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

        public static ML.Result GetAllEF(ML.Empresa empresa1)
        {
            ML.Result result = new ML.Result();
            if (empresa1.NombreEmpresa == null)
            {
                empresa1.NombreEmpresa = "";
            }
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getAllQuery = context.Empresas.FromSqlRaw($"EmpresaGetAll '{empresa1.NombreEmpresa}'").ToList();
                    result.Objects = new List<object>();
                    if(getAllQuery!=null)
                    {
                        foreach (var obj in getAllQuery)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.NombreEmpresa = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;
                            result.Objects.Add(empresa);
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

        public static ML.Result GetByIdEF(ML.Empresa empresaGetById)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getByIdQuery = context.Empresas.FromSql($"EmpresaGetById {empresaGetById.IdEmpresa}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (getByIdQuery != null)
                    {
                       
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = getByIdQuery.IdEmpresa;
                            empresa.NombreEmpresa = getByIdQuery.Nombre;
                            empresa.Telefono = getByIdQuery.Telefono;
                            empresa.Email = getByIdQuery.Email;
                            empresa.DireccionWeb = getByIdQuery.DireccionWeb;
                            empresa.Logo = getByIdQuery.Logo;
                            result.Object=empresa;
                        
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
