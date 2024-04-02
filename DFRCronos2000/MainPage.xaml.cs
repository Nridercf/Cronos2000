using DFRCronos2000;
using DFRCronos2000.Factories;
using Plugin.Maui.Audio;
using FileSystem = Microsoft.Maui.Storage.FileSystem;

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

        private async void MonImage_Tapped(object sender, EventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(2);
            string musique;
            if (randomNumber == 0)
            {
                musique = "summerplace.mp3";
            }
            else
            {
                musique = "walibi.mp3";
            }
            var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(musique));
            audioPlayer.Play();
        }
    }

}
