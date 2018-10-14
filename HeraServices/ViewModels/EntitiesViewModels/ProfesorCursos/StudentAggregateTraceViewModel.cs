using System;
using System.Collections.Generic;
using System.Text;

namespace HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos
{
    public class StudentAggregateTraceViewModel
    {
        public int Count { get; set; }
        public int SpriteCountAvg { get; set; }
        public int NonUnusedBlocksAvg { get; set; }
        public int UserDefinedBlocksAvg { get; set; }
        public int CloneUseAvg { get; set; }
        public int SecuenceUseAvg { get; set; }
        public int MultipleThreadsAvg { get; set; }
        public int TwoGreenFlagTrheadAvg { get; set; }
        public int AdvancedEventUseAvg { get; set; }
        public int UseSimpleBlocksAvg { get; set; }
        public int UseMediumBlocksAvg { get; set; }
        public int UseNestedControlAvg { get; set; }
        public int BasicInputUseAvg { get; set; }
        public int VariableUseAvg { get; set; }
        public int SpriteSensingAvg { get; set; }
        public int VariableCreationAvg { get; set; }
        public int BasicOperatorsAvg { get; set; }
        public int MediumOperatorsAvg { get; set; }
        public int AdvancedOperatorsAvg { get; set; }
        public List<int> SpriteCountMode { get; set; }
        public List<int> NonUnusedBlocksMode { get; set; }
        public List<int> UserDefinedBlocksMode { get; set; }
        public List<int> CloneUseMode { get; set; }
        public List<int> SecuenceUseMode { get; set; }
        public List<int> MultipleThreadsMode { get; set; }
        public List<int> TwoGreenFlagTrheadMode { get; set; }
        public List<int> AdvancedEventUseMode { get; set; }
        public List<int> UseSimpleBlocksMode { get; set; }
        public List<int> UseMediumBlocksMode { get; set; }
        public List<int> UseNestedControlMode { get; set; }
        public List<int> BasicInputUseMode { get; set; }
        public List<int> VariableUseMode { get; set; }
        public List<int> SpriteSensingMode { get; set; }
        public List<int> VariableCreationMode { get; set; }
        public List<int> BasicOperatorsMode { get; set; }
        public List<int> MediumOperatorsMode { get; set; }
        public List<int> AdvancedOperatorsMode { get; set; }
    }
}
