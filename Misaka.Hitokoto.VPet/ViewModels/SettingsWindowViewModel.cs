using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Misaka.Hitokoto.VPet.Models;
using VPet_Simulator.Windows.Interface;

namespace Misaka.Hitokoto.VPet.ViewModels;

public partial class SettingsWindowViewModel : ObservableObject
{
    [ObservableProperty] private string _apiHost;

    [ObservableProperty] private string[] _hitokotoTypes = null!;

    [ObservableProperty] private string[] _availableHitokotoTypes = null!;

    [ObservableProperty] private int _minRandomDuration = 30;
    [ObservableProperty] private int _maxRandomDuration = 1200;

    public SettingsWindowViewModel()
    {
        var settings = HitokotoPlugin.PluginSetting;
        _apiHost = settings.ApiBaseUrl;

        AvailableHitokotoTypes = ((HitokotoType[])Enum.GetValues(typeof(HitokotoType)))
            .Select(type => type.ToReadableString()).ToArray();

        HitokotoTypes = settings.HitokotoTypes.Length == 0
            ? AvailableHitokotoTypes
            : settings.HitokotoTypes.Select(type => type.ToReadableString()).ToArray();

        MinRandomDuration = settings.MinRandomDuration;
        MaxRandomDuration = settings.MaxRandomDuration;
    }

    [RelayCommand]
    private void SaveSettings()
    {
        var settings = HitokotoPlugin.PluginSetting;
        var apiBaseUri = settings.ApiBaseUrl;
        try
        {
            var uri = new Uri(ApiHost);
            if (uri.Scheme != "http" && uri.Scheme != "https")
                throw new InvalidOperationException();

            var uriString = uri.ToString();
            apiBaseUri = uriString.EndsWith("/") ? uriString.Substring(0, uriString.Length - 1) : uriString;

            throw new InvalidOperationException();
        }
        catch
        {
            Growl.Fatal("API 地址必须为 https/http 协议的有效 Uri", "token");
        }

        settings.HitokotoTypes = HitokotoTypes.Select(HitokotoTypeExtenstion.FromReadableString)
            .GroupBy(type => type).Select(group => group.First()).ToArray();

        settings.ApiBaseUrl = apiBaseUri;
        settings.MinRandomDuration = MinRandomDuration;
        settings.MaxRandomDuration = MaxRandomDuration;

        if (HitokotoPlugin.GameMainWindow != null) settings.Save(HitokotoPlugin.GameMainWindow.Set);
    }
}