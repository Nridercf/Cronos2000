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
		modifPersonne = _dataService.GetPersonne(idPersonne);
        EntryNom.Text = modifPersonne.Nom;
        EntryPrenom.Text = modifPersonne.Prenom;
        EntryMatricule.Text = modifPersonne.Matricule;
        _roles = _dataService.GetRoles();
        PickerRole.ItemsSource = _roles;
        PickerRole.ItemDisplayBinding = new Binding("Libelle");
        PickerRole.SelectedItem = _roles.Where(r => r.IdRole == modifPersonne.RoleUtil.IdRole).FirstOrDefault();
        BindingContext = this;
	}

    private void ValidationModification(object sender, EventArgs e)
    {
        modifPersonne.Nom = EntryNom.Text;
		modifPersonne.Prenom = EntryPrenom.Text;
		modifPersonne.Matricule = EntryMatricule.Text;
        modifPersonne.RoleUtil = (Role)PickerRole.SelectedItem;
        _dataService.UpdatePersonne(modifPersonne);
        this.Navigation.PopAsync();
    }


}