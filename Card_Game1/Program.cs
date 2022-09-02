using Igralnie_Karti;
using System;
using System.Collections.Generic;
using System.Linq;

short[] Karta_Chervi = new short[9];
short[] Karta_Kresti = new short[9];
short[] Karta_Bubna = new short[9];
short[] Karta_Pika = new short[9];

Random rnd = new Random();
for (short i = 0; i < 9; i++)
{
    do
    {
        short tmp = (short)rnd.Next(6, 15);
        if (!Array.Exists(Karta_Chervi, element => element == tmp))
            Karta_Chervi[i] = tmp;
    } while (Karta_Chervi[i] == 0);
    do
    {
        short tmp = (short)rnd.Next(6, 15);
        if (!Array.Exists(Karta_Bubna, element => element == tmp))
            Karta_Bubna[i] = tmp;
    } while (Karta_Bubna[i] == 0);
    do
    {
        short tmp = (short)rnd.Next(6, 15);
        if (!Array.Exists(Karta_Pika, element => element == tmp))
            Karta_Pika[i] = tmp;
    } while (Karta_Pika[i] == 0);
    do
    {
        short tmp = (short)rnd.Next(6, 15);
        if (!Array.Exists(Karta_Kresti, element => element == tmp))
            Karta_Kresti[i] = tmp;
    } while (Karta_Kresti[i] == 0);
}
Stack<Karta> stol = new Stack<Karta>();
short Kolichestvo_chervi = 0;
short Kolichestvo_kresti = 0;
short Kolichestvo_pika = 0;
short Kolichestvo_bubna = 0;
for (short i = 0; i < 36; i++)
{
    short tmp = 0;
    do
    {
        tmp = (short)rnd.Next(1, 5);
    } while ((tmp == 1 && Kolichestvo_chervi > 8) || (tmp == 2 && Kolichestvo_pika > 8) || (tmp == 3 && Kolichestvo_bubna > 8) || (tmp == 4 && Kolichestvo_kresti > 8));
    if (tmp == 1)
    {
        stol.Push(new Karta(new Chervi(), Karta_Chervi[Kolichestvo_chervi]));
        Kolichestvo_chervi++;
    }
    else if (tmp == 2)
    {
        stol.Push(new Karta(new Kresti(), Karta_Kresti[Kolichestvo_pika]));
        Kolichestvo_pika++;
    }
    else if (tmp == 3)
    {
        stol.Push(new Karta(new Bubna(), Karta_Bubna[Kolichestvo_bubna]));
        Kolichestvo_bubna++;
    }
    else
    {
        stol.Push(new Karta(new Pika(), Karta_Pika[Kolichestvo_kresti]));
        Kolichestvo_kresti++;
    }
}
Karta[] igrok = new Karta[6];
Karta[] bot = new Karta[6];
for (short i = 0; i < 6; i++)
{
    stol.TryPop(out igrok[i]);
    stol.TryPop(out bot[i]);
}
string Kozirnaia_Karta = stol.Peek().Mast_Karti;
Console.WriteLine($"{char.ToUpper(Kozirnaia_Karta.First()) + Kozirnaia_Karta.Substring(1)} Kozirnaia karta\n");
short menshe_koloda = 1;
while (true)
{
    if (menshe_koloda % 2 == 1)
    {
        Console.WriteLine("Vashi karti:");
        for (int i = 0; i < igrok.Length; i++)
            Console.WriteLine($"{i + 1}) {igrok[i].ToString()}");
        short Nomer_karti = 0;
        do
        {
            Console.Write("Vvedite nomer carti kotoroi hotite sigrat: ");
            Nomer_karti = short.Parse(Console.ReadLine());
        } while (Nomer_karti < 1 || Nomer_karti > igrok.Length);
        Console.WriteLine($"Sigranaia karta {igrok[Nomer_karti - 1].ToString()}");
        for (short i = 0; i < bot.Length; i++)
        {
            if ((igrok[Nomer_karti - 1].Mast_Karti.Equals(bot[i].Mast_Karti) && igrok[Nomer_karti - 1].Nomer < bot[i].Nomer) || (!igrok[Nomer_karti - 1].Mast_Karti.Equals(Kozirnaia_Karta) && bot[i].Mast_Karti.Equals(Kozirnaia_Karta)) || (igrok[Nomer_karti - 1].Mast_Karti.Equals(Kozirnaia_Karta) && bot[i].Mast_Karti.Equals(Kozirnaia_Karta) && igrok[Nomer_karti - 1].Nomer < bot[i].Nomer))
            {
                Console.WriteLine($"Bot pobilsa kartoi {bot[i].ToString()}");
                bot[i] = new Karta(new Chervi(), (short)0);
                break;
            }
        }
        if (Array.Exists(bot, element => element.Nomer == (short)0))
        {
            igrok[Nomer_karti - 1] = new Karta(new Chervi(), (short)0);
            if (stol.Count != 1)
            {
                stol.TryPop(out Karta card);
                if (igrok.Length < 7)
                    stol.TryPop(out igrok[Nomer_karti - 1]);
                if (bot.Length < 7)
                {
                    for (short i = 0; i < bot.Length; i++)
                    {
                        if (bot[i].Nomer == (short)0)
                            stol.TryPop(out bot[i]);
                    }
                }
                stol.Push(card);
            }
            else if (igrok.Length < 7)
                stol.TryPop(out igrok[Nomer_karti - 1]);
        }
        else
        {
            Console.WriteLine("Bot ne mojet pobit kartu poetomu on eio zabirait");
            menshe_koloda--;
            Array.Resize<Karta>(ref bot, bot.Length + 1);
            bot[bot.Length - 1] = igrok[Nomer_karti - 1];
            igrok[Nomer_karti - 1] = new Karta(new Chervi(), (short)0);
            if (stol.Count != 1 && igrok.Length < 7)
            {
                stol.TryPop(out Karta card);
                stol.TryPop(out igrok[Nomer_karti - 1]);
                stol.Push(card);
            }
            else if (igrok.Length < 7)
                stol.TryPop(out igrok[Nomer_karti - 1]);
        }
    }
    else
    {
        short Nomer_karti = (short)rnd.Next(0, bot.Length);
        Console.WriteLine($"Bot igraet {bot[Nomer_karti].ToString()} kartu");
        Console.WriteLine("Viberite kartu kotoroi otbitsa:");
        short temp = 1;
        Karta[] cards = new Karta[1];
        for (short i = 0; i < igrok.Length; i++)
        {
            if ((bot[Nomer_karti].Mast_Karti.Equals(igrok[i].Mast_Karti) && bot[Nomer_karti].Nomer < igrok[i].Nomer) || (!bot[Nomer_karti].Mast_Karti.Equals(Kozirnaia_Karta) && igrok[i].Mast_Karti.Equals(Kozirnaia_Karta)) || (bot[Nomer_karti].Mast_Karti.Equals(Kozirnaia_Karta) && igrok[i].Mast_Karti.Equals(Kozirnaia_Karta) && bot[Nomer_karti].Nomer < igrok[i].Nomer))
            {
                Console.WriteLine($"{temp}) {igrok[i].ToString()}");
                cards[cards.Length - 1] = igrok[i];
                Array.Resize<Karta>(ref cards, cards.Length + 1);
                temp++;
            }
        }
        if (temp == (short)1)
        {
            Console.WriteLine("Vam nechem otbit kartu poetomu eio beriote v ruku");
            menshe_koloda--;
            Array.Resize<Karta>(ref igrok, igrok.Length + 1);
            igrok[igrok.Length - 1] = bot[Nomer_karti];
            bot[Nomer_karti] = new Karta(new Chervi(), (short)0);
            if (stol.Count != 1 && bot.Length < 7)
            {
                stol.TryPop(out Karta card);
                stol.TryPop(out bot[Nomer_karti]);
                stol.Push(card);
            }
            else if (bot.Length < 7)
                stol.TryPop(out bot[Nomer_karti]);
        }
        else
        {
            short chislo_kart_igroka = 0;
            do
            {
                Console.Write("Vvedite chislo karti kotoroi hotite sigrat: ");
                chislo_kart_igroka = short.Parse(Console.ReadLine());
            } while (chislo_kart_igroka < 1 || chislo_kart_igroka > temp);
            bot[Nomer_karti] = new Karta(new Chervi(), (short)0);
            short nom = 0;
            for (short i = 0; i < igrok.Length; i++)
            {
                if (igrok[i].Mast_Karti.Equals(cards[chislo_kart_igroka - 1].Mast_Karti) && igrok[i].Nomer == cards[chislo_kart_igroka - 1].Nomer)
                {
                    igrok[i] = new Karta(new Chervi(), (short)0);
                    nom = i;
                    break;
                }
            }
            if (stol.Count != 1)
            {
                stol.TryPop(out Karta karta);
                if (bot.Length < 7)
                    stol.TryPop(out bot[Nomer_karti]);
                if (igrok.Length < 7)
                    stol.TryPop(out igrok[nom]);
                stol.Push(karta);
            }
            else if (bot.Length < 7)
                stol.TryPop(out bot[Nomer_karti]);
        }
    }
    for (short i = 0; i < igrok.Length; i++)
    {
        if (igrok[i] == null || igrok[i].Nomer == (short)0)
        {
            for (short j = i; j < igrok.Length - 1; j++)
                igrok[j] = igrok[j + 1];
            Array.Resize<Karta>(ref igrok, igrok.Length - 1);
        }
    }
    for (short i = 0; i < bot.Length; i++)
    {
        if (bot[i] == null || bot[i].Nomer == (short)0)
        {
            for (short j = i; j < bot.Length - 1; j++)
                bot[j] = bot[j + 1];
            Array.Resize<Karta>(ref bot, bot.Length - 1);
        }
    }
    if (igrok.Length == 0 || bot.Length == 0)
        break;
    Console.WriteLine($"\nU vas v ruke{igrok.Length} kart;");
    Console.WriteLine($"U bota v ruke{bot.Length} kart.");
    Console.WriteLine($"{stol.Count} kart v kolode.\n");
    menshe_koloda++;
}
Console.WriteLine();
if (igrok.Length == 0)
{
    Console.WriteLine("S pobedoi nad botom kotorii kidaet randonmie karti.");
}
else
    Console.WriteLine("S porozeniem nad botom kotorii kidaet randonmie karti.");