using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;

using Didxazapp.webservice;

namespace Didxazapp
{
    [Activity(Label = "Didxazapp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
		webservice.DidxazAppservice tra;
        TextView txt_espanio;
        TextView txt_zapoteco; 
        Button button;
        ProgressDialog progress;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            txt_espanio = FindViewById<TextView>(Resource.Id.Text1);
            txt_zapoteco = FindViewById<TextView>(Resource.Id.Text2);
            button = FindViewById<Button>(Resource.Id.button1);

            Toast.MakeText(this, "aprendezapoteco.blogspot.mx", ToastLength.Long).Show();  

            button.Click += delegate
            {        
                traducir();
            };
        }

        void traducir()
        {
            tra = new webservice.DidxazAppservice();
            if (txt_espanio.Text.Length == 0)
            {
                Toast.MakeText(this, "debes escribir una palabra o frase en español", ToastLength.Long).Show();
            }
            else
            {
                progress = ProgressDialog.Show(this, "Informacion", "Traduciendo...Espere.", true);
                Thread.Sleep(500);
                tra.quierofraseenzapotecoAsync(txt_espanio.Text);
                tra.quierofraseenzapotecoCompleted += tra_quierofraseenzapotecoCompleted;
            }
        }

        void tra_quierofraseenzapotecoCompleted(object sender, quierofraseenzapotecoCompletedEventArgs e)
        {
            try
            {  
                progress.Hide();
                progress.Dismiss();
                txt_zapoteco.Text = e.Result;
            }
            catch (Exception)
            {
                progress.Hide();
                progress.Dismiss();
                Toast.MakeText(this, "Error, asegurese de que tiene conexion a internet.", ToastLength.Long).Show();
            }
        }
    }
}