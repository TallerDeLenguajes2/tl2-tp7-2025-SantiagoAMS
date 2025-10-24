using models;

public class PresupuestosRepository
{
    private string _connectionString = "Data Source=DB/tienda.db";

    private SqliteConnection ConnectAndEnsureTable()
    {
        // Basicamente hace la conexion, y se asegura de la existencia de la tabla
        var con = new SqliteConnection(_connectionString);
        con.Open();
        string createTableQuery = "CREATE TABLE IF NOT EXISTS Presupuestos (idPresupuesto INTEGER PRIMARY KEY NOT NULL UNIQUE, NombreDestinatario TEXT (100), FechaCreacion DATE )";
        using (SqliteCommand createTableCmd = new SqliteCommand(createTableQuery, connection))
        {
            createTableCmd.ExecuteNonQuery();
        }

    }


    public void Crear(Presupuesto p) {
        using (var cmd = ConnectAndEnsureTable()){
            string sql = "";
        }
    }

    public List<Presupuesto> Listar() {
        List<Presupuesto> ret = [];
        using (SqliteConnection con = ConnectAndEnsureTable())
        {
            string sql = "SELECT idPresupuesto, NombreDestinatario, FechaCreacion FROM Presupuestos";
            using (var cmd = new SqliteCommand(sql, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
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

    public void Obtener(int id)
    {

    }

    public void Agregar(int id)
    {

    }

    public void Eliminar(int id)
    {
        
    }

}