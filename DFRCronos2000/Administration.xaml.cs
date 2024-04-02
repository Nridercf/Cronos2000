using DFRCronos2000.Factories;
using DFRCronos2000.Models;

namespace DFRCronos2000;

public partial class Administration : ContentPage
{
    private readonly DataService _dataService = new();
    public List<Utilisateur> utilisateurs { get; set; }

    public Administration()
    {
        InitializeComponent();
        utilisateurs = _dataService.GetPersonnes();
        BindingContext = this;

    }
    private void OnEditClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        int id = (int)button.CommandParameter;
        var utilisateurAModif = new ModifieUtilisateur(id);
        Navigation.InsertPageBefore(utilisateurAModif, this);
        Navigation.PopAsync();
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        int id = (int)button.CommandParameter;
        _dataService.DeletePersonne(id);
        utilisateurs = _dataService.GetPersonnes();
        BindingContext = this;
    }

    private void OnAjouteClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AjoutUtilisateur());
    }
}