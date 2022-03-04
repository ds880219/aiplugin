using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IllustratorCLI
{
    class AIActionParameter
    {
        public void WriteActionParameter(string param)
        {
            ActionData actionData = new ActionData();
            BEL_Actions bel = new BEL_Actions();
            bel.AIAction();
            bel.actions.TryGetValue(param, out actionData);

            using (StreamWriter file = new StreamWriter("param.txt"))
            {
                string[] datas = new string[20];
                file.WriteLine(actionData.InternalName);
                try
                {
                    for (int i = 0, j = 0; i < actionData.Parameters.Length; i++, j += 2)
                    {
                        datas[j] = actionData.Parameters[i].Key.ToString();
                        datas[j + 1] = actionData.Parameters[i].Value.ToString();
                    }
                    foreach (string data in datas)
                    {
                        if(data != null)
                        {
                            file.WriteLine(data);
                        }
                    }
                }
                catch (Exception)
                { }
            }
        }
    }
}
