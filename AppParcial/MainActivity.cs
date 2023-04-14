using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace AppParcial
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        EditText txtUsuario;
        EditText txtPassword;
        Button btnLogin;
        Button btnRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            txtUsuario = FindViewById<EditText>(Resource.Id.txtUserName);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;
        }

        private void BtnRegister_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(Register));
            StartActivity(i);
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            try
            {
                Login resultado = null;
                if (!string.IsNullOrEmpty(txtUsuario.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    resultado = new Auxiliar().SeleccionarUno(txtUsuario.Text.Trim(), txtPassword.Text.Trim());
                    if (resultado != null)
                    {
                        txtUsuario.Text = resultado.UserName.ToString();
                        Toast.MakeText(this, "Login realizado con exito", ToastLength.Short).Show();
                        var bienvenido = new Intent(this, typeof(Home));
                        bienvenido.PutExtra("usuario", FindViewById<EditText>(Resource.Id.txtUserName).Text);
                        StartActivity(bienvenido);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "Nombre de usuario y/o clave inválida(s)", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Los campos Nombre de usuario y/o clave estan vacios", ToastLength.Long).Show();
                }

            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}