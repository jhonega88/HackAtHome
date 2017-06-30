using Android.App;
using Android.OS;
using Android.Widget;
using HackAtHome.CustomAdapters;
using HackAtHome.Entities;
using HackAtHome.SAL;
using System.Collections.Generic;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", Theme = "@android:style/Theme.Holo", MainLauncher = false, Icon = "@drawable/icon")]
    public class ValidatorActivity : Activity
    {
        Complex Data;
        ListView ListEvidence;
        IList<string> LoginData;
        TextView textViewAlumno;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Evidencias);
            LoginData = Intent.Extras.GetStringArrayList("LoginData") ?? new string[0];
            textViewAlumno = FindViewById<TextView>(Resource.Id.textViewAlumno);
            ListEvidence = FindViewById<ListView>(Resource.Id.listView1);

            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");
            if (Data == null)
            {
                Data = new Complex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
                Validate(LoginData[0], LoginData[1]);
            }
            else
            {
                //var adapter = new EvidencesAdapter(this, Data.Lista, Resource.Layout.ListItem, Resource.Id.textViewEvidencia, Resource.Id.textViewEstado);
                ListEvidence.Adapter = Data.Adapter;
                textViewAlumno.Text = Data.Alumno;
            }

            if (savedInstanceState != null)
            {
                //ListEvidence.Adapter.se = savedInstanceState.GetInt("ListEvidencePosition", 0);
            }

            ListEvidence.ItemClick += (sender, e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(DescribeLabActivity));
                //var ItemPosition = ListEvidence.CheckedItemPosition;
                //EvidencesAdapter CA = new EvidencesAdapter(this, Data.Lista, Resource.Layout.ListItem, Resource.Id.textViewEvidencia, Resource.Id.textViewEstado);
                int Item = (int)Data.Adapter.GetItemId((int)e.Position);
                //Evidence
                Evidence evidences = Data.Lista[e.Position];

                Intent.PutExtra("ItemPosition", Item);
                Intent.PutExtra("Token", Data.Token);
                Intent.PutExtra("Alumno", Data.Alumno);
                Intent.PutExtra("Title", evidences.Title);
                Intent.PutExtra("Status", evidences.Status);
                StartActivity(Intent);
            };

            
        }

        private async void Validate(string StudentEmail, string Password)
        {
            LabItem LabItem = new LabItem
            {
                Email = LoginData[0],
                Lab = "Hack@Home",
                DeviceId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId)
                //,Id = EventID
            };

            var ServiceClient = new ServiceClient();

            var Result = await ServiceClient.AutenticateAsync(StudentEmail, Password);
            //ListEvidence = FindViewById<ListView>(Resource.Id.listView1);
            Data.Alumno = Result.FullName;
            Data.Token = Result.Token;
            Data.Lista = await ServiceClient.GetEvidencesAsync(Result.Token);

            ListEvidence.Adapter = new EvidencesAdapter(this, Data.Lista, Resource.Layout.ListItem, Resource.Id.textViewEvidencia, Resource.Id.textViewEstado);
            Data.Adapter = ListEvidence.Adapter;
            textViewAlumno.Text = Result.FullName;

            var MicrosoftServiceClient = new MicrosoftServiceClient();
            await MicrosoftServiceClient.SendEvidence(LabItem);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("ListEvidencePosition", ListEvidence.LastVisiblePosition);
            base.OnSaveInstanceState(outState);
        }
    }
}