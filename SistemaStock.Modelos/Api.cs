using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.Modelos
{
    public class Api
    {

        public string GetCotizacion(string key)
        {
            UriBuilder builder = new UriBuilder("http://api.currencylayer.com/live");
            builder.Query = $"access_key={key}";

            //Create a query
            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;

            using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
            {
                return sr.ReadToEnd();
            }
        }

    }
}
