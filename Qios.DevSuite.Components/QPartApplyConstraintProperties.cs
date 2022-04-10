// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartApplyConstraintProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartApplyConstraintProperties
  {
    private bool m_bCalledFromRoot;
    private bool m_bDoOneLoopSimple;
    private QPartDirection m_eCollectionDirection;
    private QPartOptions m_eOptions;
    private Size m_oNewSize = Size.Empty;
    private Size m_oNewUnstretchedSize = Size.Empty;

    public QPartApplyConstraintProperties()
    {
    }

    public QPartApplyConstraintProperties(bool calledFromRoot) => this.m_bCalledFromRoot = calledFromRoot;

    public QPartApplyConstraintProperties(
      bool calledFromRoot,
      QPartDirection collectionDirection,
      QPartOptions options)
    {
      this.m_bCalledFromRoot = calledFromRoot;
      this.m_eCollectionDirection = collectionDirection;
      this.m_eOptions = options;
    }

    public bool DoOneLoopSimple
    {
      get => this.m_bDoOneLoopSimple;
      set => this.m_bDoOneLoopSimple = value;
    }

    public bool CalledFromRoot => this.m_bCalledFromRoot;

    public QPartOptions Options => this.m_eOptions;

    public bool ShouldStretchHorizontal => (this.m_eOptions & QPartOptions.StretchHorizontal) != QPartOptions.None;

    public bool ShouldStretchVertical => (this.m_eOptions & QPartOptions.StretchVertical) != QPartOptions.None;

    public bool ShouldStretch(bool horizontal) => !horizontal ? (this.m_eOptions & QPartOptions.StretchVertical) != QPartOptions.None : (this.m_eOptions & QPartOptions.StretchHorizontal) != QPartOptions.None;

    public bool ShouldStretch(QPartDirection direction) => direction != QPartDirection.Horizontal ? (this.m_eOptions & QPartOptions.StretchVertical) != QPartOptions.None : (this.m_eOptions & QPartOptions.StretchHorizontal) != QPartOptions.None;

    public bool ShouldShrinkHorizontal => (this.m_eOptions & QPartOptions.ShrinkHorizontal) != QPartOptions.None;

    public bool ShouldShrinkVertical => (this.m_eOptions & QPartOptions.ShrinkVertical) != QPartOptions.None;

    public bool ShouldShrink(bool horizontal) => !horizontal ? (this.m_eOptions & QPartOptions.ShrinkVertical) != QPartOptions.None : (this.m_eOptions & QPartOptions.ShrinkHorizontal) != QPartOptions.None;

    public bool ShouldShrink(QPartDirection direction) => direction != QPartDirection.Horizontal ? (this.m_eOptions & QPartOptions.ShrinkVertical) != QPartOptions.None : (this.m_eOptions & QPartOptions.ShrinkHorizontal) != QPartOptions.None;

    public QPartDirection CollectionDirection => this.m_eCollectionDirection;

    public Size NewSize
    {
      get => this.m_oNewSize;
      set => this.m_oNewSize = value;
    }

    public Size NewUnstretchedSize
    {
      get => this.m_oNewUnstretchedSize;
      set => this.m_oNewUnstretchedSize = value;
    }
  }
}
