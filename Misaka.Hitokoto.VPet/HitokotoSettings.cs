using LinePutScript.Converter;
using Misaka.Hitokoto.VPet.Models;
using VPet_Simulator.Windows.Interface;

namespace Misaka.Hitokoto.VPet;

public class HitokotoSettings
{
    [Line] public HitokotoType[] HitokotoTypes = {};
    [Line] public string ApiBaseUrl = "https://v1.hitokoto.cn";

    public void Save(ISetting setting)
    {
        setting[nameof(HitokotoSettings)].Set(LPSConvert.SerializeObject(this, nameof(HitokotoSettings)));
    }

    public static HitokotoSettings Load(ISetting setting)
    {
        var settingLine = setting[nameof(HitokotoSettings)];
        if (settingLine == null)
        {
            return new HitokotoSettings();
        }

        return LPSConvert.DeserializeObject<HitokotoSettings>(settingLine) ?? new HitokotoSettings();
    }
}