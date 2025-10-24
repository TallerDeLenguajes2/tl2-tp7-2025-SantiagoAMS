using models;
public class ProductoRepository
{
    private string _connectionString = "Data Source=DB/tienda.db";

    private SqliteConnection ConnectAndEnsureTable()
    {
        // Basicamente hace la conexion, y se asegura de la existencia de la tabla
        var con = new SqliteConnection(_connectionString);
        con.Open();
        string createTableQuery = "CREATE TABLE IF NOT EXISTS productos (idProducto INTEGER PRIMARY KEY UNIQUE NOT NULL, Descripcion TEXT(100), precio NUMERIC(10,2))";
        using (SqliteCommand createTableCmd = new SqliteCommand(createTableQuery, connection))
        {
            createTableCmd.ExecuteNonQuery();
        }

    }

    public void Crear(Producto p)
    {
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "INSERT INTO Productos (Descripcion, Precio) VALUES(@desc, @prec); SELECT last_insert_rowid();";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Paremeters.AddWithValue("@desc", p.Descripcion);
                cmd.Paremeters.AddWithValue("@prec", p.Precio);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }


    public void Modificar(int id, Producto p)
    {
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "UPDATE Productos SET Descripcion = @desc, Precio = @prec WHERE idProducto = @id";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Paremeters.AddWithValue("@desc", P.Descripcion);
                cmd.Paremeters.AddWithValue("@prec", P.Precio);
                cmd.Paremeters.AddWithValue("@id", P.IdProducto);
                cmd.ExecuteNonQuery();
            }
        }
    }
    public List<Producto> Listar()
    {
        List<Producto> ret = [];
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "SELECT idProducto, Descripcion, Precio FROM Productos";
            using (var cmd = new SqliteCommand(sql, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Producto p = new Producto(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetDecimal(2)
                        );
                        ret.Add(p);
                    }
                }
            }
            return ret;
        }
    }

    public Producto Obtener(int id)
    {
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "SELECT idProducto, Descripcion, Precio FROM Productos WHERE idProducto = @id";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Paremeters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Producto(
                            reader.GetInt32(0),
                            reader.GetString(reader.getOrdinal("Descripcion")),
                            reader.GetDecimal(2)
                        );
                    }
                }
            }
        }
        return null;
    }

    public void Borrar(int id)
    {
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "DELETE from Producto where idProducto = @id";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Paremeters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
