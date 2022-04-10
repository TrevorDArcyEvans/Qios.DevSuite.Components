// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPropertyBagHost
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  [Obsolete("bla")]
  public class QPropertyBagHost : 
    ICustomTypeDescriptor,
    IQPropertyBagHost,
    IQResettableValue,
    IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private QPropertyBag m_oProperties;

    public QPropertyBagHost() => this.m_oProperties = new QPropertyBag();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [Browsable(false)]
    public QPropertyBag Properties => this.m_oProperties;

    public void SetToDefaultValues() => this.Properties.SetToDefaultValues();

    public bool IsSetToDefaultValues() => this.Properties.IsSetToDefaultValues();

    public TypeConverter GetConverter() => TypeDescriptor.GetConverter((object) this, true);

    public EventDescriptorCollection GetEvents(Attribute[] attributes) => TypeDescriptor.GetEvents((object) this, attributes, true);

    public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents((object) this, true);

    public string GetComponentName() => TypeDescriptor.GetComponentName((object) this, true);

    public object GetPropertyOwner(PropertyDescriptor pd) => (object) this;

    public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes((object) this, true);

    public PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      return TypeDescriptor.GetConverter((object) this, true).GetProperties((ITypeDescriptorContext) null, (object) this, attributes);
    }

    public PropertyDescriptorCollection GetProperties() => TypeDescriptor.GetConverter((object) this, true).GetProperties((object) this);

    public object GetEditor(Type editorBaseType) => TypeDescriptor.GetEditor((object) this, editorBaseType, true);

    public PropertyDescriptor GetDefaultProperty() => TypeDescriptor.GetDefaultProperty((object) this, true);

    public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent((object) this, true);

    public string GetClassName() => TypeDescriptor.GetClassName((object) this, true);
  }
}
