using System;
using System.Collections.Generic;
using System.Text;

namespace MaszynaTuringa
{
    public class Turing
    {
        enum RuchGlowicy
        {
            Brak,
            Lewo,
            Prawo
        }
        public LinkedList<char> Tasma { get; set; }
       int Stan { get; set; }
        LinkedListNode<char> PozycjaNaTasmie { get; set; }
        public Turing(string tasma)
        {
            var tablicaZnakow = tasma.ToCharArray();
            Tasma = new LinkedList<char>(tablicaZnakow);
            Stan = 0;
            PozycjaNaTasmie = Tasma.Last;
        }

        public void Automat()
        {
            while (true)
            {
                Console.WriteLine("Stan: " + Stan);
                WyswietlTasmeZPozycja();
                if (Stan == 3)
                    break;
                var odczytanyElement = PobierzElementZTasmy();
                switch (Stan)
                {
                    case 0: //proba zpisania 1 jedynki
                        if (odczytanyElement == '0')
                            Instrukcja('1', 1, RuchGlowicy.Lewo);
                        if (odczytanyElement == '1')
                            Instrukcja('0', 2, RuchGlowicy.Lewo);
                        if (odczytanyElement == 'e')
                            Instrukcja(Char.MinValue, 0, RuchGlowicy.Lewo);
                        break;
                    case 1: //1 jedynka do zapisania 0 w pamieci
                        if (odczytanyElement == '0')
                            Instrukcja('1', 3, RuchGlowicy.Lewo);
                        if (odczytanyElement == '1')
                            Instrukcja('0', 1, RuchGlowicy.Lewo);
                        if(odczytanyElement == 'e')
                            Instrukcja('1', 3, RuchGlowicy.Lewo);
                        break;
                    case 2: //1 jedynka do zapisania i 1 w pamieci
                        if (odczytanyElement == '0')
                            Instrukcja('1', 1, RuchGlowicy.Lewo);
                        if (odczytanyElement == '1')
                            Instrukcja('0', 2, RuchGlowicy.Lewo);
                        if (odczytanyElement == 'e')
                            Instrukcja('1', 1, RuchGlowicy.Lewo);
                        break;
                    case 3:
                        Instrukcja(Char.MinValue, 3, RuchGlowicy.Brak);
                        break;
                }
            }
        }
        
        private void Instrukcja(char elementDoZapisu, int kolejnyStan, RuchGlowicy ktoraStrona)
        {
            if(elementDoZapisu != Char.MinValue)
                ZapiszElementNaTasmie(elementDoZapisu);
            Stan = kolejnyStan;
            switch(ktoraStrona)
            {
                case RuchGlowicy.Lewo:
                    GlowicaLewo();
                    break;
                case RuchGlowicy.Prawo:
                    GlowicaPrawo();
                    break;
                default:
                    break;
            }
        }

        private char PobierzElementZTasmy()
        {
            return PozycjaNaTasmie.Value; 
        }

        private void ZapiszElementNaTasmie(char element)
        {
            PozycjaNaTasmie.Value = element;
        }

        private void GlowicaLewo()
        {
            if (PozycjaNaTasmie.Previous == null)
                Tasma.AddBefore(PozycjaNaTasmie, 'e');
            PozycjaNaTasmie = PozycjaNaTasmie.Previous;
        }
        private void GlowicaPrawo()
        {
            if (PozycjaNaTasmie.Next == null)
                Tasma.AddAfter(PozycjaNaTasmie, 'e');
            PozycjaNaTasmie = PozycjaNaTasmie.Next;
        }

        public string TasmaToString()
        {
            var element = Tasma.First;
            StringBuilder stringBuilder = new StringBuilder();
            while(element != null)
            {
                if(element.Value != 'e')
                    stringBuilder.Append(element.Value);
                element = element.Next;
            }
            return stringBuilder.ToString();
        }

        public void WyswietlTasmeZPozycja()
        {
            Console.Write("Tasma: ");
            var element = Tasma.First;
            while (element != null)
            {
                if (element == PozycjaNaTasmie)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(element.Value);
                    Console.ResetColor();
                }
                    
                else
                    Console.Write(element.Value);
                element = element.Next;
            }
            Console.WriteLine();
        }
    }
}
