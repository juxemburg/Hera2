using HeraScratch.Objects;
using HeraScratch.ObjectExtensions;
using HeraScratch.Valoration;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using HeraScratch.Exceptions;
using Newtonsoft.Json.Linq;

namespace HeraScratch
{
    public class Evaluator
    {
        private Client _client;

        public Evaluator()
        {
            _client = new Client("https://projects.scratch.mit.edu");
        }
        public async Task<IEnumerable<T>> Evaluate<T, U, S>(string proyectId)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
            where S : IGeneralValoration, new()
        {
            try
            {
                //TODO: Review
                var result = await _client.GetDynamic<ScratchProject>(
                "",
                (json, resource) =>
                {
                    var index = 0;
                    foreach (var target in json["targets"])
                    {
                        if (resource.Targets.Count > index)
                        {
                            resource.Targets[index].BlocksDictionary = new Dictionary<string, ScratchBlock>();
                        }

                        var blocks = target["blocks"];
                        foreach (var property in blocks.Children<JProperty>())
                        {
                            var sobject = property.Value.ToObject<ScratchBlock>();
                            sobject.Id = property.Name;
                            resource.Targets[index].BlocksDictionary.Add(sobject.Id, sobject);
                        }
                        resource.Targets[index].Initialize();
                        index++;
                    }
                    resource.Initialize();
                    return resource;
                },proyectId);


                var list = result
                    .Targets
                    .Select(sprite => sprite.Get_singleValoration<T, U>());
                var previousList =
                    list.Select(item => (U)item.AdditionalInfo)
                    .ToList();
                var gEval =
                    result.GeneralEvaluation<T, U, S>("General",
                    previousList);
                list = list.Append(gEval);

                return list;
            }
            catch (Exception inner)
            {
                throw new EvaluationException("Proyect not found", inner);
            }
        }

        public async Task<T> GeneralEvaluate<T, U, S>(string proyectId)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
            where S : IGeneralValoration, new()
        {
            try
            {
                var result = await Evaluate<T, U, S>(proyectId);
                return result.First(r => r.SpriteName == "General");
            }
            catch (EvaluationException)
            {
                throw;
            }
        }
    }
}
