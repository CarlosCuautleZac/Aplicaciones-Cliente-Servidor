using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VotacionXamarin.Models;

namespace VotacionXamarin.Services
{
    internal class VotacionCliente
    {
        HttpClient cliente = new HttpClient();

        public VotacionCliente()
        {
            cliente.BaseAddress = new Uri("https://e0ab-187-209-255-62.ngrok.io/votacion/");
        }

        public async Task<Pregunta> GetPregunta()
        {
            HttpResponseMessage responseMessage = await cliente.GetAsync("/pregunta");
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();
                Pregunta p = JsonConvert.DeserializeObject<Pregunta>(response);
                return p;
            }
            else
            {
                return null;
            }
        }

        public async void Votar(int opcion)
        {
            var response = await cliente.GetAsync("/responder/?voto=" + opcion);
            response.EnsureSuccessStatusCode();
        }
    }
}
