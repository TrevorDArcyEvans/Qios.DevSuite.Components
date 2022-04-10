// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCollapsedItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCollapsedItem : QCompositeItem
  {
    private QRibbonPanel m_oCachedRibbonPanel;
    private QCompositeIcon m_oIcon;
    private QCompositeText m_oTitle;
    private QCompositeImage m_oDropDownButton;

    public QRibbonCollapsedItem(
      QRibbonCollapsedItemConfiguration configuration,
      IQItemColorHost colorHost)
      : base(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection)
    {
      this.Configuration = configuration;
      if (colorHost != null)
        this.ColorHost = colorHost;
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.Items.SuspendChangeNotification();
      this.m_oIcon = (QCompositeIcon) new QCompositeMenuItemIcon(QCompositeItemCreationOptions.None);
      this.m_oIcon.SetObjectPainter(QPartPaintLayer.Background, (IQPartObjectPainter) new QPartShapePainter()
      {
        Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds)
      });
      this.m_oIcon.ItemName = "Icon";
      this.m_oIcon.Configuration = (QCompositeIconConfiguration) this.Configuration.IconConfiguration;
      this.Items.Add((IQPart) this.m_oIcon, false);
      this.m_oTitle = new QCompositeText(QCompositeItemCreationOptions.None);
      this.m_oTitle.Configuration = this.Configuration.TitleConfiguration;
      this.m_oTitle.ItemName = "Title";
      this.Items.Add((IQPart) this.m_oTitle, false);
      this.m_oDropDownButton = new QCompositeImage(QCompositeItemCreationOptions.None);
      this.m_oDropDownButton.Configuration = (QCompositeImageConfiguration) this.Configuration.DropDownButtonConfiguration;
      this.m_oDropDownButton.ItemName = "DropDownButton";
      this.Items.Add((IQPart) this.m_oDropDownButton, false);
      this.ConfigureSharedItemsProperties();
      this.Items.ResumeChangeNotification(false);
    }

    protected override QCompositeWindow CreateChildWindow()
    {
      QCompositeWindow childWindow = (QCompositeWindow) new QRibbonCollapsedItem.QRibbonCollapsedItemCompositeWindow((IQPart) this, this.ChildItems, (IWin32Window) this.Composite?.ParentContainer.Control);
      childWindow.MinimumClientSize = new Size(0, 0);
      return childWindow;
    }

    private void ConfigureSharedItemsProperties()
    {
      for (int index = 0; index < this.Parts.Count; ++index)
      {
        if (this.Parts[index] is QCompositeItemBase part)
          part.ColorHost = this.ColorHost;
      }
    }

    [Category("QAppearance")]
    [Description("Contains the Configuration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonCollapsedItemConfiguration Configuration
    {
      get => base.Configuration as QRibbonCollapsedItemConfiguration;
      set => this.Configuration = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QCompositeWindow CustomChildWindow
    {
      get => (QCompositeWindow) null;
      set
      {
      }
    }

    internal QCompositeImage DropDownButtonPart => this.m_oDropDownButton;

    internal QCompositeText TitlePart => this.m_oTitle;

    internal QCompositeIcon IconPart => this.m_oIcon;

    [Browsable(false)]
    public QRibbonPanel RibbonPanel
    {
      get
      {
        if (this.m_oCachedRibbonPanel == null)
          this.m_oCachedRibbonPanel = QRibbonPanel.FindRibbonPanel((IQPart) this);
        return this.m_oCachedRibbonPanel;
      }
    }

    protected override void ClearCachedParents()
    {
      base.ClearCachedParents();
      this.m_oCachedRibbonPanel = (QRibbonPanel) null;
    }

    protected internal override void ConfigureChildWindow()
    {
      base.ConfigureChildWindow();
      this.ChildWindow.SuspendChangeNotification();
      this.ChildWindow.Configuration = this.Configuration.ChildWindowConfiguration;
      this.ChildWindow.CompositeConfiguration = (QCompositeConfiguration) this.RibbonPanel.RibbonPage.Configuration;
      this.ChildWindow.ResumeChangeNotification(false);
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      base.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
      if (part != this || layoutStage != QPartLayoutStage.CalculatingSize)
        return;
      this.m_oDropDownButton.SetImage(this.Configuration.DropDownButtonConfiguration.UsedMask, false);
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet = base.HandlePaintStage(part, paintStage, paintContext);
      if (part == this && paintStage == QPartPaintStage.PaintingBackground)
      {
        if (qcolorSet == null)
          qcolorSet = this.ColorHost.GetItemColorSet((object) this, this.ItemState, (object) null);
        QPartShapePainter objectPainter = this.m_oIcon.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) as QPartShapePainter;
        objectPainter.Appearance = (QShapeAppearance) this.Configuration.IconConfiguration.Appearance;
        objectPainter.ColorSet = qcolorSet;
      }
      return qcolorSet;
    }

    public class QRibbonCollapsedItemComposite : QComposite
    {
      internal QRibbonCollapsedItemComposite(
        IQPart parentPart,
        QPartCollection useCollection,
        IQCompositeContainer parentContainer,
        QCompositeConfiguration configuration,
        QColorScheme colorScheme)
        : base(parentPart, useCollection, parentContainer, configuration, colorScheme)
      {
      }

      public QRibbonCollapsedItem CollapsedItem => this.ParentPart as QRibbonCollapsedItem;

      public override QTristateBool HasHotState => QTristateBool.True;

      public override QColorSet GetItemColorSet(
        object destinationObject,
        QItemStates state,
        object additionalProperties)
      {
        return this.CollapsedItem != null ? this.CollapsedItem.ColorHost.GetItemColorSet(destinationObject, state, additionalProperties) : base.GetItemColorSet(destinationObject, state, additionalProperties);
      }
    }

    [ToolboxItem(false)]
    public class QRibbonCollapsedItemCompositeWindow : QCompositeWindow
    {
      public QRibbonCollapsedItemCompositeWindow(
        IQPart parentPart,
        QPartCollection displayedParts,
        IWin32Window ownerWindow)
        : base(parentPart, displayedParts, (QColorScheme) null, ownerWindow)
      {
      }

      protected override QComposite CreateComposite(
        IQPart parentPart,
        QPartCollection useCollection)
      {
        QRibbonPageComposite displayParent = parentPart.DisplayParent as QRibbonPageComposite;
        return (QComposite) new QRibbonCollapsedItem.QRibbonCollapsedItemComposite(parentPart, useCollection, (IQCompositeContainer) this, (QCompositeConfiguration) displayParent.Configuration, (QColorScheme) null);
      }
    }
  }
}
