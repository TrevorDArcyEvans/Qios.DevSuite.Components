// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCompositeDesignerHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Ribbon;
using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Design
{
  public class QCompositeDesignerHelper
  {
    public static readonly Type[] DefaultCompositeCreationTypes = new Type[13]
    {
      typeof (QCompositeMenuItem),
      typeof (QCompositeLargeMenuItem),
      typeof (QCompositeButton),
      typeof (QCompositeItem),
      typeof (QCompositeGroup),
      typeof (QCompositeText),
      typeof (QCompositeMarkupText),
      typeof (QCompositeIcon),
      typeof (QCompositeImage),
      typeof (QCompositeItemInputBox),
      typeof (QCompositeItemControl),
      typeof (QCompositeSeparator),
      typeof (QCompositeResizeItem)
    };
    public static readonly Type[] DefaultRibbonPageCreationTypes = new Type[1]
    {
      typeof (QRibbonPanel)
    };
    public static readonly Type[] DefaultRibbonGroupCreationTypes = new Type[5]
    {
      typeof (QRibbonItem),
      typeof (QRibbonItemGroup),
      typeof (QRibbonItemBar),
      typeof (QRibbonInputBoxItem),
      typeof (QRibbonSeparator)
    };

    private QCompositeDesignerHelper()
    {
    }

    public static bool IgnoreAddingComponent(IComponent component) => component is QCompositeItemTemplate;
  }
}
