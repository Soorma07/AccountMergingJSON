using System;
using System.IO;
using Newtonsoft.Json;

namespace AccountMerging
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfArguments = args.Length;
            if (numberOfArguments == 1)
            {
                string jsonFile = args[0];
                Console.WriteLine("Starting account merging for: " + jsonFile);
                using (StreamReader streamReader = new StreamReader(jsonFile))
                {
                    string jsonContent = streamReader.ReadToEnd();
                    dynamic jsonEntries = JsonConvert.DeserializeObject(jsonContent);
                    foreach (var entry in jsonEntries)
                    {
                        Console.WriteLine(entry.application);
                        Console.WriteLine(entry.emails);
                        Console.WriteLine(entry.name);
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("AccountMerging requires one json file argument. Please try again.");
            }
        }
    }
}
