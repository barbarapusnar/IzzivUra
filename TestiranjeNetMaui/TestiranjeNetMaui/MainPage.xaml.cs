using System.Text.Json;
using System.Text;

namespace TestiranjeNetMaui
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }

        private  async void OnCounterClicked(object sender, EventArgs e)
        {
            HttpClient klient = new HttpClient();
            klient.BaseAddress = new Uri("https://localhost:7254/login");
            Uporanik u = new Uporanik();
            u.Ime = txtUp.Text;
            u.HashiranoGeslo = txtGeslo.Text;
            Uri uri = new Uri(string.Format("https://localhost:7254/login", string.Empty));
            try
            {
                string json = JsonSerializer.Serialize<Uporanik>(u);
                StringContent content = new StringContent(json,Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await klient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    
                    string žeton=await response.Content.ReadAsStringAsync();
                    labŽeton.Text="Prijavljen "+ žeton;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
