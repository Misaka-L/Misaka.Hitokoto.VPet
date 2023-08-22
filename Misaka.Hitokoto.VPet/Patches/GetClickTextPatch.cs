using System.Reflection;
using System.Security.Cryptography;
using HarmonyLib;
using VPet_Simulator.Windows.Interface;

namespace Misaka.Hitokoto.VPet.Patches;

public class GetClickTextPatch : IPatch
{
    public void Patch(Harmony harmony, Assembly vPetMainAssembly, Assembly vPetInterfaceAssembly)
    {
        harmony.Patch(vPetMainAssembly.GetType("VPet_Simulator.Windows.MainWindow").GetMethod("GetClickText"),
            postfix: new HarmonyMethod(typeof(GetClickTextPatch).GetMethod(nameof(GetClickTextPostfix),
                BindingFlags.NonPublic | BindingFlags.Static)));
    }

    private static void GetClickTextPostfix(ref ClickText __result)
    {
        var result = new Random(Guid.NewGuid().GetHashCode()).Next(0, 3);
        if (result > 1 && HitokotoPlugin.HitokotoItemInstance is {} hitokotoItem)
            __result = new ClickText(hitokotoItem.ToString());
    }
}