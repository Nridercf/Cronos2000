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
        SqlConnectionStringBuilder builder = new() // initalise la connexion à la base de données
        {
            DataSource = "localhost",
            UserID = "sa",
            Password = "Info76240#",
            InitialCatalog = "Cronos2000_db"
        };
        _connexion = new SqlConnection(builder.ConnectionString);
    }


    public List<Utilisateur> GetPersonnes() // recupère la liste de tout les utilisateur
    {
        String procedure = "GetPersonnes"; // nom de la procédure stockée
        List<Utilisateur> values;


        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) 
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            using (SqlDataReader reader = command.ExecuteReader()) // on execute la commande
            {
                values = reader.Cast<IDataRecord>().Select(r => new Utilisateur  // on recupère les valeurs de la base de données

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

    public List<Role> GetRoles() // recupère la liste de tout les roles
    {
        String procedure = "GetRoles"; // nom de la procédure stockée
        List<Role> values;


        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) 
        {
            command.CommandType = CommandType.StoredProcedure;  // on indique que c'est une procédure stockée
            using (SqlDataReader reader = command.ExecuteReader())  // on execute la commande
            {
                values = reader.Cast<IDataRecord>().Select(r => new Role // on recupère les valeurs de la base de données

                {
                    IdRole = r["IdRole"] as int?,
                    Libelle = r["Libelle"] as string
                }).ToList();
            }
        }
        _connexion.Close();
        return values;
    }

    public Utilisateur GetPersonne(int id) // recupère un utilisateur en fonction de son id
    {
        String procedure = "GetPersonne"; // nom de la procédure stockée
        Utilisateur value = null;
        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@IdUtil", id); // on ajoute le paramètre ID
            using (SqlDataReader reader = command.ExecuteReader()) // on execute la commande
            {

                value = reader.Cast<IDataRecord>().Select(r => new Utilisateur // on recupère les valeurs de la base de données
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

    public Utilisateur GetPersonne(string matricule) // recupère un utilisateur en fonction de son matricule
    {
        String procedure = "GetPersonneMatricule"; // nom de la procédure stockée
        Utilisateur value = null;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@Matricule", matricule); // on ajoute le paramètre Matricule
            using (SqlDataReader reader = command.ExecuteReader())  // on execute la commande
            {
                value = reader.Cast<IDataRecord>().Select(r => new Utilisateur  // on recupère les valeurs de la base de données
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

    public bool CreatePersonne(Utilisateur personne) // crée un utilisateur
    {
        String procedure = "CreatePersonne"; // nom de la procédure stockée
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@Nom", personne.Nom); // on ajoute le paramètre Nom
            command.Parameters.AddWithValue("@Prenom", personne.Prenom); // on ajoute le paramètre Prenom
            command.Parameters.AddWithValue("@Matricule", personne.Matricule); // on ajoute le paramètre Matricule
            command.Parameters.AddWithValue("@MDP", personne.Mdp); // on ajoute le paramètre MDP
            command.Parameters.AddWithValue("@IdRole", personne.RoleUtil.IdRole); // on ajoute le paramètre IdRole

            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }



    public bool UpdatePersonne(Utilisateur personne) // met à jour un utilisateur
    {
        String procedure = "UpdatePersonne"; // nom de la procédure stockée
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@Id", personne.IdUtil); // on ajoute le paramètre ID
            command.Parameters.AddWithValue("@Nom", personne.Nom); // on ajoute le paramètre Nom
            command.Parameters.AddWithValue("@Prenom", personne.Prenom); // on ajoute le paramètre Prenom
            command.Parameters.AddWithValue("@Matricule", personne.Matricule); // on ajoute le paramètre Matricule
            command.Parameters.AddWithValue("@IdRole", personne.RoleUtil.IdRole); // on ajoute le paramètre IdRole

            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool UpdatePersonneMDP(Utilisateur personne) // met à jour le mot de passe d'un utilisateur
    {
        String procedure = "UpdatePersonneMDP"; // nom de la procédure stockée
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@Id", personne.IdUtil); // on ajoute le paramètre ID
            command.Parameters.AddWithValue("@MDP", personne.Mdp); // on ajoute le paramètre MDP

            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool DeletePersonne(int id) // supprime un utilisateur
    {
        String procedure = "DeletePersonne"; // nom de la procédure stockée
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion)) // on execute la commande    
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@Id", id); // on ajoute le paramètre ID
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }
    // Partie Pointages
    public List<PointageData> GetPointagesUtil(int id) // recupère la liste de tout les pointages d'un utilisateur
    {
        String procedure = "GetPointagesUtil"; // nom de la procédure stockée
        List<PointageData> values;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))  
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@IdUtil", id); // on ajoute le paramètre ID
            using (SqlDataReader reader = command.ExecuteReader()) // on execute la commande
            {
                values = reader.Cast<IDataRecord>().Select(r => new PointageData // on recupère les valeurs de la base de données
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

    public PointageData GetPointageOuvertUtil(int id) // recupère le pointage ouvert d'un utilisateur
    {
        String procedure = "GetPointageOuvertUtil"; // nom de la procédure stockée
        PointageData value;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@IdUtil", id); // on ajoute le paramètre ID
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

    public bool UpdatePointage(int id, DateTime date) // met à jour un pointage
    {
        String procedure = "UpdatePointage"; // nom de la procédure stockée
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@Id", id); // on ajoute le paramètre ID
            command.Parameters.AddWithValue("@Sortie", date); // on ajoute le paramètre Sortie
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }

    public bool CreatePointage(int id, DateTime dateDebut) // crée un pointage
    {
        String procedure = "CreatePointage"; // nom de la procédure stockée
        bool value = false;

        _connexion.Open();
        using (SqlCommand command = new SqlCommand(procedure, _connexion))
        {
            command.CommandType = CommandType.StoredProcedure; // on indique que c'est une procédure stockée
            command.Parameters.AddWithValue("@IdUtil", id); // on ajoute le paramètre ID
            command.Parameters.AddWithValue("@Arrivee", dateDebut);     // on ajoute le paramètre Arrivee
            value = command.ExecuteNonQuery() > 0;
        }
        _connexion.Close();
        return value;
    }
}