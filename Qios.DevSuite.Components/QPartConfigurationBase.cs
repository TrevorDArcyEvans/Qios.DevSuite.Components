// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartConfigurationBase
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartConfigurationBase : 
    QFastPropertyBagHost,
    IDisposable,
    IQPartConfigurationBase,
    IQPartSharedProperties
  {
    protected internal const int PropMargin = 0;
    protected internal const int PropPadding = 1;
    protected internal const int PropShrinkHorizontal = 2;
    protected internal const int PropShrinkVertical = 3;
    protected internal const int PropStretchHorizontal = 4;
    protected internal const int PropStretchVertical = 5;
    protected internal const int PropAlignmentHorizontal = 6;
    protected internal const int PropAlignmentVertical = 7;
    protected internal const int PropMinimumSize = 8;
    protected internal const int PropMaximumSize = 9;
    protected internal const int PropVisible = 10;
    protected internal const int PropDirection = 11;
    protected internal const int PropContentLayoutOrder = 12;
    protected internal const int PropContentAlignmentHorizontal = 13;
    protected internal const int PropContentAlignmentVertical = 14;
    protected internal const int CurrentPropertyCount = 15;
    protected const int TotalPropertyCount = 15;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    public QPartConfigurationBase()
    {
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
      this.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      this.Properties.DefineProperty(1, (object) new QPadding(0, 0, 0, 0));
      this.Properties.DefineProperty(2, (object) false);
      this.Properties.DefineProperty(3, (object) false);
      this.Properties.DefineProperty(4, (object) false);
      this.Properties.DefineProperty(5, (object) false);
      this.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(7, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(9, (object) Size.Empty);
      this.Properties.DefineProperty(8, (object) Size.Empty);
      this.Properties.DefineProperty(10, (object) QTristateBool.Undefined);
      this.Properties.DefineProperty(11, (object) QPartDirection.Horizontal);
      this.Properties.DefineProperty(12, (object) null);
      this.Properties.DefineProperty(13, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(14, (object) QPartAlignment.Near);
    }

    protected override int GetRequestedCount() => 15;

    [QWeakEvent]
    [Description("Gets raised when a property of the configuration is changed")]
    [Category("QEvents")]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    public virtual QTristateBool GetVisible(IQPart part) => (QTristateBool) this.Properties.GetPropertyAsValueType(10);

    public virtual IQPadding[] GetPaddings(IQPart part) => new IQPadding[1]
    {
      (IQPadding) this.Properties.GetPropertyAsValueType(1)
    };

    public virtual IQMargin[] GetMargins(IQPart part) => new IQMargin[1]
    {
      (IQMargin) this.Properties.GetPropertyAsValueType(0)
    };

    public virtual Size GetMinimumSize(IQPart part) => (Size) this.Properties.GetPropertyAsValueType(8);

    public virtual Size GetMaximumSize(IQPart part) => (Size) this.Properties.GetPropertyAsValueType(9);

    public virtual QPartDirection GetDirection(IQPart part) => (QPartDirection) this.Properties.GetPropertyAsValueType(11);

    public virtual QPartAlignment GetAlignmentHorizontal(IQPart part) => (QPartAlignment) this.Properties.GetPropertyAsValueType(6);

    public virtual QPartAlignment GetAlignmentVertical(IQPart part) => (QPartAlignment) this.Properties.GetPropertyAsValueType(7);

    public virtual QPartAlignment GetContentAlignmentHorizontal(IQPart part) => (QPartAlignment) this.Properties.GetPropertyAsValueType(13);

    public virtual QPartAlignment GetContentAlignmentVertical(IQPart part) => (QPartAlignment) this.Properties.GetPropertyAsValueType(14);

    public virtual string GetContentLayoutOrder(IQPart part) => this.Properties.GetProperty(12) as string;

    public virtual QPartOptions GetOptions(IQPart part)
    {
      QPartOptions options = QPartOptions.None;
      if ((bool) this.Properties.GetPropertyAsValueType(4))
        options |= QPartOptions.StretchHorizontal;
      if ((bool) this.Properties.GetPropertyAsValueType(5))
        options |= QPartOptions.StretchVertical;
      if ((bool) this.Properties.GetPropertyAsValueType(2))
        options |= QPartOptions.ShrinkHorizontal;
      if ((bool) this.Properties.GetPropertyAsValueType(3))
        options |= QPartOptions.ShrinkVertical;
      return options;
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
    }

    ~QPartConfigurationBase() => this.Dispose(false);
  }
}
