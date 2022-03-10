using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fclp;

namespace IllustratorCLI
{
    class Program
    {
        /// <summary>
        /// Takes the arguments passed by the user and writes
        /// appropriate parameter value in the "param.txt" file
        /// into the root folder of Adobe Illustrator.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string Action = null;
            string Library = null;
            var p = new FluentCommandLineParser();

            p.Setup<string>('a', "action")
             .Callback(action => Action = action)
             .Required();

            p.Setup<string>('l', "library")
             .Callback(library => Library = library);

            p.Parse(args);

            if (args == null)
            {
                Console.WriteLine("Application executed without arguments");
                return;
            }

            if(Action != null)
            {
                AIActionParameter aI = new AIActionParameter();
                aI.WriteActionParameter(Action);
            }
        }
    }
}
