using System;
using Classes;
using Newtonsoft.Json;

namespace LoadSystem
{
    class Loader
    {
        public List<Game> Load(string path)
        {
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    Console.WriteLine("Successfully Loaded Data");
                    return JsonConvert.DeserializeObject<List<Game>>(json);

                }
            }
            catch
            {
                Console.WriteLine("Failed to Load Data");
                return new List<Game>();
            }
        }

        public void Save(string path, List<Game> games)
        {
            string json = JsonConvert.SerializeObject(games, Formatting.Indented);

            using (StreamWriter w = new StreamWriter(path))
            {
                w.WriteLine(json);
            }
        }
    }

    
}
