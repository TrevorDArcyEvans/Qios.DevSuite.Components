// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPropertyBag
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QPropertyBag : IQWeakEventPublisher, ICloneable
  {
    private QPropertyBag m_oBaseProperties;
    private Hashtable m_aProperties;
    private bool m_bWeakEventHandlers = true;
    private QWeakDelegate m_oPropertyChangedDelegate;

    public QPropertyBag() => this.m_aProperties = new Hashtable((IEqualityComparer) StringComparer.CurrentCultureIgnoreCase);

    [QWeakEvent]
    public event QPropertyChangedEventHandler PropertyChanged
    {
      add => this.m_oPropertyChangedDelegate = QWeakDelegate.Combine(this.m_oPropertyChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPropertyChangedDelegate = QWeakDelegate.Remove(this.m_oPropertyChangedDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public int Count => this.m_aProperties.Count;

    public string[] RetrievePropertyNames()
    {
      string[] strArray = new string[this.m_aProperties.Keys.Count];
      this.m_aProperties.Keys.CopyTo((Array) strArray, 0);
      return strArray;
    }

    public object Clone()
    {
      QPropertyBag qpropertyBag = (QPropertyBag) QObjectCloner.CloneObject((object) this);
      string[] strArray = this.RetrievePropertyNames();
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (qpropertyBag.m_aProperties[(object) strArray[index]] is ICloneable aProperty)
          qpropertyBag.m_aProperties[(object) strArray[index]] = aProperty.Clone();
      }
      return (object) qpropertyBag;
    }

    public QPropertyBag BaseProperties
    {
      get => this.m_oBaseProperties;
      set => this.SetBaseProperties(value, true);
    }

    public void SetBaseProperties(
      QPropertyBag baseProperties,
      bool setChildPropertyBags,
      bool raiseEvent)
    {
      this.m_oBaseProperties = baseProperties;
      if (setChildPropertyBags)
      {
        foreach (DictionaryEntry aProperty in this.m_aProperties)
        {
          QProperty qproperty = (QProperty) aProperty.Value;
          IQPropertyBagHost qpropertyBagHost = qproperty.Value as IQPropertyBagHost;
          if (qproperty.Value is IQPropertyBagHost)
          {
            if (this.m_oBaseProperties != null)
            {
              IQPropertyBagHost property = this.m_oBaseProperties.GetProperty(aProperty.Key as string) as IQPropertyBagHost;
              qpropertyBagHost.Properties.SetBaseProperties(property?.Properties, setChildPropertyBags, raiseEvent);
            }
            else
              qpropertyBagHost.Properties.SetBaseProperties((QPropertyBag) null, setChildPropertyBags, raiseEvent);
          }
        }
      }
      if (!raiseEvent)
        return;
      this.OnPropertyChanged(new QPropertyChangedEventArgs((string) null));
    }

    public void SetBaseProperties(QPropertyBag baseProperties, bool setChildPropertyBags) => this.SetBaseProperties(baseProperties, setChildPropertyBags, true);

    public void DefineResettableProperty(string name, IQResettableValue value)
    {
      if (!(this.m_aProperties[(object) name] is QProperty aProperty))
      {
        QProperty qproperty = new QProperty(value);
        this.m_aProperties[(object) name] = (object) qproperty;
      }
      else
        aProperty.ResettableValue = value;
    }

    public void DefineProperty(string name, object defaultValue) => this.DefineProperty(name, (object) null, defaultValue, true);

    public void DefineProperty(string name, object value, object defaultValue) => this.DefineProperty(name, value, defaultValue, true);

    public void DefineProperty(
      string name,
      object value,
      object defaultValue,
      bool returnBasePropertyWhenNull)
    {
      if (!(this.m_aProperties[(object) name] is QProperty aProperty))
      {
        QProperty qproperty = new QProperty(value, defaultValue, returnBasePropertyWhenNull);
        this.m_aProperties[(object) name] = (object) qproperty;
      }
      else
      {
        aProperty.Value = value;
        aProperty.DefaultValue = defaultValue;
        aProperty.ReturnBasePropertyOnNull = returnBasePropertyWhenNull;
      }
    }

    public object GetProperty(string name)
    {
      QProperty qproperty = this.AssertPropertyDefined(name);
      return qproperty.Value != null ? qproperty.Value : this.GetDefaultValue(name);
    }

    public ValueType GetPropertyAsValueType(string name)
    {
      QProperty qproperty = this.AssertPropertyDefined(name);
      return qproperty.Value != null ? (ValueType) qproperty.Value : this.GetDefaultValueAsValueType(name);
    }

    public void SetProperty(string name, object value)
    {
      QProperty qproperty = this.AssertPropertyDefined(name);
      if (object.Equals(qproperty.Value, value))
        return;
      qproperty.Value = value;
      this.OnPropertyChanged(new QPropertyChangedEventArgs(name));
    }

    public void ResetProperty(string name)
    {
      QProperty qproperty = this.AssertPropertyDefined(name);
      if (qproperty.ContainsResettableValue)
        qproperty.ResettableValue.SetToDefaultValues();
      else
        qproperty.Value = (object) null;
      this.OnPropertyChanged(new QPropertyChangedEventArgs(name));
    }

    public object GetDefaultValue(string name)
    {
      QProperty qproperty = this.AssertPropertyDefined(name);
      return qproperty.ReturnBasePropertyOnNull && this.m_oBaseProperties != null ? this.m_oBaseProperties.GetProperty(name) : qproperty.DefaultValue;
    }

    public ValueType GetDefaultValueAsValueType(string name)
    {
      QProperty qproperty = this.AssertPropertyDefined(name);
      return qproperty.ReturnBasePropertyOnNull && this.m_oBaseProperties != null ? this.m_oBaseProperties.GetPropertyAsValueType(name) : (ValueType) qproperty.DefaultValue;
    }

    public bool IsSetToDefaultValue(string name)
    {
      QProperty qproperty = this.AssertPropertyDefined(name);
      if (qproperty.ContainsResettableValue)
        return qproperty.ResettableValue.IsSetToDefaultValues();
      return qproperty.Value == null || object.Equals(qproperty.Value, this.GetDefaultValue(name));
    }

    public bool IsDefined(string name) => this.m_aProperties.Contains((object) name);

    public bool IsValueSet(string name) => this.AssertPropertyDefined(name).Value != null;

    public bool IsSetToDefaultValues()
    {
      foreach (string key in (IEnumerable) this.m_aProperties.Keys)
      {
        if (!this.IsSetToDefaultValue(key))
          return false;
      }
      return true;
    }

    public void SetToDefaultValues()
    {
      foreach (string key in (IEnumerable) this.m_aProperties.Keys)
        this.ResetProperty(key);
    }

    private QProperty AssertPropertyDefined(string name)
    {
      if (!(this.m_aProperties[(object) name] is QProperty aProperty))
        throw new InvalidOperationException(QResources.GetException("QPropertyBag_PropertyNotDefined", (object) name));
      return aProperty;
    }

    protected virtual void OnPropertyChanged(QPropertyChangedEventArgs e) => this.m_oPropertyChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oPropertyChangedDelegate, (object) this, (object) e);
  }
}
