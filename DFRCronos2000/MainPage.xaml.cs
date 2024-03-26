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
                var dbMDP = Hashage.Hash(mdp);
                var userMDP = personne.Mdp.ToUpper();
                if (userMDP == dbMDP)
                {
                    if (personne.IdRole == 1)
                    {
                        Navigation.PushAsync(new Adminisatration());
                    }
                    else
                        Navigation.PushAsync(new Pointage());
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
