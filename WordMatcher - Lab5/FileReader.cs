using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMatcher___Lab5
{
    class FileReader
{
    public string[] Read(string filename)
    {
        try
        {
            // Read all lines from the file using StreamReader
            string[] lines = File.ReadAllLines(filename);

            // Return the array of lines
            return lines;
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the file reading process
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            return null;
        }
    }
}
}
