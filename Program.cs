using System;
using System.Data.SqlClient;
using System.Globalization;

public class Program
{
    static string connectionString = "data source=DESKTOP-3HP58V7\\SQLEXPRESS;initial catalog=GymControl;trusted_connection=true;";

   public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("🏋️ Menú Principal");
            Console.WriteLine("1. Insertar Usuario");
            Console.WriteLine("2. Modificar Usuario");
            Console.WriteLine("3. Insertar Registro de Medidas");
            Console.WriteLine("4. Modificar Registro de Medidas");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    InsertarUsuario();
                    break;
                case "2":
                    ModificarUsuario();
                    break;
                case "3":
                    InsertarMedidas();
                    break;
                case "4":
                    ModificarMedidas();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("❌ Opción no válida. Presione Enter para continuar...");
                    Console.ReadLine();
                    break;
            }
        }
    
}

   public static void InsertarUsuario()
    {
        Console.Write("Cedula: ");
        string id_usuario = ValidarEntrada();
        Console.Write("Nombre: ");
        string nombre = ValidarEntrada();
        Console.Write("Apellido: ");
        string apellido = ValidarEntrada();
        string fechaNacimiento = ValidarFecha();
        Console.Write("Género (Masculino/Femenino/Otro): ");
        string genero = ValidarEntrada();
        Console.Write("Correo: ");
        string correo = ValidarEntrada();
        Console.Write("Teléfono: ");
        string telefono = ValidarEntrada();
        // prueba 5
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Usuarios (id_usuario,nombre, apellido, fecha_nacimiento, genero, correo, telefono) " +
                           "VALUES (@id_usuario,@nombre, @apellido, @fecha_nacimiento, @genero, @correo, @telefono)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id_usuario", id_usuario);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellido", apellido);
            cmd.Parameters.AddWithValue("@fecha_nacimiento", fechaNacimiento);
            cmd.Parameters.AddWithValue("@genero", genero);
            cmd.Parameters.AddWithValue("@correo", correo);
            cmd.Parameters.AddWithValue("@telefono", telefono);

            conn.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine(filasAfectadas > 0 ? "✅ Usuario insertado correctamente." : "❌ Error al insertar usuario.");
        }
        Console.WriteLine("Presione Enter para continuar...");
        Console.ReadLine();
    }

   public static void ModificarUsuario()
    {
        Console.Write("Ingrese el ID del usuario a modificar: ");
        int idUsuario;
        while (!int.TryParse(Console.ReadLine(), out idUsuario))
        {
            Console.WriteLine("❌ ID no válido. Ingrese un número entero:");
        }

        Console.Write("Nuevo Nombre: ");
        string nombre = ValidarEntrada();
        Console.Write("Nuevo Apellido: ");
        string apellido = ValidarEntrada();
        Console.Write("Nuevo Correo: ");
        string correo = ValidarEntrada();
        Console.Write("Nuevo Teléfono: ");
        string telefono = ValidarEntrada();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "UPDATE Usuarios SET nombre = @nombre, apellido = @apellido, correo = @correo, telefono = @telefono " +
                           "WHERE id_usuario = @id_usuario";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellido", apellido);
            cmd.Parameters.AddWithValue("@correo", correo);
            cmd.Parameters.AddWithValue("@telefono", telefono);
            cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

            conn.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine(filasAfectadas > 0 ? "✅ Usuario actualizado correctamente." : "❌ No se encontró el usuario.");
        }
        Console.WriteLine("Presione Enter para continuar...");
        Console.ReadLine();
    }

    // Métodos para Registro de Medidas
   public static void InsertarMedidas()
    {
        Console.Write("ID del Usuario: ");
        int idUsuario = ValidarEntero();

        string fechaRegistro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Fecha actual

        Console.WriteLine("Ingrese las medidas (en cm):");
        float bicepsDerecho = ValidarDecimal("Bíceps Derecho");
        float bicepsIzquierdo = ValidarDecimal("Bíceps Izquierdo");
        float antebrazoDerecho = ValidarDecimal("Antebrazo Derecho");
        float antebrazoIzquierdo = ValidarDecimal("Antebrazo Izquierdo");
        float musloDerecho = ValidarDecimal("Muslo Derecho");
        float musloIzquierdo = ValidarDecimal("Muslo Izquierdo");
        float pecho = ValidarDecimal("Pecho");
        float hombros = ValidarDecimal("Hombros");
        float gemeloDerecho = ValidarDecimal("Gemelo Derecho");
        float gemeloIzquierdo = ValidarDecimal("Gemelo Izquierdo");

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = @"INSERT INTO Registro_Medidas (id_usuario, fecha_registro, 
                                biceps_derecho, biceps_izquierdo, 
                                antebrazo_derecho, antebrazo_izquierdo, 
                                muslo_derecho, muslo_izquierdo, 
                                pecho, hombros, 
                                gemelo_derecho, gemelo_izquierdo) 
                            VALUES (@id_usuario, @fecha_registro, 
                                @biceps_derecho, @biceps_izquierdo, 
                                @antebrazo_derecho, @antebrazo_izquierdo, 
                                @muslo_derecho, @muslo_izquierdo, 
                                @pecho, @hombros, 
                                @gemelo_derecho, @gemelo_izquierdo)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
            cmd.Parameters.AddWithValue("@fecha_registro", fechaRegistro);
            cmd.Parameters.AddWithValue("@biceps_derecho", bicepsDerecho);
            cmd.Parameters.AddWithValue("@biceps_izquierdo", bicepsIzquierdo);
            cmd.Parameters.AddWithValue("@antebrazo_derecho", antebrazoDerecho);
            cmd.Parameters.AddWithValue("@antebrazo_izquierdo", antebrazoIzquierdo);
            cmd.Parameters.AddWithValue("@muslo_derecho", musloDerecho);
            cmd.Parameters.AddWithValue("@muslo_izquierdo", musloIzquierdo);
            cmd.Parameters.AddWithValue("@pecho", pecho);
            cmd.Parameters.AddWithValue("@hombros", hombros);
            cmd.Parameters.AddWithValue("@gemelo_derecho", gemeloDerecho);
            cmd.Parameters.AddWithValue("@gemelo_izquierdo", gemeloIzquierdo);

            conn.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine(filasAfectadas > 0 ? "✅ Medidas insertadas correctamente." : "❌ Error al insertar medidas.");
        }
        Console.WriteLine("Presione Enter para continuar...");
        Console.ReadLine();
    }

  public  static void ModificarMedidas()
    {
        Console.Write("Ingrese el ID del registro de medidas a modificar: ");
        int idRegistro = ValidarEntero();

        Console.WriteLine("Ingrese las nuevas medidas (en cm):");
        float bicepsDerecho = ValidarDecimal("Bíceps Derecho");
        float bicepsIzquierdo = ValidarDecimal("Bíceps Izquierdo");
        float antebrazoDerecho = ValidarDecimal("Antebrazo Derecho");
        float antebrazoIzquierdo = ValidarDecimal("Antebrazo Izquierdo");
        float musloDerecho = ValidarDecimal("Muslo Derecho");
        float musloIzquierdo = ValidarDecimal("Muslo Izquierdo");
        float pecho = ValidarDecimal("Pecho");
        float hombros = ValidarDecimal("Hombros");
        float gemeloDerecho = ValidarDecimal("Gemelo Derecho");
        float gemeloIzquierdo = ValidarDecimal("Gemelo Izquierdo");

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = @"UPDATE Registro_Medidas SET 
                                biceps_derecho = @biceps_derecho, 
                                biceps_izquierdo = @biceps_izquierdo, 
                                antebrazo_derecho = @antebrazo_derecho, 
                                antebrazo_izquierdo = @antebrazo_izquierdo, 
                                muslo_derecho = @muslo_derecho, 
                                muslo_izquierdo = @muslo_izquierdo, 
                                pecho = @pecho, 
                                hombros = @hombros, 
                                gemelo_derecho = @gemelo_derecho, 
                                gemelo_izquierdo = @gemelo_izquierdo 
                            WHERE id_registro = @id_registro";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id_registro", idRegistro);
            cmd.Parameters.AddWithValue("@biceps_derecho", bicepsDerecho);
            cmd.Parameters.AddWithValue("@biceps_izquierdo", bicepsIzquierdo);
            cmd.Parameters.AddWithValue("@antebrazo_derecho", antebrazoDerecho);
            cmd.Parameters.AddWithValue("@antebrazo_izquierdo", antebrazoIzquierdo);
            cmd.Parameters.AddWithValue("@muslo_derecho", musloDerecho);
            cmd.Parameters.AddWithValue("@muslo_izquierdo", musloIzquierdo);
            cmd.Parameters.AddWithValue("@pecho", pecho);
            cmd.Parameters.AddWithValue("@hombros", hombros);
            cmd.Parameters.AddWithValue("@gemelo_derecho", gemeloDerecho);
            cmd.Parameters.AddWithValue("@gemelo_izquierdo", gemeloIzquierdo);

            conn.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine(filasAfectadas > 0 ? "✅ Registro actualizado correctamente." : "❌ No se encontró el registro.");
        }
        Console.WriteLine("Presione Enter para continuar...");
        Console.ReadLine();
    }

    // Método para validar que la entrada no sea vacía o nula
  public static string ValidarEntrada()
    {
        string entrada;
        do
        {
            entrada = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(entrada))
            {
                Console.WriteLine("⚠ Este campo no puede estar vacío. Inténtelo nuevamente:");
            }
        } while (string.IsNullOrEmpty(entrada));

        return entrada;
    }

    // Método para validar la fecha de nacimiento
  public  static string ValidarFecha()
    {
        string fecha;
        DateTime fechaValida;
        do
        {
            Console.Write("Fecha de nacimiento (YYYY-MM-DD): ");
            fecha = Console.ReadLine()?.Trim();
            if (!DateTime.TryParseExact(fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaValida))
            {
                Console.WriteLine("⚠ Formato incorrecto. Ingrese una fecha válida en formato YYYY-MM-DD.");
            }
        } while (!DateTime.TryParseExact(fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaValida));

        return fecha;
    }



    // Métodos de validación para registro medidas 
    static int ValidarEntero()
    {
        int valor;
        while (!int.TryParse(Console.ReadLine(), out valor) || valor <= 0)
        {
            Console.WriteLine("⚠ Entrada no válida. Ingrese un número entero válido:");
        }
        return valor;
    }

    static float ValidarDecimal(string mensaje)
    {
        float valor;
        Console.Write($"{mensaje}: ");
        while (!float.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out valor) || valor <= 0)
        {
            Console.WriteLine("⚠ Entrada no válida. Ingrese un valor numérico positivo:");
            Console.Write($"{mensaje}: ");
        }
        return valor;
    }
}
