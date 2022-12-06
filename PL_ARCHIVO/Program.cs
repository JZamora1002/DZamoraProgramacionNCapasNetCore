// See https://aka.ms/new-console-template for more information
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;

ReadFile();

static void ReadFile()
{
	ML.Result result = new ML.Result();
    ML.Usuario usuario = new ML.Usuario();
    string fileInsert = @"C:\Users\digis\OneDrive\Documents\Javier David Zamora Garcia\Bloc De Notas\LayoutUsuarios.txt";
    string fileError = @"C:\Users\digis\OneDrive\Documents\Javier David Zamora Garcia\Bloc De Notas\ErrorFile.txt";
    StreamReader TextFile = new StreamReader(fileInsert);
    StreamWriter streamWriter = new StreamWriter(fileError, true, Encoding.ASCII);
    if (File.Exists(fileInsert) ==true)
	{
		
        string line, line2;
		line = TextFile.ReadLine();
		
		while ((line=TextFile.ReadLine())!=null)
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
				Console.WriteLine("Usuario "+usuario.Nombre+"  Ha sido registrado Exitosamente");
				streamWriter.WriteLine("NINGUN ERROR: TODO EXITOSO");
				
			}
			else
			{
				Console.WriteLine(result.ErrorMessage);
				streamWriter.WriteLine("ERROR CAUSADO: " + result.ErrorMessage);
                

            }
        }
        if (result.Correct == true)
        {
            Console.WriteLine("EJECUCION REALIZADA CON EXITO");
            streamWriter.WriteLine("NINGUN ERROR: TODO EXITOSO");
            streamWriter.Close();

        }
        else
        {
            result = BL.Usuario.AddEF(usuario);
            Console.WriteLine(result.ErrorMessage);
            streamWriter.WriteLine("ERROR CAUSADO: " + result.ErrorMessage);
            streamWriter.Close();

        }
    }
	else
	{
        
        streamWriter.WriteLine("NO HAY ARCHIVO DISPONIBLE");
        streamWriter.Close();

    }
}