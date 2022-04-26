using Plugin.Media;
using proj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addPersona : ContentPage
    {
        Utente utente;
        string path = "";
        FileBase file;
        public addPersona()
        {
            utente = new Utente();
            InitializeComponent();
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

        /*public void BtnPhoto(object sender, System.EventArgs e)
        {
            UploadPhoto();
        }

        async void UploadPhoto()
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

        public void Aggiungi(object sender, System.EventArgs e)
        {
            utente.AddVoltoRegistrato(file, Nome.Text.ToString());
            App.Current.MainPage = new SettingsPage();
        }

    }
}
