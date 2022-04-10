// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonPageComposite
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonPageComposite : QComposite
  {
    private bool m_bIsResizingParts;

    internal QRibbonPageComposite(
      IQCompositeContainer parentContainer,
      QCompositeConfiguration configuration,
      QColorScheme colorScheme)
      : base((IQPart) null, (QPartCollection) null, parentContainer, configuration, colorScheme)
    {
      this.PutPartName(nameof (QRibbonPageComposite));
    }

    public QRibbonPage RibbonPage => this.ParentContainer as QRibbonPage;

    public QRibbon Ribbon => this.RibbonPage == null ? (QRibbon) null : this.RibbonPage.Ribbon;

    protected override QColorScheme CreateChildCompositeColorScheme() => base.CreateChildCompositeColorScheme();

    protected override QCompositeConfiguration CreateChildCompositeConfiguration() => (QCompositeConfiguration) new QCompositeMenuConfiguration();

    protected override QCompositeWindowConfiguration CreateChildWindowConfiguration() => base.CreateChildWindowConfiguration();

    protected override QColorScheme CreateColorScheme() => (QColorScheme) null;

    public QRibbonPageCompositeConfiguration Configuration => base.Configuration as QRibbonPageCompositeConfiguration;

    private QRibbonPanelResizeBehavior GetFirstResizeBehavior() => QRibbonPanelResizeBehavior.HideHorizontalText;

    private QRibbonPanelResizeBehavior GetLastResizeBehavior() => QRibbonPanelResizeBehavior.Collapse;

    private QRibbonPanelResizeBehavior GetNextResizeBehavior(
      QRibbonPanelResizeBehavior behavior)
    {
      if (behavior == this.GetLastResizeBehavior())
        return this.GetLastResizeBehavior();
      return behavior == QRibbonPanelResizeBehavior.None ? this.GetFirstResizeBehavior() : (QRibbonPanelResizeBehavior) ((int) behavior << 1);
    }

    private QRibbonPanelResizeBehavior GetPreviousResizeBehavior(
      QRibbonPanelResizeBehavior behavior)
    {
      return behavior != QRibbonPanelResizeBehavior.None ? (QRibbonPanelResizeBehavior) ((int) behavior >> 1) : QRibbonPanelResizeBehavior.None;
    }

    private void ResizePartsWhenNeeded(
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      IQPartCollection parts = (IQPartCollection) this.Parts;
      if (parts == null || this.Properties.GetDirection((IQPart) this) != QPartDirection.Horizontal)
        return;
      Size unstretchedInnerSize = this.CalculatedProperties.UnstretchedInnerSize;
      Size innerSize = this.CalculatedProperties.InnerSize;
      if (innerSize.Width <= 0 || !this.IsVisible(layoutContext.VisibilitySelection))
        return;
      if (innerSize.Width < unstretchedInnerSize.Width)
      {
        QRibbonPanelResizeBehavior behavior = this.GetFirstResizeBehavior();
        int index = parts.Count - 1;
        while (index >= 0 && innerSize.Width < unstretchedInnerSize.Width)
        {
          IQPart part = parts[index];
          if (part is QRibbonPanel qribbonPanel && qribbonPanel.IsVisible(layoutContext.VisibilitySelection) && qribbonPanel.SupportsResizeBehavior(behavior) && !qribbonPanel.HasResizeBehaviorApplied(behavior) && qribbonPanel.ApplyResizeBehavior(behavior))
          {
            int width = part.CalculatedProperties.UnstretchedOuterSize.Width;
            part.LayoutEngine.CalculatePartSize(part, layoutContext);
            unstretchedInnerSize.Width -= width - part.CalculatedProperties.UnstretchedOuterSize.Width;
            part.CalculatedProperties.SetLayoutFlag(QPartLayoutFlags.WidthConstraintApplied, false);
            this.CalculatedProperties.SetLayoutFlag(QPartLayoutFlags.WidthConstraintApplied, false);
          }
          if (index == 0)
          {
            if (behavior != this.GetLastResizeBehavior())
            {
              index = parts.Count - 1;
              behavior = this.GetNextResizeBehavior(behavior);
            }
            else
              --index;
          }
          else
            --index;
        }
      }
      else
      {
        if (innerSize.Width < unstretchedInnerSize.Width)
          return;
        QRibbonPanelResizeBehavior behavior = this.GetLastResizeBehavior();
        int index = 0;
        while (index < parts.Count && innerSize.Width >= unstretchedInnerSize.Width)
        {
          IQPart part = parts[index];
          if (part is QRibbonPanel qribbonPanel && qribbonPanel.Visible && qribbonPanel.HasResizeBehaviorApplied(behavior))
          {
            Size availableSize = new Size(innerSize.Width - unstretchedInnerSize.Width, innerSize.Height);
            if (qribbonPanel.UnapplyResizeBehavior(behavior, availableSize, layoutContext))
            {
              int width = part.CalculatedProperties.UnstretchedOuterSize.Width;
              part.LayoutEngine.CalculatePartSize(part, layoutContext);
              unstretchedInnerSize.Width += part.CalculatedProperties.UnstretchedOuterSize.Width - width;
              part.CalculatedProperties.SetLayoutFlag(QPartLayoutFlags.WidthConstraintApplied, false);
              this.CalculatedProperties.SetLayoutFlag(QPartLayoutFlags.WidthConstraintApplied, false);
            }
            if (qribbonPanel.HasResizeBehaviorApplied(behavior))
              break;
          }
          if (index == parts.Count - 1)
          {
            if (behavior != this.GetFirstResizeBehavior())
            {
              behavior = this.GetPreviousResizeBehavior(behavior);
              index = 0;
            }
            else
              ++index;
          }
          else
            ++index;
        }
      }
    }

    protected internal override void UnfocusCurrentChildControl()
    {
      base.UnfocusCurrentChildControl();
      if (this.Ribbon == null || this.Ribbon.SelectedNavigationItem != null)
        return;
      this.Ribbon.LoseSimulatedFocus();
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      base.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
      if (this.m_bIsResizingParts || part != this || layoutStage != QPartLayoutStage.ConstraintsApplied)
        return;
      this.m_bIsResizingParts = true;
      this.ResizePartsWhenNeeded(layoutContext, additionalProperties);
      if (!this.CalculatedProperties.HasLayoutFlag(QPartLayoutFlags.WidthConstraintApplied | QPartLayoutFlags.HeightConstraintApplied))
        this.LayoutEngine.ApplyConstraints(this.CalculatedProperties.OuterSize, (IQPart) this, layoutContext, additionalProperties.ApplyConstraintProperties);
      this.m_bIsResizingParts = false;
    }
  }
}
