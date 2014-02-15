using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ratchet
{
    public class Program
    {
        private Configuration config;
        static void Main(string[] args)
        {
            Program p = new Program(args);
            p.Run();
        }

        public Program(string[] args)
        {
            config = new Configuration();
        }

        public void Run()
        {
            string line;
            while((line = Console.In.ReadLine()) != null)
            {
                if (! config.Matches(line))
                {
                    Console.Error.Write(line);
                }
            }            
        }
    }
    
    public class Configuration
    {
        private HashSet<string> knownWarnings = new HashSet<string>();
        public Configuration() : this(Path.Combine(System.Environment.CurrentDirectory, ".ratchet"))
        {
        }

        public Configuration(string fileName)
        {
            if (File.Exists(fileName))
            {
                Initialize(new StreamReader(fileName));
            }
        }

        public void Initialize(StreamReader configContents)
        {
            string line;
            while ((line = configContents.ReadLine()) != null)
            {
                knownWarnings.Add(line.Trim());
            }
        }

        public bool Matches(string s)
        {
            if (knownWarnings.Contains(s.Trim())) return true;
            return false;
        }
    }
}
