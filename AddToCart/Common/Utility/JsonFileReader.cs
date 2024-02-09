using Newtonsoft.Json;

namespace AddToCart.Common.Utility
{
    public static class JsonFileReader
    {
        public static T ReadJsonFile<T>(string filePath)
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"File not found: {filePath}");
                }

                // Read the content of the file
                string jsonContent = File.ReadAllText(filePath);

                // Deserialize the JSON content to an object of type T
                T result = JsonConvert.DeserializeObject<T>(jsonContent);

                // Check if the result is null
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