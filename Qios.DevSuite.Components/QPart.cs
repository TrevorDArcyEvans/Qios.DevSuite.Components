// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPart
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPart : IQPart, ICloneable
  {
    private bool m_bIsSystemPart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPart m_oParentPart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPartCollection m_oParentCollection;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQManagedLayoutParent m_oDisplayParent;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPartLayoutEngine m_oLayoutEngine;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPartLayoutListener m_oLayoutListener;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPartPaintEngine m_oPaintEngine;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPartPaintListener m_oPaintListener;
    private string m_sPartName;
    private object m_oContentObject;
    private bool m_bVisible = true;
    private bool m_bHiddenBecauseOfConstraints;
    private IQPartSharedProperties m_oProperties;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPartCalculatedProperties m_oCalculatedProperties;
    private int m_iLayoutOrder;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPartObjectPainter[] m_aObjectPainters;

    internal QPart() => this.InternalConstruct();

    internal QPart(string partName)
    {
      this.InternalConstruct();
      this.m_sPartName = partName;
    }

    internal QPart(
      string partName,
      bool removeContentPartsFromCurrentParent,
      params IQPart[] contentParts)
    {
      this.InternalConstruct();
      this.m_sPartName = partName;
      this.m_oContentObject = (object) this.CreateContentPartsCollection();
      if (!(this.m_oContentObject is QPartCollection oContentObject))
        return;
      for (int index = 0; index < contentParts.Length; ++index)
        oContentObject.Add(contentParts[index], removeContentPartsFromCurrentParent);
    }

    private void InternalConstruct()
    {
    }

    protected virtual IQPartCollection CreateContentPartsCollection() => (IQPartCollection) new QPartCollection((IQPart) this, (IQManagedLayoutParent) null);

    protected virtual QPartCalculatedProperties CreateCalculatedProperties() => new QPartCalculatedProperties((IQPart) this);

    protected virtual IQPartSharedProperties CreatePartProperties() => (IQPartSharedProperties) new QPartSharedProperties();

    public string PartName
    {
      get => this.m_sPartName;
      set => this.m_sPartName = value;
    }

    public virtual int LayoutOrder
    {
      get => this.m_iLayoutOrder;
      set => this.SetLayoutOrder(value, true);
    }

    public void SetLayoutOrder(int value, bool notifyChange)
    {
      this.m_iLayoutOrder = value;
      if (!notifyChange)
        return;
      this.HandleChange(true);
    }

    public QPartCollection Parts => this.m_oContentObject as QPartCollection;

    public bool HasParts => this.Parts != null && this.Parts.Count > 0;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public object ContentObject => this.m_oContentObject;

    internal void PutContentObject(object value) => this.PutContentObject(value, false);

    internal void PutContentObject(object value, bool setContentVisibility)
    {
      this.m_oContentObject = value;
      if (!setContentVisibility)
        return;
      this.SetContentVisibility(value);
    }

    internal void SetContentVisibility(object content)
    {
      switch (content)
      {
        case null:
          this.m_bVisible = false;
          break;
        case string _:
          this.m_bVisible = ((string) content).Length > 0;
          break;
        case Size size:
          this.m_bVisible = size != Size.Empty;
          break;
        case Rectangle rectangle:
          this.m_bVisible = rectangle.Size != Size.Empty;
          break;
        case IQPartSizedContent _:
          this.SetContentVisibility(((IQPartSizedContent) content).ContentObject);
          break;
        default:
          this.m_bVisible = true;
          break;
      }
    }

    public bool Visible
    {
      get
      {
        QTristateBool visible = this.Properties.GetVisible((IQPart) this);
        return visible == QTristateBool.Undefined ? this.m_bVisible : visible == QTristateBool.True;
      }
    }

    internal void PutVisible(bool value) => this.m_bVisible = value;

    internal bool HiddenBecauseOfConstraints
    {
      get => this.m_bHiddenBecauseOfConstraints;
      set => this.m_bHiddenBecauseOfConstraints = value;
    }

    [Browsable(false)]
    public virtual bool IsVisible(QPartVisibilitySelectionTypes visibilityTypes) => QPartHelper.IsVisible((IQPart) this, true, false, visibilityTypes);

    public virtual QPartHitTestResult HitTest(int x, int y) => QPartHelper.DefaultHitTest((IQPart) this, x, y);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IQPart ParentPart => this.m_oParentPart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QPartCollection ParentCollection => this.m_oParentCollection;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual IQManagedLayoutParent DisplayParent => this.m_oDisplayParent;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IQPartLayoutListener LayoutListener
    {
      get => this.m_oLayoutListener;
      set => this.m_oLayoutListener = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IQPartPaintListener PaintListener
    {
      get => this.m_oPaintListener;
      set => this.m_oPaintListener = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IQPartLayoutEngine LayoutEngine
    {
      get => this.m_oLayoutEngine == null ? (IQPartLayoutEngine) QPartLinearLayoutEngine.Default : this.m_oLayoutEngine;
      set => this.m_oLayoutEngine = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IQPartPaintEngine PaintEngine
    {
      get => this.m_oPaintEngine == null ? (IQPartPaintEngine) QPartDefaultPaintEngine.Default : this.m_oPaintEngine;
      set => this.m_oPaintEngine = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public IQPartSharedProperties Properties
    {
      get
      {
        if (this.m_oProperties == null)
          this.m_oProperties = this.CreatePartProperties();
        return this.m_oProperties;
      }
      set => this.m_oProperties = value;
    }

    [Browsable(false)]
    public QPartSharedProperties SharedProperties
    {
      get
      {
        if (this.m_oProperties == null)
          this.m_oProperties = this.CreatePartProperties();
        return this.m_oProperties as QPartSharedProperties;
      }
    }

    [TypeConverter(typeof (ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QPartCalculatedProperties CalculatedProperties
    {
      get
      {
        if (this.m_oCalculatedProperties == null)
          this.m_oCalculatedProperties = this.CreateCalculatedProperties();
        return this.m_oCalculatedProperties;
      }
      set => this.m_oCalculatedProperties = value;
    }

    public IQPartObjectPainter GetObjectPainter(
      QPartPaintLayer paintLayer,
      Type painterType)
    {
      return QPartObjectPainter.GetObjectPainter(this.m_aObjectPainters, paintLayer, painterType);
    }

    public void SetObjectPainter(QPartPaintLayer paintLayer, IQPartObjectPainter painter) => this.m_aObjectPainters = QPartObjectPainter.SetObjectPainter(this.m_aObjectPainters, paintLayer, painter);

    public virtual bool FitsInSelection(params QPartSelectionTypes[] selection) => QPartHelper.FitsInSelection((IQPart) this, selection);

    protected internal virtual void HandleChange(bool performLayout)
    {
      if (this.m_oDisplayParent == null)
        return;
      this.m_oDisplayParent.HandleChildObjectChanged(performLayout, QPartHelper.GetPartPaintBounds((IQPart) this, true));
    }

    bool IQPart.IsSystemPart => this.m_bIsSystemPart;

    internal void PutIsSystemPart(bool value) => this.m_bIsSystemPart = value;

    QRegion IQPart.ContentClipRegion => (QRegion) null;

    void IQPart.PushCalculatedProperties() => this.m_oCalculatedProperties = QPartCalculatedProperties.PushCalculatedProperties(this.m_oCalculatedProperties);

    void IQPart.PullCalculatedProperties() => this.m_oCalculatedProperties = QPartCalculatedProperties.PullCalculatedProperties(this.m_oCalculatedProperties);

    void IQPart.SetParent(
      IQPart parentPart,
      QPartCollection parentCollection,
      bool removeFromCurrentParent,
      bool addToNewParent)
    {
      if (removeFromCurrentParent && this.m_oParentCollection != null)
        this.m_oParentCollection.Remove((IQPart) this);
      this.m_oParentPart = parentPart;
      this.m_oParentCollection = parentCollection;
      if (!addToNewParent || this.m_oParentCollection == null)
        return;
      this.m_oParentCollection.Add((IQPart) this, false);
    }

    void IQPart.SetDisplayParent(IQManagedLayoutParent displayParent)
    {
      this.m_oDisplayParent = displayParent;
      if (!(this.m_oContentObject is QPartCollection oContentObject))
        return;
      oContentObject.SetDisplayParent(this.m_oDisplayParent);
    }

    void IQPart.SetLayoutOrder(int layoutOrder) => this.m_iLayoutOrder = layoutOrder;

    public virtual object Clone()
    {
      QPart newObjectInstance = (QPart) QObjectCloner.CreateNewObjectInstance((object) this);
      QObjectCloner.CopyToObject((object) this, (object) newObjectInstance, false);
      if (newObjectInstance.Parts != null)
        newObjectInstance.Parts.SetParent((IQPart) newObjectInstance, false);
      return (object) newObjectInstance;
    }
  }
}
