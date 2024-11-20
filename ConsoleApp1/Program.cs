using System;
using System.Xml.Linq;

namespace Hochregallager1
{
    internal class Program
    {
        static void Main(string[] args)
        {   //           
             Lagersystem lagersystem = new Lagersystem();
             Paket paket = new Paket();

            Console.WriteLine("Bitte geben Sie die Paket ID an:");       
            // string IDPak = Console.ReadLine();
            string IDPak = "1:2:7:1234";                   
            Console.WriteLine("Bitte geben Sie die Größe des Paketes an (1/2/3)");
            string groessestr = Console.ReadLine();
            int groesse = int.Parse(groessestr);

           
            while (groesse < 1 || groesse > 4)
            {
                groesse = 0;
                Console.WriteLine("Die Größe MUSS 1 bis 3 sein");
                groessestr = Console.ReadLine();
                groesse = int.Parse(groessestr);

            }

            //Initialisierung der 2 Regale, 2 Ebenen, 20 Fächern und 4 Segmenten
            List<Regal> regale = new List<Regal>();
            for(int i = 1; i < 3; i++)
            {
                Regal regal = new Regal("Regal"+i);
                for(int j = 1; j< 3; j++)
                {
                    Ebene ebene = new Ebene("Ebene"+j);
                    for (int k = 1;  k < 21; k++)
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
                Console.WriteLine(IDPak);
                Console.WriteLine(groesse);           
        }
        public class Lagersystem
        {
            public string IDLS;
            private int status;
        }
        public class Paket
        {
            public string IDPak { get; private set; }
            public string groesse { get; private set; }
            public string bezeichnung;
        }
        public class Regal
        {
            public string IDRG;
            public int max_Anzahl_Fächer = 20;
            public List<Ebene> Ebenenliste { get; set; }
            public Regal(string Regalname)
            {
                IDRG = Regalname;
                Ebenenliste = new List<Ebene>();
            }
            public int Ebene;
        }
        public class Fach
        {
            public string IDFach { get; set; }
            public List<Segment> Segments { get; set; }
            public int FachStatus;
            public int Kapazitaet;
            public Fach(string idFach)
            {
                IDFach = idFach;
                Segments = new List<Segment>();
            }
        }
        public class Segment
        {
            public string IDSeg { get; set; }           
            private int statusSeg;
            public Segment(string idSeg)
            {
                IDSeg = idSeg;                
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