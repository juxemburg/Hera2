using System;
using System.Linq;
using System.Collections.Generic;
using HeraScratch.Valoration;
using HeraScratch.Objects;
using System.Text.RegularExpressions;

namespace HeraScratch.ObjectExtensions
{
    static class ScratchObjectExtensions
    {
        private static Dictionary<string, string> _NonUserVariables
            = new Dictionary<string, string>()
            {
                ["answer"] = "answer",
                ["soundLevel"] = "soundLevel",
                ["timer"] = "timer",
                ["timeAndDate"] = "timeAndDate",
                ["senseVideoMotion"] = "senseVideoMotion",
                ["xpos"] = "",
                ["ypos"] = "",
                ["heading"] = "",
                ["costumeIndex"] = "",
                ["sceneName"] = "",
                ["scale"] = "",
                ["volume"] = "",
                ["tempo"] = ""
            };

        private static Dictionary<string, string> _BasicOperators
            = new Dictionary<string, string>()
            {
                ["+"] = "+",
                ["-"] = "-",
                ["*"] = "*",
                ["/"] = "/",
                ["concatenate:with:"] = "concatenate:with:"
            };

        private static Dictionary<string, string> _MediumOperators
            = new Dictionary<string, string>()
            {
                ["<"] = "<",
                ["="] = "=",
                [">"] = ">",
                ["randomFrom:to:"] = "randomFrom:to:",
                ["&"] = "&",
                ["|"] = "|",
                ["not"] = "not"
            };
        private static Dictionary<string, string> _SensingBlocks
            = new Dictionary<string, string>()
            {
                ["keyPressed"] = "",
                ["whenKeyPressed"] = "",
                ["touching:"] = ""
            };

        private static Dictionary<string, string> _ThreadingBlocks
            = new Dictionary<string, string>()
            {
                ["whenGreenFlag"] = "whenGreenFlag",
                ["whenKeyPressed"] = "whenKeyPressed",
                ["whenClicked"] = "whenClicked",
                ["whenIReceive"] = "whenIReceive",
                ["whenSceneStarts"] = "whenSceneStarts",
                ["whenSensorGreaterThan"] = "whenSensorGreaterThan",
                ["whenCloned"] = "whenCloned"
            };

        private static Dictionary<string, string> _EventBlocks
            = new Dictionary<string, string>()
            {
                ["whenGreenFlag"] = "whenGreenFlag",
                ["whenKeyPressed"] = "whenKeyPressed",
                ["whenClicked"] = "whenClicked",
                ["whenIReceive"] = "whenIReceive",
                ["whenSceneStarts"] = "whenSceneStarts",
                ["whenSensorGreaterThan"] = "whenSensorGreaterThan",
                ["broadcast:"] = "broadcast:",
                ["doBroadcastAndWait"] = "doBroadcastAndWait",
                ["procDef"] = "procDef",
                ["whenCloned"] = "whenCloned"
            };
        private static Dictionary<string, string> _BasicControlBlocks
            = new Dictionary<string, string>()
            {
                ["doIf"] = "doIf",
                ["stopScripts"] = "stopScripts",
                ["doForever"] = "doForever",
                ["doRepeat"] = "doRepeat",
                ["wait:elapsed:from:"] = "wait:elapsed:from:"
            };
        private static Dictionary<string, string> _MediumControlBlocks
            = new Dictionary<string, string>()
            {
                ["doWaitUntil"] = "doWaitUntil",
                ["doUntil"] = "doUntil",
                ["doIfElse"] = "doIfElse"
            };
        private static Dictionary<string, string> _ControlBlocks
            = new Dictionary<string, string>()
            {
                ["doWaitUntil"] = "doWaitUntil",
                ["doUntil"] = "doUntil",
                ["doIfElse"] = "doIfElse",
                ["doIf"] = "doIf",
                ["doForever"] = "doForever",
                ["doRepeat"] = "doRepeat"
            };
        private static Dictionary<string, string> _reservedBlocks
            = new Dictionary<string, string>()
            {
                { "a","" },
                { "b","" },
                { "c","" },
                { "d","" },
                { "e","" },
                { "f","" },
                { "g","" },
                { "h","" },
                { "i","" },
                { "j","" },
                { "k","" },
                { "l","" },
                { "m","" },
                { "n","" },
                { "o","" },
                { "p","" },
                { "q","" },
                { "r","" },
                { "s","" },
                { "t","" },
                { "u","" },
                { "v","" },
                { "x","" },
                { "y","" },
                { "z","" },
                { "up arrow",""},
                { "right arrow",""},
                { "left arrow",""},
                { "down arrow",""}

            };

