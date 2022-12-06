using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Empresa empresa = new ML.Empresa();

            result = BL.Empresa.GetAllEF(empresa);
            if (result.Correct == true)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Message = "OCURRÓ EL SIGUIENTE ERROR " + result.ErrorMessage;
            }
            return View(empresa);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            

            result = BL.Empresa.GetAllEF(empresa);
            if (result.Correct == true)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Message = "OCURRÓ EL SIGUIENTE ERROR " + result.ErrorMessage;
            }
            return View(empresa);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            if (IdEmpresa == null)
            {
                return View(empresa);
            }
            else
            {
                empresa.IdEmpresa = IdEmpresa.Value;
                ML.Result result = BL.Empresa.GetByIdEF(empresa);
                if (result.Correct == true)
                {
                    empresa = (ML.Empresa)result.Object;

                }
                else
                {
                    ViewBag.Message = "OCURRIO UN ERROR AL CONSULTAR POR: " + result.ErrorMessage;
                }
                return View(empresa);
            }
           
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {

            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["ImagenData"];



            if (image != null)
            {

                byte[] ImagenBytes = ConvertToBytes(image);

                empresa.Logo = Convert.ToBase64String(ImagenBytes);
            }
            if (empresa.IdEmpresa == 0)
            {
                result = BL.Empresa.AddEF(empresa);
                if (result.Correct == true)
                {
                    ViewBag.Message = "La Empresa Ha Sido Agregada Exitosamente";
                }
                else
                {
                    ViewBag.Message = "La Empresa No Ha Sido Agregada Exitosamente";
                }


            }
            else
            {
                result = BL.Empresa.UpdateEF(empresa);
                if (result.Correct == true)
                {
                    ViewBag.Message = "La Empresa Ha Sido Actualizada Exitosamente";
                }
                else
                {
                    ViewBag.Message = "La Empresa No Ha Sido Actualizada Exitosamente";
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            ML.Empresa empresa = new ML.Empresa();
            if (IdEmpresa == 0)
            {
                ViewBag.Message = "Ocurrió un Error Al Eliminar" + result.ErrorMessage;
            }
            else
            {
                empresa.IdEmpresa = IdEmpresa;
                result = BL.Empresa.DeleteEF(empresa);
                if (result.Correct == true)
                {
                    ViewBag.Message = "La Empresa Se Ha Eliminado Correctamente";
                }
                else
                {
                    ViewBag.Message = "La Empresa No Se Ha Eliminado Correctamente";
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
