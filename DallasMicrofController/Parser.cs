using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DallasMicrofController
{
    public class Parser
    {
        public static Parser Global;
        string[] args = null;
        public Parser(string[] args)
        {
            this.args = args;
        }

        public string FindParamsAndArgs(string Params, out bool Finds)
        {
            string ret = "";
            bool bol = false;
            for (int i = 0; i < args.Length; i++)
            {
                if(args[i] == Params)
                {
                    ret = args[i+1];
                    break;
                }
            }
            Finds = bol;
            return ret;
        }
        
        public bool FindParamsAndArgs(string Params, out string Finds)
        {
            string ret = "";
            bool bol = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == Params)
                {
                    bol = true;
                    ret = args[i + 1];
                    break;
                }
            }
            Finds = ret;
            return bol;
        }
        public bool FindParams(string Params)
        {
            bool bol = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == Params)
                {
                    bol = true;
                    break;
                }
            }
            return bol;
        }
    }
}
