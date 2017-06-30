
using Android.App;
using Android.Content;
using Android.OS;
using Android.Webkit;
using Android.Widget;
using HackAtHome.SAL;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", Theme = "@android:style/Theme.Holo", MainLauncher = false, Icon = "@drawable/icon")]
    public class DescribeLabActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Descripcion);
            var ItemPosition = Intent.Extras.GetInt("ItemPosition", 0);
            var Token = Intent.Extras.GetString("Token");
            var Alumno = Intent.Extras.GetString("Alumno");
            var Title = Intent.Extras.GetString("Title");
            var Status = Intent.Extras.GetString("Status");

            var textViewTituloLab = FindViewById<TextView>(Resource.Id.textViewTituloLab);
            textViewTituloLab.Text = Title;

            var textViewEstado = FindViewById<TextView>(Resource.Id.textViewEstado);
            textViewEstado.Text = Status;

            var editTexAlumno = FindViewById<TextView>(Resource.Id.textViewAlumno);
            editTexAlumno.Text = Alumno;
            ConsultaDescripcionLab(Token,ItemPosition);

        }

        private async void ConsultaDescripcionLab(string Token, int Id)
        {
            var ServiceClient = new ServiceClient();

            var Result = await ServiceClient.GetEvidenceByIDAsync(Token, Id);

            var webView = FindViewById<WebView>(Resource.Id.webView1);

            webView.LoadDataWithBaseURL(null, $"<html><head><style type='text/css'>body{{color:#fff}}</style></head><body>{Result.Description}</body></html>", "text/html", "utf-8", null);
            webView.SetBackgroundColor(Android.Graphics.Color.Transparent);


            var ImgView = FindViewById<ImageView>(Resource.Id.imageView1);

            Koush.UrlImageViewHelper.SetUrlDrawable(ImgView,Result.Url);
            //Result.
            //Data.Alumno = Result.FullName;
            //Data.Lista = await ServiceClient.GetEvidencesAsync(Result.Token);

            //ListEvidence.Adapter = new EvidencesAdapter(this, Data.Lista, Resource.Layout.ListItem, Resource.Id.textViewEvidencia, Resource.Id.textViewEstado);
            //textViewAlumno.Text = Result.FullName;

        }
    }
}