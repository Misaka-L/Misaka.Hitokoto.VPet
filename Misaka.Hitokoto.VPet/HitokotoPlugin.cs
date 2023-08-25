using System.Reflection;
using HarmonyLib;
using LinePutScript.Converter;
using Misaka.Hitokoto.VPet.Models;
using Misaka.Hitokoto.VPet.Patches;
using Misaka.Hitokoto.VPet.Views;
using VPet_Simulator.Windows.Interface;

namespace Misaka.Hitokoto.VPet
{
    public class HitokotoPlugin : MainPlugin
    {
        public override string PluginName => "一言 - Hitokoto";

        private readonly HitokotoClient _hitokotoClient = new();

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public static IMainWindow? GameMainWindow { get; private set; }
        public static HitokotoSettings PluginSetting { get; private set; } = new();
        public static HitokotoItem? HitokotoItemInstance { get; private set; }

        public HitokotoPlugin(IMainWindow mainWindow) : base(mainWindow)
        {
            GameMainWindow = mainWindow;

            var harmony = new Harmony("xyz.misakal.vpet.plugin.hitokoto");
            harmony.PatchAll();

            var vPetMainAssembly = Assembly.GetCallingAssembly();
            var vPetInterfaceAssembly = Assembly.GetAssembly(typeof(IMainWindow));

            foreach (var patch in Assembly.GetExecutingAssembly().GetTypes()
                         .Where(type => type != typeof(IPatch) && typeof(IPatch).IsAssignableFrom(type)).ToArray())
            {
                var instance = (IPatch)Activator.CreateInstance(patch);
                instance.Patch(harmony, vPetMainAssembly, vPetInterfaceAssembly);
            }
        }

        public override void LoadPlugin()
        {
            GetHitokoto();
            MW.Main.OnSay += _ => GetHitokoto();

            PluginSetting = HitokotoSettings.Load(MW.Set);
            
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    var hitokoto = await _hitokotoClient.GetHitokotoAsync(PluginSetting.HitokotoTypes);
                    MW.Main.SayRnd(hitokoto.ToString());

                    var delay = new Random().Next(30, 1200);
                    await Task.Delay(TimeSpan.FromSeconds(delay));
                }
            }, _cancellationTokenSource.Token);
        }

        public override void EndGame()
        {
            _cancellationTokenSource.Cancel();
        }

        public override void Setting()
        {
            HitokotoSettingWindow.Instance.Show();
        }

        private async void GetHitokoto()
        {
            _hitokotoClient.SetApiBaseUrl(PluginSetting.ApiBaseUrl);

            var hitokoto = await _hitokotoClient.GetHitokotoAsync(PluginSetting.HitokotoTypes);
            HitokotoItemInstance = hitokoto;
        }
    }
}