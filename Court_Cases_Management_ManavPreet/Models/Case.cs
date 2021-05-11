using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Court_Cases_Management_ManavPreet.Models
{
    public class Case
    {
        public int ID { get; set; }
        public int JudgeID { get; set; }
        public int LawyerID { get; set; }
        public int PartyID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Judge Judge { get; set; }
        public Lawyer Lawyer { get; set; }
        public Party Party { get; set; }

        public List<Hearing> Hearings { get; set; }
    }
}
