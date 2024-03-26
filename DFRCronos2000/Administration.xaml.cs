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
        //Modifier nouvellePage = new Modifier(id);
        //Navigation.InsertPageBefore(nouvellePage, this);
        //Navigation.PopAsync();
    }
}