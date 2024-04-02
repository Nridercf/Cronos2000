using DFRCronos2000.Models;
using System.Data;
using System.Data.SqlClient;

namespace DFRCronos2000.Factories;

public interface IDataService
{
    List<Utilisateur> GetPersonnes();
    Utilisateur GetPersonne(int id);

    Utilisateur GetPersonne(string matricule);
    bool CreatePersonne(Utilisateur personne);
    bool UpdatePersonne(Utilisateur personne);
    bool DeletePersonne(int id);

    List<Pointage> GetPointagesUtil(int id);
}

public class DataService
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


    public List<Utilisateur> GetPersonnes()
    {
        String procedure = "GetPersonnes";
        List<Utilisateur> values;


        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // create a new command object with the stored procedure
        {
            command.CommandType = CommandType.StoredProcedure; // set the command type to stored procedure
            using (SqlDataReader reader = command.ExecuteReader()) // execute the stored procedure
            {
                values = reader.Cast<IDataRecord>().Select(r => new Utilisateur // convert the result to a list of Utilisateur objects

                {
                    IdUtil = r["IdUtil"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string,
                    Matricule = r["Matricule"] as string,
                    IdRole = r["IdRole"] as int?
                }).ToList();
            }
        }
        _connexion.Close();
        return values;
    }


    public Utilisateur GetPersonne(int id)
    {
        String procedure = "GetPersonne";
        Utilisateur value = null;
        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Personne_Id", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {

                value = reader.Cast<IDataRecord>().Select(r => new Utilisateur
                {
                    IdUtil = r["Id"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string,
                    Matricule = r["Matricule"] as string,
                    Mdp = r["MDP"] as string,
                    IdRole = r["IdRole"] as int?
                }).FirstOrDefault();
            }
        }
        _connexion.Close();
        return value;
    }

    public Utilisateur GetPersonne(string matricule)
    {
        String procedure = "GetPersonneMatricule";
        Utilisateur value = null;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Matricule", matricule);
            using (SqlDataReader reader = command.ExecuteReader()) // execute the stored procedure
            {
                value = reader.Cast<IDataRecord>().Select(r => new Utilisateur // convert the result to a Utilisateur object
                {
                    IdUtil = r["IdUtil"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string,
                    Matricule = r["Matricule"] as string,
                    Mdp = r["MDP"] as string,
                    IdRole = r["IdRole"] as int?

                }).FirstOrDefault();
            }
        }
        _connexion.Close();
        return value;
    }




    public bool CreatePersonne(Utilisateur personne)
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



    public bool UpdatePersonne(Utilisateur personne)
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
    // Partie Pointages
    public List<PointageData> GetPointagesUtil(int id)
    {
        String procedure = "GetPointagesUtil";
        List<PointageData> values;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // create a new command object with the stored procedure
        {
            command.CommandType = CommandType.StoredProcedure; // set the command type to stored procedure
            command.Parameters.AddWithValue("@IdUtil", id);
            using (SqlDataReader reader = command.ExecuteReader()) // execute the stored procedure
            {
                values = reader.Cast<IDataRecord>().Select(r => new PointageData // convert the result to a list of Pointage objects
                {
                    IdPointage = r["IdPointage"] as int?,
                    IdUtil = r["IdUtil"] as int?,
                    DateHeureArrivee = r["DateHeureArriver"] as DateTime?,
                    DateHeureSortie = r["DateHeureSortie"] as DateTime?,
                }).ToList();
            }
        }
        _connexion.Close();
        return values;
    }

    public PointageData GetPointageOuvertUtil(int id)
    {
        String procedure = "GetPointageOuvertUtil";
        PointageData value;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // create a new command object with the stored procedure
        {
            command.CommandType = CommandType.StoredProcedure; // set the command type to stored procedure
            command.Parameters.AddWithValue("@IdUtil", id);
            using (SqlDataReader reader = command.ExecuteReader()) // execute the stored procedure
            {
                value = reader.Cast<IDataRecord>().Select(r => new PointageData // convert the result to a list of Pointage objects
                {
                    IdPointage = r["IdPointage"] as int?,
                    IdUtil = r["IdUtil"] as int?,
                    DateHeureArrivee = r["DateHeureArriver"] as DateTime?,
                    DateHeureSortie = r["DateHeureSortie"] as DateTime?,
                }).FirstOrDefault();
        }
        }
        _connexion.Close();
        return value;
    }

    public bool UpdatePointage(int id, DateTime date)
    {
        String procedure = "UpdatePointage";
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Sortie", date);
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool CreatePointage(int id, DateTime dateDebut)
    {
        String procedure = "CreatePointage";
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdUtil", id);
            command.Parameters.AddWithValue("@Arrivee", dateDebut);
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }
}