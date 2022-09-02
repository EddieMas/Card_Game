using System;

namespace Igralnie_Karti
{
    internal class Karta
    {
        private string Mast_karti;
        private short nomer;

        public short Nomer
        {
            get { return nomer; }
            set { nomer = value; }
        }
        public string Mast_Karti
        {
            get { return Mast_karti; }
            set { Mast_karti = value; }
        }
        public Karta(Mast Mast_karti, short nomer)
        {
            Mast_Karti = Mast_karti.Nazvanie;
            Nomer = nomer;
        }
        public override string ToString()
        {
            string answer = String.Empty;
            if (Nomer == 11)
                answer = "Jack";
            else if (Nomer == 12)
                answer = "Queen";
            else if (Nomer == 13)
                answer = "King";
            else if (Nomer == 14)
                answer = "Ace";
            else
                answer = Convert.ToString(Nomer);
            return $"{answer} of {Mast_karti}";
        }

    }
}