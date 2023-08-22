using System.Reflection;
using HarmonyLib;

namespace Misaka.Hitokoto.VPet.Patches;

public interface IPatch
{
    public void Patch(Harmony harmony, Assembly vPetMainAssembly, Assembly vPetInterfaceAssembly);
}