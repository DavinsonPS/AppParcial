using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppParcial
{
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        EditText txtId;
        EditText txtName;
        EditText txtDescription;
        Button btnConsult;
        Toolbar toolbarmenu;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);
            // Create your application here
            toolbarmenu = FindViewById<Toolbar>(Resource.Id.toolbarMenu);
            txtId = FindViewById<EditText>(Resource.Id.txtId);
            txtName = FindViewById<EditText>(Resource.Id.txtName);
            txtDescription = FindViewById<EditText>(Resource.Id.txtDescription);
            btnConsult = FindViewById<Button>(Resource.Id.btnConsul);

            btnConsult.Click += BtnConsult_Click;
            SetActionBar(toolbarmenu);
            ActionBar.Title = "Menu";
        }

        private void BtnConsult_Click(object sender, EventArgs e)
        {
            string urlServicio = "https://jsonplaceholder.typicode.com/posts";
            try
            {
                Cliente cliente = new Cliente();
                if (!string.IsNullOrEmpty(txtId.Text))
                {
                    int id = 0;
                    if (int.TryParse(txtId.Text.Trim(), out id))
                    {
                        var resultado =  cliente.Get<Entrada>(urlServicio + "/" + txtId.Text);
                        if (cliente.codigoHTTP == 200)
                        {
                            txtName.Text = resultado.Result.title;
                            txtDescription.Text = resultado.Result.body;
                            Toast.MakeText(this, "Consulta realizada con exito ", ToastLength.Long).Show();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long).Show();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.idHome:
                    SetContentView(Resource.Layout.Home);
                    break;
                case Resource.Id.idRegister:
                    SetContentView(Resource.Layout.Register);
                    break;
                case Resource.Id.idUsers:
                    SetContentView(Resource.Layout.Users);
                    break;

                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}