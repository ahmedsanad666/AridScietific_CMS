using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class BlocksViewModel
    {
        public IEnumerable<Block> Blocks { get; set; }
        public IEnumerable<BlockItem> BlockItems { get; set; }
    }
}
