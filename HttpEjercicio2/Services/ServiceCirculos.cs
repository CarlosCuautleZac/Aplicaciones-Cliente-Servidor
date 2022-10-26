using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpEjercicio2.Services
{
    public class ServiceCirculos
    {
        HttpListener listener = new();

        public ServiceCirculos()
        {

        }

        public event Action<double, int, int, string>? CirculoRecibido;

        public void Iniciar()
        {
            listener.Prefixes.Add("http://*:11010/circulos/");
            listener.Start();
            Thread hiloescuchar = new(new ThreadStart(Escuchar));
            hiloescuchar.IsBackground = true;
            hiloescuchar.Start();

        }

        public void Escuchar()
        {
            while (listener.IsListening)
            {
                var context = listener.GetContext();

                if (context.Request.Url.LocalPath == "/circulos/")
                {
                    byte[] buffer = File.ReadAllBytes("assets/index.html");
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/html";
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);

                }
                else if (context.Request.Url.LocalPath == "/circulos/dibujar")
                {
                    CirculoRecibido?.Invoke(
                       double.Parse(context.Request.QueryString["radio"]),
                       int.Parse(context.Request.QueryString["posX"]),
                       int.Parse(context.Request.QueryString["posY"]),
                       context.Request.QueryString["color"]
                        );

                    byte[] buffer = File.ReadAllBytes("assets/index.html");
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/html";
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }

                //if(context.Request.RawUrl==)
            }
        }
    }
}
