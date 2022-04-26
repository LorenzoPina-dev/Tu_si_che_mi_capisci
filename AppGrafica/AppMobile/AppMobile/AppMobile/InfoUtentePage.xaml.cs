using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using proj;
using System.IO;

using Xamarin.Forms;
using System.Net.Http;

namespace AppMobile
{
    public partial class InfoUtentePage : ContentPage
    {
        Utente utente;
        string path = "";
        JObject ris;
        FileBase file;
        //ImageView thisImageView;
        public InfoUtentePage()
        {
            utente = new Utente();
            ris = new JObject();
            InitializeComponent();
            Request();
        }

        async void BtnPhotoAsync(object sender, System.EventArgs e)
        {
            //UploadPhoto();
            file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;
            else
                img.Source = file.FullPath;
        }

        /*async void UploadPhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 40

            });

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);

            path = file.Path;
            img.Source = file.Path;
        }*/

        public void Indietro(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new TabbedPage1();
        }

        public void Salva(object sender, System.EventArgs e)
        {
            string password = "";
            string user = Username.Text.ToString();
            string email = Email.Text.ToString();
            if (Pass.Text.ToString().Length > 0)
                password = Pass.Text.ToString();
            else
                password = "";
            utente.ChangeInfo(user, password, email, file);
            App.Current.MainPage = new TabbedPage1();
        }

        public void Request()
        {
            string foto = "";
            ris = (JObject)utente.GetInfo();
            Email.Text = ris["Email"].ToString();
            Username.Text = ris["Username"].ToString();
            foto = ris["Immagine"].ToString();
            img.Source = ImageSource.FromStream(() => new MemoryStream(utente.GetImage("utente", foto)));
        }
    }
}