using HeraScratch.ObjectExtensions;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HeraScratch.Objects
{
    [DataContract(Name = "object")]
    public class ScratchObject : IHttpObject
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isStage")]
        public bool IsStage { get; set; }

        public Dictionary<string, ScratchBlock> BlocksDictionary { get; set; }

        public List<ScratchBlock> Scripts { get; set; }
        public List<string> ScriptsString { get; set; }

        public List<string> Blocks { get => BlocksDictionary != null ? BlocksDictionary.Select(item => item.Key).ToList() : new List<string>(); }
        public List<string> BlockNames { get => BlocksDictionary != null ? BlocksDictionary.Values.Select(item => item.BlockName).ToList() : new List<string>(); }

        public void Initialize()
        {
            if (BlocksDictionary == null)
            {
                BlocksDictionary = new Dictionary<string, ScratchBlock>();
                Scripts = new List<ScratchBlock>();
                ScriptsString = new List<string>();
                return;
            }
                

            foreach (var key in BlocksDictionary.Keys)
            {
                if (!string.IsNullOrWhiteSpace(BlocksDictionary[key].Next) &&
                    BlocksDictionary.ContainsKey(BlocksDictionary[key].Next))
                {
                    BlocksDictionary[key].NextBlock = BlocksDictionary[BlocksDictionary[key].Next];
                }

                if (!string.IsNullOrWhiteSpace(BlocksDictionary[key].Parent) &&
                    BlocksDictionary.ContainsKey(BlocksDictionary[key].Parent))
                {
                    BlocksDictionary[key].ParentBlock = BlocksDictionary[BlocksDictionary[key].Parent];
                }
            }

            foreach (var key in BlocksDictionary.Keys)
            {
                var children = BlocksDictionary.Values.Where(b => !
                    string.IsNullOrWhiteSpace(b.Parent) && b.Parent.Equals(key))
                    .ToList();
                if (BlocksDictionary[key].Next == null && children.Count == 1)
                {
                    var child = children.First();
                    BlocksDictionary[key].Child = child.Id;
                    BlocksDictionary[key].ChildBlock = BlocksDictionary[child.Id];
                }

                if (children.Count > 1)
                {
                    var child = children.FirstOrDefault(item => !this.IsNonStackBlock(item.BlockName) && !item.Id.Equals(BlocksDictionary[key].Next));
                    if(child != null)
                    {
                        BlocksDictionary[key].Child = child.Id;
                        BlocksDictionary[key].ChildBlock = BlocksDictionary[child.Id];
                    }
                }
            }
            Scripts = BlocksDictionary.Values.Where(b => b.TopLevel).ToList();
            ScriptsString = new List<string>();
            foreach (var script in Scripts)
            {
                ScriptsString.Add(getScriptString(script));
            }
        }

        #region private methods

        public string getScriptString(ScratchBlock script, string depth = "")
        {
            var res = $"{depth}{script.BlockName}";

            if(script.ChildBlock != null)
            {
                res = $"{res}\n{getScriptString(script.ChildBlock, $">{depth}")}";
            }

            if (script.NextBlock != null)
            {
                res =  $"{res}\n{getScriptString(script.NextBlock)}";
            }
            return res;
        }

        #endregion

        #region Old definition

        //public List<string> ScriptsString { get; set; }

        //public List<string> MessagesSent { get; set; }
        //public List<string> MessagesRecieved { get; set; }

        //public bool NestedControl { get; set; }
        //public bool NestedOperator { get; set; }

        //[DataMember(Name = "_variables")]
        //public IEnumerable<Variable> Variables { get; set; }

        //[DataMember(Name = "lists")]
        //public IEnumerable<ScratchList> Lists { get; set; }



        #endregion

    }
}
