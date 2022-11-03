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

namespace FotosAlbumEnTiempoReal.Services
{
    public class AlbumService
    {
        HttpListener server = new();

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

                byte[] buffer = File.ReadAllBytes("assets/index.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                
                context.Response.StatusCode = 200;
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            context.Response.Close();

        }
    }
}
