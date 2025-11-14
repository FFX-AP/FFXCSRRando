using FFXCutsceneRemover.Logging;
using System.Collections.Generic;

namespace FFXCutsceneRemover;

class OblitzeratorTransition : Transition
{
    static private List<short> CutsceneAltList = new List<short>(new short[] { 2037 });
    public override void Execute(string defaultDescription = "")
    {
        if (MemoryWatchers.OblitzeratorTransition.Current > 0)
        {
            if (CutsceneAltList.Contains(MemoryWatchers.CutsceneAlt.Current) && Stage == 0)
            {
                //FormationSwitch = formations.PreOblitzerator;
                base.Execute();

                BaseCutsceneValue = MemoryWatchers.EventFileStart.Current;
                Stage += 1;

            }
            else if (MemoryWatchers.OblitzeratorTransition.Current >= (BaseCutsceneValue + 0x513B) && Stage == 1) // 0x41F7 in Event Script
            {
                WriteValue<int>(MemoryWatchers.OblitzeratorTransition, BaseCutsceneValue + 0x5191); // 0x424D in Event Script - Load Move Animation

                Stage += 1;
            }
            else if (MemoryWatchers.OblitzeratorTransition.Current >= (BaseCutsceneValue + 0x5194) && Stage == 2) // 0x4250 in Event Script
            {
                WriteValue<int>(MemoryWatchers.OblitzeratorTransition, BaseCutsceneValue + 0x52AE); // 0x4364 in Event Script - Play BGM

                Stage += 1;
            }
            else if (MemoryWatchers.OblitzeratorTransition.Current >= (BaseCutsceneValue + 0x52B4) && Stage == 3) // 0x4370 in Event Script
            {
                WriteValue<int>(MemoryWatchers.OblitzeratorTransition, BaseCutsceneValue + 0x5359); // 0x4415 in Event Script - Set Battle Flags / Launch Battle

                Stage += 1;
            }
            else if (MemoryWatchers.BattleState2.Current == 1 && Stage == 4)
            {
                WriteValue<int>(MemoryWatchers.OblitzeratorTransition, BaseCutsceneValue + 0x569B); // 0x4760 in Event Script - Play BGM
                Stage += 1;
            }
        }
    }
}