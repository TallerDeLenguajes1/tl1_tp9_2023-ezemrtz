using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
namespace EspacioClaseJson;
partial class Program{
    static void Main(string[] args){
        ObtenerPrecios();
    }
    public static void ObtenerPrecios(){
        var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        Root moneda = null;
        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        moneda = JsonSerializer.Deserialize<Root>(responseBody);
                        Console.WriteLine("Moneda: " + moneda.bpi.USD.description + " Precio: $" + moneda.bpi.USD.rate_float);
                        Console.WriteLine("Moneda: " + moneda.bpi.EUR.description + " Precio: $" + moneda.bpi.EUR.rate_float);
                        Console.WriteLine("Moneda: " + moneda.bpi.GBP.description + " Precio: $" + moneda.bpi.GBP.rate_float);
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
        }
    }
}


