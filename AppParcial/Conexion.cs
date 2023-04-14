using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppParcial
{

    public class Login
    {
        public Login() { }

        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        [MaxLength(50)]

        public string UserName { get; set; }
        [MaxLength(20)]

        public string Password { get; set; }
    }

    public class Auxiliar
    {
        static object locker = new object();
        SQLiteConnection conexion;

        public Auxiliar()
        {
            conexion = Conectar();
            conexion.CreateTable<Login>();
            
        }

        public SQLite.SQLiteConnection Conectar()
        {
            SQLiteConnection conexionAuxiliar;
            string nombreArchivo = "tdea.db3"; //nombre de la base de datos
            string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string rutaCompleta = Path.Combine(ruta, nombreArchivo);
            conexionAuxiliar = new SQLiteConnection(rutaCompleta);
            return conexionAuxiliar;
        }

        #region Manejo de los datos Login
        //El manejo de los datos
        //Seleccionar todo
        public IEnumerable<Login> SeleccionarTodo()
        {
            lock (locker)
            {
                return (from i in conexion.Table<Login>() select i).ToList();
            }
        }
        //Seleccionar un registro
        public Login SeleccionarUno(string userName, string password)
        {
            lock (locker)
            {
                return conexion.Table<Login>().FirstOrDefault(x => x.UserName == userName && x.Password == password);
            }
        }

        public Login Consulta(int Id)
        {
            lock (locker)
            {
                return conexion.Table<Login>().FirstOrDefault(x => x.Id == Id);
            }
        }

        //Guardar
        //Actualizar o insertar
        public int Guardar(Login registro)
        {
            lock (locker)
            {
                if (registro.Id == 0)
                {
                    return conexion.Insert(registro);
                }
                else
                {
                    return conexion.Update(registro);
                }
            }
        }
        //Eliminar
        public int Eliminar(int ID)
        {
            lock (locker)
            {
                return conexion.Delete<Login>(ID);
            }
        }
        #endregion
              
    }

    public class Entrada
    {
        public Entrada()
        {
            userId = 1;
            id = 0;
            title = "";
            body = "";
        }
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public class Cliente
    {
        public Cliente()
        {
            codigoHTTP = 200;
        }
        public int codigoHTTP { get; set; }

        //Get - Consultar
        public async Task<T> Get<T>(string url)
        {
            HttpClient cliente = new HttpClient();
            var response = await cliente.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            codigoHTTP = (int)response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }

        //Post - Enviar
        public async Task<T> Post<T>(Entrada item, string url)
        {
            HttpClient cliente = new HttpClient();
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json"); //Esta linea nunca cambia, se encuentra en internet

            HttpResponseMessage response = null;
            response = await cliente.PostAsync(url, content);
            json = await response.Content.ReadAsStringAsync();
            codigoHTTP = (int)response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }

        //Put

        //Delete
    }


}