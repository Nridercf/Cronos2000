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
        Pointages = _dataService.GetPointagesUtil((int)utilisateur.IdUtil);
        BindingContext = this;
        cetteUtilisateur = utilisateur;
        PointageData pointage = _dataService.GetPointageOuvertUtil((int)cetteUtilisateur.IdUtil);
        if (pointage == null) { Pointer.Text = "Badger en entrée";} else { Pointer.Text = "Badger en sortie";}
    }

    private void OnPointerClicked(object sender, EventArgs e)
    {
        PointageData pointage = _dataService.GetPointageOuvertUtil((int)cetteUtilisateur.IdUtil);
        DateTime dateHeureActuelle = DateTime.Now;
        if (pointage == null) {
            bool succes = _dataService.CreatePointage((int)cetteUtilisateur.IdUtil, dateHeureActuelle);
            Pointages = _dataService.GetPointagesUtil((int)cetteUtilisateur.IdUtil);
            Pointer.Text = "Badger en sortie";
        } else {
            bool succes = _dataService.UpdatePointage((int)cetteUtilisateur.IdUtil, dateHeureActuelle);
            Pointages = _dataService.GetPointagesUtil((int)cetteUtilisateur.IdUtil);
            Pointer.Text = "Badger en entrée";
        }
    }
}