using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ML;
using System.Collections.ObjectModel;
using System.Data;

using DL;
using Microsoft.EntityFrameworkCore;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        //EF
        public static ML.Result AddEF(ML.Usuario usuarioAdd)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var addQuery = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuarioAdd.Nombre}', '{DateTime.ParseExact(usuarioAdd.FechaDeNacimiento, "dd-MM-yyyy", null)}', '{usuarioAdd.NumeroDeTelefono}', '{usuarioAdd.Email}', '{usuarioAdd.UserName}', '{usuarioAdd.ApellidoPaterno}', '{usuarioAdd.ApellidoMaterno}', '{usuarioAdd.Password}', '{usuarioAdd.Sexo}', '{usuarioAdd.Celular}', '{usuarioAdd.CURP}', {usuarioAdd.Rol.IdRol}, '{usuarioAdd.Imagen}','{usuarioAdd.Direccion.Calle}', '{usuarioAdd.Direccion.NumeroInterior}', '{usuarioAdd.Direccion.NumeroExterior}', {usuarioAdd.Direccion.Colonia.IdColonia}");
                    if (addQuery > 0)
                    {
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


        public static ML.Result UpdateEF(ML.Usuario usuarioUpdate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var updateQuery = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuarioUpdate.IdUsuario},'{usuarioUpdate.Nombre}', '{DateTime.ParseExact(usuarioUpdate.FechaDeNacimiento, "dd-MM-yyyy", null)}', '{usuarioUpdate.NumeroDeTelefono}', '{usuarioUpdate.Email}', '{usuarioUpdate.UserName}', '{usuarioUpdate.ApellidoPaterno}', '{usuarioUpdate.ApellidoMaterno}', '{usuarioUpdate.Password}', '{usuarioUpdate.Sexo}', '{usuarioUpdate.Celular}', '{usuarioUpdate.CURP}', {usuarioUpdate.Rol.IdRol}, '{usuarioUpdate.Imagen}','{usuarioUpdate.Direccion.Calle}', '{usuarioUpdate.Direccion.NumeroInterior}', '{usuarioUpdate.Direccion.NumeroExterior}', {usuarioUpdate.Direccion.Colonia.IdColonia}");
                    if (updateQuery > 0)
                    {
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

        public static ML.Result UpdateStatus(ML.Usuario updateStatus)
        {
            ML.Result result = new ML.Result();
            
            try
            {
                
                using (DzamoraProgramacionNcapasContext contex = new DzamoraProgramacionNcapasContext())
                {
                    var updateStatusQuery = contex.Database.ExecuteSqlRaw($"UsuarioUpdateStatus {updateStatus.Status}, {updateStatus.IdUsuario}");

                    if (updateStatusQuery > 0)
                    {
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

        public static ML.Result DeleteEF(ML.Usuario usuarioDelete)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var deleteQuery = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuarioDelete.IdUsuario}");
                    if (deleteQuery > 0)
                    {
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

        public static ML.Result GetAllEF(ML.Usuario usuario1)
        {
            ML.Result result = new ML.Result();
            
            if (usuario1.Nombre == null)
            {
                usuario1.Nombre = "";
            }
            if (usuario1.ApellidoPaterno == null)
            {
                usuario1.ApellidoPaterno = "";
            }
            if (usuario1.Rol.IdRol == null)
            {
                usuario1.Rol.IdRol = 0;
            }
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getAllQuery = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario1.Nombre}', '{usuario1.ApellidoPaterno}', {usuario1.Rol.IdRol}").ToList();

                    result.Objects = new List<object>();
                    if (getAllQuery != null)
                    {
                        foreach (var obj in getAllQuery)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.FechaDeNacimiento = obj.FechaDeNacimiento.ToString("dd-MM-yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.CURP = obj.Curp;
                            usuario.NumeroDeTelefono = obj.NumeroDeTelefono;
                            usuario.Celular = obj.Celular;
                            usuario.Email = obj.Email;
                            usuario.UserName = obj.UserName;
                            usuario.Password = obj.Password;
                            usuario.NombreCompleto = obj.Nombre + " " + obj.ApellidoPaterno + " " + obj.ApellidoMaterno;


                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = Int32.Parse(obj.IdRol.ToString());
                            usuario.Rol.NombreRol = obj.NombreRol;
                            usuario.Imagen = obj.Imagen;
                            usuario.Status = obj.Status;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                            usuario.Direccion.Colonia.NombreColonia = obj.NombreColonia;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.NombreMunicipio = obj.NombreMunicipio;


                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.NombrePais = obj.NombrePais;


                            result.Objects.Add(usuario);
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

        public static ML.Result GetByIdEF(ML.Usuario getByIdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DzamoraProgramacionNcapasContext context = new DzamoraProgramacionNcapasContext())
                {
                    var getByIdQuery = context.Usuarios.FromSql($"UsuarioGetById {getByIdUsuario.IdUsuario}").AsEnumerable().FirstOrDefault();


                    if (getByIdQuery != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = getByIdQuery.IdUsuario;
                        usuario.Nombre = getByIdQuery.Nombre;
                        usuario.ApellidoPaterno = getByIdQuery.ApellidoPaterno;
                        usuario.ApellidoMaterno = getByIdQuery.ApellidoMaterno;
                        usuario.FechaDeNacimiento = getByIdQuery.FechaDeNacimiento.ToString("dd-MM-yyyy");
                        usuario.Sexo = getByIdQuery.Sexo;
                        usuario.CURP = getByIdQuery.Curp;
                        usuario.NumeroDeTelefono = getByIdQuery.NumeroDeTelefono;
                        usuario.Celular = getByIdQuery.Celular;
                        usuario.Email = getByIdQuery.Email;
                        usuario.UserName = getByIdQuery.UserName;
                        usuario.Password = getByIdQuery.Password;
                        usuario.NombreCompleto = getByIdQuery.Nombre + " " + getByIdQuery.ApellidoPaterno + " " + getByIdQuery.ApellidoMaterno;


                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = Int32.Parse(getByIdQuery.IdRol.ToString());
                        usuario.Rol.NombreRol = getByIdQuery.NombreRol;
                        usuario.Imagen = getByIdQuery.Imagen;
                        usuario.Status = getByIdQuery.Status;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = getByIdQuery.IdDireccion;
                        usuario.Direccion.Calle = getByIdQuery.Calle;
                        usuario.Direccion.NumeroExterior = getByIdQuery.NumeroExterior;
                        usuario.Direccion.NumeroInterior = getByIdQuery.NumeroInterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = getByIdQuery.IdColonia;
                        usuario.Direccion.Colonia.CodigoPostal = getByIdQuery.CodigoPostal;
                        usuario.Direccion.Colonia.NombreColonia = getByIdQuery.NombreColonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = getByIdQuery.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.NombreMunicipio = getByIdQuery.NombreMunicipio;


                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = getByIdQuery.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = getByIdQuery.NombreEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = getByIdQuery.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.NombrePais = getByIdQuery.NombrePais;


                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;


            }
            return result;
        }
        public static ML.Result ExcelADataTable(string conexion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(conexion))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = "SELECT * FROM [Hoja1$]";
                        cmd.Connection = context;

                        DataTable tablaPrueba = new DataTable();
                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        adapter.Fill(tablaPrueba);

                        if (tablaPrueba.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaPrueba.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = row[0].ToString();
                                usuario.FechaDeNacimiento = row[1].ToString();
                                usuario.NumeroDeTelefono = row[2].ToString();

                                usuario.Email = row[3].ToString();
                                usuario.UserName = row[4].ToString();
                                usuario.ApellidoPaterno = row[5].ToString();
                                usuario.ApellidoMaterno = row[6].ToString();
                                usuario.Password = row[7].ToString();
                                usuario.Sexo = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = Int32.Parse(row[11].ToString());

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = Int32.Parse(row[15].ToString());
                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                           
                        }
                        
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

        public static ML.Result ValidarExcel(List<object> Objeto)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i = 1;
                foreach (ML.Usuario usuario in Objeto)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar El Nombre";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar El ApellidoPaterno";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "ingresar El ApellidoMaterno";
                    }
                    if (usuario.FechaDeNacimiento == "")
                    {
                        error.Mensaje += "ingresar El FechaDeNacimiento";
                    }
                    if (usuario.Sexo == "")
                    {
                        error.Mensaje += "ingresar El Sexo";
                    }
                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "ingresar El CURP";
                    }
                    if (usuario.NumeroDeTelefono == "")
                    {
                        error.Mensaje += "ingresar El NumeroDeTelefono";
                    }
                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "ingresar El Celular";
                    }
                    if (usuario.Email == "")
                    {
                        error.Mensaje += "ingresar El Email";
                    }
                    if (usuario.UserName == "")
                    {
                        error.Mensaje += "ingresar El UserName";
                    }
                    if (usuario.Password == "")
                    {
                        error.Mensaje += "ingresar El Password";
                    }
                    if (usuario.Rol.IdRol == 0)
                    {
                        error.Mensaje += "ingresar El IdRol";
                    }
                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "ingresar La Calle";
                    }
                    if (usuario.Direccion.NumeroInterior == "")
                    {
                        error.Mensaje += "ingresar El NumeroInterior";
                    }
                    if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "ingresar El NumeroExterior";
                    }
                    if (usuario.Direccion.Colonia.IdColonia == 0)
                    {
                        error.Mensaje += "ingresar El IdColonia";
                    }
                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
