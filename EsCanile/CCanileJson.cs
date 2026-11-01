using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace EsCanile
{
    public class CCanileJson
    {
        private static string percorso = "cani.json";

        public void Salva(List<CCane> cani)
        {
            string json = JsonSerializer.Serialize(cani);
            File.WriteAllText(percorso, json);
        }

        public List<CCane> Carica()
        {
            if (File.Exists(percorso))
            {
                string json = File.ReadAllText(percorso);

                List<CCane> cani = JsonSerializer.Deserialize<List<CCane>>(json);

                return cani ?? new List<CCane>();
            }

            return new List<CCane>();
        }
    }
}
