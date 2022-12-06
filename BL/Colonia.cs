using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(ML.Colonia colonia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getByIdMunicipioQuery = context.Colonia.FromSql($"ColoniaGetByIdMunicipio { colonia.Municipio.IdMunicipio}").ToList();
                    result.Objects = new List<object>();

                    if (getByIdMunicipioQuery != null)
                    {
                        foreach (var obj in getByIdMunicipioQuery)
                        {
                            ML.Colonia colonia1 = new ML.Colonia();
                            colonia1.IdColonia = obj.IdColonia;
                            colonia1.CodigoPostal = obj.CodigoPostal;
                            colonia1.NombreColonia = obj.Nombre;
                            result.Objects.Add(colonia1);
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
