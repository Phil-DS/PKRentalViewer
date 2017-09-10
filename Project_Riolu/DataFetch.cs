using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Riolu
{
    public class DataFetch
    {
        public static string getSpecies(ushort ID,int form)
        {
            
            try
            {
                if (form > 0)
                {
                    //Has a different form from normal
                    foreach (string line in getList("pokemonFormAbilities"))
                    {
                        var currLine = line.Split(',');
                        if (int.Parse(currLine[0]) == ID)
                        {
                            if (int.Parse(currLine[1]) == form)
                            {
                                return currLine[2];
                            }
                        }
                    }
                }
                return getList("Species")[ID];
                
            }catch(Exception e)
            {
                return ID.ToString();
            }
        }
        public static string getMove(ushort ID, Pokemon p)
        {
            string[] names = getList("Moves");
            try
            {
                if(names[ID] == "Hidden Power")
                {
                    return "Hidden Power [" + p.getHiddenPowerType() + "]";
                }
                return names[ID];
            }
            catch (Exception e)
            {
                return ID.ToString();
            }
        }
        public static string getAbility(ushort ID, int form,int ability)
        {
            //Update to deal with the 1,2,4.
            /*
             * P3DS thoughts: 
             *  - Split abilities into abilities and forms w/ different abilities
             *  - Then, first check if Pokemon has a form ID > 0. If it does, check the form list for it
             *    since it will require more processing than a standard list like normal.
             *  - Maybe include an exclusion list as well, to help sort the out.
             * 
            */

            try
            {
                if (form > 0)
                {
                    //Has a different form from normal
                    foreach (string line in getList("pokemonFormAbilities"))
                    {
                        var currLine = line.Split(',');
                        if (int.Parse(currLine[0]) == ID)
                        {
                            if (int.Parse(currLine[1]) == form)
                            {
                                return currLine[3+(int)Math.Log(ability,2)];
                            }
                        }
                    }
                }
                return getList("pokemonAbilities")[ID].Split(',')[(int)Math.Log(ability, 2)];

            }
            catch (Exception e)
            {
                return ability.ToString();
            }
            
        }
        public static string getItem(ushort ID)
        {
            string[] names = getList("Items");
            try
            {
                return names[ID];
            }
            catch (Exception e)
            {
                return ID.ToString();
            }
        }
        public static string getNature(byte ID)
        {
            string[] names = getList("Natures");
            try
            {
                return names[ID];
            }
            catch (Exception e)
            {
                return ID.ToString();
            }
        }
        private static string[] getList(string loc)
        {
            var txt = Properties.Resources.ResourceManager.GetString(loc); 
            if (txt == null)
                return new string[0];

            string[] rawlist = (txt).Split('\n');

            for (int i = 0; i < rawlist.Length; i++)
                rawlist[i] = rawlist[i].Trim();

            return rawlist;
        }
    }
}
