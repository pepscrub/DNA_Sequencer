using System;
using System.IO;

namespace assessment1
{
    public class getFile
    {
        /*
            Get input returns an string array broken by spaces
         */
        public static string[] getInput()
        {
            Console.WriteLine("Please enter a command:");
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
            if((i == 4 || i == 5) && !int.TryParse(arg, out t)) return true; 
            else if(string.IsNullOrEmpty(arg)) return true;
            else return false;
        }
        /*
            reads the file in the current file system 
            * Replace Directory.GetCurrentDirectory() with more standerdized solution
        */
        static string[] readFile(string file, int count, string type)
        {
            string dir = Directory.GetCurrentDirectory()+"/"+file;
            string[] fileData = new string[2];
            if(type == "-level1")
            {
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
                if(invalidArg(arg, i)) // Checks to see if arguments are empty and if arg 4-5 are ints
                {
                    error = true;
                    break;
                }
                else i++;
            }
            if(i < 5)
            {
                Console.WriteLine("Insuffient arguments were supplied! \nYour query should look appear like the follwing: program -level1 file.fasta 273 10");
                return;
            }
            else if(error)
            {
                Console.WriteLine("There was an invalid argument!\n\"{0}\" Is not a valid argument!", input[i]);
                return;
            }
            
            // Defining our input arguments after error handling is completed
            
            pgName = input[0];
            flag = input[1];
            file = input[2];
            Int32.TryParse(input[3], out lineNum);
            Int32.TryParse(input[4], out sequences);

            string[] lines = readFile(file, lineNum, flag);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            getFile.init();
        }
    }
}
