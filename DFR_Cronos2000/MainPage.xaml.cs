using DFRCronos2000;
using DFRCronos2000.Factories;

namespace DFR_Cronos2000
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
                var dbNDP = Hashage.Hash(mdp);
                if (personne.Mdp == dbNDP)
                {
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
