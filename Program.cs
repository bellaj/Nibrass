using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WPFTaskbarNotifier
{
    class Program
    {

        [STAThread]

        static void rename(string args)
        {

            try
            {

                DirectoryInfo di = new DirectoryInfo(@"C:\Users\Administrateur\Desktop\last2\try2\WPFTaskbarNotifierExample\bin\Debug\melange");

                // Get a reference to each file in that directory.

                FileInfo[] fiArr = di.GetFiles("*.jpg");

                // Define an Integer Counter

                int i = 0;

                string path;

                // Display the names of the files.

                foreach (FileInfo fri in fiArr)
                {

                    //Console.WriteLine(fri.Name);

                    //Change the prefix to something that is not already there

                    path = @"C:\Users\Administrateur\Desktop\last2\try2\WPFTaskbarNotifierExample\bin\Debug\melange\" + i.ToString() + ".jpg";

                    fri.MoveTo(path);

                    i++;

                }


                Console.WriteLine("Done");

            }


            catch (Exception e)
            {

                Console.WriteLine("The process failed: {0}", e.ToString());

            }
            Console.Read();

        }




    }
}
