// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QProperty
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  internal class QProperty : ICloneable
  {
    public object Value;
    public object DefaultValue;
    public bool ReturnBasePropertyOnNull = true;

    public QProperty()
    {
    }

    public QProperty(object value, object defaultValue)
      : this(value, defaultValue, true)
    {
    }

    public QProperty(object value, object defaultValue, bool returnBasePropertyOnNull)
    {
      this.Value = value;
      this.DefaultValue = defaultValue;
      this.ReturnBasePropertyOnNull = returnBasePropertyOnNull;
    }

    public object Clone() => (object) new QProperty(this.Value is ICloneable cloneable ? cloneable.Clone() : this.Value, this.DefaultValue, this.ReturnBasePropertyOnNull);

    public QProperty(IQResettableValue resettableValue) => this.Value = (object) resettableValue;

    public bool ContainsResettableValue => this.Value is IQResettableValue;

    public IQResettableValue ResettableValue
    {
      get => this.Value as IQResettableValue;
      set => this.Value = (object) value;
    }
  }
}
