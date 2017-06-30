using Android.App;
using Android.Widget;
using Android.OS;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Theme = "@android:style/Theme.Holo", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly System.Collections.Generic.List<string> LoginData = new System.Collections.Generic.List<string>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            var buttonValida = FindViewById<Button>(Resource.Id.buttonValida);
            var Correo = FindViewById<EditText>(Resource.Id.editTextCorreo);
            var Pass = FindViewById<EditText>(Resource.Id.editTextPass);

            buttonValida.Click += (sender, e) =>
            {
                LoginData.Add(Correo.Text);
                LoginData.Add(Pass.Text);
                var Intent = new Android.Content.Intent(this, typeof(ValidatorActivity));
                Intent.PutStringArrayListExtra ("LoginData", LoginData);
                StartActivity(Intent);
            };
        }
    }
}

