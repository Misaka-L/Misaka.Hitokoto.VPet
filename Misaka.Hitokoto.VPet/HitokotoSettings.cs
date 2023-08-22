using LinePutScript.Converter;
using Misaka.Hitokoto.VPet.Models;
using VPet_Simulator.Windows.Interface;

namespace Misaka.Hitokoto.VPet;

public class HitokotoSettings
{
    [Line] public HitokotoType[] HitokotoTypes = {};
    [Line] public string ApiBaseUrl = "https://v1.hitokoto.cn";

    public void Save(Setting setting)
    {
        setting.Remove(nameof(HitokotoSettings));
        setting.Add(LPSConvert.SerializeObject(this, nameof(HitokotoSettings)));
    }

    public static HitokotoSettings Load(Setting setting)
    {
        var settingLine = setting.FindLine(nameof(HitokotoSettings));
        if (settingLine == null)
        {
            return new HitokotoSettings();
        }

        return LPSConvert.DeserializeObject<HitokotoSettings>(settingLine) ?? new HitokotoSettings();
    }
}