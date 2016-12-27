/*    
 *  B. Donaldson - Twitter: @bitobrian
 *  12/26/2016
 *  Description: A program to remove unwanted files in the Windows
 *      operating system. Evolved from a failed PowerShell script.
 *      
 *      I had a folder of rom files with muliple versions. I only 
 *      wanted U.S. versions and only one per title. This allows you to target
 *      a directory and type in a string like "(J)" - for Japanese games - to 
 *      remove them.
 *      
 *      Originally I had a PowerShell script written to do this easily. I tried
 *      several different methods of Remove-Item and could not get it to remove
 *      certain files. Perfect excuse to write a program!
 *      
 *      Copy and paste a folder location from Windows Explorer in the exe.config 
 *      before running.
 */
using System;
using System.Linq;
using System.IO;

namespace file_unmesser
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vars
            bool cont = true;
            string workingDirectory = Unmesser.Properties.Settings.Default.TargetDirectory;

            // Handle blank folder path
            if (workingDirectory.Equals(string.Empty))
            {
                Console.WriteLine("Please specify a folder path in the exe.config or paste it here and press enter.");                
                workingDirectory = Console.ReadLine();
            }

            string[] count = Directory.GetFiles(workingDirectory);

            // Start loop
            while (cont)
            {
                // Clear the console and set file count
                count = Directory.GetFiles(workingDirectory);
                Console.Clear();
                Console.WriteLine("There are " + count.Count() + " total files.");
                Console.WriteLine("Specify a wild card not including *:");

                // Get input for matching.
                // ** I only wanted U.S. roms so I could type in '(J)' and delete most Japanese roms.
                // I did see tags like '[f1]' on some files. I stopped before doing just 'f1'.
                // There was an f1 game title so I made sure to include at least the '[f1'. 
                // The matcher is string based so it's easy to avoid. There are more cases similar to this, however.
                string input = Console.ReadLine();
                string[] fileList = Directory.GetFiles(workingDirectory, "*" + input + "*");
                
                // Some Output
                Console.WriteLine("There are " + count.Count() + " total files. \nThere will be " + fileList.Count() +
                    " deleted. \nPress n to stop or y to continue.");

                // Ask "are you sure" and Wait to continue
                string inputKey = Console.ReadLine();

                if (!inputKey.Equals("n") && !inputKey.Equals("N"))
                {

                    // Begin processing
                    foreach (string s in fileList)
                    {
                        // Log it
                        Console.WriteLine("\nDeleting " + s);

                        // TryCatch it
                        try
                        {
                            // Delete it
                            File.Delete(s);
                        }
                        catch (Exception e)
                        {
                            // Can't delete it for some reason. Log it.
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            // Simply exit to end
        }
    }
}