        public static bool IsControlBlock(this ScratchObject _this,
            string name)
        {
            return _ControlBlocks.ContainsKey(name);
        }
        public static bool IsOperatorBlock(this ScratchObject _this,
            string name)
        {
            return _BasicOperators.ContainsKey(name) ||
                _MediumOperators.ContainsKey(name);
        }

        public static bool IsReservedBlock(string name)
        {
            return _reservedBlocks.ContainsKey(name);
        }
        public static T Evaluate<T, U, S>(this ScratchObject obj,
            string objectName, bool general = false)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
            where S : IGeneralValoration, new()
        {
            if (obj.RawScripts == null)
            {
                return (T)ValorationHelper
                    .Get_DefaultSingle<T, U>(objectName);
            }

            return Get_singleValoration<T, U>(obj.Scripts,
                obj.Blocks,
                obj.ScriptsString, objectName, obj.DeadCodeCount,
                obj.NestedControl, obj.NestedOperator, general);
        }
        public static T GeneralEvaluation<T, U, S>(
            this ScratchObject obj, string objectName,
            List<U> previousValorations)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
            where S : IGeneralValoration, new()
        {
            if (obj.Children == null)
                return (T)ValorationHelper
                    .Get_DefaultSingle<T, U>(objectName);

            var blocks = new List<string>();
            var scripts = new List<List<object>>();
            var scriptList = new List<string>();
            var messagesRecieved = new List<string>();
            var messagesSent = new List<string>();
            var deadCodeSums = 0;
            if (obj.RawScripts != null)
            {
                blocks.AddRange(obj.Blocks);
                scripts.AddRange(obj.Scripts);
                scriptList.AddRange(obj.ScriptsString);
                messagesRecieved.AddRange(obj.MessagesRecieved);
                messagesSent.AddRange(obj.MessagesSent);
            }
            foreach (var child in obj.Children)
            {
                deadCodeSums += child.DeadCodeCount;
                if (child.Blocks != null)
                {
                    blocks.AddRange(child.Blocks);
                }
                if (child.Scripts != null)
                {
                    scripts.AddRange(child.Scripts);
                }
                if (child.ScriptsString != null)
                {
                    scriptList.AddRange(child.ScriptsString);
                }
                if (child.MessagesRecieved != null)
                {
                    messagesRecieved.AddRange(child.MessagesRecieved);
                }
                if (child.MessagesSent != null)
                {
                    messagesSent.AddRange(child.MessagesSent);
                }
            }
            var vars = obj.Variables != null ? obj.Variables.ToList() :
                new List<Variable>();
            var lists = obj.Lists != null ? obj.Lists.ToList() :
                new List<ScratchList>();
            return Get_generalValoration<T, U, S>(scripts,
                blocks, scriptList, objectName, previousValorations,
                vars, lists, deadCodeSums, previousValorations.Count,
                messagesRecieved, messagesSent, true);

        }

        private static string Get_firstBlock(List<object> script)
        {
            var i = ((IEnumerable<object>)script[0]).First();
            var item = ((IEnumerable<object>)i).FirstOrDefault();
            if (item != null &&
                typeof(string) == item.GetType())
            {
                var value = !_EventBlocks.ContainsKey(item.ToString());
                return item.ToString();
            }
            return "unknown";

        }

        public static int Get_ScriptLength(object[] script)
        {
            return script.Length;
        }

