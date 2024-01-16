using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace InterrogationRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DateTime dateOfToday = DateTime.Now;
                // var currentYear = int.Parse(dateOfToday.Year.ToString()); niepotrzebna podwójna konwersja
                var currentYear = dateOfToday.Year;
                var firstYear = 1;
                var minNumberMonthsInYear = 1;
                var maxNumberMonthsInYear = 12;
                var minNumberDaysInMonth = 1;
                var maxNumberDaysInMonth = 31;


                Console.WriteLine("Proszę podać swoje dane osobiste:\nImię:");
                var name = CheckString();

                Console.WriteLine("Nazwisko:");
                var surname = CheckString();

                Console.WriteLine("Miejsce urodzenia:");
                var placeOfBirth = CheckString();

                Console.WriteLine("Rok urodzenia:");
                var yearOfBirth = CheckInt(firstYear, currentYear);

                Console.WriteLine("Miesiąc urodzenia:");
                var monthOfBirth = CheckInt(minNumberMonthsInYear, maxNumberMonthsInYear);

                Console.WriteLine("Dzień urodzenia:");
                var dayOfBirth = CheckInt(minNumberDaysInMonth, maxNumberDaysInMonth);

                var dateOfBirthString = dayOfBirth + "." + monthOfBirth + "." + yearOfBirth;

                var dateOfBirth = IsDate(dateOfBirthString);

                var age = AreDatesChronological(dateOfBirth, dateOfToday);

                var differenceTime = dateOfToday - dateOfBirth;

                var ageExact = DateTime.MinValue + differenceTime;

                var numberOfYears = ageExact.Year - 1;
                var numberOfMonths = ageExact.Month - 1;
                var numberOfDays = ageExact.Day - 1;

                Console.WriteLine(
                    $"Cześć, nazywasz się {name} {surname}, urodziłeś się w miejscowości {placeOfBirth}, Twój wiek to {age} lat(lata).");
                Console.WriteLine(
                    $"Twój dokładny wiek to: {numberOfYears} lat(lata) {numberOfMonths} miesiąc(m-cy) {numberOfDays} dzień (dni). ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static string CheckString()
        {
            var input = IsNotEmpty();

            var permissibleValuesForInputString = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻaąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż-";

            foreach (var sign in input)
            {
                if (!permissibleValuesForInputString.Contains(sign))
                {
                    throw new Exception("Nazwa zawiera niewłaściwe znaki.!");
                }
            }

            if (input.StartsWith("-") | input.EndsWith("-"))
            {
                throw new Exception("Znak łącznika '-' nie może znajdować się na początku lub na końcu nazwy.!");
            }
            return input;
        }

        private static int CheckInt(int minValue, int maxvalue)
        {
            var input = IsNotEmpty();

            var permissibleValuesForInputInt = "1234567890";

            foreach (var sign in input)
            {
                if (!permissibleValuesForInputInt.Contains(sign))
                {
                    throw new Exception("Wprowadzane dane muszą składać się wyłącznie z cyfr.!");
                }
            }

            if (!int.TryParse(input, out int inputInt))
            {
                throw new Exception("Podana wartość jest nieprawidłowa.!");
            }
            
            if (inputInt < minValue | inputInt > maxvalue)
            {
                throw new Exception($"Podano liczbę z poza zakresu: {minValue} - {maxvalue}.!");
            }
            return inputInt;
        }

        private static DateTime IsDate(string inputDate)
        {
            if (!DateTime.TryParse(inputDate, out DateTime date))
            {
                throw new Exception($"Nie ma takiej daty: {inputDate} w kalendarzu.!");
            }
            return date;
        }

        private static int AreDatesChronological(DateTime birthDate, DateTime todayDate )
        {
            if (birthDate > todayDate)
            {
                throw new Exception($"Podana data urodzenia nie może być późniejsza od daty dzisiejszej {todayDate.Day}.{todayDate.Month}.{todayDate.Year}.!");
            }
            var age = todayDate.Year - birthDate.Year;
            return age;
        }

        private static string IsNotEmpty()
        {
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Nie wypełniono pola.!");
            }
            return input;
        }
    }
}
