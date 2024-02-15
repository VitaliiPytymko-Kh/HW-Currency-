using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CurrencyManager
    {
        private const string filePath = "currency_data.json";

        
        public static async Task<List<Currency>> LoadDataFromFile(string filePath)
        {
            try
            {
                string jsonData = await File.ReadAllTextAsync(filePath);
                var currencies = JsonConvert.DeserializeObject<List<Currency>>(jsonData);

                return currencies;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных из файла: {ex.Message}");
                return new List<Currency>();
            }
        }


        public static async Task<List<Currency>> GetDataFromServer(string filePath)
        {
            string pathToServer = @"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";

            var client = new HttpClient();
            var response = await client.GetAsync(pathToServer);
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();

                var jsonSettings = new JsonSerializerSettings
                {
                    DateFormatString = "dd.MM.yyyy" // Указываем формат даты
                };

                var jsonCurrencies = JsonConvert.DeserializeObject<List<Currency>>(jsonContent, jsonSettings);

                // Сохраняем данные в файл
                await Savedata(jsonCurrencies, filePath);

                return jsonCurrencies;
            }
            else
            {
                throw new Exception("Не удалось получить данные с сервера.");
            }
        }

        private static async Task Savedata(List<Currency>? jsonCurrencies, string filePath)
        {
            if (jsonCurrencies == null)
            {
                throw new ArgumentNullException(nameof(jsonCurrencies), "Список данных пуст.");
            }

            try
            {
                string jsonData = JsonConvert.SerializeObject(jsonCurrencies);
                await File.WriteAllTextAsync(filePath, jsonData);
                Console.WriteLine($"Данные успешно сохранены в файл: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
                throw;
            }
        }

        public static async Task CheckAndUpdateData()
        {
            Console.WriteLine("Проверка данных о курсах валют...");

            var currencies = await LoadDataFromFile(filePath);
            if (currencies == null || currencies.Count == 0)
            {
                Console.WriteLine("Файл с данными не найден. Загрузка данных с сервера...");
                await GetDataFromServer(filePath);
            }
            else
            {
                var today = DateTime.Today;
                var lastUpdateDate = currencies[0].exchangedate.Date;

                if (today == lastUpdateDate)
                {
                    Console.WriteLine("Курсы валют актуальны.");
                    await LoadDataFromFile(filePath);
                }
                else
                {
                    Console.WriteLine("Обновление данных о курсах валют...");
                    await GetDataFromServer(filePath);
                }
            }
        }
    }
}

    

