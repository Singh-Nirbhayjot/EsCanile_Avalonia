
using System;
using System.Collections.Generic;
using System.Text;

namespace EsCanile
{
    public class CCanile
    {
        public List<CCane> cani;
        public CCanile(List<CCane> cani)
        { 
            this.cani = cani;
        }
        public void AggiungiCane(CCane c)
        {
            cani.Add(c);
        }
        public string RicercaInfo(string nome)
        {
            foreach(CCane c in cani)
            {
                if(c.Nome == nome)
                {
                    return c.InfoCane();
                }
            }
            return "Cane non trovato";
        }
        public string AggiornaStato(CCane c, Stato s)
        {
            if (c.StatoAttuale == s)
                return "Lo stato attuale del cane è uguale a quello in cui lo si vuole modificare";

            else c.StatoAttuale = s; return $"Stato del cane: {c.Nome} cambiato in {s}";
        }
        public void AdottaCane(CCane c)
        {
            cani.Remove(c);
        }
    }
}
