namespace Igralnie_Karti
{
    abstract class Mast
    {
        private string nazvanie;

        public string Nazvanie
        {
            get { return nazvanie; }
            set { nazvanie = value; }
        }
        public Mast(string nazvanie)
        {
            Nazvanie = nazvanie;
        }
    }
}