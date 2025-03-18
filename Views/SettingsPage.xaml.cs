using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui_serialmonitor.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private Window window;

        public SettingsPage()
        {
            this.InitializeComponent();
            window = App.MainWindow;
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            if (window?.SystemBackdrop is MicaBackdrop micaBackdrop)
            {
                if (micaBackdrop.Kind == MicaKind.BaseAlt)
                {
                    BackdropStyleComboBox.SelectedIndex = 3; // Mica Alt
                }
                else
                {
                    BackdropStyleComboBox.SelectedIndex = 2; // Mica
                }
            }
            else if (window?.SystemBackdrop is DesktopAcrylicBackdrop)
            {
                BackdropStyleComboBox.SelectedIndex = 1; // Acrylic
            }
            else
            {
                BackdropStyleComboBox.SelectedIndex = 0; // None
            }
        }

        private void BackdropStyleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (window == null) return;

            var selectedStyle = BackdropStyleComboBox.SelectedItem as ComboBoxItem;
            if (selectedStyle == null) return;

            switch (selectedStyle.Content.ToString())
            {
                case "None":
                    window.SystemBackdrop = null;
                    break;
                case "Acrylic":
                    window.SystemBackdrop = new DesktopAcrylicBackdrop();
                    break;
                case "Mica":
                    window.SystemBackdrop = new MicaBackdrop();
                    break;
                case "Mica Alt":
                    var micaAltBackdrop = new MicaBackdrop
                    {
                        Kind = MicaKind.BaseAlt
                    };
                    window.SystemBackdrop = micaAltBackdrop;
                    break;
            }
        }
    }
}
