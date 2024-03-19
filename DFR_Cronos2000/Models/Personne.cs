namespace DFRCronos2000.Models;

public class Personne
{
    public int? IdUtil { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Matricul { get; set; }
    public string MDP { get; set; }

    public int IdRole { get; set; }
}