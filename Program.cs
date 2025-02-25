using System;
using System.Data.SqlClient;
using System.Globalization;

class Program
{
    static string connectionString = "data source=DESKTOP-3HP58V7\\SQLEXPRESS;initial catalog=GymControl;trusted_connection=true;";

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Insertar Usuario");
            Console.WriteLine("2. Modificar Usuario");
            Console.WriteLine("3. Salir");
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
                    return;
                default:
                    Console.WriteLine("❌ Opción no válida. Presione Enter para continuar...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void InsertarUsuario()
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

    static void ModificarUsuario()
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

    // Método para validar que la entrada no sea vacía o nula
    static string ValidarEntrada()
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
    static string ValidarFecha()
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
}
