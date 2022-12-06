using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {

        public static ML.Result GetByIdPais(ML.Estado estado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getAllQuery = context.Estados.FromSqlRaw($"EstadoGetByIdPais {estado.Pais.IdPais}").AsEnumerable().ToList();
                    result.Objects = new List<object>();
                    if(getAllQuery != null)
                    {
                        foreach (var obj in getAllQuery)
                        {
                            ML.Estado estado1 = new ML.Estado();
                            estado1.IdEstado = obj.IdEstado;
                            estado1.Nombre = obj.Nombre;
                            
                            result.Objects.Add(estado1);
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
    }
}