        public static bool Get_ValidScript(object[] script)
        {
            try
            {
                if (script.Length < 0)
                    return false;
                if (script[0] is string)
                    return _EventBlocks.ContainsKey(script[0].ToString());

                return Get_ValidScript((object[])script[0]);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static T Get_singleValoration<T, U>
            (List<List<object>> scripts, List<string> blocks,
            List<string> scriptList, string objName,
            int deadCodeCount,
            bool nestedControl,
            bool nestedOperator,
            bool general = false)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
        {
            var threadCount = scripts.Where(s =>
                 _ThreadingBlocks.Any(b => b.Key.Equals(Get_firstBlock(s))))
                    .Count();
            var loopRegex = new Regex(@"(^doRepeat)|(^doUntil)|(^doWaitUntil)|(^doForever)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var nestedRegex = new Regex(@"(^doRepeat)|(^doUntil)|(^doWaitUntil)|(^doForever)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return new T()
            {
                generalValoration = general,
                SpriteName = objName,
                ScriptCount = scripts.Count,
                BlockCount = blocks.Count(),
                DeadCodeCount = deadCodeCount,

                BlockFrequency = blocks.GroupBy(b => b)
                .Select(b => new Tuple<string, int>(
                    b.Key,
                    b.Count()))
                .OrderByDescending(i => i.Item2)
                .ToList(),
                DuplicateScriptCount = scriptList
                .GroupBy(i => i)
                .Where(grp => grp.Count() > 1)
                .Select(grp => grp.Key)
                .Count(),


                AdditionalInfo = new U()
                {
                    HasEvents = threadCount > 0,

                    //Abstraction
                    NonUnusedBlocks = deadCodeCount == 0,
                    UserDefinedBlocks =
                    blocks.Any(b => b == "procDef"),
                    CloneUse =
                     blocks.Any(b => b == "createCloneOf"),

                    //Algorithmic thinking
                    SecuenceUse =
                        scripts.Any(list =>
                        _EventBlocks.ContainsKey(Get_firstBlock(list))
                        && list.Count > 0),


                    //Sync
                    MultipleThreads =
                    threadCount > 1,
                    TwoGreenFlagTrhead =
                    scripts.Where(s =>
                    Get_firstBlock(s).Equals("whenGreenFlag"))
                    .Count() > 1,


                    AdvancedEventUse =
                    scripts.GroupBy(s
                        => Get_firstBlock(s))
                        .Count() > 1,
                    //Flux control
                    UseSimpleBlocks =
                    blocks.Any(b => _BasicControlBlocks.ContainsKey(b)),
                    UseMediumBlocks =
                    blocks.Any(b => _MediumControlBlocks.ContainsKey(b)),
                    UseNestedControl = nestedControl,

                    //Input
                    BasicInputUse = blocks
                        .Any(b => _SensingBlocks.ContainsKey(b)),
                    VariableUse = blocks
                        .Any(b => _NonUserVariables.ContainsKey(b)
                        ),
                    SpriteSensing = blocks
                    .Any(b => b == "touching:" || b == "touching"),

                    //Analysis
                    BasicOperators = blocks
                    .Any(b => _BasicOperators.ContainsKey(b)),

                    MediumOperators = blocks
                    .Any(b => _MediumOperators.ContainsKey(b)),
                    AdvancedOperators = nestedOperator,

                    ThreadCount = threadCount,
                    CloneCount = blocks.Where(b => b.Equals("createCloneOf")).Count(),
                    CloneRemovalCount = blocks.Where(b => b == "deleteClone").Count(),
                    SequentialLoopsCount = scriptList.Select(item => loopRegex.Matches(item).Count).Sum()

                }
            };
        }


        private static T Get_generalValoration<T, U, S>
            (List<List<object>> scripts, List<string> blocks,
            List<string> scriptList, string objName,
            List<U> previousValorations,
            List<Variable> variables,
            List<ScratchList> lists,
            int deadCodeSum,
            int objCount,
            List<string> messagesRecieved,
            List<string> messagesSent,
            bool general = false)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
            where S : IGeneralValoration, new()
        {
            var previousAggregate =
                previousValorations
                .Select(item => new
                {
                    NonUnusedBlocks = item.NonUnusedBlocks ? 1 : 0,
                    UserDefinedBlocks = item.UserDefinedBlocks ? 1 : 0,
                    CloneUse = item.CloneUse ? 1 : 0,
                    SecuenceUse = item.SecuenceUse ? 1 : 0,
                    MultipleThreads = item.MultipleThreads ? 1 : 0,
                    TwoGreenFlagTrhead = item.TwoGreenFlagTrhead ? 1 : 0,
                    AdvancedEventUse = item.AdvancedEventUse ? 1 : 0,
                    UseSimpleBlocks = item.UseSimpleBlocks ? 1 : 0,
                    UseMediumBlocks = item.UseMediumBlocks ? 1 : 0,
                    UseNestedControl = item.UseNestedControl ? 1 : 0,
                    BasicInputUse = item.BasicInputUse ? 1 : 0,
                    VariableUse = item.VariableUse ? 1 : 0,
                    SpriteSensing = item.SpriteSensing ? 1 : 0,
                    BasicOperators = item.BasicOperators ? 1 : 0,
                    MediumOperators = item.MediumOperators ? 1 : 0,
                    AdvancedOperators = item.AdvancedOperators ? 1 : 0,
                    item.ThreadCount,
                    item.CloneCount,
                    item.CloneRemovalCount,
                    item.SequentialLoopsCount
                })
                .Aggregate((acc, item) => new
                {
                    NonUnusedBlocks = item.NonUnusedBlocks + acc.NonUnusedBlocks,
                    UserDefinedBlocks = item.UserDefinedBlocks + acc.UserDefinedBlocks,
                    CloneUse = item.CloneUse + acc.CloneUse,
                    SecuenceUse = item.SecuenceUse + acc.SecuenceUse,
                    MultipleThreads = item.MultipleThreads + acc.MultipleThreads,
                    TwoGreenFlagTrhead = item.TwoGreenFlagTrhead + acc.TwoGreenFlagTrhead,
                    AdvancedEventUse = item.AdvancedEventUse + acc.AdvancedEventUse,
                    UseSimpleBlocks = item.UseSimpleBlocks + acc.UseSimpleBlocks,
                    UseMediumBlocks = item.UseMediumBlocks + acc.UseMediumBlocks,
                    UseNestedControl = item.UseNestedControl + acc.UseNestedControl,
                    BasicInputUse = item.BasicInputUse + acc.BasicInputUse,
                    VariableUse = item.VariableUse + acc.VariableUse,
                    SpriteSensing = item.SpriteSensing + acc.SpriteSensing,
                    BasicOperators = item.BasicOperators + acc.BasicOperators,
                    MediumOperators = item.MediumOperators + acc.MediumOperators,
                    AdvancedOperators = item.AdvancedOperators + acc.AdvancedOperators,
                    ThreadCount = item.ThreadCount + acc.ThreadCount,
                    CloneCount = item.CloneCount + acc.CloneCount,
                    CloneRemovalCount = item.CloneRemovalCount + acc.CloneRemovalCount,
                    SequentialLoopsCount = item.SequentialLoopsCount + acc.SequentialLoopsCount
                });

            return new T()
            {

                generalValoration = general,
                SpriteName = objName,
                ScriptCount = scripts.Count,
                BlockCount = blocks.Count(),
                DeadCodeCount = deadCodeSum,

                BlockFrequency = blocks.GroupBy(b => b)
                .Select(b => new Tuple<string, int>(
                    b.Key,
                    b.Count()))
                .OrderByDescending(i => i.Item2)
                .ToList(),
                DuplicateScriptCount = scriptList
                .GroupBy(i => i)
                .Where(grp => grp.Count() > 1)
                .Select(grp => grp.Key)
                .Count(),

                AdditionalInfo = new S()
                {
                    SpriteCount = objCount,
                    //General Variables
                    EventsUse = previousValorations
                    .Where(val => val.HasEvents).Count() > 1,
                    SharedVariables = variables.Count > 0,
                    MessageUse = messagesRecieved
                    .All(m => messagesSent.Contains(m))
                    && blocks.Any(b => b.Equals("broadcast:"))
                    && blocks.Any(b => b.Equals("whenIReceive")),
                    ListUse = lists.Count > 0,

                    //Particular Variables
                    NonUnusedBlocks = previousAggregate.NonUnusedBlocks,
                    UserDefinedBlocks = previousAggregate.UserDefinedBlocks,
                    CloneUse = previousAggregate.CloneUse,
                    SecuenceUse = previousAggregate.SecuenceUse,
                    MultipleThreads = previousAggregate.MultipleThreads,
                    TwoGreenFlagTrhead = previousAggregate.TwoGreenFlagTrhead,
                    AdvancedEventUse = previousAggregate.AdvancedEventUse,
                    UseSimpleBlocks = previousAggregate.UseSimpleBlocks,
                    UseMediumBlocks = previousAggregate.UseMediumBlocks,
                    UseNestedControl = previousAggregate.UseNestedControl,
                    BasicInputUse = previousAggregate.BasicInputUse,
                    VariableUse = previousAggregate.VariableUse,
                    SpriteSensing = previousAggregate.SpriteSensing,
                    BasicOperators = previousAggregate.BasicOperators,
                    MediumOperators = previousAggregate.MediumOperators,
                    AdvancedOperators = previousAggregate.AdvancedOperators,
                    ThreadCount = previousAggregate.ThreadCount,
                    CloneCount = previousAggregate.CloneCount,
                    CloneRemovalCount = previousAggregate.CloneRemovalCount,
                    SequentialLoopsCount = previousAggregate.SequentialLoopsCount
                }
            };
        }

    }

}

