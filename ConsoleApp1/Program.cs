// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ConsoleApp1;
using Newtonsoft.Json;


public static class Program
{
   
    public static async Task Main(string[] args)
    {
        try
        {
            // Проверяем и обновляем данные о курсах валют
            await CurrencyManager.CheckAndUpdateData();

            // Показываем пользователю курсы трех наиболее популярных валют относительно гривны
            var currencies = await CurrencyManager.LoadDataFromFile("currency_data.json");
            Console.WriteLine("Курсы валют (относительно гривны):");
            foreach (var currency in currencies)
            {
                if (currency.cc == "USD" || currency.cc == "EUR" || currency.cc == "PLN")
                {
                    Console.WriteLine($"{currency.txt}: {currency.rate} за 1 гривну");
                }
            }

            // Запрашиваем у пользователя сумму в гривнах
            Console.Write("Введите сумму в гривнах: ");
            decimal amountInUAH = decimal.Parse(Console.ReadLine());

            // Запрашиваем у пользователя выбранную валюту
            Console.Write("Выберите валюту для покупки (USD, EUR, PLN): ");
            string selectedCurrencyCode = Console.ReadLine().ToUpper();

            // Находим курс выбранной валюты
            decimal exchangeRate = 0;
            foreach (var currency in currencies)
            {
                if (currency.cc == selectedCurrencyCode)
                {
                    exchangeRate = currency.rate;
                    break;
                }
            }

            // Рассчитываем сумму в выбранной валюте и форматируем результат до двух знаков после запятой
            decimal amountInSelectedCurrency = Math.Round(amountInUAH / exchangeRate, 2);
            Console.WriteLine($"Сумма в выбранной валюте ({selectedCurrencyCode}): {amountInSelectedCurrency:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.ReadLine(); 
    }
    
}




