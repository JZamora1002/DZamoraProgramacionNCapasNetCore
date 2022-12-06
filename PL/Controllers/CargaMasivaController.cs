using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.OleDb;
using Microsoft.AspNetCore.Hosting.Server;
using System.Data;
using ML;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]
        public ActionResult CargaTXT()
        {

            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            string fileError = @"C:\Users\digis\OneDrive\Documents\Javier David Zamora Garcia\Bloc De Notas\ErrorFile.txt";
            System.IO.File.WriteAllText(fileError, string.Empty);


            StreamWriter streamWriter = new StreamWriter(fileError, true, Encoding.ASCII);
            
            IFormFile fileTXT = Request.Form.Files["archivoTXT"];
            if (fileTXT != null)
            {
                int totalLineas = 0, lineasCorrectas = 0;
                string line, line2;
                StreamReader TextFile = new StreamReader(fileTXT.OpenReadStream());

                line = TextFile.ReadLine();


                while ((line = TextFile.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');

                    usuario.Nombre = lines[0];
                    usuario.FechaDeNacimiento = lines[1];
                    usuario.NumeroDeTelefono = lines[2];
                    usuario.Email = lines[3];
                    usuario.UserName = lines[4];
                    usuario.ApellidoPaterno = lines[5];
                    usuario.ApellidoMaterno = lines[6];
                    usuario.Password = lines[7];
                    usuario.Sexo = lines[8];
                    usuario.Celular = lines[9];
                    usuario.CURP = lines[10];
                    usuario.Imagen = null;
                    usuario.NombreCompleto = usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno;

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = Int32.Parse(lines[11]);

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = lines[12];
                    usuario.Direccion.NumeroInterior = lines[13];
                    usuario.Direccion.NumeroExterior = lines[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = Int32.Parse(lines[15]);

                    result = BL.Usuario.AddEF(usuario);
                    if (result.Correct == true)
                    {

                        streamWriter.WriteLine("El Usuario " + usuario.NombreCompleto + " Se Registro Correctamente");
                        lineasCorrectas++;
                    }
                    else
                    {
                        streamWriter.WriteLine("El Usuario " + usuario.NombreCompleto + " No se Registro Correctamente por el siguiente Error" + result.ErrorMessage);

                    }
                    totalLineas++;

                }

                if (lineasCorrectas == totalLineas)
                {
                    ViewBag.Message = "Carga De Usuarios Exitosa";
                    streamWriter.WriteLine(ViewBag.Message);
                    streamWriter.Close();
                    return PartialView("Modal");
                }

                else
                {

                    ViewBag.Message = "Hubo Errores En La Carga, Favor De Verificar el Archivo";
                    streamWriter.Close();
                    return PartialView("ModalErrores");
                }

            }
            else
            {

                ViewBag.Message = "El Archivo No Se Encontro";
                streamWriter.WriteLine(ViewBag.Message);

                streamWriter.Close();
                return PartialView("ModalErrores");
            }



            
        }

        [HttpPost]
        public ActionResult CargaExcel(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            ML.Result resultValidacion = new ML.Result();
            IFormFile archivo = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (archivo.Length > 0)
                {
                    string fileName = Path.GetFileName(archivo.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];
                    string conexion = _configuration["ConnectionStringExcel:value"];
                    if (extensionArchivo == extensionModulo)
                    {

                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                        if (System.IO.File.Exists(filePath) == false)
                        {
                            conexion = conexion + filePath;
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                archivo.CopyTo(stream);

                            }
                            result = BL.Usuario.ExcelADataTable(conexion);
                            if (result.Correct == true)
                            {
                                resultValidacion = BL.Usuario.ValidarExcel(result.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                    
                                }
                                else
                                {
                                    resultValidacion.Correct = false;
                                    ViewBag.Message = "Ocurrio un error al leer el arhivo";
                                    

                                }
                                return View("CargaMasiva", resultValidacion);
                            }
                        }
                    }

                }
                return View("CargaMasiva", resultValidacion);

            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ExcelADataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.AddEF(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Usuario con nombre: " + usuarioItem.Nombre + " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        

                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"C:\Users\digis\OneDrive\Documents\Javier David Zamora Garcia\Bloc De Notas\ErrorFile.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Usuarios No han sido registrados correctamente";
                        return PartialView("ModalErrores");
                    }
                    else
                    {
                        ViewBag.Message = "Los Usuarios han sido registrados correctamente";
                        return PartialView("Modal");
                    }

                }
                return PartialView("Modal");
            }
            
        }

        public FileResult Archivo()
        {
            string fileError = @"C:\Users\digis\OneDrive\Documents\Javier David Zamora Garcia\Bloc De Notas\ErrorFile.txt";

            byte[] result = System.IO.File.ReadAllBytes(fileError);
            
            return File(result, "text/plain", "errores.txt");
        }
    }
}
