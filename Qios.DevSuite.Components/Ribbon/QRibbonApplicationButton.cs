// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonApplicationButton
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [Designer(typeof (QRibbonApplicationButtonDesigner), typeof (IDesigner))]
  [ToolboxItem(false)]
  [DesignTimeVisible(true)]
  [TypeConverter(typeof (ComponentConverter))]
  public class QRibbonApplicationButton : QCompositeItem
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QTranslucentWindow m_oOverlay;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Control m_oCurrentOverlayOwner;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonApplicationButton.QOverlayOwnerSub m_oOverlayOwnerSub;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bPaintingOverlay;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bShowOverlayOnFinish;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakEventConsumerCollection m_oWeakEvents;
    private static Image m_oDefaultBackgroundImage;
    private static Image m_oDefaultBackgroundImageHot;
    private static Image m_oDefaultBackgroundImagePressed;
    private Image m_oBackgroundImage;
    private Image m_oBackgroundImageHot;
    private Image m_oBackgroundImagePressed;
    private Image m_oForegroundImage;

    public QRibbonApplicationButton()
      : base(QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      if (QRibbonApplicationButton.m_oDefaultBackgroundImage == null)
      {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        QRibbonApplicationButton.m_oDefaultBackgroundImage = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.QRibbonApplicationButton.png"));
        QRibbonApplicationButton.m_oDefaultBackgroundImageHot = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.QRibbonApplicationButtonHot.png"));
        QRibbonApplicationButton.m_oDefaultBackgroundImagePressed = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.QRibbonApplicationButtonPressed.png"));
      }
      this.HotkeyText = "F";
      this.m_oWeakEvents = new QWeakEventConsumerCollection();
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QRibbonApplicationButtonConfiguration();

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Background, (IQPartObjectPainter) new QPartImagePainter());
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Foreground, (IQPartObjectPainter) new QPartImagePainter());
      currentPainters = QPartObjectPainter.AddObjectPainter(currentPainters, QPartPaintLayer.Background, (IQPartObjectPainter) new QPartApplicationButtonPainter());
      return currentPainters;
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonApplicationButton)]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [DefaultValue("F")]
    public override string HotkeyText
    {
      get => base.HotkeyText;
      set => base.HotkeyText = value;
    }

    [Category("QAppearance")]
    [Description("Contains the Configuration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonApplicationButtonConfiguration Configuration
    {
      get => base.Configuration as QRibbonApplicationButtonConfiguration;
      set => this.Configuration = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QPartCollection Items => base.Items;

    [Description("Gets or sets the foreground image to draw before the background.")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    public Image ForegroundImage
    {
      get => this.m_oForegroundImage;
      set
      {
        this.m_oForegroundImage = value;
        this.HandleChange(true);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the background image to draw behind the Icon.")]
    public Image BackgroundImage
    {
      get => this.m_oBackgroundImage;
      set
      {
        this.m_oBackgroundImage = value;
        this.HandleChange(true);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the background image to draw behind the ApplicationImage when the button is hot.")]
    [DefaultValue(null)]
    public Image BackgroundImageHot
    {
      get => this.m_oBackgroundImageHot;
      set
      {
        this.m_oBackgroundImageHot = value;
        this.HandleChange(true);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the background image to draw behind the ApplicationImage when the button is pressed.")]
    public Image BackgroundImagePressed
    {
      get => this.m_oBackgroundImagePressed;
      set
      {
        this.m_oBackgroundImagePressed = value;
        this.HandleChange(true);
      }
    }

    [Browsable(false)]
    public Image UsedBackgroundImage => this.BackgroundImage == null ? QRibbonApplicationButton.m_oDefaultBackgroundImage : this.BackgroundImage;

    [Browsable(false)]
    public Image UsedBackgroundImageHot => this.BackgroundImageHot == null ? QRibbonApplicationButton.m_oDefaultBackgroundImageHot : this.BackgroundImageHot;

    [Browsable(false)]
    public Image UsedBackgroundImagePressed => this.BackgroundImagePressed == null ? QRibbonApplicationButton.m_oDefaultBackgroundImagePressed : this.BackgroundImagePressed;

    private Image GetUsedImageBasedOnState(QItemStates itemState)
    {
      if (QItemStatesHelper.IsDisabled(itemState))
        return this.UsedBackgroundImage;
      if (QItemStatesHelper.IsPressed(itemState) || QItemStatesHelper.IsExpanded(itemState))
        return this.UsedBackgroundImagePressed;
      return QItemStatesHelper.IsHot(itemState) ? this.UsedBackgroundImageHot : this.UsedBackgroundImage;
    }

    private Size GetLargestImageSize()
    {
      Size empty = Size.Empty;
      empty.Height = Math.Max(empty.Height, this.UsedBackgroundImage.Height);
      empty.Height = Math.Max(empty.Height, this.UsedBackgroundImageHot.Height);
      empty.Height = Math.Max(empty.Height, this.UsedBackgroundImagePressed.Height);
      empty.Width = Math.Max(empty.Height, this.UsedBackgroundImage.Width);
      empty.Width = Math.Max(empty.Height, this.UsedBackgroundImageHot.Width);
      empty.Width = Math.Max(empty.Height, this.UsedBackgroundImagePressed.Width);
      return empty;
    }

    [Browsable(false)]
    public QTranslucentWindow OverlayWindow => this.m_oOverlay;

    [Browsable(false)]
    public bool OverlayVisible => this.m_oOverlay != null && this.m_oOverlay.Visible;

    protected override void SetDisplayParent(IQManagedLayoutParent displayParent)
    {
      if (this.ParentControl != null)
      {
        this.m_oWeakEvents.DetachAndRemove((Delegate) new EventHandler(this.DisplayParent_VisibleChanged));
        this.DetachToolTip();
      }
      base.SetDisplayParent(displayParent);
      if (this.ParentControl != null)
        this.m_oWeakEvents.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.DisplayParent_VisibleChanged), (object) this.ParentControl, "VisibleChanged"));
      this.SynchronizeOverlay();
      this.AttachToolTip();
    }

    private void AttachToolTip()
    {
      if (!(this.ParentControl is QControl parentControl) || this.m_oOverlay == null)
        return;
      parentControl.AddAdditionalToolTipControl((Control) this.OverlayWindow);
    }

    private void DetachToolTip()
    {
      if (!(this.ParentControl is QControl parentControl) || this.m_oOverlay == null)
        return;
      parentControl.RemoveAdditionalToolTipControl((Control) this.OverlayWindow);
    }

    private Control FindOverlayOwner() => this.ParentControl == null ? (Control) null : this.ParentControl.TopLevelControl;

    private void CleanupOverlay()
    {
      if (this.m_oOverlayOwnerSub != null)
      {
        this.m_oOverlayOwnerSub.ReleaseHandle();
        this.m_oOverlayOwnerSub = (QRibbonApplicationButton.QOverlayOwnerSub) null;
      }
      if (this.m_oOverlay != null)
      {
        this.DetachToolTip();
        this.m_oOverlay.Dispose();
        this.m_oOverlay = (QTranslucentWindow) null;
      }
      if (this.m_oCurrentOverlayOwner == null)
        return;
      this.m_oCurrentOverlayOwner = (Control) null;
    }

    private void SynchronizeOverlay()
    {
      this.SynchronizeOverlayStart();
      this.SynchronizeOverlayFinish();
    }

    protected virtual void SynchronizeOverlayStart()
    {
      this.m_bShowOverlayOnFinish = false;
      if (this.Configuration.Overlay != QMargin.Empty)
      {
        if (!this.OverlayVisible && this.m_oOverlay == null && this.FindOverlayOwner() != null)
        {
          this.m_oOverlay = (QTranslucentWindow) new QRibbonApplicationButton.QRibbonApplicationButtonOverlay(this);
          this.AttachToolTip();
          this.m_oOverlay.Visible = false;
        }
        if (this.m_oOverlay == null)
          return;
        Control overlayOwner = this.FindOverlayOwner();
        if (overlayOwner != this.m_oCurrentOverlayOwner)
        {
          if (this.m_oCurrentOverlayOwner != null && this.m_oOverlayOwnerSub != null)
            this.m_oOverlayOwnerSub.ReleaseHandle();
          this.m_oCurrentOverlayOwner = overlayOwner;
          if (this.m_oCurrentOverlayOwner != null)
          {
            this.m_oOverlay.OwnerControl = this.m_oCurrentOverlayOwner;
            this.m_oOverlayOwnerSub = new QRibbonApplicationButton.QOverlayOwnerSub(this, this.m_oCurrentOverlayOwner);
          }
        }
        if (this.m_oCurrentOverlayOwner != null)
        {
          if (NativeHelper.GetCurrentWindowState((IWin32Window) this.m_oCurrentOverlayOwner) == FormWindowState.Minimized)
            return;
          if (this.ParentControl != null && this.ParentControl.Visible && QPartHelper.IsPartHierarchyVisible((IQPart) this, QPartVisibilitySelectionTypes.IncludeAll))
          {
            this.m_bShowOverlayOnFinish = !this.m_oOverlay.Visible;
          }
          else
          {
            if (!this.m_oOverlay.Visible)
              return;
            this.m_oOverlay.Visible = false;
          }
        }
        else
        {
          if (!this.m_oOverlay.Visible)
            return;
          this.m_oOverlay.Visible = false;
        }
      }
      else
        this.CleanupOverlay();
    }

    protected virtual void SynchronizeOverlayFinish()
    {
      if (this.m_oOverlay == null || this.ParentControl == null)
        return;
      Rectangle screen = this.ParentControl.RectangleToScreen(new Rectangle(this.CalculatedProperties.Bounds.Location, this.Configuration.ButtonStyle != QRibbonApplicationButtonStyle.CustomPaint ? this.GetLargestImageSize() : this.Configuration.CustomPaintSize));
      screen.Offset(this.Configuration.Overlay.Left, this.Configuration.Overlay.Top);
      if (this.m_oOverlay.Bounds != screen)
        this.m_oOverlay.Bounds = screen;
      if (this.m_bShowOverlayOnFinish)
        this.m_oOverlay.Visible = true;
      this.m_bShowOverlayOnFinish = false;
    }

    protected internal override void ConfigureChildWindow()
    {
      base.ConfigureChildWindow();
      if (!this.OverlayVisible)
        return;
      this.ChildWindow.KeepWindowBehind = (Control) this.m_oOverlay;
      this.ChildWindow.AddAdditionalUnsuppressedMouseControl((Control) this.m_oOverlay);
    }

    protected override void OnItemExpanded(QCompositeExpandedEventArgs e)
    {
      if (e.Item == this && this.OverlayVisible)
        this.ChildWindow.AddAdditionalUnsuppressedMouseControl((Control) this.m_oOverlay);
      base.OnItemExpanded(e);
    }

    protected override void OnItemCollapsed(QCompositeEventArgs e)
    {
      if (e.Item == this && this.OverlayVisible)
      {
        this.ChildWindow.RemoveAdditionalUnsuppressedMouseControl((Control) this.m_oOverlay);
        this.ChildWindow.KeepWindowBehind = (Control) null;
      }
      base.OnItemCollapsed(e);
    }

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      if (destinationObject != this)
        return base.GetItemColorSet(destinationObject, state, additionalProperties);
      QColorSet itemColorSet = new QColorSet();
      if (QItemStatesHelper.IsDisabled(this.ItemState))
      {
        itemColorSet.Background1 = QControlPaint.DesaturateColor(this.RetrieveFirstDefinedColor("RibbonApplicationButtonBackground1"));
        itemColorSet.Background2 = QControlPaint.DesaturateColor(this.RetrieveFirstDefinedColor("RibbonApplicationButtonBackground2"));
        itemColorSet.Foreground = QControlPaint.DesaturateColor(this.RetrieveFirstDefinedColor("RibbonApplicationButtonGlow"));
        itemColorSet.Border = QControlPaint.DesaturateColor(this.RetrieveFirstDefinedColor("RibbonApplicationButtonBorder"));
      }
      else if (QItemStatesHelper.IsNormal(this.ItemState))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonApplicationButtonBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonApplicationButtonBackground2");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonApplicationButtonGlow");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonApplicationButtonBorder");
      }
      else if (QItemStatesHelper.IsPressed(this.ItemState) || QItemStatesHelper.IsExpanded(this.ItemState))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonApplicationButtonPressedBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonApplicationButtonPressedBackground2");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonApplicationButtonPressedGlow");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonApplicationButtonPressedBorder");
      }
      else if (QItemStatesHelper.IsHot(this.ItemState))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonApplicationButtonHotBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonApplicationButtonHotBackground2");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonApplicationButtonHotGlow");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonApplicationButtonHotBorder");
      }
      return itemColorSet;
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part == this)
      {
        switch (layoutStage)
        {
          case QPartLayoutStage.PreparingForLayout:
            this.SynchronizeOverlayStart();
            break;
          case QPartLayoutStage.CalculatingSize:
            this.PutContentObject((object) this.Configuration.Overlay.InflateSizeWithMargin(this.Configuration.ButtonStyle != QRibbonApplicationButtonStyle.CustomPaint ? this.GetLargestImageSize() : this.Configuration.CustomPaintSize, true, true));
            break;
          case QPartLayoutStage.BoundsCalculated:
            this.SynchronizeOverlayFinish();
            break;
        }
      }
      base.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      if (part == this)
      {
        switch (paintStage)
        {
          case QPartPaintStage.PaintingBackground:
            if (this.Configuration.ButtonStyle == QRibbonApplicationButtonStyle.CustomPaint)
            {
              QPartApplicationButtonPainter objectPainter = this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartApplicationButtonPainter)) as QPartApplicationButtonPainter;
              objectPainter.Enabled = this.m_bPaintingOverlay || !this.OverlayVisible;
              objectPainter.Size = this.Configuration.CustomPaintSize;
              objectPainter.ColorSet = this.GetItemColorSet((object) this, this.ItemState, (object) null);
              (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartImagePainter)) as QPartImagePainter).Enabled = false;
            }
            else
            {
              QPartImagePainter objectPainter = this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartImagePainter)) as QPartImagePainter;
              objectPainter.Enabled = this.m_bPaintingOverlay || !this.OverlayVisible;
              objectPainter.KeepImageSize = true;
              objectPainter.DrawDisabled = QItemStatesHelper.IsDisabled(this.ItemState);
              objectPainter.Image = this.GetUsedImageBasedOnState(this.ItemState);
              (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartApplicationButtonPainter)) as QPartApplicationButtonPainter).Enabled = false;
            }
            QPartImagePainter objectPainter1 = this.GetObjectPainter(QPartPaintLayer.Foreground, typeof (QPartImagePainter)) as QPartImagePainter;
            objectPainter1.Enabled = this.m_bPaintingOverlay || !this.OverlayVisible;
            objectPainter1.KeepImageSize = true;
            objectPainter1.DrawDisabled = QItemStatesHelper.IsDisabled(this.ItemState);
            objectPainter1.Image = this.m_oForegroundImage;
            break;
          case QPartPaintStage.ForegroundPainted:
            if (!this.m_bPaintingOverlay && this.OverlayVisible)
            {
              this.m_oOverlay.Refresh();
              break;
            }
            break;
        }
      }
      return base.HandlePaintStage(part, paintStage, paintContext);
    }

    private void DisplayParent_VisibleChanged(object sender, EventArgs e) => this.SynchronizeOverlay();

    private class QOverlayOwnerSub : NativeWindow
    {
      private QWeakEventConsumerCollection m_oEventConsumers;
      private QRibbonApplicationButton m_oButton;
      private Control m_oOwner;

      public QOverlayOwnerSub(QRibbonApplicationButton button, Control owner)
      {
        this.m_oButton = button;
        this.m_oOwner = owner;
        this.m_oEventConsumers = new QWeakEventConsumerCollection();
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Owner_HandleCreated), (object) this.m_oOwner, "HandleCreated"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Owner_HandleDestroyed), (object) this.m_oOwner, "HandleDestroyed"));
        if (!this.m_oOwner.IsHandleCreated)
          return;
        this.AssignHandle(this.m_oOwner.Handle);
      }

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == 71)
        {
          base.WndProc(ref m);
          Qios.DevSuite.Components.NativeMethods.WINDOWPOS valueType = (Qios.DevSuite.Components.NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (Qios.DevSuite.Components.NativeMethods.WINDOWPOS));
          if (((int) valueType.flags & 32) != 32 && ((int) valueType.flags & 2) == 2)
            return;
          this.m_oButton.SynchronizeOverlayFinish();
        }
        else
          base.WndProc(ref m);
      }

      private void Owner_HandleCreated(object sender, EventArgs e) => this.AssignHandle(this.m_oOwner.Handle);

      private void Owner_HandleDestroyed(object sender, EventArgs e) => this.ReleaseHandle();
    }

    public class QRibbonApplicationButtonOverlay : QTranslucentWindow
    {
      private QRibbonApplicationButton m_oButton;
      private bool m_bConstructorFinished;

      public QRibbonApplicationButtonOverlay(QRibbonApplicationButton button)
      {
        this.m_oButton = button;
        this.SetStyle(ControlStyles.Selectable, false);
        this.m_bConstructorFinished = true;
      }

      protected override CreateParams CreateParams
      {
        get
        {
          CreateParams createParams = base.CreateParams;
          createParams.ExStyle |= 134217728;
          return createParams;
        }
      }

      protected override void SetVisibleCore(bool value)
      {
        if (this.m_bConstructorFinished && (this.IsHandleCreated || value))
        {
          uint uFlags = (uint) (535 | (value ? 64 : 128));
          Qios.DevSuite.Components.NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, uFlags);
        }
        base.SetVisibleCore(value);
        if (!this.IsHandleCreated)
          return;
        QControlHelper.UpdateControlRoot((Control) this);
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        if (this.m_oButton != null)
        {
          e.Graphics.TranslateTransform((float) -this.m_oButton.CalculatedProperties.Bounds.Location.X, (float) -this.m_oButton.CalculatedProperties.Bounds.Location.Y);
          QPartPaintContext fromControl = QPartPaintContext.CreateFromControl((Control) this, e.Graphics);
          this.m_oButton.m_bPaintingOverlay = true;
          this.m_oButton.PaintEngine.PerformPaint((IQPart) this.m_oButton, fromControl);
          this.m_oButton.m_bPaintingOverlay = false;
          e.Graphics.ResetTransform();
          fromControl.Dispose();
        }
        base.OnPaint(e);
      }

      protected override void OnMouseEnter(EventArgs e)
      {
        base.OnMouseEnter(e);
        if (this.m_oButton.IsExpanded || this.m_oButton.Composite == null || !(this.m_oButton.Composite.UsedToolTipText != this.m_oButton.ToolTipText))
          return;
        this.m_oButton.Composite.PutUsedToolTipText(this.m_oButton.ToolTipText);
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        base.OnMouseMove(e);
        if (this.m_oButton == null)
          return;
        this.m_oButton.AdjustState(QItemStates.Hot, true, QCompositeActivationType.Mouse);
      }

      protected override void OnMouseLeave(EventArgs e)
      {
        base.OnMouseLeave(e);
        if (this.m_oButton == null)
          return;
        this.m_oButton.AdjustState(QItemStates.Hot, false, QCompositeActivationType.Mouse);
        if (this.m_oButton.Composite == null)
          return;
        this.m_oButton.Composite.PutUsedToolTipText((string) null);
      }

      protected override void OnMouseDown(MouseEventArgs e)
      {
        base.OnMouseDown(e);
        if (this.OwnerWindow.Handle != Qios.DevSuite.Components.NativeMethods.GetForegroundWindow())
          Qios.DevSuite.Components.NativeMethods.SetForegroundWindow(this.OwnerWindow.Handle);
        if (this.m_oButton == null)
          return;
        this.m_oButton.AdjustState(QItemStates.Pressed, true, QCompositeActivationType.Mouse);
        if (!this.m_oButton.CanExpand)
          return;
        if (!this.m_oButton.IsExpanded)
        {
          if (this.m_oButton.Composite != null)
            this.m_oButton.Composite.PutUsedToolTipText((string) null);
          this.m_oButton.Activate(QCompositeItemActivationOptions.Expand, QCompositeActivationType.Mouse);
        }
        else
        {
          if (!this.m_oButton.Composite.CloseExpandedItemOnClick((object) this.m_oButton))
            return;
          this.m_oButton.Composite.CollapseItem((QCompositeItem) this.m_oButton, QCompositeActivationType.Mouse);
        }
      }

      protected override void OnMouseUp(MouseEventArgs e)
      {
        base.OnMouseUp(e);
        if (this.m_oButton == null)
          return;
        this.m_oButton.AdjustState(QItemStates.Pressed, false, QCompositeActivationType.Mouse);
        if (this.m_oButton.CanExpand || this.m_oButton.IsExpanded)
          return;
        this.m_oButton.Activate(QCompositeItemActivationOptions.Activate, QCompositeActivationType.Mouse);
      }

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == 33)
          m.Result = (IntPtr) 3;
        else if (m.Msg == 2)
        {
          base.WndProc(ref m);
          if (this.RecreatingHandle)
            return;
          QControlHelper.ForceControlInternalVisibilityProperty((Control) this, false);
          QControlHelper.UpdateControlRoot((Control) this);
        }
        else
          base.WndProc(ref m);
      }
    }
  }
}
