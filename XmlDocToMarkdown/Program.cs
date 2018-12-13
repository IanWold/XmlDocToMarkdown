using System;
using System.IO;
using System.Xml.Serialization;

namespace XmlDocToMarkdown
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] paths;
            if (args.Length == 2)
            {
                paths = args;
            }
            else
            {
                Console.Write("[input_path] [output_path]: ");
                paths = Console.ReadLine().Split(' ');
            }

            if (File.Exists(paths[0]))
            {
                Doc doc;

                using (var reader = new StreamReader(paths[0]))
                {
                    var serializer = new XmlSerializer(typeof(Doc));
                    doc = (Doc)serializer.Deserialize(reader);
                }

                using (var writer = new StreamWriter(paths[1]))
                {
                    writer.Write(doc.ToMarkdown());
                }
            }
            else
            {
                throw new ArgumentException("Source file does not exist: " + paths[0]);
            }
        }
    }
}
