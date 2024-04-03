using DFRCronos2000;
using DFRCronos2000.Factories;

namespace DFRCronos2000
{
    public partial class MainPage : ContentPage
    {
        public readonly DataService _dataService = new();

        public MainPage()
        {
            InitializeComponent(); 
        }

        private void Tentative_Connexion(object sender, EventArgs e)
        {
            string matricule = Matricule.Text;
            string mdp = MDP.Text;

            var personne = _dataService.GetPersonne(matricule);
            if (personne != null)
            {
                var dbMDP = Hashage.Hash(mdp);//Hashage du mot de passe
                var userMDP = personne.Mdp.ToUpper();//Récupération du mot de passe de l'utilisateur
                if (userMDP == dbMDP)//Comparaison des deux mots de passe
                {
                    if (personne.RoleUtil.IdRole == 1)
                    {
                        Navigation.PushAsync(new Administration());
                    }
                    else
                        Navigation.PushAsync(new Pointage(personne));
                }
                else
                {
                    DisplayAlert("Erreur", "Mot de passe incorrect", "OK");
                }
            }
            else
            {
                DisplayAlert("Erreur", "Matricule inconnu", "OK");
            }

        }
    }

}
