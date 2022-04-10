// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeItemControl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeItemControlDesigner), typeof (IDesigner))]
  public class QCompositeItemControl : QCompositeItemBase, IQCompositeItemControl
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Control m_oControl;
    private Size m_oLastControlSize;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bPaintingControl;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bControlSuspended;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakEventConsumerCollection m_oEventConsumers;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bChangingControlParent;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bChangingControlSize;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bChangingControlProperties;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bSettingControlClip;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private object m_oLastSetRegionBounds;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private object m_oProposedScrollControlBounds;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Region m_oLastTriedRegion;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Region m_oLastSetRegion;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Bitmap m_oScrollBitmap;

    protected QCompositeItemControl(object sourceObject, QObjectClonerConstructOptions options)
      : base(sourceObject, options)
    {
      this.InternalConstruct();
    }

    public QCompositeItemControl()
      : base(QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct();
    }

    internal QCompositeItemControl(QCompositeItemCreationOptions options)
      : base(options)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.PutContentObject((object) Size.Empty);
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
    }

    [Category("QBehavior")]
    [Description("Gets or sets a possible Control that is displayed on this item.")]
    [DefaultValue(null)]
    public virtual Control Control
    {
      get => this.m_oControl;
      set => this.SetControl(value, true);
    }

    [Category("QBehavior")]
    [DefaultValue(typeof (Size), "0,0")]
    [Description("Gets or sets the prefered size of the Control")]
    public Size ControlSize
    {
      get => (Size) this.ContentObject;
      set => this.SetControlSize(value, true);
    }

    public void SetControlSize(Size size, bool notifyChange)
    {
      this.PutContentObject((object) size);
      if (!notifyChange)
        return;
      this.HandleChange(true);
    }

    public void SetControl(Control value, bool notifyParent)
    {
      if (this.m_oControl == value)
        return;
      if (this.m_oControl != null)
      {
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_Disposed));
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_SizeChanged));
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_VisibleChanged));
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_ParentChanged));
        this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_GotFocus));
        this.m_oControl.Parent = (Control) null;
        this.m_oControl.Size = this.ControlSize;
        this.m_oLastControlSize = Size.Empty;
        this.m_oControl.Region = (Region) null;
        this.m_oLastSetRegion = (Region) null;
        this.m_oLastTriedRegion = (Region) null;
        this.m_oLastSetRegionBounds = (object) null;
        this.m_oProposedScrollControlBounds = (object) null;
        this.SetControlSize(Size.Empty, false);
      }
      this.m_oControl = value;
      if (this.m_oControl != null)
      {
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_Disposed), (object) this.m_oControl, "Disposed"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_SizeChanged), (object) this.m_oControl, "SizeChanged"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_ParentChanged), (object) this.m_oControl, "ParentChanged"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_VisibleChanged), (object) this.m_oControl, "VisibleChanged"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_GotFocus), (object) this.m_oControl, "GotFocus"));
        this.m_oLastControlSize = this.m_oControl.Size;
        this.SetControlSize(this.m_oControl.Size, false);
      }
      if (notifyParent)
        this.HandleChange(true);
      this.SetControlParent();
    }

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      return currentPainters;
    }

    protected override void SetDisplayParent(IQManagedLayoutParent displayParent)
    {
      base.SetDisplayParent(displayParent);
      this.SetControlParent();
    }

    [Description("Contains the Configuration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QCompositeItemControlConfiguration Configuration
    {
      get => base.Configuration as QCompositeItemControlConfiguration;
      set => this.Configuration = value;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeItemControlConfiguration();

    public override void MoveUnclonablesToClone()
    {
      base.MoveUnclonablesToClone();
      QCompositeItemControl lastClonedItem = this.LastClonedItem as QCompositeItemControl;
      if (this.m_oControl == null || lastClonedItem == null)
        return;
      this.m_bControlSuspended = true;
      NativeMethods.SetWindowRgn(this.m_oControl.Handle, IntPtr.Zero, 0);
      this.m_oLastSetRegion = (Region) null;
      this.m_oLastTriedRegion = (Region) null;
      lastClonedItem.SetControl(this.m_oControl, false);
    }

    public override void RestoreUnclonablesFromClone()
    {
      base.RestoreUnclonablesFromClone();
      QCompositeItemControl lastClonedItem = this.LastClonedItem as QCompositeItemControl;
      if (this.m_oControl == null || lastClonedItem == null)
        return;
      lastClonedItem.SetControl((Control) null, false);
      this.m_bControlSuspended = false;
      this.SetControlParent();
    }

    protected override IList AssociatedComponents
    {
      get
      {
        IList associatedComponents = base.AssociatedComponents;
        if (this.Control != null)
          associatedComponents.Add((object) this.Control);
        return associatedComponents;
      }
    }

    internal void SetControlParent()
    {
      if (this.m_bControlSuspended)
        return;
      Control parentControl = this.ParentControl;
      if (this.m_oControl == null || this.m_oControl.Parent == parentControl)
        return;
      Control parent = this.m_oControl.Parent;
      parent?.SuspendLayout();
      parentControl?.SuspendLayout();
      this.m_bChangingControlProperties = true;
      this.m_oControl.Visible = false;
      this.m_bChangingControlProperties = false;
      this.m_bChangingControlSize = true;
      this.m_oControl.Size = this.ControlSize;
      this.m_bChangingControlSize = false;
      this.m_bChangingControlParent = true;
      QControlHelper.PatchActiveControlRemoval(parent as ContainerControl);
      this.m_oControl.Parent = parentControl;
      this.m_bChangingControlParent = false;
      parent?.ResumeLayout(false);
      parentControl?.ResumeLayout(false);
      this.SetControlVisibility(QTristateBool.Undefined);
    }

    internal void SetControlVisibility(QTristateBool shouldBeVisible)
    {
      if (this.m_bControlSuspended || this.m_oControl == null)
        return;
      bool flag1 = shouldBeVisible != QTristateBool.Undefined ? shouldBeVisible == QTristateBool.True : QPartHelper.IsPartHierarchyVisible((IQPart) this, QPartVisibilitySelectionTypes.IncludeAll);
      bool flag2;
      if (!this.m_oControl.IsHandleCreated && this.m_oControl.Parent != null)
      {
        this.m_oControl.Parent.SuspendLayout();
        flag2 = NativeHelper.IsWindowVisible(this.m_oControl, true);
        this.m_oControl.Parent.ResumeLayout(false);
      }
      else
        flag2 = NativeHelper.IsWindowVisible(this.m_oControl, true);
      if (flag2 == flag1)
        return;
      this.m_oControl.Parent?.SuspendLayout();
      this.m_bChangingControlProperties = true;
      this.m_oControl.Visible = flag1;
      this.m_bChangingControlProperties = false;
      this.m_oControl.Parent?.ResumeLayout(false);
    }

    internal bool SetControlClip(Rectangle proposedControlBounds)
    {
      if (this.m_oControl == null || this.m_bSettingControlClip)
        return false;
      this.m_bSettingControlClip = true;
      bool flag = false;
      Region parentClipRegion = (Region) this.CalculatedProperties.CachedParentClipRegion;
      if (parentClipRegion != this.m_oLastTriedRegion)
      {
        this.m_oLastTriedRegion = parentClipRegion;
        Region region = parentClipRegion?.Clone();
        if (region != null)
        {
          region.Intersect(proposedControlBounds);
          region.Translate(-proposedControlBounds.Left, -proposedControlBounds.Top);
          using (Graphics graphics = this.m_oControl.CreateGraphics())
          {
            RectangleF bounds = region.GetBounds(graphics);
            Rectangle rect = new Rectangle(Point.Empty, proposedControlBounds.Size);
            if (!bounds.Contains((RectangleF) rect))
            {
              if (this.m_oLastSetRegionBounds != null)
              {
                if (!(bounds != (RectangleF) this.m_oLastSetRegionBounds))
                  goto label_16;
              }
              this.m_oLastSetRegionBounds = (object) bounds;
              this.m_oLastSetRegion = region;
              NativeMethods.SetWindowRgn(this.m_oControl.Handle, region.GetHrgn(graphics), 0);
              flag = true;
            }
            else if (this.m_oLastSetRegion != null)
            {
              NativeMethods.SetWindowRgn(this.m_oControl.Handle, IntPtr.Zero, 0);
              this.m_oLastSetRegion = (Region) null;
              this.m_oLastSetRegionBounds = (object) null;
              flag = true;
            }
          }
        }
        else if (this.m_oLastSetRegion != null)
        {
          NativeMethods.SetWindowRgn(this.m_oControl.Handle, IntPtr.Zero, 0);
          this.m_oLastSetRegion = (Region) null;
          this.m_oLastSetRegionBounds = (object) null;
          flag = true;
        }
      }
label_16:
      this.m_bSettingControlClip = false;
      return flag;
    }

    internal void SetControlBounds(Rectangle bounds)
    {
      if (this.m_bControlSuspended || this.m_oControl == null)
        return;
      this.m_oControl.Parent?.SuspendLayout();
      this.m_bChangingControlSize = true;
      this.m_oControl.Bounds = bounds;
      this.m_bChangingControlSize = false;
      this.m_oControl.Parent?.ResumeLayout(false);
    }

    internal void SetControlEnabledState()
    {
      if (this.m_bControlSuspended || this.m_oControl == null)
        return;
      this.m_bChangingControlProperties = true;
      this.m_oControl.Enabled = !QItemStatesHelper.IsDisabled(this.ItemState);
      this.m_bChangingControlProperties = false;
    }

    internal void RedrawControl()
    {
      if (this.m_oControl == null || this.m_bPaintingControl)
        return;
      this.m_bPaintingControl = true;
      NativeMethods.RedrawWindow(this.m_oControl.Handle, IntPtr.Zero, IntPtr.Zero, 1285);
      this.m_bPaintingControl = false;
    }

    internal void CreateControlScrollBitmap()
    {
      if (this.m_oScrollBitmap != null)
      {
        this.m_oScrollBitmap.Dispose();
        this.m_oScrollBitmap = (Bitmap) null;
      }
      if (this.m_oControl == null)
        return;
      this.m_oScrollBitmap = new Bitmap(this.m_oControl.Width, this.m_oControl.Height);
      Graphics graphics = Graphics.FromImage((Image) this.m_oScrollBitmap);
      IntPtr hdc = graphics.GetHdc();
      NativeMethods.SendMessage(this.m_oControl.Handle, 791, hdc, new IntPtr(22));
      graphics.ReleaseHdc(hdc);
      graphics.Dispose();
    }

    protected internal override void HandleAncestorEnabledChanged()
    {
      base.HandleAncestorEnabledChanged();
      this.SetControlEnabledState();
    }

    protected internal override void HandleAncestorVisibilityChanged()
    {
      base.HandleAncestorVisibilityChanged();
      this.SetControlVisibility(QTristateBool.Undefined);
    }

    protected internal override void HandleScrollingStage(
      IQScrollablePart scrollingPart,
      QScrollablePartScrollStage stage)
    {
      base.HandleScrollingStage(scrollingPart, stage);
      if (this.m_oControl == null || this.m_bControlSuspended || !this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
        return;
      switch (stage)
      {
        case QScrollablePartScrollStage.ScrollingStarted:
          if (!this.Configuration.ScrollWithImage || !this.m_oControl.Visible)
            break;
          this.CreateControlScrollBitmap();
          this.m_bChangingControlProperties = true;
          this.m_oControl.Parent.SuspendLayout();
          this.m_oControl.Visible = false;
          this.m_oControl.Parent.ResumeLayout(false);
          this.m_bChangingControlProperties = false;
          break;
        case QScrollablePartScrollStage.Scrolling:
          if (this.m_oScrollBitmap != null)
            break;
          this.m_oProposedScrollControlBounds = (object) this.CalculatedProperties.CachedScrollCorrectedInnerBounds;
          break;
        case QScrollablePartScrollStage.ScrollingEnded:
          if (this.m_oScrollBitmap == null)
            break;
          this.m_oScrollBitmap.Dispose();
          this.m_oScrollBitmap = (Bitmap) null;
          this.m_oProposedScrollControlBounds = (object) this.CalculatedProperties.CachedScrollCorrectedInnerBounds;
          break;
      }
    }

    private void Control_Disposed(object sender, EventArgs e) => this.Control = (Control) null;

    private void Control_SizeChanged(object sender, EventArgs e)
    {
      if (!this.m_bChangingControlSize && !this.ParentControlIsInitializing && !this.IsDeserializingFromCode)
      {
        Size controlSize = this.ControlSize;
        if (this.m_oLastControlSize.Width != this.m_oControl.Width)
          controlSize.Width = this.m_oControl.Width;
        if (this.m_oLastControlSize.Height != this.m_oControl.Height)
          controlSize.Height = this.m_oControl.Height;
        this.SetControlSize(controlSize, false);
        if (!this.m_bControlSuspended)
          this.HandleChange(true);
      }
      this.m_oLastControlSize = this.m_oControl.Size;
    }

    private void Control_ParentChanged(object sender, EventArgs e)
    {
      if (this.m_bControlSuspended || this.m_bChangingControlParent || this.m_oControl.Parent == this.ParentControl || this.m_oControl.Site != null || this.m_oControl.Parent == null)
        return;
      this.Control = (Control) null;
    }

    private void Control_VisibleChanged(object sender, EventArgs e)
    {
      if (this.m_bControlSuspended || this.m_bChangingControlProperties)
        return;
      this.SetControlVisibility(QTristateBool.Undefined);
    }

    private void Control_GotFocus(object sender, EventArgs e) => this.ScrollIntoView(QTristateBool.False);

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part != this || this.m_bControlSuspended)
        return;
      switch (layoutStage)
      {
        case QPartLayoutStage.PreparingForLayout:
          if (QPartHelper.IsPartHierarchyVisible((IQPart) this, QPartVisibilitySelectionTypes.IncludeAll))
            break;
          this.SetControlVisibility(QTristateBool.False);
          break;
        case QPartLayoutStage.BoundsCalculated:
          this.SetControlBounds(this.CalculatedProperties.CachedScrollCorrectedInnerBounds);
          this.SetControlVisibility(QTristateBool.Undefined);
          break;
      }
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet = base.HandlePaintStage(part, paintStage, paintContext);
      if (part == this && !this.m_bControlSuspended)
      {
        if (paintStage == QPartPaintStage.PaintingContent && this.m_oScrollBitmap != null)
          paintContext.Graphics.DrawImageUnscaled((Image) this.m_oScrollBitmap, this.CalculatedProperties.Bounds);
        else if (paintStage == QPartPaintStage.PaintFinished && this.IsVisible(paintContext.VisibilitySelection) && this.m_oControl != null && this.m_oScrollBitmap == null)
        {
          if (this.m_oProposedScrollControlBounds != null)
          {
            this.SetControlClip((Rectangle) this.m_oProposedScrollControlBounds);
            this.SetControlBounds((Rectangle) this.m_oProposedScrollControlBounds);
            this.SetControlVisibility(QTristateBool.Undefined);
            this.m_oProposedScrollControlBounds = (object) null;
          }
          else
            this.SetControlClip(this.m_oControl.Bounds);
          this.RedrawControl();
        }
      }
      return qcolorSet;
    }
  }
}
