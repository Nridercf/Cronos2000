using System.Data;
using MySqlConnector;
using RSZ_MAUI_Skeleton.Models;

namespace RSZ_MAUI_Skeleton.Factories;

public interface IDataService
{
    List<Personne> GetPersonnes();
    Personne GetPersonne(int id);
    bool CreatePersonne(Personne personne);
    bool UpdatePersonne(Personne personne);
    bool DeletePersonne(int id);
}

public class DataService : IDataService
{
    private readonly MySqlConnection _connexion;
    public DataService()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

        builder.Server = "127.0.0.1";
        builder.UserID = "root";
        builder.Password = "";
        builder.Database = "RSZ_MAUI_SKELETON";

        _connexion = new MySqlConnection(builder.ConnectionString);
    }
    
    public List<Personne> GetPersonnes()
    {
        String procedure = "GetPersonnes";
        List<Personne> values;

        _connexion.Open();
        using (MySqlCommand command = new MySqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                values = reader.Cast<IDataRecord>().Select(r => new Personne
                {
                    Id = r["Id"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string
                }).ToList();
            }
        }
        _connexion.Close();
        return values;
    }

    public Personne GetPersonne(int id)
    {
        String procedure = "GetPersonne";
        Personne value = null;

        _connexion.Open();
        using (MySqlCommand command = new MySqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Personne_Id", id);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                value = reader.Cast<IDataRecord>().Select(r => new Personne
                {
                    Id = r["Id"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string
                }).FirstOrDefault();
            }
        }
        _connexion.Close();
        return value;
    }

    public bool CreatePersonne(Personne personne)
    {
        String procedure = "CreatePersonne";
        bool value = false;

        _connexion.Open();
        using (MySqlCommand command = new MySqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Nom", personne.Nom);
            command.Parameters.AddWithValue("@Prenom", personne.Prenom);
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool UpdatePersonne(Personne personne)
    {
        String procedure = "UpdatePersonne";
        bool value = false;

        _connexion.Open();
        using (MySqlCommand command = new MySqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", personne.Id);
            command.Parameters.AddWithValue("@Nom", personne.Nom);
            command.Parameters.AddWithValue("@Prenom", personne.Prenom);
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool DeletePersonne(int id)
    {
        String procedure = "DeletePersonne";
        bool value = false;

        _connexion.Open();
        using (MySqlCommand command = new MySqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }
}