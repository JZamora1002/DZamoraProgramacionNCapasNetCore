using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Empresa empresa = new ML.Empresa();

            ML.Result result = new ML.Result();
            ML.Result resultEmpresas = new ML.Result();

            resultEmpresas = BL.Empresa.GetAllEF(empresa);
            result = BL.Empleado.GetAll();
            if (result.Correct == true)
            {
                empleado.Empleados = result.Objects;
                empleado.Empresa.Empresas = resultEmpresas.Objects;
            }
            else
            {
                ViewBag.Message = "OCURRIO EL SIGUIENTE ERROR " + result.ErrorMessage;
            }
            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(string? NumeroDeEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            ML.Empresa empresa = new ML.Empresa();

            ML.Result result = new ML.Result();
            ML.Result resultEmpresas = new ML.Result();

            resultEmpresas = BL.Empresa.GetAllEF(empresa);

            if (NumeroDeEmpleado == null)
            {
                
                empleado.Empresa.Empresas = resultEmpresas.Objects;
                return View(empleado);
            }
            else
            {
                empleado.NumeroEmpleado = NumeroDeEmpleado;
                result = BL.Empleado.GetById(empleado);
                if (result.Correct == true)
                {
                    empleado = (ML.Empleado)result.Object;
                }
                else
                {
                    ViewBag.Message = "Ocurrio el siguiente error: " + result.ErrorMessage;
                }
                empleado.Empresa.Empresas = resultEmpresas.Objects;
                return View(empleado);
            }
            
        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            ML.Result resultGetById = new ML.Result();
            resultGetById = BL.Empleado.GetById(empleado);
           

            IFormFile image = Request.Form.Files["ImagenData"];

            if (image != null)
            {

                byte[] ImagenBytes = ConvertToBytes(image);

                empleado.Foto = Convert.ToBase64String(ImagenBytes);
            }
            if (resultGetById.Correct == false)
            {
                result = BL.Empleado.Add(empleado);
                if (result.Correct == true)
                {
                    ViewBag.Message = "El Empleado Se Registró Exitosamente";
                }
                else
                {
                    ViewBag.Message = "El Empleado NO Se Registró Exitosamente";
                }
            }
            else
            {
                result = BL.Empleado.Update(empleado);
                if (result.Correct == true)
                {
                    ViewBag.Message = "El Empleado Se Actualizó Exitosamente";
                }
                else
                {
                    ViewBag.Message = "El Empleado NO Se Actualizó Exitosamente";
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(string NumeroDeEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = new ML.Result();

            if (NumeroDeEmpleado == null || NumeroDeEmpleado == "")
            {
                ViewBag.Message = "Ocurrio Un Error Al Eliminar " + result.ErrorMessage;
            }
            else
            {
                empleado.NumeroEmpleado = NumeroDeEmpleado;
                result = BL.Empleado.Delete(empleado);
                if (result.Correct == true)
                {
                    ViewBag.Message = "Empleado Eliminado Exitosamente";
                }
                else
                {
                    ViewBag.Message = "El Empleado No Fue Eliminado Exitosamente";
                }
            }
            return PartialView("Modal");
        }

        public static byte[] ConvertToBytes(IFormFile Imagen)
        {
            using var fileStream = Imagen.OpenReadStream();

            byte[] datos = new byte[fileStream.Length];
            fileStream.Read(datos, 0, (int)fileStream.Length);

            return datos;
        }
    }
}
