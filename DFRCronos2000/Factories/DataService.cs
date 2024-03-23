using System.Data;
using System.Data.SqlClient;
using DFRCronos2000.Models;

namespace DFRCronos2000.Factories;

public interface IDataService
{
    List<Personne> GetPersonnes();
    Personne GetPersonne(int id);

    Personne GetPersonne(string matricule);
    bool CreatePersonne(Personne personne);
    bool UpdatePersonne(Personne personne);
    bool DeletePersonne(int id);
}

public class DataService : IDataService
{
    private readonly SqlConnection _connexion;
    public DataService()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = "localhost",
            UserID = "sa",
            Password = "Info76240#",
            InitialCatalog = "Cronos2000_db"
        };
        _connexion = new SqlConnection(builder.ConnectionString);
    }
    
    public List<Uilisateur> GetPersonnes()
    {
        String procedure = "GetPersonnes";
        List<Uilisateur> values;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                values = reader.Cast<IDataRecord>().Select(r => new Uilisateur
                {
                    IdUtil = r["Id"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string
                }).ToList();
            }
        }
        _connexion.Close();
        return values;
    }

    public Uilisateur GetPersonne(int id)
    {
        String procedure = "GetPersonne";
        Uilisateur value = null;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Personne_Id", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                value = reader.Cast<IDataRecord>().Select(r => new Uilisateur
                {
                    IdUtil = r["Id"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string,
                    Matricule = r["Matricule"] as string,
                    Mdp = r["MDP"] as string
                }).FirstOrDefault();
            }
        }
        _connexion.Close();
        return value;
    }

    public Personne GetPersonne(string matricule)
    {
        String procedure = "GetPersonneMatricule";
        Personne value = null;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) 
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Matricule", matricule);
            using (SqlDataReader reader = command.ExecuteReader()) // execute the stored procedure
            {
                value = reader.Cast<IDataRecord>().Select(r => new Personne // convert the result to a Personne object
                {
                    Id = r["IdUtil"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string,
                    Matricule = r["Matricule"] as string,
                    Mdp = r["MDP"] as string
                }).FirstOrDefault();
            }
        }
        _connexion.Close();
        return value;
    }

    public bool CreatePersonne(Uilisateur personne)
    {
        String procedure = "CreatePersonne";
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Nom", personne.Nom);
            command.Parameters.AddWithValue("@Prenom", personne.Prenom);
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool UpdatePersonne(Uilisateur personne)
    {
        String procedure = "UpdatePersonne";
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", personne.IdUtil);
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
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }
}