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
    private void OnEditClicked(object sender, EventArgs e) //Ouvre la page de modification de l'utilisateur
    {
        Button button = (Button)sender;
        int id = (int)button.CommandParameter; //R�cup�re l'id de l'utilisateur
        var utilisateurAModif = new ModifieUtilisateur(id); //Cr�e la page de modification de l'utilisateur
        Navigation.InsertPageBefore(utilisateurAModif, this); //Ins�re la page de modification avant la page actuelle
        Navigation.PopAsync(); //Retourne � la page de modification
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        int id = (int)button.CommandParameter; //R�cup�re l'id de l'utilisateur
        var unUtilisateur = _dataService.GetPersonne(id); //R�cup�re l'utilisateur
        var rep = await DisplayAlert("Confirmation", "�tes-vous sur de vouloir supprimer "+ unUtilisateur.Nom+" "+unUtilisateur.Prenom, "Oui","Non"); //Demande confirmation
        if (rep) //Si l'utilisateur confirme
        {
            _dataService.DeletePersonne(id);//Supprime l'utilisateur
            utilisateurs = _dataService.GetPersonnes();//Met � jour la liste des utilisateurs
            BindingContext = this;//Met � jour la page
            Debug.WriteLine("Utilisateur supprim�");
        }
            
    }

    private void OnAjouteClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AjoutUtilisateur()); //Ouvre la page d'ajout d'utilisateur
    }

    private async void NouvMDPClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        int id = (int)button.CommandParameter; //R�cup�re l'id de l'utilisateur
        var unUtilisateur = _dataService.GetPersonne(id); //R�cup�re l'utilisateur
        var rep = await DisplayPromptAsync("Reinitailiastion Mot De Passe", "Entrez le nouveau Mot de Passe pour " + unUtilisateur.Nom + " " + unUtilisateur.Prenom);//Demande le nouveau mot de passe
        if (rep is not null)//Si l'utilisateur a entr� un mot de passe
        {
            unUtilisateur.Mdp = Hashage.Hash(rep);//Hash le mot de passe
            _dataService.UpdatePersonneMDP(unUtilisateur);//Met � jour le mot de passe
            Debug.WriteLine("Mot de passe modifi�");
        }
    }
}