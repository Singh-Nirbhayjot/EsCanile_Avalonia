using System;
using System.Collections.Generic;
using System.Text;

namespace EsCanile
{
    public enum Razza 
    {
        BorderCollie,
        PastoreTedesco,
        Boxer,
        Bassotto,
        Shiba,
        Beagle,
        SetterInglese,
        Maltese,
        LevrieroAfgano
    }
    public enum Stato
    {
        InValutazione,
        Adottabile,
        Adottato
    }
    public class CCane 
    {
        public string Nome { get; set; }
        public int AnnoNascita { get; set; }
        public Razza Razza { get; set; }
        public string InfoUtili { get; set; }
        public Stato StatoAttuale { get; set; }
        public DateTime DataIngresso { get; set; }
        public CCane()
        {
            Nome = "";
            InfoUtili = "";
        }
        public CCane(string n, int a, Razza r, string i, Stato s, DateTime d)
        {
            Nome = n;
            AnnoNascita = a;
            Razza = r;
            InfoUtili = i;
            StatoAttuale = s;
            DataIngresso = d;
        }
        public string InfoCane()
        {
            return $"Nome: {Nome}, Anno di Nascita: {AnnoNascita}, Razza: {Razza}, Data di ingresso: {DataIngresso.ToString("dd/MM/yyyy")}, Stato Attuale: {StatoAttuale}, Info utili per l'adozione: {InfoUtili}";
        }
        public override string ToString()
        {
            return InfoCane();
        }
    }
}
