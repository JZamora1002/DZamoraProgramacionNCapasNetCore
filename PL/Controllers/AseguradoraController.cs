using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PL_MVC.Controllers
{
    public class AseguradoraController : Controller
    {
        // GET: Aseguradora
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAllEF();
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            if(result.Correct == true)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            else
            {
                ViewBag.Message = "OCURRIO UN ERROR AL CONSULTAR: " + result.ErrorMessage;
            }
            return View(aseguradora);
        }

        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result resultUsuarios = new ML.Result();
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();
            resultUsuarios = BL.Usuario.GetAllEF(usuario);

            if(IdAseguradora == null)
            {
                aseguradora.Usuario.Usuarios = resultUsuarios.Objects;
                
            }
            else
            {
                aseguradora.IdAseguradora = IdAseguradora.Value;
                ML.Result result = BL.Aseguradora.GetByIdEF(aseguradora);
                if(result.Correct == true)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                    aseguradora.Usuario.Usuarios = resultUsuarios.Objects;
                }
                else
                {
                    ViewBag.Message = "OCURRIO UN ERROR AL CONSULTAR POR: "+result.ErrorMessage;
                }
                aseguradora.Usuario.Usuarios = resultUsuarios.Objects;
                
            }
            return View(aseguradora);
        }

        [HttpPost]
        public ActionResult Form (ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            
            if(aseguradora.IdAseguradora == 0)
            {
                result = BL.Aseguradora.AddEF(aseguradora);
                if (result.Correct == true)
                {
                    ViewBag.Message = "La Aseguradora Se Ha Registrado Correctamente";
                }
                else
                {
                    ViewBag.Message = "La Aseguradora No Se Ha Registrado Correctamente";
                }
            }
            else
            {
                result = BL.Aseguradora.UpdateEF(aseguradora);
                if (result.Correct == true)
                {
                    ViewBag.Message = "La Aseguradora Se Ha Actualizado Correctamente";
                }
                else
                {
                    ViewBag.Message = "La Aseguradora No Se Ha Actualizado Correctamente";
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete (int IdAseguradora)
        {
            ML.Result result = new ML.Result();
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            if(IdAseguradora == 0)
            {
                ViewBag.Message = "Ocurrió un Error Al Eliminar" + result.ErrorMessage;
            }
            else
            {
                aseguradora.IdAseguradora = IdAseguradora;
                result = BL.Aseguradora.DeleteEF(aseguradora);
                if (result.Correct == true)
                {
                    ViewBag.Message = "La Aseguradora Se Ha Eliminado Correctamente";
                }
                else
                {
                    ViewBag.Message = "La Aseguradora No Se Ha Eliminado Correctamente";
                }
            }
            return PartialView("Modal");
        }
    }
}