using System;
using System.IO;

namespace level1
{
    public class getFile
    {
        /*
            Get input returns an string array broken by spaces so we can parse arguments
        */
        public static string[] getInput() 
        {
            Console.WriteLine("Query a fasta file.\nYou query should look similar to the following: Search16s -level1 16S.fasta 273 1");
            string[] input = {};
            input = Console.ReadLine().Split(' ');
            return input;
        }

        /*
            Helper function to ensure correct input / sanitization
        */
        static bool invalidArg(string arg, int i)
        {
            // Checking line number is int as well as sequences
            int t; // temp for tryparse
            bool err = false;
            if(i > 5)
            {
                Console.WriteLine("Too many arguments were supplied"); 
                err = true;
            }
            else if((i == 4 || i == 5) && !int.TryParse(arg, out t))
            {
                Console.WriteLine("\"{0}\" argument: {1} was a letter and not a number", arg, i);
                err = true;
            }
            else if(string.IsNullOrEmpty(arg))
            {
                Console.WriteLine("Arguments cannot be empty arg: {i}", i);
                err = true;
            }
            else err = false;

            return err;
        }
        /*
            reads the file in the current file system 
            * Replace Directory.GetCurrentDirectory() with more standerdized solution
        */
        static string[] readFile(string file, int count, string type)
        {
            string[] errMsg = {}; // Error message is empty since we check if the array is empty after the read file
            if(!File.Exists(Directory.GetCurrentDirectory()+"/"+file))
            {
                Console.WriteLine("{0} does not exist, or is not in the current directory.", file);
                return errMsg;
            }
            else if(file.Split(".")[1] != "fasta")
            {
                Console.WriteLine("{0} is the incorrect file type. Expected .fasta", file);
                return errMsg;
            }
            string dir = Directory.GetCurrentDirectory()+"/"+file;
            string[] fileData = new string[2];
            int sortType;
            // Removing string argument and converting to int (May be able to loop through later instead)
            Int32.TryParse(type.Remove(0,6), out sortType);
            if(sortType > 3 || sortType < 1) Console.WriteLine("Invalid sorting level! The sort types are 1,2,3.\nSupplied level: {0}", sortType); // Error handling
            if(sortType == 1)
            {
                fileData[0] = File.ReadAllLines(dir)[count-1];  // Meta Data
                fileData[1] = File.ReadAllLines(dir)[count];    // Actual Sequence
            }
            else if(sortType == 2)
            {
                Console.WriteLine("Level 2 sorting is not currently implemented and is defaulting to level 1 behaviours");
                fileData[0] = File.ReadAllLines(dir)[count-1];  // Meta Data
                fileData[1] = File.ReadAllLines(dir)[count];    // Actual Sequence
            }
            else if(sortType == 3)
            {
                Console.WriteLine("Level 3 sorting is not currently implemented and is defaulting to level 1 behaviours");
                fileData[0] = File.ReadAllLines(dir)[count-1];  // Meta Data
                fileData[1] = File.ReadAllLines(dir)[count];    // Actual Sequence
            }
            return fileData;
        }

        /*
            What getInput should return

            Search16s       - Program                   ?? No clue why this is even here
            -level1         - flag (type of search)     (Assumming level 1 stands for level 1 of the assessment)
            filename.fasta  - File name
            273             - Line Number
            10              - Sequences
        */
        public static void init()
        {
            string[] input = getInput();
            string pgName;
            string flag;
            string file;
            int lineNum;
            int sequences;
            bool error = false;
            int i = 0;
            
            // Error handling
            foreach( var arg in  input)
            {
                error = invalidArg(arg, i);
                i++;
            }
            if(error) return;
            
            // Defining our input arguments after error handling is completed
            
            pgName = input[0];
            flag = input[1];
            file = input[2];
            Int32.TryParse(input[3], out lineNum);
            Int32.TryParse(input[4], out sequences);

            string[] lines = readFile(file, lineNum, flag);
            if(lines == null) return; // If the lines array is empty (Should contain metadata at the very least)
            File.WriteAllLines(Directory.GetCurrentDirectory()+"/"+"query.txt", lines);
        }

    }

    class readQueryFile
    {
        public static void read()
        {
            string[] query = File.ReadAllLines(Directory.GetCurrentDirectory()+"/query.txt");
            foreach(var line in query)
            {
                Console.WriteLine(line);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            getFile.init();
            readQueryFile.read();
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
