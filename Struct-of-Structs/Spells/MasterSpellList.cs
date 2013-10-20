using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1.Effects;

namespace Struct_of_Structs.Spells
{
    //TODO: work on and finish :p
    class MasterSpellList
    {
        private const string Dir = @"Spells\";
        private const string ErrorFile = Dir + "SpellErrors.txt";
        private const string AllSpellsFile = Dir + "AllSpells.txt";

        public MasterSpellList()
        {
            using(StreamReader sr = new StreamReader(AllSpellsFile))
            using (StreamWriter sw = new StreamWriter(ErrorFile, false))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    line = line ?? "";
                    int firstColon = line.IndexOf(':');
                    int secondColon = line.IndexOf(':', firstColon + 1);
                    int thirdColon = line.IndexOf(':', secondColon + 1);
                    int fouthColon = line.IndexOf(':', thirdColon + 1);
                }
            }
        }
    }
}
