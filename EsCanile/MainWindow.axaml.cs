using System;
using System.Reflection.Metadata;
using Avalonia.Controls;

namespace EsCanile;

public partial class MainWindow : Window
{
    private CCanile canile;
    private CCanileJson json;
    private int caniAdottati = 0;
    public MainWindow()
    {
        InitializeComponent();

        cmbRazza.ItemsSource = Enum.GetValues(typeof(Razza));
        cmbStato.ItemsSource = Enum.GetValues(typeof(Stato));

        json = new CCanileJson();
        canile = new CCanile(json.Carica());

        AggiornaLista();
    }
    private void AggiungiCane_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string nome = txtNome.Text ?? "";
        string informazioni = txtInformazioni.Text ?? "";

        int anno;

        if (string.IsNullOrWhiteSpace(nome))
        {
            txtMessaggio.Text = "Inserisci il nome del cane.";
            return;
        }

        if (!int.TryParse(txtAnnoNascita.Text, out anno))
        {
            txtMessaggio.Text = "Inserisci un anno valido.";
            return;
        }

        if (anno < 1900 || anno > DateTime.Now.Year)
        {
            txtMessaggio.Text = "Anno di nascita non valido.";
            return;
        }

        if (cmbRazza.SelectedItem == null)
        {
            txtMessaggio.Text = "Seleziona una razza.";
            return;
        }

        if (cmbStato.SelectedItem == null)
        {
            txtMessaggio.Text = "Seleziona uno stato.";
            return;
        }

        if (dataIngresso.SelectedDate == null)
        {
            txtMessaggio.Text = "Seleziona la data di ingresso.";
            return;
        }

        Razza razza = (Razza)cmbRazza.SelectedItem;
        Stato stato = (Stato)cmbStato.SelectedItem;

        DateTime data = dataIngresso.SelectedDate.Value.Date;

        if (data.Date > DateTime.Today)
        {
            txtMessaggio.Text = "La data di ingresso non può essere futura.";
            return;
        }

        CCane nuovoCane = new CCane(nome, anno, razza, informazioni, stato, data);

        canile.AggiungiCane(nuovoCane);

        json.Salva(canile.cani);

        txtMessaggio.Text = $"{nome} è stato registrato con successo.";
        AggiornaLista();
        PulisciCampi();

    }

    private void AggiornaLista()
    {
        listaCani.Items.Clear();

        string ricerca = (txtRicerca.Text ?? "").ToLower();

        foreach (CCane c in canile.cani)
        {
            if (c.Nome.ToLower().Contains(ricerca))
            {
                listaCani.Items.Add(c);
            }
        }

        txtTotale.Text = "Cani presenti: " + canile.cani.Count;
        txtAdottati.Text = "Cani adottati in questa sessione: " + caniAdottati;
    }

    private void CercaCane_TextChanged(object? sender, TextChangedEventArgs e)
    {
        AggiornaLista();
    }

    private void AdottaCane_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        CCane cane = listaCani.SelectedItem as CCane;

        if (cane == null)
        {
            txtMessaggio.Text = "Seleziona un cane da adottare.";
            return;
        }

        canile.AdottaCane(cane);
        caniAdottati++;

        json.Salva(canile.cani);

        AggiornaLista();

        txtMessaggio.Text = cane.Nome + " è stato adottato";
    }
    private void PulisciCampi_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        PulisciCampi();
        txtMessaggio.Text = "Campi puliti.";
    }
    private void PulisciCampi()
    {
        txtNome.Text = "";
        txtAnnoNascita.Text = "";
        txtInformazioni.Text = "";

        cmbRazza.SelectedIndex = -1;
        cmbStato.SelectedIndex = -1;

        dataIngresso.SelectedDate = DateTime.Now;
    }
}