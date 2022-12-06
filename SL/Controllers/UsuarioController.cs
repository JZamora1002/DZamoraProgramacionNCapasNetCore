using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet ("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            ML.Result resultRoles = new ML.Result();
            resultRoles = BL.Rol.GetAllEF();
            if (result.Correct == true)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.Roles = resultRoles.Objects;
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost ("Busqueda")]
        public IActionResult GetAllB([FromBody] ML.Busqueda busqueda)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Nombre = busqueda.Nombre;
            usuario.ApellidoPaterno = busqueda.ApellidoPaterno;
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = busqueda.IdRol;

            ML.Result resultRoles = new ML.Result();
            resultRoles = BL.Rol.GetAllEF();
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            
            if (result.Correct == true)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.Roles = resultRoles.Objects;
            }
            else
            {
                
                usuario.Rol.Roles = resultRoles.Objects;
            }
            return Ok(usuario);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("GetById/{IdUsuario}")]
        public IActionResult Get(int? IdUsuario)
        {
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

            ML.Result result = new ML.Result();
            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

            if (IdUsuario == null)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                usuario.Rol.Roles = resultRoles.Objects;
                return Ok(usuario);
            }
            else
            {
                usuario.IdUsuario = IdUsuario.Value;
                result = BL.Usuario.GetByIdEF(usuario);

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
                    return NotFound(usuario);
                }

                usuario.Rol.Roles = resultRoles.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                return Ok(usuario);
            }
        }

        // POST api/<UsuarioController>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
           
            result = BL.Usuario.AddEF(usuario);
            if (result.Correct == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            
            
            result = BL.Usuario.UpdateEF(usuario);
            if (result.Correct == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("Delete/{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario == 0)
            {
                return NotFound();
            }
            else
            {
                usuario.IdUsuario = IdUsuario;
                result = BL.Usuario.DeleteEF(usuario);
                if (result.Correct == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
