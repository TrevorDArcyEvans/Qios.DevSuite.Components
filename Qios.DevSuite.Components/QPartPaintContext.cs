// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartPaintContext
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QPartPaintContext : IDisposable
  {
    private const int PreviousContextIndex = 0;
    private const int AdditonalPropertiesIndex = 1;
    private const int GraphicsIndex = 2;
    private const int FontIndex = 3;
    private const int StringFormatIndex = 4;
    private const int ReverseIndex = 5;
    private const int VisibilitySelectionIndex = 6;
    private const int RecursiveLevelsIndex = 7;
    private const int CurrentLevelIndex = 8;
    private const int PropertyCount = 9;
    private object[] m_aProperties;

    public QPartPaintContext()
    {
      this.m_aProperties = new object[9];
      this.InternalConstruct();
    }

    internal QPartPaintContext(object[] properties) => this.m_aProperties = properties;

    private void InternalConstruct()
    {
      this.Reverse = false;
      this.CurrentLevel = 0;
      this.RecursiveLevels = -1;
      this.VisibilitySelection = QPartVisibilitySelectionTypes.IncludeAll;
    }

    public static QPartPaintContext CreateFromControl(
      Control control,
      Graphics graphicsToUse)
    {
      return new QPartPaintContext()
      {
        Graphics = graphicsToUse,
        Font = control.Font,
        StringFormat = (StringFormat) StringFormat.GenericDefault.Clone()
      };
    }

    public void Push()
    {
      QPartPaintContext qpartPaintContext = new QPartPaintContext(this.m_aProperties);
      this.m_aProperties = new object[9];
      this.m_aProperties[0] = (object) qpartPaintContext;
    }

    public void Pull()
    {
      if (this.PreviousContext == null)
        return;
      this.DisposeCurrentLevelProperties();
      this.m_aProperties = this.PreviousContext.m_aProperties;
    }

    public QPartPaintContext PreviousContext => this.m_aProperties[0] as QPartPaintContext;

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

    public bool Reverse
    {
      get => (bool) this.GetProperty(5);
      set => this.m_aProperties[5] = (object) value;
    }

    public QPartVisibilitySelectionTypes VisibilitySelection
    {
      get => (QPartVisibilitySelectionTypes) this.GetProperty(6);
      set => this.m_aProperties[6] = (object) value;
    }

    public int RecursiveLevels
    {
      get => (int) this.GetProperty(7);
      set => this.m_aProperties[7] = (object) value;
    }

    public int CurrentLevel
    {
      get => (int) this.GetProperty(8);
      set => this.m_aProperties[8] = (object) value;
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
        if (index != 0 && index != 2 && index != 3 && aProperty != null)
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

    ~QPartPaintContext() => this.Dispose(false);
  }
}
