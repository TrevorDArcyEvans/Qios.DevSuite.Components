// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QColorStringEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Security.Permissions;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  internal class QColorStringEditor : UITypeEditor
  {
    private UITypeEditor m_oColorEditor;
    private TypeConverter m_oColorConverter;

    public QColorStringEditor()
    {
      this.m_oColorEditor = (UITypeEditor) TypeDescriptor.GetEditor(typeof (Color), typeof (UITypeEditor));
      this.m_oColorConverter = TypeDescriptor.GetConverter(typeof (Color));
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      if (context == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (context)));
      string text = value as string;
      Color color1 = Color.Empty;
      try
      {
        color1 = (Color) this.m_oColorConverter.ConvertFromInvariantString(text);
      }
      catch
      {
      }
      Color color2 = (Color) QMisc.AsValueType(this.m_oColorEditor.EditValue(provider, (object) color1));
      if (!(color1 != color2))
        return (object) text;
      context.OnComponentChanging();
      context.OnComponentChanged();
      return (object) this.m_oColorConverter.ConvertToInvariantString((object) color2);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override bool GetPaintValueSupported(ITypeDescriptorContext context) => this.m_oColorEditor.GetPaintValueSupported(context);

    private Color GetColorSchemeColor(string value, ITypeDescriptorContext context)
    {
      PropertyInfo property = context.Instance.GetType().GetProperty("ColorScheme", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      return property != null && property.GetValue(context.Instance, (object[]) null) is QColorScheme qcolorScheme && qcolorScheme.IsValidColor(value) ? (Color) qcolorScheme.GetColor(value) : Color.Empty;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void PaintValue(PaintValueEventArgs e)
    {
      if (e == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (e)));
      Color color = Color.Empty;
      string text = e.Value != null ? e.Value.ToString() : (string) null;
      if (!QMisc.IsEmpty((object) text))
      {
        color = this.GetColorSchemeColor(text, e.Context);
        if (color == Color.Empty)
        {
          try
          {
            color = (Color) this.m_oColorConverter.ConvertFromInvariantString(text);
          }
          catch
          {
          }
        }
      }
      this.m_oColorEditor.PaintValue(new PaintValueEventArgs(e.Context, (object) color, e.Graphics, e.Bounds));
    }
  }
}
