using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getAllQuery = context.Rols.FromSql($"RolGetAll").ToList();
                    result.Objects = new List<object>();
                    if (getAllQuery!=null)
                    {
                        foreach (var obj in getAllQuery)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = obj.IdRol;
                            rol.NombreRol = obj.Rol1;
                            result.Objects.Add(rol);
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
    }
}
