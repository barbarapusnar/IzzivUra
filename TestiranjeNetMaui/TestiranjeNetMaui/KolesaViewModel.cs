using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestiranjeNetMaui
{
    public partial class KolesaViewModel : BaseViewModel
    {
        public ObservableCollection<Kolo> Kolesa { get; } = new();
        KoloServis koloService;
        public KolesaViewModel(KoloServis koloService)
        {
            Title = "Monkey Finder";
            this.koloService = koloService;
        }

        [RelayCommand]
        async Task GoToDetails(Kolo kolo)
        {
            if (kolo == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetailsPage1), true, new Dictionary<string, object>
        {
            {"Kolo", kolo }
        });
        }

        [RelayCommand]
        async Task GetKolesaAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var kolesa = await koloService.GetKolesa();

                if (Kolesa.Count != 0)
                    Kolesa.Clear();

                foreach (var kolo in kolesa)
                    Kolesa.Add(kolo);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
