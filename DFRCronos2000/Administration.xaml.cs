using DFRCronos2000.Factories;
using DFRCronos2000.Models;
using System.Diagnostics;

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

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        int id = (int)button.CommandParameter;
        var unUtilisateur = _dataService.GetPersonne(id);
        var rep = await DisplayAlert("Confirmation", "Êtes-vous sur de vouloir supprimer "+ unUtilisateur.Nom+" "+unUtilisateur.Prenom, "Oui","Non");
        if (rep) 
        {
            _dataService.DeletePersonne(id);
            utilisateurs = _dataService.GetPersonnes();
            BindingContext = this;
            Debug.WriteLine("Utilisateur supprimé");
        }
            
    }

    private void OnAjouteClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AjoutUtilisateur());
    }

    private async void NouvMDPClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        int id = (int)button.CommandParameter;
        var unUtilisateur = _dataService.GetPersonne(id);
        var rep = await DisplayPromptAsync("Reinitailiastion Mot De Passe", "Entrez le nouveau Mot de Passe pour " + unUtilisateur.Nom + " " + unUtilisateur.Prenom);
        if (rep is not null)
        {
            unUtilisateur.Mdp = Hashage.Hash(rep);
            _dataService.UpdatePersonneMDP(unUtilisateur);
            Debug.WriteLine("Mot de passe modifié");
        }
    }
}