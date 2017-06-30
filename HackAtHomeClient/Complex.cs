using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackAtHome.Entities;

namespace HackAtHomeClient
{
    public class Complex : Fragment
    {
        public int Real { get; set; }
        public string Token { get; set; }

        public string Result { get; set; }
        public string Alumno { get; set; }

        public IListAdapter Adapter { get; set; }

        public List<Evidence> Lista { get; set; }

        public override string ToString()
        {
            return $"{Real}";
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}