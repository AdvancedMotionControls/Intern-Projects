/* *
 * Kurt Madland 6/27/2017
 * Advanced Motion Controls
 * Duplication Checker Project
 *
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace DuplicationChecker
{
    public class Program
    {
        static void Main(string[] args)
        {
            // open file specified by command line argument path 
            // sample argument: C:\Users\kmadland\Desktop\input.txt

            if (validArguments(args)) {

                // valid arguments
                Console.WriteLine("Complete provided filepath: " + args[0] + '\n');
                string input = args[0];

                if (File.Exists(input))
                {
                    output = processFile(input);
                    Console.WriteLine(output);
                }

                else if (Directory.Exists(input))
                {
                    output = ProcessDirectory(input);
                    Console.WriteLine(output);
                }

                else
                {

                    Console.WriteLine("Path not found.");
                }
            }

            else
            {
                Console.WriteLine("No filepath specified through the command line.");
            }

            // DONE; Keep printed information visible in the console
            Console.ReadLine();

        }


        // Core File and Directory Processing functions

        public static string ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            string output = "";
            foreach (string fileName in fileEntries)
            {
                output += processFile(fileName);
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                output += "BEGIN " + Path.GetFileName(subdirectory) + '\n' + '\n';
                output += ProcessDirectory(subdirectory);
                output = output.Substring(0, output.Length - 1);
                output += "END " + Path.GetFileName(subdirectory) + '\n' + '\n';
            }

            output += '\n';
            return output;
        }

        public static string processFile(string filepath)
        {

            // ensure only .adf files are processed by the program
            if (!validExtension(filepath))
            {
                return null;
            }

            // variable initialization for duplicate finding
            var currentSection = new List<string>();
            string header = "";
            string output = Path.GetFileName(filepath) + '\n';
            bool found = false;
            string curr_line;

            // open file stream to specified file
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);

            while ((curr_line = file.ReadLine()) != null)
            {
                // ignore commented sections
                if (curr_line.Contains(';')) currentSection.Clear();
                
                // new section found, update header and list
                else if (curr_line.Contains('['))
                {
                    header = curr_line;
                    currentSection.Clear();
                }

                // variable name or blank line found
                else
                {
                    // chop everything off after equals sign (preserves only variable names)
                    int index = curr_line.IndexOf("=");
                    if (index > 0) curr_line = curr_line.Substring(0, index);

                    // check variable name against variables already found to see if duplicate
                    bool duplicate = currentSection.Contains(curr_line);

                    // DUPLICATE FOUND, ADD NAME + SECTION TO OUTPUT
                    if (duplicate && curr_line != null && curr_line != "\n" && curr_line != "")
                    {
                        output = output + header + " " + curr_line + '\n';
                        found = true;
                    }

                    else currentSection.Add(curr_line);

                }

            }

            if (found == false) output += "NO DUPLICATES FOUND" + '\n';
            output += '\n';
            file.Close();
            return output;

        }
        
        // Returns true if command line arguments are provided, false if not    
        public static bool validArguments(string[] args)
        {
            if (args == null || args.Length == 0 || args.Length > 1) return false;
            else return true;
        }

        // Returns true if the provided filepath is an .adf file, false if not
        public static bool validExtension(string filepath)
        {
            string ext = Path.GetExtension(filepath);
            if (ext != ".adf") return false;
            else return true;
        }
        

         
    } // end class program

} // end namespace