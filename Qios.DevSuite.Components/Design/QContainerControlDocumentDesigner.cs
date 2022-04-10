// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QContainerControlDocumentDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QContainerControlDocumentDesigner : DocumentDesigner
  {
    private IQDesignableContainerControl m_oContainerControl;
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      this.m_oContainerControl = component as IQDesignableContainerControl;
      if (this.m_oContainerControl != null)
        this.m_oContainerControl.InitializingDocumentDesigner = true;
      base.Initialize(component);
      if (this.m_oContainerControl != null)
        this.m_oContainerControl.InitializingDocumentDesigner = false;
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      if (this.m_oContainerControl == null)
        return;
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oContainerControl.ColorScheme);
    }

    public bool ShouldSerializeBackColor() => false;

    public void ResetBackColor()
    {
    }

    public Color BackColor
    {
      get => this.m_oContainerControl.BackColor;
      set => this.m_oContainerControl.BackColor = value;
    }

    protected override void PreFilterProperties(IDictionary properties)
    {
      base.PreFilterProperties(properties);
      AttributeCollection attributes = TypeDescriptor.GetProperties(typeof (Control))["BackColor"].Attributes;
      Attribute[] attributeArray = new Attribute[attributes.Count];
      attributes.CopyTo((Array) attributeArray, 0);
      PropertyDescriptor property = TypeDescriptor.CreateProperty(typeof (QContainerControlDocumentDesigner), "BackColor", typeof (Color), attributeArray);
      properties[(object) "BackColor"] = (object) property;
    }

    public override DesignerVerbCollection Verbs => this.m_oColorSchemeXmlHandler.Verbs;

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.m_oGeneralHandler == null)
        return;
      this.m_oGeneralHandler.Dispose();
    }
  }
}
