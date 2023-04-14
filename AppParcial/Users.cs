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
    [Activity(Label = "Users")]
    public class Users : Activity
    {
        EditText txtId;
        EditText txtUser;
        EditText txtPassword;
        Button btnConsult;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Users);
            // Create your application here
            txtId = FindViewById<EditText>(Resource.Id.txtId);
            txtUser = FindViewById<EditText>(Resource.Id.txtUser);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPasswordUser);
            btnConsult = FindViewById<Button>(Resource.Id.btnConsul);

            btnConsult.Click += BtnConsult_Click;
        }

        private void BtnConsult_Click(object sender, EventArgs e)
        {
            try
            {
                Login resultado = null;
                if (!string.IsNullOrEmpty(txtId.Text.Trim()))
                {
                    resultado = new Auxiliar().Consulta(int.Parse(txtId.Text.Trim()));
                    if (resultado != null)
                    {
                        txtUser.Text = resultado.UserName.ToString();
                        txtPassword.Text = resultado.Password.ToString();
                        Toast.MakeText(this, "Consulta realizada con exito", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "No se han encontrado datos asociados al Id", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "El campo Id NO debe estar vacio", ToastLength.Long).Show();
                }

            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}