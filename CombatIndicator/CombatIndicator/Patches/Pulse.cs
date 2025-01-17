﻿using HarmonyLib;

namespace CombatIndicator.Patches
{
    [HarmonyPatch(typeof(PUI_LocalPlayerStatus))]
    internal class Pulse_Patches
    {
        [HarmonyPatch(nameof(PUI_LocalPlayerStatus.UpdateBPM))]
        [HarmonyWrapSafe]
        [HarmonyPostfix]
        public static void Initialize_Postfix(PUI_LocalPlayerStatus __instance)
        {
            string state = "Out of Combat";
            switch(DramaManager.CurrentStateEnum)
            {
                case DRAMA_State.Encounter:
                    // Removed due to desync with client
                    //state = "In Encounter";
                    //break;

                case DRAMA_State.Survival: // Unsure if this is correct - Assumed to be when you teleport between Alpha zones
                case DRAMA_State.IntentionalCombat:
                case DRAMA_State.Combat:
                    state = "In Combat";
                    break;
            }

            __instance.m_pulseText.text += $" | " + state;
#if DEBUG
            __instance.m_pulseText.text += $" ({DramaManager.CurrentStateEnum.ToString()})";
#endif
        }
    }
}