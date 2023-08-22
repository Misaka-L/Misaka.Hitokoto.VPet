using System.Windows;
using System.Windows.Controls;

namespace Misaka.Hitokoto.VPet.Controls;

public partial class WindowTitleBar : UserControl
{
    public static readonly DependencyProperty WindowProperty = DependencyProperty.Register(
        nameof(Window), typeof(Window), typeof(WindowTitleBar), new PropertyMetadata(default(Window)));

    public Window Window
    {
        get => (Window)GetValue(WindowProperty);
        set => SetValue(WindowProperty, value);
    }
    
    public WindowTitleBar()
    {
        InitializeComponent();
    }
    
    private void MinimizeWindowButton_OnClick(object sender, RoutedEventArgs e) =>
        SystemCommands.MinimizeWindow(Window);

    private void CloseWindowButton_OnClick(object sender, RoutedEventArgs e) =>
        SystemCommands.CloseWindow(Window);
}