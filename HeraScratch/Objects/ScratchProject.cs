using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HeraScratch.Objects
{
    [DataContract(Name = "project")]
    public class ScratchProject: IHttpObject
    {
        [DataMember(Name = "targets")]
        public List<ScratchObject> Targets { get; set; }

        public void Initialize()
        {
        }
    }
}
