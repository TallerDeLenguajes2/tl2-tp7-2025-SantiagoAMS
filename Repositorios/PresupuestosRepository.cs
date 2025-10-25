
using models;
using Microsoft.Data.Sqlite;
public class PresupuestosRepository
{
    private string _connectionString = "Data Source=DB/tienda.db";

    private SqliteConnection ConnectAndEnsureTable()
    {
        // Basicamente hace la conexion, y se asegura de la existencia de la tabla
        var con = new SqliteConnection(_connectionString);
        con.Open();
        string createTableQuery = "CREATE TABLE IF NOT EXISTS Presupuestos (idPresupuesto INTEGER PRIMARY KEY NOT NULL UNIQUE, NombreDestinatario TEXT (100), FechaCreacion DATE )";
        using (SqliteCommand createTableCmd = new SqliteCommand(createTableQuery, con))
        {
            createTableCmd.ExecuteNonQuery();
        }
        return con;

    }


    public void Crear(Presupuesto p)
    {
        using (var cmd = ConnectAndEnsureTable())
        {
            string sql = "";
        }
    }

    public List<Presupuesto> Listar()
    {
        List<Presupuesto> ret = [];
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "SELECT idPresupuesto, NombreDestinatario, FechaCreacion FROM Presupuestos";
            using (var cmd = new SqliteCommand(sql, con))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Presupuesto p = new Presupuesto(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2)
                        );
                        ret.Add(p);
                    }

                }
            }
            return ret;
        }
    }

    public Presupuesto Obtener(int id)
    {
        Presupuesto ret = null;
        List<PresupuestoDetalle> list = [];
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "SELECT * from Presupuestos where idPresupuesto = @idpres";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@idpres", id);
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ret = new Presupuesto(
                                 reader.GetInt32(0),
                                 reader.GetString(1),
                                 reader.GetString(2)
                             );
                    }
                }
            }
            string sql2 = "SELECT pr.idProducto, pr.Descripcion, pr.Precio, pd.Cantidad FROM Productos AS pr, PresupuestosDetalle AS pd WHERE pd.idPresupuesto = @idpres AND pr.idProducto = pd.idProducto";
            using (var cmd = new SqliteCommand(sql2, con))
            {
                cmd.Parameters.AddWithValue("@idpres", id);
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PresupuestoDetalle(
                            new Producto(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetDouble(2)
                            ), reader.GetInt32(3)
                        )
                        );

                    }
                }
            }
            return ret;
        }
    }

    public int Agregar(int id, Producto p, int cantidad)
    {
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "INSERT INTO PresupuestosDetalle (idPresupuesto,idProducto,cantidad) VALUES(@idPre, @idProd, @cant); SELECT last_insert_rowid();";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@idPre", id);
                cmd.Parameters.AddWithValue("@idProd", p.IdProducto);
                cmd.Parameters.AddWithValue("@cant", cantidad);
                return Convert.ToInt32(cmd.ExecuteNonQuery());
            }
        }
    }

    public void Eliminar(int id)
    {
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "DELETE FROM Presupuestos WHERE idPresupuesto = @id; DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@idPre", id);
                cmd.Parameters.AddWithValue("@idPre", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

}