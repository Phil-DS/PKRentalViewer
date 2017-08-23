﻿using System;
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
            string[] names = getList("Species");
            try
            {
                return names[ID];
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
        public static string getAbility(ushort ID, int PID,int form)
        {
            //Update to deal with the 1,2,4.
            
            /*string[] names = getList("Abilities");
            try
            {
                return names[ID];
            }
            catch (Exception e)
            {
                return ID.ToString();
            }*/

            return "Not Implemented";

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
