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
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        EditText txtNuevoUsuario;
        EditText txtNuevaClaveUsuario;
        Button btnRegisterUser;
        Button btnCancel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);
            // Create your application here
            txtNuevoUsuario = FindViewById<EditText>(Resource.Id.txtNewUserName);
            txtNuevaClaveUsuario = FindViewById<EditText>(Resource.Id.txtNewPassword);
            btnRegisterUser = FindViewById<Button>(Resource.Id.btnRegister);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);

            btnCancel.Click += BtnCancel_Click;
            btnRegisterUser.Click += BtnRegisterUser_Click;
        }

        private void BtnRegisterUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNuevoUsuario.Text.Trim()) && !string.IsNullOrEmpty(txtNuevaClaveUsuario.Text.Trim()))
                {
                    new Auxiliar().Guardar(new Login()
                    {
                        Id = 0,
                        UserName = txtNuevoUsuario.Text.Trim(),
                        Password = txtNuevaClaveUsuario.Text.Trim()
                    });
                    Toast.MakeText(this, "Registro guardado", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Por favor ingrese un nombre de usuario y una clave", ToastLength.Long).Show();
                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
    }
}