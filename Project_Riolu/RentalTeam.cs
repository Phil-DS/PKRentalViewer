using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Riolu
{
    public class RentalTeam
    {
        public List<Pokemon> team;
        public byte[] GLid;
        public byte[] UnknownData;

        public RentalTeam(byte[] data){
            Console.WriteLine(data.Length);
            team = new List<Pokemon>();
            team.Add(new Pokemon(data.Take(0x30).ToArray()));
            team.Add(new Pokemon(data.Skip(0x30).Take(0x30).ToArray()));
            team.Add(new Pokemon(data.Skip(0x60).Take(0x30).ToArray()));
            team.Add(new Pokemon(data.Skip(0x90).Take(0x30).ToArray()));
            team.Add(new Pokemon(data.Skip(0xC0).Take(0x30).ToArray()));
            team.Add(new Pokemon(data.Skip(0xF0).Take(0x30).ToArray()));

            foreach(Pokemon p in team)
            {
                Console.WriteLine(p.ToShowdownFormat(true)+"\n");
            }

            GLid = data.Skip(0x120).Take(8).ToArray();
            UnknownData = data.Skip(0x128).ToArray();
        }
    }
}
