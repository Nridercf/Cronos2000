namespace DFRCronos2000.Models;

public class Utilisateur
{
    public int? IdUtil { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Matricule { get; set; }
    public string Mdp { get; set; }

    public Role? RoleUtil { get; set; }

}