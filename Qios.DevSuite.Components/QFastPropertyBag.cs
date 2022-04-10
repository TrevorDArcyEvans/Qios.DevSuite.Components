// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFastPropertyBag
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QFastPropertyBag : IQWeakEventPublisher, ICloneable
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QFastPropertyBag m_oBaseProperties;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QProperty[] m_aProperties;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private int m_iSuspendChangeNotification;
    private bool m_bWeakEventHandlers = true;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oPropertyChangedDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private List<int> m_aHiddenProperties;

    public QFastPropertyBag(int size) => this.m_aProperties = new QProperty[size];

    public int Count => this.m_aProperties.Length;

    public void HideProperties(params int[] indices)
    {
      if (this.m_aHiddenProperties == null)
        this.m_aHiddenProperties = new List<int>();
      for (int index = 0; index < indices.Length; ++index)
      {
        if (!this.m_aHiddenProperties.Contains(indices[index]))
          this.m_aHiddenProperties.Add(indices[index]);
      }
    }

    public void ShowProperties(params int[] indices)
    {
      if (this.m_aHiddenProperties == null)
        return;
      for (int index = 0; index < indices.Length; ++index)
      {
        if (this.m_aHiddenProperties.Contains(indices[index]))
          this.m_aHiddenProperties.Remove(indices[index]);
      }
    }

    public bool IsHidden(int index) => this.m_aHiddenProperties != null && this.m_aHiddenProperties.Contains(index);

    public int SuspendChangeNotification()
    {
      ++this.m_iSuspendChangeNotification;
      return this.m_iSuspendChangeNotification;
    }

    public int ResumeChangeNotification(bool notifyChange)
    {
      this.m_iSuspendChangeNotification = Math.Max(0, --this.m_iSuspendChangeNotification);
      if (notifyChange)
        this.HandlePropertyChanged(new QFastPropertyChangedEventArgs(-1));
      return this.m_iSuspendChangeNotification;
    }

    [Browsable(false)]
    public int ChangeNotificationSuspended => this.m_iSuspendChangeNotification;

    [QWeakEvent]
    public event QFastPropertyChangedEventHandler PropertyChanged
    {
      add => this.m_oPropertyChangedDelegate = QWeakDelegate.Combine(this.m_oPropertyChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPropertyChangedDelegate = QWeakDelegate.Remove(this.m_oPropertyChangedDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public object Clone()
    {
      QFastPropertyBag qfastPropertyBag = new QFastPropertyBag(this.Count);
      if (this.m_aHiddenProperties != null)
        qfastPropertyBag.HideProperties(this.m_aHiddenProperties.ToArray());
      for (int index = 0; index < this.m_aProperties.Length; ++index)
        qfastPropertyBag.m_aProperties[index] = this.m_aProperties[index].Clone() as QProperty;
      return (object) qfastPropertyBag;
    }

    public QFastPropertyBag BaseProperties
    {
      get => this.m_oBaseProperties;
      set => this.SetBaseProperties(value, true);
    }

    public void SetBaseProperties(
      QFastPropertyBag baseProperties,
      bool setChildPropertyBags,
      bool raiseEvent)
    {
      this.m_oBaseProperties = baseProperties;
      if (setChildPropertyBags)
      {
        for (int index = 0; index < this.m_aProperties.Length; ++index)
        {
          if (this.m_aProperties[index].Value is IQFastPropertyBagHost qfastPropertyBagHost)
          {
            if (this.m_oBaseProperties != null)
            {
              IQFastPropertyBagHost property = this.m_oBaseProperties.GetProperty(index) as IQFastPropertyBagHost;
              qfastPropertyBagHost.Properties.SetBaseProperties(property?.Properties, setChildPropertyBags, raiseEvent);
            }
            else
              qfastPropertyBagHost.Properties.SetBaseProperties((QFastPropertyBag) null, setChildPropertyBags, raiseEvent);
          }
        }
      }
      if (!raiseEvent)
        return;
      this.HandlePropertyChanged(new QFastPropertyChangedEventArgs(-1));
    }

    public void SetBaseProperties(QFastPropertyBag baseProperties, bool setChildPropertyBags) => this.SetBaseProperties(baseProperties, setChildPropertyBags, true);

    public void DefineResettableProperty(int index, IQResettableValue value)
    {
      QProperty aProperty = this.m_aProperties[index];
      if (aProperty == null)
      {
        QProperty qproperty = new QProperty(value);
        this.m_aProperties[index] = qproperty;
      }
      else
        aProperty.ResettableValue = value;
    }

    public void DefineProperty(int index, object defaultValue) => this.DefineProperty(index, (object) null, defaultValue, false, true, true);

    public void DefineProperty(int index, object value, object defaultValue) => this.DefineProperty(index, value, defaultValue, true, true, true);

    public void DefineProperty(
      int index,
      object value,
      object defaultValue,
      bool returnBasePropertyWhenNull)
    {
      this.DefineProperty(index, value, defaultValue, true, true, returnBasePropertyWhenNull);
    }

    public void DefineProperty(
      int index,
      object value,
      object defaultValue,
      bool replaceValue,
      bool replaceDefaultValue,
      bool returnBasePropertyWhenNull)
    {
      QProperty aProperty = this.m_aProperties[index];
      if (aProperty == null)
      {
        QProperty qproperty = new QProperty(value, defaultValue, returnBasePropertyWhenNull);
        this.m_aProperties[index] = qproperty;
      }
      else
      {
        if (replaceValue)
          aProperty.Value = value;
        if (replaceDefaultValue)
          aProperty.DefaultValue = defaultValue;
        aProperty.ReturnBasePropertyOnNull = returnBasePropertyWhenNull;
      }
    }

    public object GetProperty(int index)
    {
      QProperty qproperty = this.AssertPropertyDefined(index);
      return qproperty.Value != null ? qproperty.Value : this.GetDefaultValue(index);
    }

    public ValueType GetPropertyAsValueType(int index)
    {
      QProperty qproperty = this.AssertPropertyDefined(index);
      return qproperty.Value != null ? (ValueType) qproperty.Value : this.GetDefaultValueAsValueType(index);
    }

    public void SetProperty(int index, object value)
    {
      QProperty qproperty = this.AssertPropertyDefined(index);
      if (object.Equals(qproperty.Value, value))
        return;
      qproperty.Value = value;
      this.HandlePropertyChanged(new QFastPropertyChangedEventArgs(index));
    }

    public void ResetProperty(int index)
    {
      QProperty qproperty = this.AssertPropertyDefined(index);
      if (qproperty.ContainsResettableValue)
        qproperty.ResettableValue.SetToDefaultValues();
      else
        qproperty.Value = (object) null;
      this.HandlePropertyChanged(new QFastPropertyChangedEventArgs(index));
    }

    public object GetDefaultValue(int index)
    {
      QProperty qproperty = this.AssertPropertyDefined(index);
      return qproperty.ReturnBasePropertyOnNull && this.m_oBaseProperties != null ? this.m_oBaseProperties.GetProperty(index) : qproperty.DefaultValue;
    }

    public ValueType GetDefaultValueAsValueType(int index)
    {
      QProperty qproperty = this.AssertPropertyDefined(index);
      return qproperty.ReturnBasePropertyOnNull && this.m_oBaseProperties != null ? this.m_oBaseProperties.GetPropertyAsValueType(index) : (ValueType) qproperty.DefaultValue;
    }

    public bool IsSetToDefaultValue(int index)
    {
      QProperty qproperty = this.AssertPropertyDefined(index);
      if (qproperty.ContainsResettableValue)
        return qproperty.ResettableValue.IsSetToDefaultValues();
      return qproperty.Value == null || object.Equals(qproperty.Value, this.GetDefaultValue(index));
    }

    public bool IsDefined(int index) => this.m_aProperties[index] != null;

    public bool IsValueSet(int index) => this.AssertPropertyDefined(index).Value != null;

    public bool IsSetToDefaultValues()
    {
      for (int index = 0; index < this.m_aProperties.Length; ++index)
      {
        if (!this.IsSetToDefaultValue(index))
          return false;
      }
      return true;
    }

    public void SetToDefaultValues()
    {
      for (int index = 0; index < this.m_aProperties.Length; ++index)
        this.ResetProperty(index);
    }

    private QProperty AssertPropertyDefined(int index) => this.m_aProperties[index] ?? throw new InvalidOperationException(QResources.GetException("QFastPropertyBag_PropertyNotDefined", (object) index));

    internal void HandlePropertyChanged(QFastPropertyChangedEventArgs e)
    {
      if (this.m_iSuspendChangeNotification > 0)
        return;
      this.OnPropertyChanged(e);
    }

    protected virtual void OnPropertyChanged(QFastPropertyChangedEventArgs e) => this.m_oPropertyChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oPropertyChangedDelegate, (object) this, (object) e);
  }
}
