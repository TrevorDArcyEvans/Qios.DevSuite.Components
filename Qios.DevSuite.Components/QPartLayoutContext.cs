// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartLayoutContext
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QPartLayoutContext : IDisposable
  {
    private const int PreviousContextIndex = 0;
    private const int AdditonalPropertiesIndex = 1;
    private const int GraphicsIndex = 2;
    private const int FontIndex = 3;
    private const int StringFormatIndex = 4;
    private const int HintsIndex = 5;
    private const int VisibilitySelectionIndex = 6;
    private const int CalculatePartSizeOptionsIndex = 7;
    private const int PropertyCount = 8;
    private object[] m_aProperties;

    public QPartLayoutContext()
    {
      this.m_aProperties = new object[8];
      this.InternalConstruct();
    }

    private QPartLayoutContext(object[] properties) => this.m_aProperties = properties;

    private void InternalConstruct()
    {
      this.VisibilitySelection = QPartVisibilitySelectionTypes.IncludeAll;
      this.CalculatePartSizeOptions = QPartCalculatePartSizeOptions.None;
    }

    public static QPartLayoutContext CreateFromControl(Control control) => new QPartLayoutContext()
    {
      Graphics = control.CreateGraphics(),
      Font = control.Font,
      StringFormat = (StringFormat) StringFormat.GenericDefault.Clone()
    };

    public void Push()
    {
      QPartLayoutContext qpartLayoutContext = new QPartLayoutContext(this.m_aProperties);
      this.m_aProperties = new object[8];
      this.m_aProperties[0] = (object) qpartLayoutContext;
    }

    public void Pull()
    {
      if (this.PreviousContext == null)
        return;
      this.DisposeCurrentLevelProperties();
      this.m_aProperties = this.PreviousContext.m_aProperties;
    }

    public QPartLayoutContext PreviousContext => this.m_aProperties[0] as QPartLayoutContext;

    public object AdditonalProperties
    {
      get => this.GetProperty(1);
      set => this.m_aProperties[1] = value;
    }

    public Graphics Graphics
    {
      get => this.GetProperty(2) as Graphics;
      set => this.m_aProperties[2] = (object) value;
    }

    public Font Font
    {
      get => this.GetProperty(3) as Font;
      set => this.m_aProperties[3] = (object) value;
    }

    public StringFormat StringFormat
    {
      get => this.GetProperty(4) as StringFormat;
      set => this.m_aProperties[4] = (object) value;
    }

    public QPartVisibilitySelectionTypes VisibilitySelection
    {
      get => (QPartVisibilitySelectionTypes) this.GetProperty(6);
      set => this.m_aProperties[6] = (object) value;
    }

    public QPartCalculatePartSizeOptions CalculatePartSizeOptions
    {
      get => (QPartCalculatePartSizeOptions) this.GetProperty(7);
      set => this.m_aProperties[7] = (object) value;
    }

    private object GetProperty(int index)
    {
      if (this.m_aProperties[index] != null)
        return this.m_aProperties[index];
      return this.PreviousContext != null ? this.PreviousContext.GetProperty(index) : (object) null;
    }

    private void DisposeCurrentLevelProperties()
    {
      for (int index = 0; index < this.m_aProperties.Length; ++index)
      {
        IDisposable aProperty = this.m_aProperties[index] as IDisposable;
        if (index != 0 && index != 3 && aProperty != null)
        {
          aProperty.Dispose();
          this.m_aProperties[index] = (object) null;
        }
      }
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.DisposeCurrentLevelProperties();
    }

    public void Dispose() => this.Dispose(true);

    ~QPartLayoutContext() => this.Dispose(false);
  }
}
