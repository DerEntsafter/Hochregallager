using System;
using System.Collections.Generic;

namespace Hochregallager1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialisierung von Regalen und Struktur
            List<Regal> regale = InitialisiereLager();

            // Testpakete hinzufügen
            regale[0].Ebenenliste[1].Fachlist[6].Segments[3].GelagertesPaket = new Paket { IDPak = "1:2:7:1234", bezeichnung = "SchraubenM8" };
            regale[1].Ebenenliste[1].Fachlist[15].Segments[2].GelagertesPaket = new Paket { IDPak = "2:2:16:12", bezeichnung = "Testpaket 2" };

            // Paket auslagern Methode aufrufen
            PaketAuslagern(regale);

            Console.WriteLine("Programm beendet.");
        }

        // Methode zur Initialisierung des Lagers
        static List<Regal> InitialisiereLager()
        {
            List<Regal> regale = new List<Regal>();
            for (int i = 1; i < 3; i++)
            {
                Regal regal = new Regal("Regal" + i);
                for (int j = 1; j < 3; j++)
                {
                    Ebene ebene = new Ebene("Ebene" + j);
                    for (int k = 1; k < 21; k++)
                    {
                        Fach fach = new Fach("Fach" + k);
                        for (int l = 1; l < 5; l++)
                        {
                            Segment segment = new Segment("Segment" + l);
                            fach.Segments.Add(segment);
                        }
                        ebene.Fachlist.Add(fach);
                    }
                    regal.Ebenenliste.Add(ebene);
                }
                regale.Add(regal);
            }
            return regale;
        }

        // Methode zur Paket-Auslagerung
        static void PaketAuslagern(List<Regal> regale)
        {
            while (true)
            {
                Console.WriteLine("Möchten Sie ein Paket auslagern? Wählen Sie die Suchmethode:");
                Console.WriteLine("1: Suche nach Paket-ID");
                Console.WriteLine("2: Suche nach Paketbezeichnung");
                string auswahl = Console.ReadLine();

                if (auswahl == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("Geben Sie die ID des Pakets ein, das ausgelagert werden soll:");
                        string paketID = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(paketID))
                        {
                            if (LagerAuslagern(regale, paketID, null))
                            {
                                break; // Erfolgreiche Auslagerung
                            }
                        }
                        Console.WriteLine("Das Paket konnte nicht ausgelagert werden. Bitte versuchen Sie es erneut.");
                    }
                }
                else if (auswahl == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("Geben Sie die Bezeichnung des Pakets ein, das ausgelagert werden soll:");
                        string paketBezeichnung = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(paketBezeichnung))
                        {
                            if (LagerAuslagern(regale, null, paketBezeichnung))
                            {
                                break; // Erfolgreiche Auslagerung
                            }
                        }
                        Console.WriteLine("Das Paket konnte nicht ausgelagert werden. Bitte versuchen Sie es erneut.");
                    }
                }
                else
                {
                    Console.WriteLine("Ungültige Auswahl. Bitte geben Sie 1 oder 2 ein.");
                    continue; // Zurück zum Menü
                }

                Console.WriteLine("\nMöchten Sie ein weiteres Paket auslagern? (j/n)");
                string weiter = Console.ReadLine().ToLower();

                if (weiter != "j")
                {
                    break; // Schleife verlassen und Programm beenden
                }
            }
        }

        // Methode zur Auslagerung, die die Lagerstruktur durchsucht
        static bool LagerAuslagern(List<Regal> regale, string paketID, string paketBezeichnung)
        {
            foreach (var regal in regale)
            {
                foreach (var ebene in regal.Ebenenliste)
                {
                    foreach (var fach in ebene.Fachlist)
                    {
                        if (fach.EntfernePaket(paketID, paketBezeichnung, regal.IDRG, ebene.NameEb))
                        {
                            if (paketID != null)
                                Console.WriteLine($"Paket mit ID {paketID} erfolgreich ausgelagert.");
                            else
                                Console.WriteLine($"Paket mit Bezeichnung \"{paketBezeichnung}\" erfolgreich ausgelagert.");
                            return true;
                        }
                    }
                }
            }

            if (paketID != null)
                Console.WriteLine($"Paket mit ID {paketID} wurde nicht gefunden.");
            else
                Console.WriteLine($"Paket mit Bezeichnung \"{paketBezeichnung}\" wurde nicht gefunden.");

            return false;
        }

        // Klassenstruktur
        public class Paket
        {
            public string IDPak { get; set; }
            public string groesse { get; set; }
            public string bezeichnung { get; set; }
        }

        public class Regal
        {
            public string IDRG;
            public List<Ebene> Ebenenliste { get; set; }
            public Regal(string Regalname)
            {
                IDRG = Regalname;
                Ebenenliste = new List<Ebene>();
            }
        }

        public class Fach
        {
            public string IDFach { get; set; }
            public List<Segment> Segments { get; set; }

            public Fach(string idFach)
            {
                IDFach = idFach;
                Segments = new List<Segment>();
            }

            // Methode, um ein Paket aus einem Fach zu entfernen
            public bool EntfernePaket(string paketID, string paketBezeichnung, string regalName, string ebeneName)
            {
                foreach (var segment in Segments)
                {
                    if (segment.GelagertesPaket != null &&
                        (segment.GelagertesPaket.IDPak == paketID || segment.GelagertesPaket.bezeichnung == paketBezeichnung))
                    {
                        Console.WriteLine($"Paket aus {regalName}, {ebeneName}, {IDFach}, {segment.IDSeg} entfernt.");
                        Console.WriteLine($"Details - ID: {segment.GelagertesPaket.IDPak}, Bezeichnung: {segment.GelagertesPaket.bezeichnung}");
                        segment.GelagertesPaket = null;
                        return true;
                    }
                }
                return false;
            }
        }

        public class Segment
        {
            public string IDSeg { get; set; }
            public Paket GelagertesPaket { get; set; } // Paket im Segment

            public Segment(string idSeg)
            {
                IDSeg = idSeg;
                GelagertesPaket = null; // Standardmäßig kein Paket
            }
        }

        public class Ebene
        {
            public List<Fach> Fachlist { get; set; }
            public string NameEb { get; set; }
            public Ebene(string Ebenename)
            {
                NameEb = Ebenename;
                Fachlist = new List<Fach>();
            }
        }
    }
}
