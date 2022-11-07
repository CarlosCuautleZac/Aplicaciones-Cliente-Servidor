using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FotosAlbumEnTiempoReal.Services
{
    public class AlbumService
    {
        HttpListener server = new();
        public event Action<string?> ImagenRecibida;

        public void Start()
        {
            if(!server.IsListening)
            {
                server.Prefixes.Add("http://*:9876/album/");
                server.Start();
                Escuchar();
            }
        }

        void Escuchar()
        {
            Thread peticion = new(new ThreadStart(Atender));
            peticion.IsBackground = true;
            peticion.Start();
        }

        private void Atender()
        {
            var context = server.GetContext();
            Escuchar();


            //Atender
            if (context.Request.Url.LocalPath == "/album/")
            {

                byte[] buffer = File.ReadAllBytes("Assets/index.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);

                context.Response.StatusCode = 200;
                
            }
            else if (context.Request.Url.LocalPath == "/album/imagen" && context.Request.HttpMethod == "POST")
            {
                var stream = new StreamReader(context.Request.InputStream);
                var data = HttpUtility.UrlDecode(stream.ReadToEnd());

                //Tomar todo lo que essta despues del MIME type
                var imageb64 = data.Substring(data.IndexOf(',')+1);
                var buffer = Convert.FromBase64String(imageb64);

                var ruta = $"Assets/imagen_{DateTime.Now.ToString("ddMMyyyyhhhmmmss")}.png";
                File.WriteAllBytes(ruta,buffer);
                ImagenRecibida?.Invoke(ruta);

                context.Response.StatusCode = 200;
                context.Response.Redirect("/album/");
            }

            else
            {
                context.Response.StatusCode = 404;


            }

            context.Response.Close();
        }
    }
}
