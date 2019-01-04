using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HeraScratch.Objects
{

    [DataContract(Name = "block")]
    public class ScratchBlock
    {
        public string Id { get; set; }

        //opcode
        [DataMember(Name = "opcode")]
        public string BlockName { get; set; }

        //next
        [DataMember(Name = "next")]
        public string Next { get; set; }
        public ScratchBlock NextBlock { get; set; }

        //parent
        [DataMember(Name = "parent")]
        public string Parent { get; set; }
        public ScratchBlock ParentBlock { get; set; }

        public string Child { get; set; }
        public ScratchBlock ChildBlock { get; set; }

        [DataMember(Name = "topLevel")]
        public bool TopLevel { get; set; }

    }
}
