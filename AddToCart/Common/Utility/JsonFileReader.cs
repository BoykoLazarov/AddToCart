using Newtonsoft.Json;

namespace AddToCart.Common.Utility
{
    public static class JsonFileReader
    {
        public static T ReadJsonFile<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"File not found: {filePath}");
                }

                string jsonContent = File.ReadAllText(filePath);

                T result = JsonConvert.DeserializeObject<T>(jsonContent);

                if (result == null)
                {
                    throw new InvalidOperationException("Deserialization result is null.");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                throw;
            }
        }
    }
}