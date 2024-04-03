using DFRCronos2000.Factories;
using DFRCronos2000.Models;

namespace DFRCronos2000;

public partial class Pointage : ContentPage
{
    private readonly DataService _dataService = new();
    public List<PointageData> Pointages { get; set; }
    public Utilisateur cetteUtilisateur { get; set; }
    public Pointage(Utilisateur utilisateur)
    {
        InitializeComponent();
        Pointages = _dataService.GetPointagesUtil((int)utilisateur.IdUtil); // Récupère les pointages de l'utilisateur
        BindingContext = this; // Lie le contexte de la page
        cetteUtilisateur = utilisateur; // Récupère l'utilisateur
        PointageData pointage = _dataService.GetPointageOuvertUtil((int)cetteUtilisateur.IdUtil); // Récupère le pointage ouvert de l'utilisateur
        if (pointage == null) { Pointer.Text = "Badger en entrée";} else { Pointer.Text = "Badger en sortie";} // Affiche le texte en fonction du pointage
    }

    private void OnPointerClicked(object sender, EventArgs e) // Pointe l'utilisateur
    {
        PointageData pointage = _dataService.GetPointageOuvertUtil((int)cetteUtilisateur.IdUtil); // Récupère le pointage ouvert de l'utilisateur
        DateTime dateHeureActuelle = DateTime.Now; // Récupère la date et l'heure actuelle
        if (pointage == null) {
            bool succes = _dataService.CreatePointage((int)cetteUtilisateur.IdUtil, dateHeureActuelle); // Crée un pointage
            Pointages = _dataService.GetPointagesUtil((int)cetteUtilisateur.IdUtil); // Récupère les pointages de l'utilisateur
            Pointer.Text = "Badger en sortie";// Affiche le texte en fonction du pointage
        } else {
            bool succes = _dataService.UpdatePointage((int)cetteUtilisateur.IdUtil, dateHeureActuelle); // Met à jour le pointage
            Pointages = _dataService.GetPointagesUtil((int)cetteUtilisateur.IdUtil); // Récupère les pointages de l'utilisateur
            Pointer.Text = "Badger en entrée"; // Affiche le texte en fonction du pointage
        }
    }
}