// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QDesignerMainTextAttribute
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Design
{
  [AttributeUsage(AttributeTargets.Property)]
  public class QDesignerMainTextAttribute : Attribute
  {
    private bool m_bIsMainText;
    public static readonly QDesignerMainTextAttribute False = new QDesignerMainTextAttribute(false);
    public static readonly QDesignerMainTextAttribute True = new QDesignerMainTextAttribute(true);
    public static readonly QDesignerMainTextAttribute Default = QDesignerMainTextAttribute.False;

    public QDesignerMainTextAttribute() => this.m_bIsMainText = true;

    public QDesignerMainTextAttribute(bool isMainText) => this.m_bIsMainText = isMainText;

    public bool IsMainText => this.m_bIsMainText;

    public override bool Equals(object obj) => obj is QDesignerMainTextAttribute mainTextAttribute && mainTextAttribute.m_bIsMainText == this.m_bIsMainText;

    public override bool IsDefaultAttribute() => !this.m_bIsMainText;

    public override int GetHashCode() => this.m_bIsMainText.GetHashCode();

    public static void SetPossibleText(object destination, string text)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(destination, new Attribute[1]
      {
        (Attribute) QDesignerMainTextAttribute.True
      });
      for (int index = 0; index < properties.Count; ++index)
        properties[index].SetValue(destination, (object) text);
    }
  }
}
