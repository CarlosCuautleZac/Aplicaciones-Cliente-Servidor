using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerEjemplo2
{
    public class ServidorHttp
    {
        HttpListener server = new();

        public event Action<string, string>? MensajeRecibido;

        public void Iniciar()
        {
            server.Prefixes.Add("http://*:3001/notificar/");
            server.Start();

            Thread hilo1 = new(new ThreadStart(RecibirPeticiones));
            hilo1.IsBackground = true;
            hilo1.Start();
        }

        private void RecibirPeticiones()
        {
            while (server.IsListening)
            {
                var context = server.GetContext();
                
                //http://127.0.0.1:3000/notificar?mensaje=hola&titulo=algo

                if(context.Request.Url.LocalPath == "/notificar")
                {
                    var mensaje = context.Request.QueryString["mensaje"];
                    var titulo = context.Request.QueryString["titulo"];
                    if(mensaje!=null && titulo != null)
                    {
                        MensajeRecibido?.Invoke(titulo,mensaje);
                        context.Response.StatusCode = (int)HttpStatusCode.OK; //Todo bien
                    }
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;                      
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;//Not Found
                }
                context.Response.Close();
            }
        }

        public void Detener()
        {
            server.Stop();
        }
    }
}
