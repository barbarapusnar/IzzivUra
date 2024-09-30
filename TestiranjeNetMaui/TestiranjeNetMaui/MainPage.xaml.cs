using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

using System.Net.Http;

namespace TestiranjeNetMaui
{
    public partial class MainPage : ContentPage
    {

        string žeton = "";
        //public static string BaseAddress =
        //    DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7034" : "https://localhost:7034";
        public static string BaseAddress =
           DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7034" : "http://challenger.scng.si";
        public static string up = $"{BaseAddress}/api/Uporabnik/";
        public MainPage()
        {
            InitializeComponent();
        }

        private  async void OnCounterClicked(object sender, EventArgs e)
        {
            //android
//#if DEBUG
            //            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            //            HttpClient klient = new HttpClient(handler.GetPlatformMessageHandler());
            //#else
            //                        HttpClient klient = new HttpClient();
            //#endif
            //windows
            HttpClient klient = new HttpClient();


            Uporabnik u = new Uporabnik();
            u.ime = txtUp.Text;
            u.kodiranoGeslo = txtGeslo.Text;
            Uri uri = new Uri(up);
            try
            {
                string json = JsonSerializer.Serialize<Uporabnik>(u);
                StringContent content = new StringContent(json,Encoding.UTF8,"application/json");

                HttpResponseMessage response = null;
                response = await klient.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {

                    žeton = await response.Content.ReadAsStringAsync();
                    labŽeton.Text = "Prijavljen " + žeton;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error! ",uri+" "+content.ToString(),"OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error! ", uri + " " , "OK");
            }
        }

        private async void btnKlic_Clicked(object sender, EventArgs e)
        {
            //Android
//#if DEBUG
//            HttpsClientHandlerService handler = new HttpsClientHandlerService();
//            HttpClient klient = new HttpClient(handler.GetPlatformMessageHandler());
//#else
//                        HttpClient klient = new HttpClient();
//#endif
           //windows
           HttpClient klient = new HttpClient();
            
            Uri uri = new Uri(up);
            try
            {
                klient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", žeton);
                HttpResponseMessage response = null;
                response = await klient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    string odgovor = await response.Content.ReadAsStringAsync();
                    labOdGet.Text = odgovor;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error! ", uri + " ", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
