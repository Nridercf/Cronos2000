using DFRCronos2000.Factories;
using DFRCronos2000.Models;

namespace DFRCronos2000;

public partial class AjoutUtilisateur : ContentPage
{
    private Utilisateur creePersonne = new();
    public readonly DataService _dataService = new();
    public List<Role> _roles;
    public AjoutUtilisateur()
	{
		InitializeComponent();
        _roles = _dataService.GetRoles();
        PickerRole.ItemsSource = _roles; // On ajoute les roles dans le picker
        PickerRole.ItemDisplayBinding = new Binding("Libelle"); // On affiche le libelle des roles
        BindingContext = this; // On lie le contexte de la page à la page

    }

    private void ValidationModification(object sender, EventArgs e)
    {
        if (EntryNom.Text is null || EntryPrenom.Text is null || EntryMatricule.Text is null || EntryMDP.Text is null || PickerRole.SelectedIndex == -1) // On vérifie que les champs sont remplis
        {
            DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }
        creePersonne.Nom = EntryNom.Text;
        creePersonne.Prenom = EntryPrenom.Text;
        creePersonne.Matricule = EntryMatricule.Text;
        creePersonne.Mdp = Hashage.Hash(EntryMDP.Text); // On hash le mot de passe
        creePersonne.RoleUtil = (Role)PickerRole.SelectedItem; // On récupère le role selectionné
        _dataService.CreatePersonne(creePersonne); // On crée la personne
        this.Navigation.PopAsync(); // On revient à la page précédente
    }
}