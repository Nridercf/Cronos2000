using Factories.DataService;

namespace DFR_Cronos2000
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            var _dataService = IDataService;
        }

        private void Tentative_Connexion(object sender, EventArgs e)
        {
            string matricule = Matricule.Text;
            string mdp = MDP.Text;

        }
    }

}
