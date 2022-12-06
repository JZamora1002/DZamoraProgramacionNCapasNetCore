using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            ML.DependienteTipo dependienteTipo = new ML.DependienteTipo();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getAllQuery = context.DependienteTipos.FromSqlRaw($"DependienteTipoGetAll");
                    if (getAllQuery !=null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in getAllQuery)
                        {
                            dependienteTipo.IdDependienteTipo = obj.IdDependienteTipo;
                            dependienteTipo.NombreDependienteTipo = obj.Nombre;
                            result.Objects.Add(dependienteTipo);
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
