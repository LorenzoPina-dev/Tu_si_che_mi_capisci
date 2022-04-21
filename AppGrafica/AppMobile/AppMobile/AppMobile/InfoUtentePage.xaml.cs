
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile
{
    public partial class InfoUtentePage : ContentPage
    {
        //ImageView thisImageView;
        public InfoUtentePage()
        {
            InitializeComponent();
        }

        public void BtnPhoto(object sender, System.EventArgs e)
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

            //Convert file to byre array, to bitmap and set it to our ImageView

            /*
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            thisImageView.SetImageBitmap(bitmap);*/
        }
    }
}