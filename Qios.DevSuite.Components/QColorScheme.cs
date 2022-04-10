// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QColorScheme
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Ribbon;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QColorScheme : QColorSchemeBase, ICloneable
  {
    private static QGlobalColorScheme m_oGlobal = new QGlobalColorScheme(true, true);

    public QColorScheme()
      : this(false, true, true)
    {
    }

    public QColorScheme(bool register)
      : this(false, true, register)
    {
    }

    internal QColorScheme(bool isGlobalColorScheme, bool addDefaultValues, bool register)
      : base(isGlobalColorScheme, register)
    {
      if (!addDefaultValues)
        return;
      this.AddDefaultThemes();
      if (!isGlobalColorScheme)
        return;
      this.AddDefaultColors();
    }

    private void AddDefaultThemes()
    {
      this.Themes.Add(new QThemeInfo("Default", "", "", true));
      this.Themes.Add(new QThemeInfo("LunaBlue", "Luna.msstyles", "NormalColor", true));
      this.Themes.Add(new QThemeInfo("LunaOlive", "Luna.msstyles", "HomeStead", true));
      this.Themes.Add(new QThemeInfo("LunaSilver", "Luna.msstyles", "Metallic", true));
      this.Themes.Add(new QThemeInfo("HighContrast", "", "", true));
      this.Themes.Add(new QThemeInfo("VistaBlack", "aero.msstyles", "NormalColor", true));
    }

    private void AddMiscColors()
    {
      this.AddColor("Empty", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("Transparent", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
      this.AddColor("TextColor", SystemColors.ControlText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
      this.AddColor("DisabledTextColor", SystemColors.GrayText, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.FromArgb(141, 141, 141));
      this.AddColor("ContainerControlBackground1", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("ContainerControlBackground2", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("ContainerControlBorder", SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark);
    }

    private void AddInputBoxColors()
    {
      this.AddColor("TextCueColor", SystemColors.GrayText, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.DarkGray);
      this.AddColor("InputBoxBackground", Color.White, Color.White, Color.White, Color.White, SystemColors.Window, Color.White);
      this.AddColor("InputBoxDisabledBackground", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("InputBoxHotBackground", "@InputBoxBackground");
      this.AddColor("InputBoxFocusedBackground", "@InputBoxBackground");
      this.AddColor("InputBoxOuterBackground1", Color.White, Color.White, Color.White, Color.White, SystemColors.Window, Color.White);
      this.AddColor("InputBoxOuterBackground2", Color.White, Color.White, Color.White, Color.White, SystemColors.Window, Color.White);
      this.AddColor("InputBoxOuterBorder", SystemColors.ControlDark, Color.FromArgb(155, 183, 220), Color.FromArgb(195, 205, 149), Color.FromArgb(164, 164, 164), SystemColors.Highlight, Color.FromArgb(137, 137, 137));
      this.AddColor("InputBoxDisabledOuterBackground1", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("InputBoxDisabledOuterBackground2", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("InputBoxDisabledOuterBorder", SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText);
      this.AddColor("InputBoxHotOuterBackground1", "@InputBoxOuterBackground1");
      this.AddColor("InputBoxHotOuterBackground2", "@InputBoxOuterBackground2");
      this.AddColor("InputBoxHotOuterBorder", "@InputBoxOuterBorder");
      this.AddColor("InputBoxFocusedOuterBackground1", "@InputBoxOuterBackground1");
      this.AddColor("InputBoxFocusedOuterBackground2", "@InputBoxOuterBackground2");
      this.AddColor("InputBoxFocusedOuterBorder", "@InputBoxOuterBorder");
      this.AddColor("InputBoxButtonBackground1", SystemColors.ControlLightLight, Color.FromArgb(227, 237, 251), Color.FromArgb(254, 253, 239), Color.FromArgb(236, 240, 245), SystemColors.Control, Color.FromArgb(236, 240, 245));
      this.AddColor("InputBoxButtonBackground2", SystemColors.Control, Color.FromArgb(188, 208, 233), Color.FromArgb(212, 215, 187), Color.FromArgb(206, 214, 222), SystemColors.Control, Color.FromArgb(220, 225, 232));
      this.AddColor("InputBoxButtonBorder", SystemColors.ControlDark, Color.FromArgb(155, 183, 220), Color.FromArgb(195, 205, 149), Color.FromArgb(164, 164, 164), SystemColors.Highlight, Color.FromArgb(137, 137, 137));
      this.AddColor("InputBoxDisabledButtonBackground1", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("InputBoxDisabledButtonBackground2", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("InputBoxDisabledButtonBorder", SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText);
      this.AddColor("InputBoxHotButtonBackground1", Color.FromArgb((int) byte.MaxValue, 254, 227), Color.FromArgb((int) byte.MaxValue, 254, 227), Color.FromArgb((int) byte.MaxValue, 254, 227), Color.FromArgb((int) byte.MaxValue, 254, 227), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 254, 227));
      this.AddColor("InputBoxHotButtonBackground2", Color.FromArgb((int) byte.MaxValue, 215, 74), Color.FromArgb((int) byte.MaxValue, 215, 74), Color.FromArgb((int) byte.MaxValue, 215, 74), Color.FromArgb((int) byte.MaxValue, 215, 74), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 254, 227));
      this.AddColor("InputBoxHotButtonBorder", Color.FromArgb(191, 179, 129), Color.FromArgb(191, 179, 129), Color.FromArgb(191, 179, 129), Color.FromArgb(164, 164, 164), SystemColors.Highlight, Color.FromArgb(191, 179, 129));
      this.AddColor("InputBoxPressedButtonBackground1", Color.FromArgb(254, 214, 169), Color.FromArgb(254, 214, 169), Color.FromArgb(254, 214, 169), Color.FromArgb(254, 214, 169), SystemColors.Highlight, Color.FromArgb(254, 214, 169));
      this.AddColor("InputBoxPressedButtonBackground2", Color.FromArgb((int) byte.MaxValue, 165, 64), Color.FromArgb((int) byte.MaxValue, 165, 64), Color.FromArgb((int) byte.MaxValue, 165, 64), Color.FromArgb((int) byte.MaxValue, 165, 64), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 165, 64));
      this.AddColor("InputBoxPressedButtonBorder", Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), SystemColors.Highlight, Color.FromArgb(142, 129, 101));
    }

    private void AddCustomToolWindowColors()
    {
      this.AddColor("CustomToolWindowBackground1", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("CustomToolWindowBackground2", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
      this.AddColor("CustomToolWindowCaption1", SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark);
      this.AddColor("CustomToolWindowCaption2", SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.ControlDark, SystemColors.Control);
      this.AddColor("CustomToolWindowBorder", SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark, SystemColors.ControlDark);
    }

    private void AddStatusBarColors()
    {
      this.AddColor("StatusBarBackground1", SystemColors.ControlLightLight, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(97, 106, 118));
      this.AddColor("StatusBarBackground2", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(35, 38, 42));
      this.AddColor("StatusBarBorder", Color.Empty, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(153, 157, 169), Color.Empty, Color.Transparent);
      this.AddColor("StatusBarPanelBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(153, 157, 169), SystemColors.ControlDark, Color.FromArgb(221, 224, 227));
      this.AddColor("StatusBarPanelBackground1", SystemColors.ControlLightLight, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), Color.Transparent, Color.Transparent);
      this.AddColor("StatusBarPanelBackground2", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
      this.AddColor("StatusBarSizingGripDark", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(153, 157, 169), SystemColors.ControlDark, Color.FromArgb(145, 153, 164));
      this.AddColor("StatusBarSizingGripLight", SystemColors.ControlLightLight, Color.FromArgb(228, 228, 234), Color.FromArgb(228, 228, 234), Color.FromArgb(228, 228, 234), SystemColors.ControlLightLight, Color.FromArgb(221, 224, 227));
    }

    private void AddProgressBarColors()
    {
      this.AddColor("ProgressBarBackground1", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
      this.AddColor("ProgressBarBackground2", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
      this.AddColor("ProgressBarBorder", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
      this.AddColor("ProgressBarColor1", SystemColors.ActiveCaption, Color.NavajoWhite, Color.NavajoWhite, Color.NavajoWhite, SystemColors.ActiveCaption, Color.FromArgb(234, 252, 202));
      this.AddColor("ProgressBarColor2", SystemColors.ActiveCaption, Color.DarkOrange, Color.DarkOrange, Color.DarkOrange, SystemColors.ActiveCaption, Color.FromArgb(103, 150, 32));
      this.AddColor("ProgressBarText", Color.Black, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
    }

    private void AddPanelColors()
    {
      this.AddColor("PanelBackground1", SystemColors.ControlLightLight, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(215, 219, 224));
      this.AddColor("PanelBackground2", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(180, 187, 197));
      this.AddColor("PanelBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(153, 157, 169), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
    }

    private void AddDockingWindowColors()
    {
      this.AddColor("DockBarButtonBackground1", SystemColors.ControlLightLight, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(205, 208, 213));
      this.AddColor("DockBarButtonBackground2", SystemColors.Control, Color.White, Color.White, Color.White, SystemColors.Control, Color.FromArgb(145, 153, 164));
      this.AddColor("DockBarButtonBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(153, 157, 169), SystemColors.ControlDark, Color.FromArgb(76, 83, 92));
      this.AddColor("DockBarBackground1", SystemColors.ControlLightLight, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(83, 83, 83));
      this.AddColor("DockBarBackground2", SystemColors.Control, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(83, 83, 83));
      this.AddColor("DockBarBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(153, 157, 169), SystemColors.ControlDark, Color.Transparent);
      this.AddColor("DockingWindowBackground1", SystemColors.ControlLight, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(235, 235, 235));
      this.AddColor("DockingWindowBackground2", SystemColors.Control, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(235, 235, 235));
      this.AddColor("DockingWindowTabStrip1", SystemColors.ControlLightLight, Color.White, Color.White, Color.White, SystemColors.ControlLightLight, Color.FromArgb(83, 83, 83));
      this.AddColor("DockingWindowTabStrip2", SystemColors.ControlLightLight, Color.White, Color.White, Color.White, SystemColors.ControlLightLight, Color.FromArgb(83, 83, 83));
      this.AddColor("DockingWindowTabButton1", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(235, 235, 235));
      this.AddColor("DockingWindowTabButton2", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(235, 235, 235));
      this.AddColor("DockingWindowTabButtonBorder", SystemColors.ControlDarkDark, Color.Black, Color.Black, Color.Black, SystemColors.ControlDarkDark, Color.FromArgb(53, 53, 53));
      this.AddColor("DockingWindowTabButtonBorderNotActive", SystemColors.ControlDarkDark, Color.FromArgb(153, 157, 169), Color.FromArgb(96, 128, 88), Color.FromArgb(153, 157, 169), SystemColors.ControlDarkDark, Color.FromArgb(145, 153, 164));
      this.AddColor("DockingWindowTabButtonText", SystemColors.ControlText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
      this.AddColor("DockingWindowTabButtonTextNotActive", SystemColors.GrayText, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.White);
      this.AddColor("DockContainerBackground1", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(205, 208, 213));
      this.AddColor("DockContainerBackground2", SystemColors.Control, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(205, 208, 213));
    }

    private void AddMenuColors()
    {
      this.AddColor("MainMenuBorder", Color.Empty, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(76, 83, 92));
      this.AddColor("MainMenuBackground1", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Menu, Color.Transparent);
      this.AddColor("MainMenuBackground2", SystemColors.Control, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Menu, Color.Transparent);
      this.AddColor("MainMenuBevel1", SystemColors.ControlLightLight, Color.FromArgb(223, 237, 254), Color.FromArgb(244, 247, 222), Color.FromArgb(243, 244, 250), SystemColors.Control, Color.FromArgb(205, 208, 213));
      this.AddColor("MainMenuBevel2", SystemColors.Control, Color.FromArgb(129, 169, 226), Color.FromArgb(183, 198, 145), Color.FromArgb(153, 151, 181), SystemColors.Control, Color.FromArgb(148, 156, 166));
      this.AddColor("MainMenuShadeLine", SystemColors.ControlDark, Color.FromArgb(59, 97, 156), Color.FromArgb(96, 128, 88), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(76, 83, 92));
      this.AddColor("MainMenuMoreItemsArea1", SystemColors.ControlDark, Color.FromArgb(117, 166, 241), Color.FromArgb(176, 194, 140), Color.FromArgb(179, 178, 200), SystemColors.Control, Color.FromArgb(178, 183, 191));
      this.AddColor("MainMenuMoreItemsArea2", SystemColors.ControlDark, Color.FromArgb(0, 53, 145), Color.FromArgb(96, 119, 107), Color.FromArgb(124, 124, 148), SystemColors.Control, Color.FromArgb(76, 83, 92));
      this.AddColor("MainMenuSizingGrip", SystemColors.ControlDark, Color.FromArgb(39, 65, 118), Color.FromArgb(81, 94, 51), Color.FromArgb(84, 84, 117), SystemColors.ControlDark, Color.FromArgb(76, 83, 92));
      this.AddColor("MainMenuShade", SystemColors.ControlLightLight, Color.White, Color.White, Color.White, SystemColors.ControlLightLight, Color.FromArgb(221, 224, 227));
      this.AddColor("MainMenuExpandedItemBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(145, 153, 164));
      this.AddColor("MainMenuExpandedItemBackground1", SystemColors.Highlight, Color.White, Color.White, Color.White, SystemColors.Highlight, Color.FromArgb(145, 153, 164));
      this.AddColor("MainMenuExpandedItemBackground2", SystemColors.Highlight, Color.FromArgb(142, 179, 231), Color.FromArgb(194, 206, 159), Color.FromArgb(186, 186, 205), SystemColors.Highlight, Color.FromArgb(108, 117, 128));
      this.AddColor("MainMenuHotItemBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb((int) byte.MaxValue, 189, 105));
      this.AddColor("MainMenuHotItemBackground1", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 231, 162));
      this.AddColor("MainMenuHotItemBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 193, 118), Color.FromArgb((int) byte.MaxValue, 193, 118), Color.FromArgb((int) byte.MaxValue, 193, 118), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 219, 117));
      this.AddColor("MainMenuPressedItemBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("MainMenuPressedItemBackground1", SystemColors.Highlight, Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("MainMenuPressedItemBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 184, 94));
      this.AddColor("MainMenuSeparator", SystemColors.ControlDark, Color.FromArgb(106, 140, 203), Color.FromArgb(96, 128, 88), Color.FromArgb(110, 110, 149), SystemColors.ControlDark, Color.FromArgb(145, 153, 164));
      this.AddColor("MainMenuText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
      this.AddColor("MainMenuTextActive", SystemColors.HighlightText, Color.Black, Color.Black, Color.Black, SystemColors.HighlightText, Color.Black);
      this.AddColor("mainMenuTextDisabled", SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, Color.FromArgb(141, 141, 141));
      this.AddColor("MenuText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
      this.AddColor("MenuTextActive", SystemColors.HighlightText, Color.Black, Color.Black, Color.Black, SystemColors.HighlightText, Color.Black);
      this.AddColor("MenuTextDisabled", SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, Color.FromArgb(141, 141, 141));
      this.AddColor("MenuBackground1", SystemColors.Window, Color.White, Color.White, Color.White, SystemColors.Window, Color.FromArgb(246, 246, 246));
      this.AddColor("MenuBackground2", SystemColors.Window, Color.LightGray, Color.LightGray, Color.FromArgb(225, 225, 235), SystemColors.Window, Color.FromArgb(246, 246, 246));
      this.AddColor("MenuShade", Color.Empty, Color.Black, Color.Black, Color.Black, Color.Empty, Color.Black);
      this.AddColor("MenuDepersonalizeImageForeground", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
      this.AddColor("MenuDepersonalizeImageBackground", SystemColors.ControlDark, Color.FromArgb(129, 169, 226), Color.FromArgb(168, 181, 132), Color.FromArgb(166, 164, 187), Color.Transparent, Color.FromArgb(76, 83, 92));
      this.AddColor("MenuBorder", SystemColors.ControlDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), SystemColors.ControlDark, Color.FromArgb(145, 153, 164));
      this.AddColor("MenuHotItemBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb((int) byte.MaxValue, 189, 105));
      this.AddColor("MenuHotItemBackground1", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 231, 162));
      this.AddColor("MenuHotItemBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 214, 154), Color.FromArgb((int) byte.MaxValue, 214, 154), Color.FromArgb((int) byte.MaxValue, 214, 154), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 219, 117));
      this.AddColor("MenuPressedItemBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("MenuPressedItemBackground1", SystemColors.Highlight, Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("MenuPressedItemBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 184, 94));
      this.AddColor("MenuCheckedItemBorder", SystemColors.Highlight, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("MenuCheckedItemBackground1", Color.Empty, Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("MenuCheckedItemBackground2", Color.Empty, Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.Empty, Color.FromArgb((int) byte.MaxValue, 184, 94));
      this.AddColor("MenuSeparator", SystemColors.ControlDark, Color.FromArgb(106, 140, 203), Color.FromArgb(96, 128, 88), Color.FromArgb(110, 110, 149), SystemColors.ControlDark, Color.FromArgb(145, 153, 164));
      this.AddColor("MenuIconBackground1", SystemColors.Control, Color.FromArgb(227, 239, (int) byte.MaxValue), Color.FromArgb(237, 242, 211), Color.FromArgb(240, 240, 248), Color.Transparent, Color.FromArgb(239, 239, 239));
      this.AddColor("MenuIconBackground2", SystemColors.Control, Color.FromArgb(135, 173, 228), Color.FromArgb(186, 201, 148), Color.FromArgb(165, 165, 189), Color.Transparent, Color.FromArgb(239, 239, 239));
      this.AddColor("MenuIconBackgroundDepersonalized1", SystemColors.Control, Color.FromArgb(203, 221, 246), Color.FromArgb(230, 230, 209), Color.FromArgb(215, 215, 226), Color.Transparent, Color.FromArgb(189, 193, 200));
      this.AddColor("MenuIconBackgroundDepersonalized2", SystemColors.Control, Color.FromArgb(121, 161, 220), Color.FromArgb(164, 180, 120), Color.FromArgb(133, 133, 162), Color.Transparent, Color.FromArgb(210, 213, 218));
    }

    private void AddBalloonWindowColors()
    {
      this.AddColor("BalloonWindowBackground1", SystemColors.Info, SystemColors.Info, SystemColors.Info, SystemColors.Info, SystemColors.Info, SystemColors.Info);
      this.AddColor("BalloonWindowBackground2", SystemColors.Info, SystemColors.Info, SystemColors.Info, SystemColors.Info, SystemColors.Info, SystemColors.Info);
      this.AddColor("BalloonWindowBorder", Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
      this.AddColor("BalloonWindowShade", Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black));
      this.AddColor("BalloonWindowHotButtonBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb((int) byte.MaxValue, 189, 105));
      this.AddColor("BalloonWindowHotButtonBackground1", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 231, 162));
      this.AddColor("BalloonWindowHotButtonBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 193, 118), Color.FromArgb((int) byte.MaxValue, 193, 118), Color.FromArgb((int) byte.MaxValue, 193, 118), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 219, 117));
      this.AddColor("BalloonWindowPressedButtonBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("BalloonWindowPressedButtonBackground1", SystemColors.Highlight, Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("BalloonWindowPressedButtonBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 184, 94));
    }

    private void AddShapedWindowColors()
    {
      this.AddColor("ShapedWindowBackground1", SystemColors.Window, SystemColors.Window, SystemColors.Window, SystemColors.Window, SystemColors.Window, SystemColors.Window);
      this.AddColor("ShapedWindowBackground2", SystemColors.Window, SystemColors.Window, SystemColors.Window, SystemColors.Window, SystemColors.Window, SystemColors.Window);
      this.AddColor("ShapedWindowBorder", Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black);
      this.AddColor("ShapedWindowShade", Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black), Color.FromArgb(64, Color.Black));
      this.AddColor("ShapedWindowHotButtonBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb((int) byte.MaxValue, 189, 105));
      this.AddColor("ShapedWindowHotButtonBackground1", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 231, 162));
      this.AddColor("ShapedWindowHotButtonBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 193, 118), Color.FromArgb((int) byte.MaxValue, 193, 118), Color.FromArgb((int) byte.MaxValue, 193, 118), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 219, 117));
      this.AddColor("ShapedWindowPressedButtonBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("ShapedWindowPressedButtonBackground1", SystemColors.Highlight, Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("ShapedWindowPressedButtonBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 184, 94));
    }

    private void AddExplorerBarColors()
    {
      this.AddColor("ExplorerBarBackground1", SystemColors.ControlLight, Color.FromArgb(122, 161, 230), Color.FromArgb(203, 216, 172), Color.FromArgb(195, 199, 211), SystemColors.Window, Color.FromArgb(246, 246, 246));
      this.AddColor("ExplorerBarBackground2", SystemColors.Control, Color.FromArgb(99, 117, 214), Color.FromArgb(165, 189, 132), Color.FromArgb(177, 179, 200), SystemColors.Window, Color.FromArgb(246, 246, 246));
      this.AddColor("ExplorerBarBorder", Color.Empty, Color.FromArgb(0, 73, 181), Color.FromArgb(119, 140, 64), Color.FromArgb(119, 119, 146), Color.Empty, Color.Black);
      this.AddColor("ExplorerBarDepersonalizeImageForeground", SystemColors.MenuText, Color.FromArgb(33, 93, 198), Color.FromArgb(86, 102, 45), Color.FromArgb(63, 61, 61), SystemColors.ControlText, Color.Black);
      this.AddColor("ExplorerBarDepersonalizeImageBackground", SystemColors.ControlDark, Color.FromArgb(214, 223, 247), Color.FromArgb(246, 246, 236), Color.FromArgb(240, 241, 245), Color.Transparent, Color.FromArgb(145, 153, 164));
      this.AddColor("ExplorerBarText", SystemColors.MenuText, Color.FromArgb(33, 93, 198), Color.FromArgb(86, 102, 45), Color.FromArgb(63, 61, 61), SystemColors.MenuText, Color.FromArgb(53, 53, 53));
      this.AddColor("ExplorerBarTextHot", SystemColors.MenuText, Color.FromArgb(66, 142, (int) byte.MaxValue), Color.FromArgb(114, 146, 29), Color.FromArgb(126, 124, 124), SystemColors.HighlightText, Color.FromArgb(83, 83, 83));
      this.AddColor("ExplorerBarTextPressed", SystemColors.MenuText, Color.FromArgb(66, 142, (int) byte.MaxValue), Color.FromArgb(114, 146, 29), Color.FromArgb(126, 124, 124), SystemColors.HighlightText, Color.FromArgb(83, 83, 83));
      this.AddColor("ExplorerBarTextExpanded", SystemColors.MenuText, Color.FromArgb(33, 93, 198), Color.FromArgb(86, 102, 45), Color.FromArgb(63, 61, 61), SystemColors.HighlightText, Color.FromArgb(53, 53, 53));
      this.AddColor("ExplorerBarTextDisabled", SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, Color.FromArgb(141, 141, 141));
      this.AddColor("ExplorerBarSeparator", SystemColors.ControlDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), SystemColors.ControlDark, Color.FromArgb(145, 153, 164));
      this.AddColor("ExplorerBarExpandedItemBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, SystemColors.Highlight, Color.Empty);
      this.AddColor("ExplorerBarExpandedItemBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, SystemColors.Highlight, Color.Empty);
      this.AddColor("ExplorerBarExpandedItemBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("ExplorerBarHotItemBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("ExplorerBarHotItemBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, SystemColors.Highlight, Color.Empty);
      this.AddColor("ExplorerBarHotItemBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, SystemColors.Highlight, Color.Empty);
      this.AddColor("ExplorerBarPressedItemBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("ExplorerBarPressedItemBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, SystemColors.Highlight, Color.Empty);
      this.AddColor("ExplorerBarPressedItemBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, SystemColors.Highlight, Color.Empty);
      this.AddColor("ExplorerBarCheckedItemBorder", SystemColors.Highlight, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("ExplorerBarCheckedItemBackground1", Color.Empty, Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("ExplorerBarCheckedItemBackground2", Color.Empty, Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.Empty, Color.FromArgb((int) byte.MaxValue, 184, 94));
      this.AddColor("ExplorerBarCheckedGroupItemBorder", SystemColors.Highlight, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("ExplorerBarCheckedGroupItemBackground1", Color.Empty, Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("ExplorerBarCheckedGroupItemBackground2", Color.Empty, Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.Empty, Color.FromArgb((int) byte.MaxValue, 184, 94));
      this.AddColor("ExplorerBarGroupItemBackground1", SystemColors.ControlLight, Color.White, Color.FromArgb((int) byte.MaxValue, 252, 236), Color.White, SystemColors.Highlight, Color.FromArgb(205, 208, 213));
      this.AddColor("ExplorerBarGroupItemBackground2", SystemColors.ControlDark, Color.FromArgb(192, 211, 247), Color.FromArgb(224, 231, 184), Color.FromArgb(214, 215, 224), SystemColors.Highlight, Color.FromArgb(145, 153, 164));
      this.AddColor("ExplorerBarGroupItemBorder", SystemColors.ControlDarkDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(53, 53, 53));
      this.AddColor("ExplorerBarGroupPanelBackground1", SystemColors.Window, Color.FromArgb(214, 223, 247), Color.FromArgb(246, 246, 236), Color.FromArgb(240, 241, 245), SystemColors.Window, Color.FromArgb(246, 246, 246));
      this.AddColor("ExplorerBarGroupPanelBackground2", SystemColors.Window, Color.FromArgb(214, 223, 247), Color.FromArgb(246, 246, 236), Color.FromArgb(240, 241, 245), SystemColors.Window, Color.FromArgb(246, 246, 246));
      this.AddColor("ExplorerBarGroupPanelBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("ExplorerBarExpandedGroupItemBackground1", SystemColors.ControlLight, Color.White, Color.FromArgb((int) byte.MaxValue, 252, 236), Color.White, SystemColors.Highlight, Color.FromArgb(205, 208, 213));
      this.AddColor("ExplorerBarExpandedGroupItemBackground2", SystemColors.ControlDark, Color.FromArgb(192, 211, 247), Color.FromArgb(224, 231, 184), Color.FromArgb(214, 215, 224), SystemColors.Highlight, Color.FromArgb(145, 153, 164));
      this.AddColor("ExplorerBarExpandedGroupItemBorder", SystemColors.ControlDarkDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(53, 53, 53));
      this.AddColor("ExplorerBarHotGroupItemBorder", SystemColors.ControlDarkDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(53, 53, 53));
      this.AddColor("ExplorerBarHotGroupItemBackground1", SystemColors.ControlLight, Color.White, Color.FromArgb((int) byte.MaxValue, 252, 236), Color.White, SystemColors.Highlight, Color.FromArgb(205, 208, 213));
      this.AddColor("ExplorerBarHotGroupItemBackground2", SystemColors.ControlDark, Color.FromArgb(192, 211, 247), Color.FromArgb(224, 231, 184), Color.FromArgb(214, 215, 224), SystemColors.Highlight, Color.FromArgb(145, 153, 164));
      this.AddColor("ExplorerBarPressedGroupItemBorder", SystemColors.ControlDarkDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(53, 53, 53));
      this.AddColor("ExplorerBarPressedGroupItemBackground1", SystemColors.ControlLight, Color.White, Color.FromArgb((int) byte.MaxValue, 252, 236), Color.White, SystemColors.Highlight, Color.FromArgb(205, 208, 213));
      this.AddColor("ExplorerBarPressedGroupItemBackground2", SystemColors.ControlDark, Color.FromArgb(192, 211, 247), Color.FromArgb(224, 231, 184), Color.FromArgb(214, 215, 224), SystemColors.Highlight, Color.FromArgb(145, 153, 164));
      this.AddColor("ExplorerBarHasMoreChildItemsColor", SystemColors.ControlDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), SystemColors.ControlDark, Color.FromArgb(83, 83, 83));
      this.AddColor("ExplorerBarGroupItemShade", SystemColors.ControlDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(53, 53, 53));
    }

    private void AddToolBarColors()
    {
      this.AddColor("ToolBarBackground1", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, SystemColors.Control, Color.Transparent);
      this.AddColor("ToolBarBackground2", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, SystemColors.Control, Color.Transparent);
      this.AddColor("ToolBarBorder", Color.Empty, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(76, 83, 92));
      this.AddColor("ToolBarBevel1", SystemColors.ControlLightLight, Color.FromArgb(223, 237, 254), Color.FromArgb(244, 247, 222), Color.FromArgb(243, 244, 250), SystemColors.Control, Color.FromArgb(205, 208, 213));
      this.AddColor("ToolBarBevel2", SystemColors.Control, Color.FromArgb(129, 169, 226), Color.FromArgb(183, 198, 145), Color.FromArgb(153, 151, 181), SystemColors.Control, Color.FromArgb(148, 156, 166));
      this.AddColor("ToolBarShadeLine", SystemColors.ControlDark, Color.FromArgb(59, 97, 156), Color.FromArgb(96, 128, 88), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(76, 83, 92));
      this.AddColor("ToolBarMoreItemsArea1", SystemColors.ControlDark, Color.FromArgb(117, 166, 241), Color.FromArgb(176, 194, 140), Color.FromArgb(179, 178, 200), SystemColors.Control, Color.FromArgb(178, 183, 191));
      this.AddColor("ToolBarMoreItemsArea2", SystemColors.ControlDark, Color.FromArgb(0, 53, 145), Color.FromArgb(96, 119, 107), Color.FromArgb(124, 124, 148), SystemColors.Control, Color.FromArgb(76, 83, 92));
      this.AddColor("ToolBarSizingGrip", SystemColors.ControlDark, Color.FromArgb(39, 65, 118), Color.FromArgb(81, 94, 51), Color.FromArgb(84, 84, 117), SystemColors.ControlDark, Color.FromArgb(76, 83, 92));
      this.AddColor("ToolBarShade", SystemColors.ControlLightLight, Color.White, Color.White, Color.White, SystemColors.ControlLightLight, Color.FromArgb(221, 224, 227));
      this.AddColor("ToolBarText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
      this.AddColor("ToolBarTextActive", SystemColors.HighlightText, Color.Black, Color.Black, Color.Black, SystemColors.HighlightText, Color.Black);
      this.AddColor("ToolBarTextDisabled", SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, Color.FromArgb(141, 141, 141));
      this.AddColor("ToolBarExpandedItemBackground1", SystemColors.Highlight, Color.White, Color.White, Color.White, SystemColors.Highlight, Color.FromArgb(145, 153, 164));
      this.AddColor("ToolBarExpandedItemBackground2", SystemColors.Highlight, Color.FromArgb(142, 179, 231), Color.FromArgb(194, 206, 159), Color.FromArgb(186, 186, 205), SystemColors.Highlight, Color.FromArgb(108, 117, 128));
      this.AddColor("ToolBarExpandedItemBorder", Color.Empty, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(145, 153, 164));
      this.AddColor("ToolBarHotItemBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb((int) byte.MaxValue, 189, 105));
      this.AddColor("ToolBarHotItemBackground1", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), Color.FromArgb((int) byte.MaxValue, 244, 204), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 231, 162));
      this.AddColor("ToolBarHotItemBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 193, 118), Color.FromArgb((int) byte.MaxValue, 193, 118), Color.FromArgb((int) byte.MaxValue, 193, 118), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 219, 117));
      this.AddColor("ToolBarPressedItemBorder", Color.Empty, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("ToolBarPressedItemBackground1", SystemColors.Highlight, Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("ToolBarPressedItemBackground2", SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 184, 94));
      this.AddColor("ToolBarSeparator", SystemColors.ControlDark, Color.FromArgb(106, 140, 203), Color.FromArgb(96, 128, 88), Color.FromArgb(110, 110, 143), SystemColors.ControlDark, Color.FromArgb(145, 153, 164));
      this.AddColor("ToolBarCheckedItemBorder", SystemColors.Highlight, Color.DarkBlue, Color.FromArgb(63, 93, 56), Color.FromArgb(75, 75, 111), SystemColors.Highlight, Color.FromArgb(251, 140, 60));
      this.AddColor("ToolBarCheckedItemBackground1", Color.Empty, Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.FromArgb(192, (int) byte.MaxValue, 244, 204), Color.Empty, Color.FromArgb(251, 140, 60));
      this.AddColor("ToolBarCheckedItemBackground2", Color.Empty, Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.FromArgb(192, (int) byte.MaxValue, 214, 154), Color.Empty, Color.FromArgb((int) byte.MaxValue, 184, 94));
      this.AddColor("ToolBarFormBackground1", SystemColors.Control, Color.FromArgb(223, 237, 254), Color.FromArgb(209, 222, 173), Color.FromArgb(219, 218, 228), SystemColors.Control, Color.FromArgb(118, 128, 142));
      this.AddColor("ToolBarFormBackground2", SystemColors.Control, Color.FromArgb(223, 237, 254), Color.FromArgb(209, 222, 173), Color.FromArgb(219, 218, 228), SystemColors.Control, Color.FromArgb(118, 128, 142));
      this.AddColor("ToolBarFormCaption1", SystemColors.ControlDark, Color.FromArgb(42, 102, 201), Color.FromArgb(116, 134, 94), Color.FromArgb(122, 121, 153), SystemColors.Control, Color.FromArgb(83, 83, 83));
      this.AddColor("ToolBarFormCaption2", SystemColors.ControlDark, Color.FromArgb(42, 102, 201), Color.FromArgb(116, 134, 94), Color.FromArgb(122, 121, 153), SystemColors.Control, Color.FromArgb(83, 83, 83));
      this.AddColor("ToolBarFormBorder", SystemColors.ControlDark, Color.FromArgb(42, 102, 201), Color.FromArgb(116, 134, 94), Color.FromArgb(122, 121, 153), SystemColors.Control, Color.FromArgb(55, 60, 67));
      this.AddColor("ToolBarFormButton", SystemColors.ActiveCaptionText, Color.White, Color.White, Color.White, SystemColors.ActiveCaptionText, Color.White);
      this.AddColor("ToolBarFormButtonActive", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
    }

    private void AddToolBarHostColors()
    {
      this.AddColor("ToolBarHostBorder", Color.Empty, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(83, 83, 83));
      this.AddColor("ToolBarHostBackground1", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(83, 83, 83));
      this.AddColor("ToolBarHostBackground2", SystemColors.ControlLightLight, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(83, 83, 83));
    }

    private void AddTabControlColors()
    {
      this.AddColor("TabControlBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
      this.AddColor("TabControlBackground1", SystemColors.ControlLightLight, Color.White, Color.White, Color.White, SystemColors.Control, Color.FromArgb(246, 246, 246));
      this.AddColor("TabControlBackground2", SystemColors.ControlLightLight, Color.White, Color.White, Color.White, SystemColors.Control, Color.FromArgb(246, 246, 246));
      this.AddColor("TabControlContentBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("TabControlContentBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("TabControlContentBackground2", SystemColors.ControlLightLight, Color.White, Color.White, Color.White, SystemColors.Control, Color.White);
      this.AddColor("TabControlContentShade", Color.FromArgb(128, 0, 0, 0), Color.FromArgb(128, 0, 0, 0), Color.FromArgb(128, 0, 0, 0), Color.FromArgb(128, 0, 0, 0), Color.Empty, Color.FromArgb(128, 0, 0, 0));
      this.AddColor("TabControlDropIndicatorBackground", Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red);
      this.AddColor("TabControlDropIndicatorBorder", Color.DarkRed, Color.DarkRed, Color.DarkRed, Color.DarkRed, Color.DarkRed, Color.DarkRed);
      this.AddColor("TabStripBorder", SystemColors.ControlDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
      this.AddColor("TabStripBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("TabStripBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("TabStripNavigationAreaBorder", SystemColors.ControlDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
      this.AddColor("TabStripNavigationAreaBackground1", Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), SystemColors.Window, Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("TabStripNavigationAreaBackground2", Color.White, Color.White, Color.White, Color.White, SystemColors.Window, Color.White);
      this.AddColor("TabPageBorder", Color.Empty, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(123, 123, 123));
      this.AddColor("TabPageBackground1", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(215, 219, 224));
      this.AddColor("TabPageBackground2", SystemColors.ControlLightLight, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(180, 187, 197));
      this.AddColor("TabButtonBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(153, 157, 169), Color.FromArgb(153, 157, 169), SystemColors.ControlDark, Color.FromArgb(180, 187, 197));
      this.AddColor("TabButtonBackground1", SystemColors.Control, Color.FromArgb(228, 228, 234), Color.FromArgb(228, 228, 234), Color.FromArgb(228, 228, 234), SystemColors.Control, Color.FromArgb(238, 239, 240));
      this.AddColor("TabButtonBackground2", SystemColors.ControlLightLight, Color.FromArgb(243, 243, 247), Color.FromArgb(243, 243, 247), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(243, 243, 247));
      this.AddColor("TabButtonText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
      this.AddColor("TabButtonTextDisabled", SystemColors.GrayText, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.FromArgb(141, 141, 141));
      this.AddColor("TabButtonShade", Color.FromArgb(128, 0, 0, 0), Color.FromArgb(128, 0, 0, 0), Color.FromArgb(128, 0, 0, 0), Color.FromArgb(128, 0, 0, 0), Color.Empty, Color.FromArgb(128, 0, 0, 0));
      this.AddColor("TabButtonActiveBorder", SystemColors.ControlDark, Color.FromArgb(0, 45, 150), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
      this.AddColor("TabButtonActiveBackground1", SystemColors.Control, Color.FromArgb(142, 179, 231), Color.FromArgb(217, 217, 176), Color.FromArgb(215, 215, 229), SystemColors.Control, Color.FromArgb(215, 219, 224));
      this.AddColor("TabButtonActiveBackground2", SystemColors.ControlLightLight, Color.FromArgb(223, 237, 254), Color.FromArgb(242, 240, 228), Color.FromArgb(243, 243, 247), SystemColors.Control, Color.FromArgb(180, 187, 197));
      this.AddColor("TabButtonActiveText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
      this.AddColor("TabButtonHotBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(153, 157, 169), Color.FromArgb(153, 157, 169), SystemColors.ControlDark, Color.FromArgb((int) byte.MaxValue, 189, 105));
      this.AddColor("TabButtonHotBackground1", Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), Color.FromArgb(254, 145, 78), SystemColors.Control, Color.FromArgb((int) byte.MaxValue, 231, 162));
      this.AddColor("TabButtonHotBackground2", Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), Color.FromArgb((int) byte.MaxValue, 211, 142), SystemColors.Control, Color.FromArgb((int) byte.MaxValue, 219, 117));
      this.AddColor("TabButtonHotText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
    }

    private void AddMarkupLabelColors()
    {
      this.AddColor("MarkupLabelBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
      this.AddColor("MarkupLabelBackground1", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
      this.AddColor("MarkupLabelBackground2", Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
    }

    private void AddMarkupTextColors()
    {
      this.AddColor("MarkupText", SystemColors.ControlText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
      this.AddColor("MarkupTextAnchor", Color.Blue, Color.Blue, Color.Blue, Color.Blue, SystemColors.HotTrack, Color.Blue);
      this.AddColor("MarkupTextAnchorHot", Color.Purple, Color.Purple, Color.Purple, Color.Purple, SystemColors.HotTrack, Color.Purple);
      this.AddColor("MarkupTextAnchorActive", Color.Purple, Color.Purple, Color.Purple, Color.Purple, SystemColors.HotTrack, Color.Purple);
      this.AddColor("MarkupTextAnchorDisabled", SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText);
      this.AddColor("MarkupTextCustom1", SystemColors.ControlText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
      this.AddColor("MarkupTextCustom2", SystemColors.ControlText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
      this.AddColor("MarkupTextCustom3", SystemColors.ControlText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
      this.AddColor("MarkupTextCustom4", SystemColors.ControlText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
      this.AddColor("MarkupTextCustom5", SystemColors.ControlText, Color.Black, Color.Black, Color.Black, SystemColors.ControlText, Color.Black);
    }

    private void AddCompositeColors()
    {
      this.AddColor("CompositeScrollBarBorder", "@ScrollBarBorder");
      this.AddColor("CompositeScrollBarBackground1", "@ScrollBarBackground1");
      this.AddColor("CompositeScrollBarBackground2", "@ScrollBarBackground2");
      this.AddColor("CompositeScrollBarPressedBorder", "@ScrollBarPressedBorder");
      this.AddColor("CompositeScrollBarPressedBackground1", "@ScrollBarPressedBackground1");
      this.AddColor("CompositeScrollBarPressedBackground2", "@ScrollBarPressedBackground2");
      this.AddColor("CompositeScrollButtonDisabledBorder", "@CompositeButtonDisabledBorder");
      this.AddColor("CompositeScrollButtonDisabledBackground1", "@CompositeButtonDisabledBackground1");
      this.AddColor("CompositeScrollButtonDisabledBackground2", "@CompositeButtonDisabledBackground2");
      this.AddColor("CompositeScrollButtonBorder", "@CompositeButtonBorder");
      this.AddColor("CompositeScrollButtonBackground1", "@CompositeButtonBackground1");
      this.AddColor("CompositeScrollButtonBackground2", "@CompositeButtonBackground2");
      this.AddColor("CompositeScrollButtonHotBorder", "@CompositeItemHotBorder");
      this.AddColor("CompositeScrollButtonHotBackground1", "@CompositeItemHotBackground1");
      this.AddColor("CompositeScrollButtonHotBackground2", "@CompositeItemHotBackground2");
      this.AddColor("CompositeScrollButtonPressedBorder", "@CompositeItemPressedBorder");
      this.AddColor("CompositeScrollButtonPressedBackground1", "@CompositeItemPressedBackground1");
      this.AddColor("CompositeScrollButtonPressedBackground2", "@CompositeItemPressedBackground2");
      this.AddColor("CompositeBalloonBorder", SystemColors.WindowFrame, Color.FromArgb(157, 190, 217), Color.FromArgb(195, 205, 149), Color.FromArgb(118, 118, 118), SystemColors.ControlDark, Color.FromArgb(118, 118, 118));
      this.AddColor("CompositeBalloonBackground1", SystemColors.Info, Color.FromArgb(243, 249, 254), Color.FromArgb(254, 253, 239), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), SystemColors.Control, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("CompositeBalloonBackground2", SystemColors.Info, Color.FromArgb(199, 216, 237), Color.FromArgb(212, 215, 187), Color.FromArgb(228, 228, 240), SystemColors.Control, Color.FromArgb(228, 228, 240));
      this.AddColor("CompositeBorder", Color.FromArgb(134, 134, 134), Color.FromArgb(134, 134, 134), Color.FromArgb(134, 134, 134), Color.FromArgb(134, 134, 134), SystemColors.ControlDark, Color.FromArgb(134, 134, 134));
      this.AddColor("CompositeBackground1", Color.White, Color.White, Color.White, Color.White, SystemColors.Control, Color.White);
      this.AddColor("CompositeBackground2", Color.White, Color.White, Color.White, Color.White, SystemColors.Control, Color.White);
      this.AddColor("CompositeIconBackground1", SystemColors.ControlLight, Color.FromArgb(233, 238, 238), Color.FromArgb(254, 253, 239), Color.FromArgb(233, 238, 238), SystemColors.Control, Color.FromArgb(239, 239, 239));
      this.AddColor("CompositeIconBackground2", SystemColors.ControlLight, Color.FromArgb(233, 238, 238), Color.FromArgb(254, 253, 239), Color.FromArgb(233, 238, 238), SystemColors.Control, Color.FromArgb(239, 239, 239));
      this.AddColor("CompositeIconBackgroundBorder", SystemColors.ControlDark, Color.FromArgb(197, 197, 197), Color.FromArgb(197, 197, 197), Color.FromArgb(197, 197, 197), SystemColors.Control, Color.FromArgb(197, 197, 197));
      this.AddColor("CompositeButtonBorder", SystemColors.ControlDark, Color.FromArgb(119, 147, 185), Color.FromArgb(195, 205, 149), Color.FromArgb(203, 203, 204), SystemColors.Control, Color.FromArgb(141, 149, 155));
      this.AddColor("CompositeButtonBackground1", SystemColors.ControlLightLight, Color.FromArgb(237, 242, 248), Color.FromArgb(254, 253, 239), Color.FromArgb(243, 245, 245), SystemColors.Control, Color.FromArgb(223, 230, 230));
      this.AddColor("CompositeButtonBackground2", SystemColors.Control, Color.FromArgb(219, 225, 244), Color.FromArgb(212, 215, 187), Color.FromArgb(225, 228, 232), SystemColors.Control, Color.FromArgb(206, 213, 215));
      this.AddColor("CompositeButtonDisabledBorder", SystemColors.ControlDark, Color.FromArgb(119, 147, 185), Color.FromArgb(195, 205, 149), Color.FromArgb(203, 203, 204), SystemColors.Control, Color.FromArgb(141, 149, 155));
      this.AddColor("CompositeButtonDisabledBackground1", SystemColors.ControlLightLight, Color.FromArgb(237, 242, 248), Color.FromArgb(254, 253, 239), Color.FromArgb(243, 245, 245), SystemColors.Control, Color.FromArgb(223, 230, 230));
      this.AddColor("CompositeButtonDisabledBackground2", SystemColors.Control, Color.FromArgb(219, 225, 244), Color.FromArgb(212, 215, 187), Color.FromArgb(225, 228, 232), Color.FromArgb(219, 225, 244), Color.FromArgb(206, 213, 215));
      this.AddColor("CompositeGroupBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeGroupBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeGroupBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeItemBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeItemBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeItemBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeItemDisabledBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeItemDisabledBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeItemDisabledBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("CompositeItemExpandedBorder", Color.FromArgb(214, 181, 100), Color.FromArgb(214, 181, 100), Color.FromArgb(214, 181, 100), Color.FromArgb(214, 181, 100), SystemColors.ControlDark, Color.FromArgb(214, 181, 100));
      this.AddColor("CompositeItemExpandedBackground1", Color.FromArgb((int) byte.MaxValue, 250, 211), Color.FromArgb((int) byte.MaxValue, 250, 211), Color.FromArgb((int) byte.MaxValue, 250, 211), Color.FromArgb((int) byte.MaxValue, 250, 211), SystemColors.Control, Color.FromArgb((int) byte.MaxValue, 250, 211));
      this.AddColor("CompositeItemExpandedBackground2", Color.FromArgb((int) byte.MaxValue, 214, 73), Color.FromArgb((int) byte.MaxValue, 214, 73), Color.FromArgb((int) byte.MaxValue, 214, 73), Color.FromArgb((int) byte.MaxValue, 214, 73), SystemColors.Control, Color.FromArgb((int) byte.MaxValue, 214, 73));
      this.AddColor("CompositeItemHotBorder", Color.FromArgb(214, 181, 100), Color.FromArgb(214, 181, 100), Color.FromArgb(214, 181, 100), Color.FromArgb(214, 181, 100), SystemColors.Highlight, Color.FromArgb(214, 181, 100));
      this.AddColor("CompositeItemHotBackground1", Color.FromArgb((int) byte.MaxValue, 250, 211), Color.FromArgb((int) byte.MaxValue, 250, 211), Color.FromArgb((int) byte.MaxValue, 250, 211), Color.FromArgb((int) byte.MaxValue, 250, 211), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 250, 211));
      this.AddColor("CompositeItemHotBackground2", Color.FromArgb((int) byte.MaxValue, 214, 73), Color.FromArgb((int) byte.MaxValue, 214, 73), Color.FromArgb((int) byte.MaxValue, 214, 73), Color.FromArgb((int) byte.MaxValue, 214, 73), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 214, 73));
      this.AddColor("CompositeItemPressedBorder", Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), SystemColors.Highlight, Color.FromArgb(142, 129, 101));
      this.AddColor("CompositeItemPressedBackground1", Color.FromArgb(252, 213, 165), Color.FromArgb(252, 213, 165), Color.FromArgb(252, 213, 165), Color.FromArgb(252, 213, 165), SystemColors.Highlight, Color.FromArgb(252, 213, 165));
      this.AddColor("CompositeItemPressedBackground2", Color.FromArgb(250, 146, 42), Color.FromArgb(250, 146, 42), Color.FromArgb(250, 146, 42), Color.FromArgb(250, 146, 42), SystemColors.Highlight, Color.FromArgb(250, 146, 42));
      this.AddColor("CompositeText", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(0, 0, 0));
      this.AddColor("CompositeTextExpanded", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(0, 0, 0));
      this.AddColor("CompositeTextHot", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(0, 0, 0));
      this.AddColor("CompositeTextPressed", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(0, 0, 0));
      this.AddColor("CompositeTextDisabled", Color.DarkGray, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.DarkGray);
      this.AddColor("CompositeSeparator1", Color.FromArgb(197, 197, 197), Color.FromArgb(197, 197, 197), Color.FromArgb(197, 197, 197), Color.FromArgb(197, 197, 197), SystemColors.ControlDark, Color.FromArgb(197, 197, 197));
      this.AddColor("CompositeSeparator2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
    }

    private void AddHotkeyWindowColors()
    {
      this.AddColor("HotkeyWindowBorder", Color.FromArgb(118, 118, 118), Color.FromArgb(118, 118, 118), Color.FromArgb(118, 118, 118), Color.FromArgb(118, 118, 118), SystemColors.ControlDark, Color.FromArgb(118, 118, 118));
      this.AddColor("HotkeyWindowBackground1", Color.White, Color.White, Color.White, Color.White, SystemColors.Window, Color.White);
      this.AddColor("HotkeyWindowBackground2", SystemColors.Info, Color.FromArgb(208, 222, 241), Color.FromArgb(224, 231, 184), Color.FromArgb(208, 222, 241), SystemColors.Window, Color.FromArgb(228, 228, 240));
      this.AddColor("HotkeyWindowText", Color.Black, Color.Black, Color.Black, Color.Black, SystemColors.WindowText, Color.Black);
    }

    private void AddRibbonColors()
    {
      this.AddColor("RibbonBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonBackground1", SystemColors.Control, Color.FromArgb(191, 219, (int) byte.MaxValue), Color.FromArgb(222, 232, 183), Color.FromArgb(208, 212, 221), Color.Transparent, Color.FromArgb(83, 83, 83));
      this.AddColor("RibbonBackground2", SystemColors.Control, Color.FromArgb(191, 219, (int) byte.MaxValue), Color.FromArgb(222, 232, 183), Color.FromArgb(208, 212, 221), Color.Transparent, Color.FromArgb(83, 83, 83));
      this.AddColor("RibbonContentBorder", SystemColors.ControlDark, Color.FromArgb(142, 179, 231), Color.FromArgb(195, 205, 149), Color.FromArgb(160, 160, 160), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
      this.AddColor("RibbonContentBackground1", SystemColors.Control, Color.FromArgb(227, 244, 254), Color.FromArgb(254, 253, 239), Color.FromArgb(238, 241, 246), SystemColors.Control, Color.FromArgb(215, 219, 224));
      this.AddColor("RibbonContentBackground2", SystemColors.ControlLight, Color.FromArgb(199, 216, 237), Color.FromArgb(212, 215, 187), Color.FromArgb(214, 220, 231), SystemColors.Control, Color.FromArgb(180, 187, 197));
      this.AddColor("RibbonContentShade", Color.FromArgb(92, 0, 0, 0), Color.FromArgb(92, 0, 0, 0), Color.FromArgb(92, 0, 0, 0), Color.FromArgb(92, 0, 0, 0), Color.Empty, Color.FromArgb(92, 0, 0, 0));
      this.AddColor("RibbonDropIndicatorBackground", Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red);
      this.AddColor("RibbonDropIndicatorBorder", Color.DarkRed, Color.DarkRed, Color.DarkRed, Color.DarkRed, Color.DarkRed, Color.DarkRed);
      this.AddColor("RibbonTabStripBorder", SystemColors.ControlDark, Color.FromArgb(142, 179, 231), Color.FromArgb(195, 205, 149), Color.FromArgb(160, 160, 160), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
      this.AddColor("RibbonTabStripBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonTabStripBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonTabStripNavigationAreaBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonTabStripNavigationAreaBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonTabStripNavigationAreaBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonTabStripNavigationButtonHot1", "@CompositeItemHotBackground1");
      this.AddColor("RibbonTabStripNavigationButtonHot2", "@CompositeItemHotBackground2");
      this.AddColor("RibbonTabStripNavigationButtonHotBorder", "@CompositeItemHotBorder");
      this.AddColor("RibbonTabStripNavigationButtonActive1", "@CompositeItemPressedBackground1");
      this.AddColor("RibbonTabStripNavigationButtonActive2", "@CompositeItemPressedBackground2");
      this.AddColor("RibbonTabStripNavigationButtonActiveBorder", "@CompositeItemPressedBorder");
      this.AddColor("RibbonPageBorder", SystemColors.ControlDark, Color.FromArgb(153, 157, 169), Color.FromArgb(117, 141, 94), Color.FromArgb(124, 124, 148), Color.Empty, Color.FromArgb(123, 123, 132));
      this.AddColor("RibbonPageBackground1", SystemColors.ControlLightLight, Color.FromArgb(243, 249, 254), Color.FromArgb(254, 253, 239), Color.FromArgb(238, 241, 246), SystemColors.Control, Color.FromArgb(215, 219, 224));
      this.AddColor("RibbonPageBackground2", SystemColors.Control, Color.FromArgb(199, 216, 237), Color.FromArgb(212, 215, 187), Color.FromArgb(214, 220, 231), SystemColors.Control, Color.FromArgb(180, 187, 197));
      this.AddColor("RibbonTabButtonBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonTabButtonBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonTabButtonBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonTabButtonMask", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(159, 170, 184));
      this.AddColor("RibbonTabButtonMaskDisabled", SystemColors.GrayText, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.FromArgb(141, 141, 141));
      this.AddColor("RibbonTabButtonText", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.White);
      this.AddColor("RibbonTabButtonTextDisabled", SystemColors.GrayText, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.FromArgb(141, 141, 141));
      this.AddColor("RibbonTabButtonShade", Color.FromArgb(96, 0, 0, 0), Color.FromArgb(96, 0, 0, 0), Color.FromArgb(96, 0, 0, 0), Color.FromArgb(96, 0, 0, 0), Color.Empty, Color.FromArgb(96, 0, 0, 0));
      this.AddColor("RibbonTabButtonActiveBorder", SystemColors.ControlDark, Color.FromArgb(142, 179, 231), Color.FromArgb(195, 205, 149), Color.FromArgb(160, 160, 160), SystemColors.ControlDark, Color.FromArgb(123, 123, 123));
      this.AddColor("RibbonTabButtonActiveBackground1", SystemColors.ControlLightLight, Color.FromArgb(243, 249, 254), Color.FromArgb(254, 253, 239), Color.FromArgb(245, 245, 247), SystemColors.Control, Color.FromArgb(249, 249, 249));
      this.AddColor("RibbonTabButtonActiveBackground2", SystemColors.ControlLight, Color.FromArgb(219, 230, 245), Color.FromArgb(212, 215, 187), Color.FromArgb(229, 234, 241), SystemColors.Control, Color.FromArgb(214, 217, 223));
      this.AddColor("RibbonTabButtonActiveText", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.Black);
      this.AddColor("RibbonTabButtonHotBorder", SystemColors.ControlDarkDark, Color.FromArgb(142, 179, 231), Color.FromArgb(195, 205, 149), Color.FromArgb(190, 190, 190), SystemColors.ControlDark, Color.FromArgb(123, 123, 132));
      this.AddColor("RibbonTabButtonHotBackground1", SystemColors.ControlLightLight, Color.FromArgb(227, 244, 254), Color.FromArgb(254, 253, 239), Color.FromArgb(247, 248, 250), SystemColors.Control, Color.FromArgb(215, 219, 224));
      this.AddColor("RibbonTabButtonHotBackground2", SystemColors.Control, Color.FromArgb(196, 221, 254), Color.FromArgb(195, 205, 149), Color.FromArgb(238, 241, 245), SystemColors.Control, Color.FromArgb(180, 187, 197));
      this.AddColor("RibbonTabButtonHotOutline", Color.FromArgb(128, (int) byte.MaxValue, 215, 74), Color.FromArgb(128, (int) byte.MaxValue, 215, 74), Color.FromArgb(128, (int) byte.MaxValue, 215, 74), Color.FromArgb(128, (int) byte.MaxValue, 215, 74), SystemColors.Highlight, Color.FromArgb(128, (int) byte.MaxValue, 215, 74));
      this.AddColor("RibbonTabButtonHotText", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.Black);
    }

    private void AddRibbonPanelColors()
    {
      this.AddColor("RibbonPanelBorder", SystemColors.ControlDark, Color.FromArgb(157, 190, 217), Color.FromArgb(195, 205, 149), Color.FromArgb(131, 133, 134), SystemColors.ControlDark, Color.FromArgb(123, 123, 132));
      this.AddColor("RibbonPanelBackground1", SystemColors.ControlLightLight, Color.FromArgb(243, 249, 254), Color.FromArgb(254, 253, 239), Color.FromArgb(238, 241, 246), SystemColors.Control, Color.FromArgb(215, 219, 224));
      this.AddColor("RibbonPanelBackground2", SystemColors.Control, Color.FromArgb(199, 216, 237), Color.FromArgb(212, 215, 187), Color.FromArgb(214, 220, 231), SystemColors.Control, Color.FromArgb(180, 187, 197));
      this.AddColor("RibbonPanelHotBorder", SystemColors.ControlDark, Color.FromArgb(169, 198, 221), Color.FromArgb(195, 205, 149), Color.FromArgb(131, 133, 134), SystemColors.ControlDark, Color.FromArgb(123, 123, 132));
      this.AddColor("RibbonPanelHotBackground1", SystemColors.ControlLightLight, Color.FromArgb(248, 251, 254), Color.FromArgb(252, 251, 236), Color.FromArgb(247, 248, 250), SystemColors.Control, Color.FromArgb(236, 237, 240));
      this.AddColor("RibbonPanelHotBackground2", SystemColors.ControlLight, Color.FromArgb(227, 236, 246), Color.FromArgb(236, 233, 216), Color.FromArgb(238, 241, 245), SystemColors.Control, Color.FromArgb(227, 230, 233));
      this.AddColor("RibbonPanelActiveBorder", SystemColors.ControlDarkDark, Color.FromArgb(135, 166, 191), Color.FromArgb(195, 205, 149), Color.FromArgb(131, 133, 134), SystemColors.ControlDark, Color.FromArgb(123, 123, 132));
      this.AddColor("RibbonPanelActiveBackground1", SystemColors.Control, Color.FromArgb(199, 216, 237), Color.FromArgb(194, 205, 149), Color.FromArgb(225, 227, 229), SystemColors.Highlight, Color.FromArgb(225, 226, 226));
      this.AddColor("RibbonPanelActiveBackground2", SystemColors.ControlDark, Color.FromArgb(136, 167, 208), Color.FromArgb(168, 182, 128), Color.FromArgb(179, 185, 195), SystemColors.Highlight, Color.FromArgb(156, 162, 171));
      this.AddColor("RibbonPanelCaptionArea1", SystemColors.Control, Color.FromArgb(194, 216, 241), Color.FromArgb(194, 205, 149), Color.FromArgb(223, 227, 239), SystemColors.Control, Color.FromArgb(181, 183, 183));
      this.AddColor("RibbonPanelCaptionArea2", SystemColors.Control, Color.FromArgb(192, 216, 239), Color.FromArgb(194, 205, 149), Color.FromArgb(195, 199, 209), SystemColors.Control, Color.FromArgb(156, 158, 158));
      this.AddColor("RibbonPanelHotCaptionArea1", SystemColors.ControlLight, Color.FromArgb(214, 237, (int) byte.MaxValue), Color.FromArgb(204, 216, 157), Color.FromArgb(222, 226, 238), SystemColors.Control, Color.FromArgb(170, 170, 170));
      this.AddColor("RibbonPanelHotCaptionArea2", SystemColors.ControlLight, Color.FromArgb(200, 224, (int) byte.MaxValue), Color.FromArgb(204, 216, 157), Color.FromArgb(179, 185, 199), SystemColors.Control, Color.FromArgb(110, 110, 110));
      this.AddColor("RibbonPanelCaptionShowDialog", SystemColors.MenuText, Color.FromArgb(62, 106, 170), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(101, 104, 112));
      this.AddColor("RibbonPanelCaptionShowDialogHot", SystemColors.MenuText, Color.FromArgb(62, 106, 170), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(101, 104, 112));
      this.AddColor("RibbonPanelCaptionShowDialogDisabled", Color.DarkGray, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.FromArgb(101, 104, 112));
      this.AddColor("RibbonPanelText", SystemColors.MenuText, Color.FromArgb(62, 106, 170), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("RibbonPanelTextHot", SystemColors.MenuText, Color.FromArgb(62, 106, 170), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("RibbonPanelTextActive", SystemColors.MenuText, Color.FromArgb(62, 106, 170), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("RibbonPanelTextDisabled", Color.DarkGray, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("RibbonInputBoxBackground", SystemColors.ControlLightLight, Color.FromArgb(234, 242, 251), Color.FromArgb(254, 253, 239), Color.FromArgb(231, 234, 238), SystemColors.Window, Color.FromArgb(232, 232, 232));
      this.AddColor("RibbonInputBoxOuterBackground1", SystemColors.ControlLightLight, Color.FromArgb(234, 242, 251), Color.FromArgb(254, 253, 239), Color.FromArgb(231, 234, 238), SystemColors.Window, Color.FromArgb(232, 232, 232));
      this.AddColor("RibbonInputBoxOuterBackground2", SystemColors.ControlLightLight, Color.FromArgb(234, 242, 251), Color.FromArgb(254, 253, 239), Color.FromArgb(231, 234, 238), SystemColors.Window, Color.FromArgb(232, 232, 232));
      this.AddColor("RibbonItemBarBorder", SystemColors.ControlDark, Color.FromArgb(155, 183, 220), Color.FromArgb(195, 205, 149), Color.FromArgb(203, 203, 204), Color.Empty, Color.FromArgb(141, 149, 155));
      this.AddColor("RibbonItemBarBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonItemBarBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonItemBarItemDisabledBorder", SystemColors.ControlDark, Color.FromArgb(155, 183, 220), Color.FromArgb(195, 205, 149), Color.FromArgb(221, 221, 221), Color.Empty, Color.FromArgb(141, 149, 155));
      this.AddColor("RibbonItemBarItemDisabledBackground1", SystemColors.ControlLightLight, Color.FromArgb(227, 237, 251), Color.FromArgb(254, 253, 239), Color.FromArgb(243, 245, 245), Color.Empty, Color.FromArgb(215, 219, 224));
      this.AddColor("RibbonItemBarItemDisabledBackground2", SystemColors.Control, Color.FromArgb(188, 208, 233), Color.FromArgb(212, 215, 187), Color.FromArgb(225, 228, 232), Color.Empty, Color.FromArgb(180, 187, 197));
      this.AddColor("RibbonItemBarItemBorder", SystemColors.ControlDark, Color.FromArgb(155, 183, 220), Color.FromArgb(195, 205, 149), Color.FromArgb(221, 221, 221), Color.Empty, Color.FromArgb(141, 149, 155));
      this.AddColor("RibbonItemBarItemBackground1", SystemColors.ControlLightLight, Color.FromArgb(227, 237, 251), Color.FromArgb(254, 253, 239), Color.FromArgb(243, 245, 245), Color.Empty, Color.FromArgb(215, 219, 224));
      this.AddColor("RibbonItemBarItemBackground2", SystemColors.ControlLight, Color.FromArgb(188, 208, 233), Color.FromArgb(212, 215, 187), Color.FromArgb(225, 228, 232), Color.Empty, Color.FromArgb(180, 187, 197));
      this.AddColor("RibbonItemBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonItemBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonItemBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonItemDisabledBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonItemDisabledBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonItemDisabledBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonItemText", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(0, 0, 0));
      this.AddColor("RibbonItemTextDisabled", Color.DarkGray, Color.DarkGray, Color.DarkGray, Color.DarkGray, SystemColors.GrayText, Color.DarkGray);
      this.AddColor("RibbonItemHotBorder", Color.FromArgb(191, 179, 129), Color.FromArgb(191, 179, 129), Color.FromArgb(191, 179, 129), Color.FromArgb(191, 179, 129), Color.Empty, Color.FromArgb(191, 179, 129));
      this.AddColor("RibbonItemHotBackground1", Color.FromArgb((int) byte.MaxValue, 254, 227), Color.FromArgb((int) byte.MaxValue, 254, 227), Color.FromArgb((int) byte.MaxValue, 254, 227), Color.FromArgb((int) byte.MaxValue, 254, 227), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 254, 227));
      this.AddColor("RibbonItemHotBackground2", Color.FromArgb((int) byte.MaxValue, 215, 74), Color.FromArgb((int) byte.MaxValue, 215, 74), Color.FromArgb((int) byte.MaxValue, 215, 74), Color.FromArgb((int) byte.MaxValue, 215, 74), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 215, 74));
      this.AddColor("RibbonItemHotText", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(0, 0, 0));
      this.AddColor("RibbonItemActiveBorder", Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), Color.Empty, Color.FromArgb(142, 129, 101));
      this.AddColor("RibbonItemActiveBackground1", Color.FromArgb(254, 214, 169), Color.FromArgb(254, 214, 169), Color.FromArgb(254, 214, 169), Color.FromArgb(254, 214, 169), SystemColors.Highlight, Color.FromArgb(254, 214, 169));
      this.AddColor("RibbonItemActiveBackground2", Color.FromArgb((int) byte.MaxValue, 165, 64), Color.FromArgb((int) byte.MaxValue, 165, 64), Color.FromArgb((int) byte.MaxValue, 165, 64), Color.FromArgb((int) byte.MaxValue, 165, 64), SystemColors.Highlight, Color.FromArgb((int) byte.MaxValue, 165, 64));
      this.AddColor("RibbonItemActiveText", SystemColors.MenuText, Color.FromArgb(21, 66, 139), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.MenuText, Color.FromArgb(0, 0, 0));
      this.AddColor("RibbonSeparator1", SystemColors.ControlLightLight, Color.FromArgb(243, 249, 254), Color.FromArgb(254, 253, 239), Color.FromArgb(238, 241, 246), SystemColors.Control, Color.FromArgb(215, 219, 224));
      this.AddColor("RibbonSeparator2", SystemColors.ControlDark, Color.FromArgb(157, 190, 217), Color.FromArgb(195, 205, 149), Color.FromArgb(131, 133, 134), SystemColors.ControlDark, Color.FromArgb(123, 123, 132));
    }

    private void AddRibbonCaptionColors()
    {
      this.AddColor("RibbonCaptionBorder", SystemColors.ActiveBorder, Color.FromArgb(176, 207, 247), Color.FromArgb(195, 205, 149), Color.FromArgb(172, 175, 183), Color.Empty, Color.FromArgb(47, 48, 48));
      this.AddColor("RibbonCaptionBackground1", SystemColors.ActiveCaption, Color.FromArgb(227, 235, 246), Color.FromArgb(254, 253, 239), Color.FromArgb(231, 232, 235), SystemColors.ActiveCaption, Color.FromArgb(77, 81, 82));
      this.AddColor("RibbonCaptionBackground2", SystemColors.InactiveCaption, Color.FromArgb(202, 222, 247), Color.FromArgb(212, 215, 187), Color.FromArgb(186, 193, 202), SystemColors.ActiveCaption, Color.FromArgb(47, 48, 48));
      this.AddColor("RibbonCaptionInactiveBorder", SystemColors.InactiveBorder, Color.FromArgb(204, 218, 236), Color.FromArgb(188, 193, 172), Color.FromArgb(182, 181, 181), Color.Empty, Color.FromArgb(146, 146, 146));
      this.AddColor("RibbonCaptionInactiveBackground1", SystemColors.InactiveCaption, Color.FromArgb(227, 231, 236), Color.FromArgb(237, 235, 211), Color.FromArgb(243, 244, 245), SystemColors.InactiveCaption, Color.FromArgb(153, 154, 159));
      this.AddColor("RibbonCaptionInactiveBackground2", SystemColors.Desktop, Color.FromArgb(216, 225, 236), Color.FromArgb(214, 216, 187), Color.FromArgb(221, 224, 229), SystemColors.InactiveCaption, Color.FromArgb(146, 146, 146));
      this.AddColor("RibbonCaptionApplicationText", SystemColors.ActiveCaptionText, Color.FromArgb(62, 106, 170), Color.FromArgb(62, 106, 170), Color.FromArgb(62, 106, 170), SystemColors.ActiveCaptionText, Color.FromArgb(174, 209, (int) byte.MaxValue));
      this.AddColor("RibbonCaptionDocumentText", SystemColors.ActiveCaptionText, Color.FromArgb(105, 112, 121), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), SystemColors.ActiveCaptionText, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("RibbonCaptionInactiveApplicationText", SystemColors.InactiveCaptionText, Color.FromArgb(160, 160, 160), Color.FromArgb(160, 160, 160), Color.FromArgb(160, 160, 160), SystemColors.InactiveCaptionText, Color.FromArgb(210, 210, 210));
      this.AddColor("RibbonCaptionInactiveDocumentText", SystemColors.InactiveCaptionText, Color.FromArgb(160, 160, 160), Color.FromArgb(160, 160, 160), Color.FromArgb(160, 160, 160), SystemColors.InactiveCaptionText, Color.FromArgb(210, 210, 210));
      this.AddColor("RibbonCaptionButtonBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonCaptionButtonBackground1", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonCaptionButtonBackground2", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonCaptionButtonHotBorder", SystemColors.Desktop, Color.FromArgb(162, 197, 244), Color.FromArgb(195, 205, 149), Color.FromArgb(181, 191, 205), Color.Empty, Color.FromArgb(86, 96, 109));
      this.AddColor("RibbonCaptionButtonHotBackground1", Color.White, Color.White, Color.White, Color.White, SystemColors.Highlight, Color.FromArgb(195, 201, 208));
      this.AddColor("RibbonCaptionButtonHotBackground2", SystemColors.ActiveCaption, Color.FromArgb(210, 228, 254), Color.FromArgb(236, 233, 216), Color.FromArgb(222, 230, 242), SystemColors.Highlight, Color.FromArgb(91, 105, 123));
      this.AddColor("RibbonCaptionButtonPressedBorder", SystemColors.Desktop, Color.FromArgb(162, 191, 227), Color.FromArgb(117, 141, 94), Color.FromArgb(151, 156, 160), SystemColors.Highlight, Color.FromArgb(22, 23, 27));
      this.AddColor("RibbonCaptionButtonPressedBackground1", Color.White, Color.FromArgb(196, 219, 250), Color.FromArgb(230, 230, 209), Color.FromArgb(205, 209, 213), SystemColors.Highlight, Color.FromArgb(102, 102, 102));
      this.AddColor("RibbonCaptionButtonPressedBackground2", SystemColors.InactiveCaption, Color.FromArgb(148, 186, 229), Color.FromArgb(164, 180, 120), Color.FromArgb(137, 144, 151), SystemColors.Highlight, Color.FromArgb(0, 0, 0));
      this.AddColor("RibbonCaptionButtonMask", SystemColors.InactiveCaptionText, Color.FromArgb(135, 162, 197), Color.FromArgb(117, 141, 94), Color.FromArgb(123, 138, 157), SystemColors.ActiveCaptionText, Color.FromArgb(159, 170, 184));
      this.AddColor("RibbonCaptionButtonMaskInactive", SystemColors.InactiveCaptionText, Color.FromArgb(179, 192, 209), Color.FromArgb(188, 193, 172), Color.FromArgb(181, 187, 196), SystemColors.InactiveCaptionText, Color.FromArgb(98, 99, 101));
    }

    private void AddRibbonLaunchBarColors()
    {
      this.AddColor("RibbonLaunchBarBackground1", SystemColors.Window, Color.FromArgb(240, 248, (int) byte.MaxValue), Color.FromArgb(254, 253, 239), Color.FromArgb(238, 239, 240), Color.Empty, Color.FromArgb(123, 126, 132));
      this.AddColor("RibbonLaunchBarBackground2", SystemColors.ActiveCaption, Color.FromArgb(191, 207, 228), Color.FromArgb(195, 205, 149), Color.FromArgb(217, 218, 220), Color.Empty, Color.FromArgb(68, 68, 68));
      this.AddColor("RibbonLaunchBarBorder", SystemColors.Desktop, Color.FromArgb(154, 179, 213), Color.FromArgb(117, 141, 94), Color.FromArgb(177, 177, 178), Color.Empty, Color.FromArgb(0, 0, 0));
      this.AddColor("RibbonLaunchBarInactiveBackground1", SystemColors.Window, Color.FromArgb(233, 237, 243), Color.FromArgb(254, 253, 239), Color.FromArgb(238, 239, 240), Color.Empty, Color.FromArgb(179, 180, 183));
      this.AddColor("RibbonLaunchBarInactiveBackground2", SystemColors.InactiveCaption, Color.FromArgb(200, 211, 227), Color.FromArgb(212, 215, 187), Color.FromArgb(217, 218, 220), Color.Empty, Color.FromArgb(136, 136, 136));
      this.AddColor("RibbonLaunchBarInactiveBorder", SystemColors.Desktop, Color.FromArgb(188, 203, 222), Color.FromArgb(168, 182, 128), Color.FromArgb(177, 177, 178), Color.Empty, Color.FromArgb(107, 107, 107));
      this.AddColor("RibbonLaunchBarHostBackground1", SystemColors.Control, Color.FromArgb(175, 202, 236), Color.FromArgb(195, 205, 149), Color.FromArgb(181, 191, 205), SystemColors.Control, Color.FromArgb(123, 126, 132));
      this.AddColor("RibbonLaunchBarHostBackground2", SystemColors.Control, Color.FromArgb(175, 202, 236), Color.FromArgb(195, 205, 149), Color.FromArgb(181, 191, 205), SystemColors.Control, Color.FromArgb(68, 68, 68));
      this.AddColor("RibbonLaunchBarHostBorder", SystemColors.ControlDark, Color.FromArgb(126, 161, 205), Color.FromArgb(117, 141, 94), Color.FromArgb(105, 112, 121), SystemColors.ControlDark, Color.FromArgb(0, 0, 0));
    }

    private void AddRibbonApplicationButtonColors()
    {
      this.AddColor("RibbonApplicationButtonBorder", SystemColors.ControlDarkDark, Color.FromArgb(126, 138, 156), Color.FromArgb(101, 115, 65), Color.FromArgb(76, 83, 92), Color.Transparent, Color.FromArgb(0, 0, 0));
      this.AddColor("RibbonApplicationButtonGlow", SystemColors.ControlLight, Color.FromArgb(243, 245, 248), Color.FromArgb(199, 210, 155), Color.FromArgb(226, 227, 237), SystemColors.ControlDarkDark, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("RibbonApplicationButtonBackground1", SystemColors.ControlLight, Color.FromArgb(243, 245, 248), Color.FromArgb(252, 251, 237), Color.FromArgb(229, 230, 233), SystemColors.Control, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("RibbonApplicationButtonBackground2", SystemColors.ControlDark, Color.FromArgb(165, 180, 204), Color.FromArgb(214, 217, 189), Color.FromArgb(187, 194, 203), SystemColors.Control, Color.FromArgb(152, 152, 152));
      this.AddColor("RibbonApplicationButtonHotBorder", Color.FromArgb(105, 94, 51), Color.FromArgb(105, 94, 51), Color.FromArgb(105, 94, 51), Color.FromArgb(105, 94, 51), Color.Transparent, Color.FromArgb(105, 94, 51));
      this.AddColor("RibbonApplicationButtonHotGlow", Color.FromArgb((int) byte.MaxValue, 244, 63), Color.FromArgb((int) byte.MaxValue, 244, 63), Color.FromArgb((int) byte.MaxValue, 244, 63), Color.FromArgb((int) byte.MaxValue, 244, 63), SystemColors.ControlDarkDark, Color.FromArgb((int) byte.MaxValue, 244, 63));
      this.AddColor("RibbonApplicationButtonHotBackground1", Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), SystemColors.Control, Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      this.AddColor("RibbonApplicationButtonHotBackground2", Color.FromArgb(222, 168, 20), Color.FromArgb(222, 168, 20), Color.FromArgb(222, 168, 20), Color.FromArgb(222, 168, 20), SystemColors.Control, Color.FromArgb(222, 168, 20));
      this.AddColor("RibbonApplicationButtonPressedBorder", Color.FromArgb(94, 67, 34), Color.FromArgb(94, 67, 34), Color.FromArgb(94, 67, 34), Color.FromArgb(94, 67, 34), Color.Transparent, Color.FromArgb(94, 67, 34));
      this.AddColor("RibbonApplicationButtonPressedGlow", Color.FromArgb(253, 153, 25), Color.FromArgb(253, 153, 25), Color.FromArgb(253, 153, 25), Color.FromArgb(253, 153, 25), SystemColors.ControlDarkDark, Color.FromArgb(253, 153, 25));
      this.AddColor("RibbonApplicationButtonPressedBackground1", Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), SystemColors.Control, Color.FromArgb(253, 153, 25));
      this.AddColor("RibbonApplicationButtonPressedBackground2", Color.FromArgb(168, 101, 11), Color.FromArgb(168, 101, 11), Color.FromArgb(168, 101, 11), Color.FromArgb(168, 101, 11), SystemColors.Control, Color.FromArgb(168, 101, 11));
    }

    private void AddRibbonFormColors()
    {
      this.AddColor("RibbonFormBorder", SystemColors.WindowFrame, Color.FromArgb(59, 90, 130), Color.FromArgb(117, 141, 94), Color.FromArgb(148, 148, 148), SystemColors.ControlDarkDark, Color.FromArgb(0, 0, 0));
      this.AddColor("RibbonFormBackground1", SystemColors.ControlLight, Color.FromArgb(226, 235, 246), Color.FromArgb(240, 238, 225), Color.FromArgb(233, 237, 243), SystemColors.Control, Color.FromArgb(70, 70, 70));
      this.AddColor("RibbonFormBackground2", SystemColors.ControlLight, Color.FromArgb(226, 235, 246), Color.FromArgb(240, 238, 225), Color.FromArgb(233, 237, 243), SystemColors.Control, Color.FromArgb(70, 70, 70));
      this.AddColor("RibbonFormInactiveBorder", SystemColors.WindowFrame, Color.FromArgb(192, 198, 206), Color.FromArgb(188, 193, 172), Color.FromArgb(180, 185, 192), SystemColors.ControlDark, Color.FromArgb(0, 0, 0));
      this.AddColor("RibbonFormInactiveBackground1", SystemColors.Control, Color.FromArgb(212, 222, 236), Color.FromArgb(230, 232, 211), Color.FromArgb(214, 220, 231), SystemColors.Control, Color.FromArgb(153, 154, 159));
      this.AddColor("RibbonFormInactiveBackground2", SystemColors.Control, Color.FromArgb(212, 222, 236), Color.FromArgb(230, 232, 211), Color.FromArgb(214, 220, 231), SystemColors.Control, Color.FromArgb(153, 154, 159));
    }

    private void AddRibbonMenuColors()
    {
      this.AddColor("RibbonMenuBackground1", SystemColors.ControlLightLight, Color.FromArgb(215, 229, 247), Color.FromArgb(254, 253, 239), Color.FromArgb(231, 232, 235), SystemColors.Control, Color.FromArgb(103, 102, 103));
      this.AddColor("RibbonMenuBackground2", "@RibbonMenuBackground1");
      this.AddColor("RibbonMenuBorder", SystemColors.WindowFrame, Color.FromArgb(155, 175, 202), Color.FromArgb(117, 141, 94), Color.FromArgb(169, 174, 180), SystemColors.ControlDark, Color.FromArgb(66, 66, 66));
      this.AddColor("RibbonMenuHeaderFooter1", "@RibbonMenuBackground1");
      this.AddColor("RibbonMenuHeaderFooter2", SystemColors.Control, Color.FromArgb(183, 202, 230), Color.FromArgb(212, 215, 187), Color.FromArgb(186, 193, 202), SystemColors.Control, Color.FromArgb(88, 88, 88));
      this.AddColor("RibbonMenuHeaderFooterBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonMenuContentAreaBackground1", Color.White, Color.White, Color.White, Color.White, SystemColors.Control, Color.White);
      this.AddColor("RibbonMenuContentAreaBackground2", "@RibbonMenuContentAreaBackground1");
      this.AddColor("RibbonMenuContentAreaBorder", "@RibbonMenuBorder");
      this.AddColor("RibbonMenuCaptionBackground1", SystemColors.Control, Color.FromArgb(212, 231, 238), Color.FromArgb(254, 253, 239), Color.FromArgb(217, 218, 230), Color.Empty, Color.FromArgb(235, 235, 235));
      this.AddColor("RibbonMenuCaptionBackground2", "@RibbonMenuCaptionBackground1");
      this.AddColor("RibbonMenuCaptionBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
      this.AddColor("RibbonMenuDocumentAreaBackground1", SystemColors.ControlLightLight, Color.FromArgb(233, 234, 238), Color.FromArgb(248, 247, 237), Color.FromArgb(233, 234, 238), SystemColors.Control, Color.FromArgb(233, 234, 238));
      this.AddColor("RibbonMenuDocumentAreaBackground2", "@RibbonMenuDocumentAreaBackground1");
      this.AddColor("RibbonMenuDocumentAreaBorder", Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
    }

    private void AddScrollBarColors()
    {
      this.AddColor("ScrollBarBackground1", SystemColors.ControlLight, Color.FromArgb(252, 252, 252), Color.FromArgb(252, 252, 252), Color.FromArgb(252, 252, 252), SystemColors.ControlDark, Color.FromArgb(252, 252, 252));
      this.AddColor("ScrollBarBackground2", SystemColors.ControlLightLight, Color.FromArgb(240, 240, 240), Color.FromArgb(240, 240, 240), Color.FromArgb(240, 240, 240), SystemColors.ControlDark, Color.FromArgb(240, 240, 240));
      this.AddColor("ScrollBarBorder", SystemColors.ControlLight, Color.FromArgb(235, 237, 239), Color.FromArgb(235, 237, 239), Color.FromArgb(235, 237, 239), SystemColors.ControlDark, Color.FromArgb(235, 237, 239));
      this.AddColor("ScrollBarPressedBorder", SystemColors.ControlLight, Color.FromArgb(235, 237, 239), Color.FromArgb(235, 237, 239), Color.FromArgb(235, 237, 239), SystemColors.ControlDark, Color.FromArgb(235, 237, 239));
      this.AddColor("ScrollBarPressedBackground1", SystemColors.ControlDark, Color.FromArgb(204, 204, 204), Color.FromArgb(204, 204, 204), Color.FromArgb(204, 204, 204), SystemColors.ControlDark, Color.FromArgb(204, 204, 204));
      this.AddColor("ScrollBarPressedBackground2", SystemColors.ControlLight, Color.FromArgb(219, 219, 219), Color.FromArgb(219, 219, 219), Color.FromArgb(219, 219, 219), SystemColors.ControlDark, Color.FromArgb(219, 219, 219));
      this.AddColor("ScrollBarButtonDisabledBorder", "@CompositeButtonDisabledBorder");
      this.AddColor("ScrollBarButtonDisabledBackground1", "@CompositeButtonDisabledBackground1");
      this.AddColor("ScrollBarButtonDisabledBackground2", "@CompositeButtonDisabledBackground2");
      this.AddColor("ScrollBarButtonBorder", "@CompositeButtonBorder");
      this.AddColor("ScrollBarButtonBackground1", "@CompositeButtonBackground1");
      this.AddColor("ScrollBarButtonBackground2", "@CompositeButtonBackground2");
      this.AddColor("ScrollBarButtonHotBorder", "@CompositeItemHotBorder");
      this.AddColor("ScrollBarButtonHotBackground1", "@CompositeItemHotBackground1");
      this.AddColor("ScrollBarButtonHotBackground2", "@CompositeItemHotBackground2");
      this.AddColor("ScrollBarButtonPressedBorder", "@CompositeItemPressedBorder");
      this.AddColor("ScrollBarButtonPressedBackground1", "@CompositeItemPressedBackground1");
      this.AddColor("ScrollBarButtonPressedBackground2", "@CompositeItemPressedBackground2");
    }

    private void AddButtonColors()
    {
      this.AddColor("ButtonBackground1", SystemColors.ControlLight, Color.FromArgb(252, 252, 252), Color.FromArgb(252, 252, 252), Color.FromArgb(252, 252, 252), SystemColors.ControlDark, Color.FromArgb(252, 252, 252));
      this.AddColor("ButtonBackground2", SystemColors.ControlLightLight, Color.FromArgb(200, 200, 200), Color.FromArgb(200, 200, 200), Color.FromArgb(200, 200, 200), SystemColors.ControlDark, Color.FromArgb(200, 200, 200));
      this.AddColor("ButtonBorder", SystemColors.ControlLight, Color.FromArgb(134, 134, 134), Color.FromArgb(134, 134, 134), Color.FromArgb(134, 134, 134), SystemColors.ControlDark, Color.FromArgb(134, 134, 134));
      this.AddColor("ButtonText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
      this.AddColor("ButtonHotBackground1", SystemColors.ControlDark, Color.FromArgb((int) byte.MaxValue, 250, 211), Color.FromArgb((int) byte.MaxValue, 250, 211), Color.FromArgb((int) byte.MaxValue, 250, 211), SystemColors.ControlDark, Color.FromArgb((int) byte.MaxValue, 250, 211));
      this.AddColor("ButtonHotBackground2", SystemColors.ControlLight, Color.FromArgb((int) byte.MaxValue, 214, 73), Color.FromArgb((int) byte.MaxValue, 214, 73), Color.FromArgb((int) byte.MaxValue, 214, 73), SystemColors.ControlDark, Color.FromArgb((int) byte.MaxValue, 214, 73));
      this.AddColor("ButtonHotBorder", SystemColors.ControlLight, Color.FromArgb(214, 181, 100), Color.FromArgb(214, 181, 100), Color.FromArgb(214, 181, 100), SystemColors.ControlDark, Color.FromArgb(214, 181, 100));
      this.AddColor("ButtonHotText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
      this.AddColor("ButtonPressedBackground1", SystemColors.ControlDark, Color.FromArgb(252, 213, 165), Color.FromArgb(252, 213, 165), Color.FromArgb(252, 213, 165), SystemColors.ControlDark, Color.FromArgb(252, 213, 165));
      this.AddColor("ButtonPressedBackground2", SystemColors.ControlLight, Color.FromArgb(250, 146, 42), Color.FromArgb(250, 146, 42), Color.FromArgb(250, 146, 42), SystemColors.ControlDark, Color.FromArgb(250, 146, 42));
      this.AddColor("ButtonPressedBorder", SystemColors.ControlLight, Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), Color.FromArgb(142, 129, 101), SystemColors.ControlDark, Color.FromArgb(142, 129, 101));
      this.AddColor("ButtonPressedText", SystemColors.MenuText, Color.Black, Color.Black, Color.Black, SystemColors.MenuText, Color.Black);
      this.AddColor("ButtonDisabledBackground1", SystemColors.ControlLightLight, Color.FromArgb(252, 252, 252), Color.FromArgb(252, 252, 252), Color.FromArgb(252, 252, 252), SystemColors.Control, Color.FromArgb(252, 252, 252));
      this.AddColor("ButtonDisabledBackground2", SystemColors.Control, Color.FromArgb(220, 220, 220), Color.FromArgb(220, 220, 220), Color.FromArgb(220, 220, 220), SystemColors.Control, Color.FromArgb(220, 220, 220));
      this.AddColor("ButtonDisabledBorder", SystemColors.ControlDark, Color.FromArgb(160, 160, 160), Color.FromArgb(160, 160, 160), Color.FromArgb(160, 160, 160), SystemColors.Control, Color.FromArgb(160, 160, 160));
      this.AddColor("ButtonDisabledText", SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText, SystemColors.GrayText);
      this.AddColor("ButtonFocusedInnerGlow", "@ButtonPressedBackground2");
    }

    [Description("Defines an Empty QColor. This is basically created to be referenced to make a color property empty.")]
    [Category("Misc")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.None)]
    public QColor Empty => this[nameof (Empty)];

    [QColorDesignVisible(QColorDesignVisibilityType.None)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Defines a Transparent QColor. This is basically created to be referenced to make a color property empty.")]
    [Category("Misc")]
    public QColor Transparent => this[nameof (Transparent)];

    [Description("Gets the color of the text of the components")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.All)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    [Category("Misc")]
    public QColor TextColor => this[nameof (TextColor)];

    [Category("Misc")]
    [Description("Gets the color of the disabled text of the components")]
    [QColorDesignVisible(QColorDesignVisibilityType.AllExcept, typeof (QInputBox))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DisabledTextColor => this[nameof (DisabledTextColor)];

    [Description("Gets the first background color of the QContainerControl")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Category("Misc")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ContainerControlBackground1 => this[nameof (ContainerControlBackground1)];

    [Category("Misc")]
    [Description("Gets the second background color of the QContainerControl. This is only used when the BackgroundStyle is set to Gradient.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor ContainerControlBackground2 => this[nameof (ContainerControlBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the border color of the QContainerControl")]
    [Category("Misc")]
    public QColor ContainerControlBorder => this[nameof (ContainerControlBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCustomToolWindow), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the first backgroundColor of a QCustomToolWindow.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("CustomToolWindow")]
    public QColor CustomToolWindowBackground1 => this[nameof (CustomToolWindowBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCustomToolWindow), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Category("CustomToolWindow")]
    [Description("Gets the second backgroundColor of a QCustomToolWindow.")]
    public QColor CustomToolWindowBackground2 => this[nameof (CustomToolWindowBackground2)];

    [Description("Gets the first backgroundColor of a the Caption of a QCustomToolWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCustomToolWindow), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Category("CustomToolWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CustomToolWindowCaption1 => this[nameof (CustomToolWindowCaption1)];

    [Category("CustomToolWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCustomToolWindow), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the second backgroundColor of the Caption of a QCustomToolWindow.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CustomToolWindowCaption2 => this[nameof (CustomToolWindowCaption2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCustomToolWindow), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the border color a QCustomToolWindow.")]
    [Category("CustomToolWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CustomToolWindowBorder => this[nameof (CustomToolWindowBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QStatusBar))]
    [Description("Gets or sets the First background color of QStatusBar")]
    [Category("StatusBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor StatusBarBackground1 => this[nameof (StatusBarBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QStatusBar))]
    [Description("Gets or sets the First background color of QStatusBar")]
    [Category("StatusBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor StatusBarBackground2 => this[nameof (StatusBarBackground2)];

    [Category("StatusBar")]
    [Description("Gets the Border color of QStatusBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QStatusBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor StatusBarBorder => this[nameof (StatusBarBorder)];

    [Description("Gets or sets the color of the borders of the panels of QStatusBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QStatusBar))]
    [Category("StatusBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor StatusBarPanelBorder => this[nameof (StatusBarPanelBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QStatusBar))]
    [Description("Gets the color of the Background of the Panels of QStatusBar. If the QColorStyle is set to Gradient it defines the first color of that gradient")]
    [Category("StatusBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor StatusBarPanelBackground1 => this[nameof (StatusBarPanelBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("StatusBar")]
    [Description("If the Qios.DevSuite.Components.QColorStyle of a QStatusBar is set to Gradient this property defines the second color of that gradient. If the background is not set to Gradient this color is ignored.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QStatusBar))]
    public QColor StatusBarPanelBackground2 => this[nameof (StatusBarPanelBackground2)];

    [Description("Gets the dark color of the SizingGrip of a QStatusBar")]
    [Category("StatusBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QStatusBar))]
    public QColor StatusBarSizingGripDark => this[nameof (StatusBarSizingGripDark)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QStatusBar))]
    [Description("Gets the light color of the SizingGrip of a QStatusBar")]
    [Category("StatusBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor StatusBarSizingGripLight => this[nameof (StatusBarSizingGripLight)];

    [Description("Gets the first background color of QProgressBar")]
    [Category("ProgressBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QProgressBar))]
    public QColor ProgressBarBackground1 => this[nameof (ProgressBarBackground1)];

    [Category("ProgressBar")]
    [Description("Gets the Second background color of QProgressBar. This is only used when the StatusBar BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QProgressBar))]
    public QColor ProgressBarBackground2 => this[nameof (ProgressBarBackground2)];

    [Category("ProgressBar")]
    [Description("Gets the border color of QProgressBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QProgressBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ProgressBarBorder => this[nameof (ProgressBarBorder)];

    [Description("Gets the first color of the ProgressBar. This color is the regular color or the first color of the Gradient that might be used")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QProgressBar))]
    [Category("ProgressBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ProgressBarColor1 => this[nameof (ProgressBarColor1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QProgressBar))]
    [Description("Gets the second color of the ProgressBar. This color is only used when a Gradient is drawn")]
    [Category("ProgressBar")]
    public QColor ProgressBarColor2 => this[nameof (ProgressBarColor2)];

    [Category("ProgressBar")]
    [Description("Gets the fore color of the ProgressBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QProgressBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ProgressBarText => this[nameof (ProgressBarText)];

    [Description("Gets the first background color of QPanel")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QPanel))]
    [Category("Panel")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor PanelBackground1 => this[nameof (PanelBackground1)];

    [Category("Panel")]
    [Description("Gets the second background color of QPanel. This is only used when the Panel BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QPanel))]
    public QColor PanelBackground2 => this[nameof (PanelBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QPanel))]
    [Description("Gets the color of the borders of QPanel")]
    [Category("Panel")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor PanelBorder => this[nameof (PanelBorder)];

    [Category("Docking")]
    [Description("Gets the first background color of the buttons of QDockBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockBarButtonBackground1 => this[nameof (DockBarButtonBackground1)];

    [Description("Gets the second background color of the buttons of QDockBar. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockBar))]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockBarButtonBackground2 => this[nameof (DockBarButtonBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockBar))]
    [Description("Gets the color of the borders of the buttons of QDockBar")]
    [Category("Docking")]
    public QColor DockBarButtonBorder => this[nameof (DockBarButtonBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockBar))]
    [Description("Gets the first background color of QDockBar")]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockBarBackground1 => this[nameof (DockBarBackground1)];

    [Category("Docking")]
    [Description("Gets the second background color of QDockBar. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockBarBackground2 => this[nameof (DockBarBackground2)];

    [Description("Gets the color of the borders of QDockBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockBar))]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockBarBorder => this[nameof (DockBarBorder)];

    [Description("Gets the first background color of QDockingWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowBackground1 => this[nameof (DockingWindowBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Description("Gets the second background color of QDockingWindow. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowBackground2 => this[nameof (DockingWindowBackground2)];

    [Description("Gets the first color of the QDockingWindowTabStrip. This color is used by QDockingWindows that are shown in a tabbed way.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowTabStrip1 => this[nameof (DockingWindowTabStrip1)];

    [Description("Gets the second color of the QDockingWindowTabStrip. This color is used by QDockingWindows that are shown in a tabbed way.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Category("Docking")]
    public QColor DockingWindowTabStrip2 => this[nameof (DockingWindowTabStrip2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Description("Gets the first color of a tabbutton on a QDockingWindowTabStrip. This color is used by QDockingWindows that are shown in a tabbed way.")]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowTabButton1 => this[nameof (DockingWindowTabButton1)];

    [Category("Docking")]
    [Description("Gets the second color of a tabbutton on a QDockingWindowTabStrip. This color is used by QDockingWindows that are shown in a tabbed way.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowTabButton2 => this[nameof (DockingWindowTabButton2)];

    [Description("Gets the color of the border of tabbutton on a QDockingWindowTabStrip. This color is used by QDockingWindows that are shown in a tabbed way.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowTabButtonBorder => this[nameof (DockingWindowTabButtonBorder)];

    [Category("Docking")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Description("Gets the color of the border of tabbutton on a QDockingWindowTabStrip when is not active. This color is used by QDockingWindows that are shown in a tabbed way.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowTabButtonBorderNotActive => this[nameof (DockingWindowTabButtonBorderNotActive)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Description("Gets the color of text of tabbutton on a QDockingWindowTabStrip This color is used by QDockingWindows that are shown in a tabbed way")]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowTabButtonText => this[nameof (DockingWindowTabButtonText)];

    [Category("Docking")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Description("Gets the color of text of tabbutton on a QDockingWindowTabStrip when the button is not active. This color is used by QDockingWindows that are shown in a tabbed way.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockingWindowTabButtonTextNotActive => this[nameof (DockingWindowTabButtonTextNotActive)];

    [Category("Docking")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of QDockContainer. A QDockContainer is used to place one or more instances of QDockingWindow on.")]
    public QColor DockContainerBackground1 => this[nameof (DockContainerBackground1)];

    [Description("Gets the second background color of QDockContainer. This is only used when the BackgroundStyle is set to Gradient. A QDockContainer is used to place one or more instances of QDockingWindow on.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QDockingWindow))]
    [Category("Docking")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor DockContainerBackground2 => this[nameof (DockContainerBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the Border color of QMainMenu")]
    [Category("Menu")]
    public QColor MainMenuBorder => this[nameof (MainMenuBorder)];

    [Description("Gets the first background color of QMainMenu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuBackground1 => this[nameof (MainMenuBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of QMainMenu. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Menu")]
    public QColor MainMenuBackground2 => this[nameof (MainMenuBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Menu")]
    [Description("Gets the first bevel color of QMainMenu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    public QColor MainMenuBevel1 => this[nameof (MainMenuBevel1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the second bevel color of a QMainMenu. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuBevel2 => this[nameof (MainMenuBevel2)];

    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the first color of the MoreItemsArea of a QMainMenu")]
    public QColor MainMenuMoreItemsArea1 => this[nameof (MainMenuMoreItemsArea1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second color of the MoreItemsArea of a QMainMenu. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Category("Menu")]
    public QColor MainMenuMoreItemsArea2 => this[nameof (MainMenuMoreItemsArea2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the color of the ShadeLine of the MainMenu.")]
    [Category("Menu")]
    public QColor MainMenuShadeLine => this[nameof (MainMenuShadeLine)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Menu")]
    [Description("Gets the color of the SizingGrip of the MainMenu.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    public QColor MainMenuSizingGrip => this[nameof (MainMenuSizingGrip)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the shade color of the various elements of the MainMenu.")]
    public QColor MainMenuShade => this[nameof (MainMenuShade)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the Border color of the expanded MenuItem of a MainMenu")]
    [Category("Menu")]
    public QColor MainMenuExpandedItemBorder => this[nameof (MainMenuExpandedItemBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the first background color of the expanded MenuItem")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuExpandedItemBackground1 => this[nameof (MainMenuExpandedItemBackground1)];

    [Category("Menu")]
    [Description("Gets the second background color of the Expanded MenuItem.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuExpandedItemBackground2 => this[nameof (MainMenuExpandedItemBackground2)];

    [Description("Gets the Border color of the hot MenuItem on a QMainMenu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuHotItemBorder => this[nameof (MainMenuHotItemBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the first background color of the Hot MenuItem")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuHotItemBackground1 => this[nameof (MainMenuHotItemBackground1)];

    [Description("Gets the second background color of the hot MenuItem.")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    public QColor MainMenuHotItemBackground2 => this[nameof (MainMenuHotItemBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the Border color of the pressed MenuItem on a QMainMenu")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuPressedItemBorder => this[nameof (MainMenuPressedItemBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the first background color of the pressed MenuItem")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuPressedItemBackground1 => this[nameof (MainMenuPressedItemBackground1)];

    [Category("Menu")]
    [Description("Gets the second background color of the pressed MenuItem.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuPressedItemBackground2 => this[nameof (MainMenuPressedItemBackground2)];

    [Description("Gets color of the separator of a MainMenu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MainMenuSeparator => this[nameof (MainMenuSeparator)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the color of the texts on a MainMenu")]
    public QColor MainMenuText => this[nameof (MainMenuText)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the color of the texts on a MainMenu when the item is expanded or hot")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Menu")]
    public QColor MainMenuTextActive => this[nameof (MainMenuTextActive)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Menu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMainMenu))]
    [Description("Gets the color of the texts on a MainMenu when the item is disabled")]
    public QColor MainMenuTextDisabled => this[nameof (MainMenuTextDisabled)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [Description("Gets the color of the texts on a Menu")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MenuText => this[nameof (MenuText)];

    [Category("Menu")]
    [Description("Gets the color of the texts on a Menu when the item is expanded or hot")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuTextActive => this[nameof (MenuTextActive)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [Description("Gets the color of the texts on a Menu when the item is disabled")]
    [Category("Menu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuTextDisabled => this[nameof (MenuTextDisabled)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [Category("Menu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [Description("Gets the first background color of QMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MenuBackground1 => this[nameof (MenuBackground1)];

    [Category("Menu")]
    [Description("Gets the second background color of a QContextMenu. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuBackground2 => this[nameof (MenuBackground2)];

    [Description("Gets the color of the MenuShade")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuShade => this[nameof (MenuShade)];

    [Description("Gets the ForegroundColor of the DepersonalizeMenu Image. This is the Image that is shown when a menu has more items then visible.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuDepersonalizeImageForeground => this[nameof (MenuDepersonalizeImageForeground)];

    [Category("Menu")]
    [Description("Gets the BackColor of the DepersonalizeMenu Image. This is the Image that is shown when a menu has more items then visible.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuDepersonalizeImageBackground => this[nameof (MenuDepersonalizeImageBackground)];

    [Description("Gets the Border color of QContextMenu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuBorder => this[nameof (MenuBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [Description("Gets the Border color of the hot MenuItem")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MenuHotItemBorder => this[nameof (MenuHotItemBorder)];

    [Description("Gets the first background color of the hot MenuItem")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuHotItemBackground1 => this[nameof (MenuHotItemBackground1)];

    [Category("Menu")]
    [Description("Gets the second background color of the Hot MenuItem.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuHotItemBackground2 => this[nameof (MenuHotItemBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Menu")]
    [Description("Gets the Border color of the pressed MenuItem on a QMainMenu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuPressedItemBorder => this[nameof (MenuPressedItemBorder)];

    [Description("Gets the first background color of the pressed MenuItem")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuPressedItemBackground1 => this[nameof (MenuPressedItemBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [Description("Gets the second background color of the pressed MenuItem.")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuPressedItemBackground2 => this[nameof (MenuPressedItemBackground2)];

    [Description("Gets the Border color of the icon area when the item is checked")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuCheckedItemBorder => this[nameof (MenuCheckedItemBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Menu")]
    [Description("Gets the first background of the Icon area when the MenuItem is checked")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuCheckedItemBackground1 => this[nameof (MenuCheckedItemBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Menu")]
    [Description("Gets the second background color of the Hot MenuItem.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuCheckedItemBackground2 => this[nameof (MenuCheckedItemBackground2)];

    [Description("Gets the color of the separator of Menus")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuSeparator => this[nameof (MenuSeparator)];

    [Description("Gets the first background color of the IconBackground of a Menu")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuIconBackground1 => this[nameof (MenuIconBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    [Description("Gets the second background color of the Hot MenuItem.")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    public QColor MenuIconBackground2 => this[nameof (MenuIconBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [Description("Gets first background color of the IconBackground of the items that are not visible when personalized of a Menu")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuIconBackgroundDepersonalized1 => this[nameof (MenuIconBackgroundDepersonalized1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMenu))]
    [Description("Gets the second background color of the IconBackground of DepersonalizedItems of a Menu")]
    [Category("Menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu))]
    public QColor MenuIconBackgroundDepersonalized2 => this[nameof (MenuIconBackgroundDepersonalized2)];

    [Description("Gets the first background color of QBalloonWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    [Category("BalloonWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    public QColor BalloonWindowBackground1 => this[nameof (BalloonWindowBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of a QBalloonWindow. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("BalloonWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowBackground2 => this[nameof (BalloonWindowBackground2)];

    [Description("Gets the Border color of QBalloonWindow")]
    [Category("BalloonWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowBorder => this[nameof (BalloonWindowBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [Category("BalloonWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [Description("Gets the shade color of QBalloonWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowShade => this[nameof (BalloonWindowShade)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [Category("QBalloonWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [Description("Gets the Border color of the hot button on a QBalloonWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowHotButtonBorder => this[nameof (BalloonWindowHotButtonBorder)];

    [Description("Gets the first background color of the hot button on a QBalloonWindow")]
    [Category("QBalloonWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowHotButtonBackground1 => this[nameof (BalloonWindowHotButtonBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [Category("QBalloonWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [Description("Gets the second background color of the hot button on a QBalloonWindow.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowHotButtonBackground2 => this[nameof (BalloonWindowHotButtonBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [Category("QBalloonWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [Description("Gets the Border color of the pressed button on a QBalloonWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowPressedButtonBorder => this[nameof (BalloonWindowPressedButtonBorder)];

    [Description("Gets the first background color of the pressed button on a QBalloonWindow")]
    [Category("QBalloonWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowPressedButtonBackground1 => this[nameof (BalloonWindowPressedButtonBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QBalloonWindow")]
    [Description("Gets the second background color of the Pressed button on a QBalloonWindow.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor BalloonWindowPressedButtonBackground2 => this[nameof (BalloonWindowPressedButtonBackground2)];

    [Description("Gets the first background color of QShapedWindow")]
    [Category("ShapedWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowBackground1 => this[nameof (ShapedWindowBackground1)];

    [Category("ShapedWindow")]
    [Description("Gets the second background color of a QShapedWindow. This is only used when the BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowBackground2 => this[nameof (ShapedWindowBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ShapedWindow")]
    [Description("Gets the Border color of QShapedWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowBorder => this[nameof (ShapedWindowBorder)];

    [Description("Gets the shade color of QShapedWindow")]
    [Category("ShapedWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowShade => this[nameof (ShapedWindowShade)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the Border color of the hot button on a QShapedWindow")]
    [Category("QShapedWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowHotButtonBorder => this[nameof (ShapedWindowHotButtonBorder)];

    [Description("Gets the first background color of the hot button on a QShapedWindow")]
    [Category("QShapedWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowHotButtonBackground1 => this[nameof (ShapedWindowHotButtonBackground1)];

    [Description("Gets the second background color of the hot button on a QShapedWindow.")]
    [Category("QShapedWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowHotButtonBackground2 => this[nameof (ShapedWindowHotButtonBackground2)];

    [Description("Gets the Border color of the pressed button on a QShapedWindow")]
    [Category("QShapedWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowPressedButtonBorder => this[nameof (ShapedWindowPressedButtonBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [Category("QShapedWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of the pressed button on a QShapedWindow")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowPressedButtonBackground1 => this[nameof (ShapedWindowPressedButtonBackground1)];

    [Description("Gets the second background color of the Pressed button on a QShapedWindow.")]
    [Category("QShapedWindow")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QShapedWindowComponent))]
    public QColor ShapedWindowPressedButtonBackground2 => this[nameof (ShapedWindowPressedButtonBackground2)];

    [QColorCategoryVisible(QColorCategory.InputBox)]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the color of the text that is displayed as the textual cue")]
    public QColor TextCueColor => this[nameof (TextCueColor)];

    [Description("Gets the background color of the input area of the QInputBox")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxBackground => this[nameof (InputBoxBackground)];

    [Description("Gets the background color of the input area of the QInputBox when it is disabled.")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxDisabledBackground => this[nameof (InputBoxDisabledBackground)];

    [Description("Gets the background color of the input area of the QInputBox when it is hot.")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxHotBackground => this[nameof (InputBoxHotBackground)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the background color of the input area of the QInputBox when it is focused.")]
    [Category("InputBox")]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxFocusedBackground => this[nameof (InputBoxFocusedBackground)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("InputBox")]
    [Description("Gets the first outer background color of QInputBox")]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxOuterBackground1 => this[nameof (InputBoxOuterBackground1)];

    [Description("Gets the second outer background color of a QInputBox. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxOuterBackground2 => this[nameof (InputBoxOuterBackground2)];

    [Description("Gets the outer border color of QInputBox")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxOuterBorder => this[nameof (InputBoxOuterBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("InputBox")]
    [Description("Gets the first outer background color of QInputBox when it is disabled.")]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxDisabledOuterBackground1 => this[nameof (InputBoxDisabledOuterBackground1)];

    [Description("Gets the second outer background color of a QInputBox when it is disabled. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxDisabledOuterBackground2 => this[nameof (InputBoxDisabledOuterBackground2)];

    [Category("InputBox")]
    [Description("Gets the outer border color of QInputBox when it is disabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxDisabledOuterBorder => this[nameof (InputBoxDisabledOuterBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("InputBox")]
    [Description("Gets the first outer background color of QInputBox when the mouse pointer is over the control")]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxHotOuterBackground1 => this[nameof (InputBoxHotOuterBackground1)];

    [Description("Gets the second outer background color of a QInputBox when the mouse pointer is over the control. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxHotOuterBackground2 => this[nameof (InputBoxHotOuterBackground2)];

    [Category("InputBox")]
    [Description("Gets the outer border color of QInputBox when the mouse pointer is over the control")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    public QColor InputBoxHotOuterBorder => this[nameof (InputBoxHotOuterBorder)];

    [QColorCategoryVisible(QColorCategory.InputBox)]
    [Description("Gets the first outer background color of QInputBox when the control is focused and the mouse pointer is not over the control")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxFocusedOuterBackground1 => this[nameof (InputBoxFocusedOuterBackground1)];

    [Category("InputBox")]
    [Description("Gets the second outer background color of a QInputBox when the control is focused and the mouse pointer is not over the control. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxFocusedOuterBackground2 => this[nameof (InputBoxFocusedOuterBackground2)];

    [Description("Gets the outer border color of QInputBox when the control is focused and the mouse pointer is not over the control")]
    [QColorCategoryVisible(QColorCategory.InputBox)]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxFocusedOuterBorder => this[nameof (InputBoxFocusedOuterBorder)];

    [Description("Gets the first background color of the button of the QInputBox")]
    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxButtonBackground1 => this[nameof (InputBoxButtonBackground1)];

    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [Description("Gets the second background color of the button of the QInputBox")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxButtonBackground2 => this[nameof (InputBoxButtonBackground2)];

    [Category("InputBox")]
    [Description("Gets the Border color of the button of the QInputBox")]
    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxButtonBorder => this[nameof (InputBoxButtonBorder)];

    [Description("Gets the first background color of the button of the QInputBox when when it is disabled.")]
    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxDisabledButtonBackground1 => this[nameof (InputBoxDisabledButtonBackground1)];

    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [Description("Gets the second background color of the button of the QInputBox when it is disabled.")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxDisabledButtonBackground2 => this[nameof (InputBoxDisabledButtonBackground2)];

    [Description("Gets the Border color of the button of the QInputBox when the mouse pointer is over the button")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    public QColor InputBoxDisabledButtonBorder => this[nameof (InputBoxDisabledButtonBorder)];

    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [Description("Gets the first background color of the button of the QInputBox when the mouse pointer is over the button")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxHotButtonBackground1 => this[nameof (InputBoxHotButtonBackground1)];

    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [Description("Gets the second background color of the button of the QInputBox when the mouse pointer is over the button")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxHotButtonBackground2 => this[nameof (InputBoxHotButtonBackground2)];

    [Category("InputBox")]
    [Description("Gets the Border color of the button of the QInputBox when the mouse pointer is over the button")]
    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxHotButtonBorder => this[nameof (InputBoxHotButtonBorder)];

    [Description("Gets the first background color of the button of the QInputBox when the button is pressed")]
    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxPressedButtonBackground1 => this[nameof (InputBoxPressedButtonBackground1)];

    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    [Description("Gets the second background color of the button of the QInputBox when the button is pressed")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor InputBoxPressedButtonBackground2 => this[nameof (InputBoxPressedButtonBackground2)];

    [Description("Gets the Border color of the button of the QInputBox when the button is pressed")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.InputBoxButton)]
    public QColor InputBoxPressedButtonBorder => this[nameof (InputBoxPressedButtonBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ExplorerBar")]
    [Description("Gets the first background color of QExplorerBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarBackground1 => this[nameof (ExplorerBarBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ExplorerBar")]
    [Description("Gets the second background color of a QExplorerBar. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarBackground2 => this[nameof (ExplorerBarBackground2)];

    [Description("Gets the Border color of QExplorerBar")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarBorder => this[nameof (ExplorerBarBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [Description("Gets the ForegroundColor of the DepersonalizeMenu Image. This is the Image that is shown when a group item has more items then visible.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ExplorerBarDepersonalizeImageForeground => this[nameof (ExplorerBarDepersonalizeImageForeground)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ExplorerBar")]
    [Description("Gets the backgroundColor of the DepersonalizeMenu Image. This is the Image that is shown when a group item has more items then visible.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarDepersonalizeImageBackground => this[nameof (ExplorerBarDepersonalizeImageBackground)];

    [Description("Gets the text color of the various elements of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarText => this[nameof (ExplorerBarText)];

    [Description("Gets the text color of the hot item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarTextHot => this[nameof (ExplorerBarTextHot)];

    [Category("ExplorerBar")]
    [Description("Gets the text color of the pressed item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarTextPressed => this[nameof (ExplorerBarTextPressed)];

    [Description("Gets the text color of the expanded item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarTextExpanded => this[nameof (ExplorerBarTextExpanded)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the text color of the disabled items of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarTextDisabled => this[nameof (ExplorerBarTextDisabled)];

    [Description("Gets the first background color of the group item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarGroupItemBackground1 => this[nameof (ExplorerBarGroupItemBackground1)];

    [Description("Gets the second background color of the group item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarGroupItemBackground2 => this[nameof (ExplorerBarGroupItemBackground2)];

    [Description("Gets the border color of the group item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarGroupItemBorder => this[nameof (ExplorerBarGroupItemBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ExplorerBar")]
    [Description("Gets the first background color of the group panel of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarGroupPanelBackground1 => this[nameof (ExplorerBarGroupPanelBackground1)];

    [Description("Gets the second background color of the group panel of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarGroupPanelBackground2 => this[nameof (ExplorerBarGroupPanelBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ExplorerBar")]
    [Description("Gets the border color of the group panel of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarGroupPanelBorder => this[nameof (ExplorerBarGroupPanelBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ExplorerBar")]
    [Description("Gets the first background color of the expanded item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarExpandedItemBackground1 => this[nameof (ExplorerBarExpandedItemBackground1)];

    [Description("Gets the second background color of the expanded item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarExpandedItemBackground2 => this[nameof (ExplorerBarExpandedItemBackground2)];

    [Category("ExplorerBar")]
    [Description("Gets the border color of the expanded item of the ExplorerBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarExpandedItemBorder => this[nameof (ExplorerBarExpandedItemBorder)];

    [Description("Gets the first background color of the hot item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarHotItemBackground1 => this[nameof (ExplorerBarHotItemBackground1)];

    [Description("Gets the second background color of the hot item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarHotItemBackground2 => this[nameof (ExplorerBarHotItemBackground2)];

    [Description("Gets the border color of the hot item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarHotItemBorder => this[nameof (ExplorerBarHotItemBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Description("Gets the first background color of the pressed item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarPressedItemBackground1 => this[nameof (ExplorerBarPressedItemBackground1)];

    [Description("Gets the second background color of the pressed item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarPressedItemBackground2 => this[nameof (ExplorerBarPressedItemBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Description("Gets the border color of the pressed item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarPressedItemBorder => this[nameof (ExplorerBarPressedItemBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Description("Gets the color of separators of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarSeparator => this[nameof (ExplorerBarSeparator)];

    [Category("ExplorerBar")]
    [Description("Gets the first background color of a checked item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarCheckedItemBackground1 => this[nameof (ExplorerBarCheckedItemBackground1)];

    [Description("Gets the second background color of a checked item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarCheckedItemBackground2 => this[nameof (ExplorerBarCheckedItemBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [Description("Gets the border color of a checked item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ExplorerBarCheckedItemBorder => this[nameof (ExplorerBarCheckedItemBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Description("Gets the first background color of a checked group item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarCheckedGroupItemBackground1 => this[nameof (ExplorerBarCheckedGroupItemBackground1)];

    [Category("ExplorerBar")]
    [Description("Gets the second background color of a checked group item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarCheckedGroupItemBackground2 => this[nameof (ExplorerBarCheckedGroupItemBackground2)];

    [Description("Gets the border color of a checked group item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarCheckedGroupItemBorder => this[nameof (ExplorerBarCheckedGroupItemBorder)];

    [Category("ExplorerBar")]
    [Description("Gets the first background color of the expanded item of the ExplorerBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarExpandedGroupItemBackground1 => this[nameof (ExplorerBarExpandedGroupItemBackground1)];

    [Description("Gets the second background color of the expanded item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarExpandedGroupItemBackground2 => this[nameof (ExplorerBarExpandedGroupItemBackground2)];

    [Category("ExplorerBar")]
    [Description("Gets the border color of the expanded item of the ExplorerBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarExpandedGroupItemBorder => this[nameof (ExplorerBarExpandedGroupItemBorder)];

    [Category("ExplorerBar")]
    [Description("Gets the first background color of the hot item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarHotGroupItemBackground1 => this[nameof (ExplorerBarHotGroupItemBackground1)];

    [Description("Gets the second background color of the hot item of the ExplorerBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarHotGroupItemBackground2 => this[nameof (ExplorerBarHotGroupItemBackground2)];

    [Category("ExplorerBar")]
    [Description("Gets the border color of the hot item of the ExplorerBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarHotGroupItemBorder => this[nameof (ExplorerBarHotGroupItemBorder)];

    [Description("Gets the first background color of the pressed item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarPressedGroupItemBackground1 => this[nameof (ExplorerBarPressedGroupItemBackground1)];

    [Description("Gets the second background color of the pressed item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarPressedGroupItemBackground2 => this[nameof (ExplorerBarPressedGroupItemBackground2)];

    [Description("Gets the border color of the pressed item of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarPressedGroupItemBorder => this[nameof (ExplorerBarPressedGroupItemBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    [Description("Gets the color of the HasMoreChildItems image of the ExplorerBar.")]
    [Category("ExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    public QColor ExplorerBarHasMoreChildItemsColor => this[nameof (ExplorerBarHasMoreChildItemsColor)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ExplorerBar")]
    [Description("Gets the color of the shade of a group item.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerBar))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QExplorerItem))]
    public QColor ExplorerBarGroupItemShade => this[nameof (ExplorerBarGroupItemShade)];

    [Description("Gets the first background color of QToolBar")]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    public QColor ToolBarBackground1 => this[nameof (ToolBarBackground1)];

    [Description("Gets the second background color of a QToolBar. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarBackground2 => this[nameof (ToolBarBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the Border color of QToolBar")]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarBorder => this[nameof (ToolBarBorder)];

    [Category("ToolBar")]
    [Description("Gets the first bevel color of QToolBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarBevel1 => this[nameof (ToolBarBevel1)];

    [Description("Gets the second bevel color of a QToolBar. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarBevel2 => this[nameof (ToolBarBevel2)];

    [Category("ToolBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the first color of the MoreItemsArea of a QToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarMoreItemsArea1 => this[nameof (ToolBarMoreItemsArea1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the second color of the MoreItemsArea of a QToolBar. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarMoreItemsArea2 => this[nameof (ToolBarMoreItemsArea2)];

    [Category("ToolBar")]
    [Description("Gets the color of the ShadeLine of the ToolBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    public QColor ToolBarShadeLine => this[nameof (ToolBarShadeLine)];

    [Category("ToolBar")]
    [Description("Gets the color of the SizingGrip of the ToolBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarSizingGrip => this[nameof (ToolBarSizingGrip)];

    [Description("Gets the shade color of the various elements of the ToolBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarShade => this[nameof (ToolBarShade)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the text color of the various elements of the ToolBar.")]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarText => this[nameof (ToolBarText)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ToolBar")]
    [Description("Gets the text color of the active item of the ToolBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    public QColor ToolBarTextActive => this[nameof (ToolBarTextActive)];

    [Description("Gets the text color of the disabled items of the ToolBar.")]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    public QColor ToolBarTextDisabled => this[nameof (ToolBarTextDisabled)];

    [Category("ToolBar")]
    [Description("Gets the first background color of the expanded item of the ToolBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    public QColor ToolBarExpandedItemBackground1 => this[nameof (ToolBarExpandedItemBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the second background color of the expanded item of the ToolBar.")]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarExpandedItemBackground2 => this[nameof (ToolBarExpandedItemBackground2)];

    [Category("ToolBar")]
    [Description("Gets the border color of the expanded item of the ToolBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarExpandedItemBorder => this[nameof (ToolBarExpandedItemBorder)];

    [Description("Gets the first background color of the hot item of the ToolBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarHotItemBackground1 => this[nameof (ToolBarHotItemBackground1)];

    [Description("Gets the second background color of the hot item of the ToolBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarHotItemBackground2 => this[nameof (ToolBarHotItemBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the border color of the hot item of the ToolBar.")]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarHotItemBorder => this[nameof (ToolBarHotItemBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the first background color of the pressed item of the ToolBar.")]
    [Category("ToolBar")]
    public QColor ToolBarPressedItemBackground1 => this[nameof (ToolBarPressedItemBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the second background color of the pressed item of the ToolBar.")]
    [Category("ToolBar")]
    public QColor ToolBarPressedItemBackground2 => this[nameof (ToolBarPressedItemBackground2)];

    [Description("Gets the border color of the pressed item of the ToolBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBar")]
    public QColor ToolBarPressedItemBorder => this[nameof (ToolBarPressedItemBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the color of separators of the ToolBar.")]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarSeparator => this[nameof (ToolBarSeparator)];

    [Description("Gets the first background color of a checked item of the ToolBar.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarCheckedItemBackground1 => this[nameof (ToolBarCheckedItemBackground1)];

    [Description("Gets the second background color of a checked item of the ToolBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ToolBar")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    public QColor ToolBarCheckedItemBackground2 => this[nameof (ToolBarCheckedItemBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the border color of a checked item of the ToolBar.")]
    [Category("ToolBar")]
    public QColor ToolBarCheckedItemBorder => this[nameof (ToolBarCheckedItemBorder)];

    [Category("ToolBarForm")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the first backgroundColor of a QToolBarForm.")]
    public QColor ToolBarFormBackground1 => this[nameof (ToolBarFormBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Description("Gets the second backgroundColor of a QToolBarForm.")]
    [Category("ToolBarForm")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarFormBackground2 => this[nameof (ToolBarFormBackground2)];

    [Description("Gets the first backgroundColor of a the Caption of a QToolBarForm")]
    [Category("ToolBarForm")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    public QColor ToolBarFormCaption1 => this[nameof (ToolBarFormCaption1)];

    [Category("ToolBarForm")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second backgroundColor of the Caption of a QToolBarForm.")]
    public QColor ToolBarFormCaption2 => this[nameof (ToolBarFormCaption2)];

    [Description("Gets the border color a QToolBarForm.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBarForm")]
    public QColor ToolBarFormBorder => this[nameof (ToolBarFormBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the color that is used to replace the button masks with on a QToolBarForm.")]
    [Category("ToolBarForm")]
    public QColor ToolBarFormButton => this[nameof (ToolBarFormButton)];

    [Description("Gets the color that is used to replace the button masks with on a QToolBarForm when it is active.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBar))]
    [Category("ToolBarForm")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarFormButtonActive => this[nameof (ToolBarFormButtonActive)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBarHost))]
    [Description("Gets the first background color of QToolBarHost")]
    [Category("ToolBarHost")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarHostBackground1 => this[nameof (ToolBarHostBackground1)];

    [Category("ToolBarHost")]
    [Description("Gets the second background color of a QToolBarHost. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBarHost))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarHostBackground2 => this[nameof (ToolBarHostBackground2)];

    [Description("Gets the border color of a QToolBarHost")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QToolBarHost))]
    [Category("ToolBarHost")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ToolBarHostBorder => this[nameof (ToolBarHostBorder)];

    [Category("TabControl")]
    [Description("Gets the background color of the drop indicator of the QTabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabControlDropIndicatorBackground => this[nameof (TabControlDropIndicatorBackground)];

    [Description("Gets the border color of the drop indicator of the QTabControl")]
    [Category("TabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabControlDropIndicatorBorder => this[nameof (TabControlDropIndicatorBorder)];

    [Category("TabControl")]
    [Description("Gets the first background color of QTabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabControlBackground1 => this[nameof (TabControlBackground1)];

    [Category("TabControl")]
    [Description("Gets the second background color of a QTabControl. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor TabControlBackground2 => this[nameof (TabControlBackground2)];

    [Description("Gets the border color of a QTabControl")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Category("TabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor TabControlBorder => this[nameof (TabControlBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the first background color of the ContentShape of QTabControl")]
    [Category("TabControl")]
    public QColor TabControlContentBackground1 => this[nameof (TabControlContentBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the second background color of the ContentShape of QTabControl. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("TabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor TabControlContentBackground2 => this[nameof (TabControlContentBackground2)];

    [Category("TabControl")]
    [Description("Gets the border color of the ContentShape of QTabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabControlContentBorder => this[nameof (TabControlContentBorder)];

    [Category("TabControl")]
    [Description("Gets the shade color of the ContentShape of QTabControl")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor TabControlContentShade => this[nameof (TabControlContentShade)];

    [Description("Gets the first background color of a QTabStrip on QTabControl")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Category("TabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor TabStripBackground1 => this[nameof (TabStripBackground1)];

    [Description("Gets the second background color of a QTabStrip on QTabControl")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Category("TabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor TabStripBackground2 => this[nameof (TabStripBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the border color of a QTabStrip on QTabControl")]
    [Category("TabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor TabStripBorder => this[nameof (TabStripBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("TabControl")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the first background color of the navigation area of a QTabStrip on QTabControl")]
    public QColor TabStripNavigationAreaBackground1 => this[nameof (TabStripNavigationAreaBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the second background color of the navigation area of a QTabStrip on QTabControl")]
    [Category("TabControl")]
    public QColor TabStripNavigationAreaBackground2 => this[nameof (TabStripNavigationAreaBackground2)];

    [Category("TabControl")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the border color of the navigation area of a QTabStrip on QTabControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor TabStripNavigationAreaBorder => this[nameof (TabStripNavigationAreaBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the first background color of QTabPage")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabPageBackground1 => this[nameof (TabPageBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the second background color of a QTabPage. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabPageBackground2 => this[nameof (TabPageBackground2)];

    [Description("Gets the border color of a QTabControl")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabPageBorder => this[nameof (TabPageBorder)];

    [Description("Gets the first background color of a TabButton")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonBackground1 => this[nameof (TabButtonBackground1)];

    [Description("Gets the second background color of a TabButton. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonBackground2 => this[nameof (TabButtonBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("TabPage")]
    [Description("Gets the border color of a TabButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonBorder => this[nameof (TabButtonBorder)];

    [Description("Gets the text color of a TabButton")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonText => this[nameof (TabButtonText)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("TabPage")]
    [Description("Gets the text color of a disabled TabButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonTextDisabled => this[nameof (TabButtonTextDisabled)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("TabPage")]
    [Description("Gets the color of the shade of a QTabButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonShade => this[nameof (TabButtonShade)];

    [Description("Gets the first background color of an active TabButton")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonActiveBackground1 => this[nameof (TabButtonActiveBackground1)];

    [Description("Gets the second background color of an active TabButton. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonActiveBackground2 => this[nameof (TabButtonActiveBackground2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Description("Gets the border color of an active TabButton")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonActiveBorder => this[nameof (TabButtonActiveBorder)];

    [Category("TabPage")]
    [Description("Gets the text color of an active TabButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonActiveText => this[nameof (TabButtonActiveText)];

    [Description("Gets the first background color of a hot TabButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonHotBackground1 => this[nameof (TabButtonHotBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of a hot TabButton. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("TabPage")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonHotBackground2 => this[nameof (TabButtonHotBackground2)];

    [Description("Gets the border color of a hot TabButton")]
    [Category("TabPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonHotBorder => this[nameof (TabButtonHotBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the text color of a hot TabButton")]
    [Category("TabPage")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabControl), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QTabPage), QColorDesignVisibilityInheritanceType.ExternalOnly)]
    public QColor TabButtonHotText => this[nameof (TabButtonHotText)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a MarkupLabel")]
    [Category("MarkupLabel")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    public QColor MarkupLabelBackground1 => this[nameof (MarkupLabelBackground1)];

    [Category("MarkupLabel")]
    [Description("Gets the second background color of a MarkupLabel. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MarkupLabelBackground2 => this[nameof (MarkupLabelBackground2)];

    [Description("Gets the border color of a MarkupLabel")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [Category("MarkupLabel")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor MarkupLabelBorder => this[nameof (MarkupLabelBorder)];

    [QColorCategoryVisible(QColorCategory.MarkupText)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    [Description("Gets a Text color of MarkupText.")]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    public QColor MarkupText => this[nameof (MarkupText)];

    [QColorCategoryVisible(QColorCategory.MarkupText)]
    [Description("Gets the Anchor color of a MarkupText.")]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor MarkupTextAnchor => this[nameof (MarkupTextAnchor)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [Description("Gets the hot Anchor color of a MarkupText.")]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    [QColorCategoryVisible(QColorCategory.MarkupText)]
    public QColor MarkupTextAnchorHot => this[nameof (MarkupTextAnchorHot)];

    [Description("Gets the active Anchor color of a MarkupText.")]
    [QColorCategoryVisible(QColorCategory.MarkupText)]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor MarkupTextAnchorActive => this[nameof (MarkupTextAnchorActive)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    [Description("Gets the disabled Anchor color of a MarkupText.")]
    [Category("MarkupText")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    public QColor MarkupTextAnchorDisabled => this[nameof (MarkupTextAnchorDisabled)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    [Description("Gets a custom text color of a MarkupText")]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    public QColor MarkupTextCustom1 => this[nameof (MarkupTextCustom1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorCategoryVisible(QColorCategory.MarkupText)]
    [Description("Gets a custom text color of a MarkupText")]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor MarkupTextCustom2 => this[nameof (MarkupTextCustom2)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    [Description("Gets a custom text color of a MarkupText")]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorCategoryVisible(QColorCategory.MarkupText)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    public QColor MarkupTextCustom3 => this[nameof (MarkupTextCustom3)];

    [Description("Gets a custom text color of a MarkupText")]
    [QColorCategoryVisible(QColorCategory.MarkupText)]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    public QColor MarkupTextCustom4 => this[nameof (MarkupTextCustom4)];

    [QColorCategoryVisible(QColorCategory.MarkupText)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContextMenu), true)]
    [Description("Gets a custom text color of a MarkupText")]
    [Category("MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QMarkupLabel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QBalloonWindow))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QControl), true)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QContainerControl), true)]
    public QColor MarkupTextCustom5 => this[nameof (MarkupTextCustom5)];

    [Description("Gets the first background color of the scrollbar on a QComposite")]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollBarBackground1 => this[nameof (CompositeScrollBarBackground1)];

    [Category("Composite")]
    [Description("Gets the second background color of the scrollbar on a QComposite")]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollBarBackground2 => this[nameof (CompositeScrollBarBackground2)];

    [Description("Gets the border color of the scrollbar on a QComposite")]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollBarBorder => this[nameof (CompositeScrollBarBorder)];

    [Description("Gets the first background color of the pressed scrollbar on a QComposite")]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollBarPressedBackground1 => this[nameof (CompositeScrollBarPressedBackground1)];

    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Description("Gets the second background color of the pressed scrollbar on a QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollBarPressedBackground2 => this[nameof (CompositeScrollBarPressedBackground2)];

    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of the pressed scrollbar on a QComposite")]
    [Category("Composite")]
    public QColor CompositeScrollBarPressedBorder => this[nameof (CompositeScrollBarPressedBorder)];

    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of the scrollbutton on a QComposite")]
    public QColor CompositeScrollButtonBackground1 => this[nameof (CompositeScrollButtonBackground1)];

    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Description("Gets the second background color of the scrollbutton on a QComposite")]
    public QColor CompositeScrollButtonBackground2 => this[nameof (CompositeScrollButtonBackground2)];

    [Description("Gets the border color of the scrollbutton on a QComposite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Category("Composite")]
    public QColor CompositeScrollButtonBorder => this[nameof (CompositeScrollButtonBorder)];

    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Description("Gets the first background color of the disabled scrollbutton on a QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollButtonDisabledBackground1 => this[nameof (CompositeScrollButtonDisabledBackground1)];

    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Description("Gets the second background color of the disabled scrollbutton on a QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollButtonDisabledBackground2 => this[nameof (CompositeScrollButtonDisabledBackground2)];

    [Description("Gets the border color of the disabled scrollbutton on a QComposite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Category("Composite")]
    public QColor CompositeScrollButtonDisabledBorder => this[nameof (CompositeScrollButtonDisabledBorder)];

    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Description("Gets the first background color of a hot scrollbutton on a QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollButtonHotBackground1 => this[nameof (CompositeScrollButtonHotBackground1)];

    [Description("Gets the second background color of a hot scrollbutton on a QComposite")]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollButtonHotBackground2 => this[nameof (CompositeScrollButtonHotBackground2)];

    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Description("Gets the border color of a hot scrollbutton on a QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollButtonHotBorder => this[nameof (CompositeScrollButtonHotBorder)];

    [Category("Composite")]
    [Description("Gets the first background color of a pressed scrollbutton on a QComposite")]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollButtonPressedBackground1 => this[nameof (CompositeScrollButtonPressedBackground1)];

    [Description("Gets the second background color of a pressed scrollbutton on a QComposite")]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeScrollButtonPressedBackground2 => this[nameof (CompositeScrollButtonPressedBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of a pressed scrollbutton on a QComposite")]
    [Category("Composite")]
    [QColorCategoryVisible(QColorCategory.CompositeScroll)]
    public QColor CompositeScrollButtonPressedBorder => this[nameof (CompositeScrollButtonPressedBorder)];

    [Description("Gets the first background color of the balloon of a ToolTip of a QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeBalloon)]
    public QColor CompositeBalloonBackground1 => this[nameof (CompositeBalloonBackground1)];

    [Description("Gets the second background color of the balloon of a ToolTip of a QComposite. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeBalloon)]
    public QColor CompositeBalloonBackground2 => this[nameof (CompositeBalloonBackground2)];

    [Description("Gets the border color of a the balloon of a ToolTip of a QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeBalloon)]
    public QColor CompositeBalloonBorder => this[nameof (CompositeBalloonBorder)];

    [Category("Composite")]
    [Description("Gets the first background color of QComposite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Composite)]
    public QColor CompositeBackground1 => this[nameof (CompositeBackground1)];

    [Description("Gets the second background color of a QComposite. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Composite)]
    public QColor CompositeBackground2 => this[nameof (CompositeBackground2)];

    [Category("Composite")]
    [Description("Gets the border color of a QComposite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Composite)]
    public QColor CompositeBorder => this[nameof (CompositeBorder)];

    [Category("Composite")]
    [Description("Gets the first background color of the icon area of a QComposite that is configured as a menu.")]
    [QColorCategoryVisible(QColorCategory.Composite)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeIconBackground1 => this[nameof (CompositeIconBackground1)];

    [Description("Gets the second background color of the icon area of a QComposite that is configured as a menu.")]
    [QColorCategoryVisible(QColorCategory.Composite)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeIconBackground2 => this[nameof (CompositeIconBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Composite)]
    [Description("Gets the border color of the icon area of a QComposite that is configured as a menu.")]
    [Category("Composite")]
    public QColor CompositeIconBackgroundBorder => this[nameof (CompositeIconBackgroundBorder)];

    [QColorCategoryVisible(QColorCategory.CompositeButton)]
    [Description("Gets the first background color of QCompositeItem")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeButtonBackground1 => this[nameof (CompositeButtonBackground1)];

    [Category("Composite")]
    [Description("Gets the second background color of a QCompositeButton. This is only used when the BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeButton)]
    public QColor CompositeButtonBackground2 => this[nameof (CompositeButtonBackground2)];

    [Category("Composite")]
    [Description("Gets the border color of a QCompositeItem")]
    [QColorCategoryVisible(QColorCategory.CompositeButton)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeButtonBorder => this[nameof (CompositeButtonBorder)];

    [Description("Gets the first background color of QCompositeItem")]
    [QColorCategoryVisible(QColorCategory.CompositeButton)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeButtonDisabledBackground1 => this[nameof (CompositeButtonDisabledBackground1)];

    [Category("Composite")]
    [Description("Gets the second background color of a disabled QCompositeButton. This is only used when the BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeButton)]
    public QColor CompositeButtonDisabledBackground2 => this[nameof (CompositeButtonDisabledBackground2)];

    [Description("Gets the border color of a disabled QCompositeItem")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeButton)]
    public QColor CompositeButtonDisabledBorder => this[nameof (CompositeButtonDisabledBorder)];

    [QColorCategoryVisible(QColorCategory.CompositeGroup)]
    [Description("Gets the first background color of QCompositeGroup")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeGroupBackground1 => this[nameof (CompositeGroupBackground1)];

    [QColorCategoryVisible(QColorCategory.CompositeGroup)]
    [Description("Gets the second background color of a QCompositeGroup. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeGroupBackground2 => this[nameof (CompositeGroupBackground2)];

    [Category("Composite")]
    [Description("Gets the border color of a QCompositeGroup")]
    [QColorCategoryVisible(QColorCategory.CompositeGroup)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeGroupBorder => this[nameof (CompositeGroupBorder)];

    [Description("Gets the first background color of QCompositeItem")]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeItemBackground1 => this[nameof (CompositeItemBackground1)];

    [Category("Composite")]
    [Description("Gets the second background color of a QCompositeItem. This is only used when the BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemBackground2 => this[nameof (CompositeItemBackground2)];

    [Description("Gets the border color of a QCompositeItem")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemBorder => this[nameof (CompositeItemBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Composite")]
    [Description("Gets the first background color of a disabled QCompositeItem")]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemDisabledBackground1 => this[nameof (CompositeItemDisabledBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Composite")]
    [Description("Gets the second background color of a disabled QCompositeItem. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemDisabledBackground2 => this[nameof (CompositeItemDisabledBackground2)];

    [Description("Gets the border color of a disabled QCompositeItem")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemDisabledBorder => this[nameof (CompositeItemDisabledBorder)];

    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    [Description("Gets the first background color of QCompositeItem when it is expanded")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeItemExpandedBackground1 => this[nameof (CompositeItemExpandedBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Composite")]
    [Description("Gets the second background color of a QCompositeItem when it is expanded. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemExpandedBackground2 => this[nameof (CompositeItemExpandedBackground2)];

    [Description("Gets the border color of a QCompositeItem when it is expanded")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemExpandedBorder => this[nameof (CompositeItemExpandedBorder)];

    [Category("Composite")]
    [Description("Gets the first background color of QCompositeItem when it is hot")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemHotBackground1 => this[nameof (CompositeItemHotBackground1)];

    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    [Description("Gets the second background color of a QCompositeItem when it is hot. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeItemHotBackground2 => this[nameof (CompositeItemHotBackground2)];

    [Category("Composite")]
    [Description("Gets the border color of a QCompositeItem when it is hot")]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeItemHotBorder => this[nameof (CompositeItemHotBorder)];

    [Description("Gets the first background color of QCompositeItem when it is pressed")]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeItemPressedBackground1 => this[nameof (CompositeItemPressedBackground1)];

    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    [Description("Gets the second background color of a QCompositeItem when it is pressed. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor CompositeItemPressedBackground2 => this[nameof (CompositeItemPressedBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Composite")]
    [Description("Gets the border color of a QComposite when it is pressed")]
    [QColorCategoryVisible(QColorCategory.CompositeItem)]
    public QColor CompositeItemPressedBorder => this[nameof (CompositeItemPressedBorder)];

    [Description("Gets the text color of QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeText)]
    public QColor CompositeText => this[nameof (CompositeText)];

    [Description("Gets the text color of QComposite when it is expanded")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeText)]
    public QColor CompositeTextExpanded => this[nameof (CompositeTextExpanded)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Composite")]
    [Description("Gets the text color of QComposite when it is hot")]
    [QColorCategoryVisible(QColorCategory.CompositeText)]
    public QColor CompositeTextHot => this[nameof (CompositeTextHot)];

    [Description("Gets the text color of QComposite when it is pressed")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeText)]
    public QColor CompositeTextPressed => this[nameof (CompositeTextPressed)];

    [Description("Gets the disabled text color of QComposite")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeText)]
    public QColor CompositeTextDisabled => this[nameof (CompositeTextDisabled)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Composite")]
    [Description("Gets the first separator color of QCompositeSeparator")]
    [QColorCategoryVisible(QColorCategory.CompositeSeparator)]
    public QColor CompositeSeparator1 => this[nameof (CompositeSeparator1)];

    [Description("Gets the second separator color of QCompositeSeparator")]
    [Category("Composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.CompositeSeparator)]
    public QColor CompositeSeparator2 => this[nameof (CompositeSeparator2)];

    [Description("Gets the background color of the drop indicator of the QRibbon")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonDropIndicatorBackground => this[nameof (RibbonDropIndicatorBackground)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Ribbon")]
    [Description("Gets the border color of the drop indicator of the QRibbon")]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonDropIndicatorBorder => this[nameof (RibbonDropIndicatorBorder)];

    [Description("Gets the first background color of QRibbon")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonBackground1 => this[nameof (RibbonBackground1)];

    [Category("Ribbon")]
    [Description("Gets the second background color of a QRibbon. This is only used when the BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonBackground2 => this[nameof (RibbonBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Ribbon")]
    [Description("Gets the border color of a QRibbon")]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonBorder => this[nameof (RibbonBorder)];

    [Description("Gets the first background color of the ContentShape of QRibbon")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonContentBackground1 => this[nameof (RibbonContentBackground1)];

    [Description("Gets the second background color of the ContentShape of QRibbon. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonContentBackground2 => this[nameof (RibbonContentBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Ribbon")]
    [Description("Gets the border color of the ContentShape of QRibbon")]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonContentBorder => this[nameof (RibbonContentBorder)];

    [Description("Gets the first background color of a QTabStrip on QRIbbon")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonTabStripBackground1 => this[nameof (RibbonTabStripBackground1)];

    [Category("Ribbon")]
    [Description("Gets the second background color of a QTabStrip on QRibbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonTabStripBackground2 => this[nameof (RibbonTabStripBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Ribbon")]
    [Description("Gets the hade color of the ContentShape of QRibbon")]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonContentShade => this[nameof (RibbonContentShade)];

    [Description("Gets the border color of a QTabStrip on QRibbon")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonTabStripBorder => this[nameof (RibbonTabStripBorder)];

    [Category("Ribbon")]
    [Description("Gets the first background color of the navigation area of a QTabStrip on QRibbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonTabStripNavigationAreaBackground1 => this[nameof (RibbonTabStripNavigationAreaBackground1)];

    [QColorCategoryVisible(QColorCategory.Ribbon)]
    [Description("Gets the second background color of the navigation area of a QTabStrip on QRibbon")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabStripNavigationAreaBackground2 => this[nameof (RibbonTabStripNavigationAreaBackground2)];

    [Category("Ribbon")]
    [Description("Gets the border color of the navigation area of a QTabStrip on QRibbon")]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabStripNavigationAreaBorder => this[nameof (RibbonTabStripNavigationAreaBorder)];

    [Description("Gets the first background color of a hot button on the navigation area of a QTabStrip on QRibbon")]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabStripNavigationButtonHot1 => this[nameof (RibbonTabStripNavigationButtonHot1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    [Description("Gets the second background color of a hot button on the navigation area of a QTabStrip on QRibbon")]
    [Category("Ribbon")]
    public QColor RibbonTabStripNavigationButtonHot2 => this[nameof (RibbonTabStripNavigationButtonHot2)];

    [QColorCategoryVisible(QColorCategory.Ribbon)]
    [Description("Gets the border color of a hot button on the navigation area of a QTabStrip on QRibbon")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabStripNavigationButtonHotBorder => this[nameof (RibbonTabStripNavigationButtonHotBorder)];

    [Category("Ribbon")]
    [Description("Gets the first background color of a pressed button on the navigation area of a QTabStrip on QRibbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    public QColor RibbonTabStripNavigationButtonActive1 => this[nameof (RibbonTabStripNavigationButtonActive1)];

    [Category("Ribbon")]
    [Description("Gets the second background color of a pressed button on the navigation area of a QTabStrip on QRibbon")]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabStripNavigationButtonActive2 => this[nameof (RibbonTabStripNavigationButtonActive2)];

    [Description("Gets the border color of a pressed button on the navigation area of a QTabStrip on QRibbon")]
    [QColorCategoryVisible(QColorCategory.Ribbon)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabStripNavigationButtonActiveBorder => this[nameof (RibbonTabStripNavigationButtonActiveBorder)];

    [Category("RibbonPage")]
    [Description("Gets the first background color of QRibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonPageBackground1 => this[nameof (RibbonPageBackground1)];

    [Description("Gets the second background color of a QRibbonPage. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonPageBackground2 => this[nameof (RibbonPageBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [Description("Gets the border color of a QRibbonPage")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPageBorder => this[nameof (RibbonPageBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [Description("Gets the first background color of a RibbonTabButton")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabButtonBackground1 => this[nameof (RibbonTabButtonBackground1)];

    [Category("RibbonPage")]
    [Description("Gets the second background color of a RibbonTabButton. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabButtonBackground2 => this[nameof (RibbonTabButtonBackground2)];

    [Description("Gets the border color of a RibbonTabButton")]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabButtonBorder => this[nameof (RibbonTabButtonBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [Description("Gets the color of the mask of a button on a RibbonTabStrip")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabButtonMask => this[nameof (RibbonTabButtonMask)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonPage")]
    [Description("Gets the color of the mask of a disabled button on a RibbonTabStrip")]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonMaskDisabled => this[nameof (RibbonTabButtonMaskDisabled)];

    [Description("Gets the text color of a RibbonTabButton")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonText => this[nameof (RibbonTabButtonText)];

    [Description("Gets the text color of a disabled RibbonTabButton")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonTextDisabled => this[nameof (RibbonTabButtonTextDisabled)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonPage")]
    [Description("Gets the color of the shade of a RibbonTabButton.")]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonShade => this[nameof (RibbonTabButtonShade)];

    [Description("Gets the first background color of an active RibbonTabButton")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonActiveBackground1 => this[nameof (RibbonTabButtonActiveBackground1)];

    [Category("RibbonPage")]
    [Description("Gets the second background color of an active RibbonTabButton. This is only used when the BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonActiveBackground2 => this[nameof (RibbonTabButtonActiveBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonPage")]
    [Description("Gets the border color of an active RibbonTabButton")]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonActiveBorder => this[nameof (RibbonTabButtonActiveBorder)];

    [Description("Gets the text color of an active RibbonTabButton")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonActiveText => this[nameof (RibbonTabButtonActiveText)];

    [Category("RibbonPage")]
    [Description("Gets the first background color of a hot RibbonTabButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    public QColor RibbonTabButtonHotBackground1 => this[nameof (RibbonTabButtonHotBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [Description("Gets the second background color of a hot QRibbonTabButton. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("RibbonPage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabButtonHotBackground2 => this[nameof (RibbonTabButtonHotBackground2)];

    [Category("RibbonPage")]
    [Description("Gets the outline color of a hot QRibbonTabButton.")]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabButtonHotOutline => this[nameof (RibbonTabButtonHotOutline)];

    [Description("Gets the border color of a hot RibbonTabButton")]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonTabButtonHotBorder => this[nameof (RibbonTabButtonHotBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPage)]
    [Description("Gets the text color of a hot RibbonTabButton")]
    [Category("Ribbon")]
    public QColor RibbonTabButtonHotText => this[nameof (RibbonTabButtonHotText)];

    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Description("Gets the first background color of the ContentShape of QRibbonPanel")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelBackground1 => this[nameof (RibbonPanelBackground1)];

    [Category("Ribbon")]
    [Description("Gets the second background color of the shape of QRibbonPanel. This is only used when the BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    public QColor RibbonPanelBackground2 => this[nameof (RibbonPanelBackground2)];

    [Category("RibbonPanel")]
    [Description("Gets the border color of the shape of QRibbonPanel")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelBorder => this[nameof (RibbonPanelBorder)];

    [Description("Gets the first background color of the hot ContentShape of QRibbonPanel")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelHotBackground1 => this[nameof (RibbonPanelHotBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Description("Gets the second background color of the hot ContentShape of QRibbonPanel. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelHotBackground2 => this[nameof (RibbonPanelHotBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonPanel")]
    [Description("Gets the border color of the Hot shape of QRibbonPanel")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    public QColor RibbonPanelHotBorder => this[nameof (RibbonPanelHotBorder)];

    [Description("Gets the first background color of the active ContentShape of QRibbonPanel")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    public QColor RibbonPanelActiveBackground1 => this[nameof (RibbonPanelActiveBackground1)];

    [Category("Ribbon")]
    [Description("Gets the second background color of the active ContentShape of QRibbonPanel. This is only used when the BackgroundStyle is set to Gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    public QColor RibbonPanelActiveBackground2 => this[nameof (RibbonPanelActiveBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Description("Gets the border color of the active shape of QRibbonPanel")]
    [Category("RibbonPanel")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelActiveBorder => this[nameof (RibbonPanelActiveBorder)];

    [Category("Ribbon")]
    [Description("Gets the color of the show dialog button of the captionArea of QRibbonPanel.")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelCaptionShowDialog => this[nameof (RibbonPanelCaptionShowDialog)];

    [Description("Gets the color of the disabled show dialog button of the captionArea of QRibbonPanel.")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelCaptionShowDialogDisabled => this[nameof (RibbonPanelCaptionShowDialogDisabled)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Description("Gets the color of the hot show dialog button of the captionArea of QRibbonPanel.")]
    [Category("Ribbon")]
    public QColor RibbonPanelCaptionShowDialogHot => this[nameof (RibbonPanelCaptionShowDialogHot)];

    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Description("Gets the first background color of the captionArea of QRibbonPanel")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelCaptionArea1 => this[nameof (RibbonPanelCaptionArea1)];

    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Description("Gets the second background color of the captionArea of QRibbonPanel. This is only used when the BackgroundStyle is set to Gradient")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelCaptionArea2 => this[nameof (RibbonPanelCaptionArea2)];

    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Description("Gets the first background color of the hot captionArea of QRibbonPanel")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelHotCaptionArea1 => this[nameof (RibbonPanelHotCaptionArea1)];

    [Category("Ribbon")]
    [Description("Gets the second background color of the hot captionArea of QRibbonPanel. This is only used when the BackgroundStyle is set to Gradient")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelHotCaptionArea2 => this[nameof (RibbonPanelHotCaptionArea2)];

    [Description("Gets the text color of QRibbonPanel")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelText => this[nameof (RibbonPanelText)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Description("Gets the hot text color of QRibbonPanel")]
    [Category("Ribbon")]
    public QColor RibbonPanelTextHot => this[nameof (RibbonPanelTextHot)];

    [Category("Ribbon")]
    [Description("Gets the text color of QRibbonPanel")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelTextActive => this[nameof (RibbonPanelTextActive)];

    [Description("Gets the disabled text color of QRibbonPanel")]
    [QColorCategoryVisible(QColorCategory.RibbonPanel)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonPanelTextDisabled => this[nameof (RibbonPanelTextDisabled)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonInputBox)]
    [Description("Gets the background color of the input area of the QRibbonInputBox")]
    [Category("InputBox")]
    public QColor RibbonInputBoxBackground => this[nameof (RibbonInputBoxBackground)];

    [QColorCategoryVisible(QColorCategory.RibbonInputBox)]
    [Description("Gets the background color of the input area of the QInputBox")]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonInputBoxOuterBackground1 => this[nameof (RibbonInputBoxOuterBackground1)];

    [Description("Gets the background color of the input area of the QInputBox")]
    [QColorCategoryVisible(QColorCategory.RibbonInputBox)]
    [Category("InputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonInputBoxOuterBackground2 => this[nameof (RibbonInputBoxOuterBackground2)];

    [Description("Gets the first background color of a QRibbonItemBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonItemBar)]
    [Category("Ribbon")]
    public QColor RibbonItemBarBackground1 => this[nameof (RibbonItemBarBackground1)];

    [Description("Gets the second background color of a QRibbonItemBar.")]
    [Category("Ribbon")]
    [QColorCategoryVisible(QColorCategory.RibbonItemBar)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemBarBackground2 => this[nameof (RibbonItemBarBackground2)];

    [Description("Gets the border color of a QRibbonItemBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonItemBar)]
    [Category("Ribbon")]
    public QColor RibbonItemBarBorder => this[nameof (RibbonItemBarBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Description("Gets the first background color of a QRibbonItem located on a QRibbonItemBar.")]
    [Category("Ribbon")]
    public QColor RibbonItemBarItemBackground1 => this[nameof (RibbonItemBarItemBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of a QRibbonItem located on a QRibbonItemBar.")]
    [Category("Ribbon")]
    public QColor RibbonItemBarItemBackground2 => this[nameof (RibbonItemBarItemBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of a QRibbonItem located on a QRibbonItemBar.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    public QColor RibbonItemBarItemBorder => this[nameof (RibbonItemBarItemBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a disabled QRibbonItem located on a QRibbonItemBar.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    public QColor RibbonItemBarItemDisabledBackground1 => this[nameof (RibbonItemBarItemDisabledBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    [Description("Gets the second background color of a disabled QRibbonItem located on a QRibbonItemBar.")]
    public QColor RibbonItemBarItemDisabledBackground2 => this[nameof (RibbonItemBarItemDisabledBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of a disabled QRibbonItem located on a QRibbonItemBar.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    public QColor RibbonItemBarItemDisabledBorder => this[nameof (RibbonItemBarItemDisabledBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a QRibbonItem.")]
    [Category("Ribbon")]
    public QColor RibbonItemBackground1 => this[nameof (RibbonItemBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Ribbon")]
    [Description("Gets the second background color of a QRibbonItem.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    public QColor RibbonItemBackground2 => this[nameof (RibbonItemBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    [Description("Gets the border color of a QRibbonItem.")]
    public QColor RibbonItemBorder => this[nameof (RibbonItemBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a disabled QRibbonItem.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    public QColor RibbonItemDisabledBackground1 => this[nameof (RibbonItemDisabledBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    [Description("Gets the second background color of a QRibbonItem.")]
    public QColor RibbonItemDisabledBackground2 => this[nameof (RibbonItemDisabledBackground2)];

    [Description("Gets the border color of a disabled QRibbonItem.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemDisabledBorder => this[nameof (RibbonItemDisabledBorder)];

    [Category("Ribbon")]
    [Description("Gets the border color of a QRibbonItem.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemText => this[nameof (RibbonItemText)];

    [Description("Gets the first background color of a hot QRibbonItem.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemHotBackground1 => this[nameof (RibbonItemHotBackground1)];

    [Category("Ribbon")]
    [Description("Gets the second background color of a hot QRibbonItem.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    public QColor RibbonItemHotBackground2 => this[nameof (RibbonItemHotBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Description("Gets the border color of a hot QRibbonItem.")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemHotBorder => this[nameof (RibbonItemHotBorder)];

    [Category("Ribbon")]
    [Description("Gets the hot text color of a QRibbonItem.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemHotText => this[nameof (RibbonItemHotText)];

    [Description("Gets the first background color of an active QRibbonItem.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemActiveBackground1 => this[nameof (RibbonItemActiveBackground1)];

    [Description("Gets the second background color of a QRibbonItem.")]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemActiveBackground2 => this[nameof (RibbonItemActiveBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    [Description("Gets the border color of an active QRibbonItem.")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonItemActiveBorder => this[nameof (RibbonItemActiveBorder)];

    [Category("Ribbon")]
    [Description("Gets the text color of a n active QRibbonItem.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonItem)]
    public QColor RibbonItemActiveText => this[nameof (RibbonItemActiveText)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonSeparator)]
    [Description("Gets the first separator color for a QRibbonSeparator")]
    [Category("Ribbon")]
    public QColor RibbonSeparator1 => this[nameof (RibbonSeparator1)];

    [Description("Gets the first separator color for a QRibbonSeparator")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonSeparator)]
    [Category("Ribbon")]
    public QColor RibbonSeparator2 => this[nameof (RibbonSeparator2)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Category("RibbonCaption")]
    [Description("Gets the first background color of QRibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionBackground1 => this[nameof (RibbonCaptionBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonCaption")]
    [Description("Gets the second background color of QRibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    public QColor RibbonCaptionBackground2 => this[nameof (RibbonCaptionBackground2)];

    [Description("Gets the border color of QRibbonCaption")]
    [Category("RibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    public QColor RibbonCaptionBorder => this[nameof (RibbonCaptionBorder)];

    [Description("Gets the text color of the application part of the caption.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Category("RibbonCaption")]
    public QColor RibbonCaptionApplicationText => this[nameof (RibbonCaptionApplicationText)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the text color of the document part of the caption.")]
    [Category("RibbonCaption")]
    public QColor RibbonCaptionDocumentText => this[nameof (RibbonCaptionDocumentText)];

    [Description("Gets the first background color of an inactive QRibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Category("RibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionInactiveBackground1 => this[nameof (RibbonCaptionInactiveBackground1)];

    [Category("RibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of an inactive QRibbonCaption")]
    public QColor RibbonCaptionInactiveBackground2 => this[nameof (RibbonCaptionInactiveBackground2)];

    [Description("Gets the border color of an inactive QRibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Category("RibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionInactiveBorder => this[nameof (RibbonCaptionInactiveBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Description("Gets the text color of the application part of the caption.")]
    public QColor RibbonCaptionInactiveApplicationText => this[nameof (RibbonCaptionInactiveApplicationText)];

    [Description("Gets the text color of the document part of an inactive caption.")]
    [Category("RibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionInactiveDocumentText => this[nameof (RibbonCaptionInactiveDocumentText)];

    [Description("Gets the first background color of a button on the QRibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Category("RibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionButtonBackground1 => this[nameof (RibbonCaptionButtonBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Description("Gets the second background color of a button on the QRibbonCaption")]
    [Category("RibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionButtonBackground2 => this[nameof (RibbonCaptionButtonBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of a button on the QRibbonCaption")]
    [Category("RibbonCaption")]
    public QColor RibbonCaptionButtonBorder => this[nameof (RibbonCaptionButtonBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a button on the QRibbonCaption")]
    [Category("RibbonCaption")]
    public QColor RibbonCaptionButtonHotBackground1 => this[nameof (RibbonCaptionButtonHotBackground1)];

    [Category("RibbonCaption")]
    [Description("Gets the second background color of a button on the QRibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionButtonHotBackground2 => this[nameof (RibbonCaptionButtonHotBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonCaption")]
    [Description("Gets the border color of a button on the QRibbonCaption")]
    public QColor RibbonCaptionButtonHotBorder => this[nameof (RibbonCaptionButtonHotBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Description("Gets the first background color of a button on the QRibbonCaption")]
    [Category("RibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionButtonPressedBackground1 => this[nameof (RibbonCaptionButtonPressedBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of a button on the QRibbonCaption")]
    [Category("RibbonCaption")]
    public QColor RibbonCaptionButtonPressedBackground2 => this[nameof (RibbonCaptionButtonPressedBackground2)];

    [Category("RibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of a button on the QRibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    public QColor RibbonCaptionButtonPressedBorder => this[nameof (RibbonCaptionButtonPressedBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Description("Gets the color the mask of a button must be replaced with on a QRibbonCaption")]
    [Category("RibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionButtonMask => this[nameof (RibbonCaptionButtonMask)];

    [Category("RibbonCaption")]
    [QColorCategoryVisible(QColorCategory.RibbonCaption)]
    [Description("Gets the color the mask of a button must be replaced with on a QRibbonCaption")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonCaptionButtonMaskInactive => this[nameof (RibbonCaptionButtonMaskInactive)];

    [QColorCategoryVisible(QColorCategory.RibbonLaunchBar)]
    [Category("RibbonLaunchBar")]
    [Description("Gets the first background color of QRibbonLaunchBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonLaunchBarBackground1 => this[nameof (RibbonLaunchBarBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonLaunchBar)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonLaunchBar")]
    [Description("Gets the second background color of QRibbonLaunchBar")]
    public QColor RibbonLaunchBarBackground2 => this[nameof (RibbonLaunchBarBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonLaunchBar)]
    [Description("Gets the border color of QRibbonLaunchBar")]
    [Category("RibbonLaunchBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonLaunchBarBorder => this[nameof (RibbonLaunchBarBorder)];

    [Category("RibbonLaunchBar")]
    [Description("Gets the first background color an inactive of QRibbonLaunchBar")]
    [QColorCategoryVisible(QColorCategory.RibbonLaunchBar)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonLaunchBarInactiveBackground1 => this[nameof (RibbonLaunchBarInactiveBackground1)];

    [Description("Gets the second background color of an inactive QRibbonLaunchBar")]
    [Category("RibbonLaunchBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonLaunchBar)]
    public QColor RibbonLaunchBarInactiveBackground2 => this[nameof (RibbonLaunchBarInactiveBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonLaunchBar)]
    [Description("Gets the border color of an inactive QRibbonLaunchBar")]
    [Category("RibbonLaunchBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonLaunchBarInactiveBorder => this[nameof (RibbonLaunchBarInactiveBorder)];

    [Category("RibbonLaunchBar")]
    [QColorCategoryVisible(QColorCategory.RibbonLaunchBarHost)]
    [Description("Gets the first background color of QRibbonLaunchBarHost")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonLaunchBarHostBackground1 => this[nameof (RibbonLaunchBarHostBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonLaunchBarHost)]
    [Category("RibbonLaunchBar")]
    [Description("Gets the second background color of QRibbonLaunchBarHost")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonLaunchBarHostBackground2 => this[nameof (RibbonLaunchBarHostBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonLaunchBarHost)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonLaunchBar")]
    [Description("Gets the border color of QRibbonLaunchBarHost")]
    public QColor RibbonLaunchBarHostBorder => this[nameof (RibbonLaunchBarHostBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [Description("Gets the border color of QRibbonApplicationButton")]
    [Category("RibbonApplicationButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonApplicationButtonBorder => this[nameof (RibbonApplicationButtonBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the glow color of QRibbonApplicationButton")]
    [Category("RibbonApplicationButton")]
    public QColor RibbonApplicationButtonGlow => this[nameof (RibbonApplicationButtonGlow)];

    [Category("RibbonApplicationButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of QRibbonApplicationButton")]
    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    public QColor RibbonApplicationButtonBackground1 => this[nameof (RibbonApplicationButtonBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [Description("Gets the second background color of QRibbonApplicationButton")]
    [Category("RibbonApplicationButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonApplicationButtonBackground2 => this[nameof (RibbonApplicationButtonBackground2)];

    [Category("RibbonApplicationButton")]
    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [Description("Gets the border color of a hot QRibbonApplicationButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonApplicationButtonHotBorder => this[nameof (RibbonApplicationButtonHotBorder)];

    [Category("RibbonApplicationButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [Description("Gets the glow color of a hot QRibbonApplicationButton")]
    public QColor RibbonApplicationButtonHotGlow => this[nameof (RibbonApplicationButtonHotGlow)];

    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a hot QRibbonApplicationButton")]
    [Category("RibbonApplicationButton")]
    public QColor RibbonApplicationButtonHotBackground1 => this[nameof (RibbonApplicationButtonHotBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of a hot QRibbonApplicationButton")]
    [Category("RibbonApplicationButton")]
    public QColor RibbonApplicationButtonHotBackground2 => this[nameof (RibbonApplicationButtonHotBackground2)];

    [Category("RibbonApplicationButton")]
    [Description("Gets the border color of a pressed QRibbonApplicationButton")]
    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonApplicationButtonPressedBorder => this[nameof (RibbonApplicationButtonPressedBorder)];

    [Description("Gets the glow color of a pressed QRibbonApplicationButton")]
    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [Category("RibbonApplicationButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonApplicationButtonPressedGlow => this[nameof (RibbonApplicationButtonPressedGlow)];

    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    [Description("Gets the first background color of a pressed QRibbonApplicationButton")]
    [Category("RibbonApplicationButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonApplicationButtonPressedBackground1 => this[nameof (RibbonApplicationButtonPressedBackground1)];

    [Description("Gets the second background color of a pressed QRibbonApplicationButton")]
    [Category("RibbonApplicationButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonApplicationButton)]
    public QColor RibbonApplicationButtonPressedBackground2 => this[nameof (RibbonApplicationButtonPressedBackground2)];

    [Category("RibbonForm")]
    [Description("Gets the first background color of QRibbonForm")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonForm)]
    public QColor RibbonFormBackground1 => this[nameof (RibbonFormBackground1)];

    [Category("RibbonForm")]
    [Description("Gets the second background color of QRibbonForm")]
    [QColorCategoryVisible(QColorCategory.RibbonForm)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonFormBackground2 => this[nameof (RibbonFormBackground2)];

    [Description("Gets the border color of QRibbonForm")]
    [QColorCategoryVisible(QColorCategory.RibbonForm)]
    [Category("RibbonForm")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonFormBorder => this[nameof (RibbonFormBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonForm)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of an inactive QRibbonForm")]
    [Category("RibbonForm")]
    public QColor RibbonFormInactiveBackground1 => this[nameof (RibbonFormInactiveBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonForm)]
    [Description("Gets the second background color of an inactive QRibbonForm")]
    [Category("RibbonForm")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonFormInactiveBackground2 => this[nameof (RibbonFormInactiveBackground2)];

    [Category("RibbonForm")]
    [Description("Gets the border color of an inactive QRibbonForm")]
    [QColorCategoryVisible(QColorCategory.RibbonForm)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonFormInactiveBorder => this[nameof (RibbonFormInactiveBorder)];

    [Description("Gets the first background color of a QRibbonMenu")]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [Category("RibbonMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonMenuBackground1 => this[nameof (RibbonMenuBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second color of a QRibbonMenu")]
    [Category("RibbonMenu")]
    public QColor RibbonMenuBackground2 => this[nameof (RibbonMenuBackground2)];

    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [Description("Gets the border color of a QRibbonMenu")]
    [Category("RibbonMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonMenuBorder => this[nameof (RibbonMenuBorder)];

    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of the item area on a QRibbonMenu")]
    [Category("RibbonMenu")]
    public QColor RibbonMenuContentAreaBackground1 => this[nameof (RibbonMenuContentAreaBackground1)];

    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of the item area on a QRibbonMenu")]
    [Category("RibbonMenu")]
    public QColor RibbonMenuContentAreaBackground2 => this[nameof (RibbonMenuContentAreaBackground2)];

    [Category("RibbonMenu")]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first border color of the item area on a QRibbonMenu")]
    public QColor RibbonMenuContentAreaBorder => this[nameof (RibbonMenuContentAreaBorder)];

    [Description("Gets the first background color of the header and footer of a QRibbonMenu")]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [Category("RibbonMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor RibbonMenuHeaderFooter1 => this[nameof (RibbonMenuHeaderFooter1)];

    [Category("RibbonMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [Description("Gets the first background color of the header and footer of a QRibbonMenu")]
    public QColor RibbonMenuHeaderFooter2 => this[nameof (RibbonMenuHeaderFooter2)];

    [Description("Gets the border color of the header and footer of a QRibbonMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [Category("RibbonMenu")]
    public QColor RibbonMenuHeaderFooterBorder => this[nameof (RibbonMenuHeaderFooterBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a caption on a QRibbonMenu")]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    [Category("RibbonMenu")]
    public QColor RibbonMenuCaptionBackground1 => this[nameof (RibbonMenuCaptionBackground1)];

    [Category("RibbonMenu")]
    [Description("Gets the second background color of a caption on a QRibbonMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    public QColor RibbonMenuCaptionBackground2 => this[nameof (RibbonMenuCaptionBackground2)];

    [Description("Gets the border color of a caption on a QRibbonMenu")]
    [Category("RibbonMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    public QColor RibbonMenuCaptionBorder => this[nameof (RibbonMenuCaptionBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonMenu")]
    [Description("Gets the first background color of the document area on a QRibbonMenu")]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    public QColor RibbonMenuDocumentAreaBackground1 => this[nameof (RibbonMenuDocumentAreaBackground1)];

    [Description("Gets the second background color of the document area on a QRibbonMenu")]
    [Category("RibbonMenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    public QColor RibbonMenuDocumentAreaBackground2 => this[nameof (RibbonMenuDocumentAreaBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("RibbonMenu")]
    [Description("Gets the border of the document area on a QRibbonMenu")]
    [QColorCategoryVisible(QColorCategory.RibbonMenuWindow)]
    public QColor RibbonMenuDocumentAreaBorder => this[nameof (RibbonMenuDocumentAreaBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Ribbon")]
    [Description("Gets the first background color of the hotkey window of a Ribbon")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QRibbon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QRibbonPanel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QComposite))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCompositeControl))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCompositeWindow))]
    public QColor HotkeyWindowBackground1 => this[nameof (HotkeyWindowBackground1)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QRibbonPanel))]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QRibbon))]
    [Description("Gets the second background color of the hotkey window of a Ribbon")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QComposite))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCompositeControl))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCompositeWindow))]
    public QColor HotkeyWindowBackground2 => this[nameof (HotkeyWindowBackground2)];

    [Description("Gets the second border color of the hotkey.")]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QRibbon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QRibbonPanel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QComposite))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCompositeControl))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCompositeWindow))]
    public QColor HotkeyWindowBorder => this[nameof (HotkeyWindowBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCompositeControl))]
    [Category("Ribbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QRibbon))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QRibbonPanel))]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QComposite))]
    [Description("Gets the text color of the hotkey window.")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QCompositeWindow))]
    public QColor HotkeyWindowText => this[nameof (HotkeyWindowText)];

    [Description("Gets the first background color of the scrollbar")]
    [Category("ScrollBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    public QColor ScrollBarBackground1 => this[nameof (ScrollBarBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ScrollBar")]
    [Description("Gets the second background color of the scrollbar")]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    public QColor ScrollBarBackground2 => this[nameof (ScrollBarBackground2)];

    [Description("Gets the second border color of the scrollbar.")]
    [Category("ScrollBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    public QColor ScrollBarBorder => this[nameof (ScrollBarBorder)];

    [Description("Gets the first background color of the pressed scrollbar")]
    [Category("ScrollBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    public QColor ScrollBarPressedBackground1 => this[nameof (ScrollBarPressedBackground1)];

    [Description("Gets the second background color of the pressed scrollbar")]
    [Category("ScrollBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    public QColor ScrollBarPressedBackground2 => this[nameof (ScrollBarPressedBackground2)];

    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of the pressed scrollbar")]
    [Category("ScrollBar")]
    public QColor ScrollBarPressedBorder => this[nameof (ScrollBarPressedBorder)];

    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of the scrollbutton")]
    [Category("ScrollBar")]
    public QColor ScrollBarButtonBackground1 => this[nameof (ScrollBarButtonBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Category("ScrollBar")]
    [Description("Gets the second background color of the scrollbutton")]
    public QColor ScrollBarButtonBackground2 => this[nameof (ScrollBarButtonBackground2)];

    [Category("ScrollBar")]
    [Description("Gets the border color of the scrollbutton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    public QColor ScrollBarButtonBorder => this[nameof (ScrollBarButtonBorder)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Category("ScrollBar")]
    [Description("Gets the first background color of the disabled scrollbutton")]
    public QColor ScrollBarButtonDisabledBackground1 => this[nameof (ScrollBarButtonDisabledBackground1)];

    [Description("Gets the second background color of the disabled scrollbutton")]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Category("ScrollBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ScrollBarButtonDisabledBackground2 => this[nameof (ScrollBarButtonDisabledBackground2)];

    [Category("ScrollBar")]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Description("Gets the border color of the disabled scrollbutton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ScrollBarButtonDisabledBorder => this[nameof (ScrollBarButtonDisabledBorder)];

    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Description("Gets the first background color of a hot scrollbutton")]
    [Category("ScrollBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ScrollBarButtonHotBackground1 => this[nameof (ScrollBarButtonHotBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("ScrollBar")]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Description("Gets the second background color of a hot scrollbutton")]
    public QColor ScrollBarButtonHotBackground2 => this[nameof (ScrollBarButtonHotBackground2)];

    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Category("ScrollBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of a hot scrollbutton")]
    public QColor ScrollBarButtonHotBorder => this[nameof (ScrollBarButtonHotBorder)];

    [Category("ScrollBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a pressed scrollbutton")]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    public QColor ScrollBarButtonPressedBackground1 => this[nameof (ScrollBarButtonPressedBackground1)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the second background color of a pressed scrollbutton")]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Category("ScrollBar")]
    public QColor ScrollBarButtonPressedBackground2 => this[nameof (ScrollBarButtonPressedBackground2)];

    [Category("ScrollBar")]
    [QColorCategoryVisible(QColorCategory.ScrollBar)]
    [Description("Gets the border color of a pressed scrollbutton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ScrollBarButtonPressedBorder => this[nameof (ScrollBarButtonPressedBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Description("Gets the first background color of a QButton")]
    [Category("Button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonBackground1 => this[nameof (ButtonBackground1)];

    [Category("Button")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Description("Gets the second background color of a QButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonBackground2 => this[nameof (ButtonBackground2)];

    [Category("Button")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the border color of a QButton")]
    public QColor ButtonBorder => this[nameof (ButtonBorder)];

    [Description("Gets the text color of a QButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Category("Button")]
    public QColor ButtonText => this[nameof (ButtonText)];

    [Category("Button")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the first background color of a hot QButton")]
    public QColor ButtonHotBackground1 => this[nameof (ButtonHotBackground1)];

    [Description("Gets the second background color of a hot QButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Category("Button")]
    public QColor ButtonHotBackground2 => this[nameof (ButtonHotBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Description("Gets the border color of a hot QButton")]
    [Category("Button")]
    public QColor ButtonHotBorder => this[nameof (ButtonHotBorder)];

    [Description("Gets the text color of hot a QButton")]
    [Category("Button")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonHotText => this[nameof (ButtonHotText)];

    [Category("Button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Description("Gets the first background color of a pressed QButton")]
    public QColor ButtonPressedBackground1 => this[nameof (ButtonPressedBackground1)];

    [Category("Button")]
    [Description("Gets the second background color of a pressed QButton")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    public QColor ButtonPressedBackground2 => this[nameof (ButtonPressedBackground2)];

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Button")]
    [Description("Gets the border color of a pressed QButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    public QColor ButtonPressedBorder => this[nameof (ButtonPressedBorder)];

    [Description("Gets the text color of a pressed QButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Category("Button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonPressedText => this[nameof (ButtonPressedText)];

    [Category("Button")]
    [Description("Gets the first background color of a disabled QButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonDisabledBackground1 => this[nameof (ButtonDisabledBackground1)];

    [Description("Gets the second background color of a disabled QButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Category("Button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonDisabledBackground2 => this[nameof (ButtonDisabledBackground2)];

    [Description("Gets the border color of a disabled QButton")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Category("Button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonDisabledBorder => this[nameof (ButtonDisabledBorder)];

    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Description("Gets the text color of a disabled QButton")]
    [Category("Button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonDisabledText => this[nameof (ButtonDisabledText)];

    [Description("Gets the color used for the InnerGlow when the button has the focus or is the default button")]
    [QColorDesignVisible(QColorDesignVisibilityType.NoneExcept, typeof (QButton))]
    [Category("Button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QColor ButtonFocusedInnerGlow => this[nameof (ButtonFocusedInnerGlow)];

    [Browsable(false)]
    public static QGlobalColorScheme Global => QColorScheme.m_oGlobal;

    private void AddDefaultColors()
    {
      this.AddMiscColors();
      this.AddInputBoxColors();
      this.AddCustomToolWindowColors();
      this.AddStatusBarColors();
      this.AddProgressBarColors();
      this.AddPanelColors();
      this.AddDockingWindowColors();
      this.AddMenuColors();
      this.AddExplorerBarColors();
      this.AddBalloonWindowColors();
      this.AddShapedWindowColors();
      this.AddToolBarColors();
      this.AddToolBarHostColors();
      this.AddTabControlColors();
      this.AddMarkupLabelColors();
      this.AddMarkupTextColors();
      this.AddCompositeColors();
      this.AddRibbonColors();
      this.AddRibbonCaptionColors();
      this.AddRibbonLaunchBarColors();
      this.AddRibbonApplicationButtonColors();
      this.AddRibbonFormColors();
      this.AddRibbonMenuColors();
      this.AddRibbonPanelColors();
      this.AddHotkeyWindowColors();
      this.AddScrollBarColors();
      this.AddButtonColors();
    }

    protected override QColorSchemeBase CreateInstanceForClone() => (QColorSchemeBase) new QColorScheme(false, true, this.IsRegistered);

    public object Clone() => (object) this.CloneColorScheme();
  }
}
