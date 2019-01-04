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
        private static Dictionary<string, string> _NonStackBlocks
            = new Dictionary<string, string>()
            {
                ["sensing_touchingobject"] = "sensing_touchingobject",
                ["sensing_touchingobjectmenu"] = "sensing_touchingobjectmenu",
                ["sensing_touchingcolor"] = "sensing_touchingcolor",
                ["sensing_coloristouchingcolor"] = "sensing_coloristouchingcolor",
                ["sensing_keypressed"] = "sensing_keypressed",
                ["sensing_keyoptions"] = "sensing_keyoptions",
                ["operator_gt"] = "operator_gt",
                ["operator_lt"] = "operator_lt",
                ["operator_equals"] = "operator_equals",
                ["operator_and"] = "operator_and",
                ["operator_or"] = "operator_or",
                ["operator_not"] = "operator_not",
                ["operator_contains"] = "operator_contains",
                //variables
                ["motion_xposition"] = "motion_xposition",
                ["motion_yposition"] = "motion_yposition",
                ["motion_direction"] = "motion_direction",
                ["looks_costumenumbername"] = "looks_costumenumbername",
                ["looks_backdropnumbername"] = "looks_backdropnumbername",
                ["looks_size"] = "looks_size",
                ["sound_volume"] = "sound_volume",
                ["sensing_answer"] = "sensing_answer",
                ["sensing_mousex"] = "sensing_mousex",
                ["sensing_mousey"] = "sensing_mousey",
                ["sensing_loudness"] = "sensing_loudness",
                ["sensing_timer"] = "sensing_timer",
                ["sensing_current"] = "sensing_current",
                ["sensing_dayssince2000"] = "sensing_dayssince2000",
                ["sensing_username"] = "sensing_username",
                ["operator_mod"] = "operator_mod",
                ["operator_random"] = "operator_random",
                ["operator_letter_of"] = "operator_letter_of",
                ["operator_join"] = "operator_join",
                ["operator_length"] = "operator_length",
                ["operator_round"] = "operator_round",
                ["operator_mathop"] = "operator_mathop"
            };

        private static Dictionary<string, string> _NonUserVariables
            = new Dictionary<string, string>()
            {
                ["motion_xposition"] = "motion_xposition",
                ["motion_yposition"] = "motion_yposition",
                ["motion_direction"] = "motion_direction",
                ["looks_costumenumbername"] = "looks_costumenumbername",
                ["looks_backdropnumbername"] = "looks_backdropnumbername",
                ["looks_size"] = "looks_size",
                ["sound_volume"] = "sound_volume",
                ["sensing_answer"] = "sensing_answer",
                ["sensing_mousex"] = "sensing_mousex",
                ["sensing_mousey"] = "sensing_mousey",
                ["sensing_loudness"] = "sensing_loudness",
                ["sensing_timer"] = "sensing_timer",
                ["sensing_current"] = "sensing_current",
                ["sensing_dayssince2000"] = "sensing_dayssince2000",
                ["sensing_username"] = "sensing_username",
                ["operator_mod"] = "operator_mod",
                ["operator_random"] = "operator_random",
                ["operator_letter_of"] = "operator_letter_of",
                ["operator_join"] = "operator_join",
                ["operator_length"] = "operator_length",
                ["operator_round"] = "operator_round",
                ["operator_mathop"] = "operator_mathop"
            };

        private static Dictionary<string, string> _BasicOperators
            = new Dictionary<string, string>()
            {
                ["operator_add"] = "operator_add",
                ["operator_subtract"] = "operator_subtract",
                ["operator_multiply"] = "operator_multiply",
                ["operator_divide"] = "operator_divide",
                ["operator_join"] = "operator_join"
            };

        private static Dictionary<string, string> _MediumOperators
            = new Dictionary<string, string>()
            {
                ["operator_gt"] = "operator_gt",
                ["operator_equals"] = "operator_equals",
                ["operator_lt"] = "operator_lt",
                ["operator_random"] = "operator_random",
                ["operator_and"] = "operator_and",
                ["operator_or"] = "operator_or",
                ["operator_not"] = "operator_not"
            };
        private static Dictionary<string, string> _SensingBlocks
            = new Dictionary<string, string>()
            {
                ["sensing_keypressed"] = "sensing_keypressed",
                ["event_whenkeypressed"] = "event_whenkeypressed",
                ["sensing_touchingobject"] = "sensing_touchingobject"
            };

        private static Dictionary<string, string> _ThreadingBlocks
            = new Dictionary<string, string>()
            {
                ["event_whenflagclicked"] = "event_whenflagclicked",
                ["event_whenkeypressed"] = "event_whenkeypressed",
                ["event_whenthisspriteclicked"] = "event_whenthisspriteclicked",
                ["whenIReceive"] = "whenIReceive",
                ["whenSceneStarts"] = "whenSceneStarts",
                ["whenSensorGreaterThan"] = "whenSensorGreaterThan",
                ["control_start_as_clone"] = "control_start_as_clone"
            };

        private static Dictionary<string, string> _EventBlocks
            = new Dictionary<string, string>()
            {
                ["event_whenflagclicked"] = "event_whenflagclicked",
                ["event_whenkeypressed"] = "event_whenkeypressed",
                ["event_whenthisspriteclicked"] = "event_whenthisspriteclicked",
                ["event_whengreaterthan"] = "event_whengreaterthan",
                ["event_whenbackdropswitchesto"] = "event_whenbackdropswitchesto",
                ["event_whenbroadcastreceived"] = "event_whenbroadcastreceived",
                ["control_start_as_clone"] = "control_start_as_clone",
                //["doBroadcastAndWait"] = "doBroadcastAndWait",
                //["procDef"] = "procDef",
                //["control_start_as_clone"] = "control_start_as_clone"
            };
        private static Dictionary<string, string> _BasicControlBlocks
            = new Dictionary<string, string>()
            {
                ["control_if"] = "control_if",
                ["control_stop"] = "control_stop",
                ["control_forever"] = "control_forever",
                ["control_repeat"] = "control_repeat",
                ["wait:elapsed:from:"] = "wait:elapsed:from:"
            };
        private static Dictionary<string, string> _MediumControlBlocks
            = new Dictionary<string, string>()
            {
                ["control_wait_until"] = "control_wait_until",
                ["control_repeat_until"] = "control_repeat_until",
                ["control_if_else"] = "control_if_else"
            };
        private static Dictionary<string, string> _ControlBlocks
            = new Dictionary<string, string>()
            {
                ["control_wait"] = "control_wait",
                ["control_repeat"] = "control_repeat",
                ["control_forever"] = "control_forever",
                ["control_if"] = "control_if",
                ["control_if_else"] = "control_if_else",
                ["control_wait_until"] = "control_wait_until",
                ["control_repeat_until"] = "control_repeat_until",
                ["control_stop"] = "control_stop",
                ["control_create_clone_of"] = "control_create_clone_of",
                ["control_delete_this_clone"] = "control_delete_this_clone"
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

        public static bool IsNonStackBlock(this ScratchObject _this,
            string name)
        {
            return _NonStackBlocks.ContainsKey(name);
        }

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

        public static T SpriteValoration<T, U>(this ScratchObject sprite,
            string objectName, bool general = false)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
        {
            return default(T);
        }


        public static T GeneralEvaluation<T, U, S>(
            this ScratchProject proj, string objectName,
            List<U> previousValorations)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
            where S : IGeneralValoration, new()
        {
            //if (obj.Children == null)
            //    return (T)ValorationHelper
            //        .Get_DefaultSingle<T, U>(objectName);

            var blocks = new List<string>();
            var scripts = new List<List<object>>();
            var scriptList = new List<string>();
            var messagesRecieved = new List<string>();
            var messagesSent = new List<string>();
            
            foreach (var child in proj.Targets)
            {
                if (child.Blocks != null)
                {
                    blocks.AddRange(child.Blocks);
                }
                
            }
            var vars = new List<Variable>();
            var lists = new List<ScratchList>();

            return Get_generalValoration<T, U, S>(proj.Targets, previousValorations);

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

        public static T Get_singleValoration<T, U>(this ScratchObject sprite)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
        {

            var threadCount = sprite.Scripts.Where(s =>
            _ThreadingBlocks.Any(b => b.Key.Equals(s.BlockName))).Count();
            var deadCodeCount = sprite.Scripts.Where(b => !_EventBlocks.ContainsKey(b.BlockName)).Count();
            var loopRegex = new Regex(@"(^control_repeat)|(^control_repeat_until)|(^control_wait_until)|(^control_forever)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var nestedRegex = new Regex(@"(^control_repeat)|(^control_repeat_until)|(^control_wait_until)|(^control_forever)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);


            return new T()
            {
                generalValoration = false, //TODO:Review

                SpriteName = sprite.Name,
                ScriptCount = sprite.Scripts.Count,
                BlockCount = sprite.Blocks.Count,
                DeadCodeCount = deadCodeCount,

                BlockFrequency = sprite.BlocksDictionary.Values.GroupBy(b => b.BlockName)
                .Select(b => new Tuple<string, int>(
                    b.Key,
                    b.Count()))
                .OrderByDescending(i => i.Item2)
                .ToList(),

                DuplicateScriptCount = sprite.ScriptsString
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
                    sprite.BlockNames.Any(b => b == "procedures_definition"),
                    CloneUse =
                     sprite.BlockNames.Any(b => b == "control_create_clone_of"),

                    //Algorithmic thinking
                    SecuenceUse =
                        sprite.Scripts.Any(b =>
                        _EventBlocks.ContainsKey(b.BlockName)
                        && b.NextBlock != null),


                    //Sync
                    MultipleThreads =
                    threadCount > 1,

                    TwoGreenFlagTrhead =
                    sprite.Scripts.Where(s =>
                    s.BlockName.Equals("event_whenflagclicked"))
                    .Count() > 1,

                    //TODO: Review
                    AdvancedEventUse =
                    sprite.Scripts.GroupBy(s
                        => s.BlockName)
                        .Count() > 1,

                    //Flux control
                    UseSimpleBlocks =
                    sprite.BlockNames.Any(b => _BasicControlBlocks.ContainsKey(b)),
                    UseMediumBlocks =
                    sprite.BlockNames.Any(b => _MediumControlBlocks.ContainsKey(b)),

                    //TODO: Review
                    UseNestedControl = false, //nestedControl,

                    //Input
                    BasicInputUse = sprite.BlockNames
                        .Any(b => _SensingBlocks.ContainsKey(b)),
                    VariableUse = sprite.BlockNames
                        .Any(b => _NonUserVariables.ContainsKey(b)
                        ),
                    SpriteSensing = sprite.BlockNames
                    .Any(b => b == "touching:" || b == "touching"),

                    //Analysis
                    BasicOperators = sprite.BlockNames
                    .Any(b => _BasicOperators.ContainsKey(b)),

                    MediumOperators = sprite.BlockNames
                    .Any(b => _MediumOperators.ContainsKey(b)),
                    //TODO: Review
                    AdvancedOperators = false,

                    ThreadCount = threadCount,
                    CloneCount = sprite.BlockNames.Where(b => b.Equals("control_create_clone_of")).Count(),
                    CloneRemovalCount = sprite.BlockNames.Where(b => b == "deleteClone").Count(),
                    SequentialLoopsCount = sprite.ScriptsString.Select(item => loopRegex.Matches(item).Count).Sum()

                }
            };
        }


        private static T Get_generalValoration<T, U, S>
            (List<ScratchObject> sprites,
            List<U> previousValorations,
            bool general = true)
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
                SpriteName = "General",
                ScriptCount = sprites.Sum(s => s.Scripts.Count),
                BlockCount = sprites.Sum(s => s.Blocks.Count),
                
                DeadCodeCount = sprites.SelectMany(s => s.Scripts).Where(b => !_EventBlocks.ContainsKey(b.BlockName)).Count(),

                BlockFrequency = sprites.SelectMany(s => s.BlockNames).GroupBy(b => b)
                .Select(b => new Tuple<string, int>(
                    b.Key,
                    b.Count()))
                .OrderByDescending(i => i.Item2)
                .ToList(),
                DuplicateScriptCount = sprites.SelectMany(s => s.ScriptsString)
                .GroupBy(i => i)
                .Where(grp => grp.Count() > 1)
                .Select(grp => grp.Key)
                .Count(),

                AdditionalInfo = new S()
                {
                    SpriteCount = sprites.Count,
                    //General Variables
                    EventsUse = previousValorations
                    .Where(val => val.HasEvents).Count() > 1,
                    SharedVariables = false,
                    MessageUse = sprites.SelectMany(s => s.BlockNames).Any(b => b.Equals("event_whenbroadcastreceived")),
                    ListUse = false,

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

