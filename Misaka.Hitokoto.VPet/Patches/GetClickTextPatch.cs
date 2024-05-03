using System.Reflection;
using HarmonyLib;
using VPet_Simulator.Windows.Interface;

namespace Misaka.Hitokoto.VPet.Patches;

public class GetClickTextPatch : IPatch
{
    public void Patch(Harmony harmony, Assembly vPetMainAssembly, Assembly vPetInterfaceAssembly)
    {
        var mainWindowsType = vPetMainAssembly.GetType("VPet_Simulator.Windows.MainWindow");

        if (mainWindowsType is null)
        {
            Console.WriteLine("[Misaka.Hitokoto.VPet] Can't find VPet_Simulator.Windows.MainWindow");
            return;
        }

        var getClickTextMethod = mainWindowsType.GetMethod("GetClickText", BindingFlags.Public | BindingFlags.Instance);

        if (getClickTextMethod is null)
        {
            Console.WriteLine("[Misaka.Hitokoto.VPet] Can't find VPet_Simulator.Windows.MainWindow.GetClickText");
            return;
        }

        var patchMethod = new HarmonyMethod(typeof(GetClickTextPatch).GetMethod(nameof(GetClickTextPostfix), BindingFlags.NonPublic | BindingFlags.Static));

        harmony.Patch(getClickTextMethod, postfix: patchMethod);
    }

    private static void GetClickTextPostfix(ref ClickText __result)
    {
        var result = new Random(Guid.NewGuid().GetHashCode()).Next(0, 3);
        if (result > 1 && HitokotoPlugin.HitokotoItemInstance is { } hitokotoItem)
            __result = new ClickText(hitokotoItem.ToString());
    }
}