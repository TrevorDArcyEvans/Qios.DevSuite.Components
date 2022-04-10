// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QGlobalColorSchemeManager
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(true)]
  [Designer(typeof (QGlobalColorSchemeManagerDesigner), typeof (IDesigner))]
  [ToolboxBitmap(typeof (QGlobalColorSchemeManager), "Resources.ControlImages.QGlobalColorSchemeManager.bmp")]
  public class QGlobalColorSchemeManager : Component
  {
    public bool ShouldSerializeGlobal() => this.Global.ShouldSerialize();

    public void ResetGlobal() => this.Global.Reset();

    [Description("Represents the static QColorScheme.Global, editting this value has influance on the complete application")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QBehavior")]
    public QGlobalColorScheme Global => QColorScheme.Global != null ? QColorScheme.Global : (QGlobalColorScheme) null;

    public QGlobalColorSchemeManager(IContainer container)
    {
      if (container == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (container)));
      container.Add((IComponent) this);
    }

    public QGlobalColorSchemeManager()
    {
    }
  }
}
