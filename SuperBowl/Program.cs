using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SuperBowl
{
    class RomaiSorszam
    {
        public string RomaiSsz { get; private set; }

        private static Dictionary<char, int> RomaiMap = new Dictionary<char, int>()
    {
        {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}
    };

        public string ArabSsz
        {
            get
            {
                int ertek = 0;
                string romaiSzam = RomaiSsz.TrimEnd('.');
                for (int i = 0; i < romaiSzam.Length; i++)
                {
                    if (i + 1 < romaiSzam.Length &&
                        RomaiMap[romaiSzam[i]] < RomaiMap[romaiSzam[i + 1]])
                    {
                        ertek -= RomaiMap[romaiSzam[i]];
                    }
                    else
                    {
                        ertek += RomaiMap[romaiSzam[i]];
                    }
                }
                return $"{ertek}.";
            }
        }

        public RomaiSorszam(string romaiSsz)
        {
            RomaiSsz = romaiSsz.ToUpper();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("SuperBowl.txt");
            sr.ReadLine();
            List<NFL> listam = new List<NFL>();

            while(!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string[] db = sor.Split(';');
                NFL n = new NFL();
                n.Ssz = db[0];
                n.datum =Convert.ToDateTime(db[1]);
                n.gyoztes =db[2];
                n.eredmeny =db[3];
                n.vesztes = db[4];
                n.helyszin = db[5];
                n.varosallam = db[6];
                n.nezoszam =Convert.ToInt32(db[7]);
                listam.Add(n);
            }

            sr.Close();

            //4.Feladat:
            int szam = 0;
            foreach (var item in listam)
            {
                 szam=listam.Count();
            }
            Console.WriteLine("4.Feladat: Döntők száma: "+szam);

            //5.Feladat:
            double gyoztes =0;
            double vesztes =0;
            double sum= 0;
            foreach (var item in listam)
            {

            gyoztes=Convert.ToInt32(item.eredmeny.Substring(0,2));
            vesztes=Convert.ToInt32(item.eredmeny.Substring(item.eredmeny.IndexOf('-')+1));

                sum += (gyoztes - vesztes);  
                

            }
            
            double atlag =sum/ listam.Count();

            Console.WriteLine("5.Feladat:Átlagos pontkülönbség:"+Math.Round(atlag,2));

            //6.Feladat:

            


            
            int legnagyobb = 0;
            string helyszin = "";
            string varosallam = "";
            string vesztescsapat = "";
            int vesztespontok = 0;
            string gyoztescsapat = "";
            int gyoztespontok = 0;
            DateTime datum = new DateTime();
            RomaiSorszam ro = new RomaiSorszam("XIV");


            foreach (var item in listam)
            {
                 
                  if (item.nezoszam>legnagyobb)
                    {
                        legnagyobb =item.nezoszam;
                        helyszin = item.helyszin;
                        varosallam = item.varosallam;
                    vesztescsapat = item.vesztes;
                    vesztespontok = Convert.ToInt32(item.eredmeny.Substring(item.eredmeny.IndexOf('-') + 1));
                    gyoztescsapat = item.gyoztes;
                    gyoztespontok = Convert.ToInt32(item.eredmeny.Substring(0, 2));
                    datum = item.datum;
                    

                }

                    
                
                
            }
            Console.WriteLine("6.Feladat: Legmagasabb nézőpont a döntők során:");
            Console.WriteLine("Sorszám"+ro +"(Dátum):"+datum.Year+"."+datum.Month+"."+datum.Day);
            Console.WriteLine("Győztes csapat:" + gyoztescsapat + "," + " Szerzett pontok " + gyoztespontok);
            Console.WriteLine("Vesztes csapat:"+vesztescsapat+","+" Szerzett pontok "+vesztespontok);
            Console.WriteLine("Helyszín:"+helyszin);
            Console.WriteLine("Város,állam:"+varosallam);
            Console.WriteLine("Nézőszám:"+legnagyobb);

            Console.ReadLine();
        }
    }
}
