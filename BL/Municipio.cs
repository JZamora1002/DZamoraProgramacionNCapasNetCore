using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(ML.Municipio municipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getByIdEstadoQuery = context.Municipios.FromSql($"MunicipioGetByIdEstado {municipio.Estado.IdEstado}").ToList();
                    result.Objects = new List<object>();

                    if (getByIdEstadoQuery != null)
                    {
                        foreach (var obj in getByIdEstadoQuery)
                        {
                            ML.Municipio municipio1 = new ML.Municipio();
                            municipio1.IdMunicipio = obj.IdMunicipio;
                            municipio1.NombreMunicipio = obj.Nombre;
                            result.Objects.Add(municipio1);
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
