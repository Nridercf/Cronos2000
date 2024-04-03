using DFRCronos2000.Factories;
using DFRCronos2000.Models;

namespace DFRCronos2000;

public partial class ModifieUtilisateur : ContentPage
{
	private Utilisateur modifPersonne;
    public readonly DataService _dataService = new();
    private List<Role> _roles;
    public ModifieUtilisateur(int idPersonne)
	{
		InitializeComponent();
		modifPersonne = _dataService.GetPersonne(idPersonne); // Récupère la personne à modifier
        EntryNom.Text = modifPersonne.Nom;
        EntryPrenom.Text = modifPersonne.Prenom;
        EntryMatricule.Text = modifPersonne.Matricule;
        _roles = _dataService.GetRoles(); // Récupère les rôles
        PickerRole.ItemsSource = _roles;// Ajoute les rôles dans le picker
        PickerRole.ItemDisplayBinding = new Binding("Libelle"); // Affiche le libellé du rôle
        PickerRole.SelectedItem = _roles.Where(r => r.IdRole == modifPersonne.RoleUtil.IdRole).FirstOrDefault(); // Sélectionne le rôle de la personne
        BindingContext = this;// Lie le contexte de la page
	}

    private void ValidationModification(object sender, EventArgs e)// Valide la modification de la personne
    {
        modifPersonne.Nom = EntryNom.Text;
		modifPersonne.Prenom = EntryPrenom.Text;
		modifPersonne.Matricule = EntryMatricule.Text;
        modifPersonne.RoleUtil = (Role)PickerRole.SelectedItem;
        _dataService.UpdatePersonne(modifPersonne);// Met à jour la personne
        this.Navigation.PopAsync();// Retourne à la page précédente
    }


}