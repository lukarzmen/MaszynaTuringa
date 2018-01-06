using System;

namespace MaszynaTuringa
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Zwiękaszanie liczby binarnej o 3";
            while (true)
            {
                Console.WriteLine("Podaj ciąg znaków: ");
                var ciagWejsciowy = Console.ReadLine();
                Wykonaj(ciagWejsciowy);
                Console.WriteLine();
                Console.WriteLine("Nacisnij dowolny klawisz...");
                Console.WriteLine();
                Console.ReadKey();
            } 
        }

        private static void Wykonaj(string tasma)
        {
            Console.WriteLine("Ciag wejsciowy: " + tasma);
            Turing turing = new Turing(tasma);
            turing.Automat();
            Console.WriteLine("Ciag wyjsciowy: " + turing.TasmaToString());
        }
    }
}
