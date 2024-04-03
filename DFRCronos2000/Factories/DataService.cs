using DFRCronos2000.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace DFRCronos2000.Factories;


public class DataService
{
    private readonly SqlConnection _connexion;
    public DataService()
    {
        SqlConnectionStringBuilder builder = new() // initalise la connexion � la base de donn�es
        {
            DataSource = "localhost",
            UserID = "sa",
            Password = "Info76240#",
            InitialCatalog = "Cronos2000_db"
        };
        _connexion = new SqlConnection(builder.ConnectionString);
    }


    public List<Utilisateur> GetPersonnes() // recup�re la liste de tout les utilisateur
    {
        String procedure = "GetPersonnes"; // nom de la proc�dure stock�e
        List<Utilisateur> values;


        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) 
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            using (SqlDataReader reader = command.ExecuteReader()) // on execute la commande
            {
                values = reader.Cast<IDataRecord>().Select(r => new Utilisateur  // on recup�re les valeurs de la base de donn�es

                {
                    IdUtil = r["IdUtil"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string,
                    Matricule = r["Matricule"] as string,
                    RoleUtil = new Role
                    {
                        IdRole = r["IdRole"] as int?,
                        Libelle = r["LibelleRole"] as string
                    }
                }).ToList();
            }
        }
        _connexion.Close();
        return values;
    }

    public List<Role> GetRoles() // recup�re la liste de tout les roles
    {
        String procedure = "GetRoles"; // nom de la proc�dure stock�e
        List<Role> values;


        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) 
        {
            command.CommandType = CommandType.StoredProcedure;  // on indique que c'est une proc�dure stock�e
            using (SqlDataReader reader = command.ExecuteReader())  // on execute la commande
            {
                values = reader.Cast<IDataRecord>().Select(r => new Role // on recup�re les valeurs de la base de donn�es

                {
                    IdRole = r["IdRole"] as int?,
                    Libelle = r["Libelle"] as string
                }).ToList();
            }
        }
        _connexion.Close();
        return values;
    }

    public Utilisateur GetPersonne(int id) // recup�re un utilisateur en fonction de son id
    {
        String procedure = "GetPersonne"; // nom de la proc�dure stock�e
        Utilisateur value = null;
        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@IdUtil", id); // on ajoute le param�tre ID
            using (SqlDataReader reader = command.ExecuteReader()) // on execute la commande
            {

                value = reader.Cast<IDataRecord>().Select(r => new Utilisateur // on recup�re les valeurs de la base de donn�es
                {
                    IdUtil = r["IdUtil"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string,
                    Matricule = r["Matricule"] as string,
                    RoleUtil = new Role
                    {
                        IdRole = r["IdRole"] as int?,
                        Libelle = r["LibelleRole"] as string
                    }
                }).FirstOrDefault();
            }
        }
        _connexion.Close();
        return value;
    }

    public Utilisateur GetPersonne(string matricule) // recup�re un utilisateur en fonction de son matricule
    {
        String procedure = "GetPersonneMatricule"; // nom de la proc�dure stock�e
        Utilisateur value = null;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@Matricule", matricule); // on ajoute le param�tre Matricule
            using (SqlDataReader reader = command.ExecuteReader())  // on execute la commande
            {
                value = reader.Cast<IDataRecord>().Select(r => new Utilisateur  // on recup�re les valeurs de la base de donn�es
                {
                    IdUtil = r["IdUtil"] as int?,
                    Nom = r["Nom"] as string,
                    Prenom = r["Prenom"] as string,
                    Matricule = r["Matricule"] as string,
                    Mdp = r["MDP"] as string,
                    RoleUtil = new Role
                    {
                        IdRole = r["IdRole"] as int?
                    }
                }).FirstOrDefault();
            }
        }
        _connexion.Close();
        return value;
    }

    public bool CreatePersonne(Utilisateur personne) // cr�e un utilisateur
    {
        String procedure = "CreatePersonne"; // nom de la proc�dure stock�e
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@Nom", personne.Nom); // on ajoute le param�tre Nom
            command.Parameters.AddWithValue("@Prenom", personne.Prenom); // on ajoute le param�tre Prenom
            command.Parameters.AddWithValue("@Matricule", personne.Matricule); // on ajoute le param�tre Matricule
            command.Parameters.AddWithValue("@MDP", personne.Mdp); // on ajoute le param�tre MDP
            command.Parameters.AddWithValue("@IdRole", personne.RoleUtil.IdRole); // on ajoute le param�tre IdRole

            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }



    public bool UpdatePersonne(Utilisateur personne) // met � jour un utilisateur
    {
        String procedure = "UpdatePersonne"; // nom de la proc�dure stock�e
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@Id", personne.IdUtil); // on ajoute le param�tre ID
            command.Parameters.AddWithValue("@Nom", personne.Nom); // on ajoute le param�tre Nom
            command.Parameters.AddWithValue("@Prenom", personne.Prenom); // on ajoute le param�tre Prenom
            command.Parameters.AddWithValue("@Matricule", personne.Matricule); // on ajoute le param�tre Matricule
            command.Parameters.AddWithValue("@IdRole", personne.RoleUtil.IdRole); // on ajoute le param�tre IdRole

            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool UpdatePersonneMDP(Utilisateur personne) // met � jour le mot de passe d'un utilisateur
    {
        String procedure = "UpdatePersonneMDP"; // nom de la proc�dure stock�e
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@Id", personne.IdUtil); // on ajoute le param�tre ID
            command.Parameters.AddWithValue("@MDP", personne.Mdp); // on ajoute le param�tre MDP

            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool DeletePersonne(int id) // supprime un utilisateur
    {
        String procedure = "DeletePersonne"; // nom de la proc�dure stock�e
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande    
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@Id", id); // on ajoute le param�tre ID
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }
    // Partie Pointages
    public List<PointageData> GetPointagesUtil(int id) // recup�re la liste de tout les pointages d'un utilisateur
    {
        String procedure = "GetPointagesUtil"; // nom de la proc�dure stock�e
        List<PointageData> values;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))  
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@IdUtil", id); // on ajoute le param�tre ID
            using (SqlDataReader reader = command.ExecuteReader()) // on execute la commande
            {
                values = reader.Cast<IDataRecord>().Select(r => new PointageData // on recup�re les valeurs de la base de donn�es
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

    public PointageData GetPointageOuvertUtil(int id) // recup�re le pointage ouvert d'un utilisateur
    {
        String procedure = "GetPointageOuvertUtil"; // nom de la proc�dure stock�e
        PointageData value;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@IdUtil", id); // on ajoute le param�tre ID
            using (SqlDataReader reader = command.ExecuteReader())
            {
                value = reader.Cast<IDataRecord>().Select(r => new PointageData 
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

    public bool UpdatePointage(int id, DateTime date) // met � jour un pointage
    {
        String procedure = "UpdatePointage"; // nom de la proc�dure stock�e
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@Id", id); // on ajoute le param�tre ID
            command.Parameters.AddWithValue("@Sortie", date); // on ajoute le param�tre Sortie
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool CreatePointage(int id, DateTime dateDebut) // cr�e un pointage
    {
        String procedure = "CreatePointage"; // nom de la proc�dure stock�e
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une proc�dure stock�e
            command.Parameters.AddWithValue("@IdUtil", id); // on ajoute le param�tre ID
            command.Parameters.AddWithValue("@Arrivee", dateDebut);     // on ajoute le param�tre Arrivee
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }
}