using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Votacion.Models;

namespace Votacion.Services
{
    public class VotacionService
    {
        public event Action<int> VotoRecibido;

        #region Campos    
        private string pregunta ="";
        #endregion

        //Objetos

        HttpListener listener = new();

        //Constructor
        public VotacionService()
        {
            listener.Prefixes.Add("http://*:3506/votacion/");

        }


        #region Metodos
        public void Iniciar()
        {
            if (!listener.IsListening)
            {
                listener.Start();
                listener.BeginGetContext(ContextoRecibido, null);
            }
        }

        public void EstablecerPregunta(Pregunta p)
        {
            pregunta = JsonConvert.SerializeObject(p);
        }

        private void ContextoRecibido(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(ContextoRecibido, null);

            //Atender

            if (context.Request.Url != null)
            {
                if (context.Request.Url.LocalPath == "/votacion/pregunta")
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(pregunta);
                    context.Response.ContentType="application/json"; //MIME
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.StatusCode = 200;
                    context.Response.Close();
                }
                else if (context.Request.Url.LocalPath == "/votacion/responder")//?voto=1
                {
                    if (context.Request.QueryString["voto"] != null)
                    {

                        int voto = int.Parse(context.Request.QueryString["voto"]);
                        context.Response.StatusCode = 200;
                        VotoRecibido?.Invoke(voto); 
                        context.Response.Close();
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                    context.Response.Close();
                }


            }

        }

        #endregion
    }
}
