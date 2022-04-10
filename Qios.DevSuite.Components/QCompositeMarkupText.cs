// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMarkupText
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeMarkupTextDesigner), typeof (IDesigner))]
  public class QCompositeMarkupText : QCompositeItemBase
  {
    private Cursor m_oCursor;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oElementMouseEnterDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oElementMouseLeaveDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oElementMouseDownDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oElementMouseUpDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oElementMouseClickDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oElementLinkClickDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bSettingMarkupTextProperties;

    protected QCompositeMarkupText(object sourceObject, QObjectClonerConstructOptions options)
      : base(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeMarkupText()
      : base(QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct();
    }

    public QCompositeMarkupText(QCompositeItemCreationOptions options)
      : base(options)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.m_oCursor = Cursors.Default;
      this.PutContentObject((object) new QMarkupText(this.ColorScheme, (Font) null, (Color) this.ColorScheme.MarkupText));
      this.MarkupTextObject.Configuration.CanFocus = false;
      this.AttachEvents();
    }

    private void AttachEvents()
    {
      this.MarkupTextObject.UpdateRequested += new QCommandUIRequestEventHandler(this.MarkupTextObject_UpdateRequested);
      this.MarkupTextObject.Root.MouseEnter += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseEnter);
      this.MarkupTextObject.Root.MouseLeave += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseLeave);
      this.MarkupTextObject.Root.MouseDown += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseDown);
      this.MarkupTextObject.Root.MouseUp += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseUp);
      this.MarkupTextObject.Root.MouseClick += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseClick);
      this.MarkupTextObject.Root.LinkClick += new QMarkupTextElementEventHandler(this.MarkupTextRoot_LinkClick);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the mouse enters an element.")]
    public event QMarkupTextElementEventHandler ElementMouseEnter
    {
      add => this.m_oElementMouseEnterDelegate = QWeakDelegate.Combine(this.m_oElementMouseEnterDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseEnterDelegate = QWeakDelegate.Remove(this.m_oElementMouseEnterDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the mouse leaves this element.")]
    public event QMarkupTextElementEventHandler ElementMouseLeave
    {
      add => this.m_oElementMouseLeaveDelegate = QWeakDelegate.Combine(this.m_oElementMouseLeaveDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseLeaveDelegate = QWeakDelegate.Remove(this.m_oElementMouseLeaveDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user presses the mousebutton on this element.")]
    public event QMarkupTextElementEventHandler ElementMouseDown
    {
      add => this.m_oElementMouseDownDelegate = QWeakDelegate.Combine(this.m_oElementMouseDownDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseDownDelegate = QWeakDelegate.Remove(this.m_oElementMouseDownDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user releases the mousebutton on this element.")]
    public event QMarkupTextElementEventHandler ElementMouseUp
    {
      add => this.m_oElementMouseUpDelegate = QWeakDelegate.Combine(this.m_oElementMouseUpDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseUpDelegate = QWeakDelegate.Remove(this.m_oElementMouseUpDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Occurs when the user clicks on this element.")]
    [QWeakEvent]
    public event QMarkupTextElementEventHandler ElementMouseClick
    {
      add => this.m_oElementMouseClickDelegate = QWeakDelegate.Combine(this.m_oElementMouseClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseClickDelegate = QWeakDelegate.Remove(this.m_oElementMouseClickDelegate, (Delegate) value);
    }

    [Description("Occurs when the user clicks on a link.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QMarkupTextElementEventHandler ElementLinkClick
    {
      add => this.m_oElementLinkClickDelegate = QWeakDelegate.Combine(this.m_oElementLinkClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementLinkClickDelegate = QWeakDelegate.Remove(this.m_oElementLinkClickDelegate, (Delegate) value);
    }

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = base.CreatePainters(currentPainters);
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Content, (IQPartObjectPainter) new QPartMarkupTextPainter());
      return currentPainters;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeMarkupTextConfiguration();

    public override Cursor Cursor
    {
      get => this.m_oCursor;
      set => this.m_oCursor = value;
    }

    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.MarkupText)]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [Category("QAppearance")]
    [Description("Contains the Configuration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeMarkupTextConfiguration Configuration
    {
      get => base.Configuration as QCompositeMarkupTextConfiguration;
      set => this.Configuration = (QContentPartConfiguration) value;
    }

    [Localizable(true)]
    [DefaultValue("")]
    [QDesignerMainText(true)]
    [Category("QAppearance")]
    [Description("Gets or sets the Markup text for this QCompositeMarkupText. The formatting happens in XML.")]
    public string MarkupText
    {
      get => this.MarkupTextObject.MarkupText;
      set => this.MarkupTextObject.MarkupText = value != null ? value : string.Empty;
    }

    [Description("Gets the collection with custom styles. This can be used to handle custom tags or handle default tags different than the regular behavior.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QBehavior")]
    public QMarkupTextStyleCollection CustomStyles => this.MarkupTextObject.CustomStyles;

    [Browsable(false)]
    public QMarkupText MarkupTextObject => this.ContentObject as QMarkupText;

    public override IQPartLayoutEngine LayoutEngine
    {
      get => base.LayoutEngine == QPartLinearLayoutEngine.Default ? (IQPartLayoutEngine) QPartMarkupTextLayoutEngine.Default : base.LayoutEngine;
      set => base.LayoutEngine = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeMarkupText OriginalItem => base.OriginalItem as QCompositeMarkupText;

    private MouseEventArgs CreateScrollCorrectedMouseEventArgs(MouseEventArgs e)
    {
      Point point = new Point(e.X, e.Y);
      point.Offset(-this.CalculatedProperties.CachedScrollOffset.X, -this.CalculatedProperties.CachedScrollOffset.Y);
      return new MouseEventArgs(e.Button, e.Clicks, point.X, point.Y, e.Delta);
    }

    public override QPartHitTestResult HitTest(int x, int y)
    {
      QPartHitTestResult qpartHitTestResult = base.HitTest(x, y);
      if (qpartHitTestResult == QPartHitTestResult.Bounds)
      {
        QMarkupTextElement qmarkupTextElement = this.MarkupTextObject.Root.GetElementThatContainsAbsolutePoint((PointF) new Point(x, y));
        if (qmarkupTextElement != null)
          qmarkupTextElement = qmarkupTextElement.GetSourceForEvents();
        if (qmarkupTextElement != null && qmarkupTextElement.ReapplyAttributesOnStateChange)
          qpartHitTestResult = QPartHitTestResult.BoundsCustom;
      }
      return qpartHitTestResult;
    }

    protected internal override void HandleAncestorEnabledChanged()
    {
      base.HandleAncestorEnabledChanged();
      bool flag = !QItemStatesHelper.IsDisabled(this.ItemState);
      if (this.MarkupTextObject.Enabled == flag)
        return;
      this.MarkupTextObject.Enabled = flag;
    }

    protected internal override bool HandleMouseMove(MouseEventArgs e)
    {
      base.HandleMouseMove(e);
      if (!this.IsEnabled || !this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
        return true;
      MouseEventArgs correctedMouseEventArgs = this.CreateScrollCorrectedMouseEventArgs(e);
      switch (this.HitTest(correctedMouseEventArgs.X, correctedMouseEventArgs.Y))
      {
        case QPartHitTestResult.Bounds:
        case QPartHitTestResult.BoundsCustom:
          this.MarkupTextObject.HandleMouseMove(this.CreateScrollCorrectedMouseEventArgs(e));
          if (this.MarkupTextObject.HotElement != null && this.MarkupTextObject.HotElement.IsOrHasParentOfType(typeof (QMarkupTextElementAnchor)))
          {
            this.Cursor = Cursors.Hand;
            break;
          }
          this.Cursor = Cursors.Default;
          break;
        default:
          this.Cursor = Cursors.Default;
          this.MarkupTextObject.HandleMouseLeave();
          break;
      }
      return true;
    }

    protected internal override bool HandleMouseDown(MouseEventArgs e)
    {
      base.HandleMouseDown(e);
      if (!this.IsEnabled || !this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
        return true;
      this.MarkupTextObject.HandleMouseDown(this.CreateScrollCorrectedMouseEventArgs(e));
      return true;
    }

    protected internal override bool HandleMouseUp(MouseEventArgs e)
    {
      base.HandleMouseUp(e);
      if (!this.Enabled || !this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
        return true;
      this.MarkupTextObject.HandleMouseUp(this.CreateScrollCorrectedMouseEventArgs(e));
      return true;
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part != this)
        return;
      switch (layoutStage)
      {
        case QPartLayoutStage.CalculatingSize:
          this.m_bSettingMarkupTextProperties = true;
          this.MarkupTextObject.SetProperties(this.Configuration.FontDefinition.GetFontFromCache(layoutContext.Font), this.ColorScheme, (Color) this.ColorScheme.MarkupText);
          this.m_bSettingMarkupTextProperties = false;
          break;
        case QPartLayoutStage.BoundsCalculated:
          this.MarkupTextObject.PutBounds(this.CalculatedProperties.InnerBounds);
          break;
      }
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet1 = (QColorSet) null;
      if (part == this && paintStage != QPartPaintStage.PaintingContent)
        ;
      QColorSet qcolorSet2 = base.HandlePaintStage(part, paintStage, paintContext);
      return qcolorSet1 ?? qcolorSet2;
    }

    public override object Clone()
    {
      QCompositeMarkupText qcompositeMarkupText = base.Clone() as QCompositeMarkupText;
      qcompositeMarkupText.AttachEvents();
      return (object) qcompositeMarkupText;
    }

    private void MarkupTextObject_UpdateRequested(object sender, QCommandUIRequestEventArgs e)
    {
      if (this.m_bSettingMarkupTextProperties)
        return;
      this.HandleChange(e.Request == QCommandUIRequest.PerformLayout);
    }

    private void MarkupTextRoot_MouseEnter(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseEnter(e);

    private void MarkupTextRoot_MouseLeave(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseLeave(e);

    private void MarkupTextRoot_MouseDown(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseDown(e);

    private void MarkupTextRoot_MouseUp(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseUp(e);

    private void MarkupTextRoot_MouseClick(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseClick(e);

    private void MarkupTextRoot_LinkClick(object sender, QMarkupTextElementEventArgs e) => this.OnElementLinkClick(e);

    protected virtual void OnElementMouseEnter(QMarkupTextElementEventArgs e)
    {
      QCompositeMarkupText qcompositeMarkupText = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeMarkupText) null;
      if (qcompositeMarkupText != null)
        qcompositeMarkupText.m_oElementMouseEnterDelegate = QWeakDelegate.InvokeDelegate(qcompositeMarkupText.m_oElementMouseEnterDelegate, (object) this, (object) e);
      this.m_oElementMouseEnterDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseEnterDelegate, (object) this, (object) e);
    }

    protected virtual void OnElementMouseLeave(QMarkupTextElementEventArgs e)
    {
      QCompositeMarkupText qcompositeMarkupText = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeMarkupText) null;
      if (qcompositeMarkupText != null)
        qcompositeMarkupText.m_oElementMouseLeaveDelegate = QWeakDelegate.InvokeDelegate(qcompositeMarkupText.m_oElementMouseLeaveDelegate, (object) this, (object) e);
      this.m_oElementMouseLeaveDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseLeaveDelegate, (object) this, (object) e);
    }

    protected virtual void OnElementMouseDown(QMarkupTextElementEventArgs e)
    {
      QCompositeMarkupText qcompositeMarkupText = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeMarkupText) null;
      if (qcompositeMarkupText != null)
        qcompositeMarkupText.m_oElementMouseDownDelegate = QWeakDelegate.InvokeDelegate(qcompositeMarkupText.m_oElementMouseDownDelegate, (object) this, (object) e);
      this.m_oElementMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseDownDelegate, (object) this, (object) e);
    }

    protected virtual void OnElementMouseUp(QMarkupTextElementEventArgs e)
    {
      QCompositeMarkupText qcompositeMarkupText = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeMarkupText) null;
      if (qcompositeMarkupText != null)
        qcompositeMarkupText.m_oElementMouseUpDelegate = QWeakDelegate.InvokeDelegate(qcompositeMarkupText.m_oElementMouseUpDelegate, (object) this, (object) e);
      this.m_oElementMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseUpDelegate, (object) this, (object) e);
    }

    protected virtual void OnElementMouseClick(QMarkupTextElementEventArgs e)
    {
      QCompositeMarkupText qcompositeMarkupText = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeMarkupText) null;
      if (qcompositeMarkupText != null)
        qcompositeMarkupText.m_oElementMouseClickDelegate = QWeakDelegate.InvokeDelegate(qcompositeMarkupText.m_oElementMouseClickDelegate, (object) this, (object) e);
      this.m_oElementMouseClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseClickDelegate, (object) this, (object) e);
    }

    protected virtual void OnElementLinkClick(QMarkupTextElementEventArgs e)
    {
      QCompositeMarkupText qcompositeMarkupText = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeMarkupText) null;
      if (qcompositeMarkupText != null)
        qcompositeMarkupText.m_oElementLinkClickDelegate = QWeakDelegate.InvokeDelegate(qcompositeMarkupText.m_oElementLinkClickDelegate, (object) this, (object) e);
      this.m_oElementLinkClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementLinkClickDelegate, (object) this, (object) e);
    }
  }
}
