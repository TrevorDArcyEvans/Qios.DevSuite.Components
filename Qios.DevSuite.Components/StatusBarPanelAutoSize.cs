// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QStatusBarPanel
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components;

// stolen from:
//    https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.statusbarpanelautosize?view=netframework-4.8&viewFallbackFrom=net-6.0
public enum StatusBarPanelAutoSize
{
  /// <summary>
  /// The StatusBarPanel does not change size when the StatusBar control resizes.
  /// </summary>
  None = 1,

  /// <summary>
  /// The StatusBarPanel shares the available space on the StatusBar
  /// (the space not taken up by other panels whose AutoSize property
  /// is set to None or Contents) with other panels that have their
  /// AutoSize property set to Spring.
  /// </summary>
  Spring = 2,

  /// <summary>
  /// The width of the StatusBarPanel is determined by its contents.
  /// </summary>
  Contents = 3
}
