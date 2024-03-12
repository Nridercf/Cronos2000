using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using RSZ_MAUI_Skeleton.Factories;
using RSZ_MAUI_Skeleton.Models;

namespace RSZ_MAUI_Skeleton;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Personne> Personnes { get; set; }
    private readonly IDataService _dataService;

    public MainPage()
    {
        InitializeComponent();
        _dataService = Application.Current!.MainPage!
            .Handler!
            .MauiContext!
            .Services  // IServiceProvider
            .GetService<IDataService>();
        Personnes = new ObservableCollection<Personne>(_dataService.GetPersonnes());
        BindingContext = this;
    }
    
    //add the AddPersonneCommand command button
    public Command AddPersonneCommand => new Command(() =>
    {
        var random = new Random().Next(0, 100);
        Personne personne = new Personne
        {
            Nom = "Nom" + random,
            Prenom = "Prenom" + random
        };
        _dataService.CreatePersonne(personne);
        Personnes.Add(personne);
    });
}