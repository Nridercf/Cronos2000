using DFRCronos2000.Factories;
using DFRCronos2000.Models;

namespace DFRCronos2000;

public partial class Pointage : ContentPage
{
    private readonly DataService _dataService = new();
    public List<Pointage> pointages { get; set; }
    public Pointage(Utilisateur utilisateur)
    {
        InitializeComponent();
        pointages = _dataService.GetPointagesUtil(utilisateur.IdUtil);
        BindingContext = this;
        Test.Text = "Hello Bitch";
    }
}