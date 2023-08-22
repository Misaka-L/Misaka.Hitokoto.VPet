using System.Diagnostics;
using System.Windows.Controls;
using HandyControl.Controls;
using Misaka.Hitokoto.VPet.ViewModels;
using Window = System.Windows.Window;

namespace Misaka.Hitokoto.VPet.Views;

public partial class HitokotoSettingWindow : Window
{
    public static HitokotoSettingWindow Instance { get; private set; } = new();
    
    public HitokotoSettingWindow()
    {
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        Instance = new HitokotoSettingWindow();
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (CheckComboBox)sender;
        var viewModel = (SettingsWindowViewModel)DataContext;
        viewModel.HitokotoTypes = comboBox.SelectedItems.Cast<string>().ToArray();
    }
}