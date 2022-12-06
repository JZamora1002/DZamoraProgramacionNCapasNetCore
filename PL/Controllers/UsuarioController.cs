using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = new ML.Result();

            ML.Result resultRoles = new ML.Result();
            resultRoles = BL.Rol.GetAllEF();

            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Usuario/GetAll");
                    

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result.Objects = new List<object>();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                if (result.Correct == true)
                {
                    usuario.Usuarios = result.Objects;
                    usuario.Rol.Roles = resultRoles.Objects;
                }
                else
                {
                    ViewBag.Message = "OCURRÓ EL SIGUIENTE ERROR " + result.ErrorMessage;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            
            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Busqueda busqueda = new ML.Busqueda();
            busqueda.Nombre = usuario.Nombre;
            busqueda.ApellidoPaterno = usuario.ApellidoPaterno;
            busqueda.IdRol = usuario.Rol.IdRol;
           ML.Result result = new ML.Result();  
            ML.Result resultRoles = new ML.Result();
            resultRoles = BL.Rol.GetAllEF();
            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.PostAsJsonAsync<ML.Busqueda>("Usuario/Busqueda",busqueda);


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Usuario>();
                        readTask.Wait();
                        result.Objects = new List<object>();
                        foreach (var resultItem in readTask.Result.Usuarios)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                usuario.Rol = new ML.Rol();

                if (result.Correct == true)
                {
                    usuario.Usuarios = result.Objects;
                    usuario.Rol.Roles = resultRoles.Objects;
                }
                else
                {
                    ViewBag.Message = "OCURRÓ EL SIGUIENTE ERROR " + result.ErrorMessage;
                    usuario.Rol.Roles = resultRoles.Objects;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            
            return View(usuario);
           
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Result result = new Result();
            ML.Result resultRoles = new ML.Result();
            ML.Result resultPaises = new ML.Result();
            ML.Result resultEstados = new ML.Result();
            ML.Result resultMunicipios = new ML.Result();
            ML.Result resultColonias = new ML.Result();

            ML.Usuario usuario = new ML.Usuario();
            ML.Pais pais = new ML.Pais();
            ML.Estado estado = new ML.Estado();
            ML.Municipio municipio = new ML.Municipio();
            ML.Colonia colonia = new ML.Colonia();


            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            estado.Pais = new ML.Pais();
            municipio.Estado = new ML.Estado();
            colonia.Municipio = new ML.Municipio();

            usuario.Rol = new ML.Rol();
            resultRoles = BL.Rol.GetAllEF();
            resultPaises = BL.Pais.GetAll();

            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync(("Usuario/GetById/" + IdUsuario.Value));


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Usuario>();
                        readTask.Wait();
                        result.Object = readTask.Result;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                if (IdUsuario == null)
                {
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                    usuario.Rol.Roles = resultRoles.Objects;
                    return View(usuario);
                }
                else
                {
                    if (result.Correct == true)
                    {

                        usuario = (ML.Usuario)result.Object;

                        estado.Pais.IdPais = usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais;
                        resultEstados = BL.Estado.GetByIdPais(estado);

                        municipio.Estado.IdEstado = usuario.Direccion.Colonia.Municipio.Estado.IdEstado;
                        resultMunicipios = BL.Municipio.GetByIdEstado(municipio);

                        colonia.Municipio.IdMunicipio = usuario.Direccion.Colonia.Municipio.IdMunicipio;
                        resultColonias = BL.Colonia.GetByIdMunicipio(colonia);

                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                        usuario.Direccion.Colonia.Colonias = resultColonias.Objects;

                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio el siguiente error: " + result.ErrorMessage;
                    }
                }
                

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }



            usuario.Rol.Roles = resultRoles.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                return View(usuario);
            
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["ImagenData"];


            
            if (image != null)
            {
                
                byte[] ImagenBytes = ConvertToBytes(image);
                
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if(ModelState.IsValid == true)
            {
                if (usuario.IdUsuario == 0)
                {
                    try
                    {
                        string urlAPI = _configuration["UrlAPI"];
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlAPI);

                            var responseTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);


                            responseTask.Wait();

                            var resultServicio = responseTask.Result;

                            if (resultServicio.IsSuccessStatusCode)
                            {

                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                            }
                        }
                        if (result.Correct == true)
                        {
                            ViewBag.Message = "El Usuario Se ha Registrado Con Exito";
                        }
                        else
                        {
                            ViewBag.Message = "El Usuario No Se ha Registrado Con Exito";
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }
                    
                }
                else
                {
                    try
                    {
                        string urlAPI = _configuration["UrlAPI"];
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlAPI);

                            var responseTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);


                            responseTask.Wait();

                            var resultServicio = responseTask.Result;

                            if (resultServicio.IsSuccessStatusCode)
                            {

                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                            }
                        }
                        if (result.Correct == true)
                        {
                            ViewBag.Message = "El Usuario Se ha Actualizado Con Exito";
                        }
                        else
                        {
                            ViewBag.Message = "El Usuario No Se ha Actualizado Con Exito";
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }

                }
                return PartialView("Modal");
            }
            else
            {
                ML.Result resultRoles = new ML.Result();
                ML.Result resultPaises = new ML.Result();
                ML.Result resultEstados = new ML.Result();
                ML.Result resultMunicipios = new ML.Result();
                ML.Result resultColonias = new ML.Result();

                
                ML.Pais pais = new ML.Pais();
                ML.Estado estado = new ML.Estado();
                ML.Municipio municipio = new ML.Municipio();
                ML.Colonia colonia = new ML.Colonia();


                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                estado.Pais = new ML.Pais();
                municipio.Estado = new ML.Estado();
                colonia.Municipio = new ML.Municipio();

                usuario.Rol = new ML.Rol();
                resultRoles = BL.Rol.GetAllEF();
                resultPaises = BL.Pais.GetAll();

                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                usuario.Rol.Roles = resultRoles.Objects;
                return View(usuario);
                
            }
            
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            if(IdUsuario == 0)
            {
                ViewBag.Message = "Ocurrió un Error Al Eliminar";
            }
            else
            {
                usuario.IdUsuario = IdUsuario;
                result = BL.Usuario.DeleteEF(usuario);
                if (result.Correct == true)
                {
                    ViewBag.Message = "El Usuario Se ha Eliminado Con Exito";
                }
                else
                {
                    ViewBag.Message = "El Usuario No Se ha Eliminado Con Exito";
                }
            }
            return PartialView("Modal");
        }

        public JsonResult GetEstado(int IdPais)
        {

            ML.Result result = new ML.Result();
            ML.Estado estado = new ML.Estado();
            estado.Pais = new ML.Pais();
            estado.Pais.IdPais = IdPais;
            result = BL.Estado.GetByIdPais(estado);
            return Json(result.Objects);
        }
        public JsonResult GetMunicipio(int IdEstado)
        {

            ML.Result result = new ML.Result();
            ML.Municipio municipio = new ML.Municipio();
            municipio.Estado = new ML.Estado();
            municipio.Estado.IdEstado = IdEstado;
            result = BL.Municipio.GetByIdEstado(municipio);
            return Json(result.Objects);
        }
        public JsonResult GetColonia(int IdMunicipio)
        {

            ML.Result result = new ML.Result();
            ML.Colonia colonia = new ML.Colonia();
            colonia.Municipio = new ML.Municipio();
            colonia.Municipio.IdMunicipio = IdMunicipio;
            result = BL.Colonia.GetByIdMunicipio(colonia);
            return Json(result.Objects);
        }
        public JsonResult UpdateStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            usuario.Status = Status;
            result = BL.Usuario.UpdateStatus(usuario);
            return Json(result);
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