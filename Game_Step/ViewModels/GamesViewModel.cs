using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_Step.ViewModels
{
    public class GamesViewModel
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public string Features { get; set; }
        public string Region { get; set; }
        public string WhereKeyActivated { get; set; }

        public string MinOC { get; set; }
        public string MinCPU { get; set; }
        public string MinRAM { get; set; }
        public string MinVideoCard { get; set; }
        public string MinDirectX { get; set; }
        public string MinHDD { get; set; }

        public string RecOC { get; set; }
        public string RecCPU { get; set; }
        public string RecRAM { get; set; }
        public string RecVideoCard { get; set; }
        public string RecDirectX { get; set; }
        public string RecHDD { get; set; }
    }
}
