using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Riolu
{
    public class Pokemon
    {
        uint Key;
        byte HyperTrainingFlags;
        byte field_5;
        byte field_6;
        byte field_7;
        byte[] PPUps;
        uint IvFlags;
        uint field_10;
        ushort MonsNo;
        ushort HoldItem;
        ushort[] Moves;
        byte field_20;
        byte AbilityFlags;
        byte Nature;
        byte EncounterFlags;
        byte EffortHp;
        byte EffortAtk;
        byte EffortDef;
        byte EffortSpeed;
        byte EffortSpAtk;
        byte EffortSpDef;
        byte field_2A;
        byte Familiarity;
        byte Pokeball;
        byte Level;
        byte CassetteVersion;
        byte LangId;
        byte[] rawData;
        int[] IVs;

        public Pokemon(byte[] pkmData)
        {
            Console.WriteLine(pkmData.Length);
            try
            {
                //split the data down into the correct sections.
                rawData = pkmData.Skip(0).ToArray();
                Key = BitConverter.ToUInt32(pkmData,0);
                HyperTrainingFlags = pkmData[4];
                field_5 = pkmData[5];
                field_6 = pkmData[6];
                field_7 = pkmData[7];
                PPUps = pkmData.Skip(8).Take(4).ToArray();
                IvFlags = BitConverter.ToUInt32(pkmData,12);
                field_10 = BitConverter.ToUInt32(pkmData,16);
                MonsNo = BitConverter.ToUInt16(pkmData,20);
                //Console.WriteLine(MonsNo);

                HoldItem = BitConverter.ToUInt16(pkmData,22);

                Moves = new ushort[4];
                Moves[0] = BitConverter.ToUInt16(pkmData,24);
                Moves[1] = BitConverter.ToUInt16(pkmData,26);
                Moves[2] = BitConverter.ToUInt16(pkmData,28);
                Moves[3] = BitConverter.ToUInt16(pkmData,30);

                field_20 = pkmData[32];
                AbilityFlags = pkmData[33];
                Nature = pkmData[34];
                EncounterFlags = pkmData[35];
                EffortHp = pkmData[36];
                EffortAtk = pkmData[37];
                EffortDef = pkmData[38];
                EffortSpeed = pkmData[39];
                EffortSpAtk = pkmData[40];
                EffortSpDef = pkmData[41];
                field_2A = pkmData[42];
                Familiarity = pkmData[43];
                Pokeball = pkmData[44];
                Level = pkmData[45];
                CassetteVersion = pkmData[46];
                LangId = pkmData[47];

                //IVs = new int[6];

                //split the IVs
                string IV = Convert.ToString(IvFlags, 2);
                Console.WriteLine(IV);
                for (int i = 0; i < 30 - IV.Length; i++)
                {
                    IV = "0" + IV;
                }

                int[] IVTemp = new int[6];
                char[] IVBits = IV.ToCharArray();

                for (int i = 0; i < 6; i++)
                {
                    IVTemp[i] = Convert.ToInt32(new string(IVBits.Skip(i * 5).Take(5).ToArray()), 2);
                }

                IVs = new int[6]{
                    IVTemp[5],
                    IVTemp[4],
                    IVTemp[3],
                    IVTemp[1],
                    IVTemp[0],
                    IVTemp[2]
                };
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public string ToShowdownFormat(bool HT)
        {
            string HTFlags = Convert.ToString(HyperTrainingFlags, 2);
            string[] IVString = new string[6];
            for (int i = 0; i < 30 - HTFlags.Length; i++)
            {
                HTFlags = "0" + HTFlags;
            }

            char[] chars = HTFlags.Reverse().ToArray();

            for(int i=0;i<6;i++)
            {
                if (chars[i] == '1') {
                    if (HT)
                    {
                        IVString[i] = IVs[i].ToString() + "(HT)";
                    }
                    else
                    {
                        IVString[i] = "31";
                    }
                }else
                {
                    IVString[i] = IVs[i].ToString();
                }
            }

            string[] format =
            {
                DataFetch.getSpecies(MonsNo,0) + /*" (%Gender)*/" @ "+DataFetch.getItem(HoldItem),
                "Ability: "+AbilityFlags,
                "Level: "+Level,
                "Happiness: 0",
                "EVs: " + EffortHp + " HP / " + EffortAtk + " Atk / " + EffortDef + " Def / " + EffortSpAtk + " SpA / " + EffortSpDef + " SpD / " + EffortSpeed + " Spe",
                DataFetch.getNature(Nature) + " Nature",
                "IVs: " + IVString[0] + " HP / " + IVString[1] + " Atk / " + IVString[2] + " Def / " + IVString[3] + " SpA / " + IVString[4] + " SpD / " + IVString[5] + " Spe ",
                "- " + DataFetch.getMove(Moves[0]),
                "- " + DataFetch.getMove(Moves[1]),
                "- " + DataFetch.getMove(Moves[2]),
                "- " + DataFetch.getMove(Moves[3])
            };

            return string.Join("\n", format);
        }

        public string getStatsData()
        {
            string HTFlags = Convert.ToString(HyperTrainingFlags, 2);
            for (int i = 0; i < 30 - HTFlags.Length; i++)
            {
                HTFlags = "0" + HTFlags;
            }

            char[] chars = HTFlags.Reverse().ToArray();
            string HT = "HT: ";

            for (int i = 0; i < 6; i++)
            {
                if (i != 0)
                {
                    HT += "/";
                }
                if (chars[i] == '1')
                {
                    HT+= "HT";
                }
                else
                {
                    HT+= "X";
                }
                
            }

            string[] format =
            {
                "Item: " + DataFetch.getItem(HoldItem),
                HT,
                "EVs: " + EffortHp + "H " + EffortAtk + "A " + EffortDef + "B " + EffortSpAtk + "C " + EffortSpDef + "D " + EffortSpeed + "S",
                "IVs: " + IVs[0] + "/" + IVs[1] + "/" + IVs[2] + "/" + IVs[3] + "/" + IVs[4] + "/" + IVs[5]
            };

            return string.Join("\n", format);
        }

        public string getMovesString()
        {
            string[] format =
            {
                " - " + DataFetch.getMove(Moves[0]),
                " - " + DataFetch.getMove(Moves[1]),
                " - " + DataFetch.getMove(Moves[2]),
                " - " + DataFetch.getMove(Moves[3])
            };

            return string.Join("\n", format);
        }

        public override string ToString()
        {
            try
            {
                //return "Pokemon is: " + DataFetch.getSpecies(MonsNo,0) + "@ Item ID: " + DataFetch.getItem(HoldItem) + ". \nAbility: "+ AbilityFlags +"\nHas moves: " + string.Join(", ", Array.ConvertAll(Moves, (a) => DataFetch.getMove(a)));
                return "\n" + ToShowdownFormat(true) + "\n";
            } catch(Exception e)
            {
                return "No Pokemon in this Slot";
            }
        }
    }
}
