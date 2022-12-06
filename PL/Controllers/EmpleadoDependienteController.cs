using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAllEmp()
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
        public ActionResult GetAllDep(string NumeroDeEmpleado)
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Empleado empleado = new ML.Empleado();

            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.DependienteTipo = new ML.DependienteTipo();
            dependiente.Empleado = new ML.Empleado();
            dependiente.Empleado.Empresa = new ML.Empresa();

            ML.Result result = new ML.Result();
            ML.Result resultEmpresas = new ML.Result();
            ML.Result resultDependiente = new ML.Result();
            ML.Result resultDependienteTipo = new ML.Result();


            resultEmpresas = BL.Empresa.GetAllEF(empresa);
            resultDependienteTipo = BL.DependienteTipo.GetAll();

            dependiente.Empleado.NumeroEmpleado = NumeroDeEmpleado;
            empleado.NumeroEmpleado = NumeroDeEmpleado;
            result = BL.Empleado.GetById(empleado);
            
            if (result.Correct == true)
            {
                empleado = (ML.Empleado)result.Object;
                dependiente.Empleado = empleado;
                resultDependiente = BL.Dependiente.GetById(dependiente);

                if (resultDependiente.Correct == true)
                {
                    dependiente.Dependientes = resultDependiente.Objects;
                    dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                }
                else
                {
                    ViewBag.Message = "OCURRIO EL SIGUIENTE ERROR " + resultDependiente.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Message = "Ocurrio el siguiente error: " + result.ErrorMessage;
            }
            dependiente.Empleado.Empresa.Empresas = resultEmpresas.Objects;
            
            return View(dependiente);
            
        }

        [HttpGet]
        public ActionResult Form(int? IdDependiente, string NumeroDeEmpleado)
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return PartialView();
        }
    }
}
