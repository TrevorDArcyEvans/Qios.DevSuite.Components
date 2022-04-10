// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QAppearanceWrapper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QAppearanceWrapper : 
    IQAppearance,
    IQGradientAppearance,
    IQMetallicAppearance,
    IQBorderedAppearance,
    IQBorderedAdvancedAppearance,
    IQShadedShapeAppearance,
    IQShadedAppearance,
    IQShapeAppearance
  {
    private const int BackgroundStyleIndex = 0;
    private const int ValueCount = 1;
    private const int GradientAngleIndex = 0;
    private const int GradientBlendPositionIndex = 1;
    private const int GradientBlendFactorIndex = 2;
    private const int GradientValueCount = 3;
    private const int MetallicOffsetIndex = 0;
    private const int MetallicOffsetUnitIndex = 1;
    private const int MetallicNearIntensityIndex = 2;
    private const int MetallicFarIntensityIndex = 3;
    private const int MetallicInnerGlowWidthIndex = 4;
    private const int MetallicShineIntensityIndex = 5;
    private const int MetallicDirectionIndex = 6;
    private const int MetallicShineSaturationIndex = 7;
    private const int MetallicAutomaticColorOrderIndex = 8;
    private const int MetallicValueCount = 9;
    private const int BorderWidthIndex = 0;
    private const int BorderedValueCount = 1;
    private const int ShowBorderLeftIndex = 0;
    private const int ShowBorderTopIndex = 1;
    private const int ShowBorderBottomIndex = 2;
    private const int ShowBorderRightIndex = 3;
    private const int ShowBordersIndex = 4;
    private const int BorderedAdvancedValueCount = 5;
    private const int SmoothingModeIndex = 0;
    private const int ShapeIndex = 1;
    private const int ShapeValueCount = 2;
    private const int ShadeOffsetIndex = 0;
    private const int ShadeGradientSizeIndex = 1;
    private const int ShadeVisibleIndex = 2;
    private const int ShadeClipToShapeBoundsIndex = 3;
    private const int ShadeClipMarginIndex = 4;
    private const int ShadeGrowPaddingIndex = 5;
    private const int ShadedValueCount = 6;
    private IQAppearance m_oBaseAppearance;
    private object[] m_aValues;
    private object[] m_aGradientValues;
    private object[] m_aMetallicValues;
    private object[] m_aBorderedValues;
    private object[] m_aBorderedAdvancedValues;
    private object[] m_aShapeValues;
    private object[] m_aShadedValues;

    public QAppearanceWrapper(IQAppearance baseAppearance) => this.m_oBaseAppearance = baseAppearance;

    public QColorStyle BackgroundStyle
    {
      get
      {
        IQAppearance oBaseAppearance = this.m_oBaseAppearance;
        if (this.m_aValues != null && this.m_aValues[0] != null)
          return (QColorStyle) this.m_aValues[0];
        return oBaseAppearance != null ? oBaseAppearance.BackgroundStyle : QColorStyle.Gradient;
      }
      set
      {
        if (this.m_aValues == null)
          this.m_aValues = new object[1];
        this.m_aValues[0] = (object) value;
      }
    }

    public int GradientAngle
    {
      get
      {
        IQGradientAppearance oBaseAppearance = this.m_oBaseAppearance as IQGradientAppearance;
        if (this.m_aGradientValues != null && this.m_aGradientValues[0] != null)
          return (int) this.m_aGradientValues[0];
        return oBaseAppearance != null ? oBaseAppearance.GradientAngle : 0;
      }
      set
      {
        if (this.m_aGradientValues == null)
          this.m_aGradientValues = new object[3];
        this.m_aGradientValues[0] = (object) value;
      }
    }

    public int GradientBlendPosition
    {
      get
      {
        IQGradientAppearance oBaseAppearance = this.m_oBaseAppearance as IQGradientAppearance;
        if (this.m_aGradientValues != null && this.m_aGradientValues[1] != null)
          return (int) this.m_aGradientValues[1];
        return oBaseAppearance != null ? oBaseAppearance.GradientBlendPosition : 100;
      }
      set
      {
        if (this.m_aGradientValues == null)
          this.m_aGradientValues = new object[1];
        this.m_aGradientValues[1] = (object) value;
      }
    }

    public int GradientBlendFactor
    {
      get
      {
        IQGradientAppearance oBaseAppearance = this.m_oBaseAppearance as IQGradientAppearance;
        if (this.m_aGradientValues != null && this.m_aGradientValues[2] != null)
          return (int) this.m_aGradientValues[2];
        return oBaseAppearance != null ? oBaseAppearance.GradientBlendFactor : 100;
      }
      set
      {
        if (this.m_aGradientValues == null)
          this.m_aGradientValues = new object[3];
        this.m_aGradientValues[2] = (object) value;
      }
    }

    public int MetallicOffset
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[0] != null)
          return (int) this.m_aMetallicValues[0];
        return oBaseAppearance != null ? oBaseAppearance.MetallicOffset : 40;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[0] = (object) value;
      }
    }

    public QAppearanceUnit MetallicOffsetUnit
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[1] != null)
          return (QAppearanceUnit) this.m_aMetallicValues[1];
        return oBaseAppearance != null ? oBaseAppearance.MetallicOffsetUnit : QAppearanceUnit.Percent;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[1] = (object) value;
      }
    }

    public int MetallicNearIntensity
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[2] != null)
          return (int) this.m_aMetallicValues[2];
        return oBaseAppearance != null ? oBaseAppearance.MetallicNearIntensity : 50;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[2] = (object) value;
      }
    }

    public int MetallicFarIntensity
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[3] != null)
          return (int) this.m_aMetallicValues[3];
        return oBaseAppearance != null ? oBaseAppearance.MetallicFarIntensity : 50;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[3] = (object) value;
      }
    }

    public int MetallicInnerGlowWidth
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[4] != null)
          return (int) this.m_aMetallicValues[4];
        return oBaseAppearance != null ? oBaseAppearance.MetallicInnerGlowWidth : 0;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[4] = (object) value;
      }
    }

    public int MetallicShineIntensity
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[5] != null)
          return (int) this.m_aMetallicValues[5];
        return oBaseAppearance != null ? oBaseAppearance.MetallicShineIntensity : 70;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[5] = (object) value;
      }
    }

    public int MetallicShineSaturation
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[7] != null)
          return (int) this.m_aMetallicValues[7];
        return oBaseAppearance != null ? oBaseAppearance.MetallicShineSaturation : 100;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[7] = (object) value;
      }
    }

    public bool MetallicAutomaticColorOrder
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[8] != null)
          return (bool) this.m_aMetallicValues[8];
        return oBaseAppearance == null || oBaseAppearance.MetallicAutomaticColorOrder;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[8] = (object) value;
      }
    }

    public QMetallicAppearanceDirection MetallicDirection
    {
      get
      {
        IQMetallicAppearance oBaseAppearance = this.m_oBaseAppearance as IQMetallicAppearance;
        if (this.m_aMetallicValues != null && this.m_aMetallicValues[6] != null)
          return (QMetallicAppearanceDirection) this.m_aMetallicValues[6];
        return oBaseAppearance != null ? oBaseAppearance.MetallicDirection : QMetallicAppearanceDirection.Horizontal;
      }
      set
      {
        if (this.m_aMetallicValues == null)
          this.m_aMetallicValues = new object[9];
        this.m_aMetallicValues[6] = (object) value;
      }
    }

    public int BorderWidth
    {
      get
      {
        IQBorderedAppearance oBaseAppearance = this.m_oBaseAppearance as IQBorderedAppearance;
        if (this.m_aBorderedValues != null && this.m_aBorderedValues[0] != null)
          return (int) this.m_aBorderedValues[0];
        return oBaseAppearance != null ? oBaseAppearance.BorderWidth : 1;
      }
      set
      {
        if (this.m_aBorderedValues == null)
          this.m_aBorderedValues = new object[1];
        this.m_aBorderedValues[0] = (object) value;
      }
    }

    public bool ShowBorderLeft
    {
      get
      {
        IQBorderedAdvancedAppearance oBaseAppearance = this.m_oBaseAppearance as IQBorderedAdvancedAppearance;
        if (this.m_aBorderedAdvancedValues != null && this.m_aBorderedAdvancedValues[0] != null)
          return (bool) this.m_aBorderedAdvancedValues[0];
        return oBaseAppearance == null || oBaseAppearance.ShowBorderLeft;
      }
      set
      {
        if (this.m_aBorderedAdvancedValues == null)
          this.m_aBorderedAdvancedValues = new object[5];
        this.m_aBorderedAdvancedValues[0] = (object) value;
      }
    }

    public bool ShowBorderTop
    {
      get
      {
        IQBorderedAdvancedAppearance oBaseAppearance = this.m_oBaseAppearance as IQBorderedAdvancedAppearance;
        if (this.m_aBorderedAdvancedValues != null && this.m_aBorderedAdvancedValues[1] != null)
          return (bool) this.m_aBorderedAdvancedValues[1];
        return oBaseAppearance == null || oBaseAppearance.ShowBorderTop;
      }
      set
      {
        if (this.m_aBorderedAdvancedValues == null)
          this.m_aBorderedAdvancedValues = new object[5];
        this.m_aBorderedAdvancedValues[1] = (object) value;
      }
    }

    public bool ShowBorderBottom
    {
      get
      {
        IQBorderedAdvancedAppearance oBaseAppearance = this.m_oBaseAppearance as IQBorderedAdvancedAppearance;
        if (this.m_aBorderedAdvancedValues != null && this.m_aBorderedAdvancedValues[2] != null)
          return (bool) this.m_aBorderedAdvancedValues[2];
        return oBaseAppearance == null || oBaseAppearance.ShowBorderBottom;
      }
      set
      {
        if (this.m_aBorderedAdvancedValues == null)
          this.m_aBorderedAdvancedValues = new object[5];
        this.m_aBorderedAdvancedValues[2] = (object) value;
      }
    }

    public bool ShowBorderRight
    {
      get
      {
        IQBorderedAdvancedAppearance oBaseAppearance = this.m_oBaseAppearance as IQBorderedAdvancedAppearance;
        if (this.m_aBorderedAdvancedValues != null && this.m_aBorderedAdvancedValues[3] != null)
          return (bool) this.m_aBorderedAdvancedValues[3];
        return oBaseAppearance == null || oBaseAppearance.ShowBorderRight;
      }
      set
      {
        if (this.m_aBorderedAdvancedValues == null)
          this.m_aBorderedAdvancedValues = new object[5];
        this.m_aBorderedAdvancedValues[3] = (object) value;
      }
    }

    public bool ShowBorders
    {
      get
      {
        IQBorderedAdvancedAppearance oBaseAppearance = this.m_oBaseAppearance as IQBorderedAdvancedAppearance;
        if (this.m_aBorderedAdvancedValues != null && this.m_aBorderedAdvancedValues[4] != null)
          return (bool) this.m_aBorderedAdvancedValues[4];
        return oBaseAppearance == null || oBaseAppearance.ShowBorders;
      }
      set
      {
        if (this.m_aBorderedAdvancedValues == null)
          this.m_aBorderedAdvancedValues = new object[5];
        this.m_aBorderedAdvancedValues[4] = (object) value;
      }
    }

    public QSmoothingMode SmoothingMode
    {
      get
      {
        IQShapeAppearance oBaseAppearance = this.m_oBaseAppearance as IQShapeAppearance;
        if (this.m_aShapeValues != null && this.m_aShapeValues[0] != null)
          return (QSmoothingMode) this.m_aShapeValues[0];
        return oBaseAppearance != null ? oBaseAppearance.SmoothingMode : QSmoothingMode.AntiAlias;
      }
      set
      {
        if (this.m_aShapeValues == null)
          this.m_aShapeValues = new object[2];
        this.m_aShapeValues[0] = (object) value;
      }
    }

    public QShape Shape
    {
      get
      {
        IQShapeAppearance oBaseAppearance = this.m_oBaseAppearance as IQShapeAppearance;
        if (this.m_aShapeValues != null && this.m_aShapeValues[1] != null)
          return (QShape) this.m_aShapeValues[1];
        return oBaseAppearance != null ? oBaseAppearance.Shape : new QShape(QBaseShapeType.RectangleShape);
      }
      set
      {
        if (this.m_aShapeValues == null)
          this.m_aShapeValues = new object[2];
        this.m_aShapeValues[1] = (object) value;
      }
    }

    public Point ShadeOffset
    {
      get
      {
        IQShadedAppearance oBaseAppearance = this.m_oBaseAppearance as IQShadedAppearance;
        if (this.m_aShadedValues != null && this.m_aShadedValues[0] != null)
          return (Point) this.m_aShadedValues[0];
        return oBaseAppearance != null ? oBaseAppearance.ShadeOffset : new Point(3, 3);
      }
      set
      {
        if (this.m_aShadedValues == null)
          this.m_aShadedValues = new object[6];
        this.m_aShadedValues[0] = (object) value;
      }
    }

    public int ShadeGradientSize
    {
      get
      {
        IQShadedAppearance oBaseAppearance = this.m_oBaseAppearance as IQShadedAppearance;
        if (this.m_aShadedValues != null && this.m_aShadedValues[1] != null)
          return (int) this.m_aShadedValues[1];
        return oBaseAppearance != null ? oBaseAppearance.ShadeGradientSize : 3;
      }
      set
      {
        if (this.m_aShadedValues == null)
          this.m_aShadedValues = new object[6];
        this.m_aShadedValues[1] = (object) value;
      }
    }

    public bool ShadeVisible
    {
      get
      {
        IQShadedAppearance oBaseAppearance = this.m_oBaseAppearance as IQShadedAppearance;
        if (this.m_aShadedValues != null && this.m_aShadedValues[2] != null)
          return (bool) this.m_aShadedValues[2];
        return oBaseAppearance != null && oBaseAppearance.ShadeVisible;
      }
      set
      {
        if (this.m_aShadedValues == null)
          this.m_aShadedValues = new object[6];
        this.m_aShadedValues[2] = (object) value;
      }
    }

    public bool ShadeClipToShapeBounds
    {
      get
      {
        IQShadedAppearance oBaseAppearance = this.m_oBaseAppearance as IQShadedAppearance;
        if (this.m_aShadedValues != null && this.m_aShadedValues[3] != null)
          return (bool) this.m_aShadedValues[3];
        return oBaseAppearance != null && oBaseAppearance.ShadeClipToShapeBounds;
      }
      set
      {
        if (this.m_aShadedValues == null)
          this.m_aShadedValues = new object[6];
        this.m_aShadedValues[3] = (object) value;
      }
    }

    public QMargin ShadeClipMargin
    {
      get
      {
        IQShadedAppearance oBaseAppearance = this.m_oBaseAppearance as IQShadedAppearance;
        if (this.m_aShadedValues != null && this.m_aShadedValues[4] != null)
          return (QMargin) this.m_aShadedValues[4];
        return oBaseAppearance != null ? oBaseAppearance.ShadeClipMargin : new QMargin(0, 0, 0, 0);
      }
      set
      {
        if (this.m_aShadedValues == null)
          this.m_aShadedValues = new object[6];
        this.m_aShadedValues[4] = (object) value;
      }
    }

    public QPadding ShadeGrowPadding
    {
      get
      {
        IQShadedAppearance oBaseAppearance = this.m_oBaseAppearance as IQShadedAppearance;
        if (this.m_aShadedValues != null && this.m_aShadedValues[5] != null)
          return (QPadding) this.m_aShadedValues[5];
        return oBaseAppearance != null ? oBaseAppearance.ShadeGrowPadding : new QPadding(0, 0, 0, 0);
      }
      set
      {
        if (this.m_aShadedValues == null)
          this.m_aShadedValues = new object[6];
        this.m_aShadedValues[5] = (object) value;
      }
    }

    internal void AdjustBordersForVerticalOrientation()
    {
      if (!(this.m_oBaseAppearance is IQBorderedAdvancedAppearance oBaseAppearance))
        return;
      this.ShowBorderTop = oBaseAppearance.ShowBorderLeft;
      this.ShowBorderRight = oBaseAppearance.ShowBorderTop;
      this.ShowBorderLeft = oBaseAppearance.ShowBorderBottom;
      this.ShowBorderBottom = oBaseAppearance.ShowBorderRight;
    }

    internal void ShowAllBorders(bool show) => this.ShowBorderBottom = this.ShowBorderLeft = this.ShowBorderRight = this.ShowBorderTop = this.ShowBorders = show;

    internal void ResetBorders()
    {
      if (this.m_aBorderedAdvancedValues != null)
        Array.Clear((Array) this.m_aBorderedAdvancedValues, 0, this.m_aBorderedAdvancedValues.Length);
      if (this.m_aBorderedValues == null)
        return;
      Array.Clear((Array) this.m_aBorderedValues, 0, this.m_aBorderedValues.Length);
    }

    internal void AdjustBackgroundOrientationsToVertical()
    {
      IQGradientAppearance oBaseAppearance1 = this.m_oBaseAppearance as IQGradientAppearance;
      IQMetallicAppearance oBaseAppearance2 = this.m_oBaseAppearance as IQMetallicAppearance;
      if (oBaseAppearance1 != null)
        this.GradientAngle = QMath.RotateAngle(oBaseAppearance1.GradientAngle, -90);
      if (oBaseAppearance2 == null)
        return;
      this.MetallicDirection = oBaseAppearance2.MetallicDirection == QMetallicAppearanceDirection.Horizontal ? QMetallicAppearanceDirection.Vertical : QMetallicAppearanceDirection.Horizontal;
    }

    internal void ResetBackgroundOrientations()
    {
      if (this.m_aGradientValues != null)
        this.m_aGradientValues[0] = (object) null;
      if (this.m_aMetallicValues == null)
        return;
      this.m_aMetallicValues[6] = (object) null;
    }
  }
}
