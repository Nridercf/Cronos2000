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
        PickerRole.ItemsSource = _roles;
        PickerRole.ItemDisplayBinding = new Binding("Libelle");
        BindingContext = this;

    }

    private void ValidationModification(object sender, EventArgs e)
    {
        creePersonne.Nom = EntryNom.Text;
        creePersonne.Prenom = EntryPrenom.Text;
        creePersonne.Matricule = EntryMatricule.Text;
        creePersonne.Mdp = Hashage.Hash(EntryMDP.Text);
        creePersonne.RoleUtil = (Role)PickerRole.SelectedItem;
        _dataService.CreatePersonne(creePersonne);
        this.Navigation.PopAsync();
    }
}