// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMainMenuConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QMenuConfigurationTypeConverter))]
  public class QMainMenuConfiguration : QToolBarConfiguration
  {
    private bool m_bSimplePersonalizing;
    private QMainMenuMdiButtonsStyle m_eMdiButtonsStyle;
    private bool m_bMdiCloseButtonVisible = true;
    private bool m_bMdiRestoreButtonVisible = true;
    private bool m_bMdiMinimizeButtonVisible = true;
    private bool m_bActiveMdiChildIconVisible = true;
    private static Image m_oDefaultCustomMdiRestoreButtonMask;
    private static Image m_oDefaultCustomMdiMinimizeButtonMask;
    private static Image m_oDefaultCustomMdiCloseButtonMask;
    private Image m_oCustomMdiRestoreButtonMask;
    private Image m_oCustomMdiMinimizeButtonMask;
    private Image m_oCustomMdiCloseButtonMask;
    private QPadding m_oMdiButtonsPadding;
    private Size m_oMdiButtonsSize;
    private QPadding m_oActiveMdiChildIconPadding;

    public QMainMenuConfiguration()
    {
      if (QMainMenuConfiguration.m_oDefaultCustomMdiRestoreButtonMask != null)
        return;
      QMainMenuConfiguration.m_oDefaultCustomMdiRestoreButtonMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.MdiRestoreMask.png"));
      QMainMenuConfiguration.m_oDefaultCustomMdiMinimizeButtonMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.MdiMinimizeMask.png"));
      QMainMenuConfiguration.m_oDefaultCustomMdiCloseButtonMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.MdiCloseMask.png"));
    }

    [DefaultValue(typeof (QMargin), "2,2,2,2")]
    public override QMargin ItemMargin
    {
      get => base.ItemMargin;
      set => base.ItemMargin = value;
    }

    [DefaultValue(typeof (QPadding), "3,3,3,3")]
    public override QPadding ItemPadding
    {
      get => base.ItemPadding;
      set => base.ItemPadding = value;
    }

    [DefaultValue(typeof (QSpacing), "1,1")]
    public override QSpacing IconSpacing
    {
      get => base.IconSpacing;
      set => base.IconSpacing = value;
    }

    [DefaultValue(typeof (QSpacing), "5,5")]
    public override QSpacing TitleSpacing
    {
      get => base.TitleSpacing;
      set => base.TitleSpacing = value;
    }

    [DefaultValue(typeof (QPadding), "4,2,2,4")]
    [Category("QAppearance")]
    [Description("Contains the padding between the QMainMenu and the MdiButtons.")]
    public QPadding MdiButtonsPadding
    {
      get => this.m_oMdiButtonsPadding;
      set
      {
        this.m_oMdiButtonsPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(false)]
    [Description("Determines if the personalizing behaviour should be simplified")]
    [Category("QAppearance")]
    public bool SimplePersonalizing
    {
      get => this.m_bSimplePersonalizing;
      set
      {
        this.m_bSimplePersonalizing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the size of the of the MdiButtons.")]
    [Category("QAppearance")]
    [DefaultValue(typeof (Size), "16, 16")]
    public Size MdiButtonsSize
    {
      get => this.m_oMdiButtonsSize;
      set
      {
        this.m_oMdiButtonsSize = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Contains the Padding between the QMainMenu and the ActiveMdiChildIcon.")]
    [DefaultValue(typeof (QPadding), "3,3,3,3")]
    public QPadding ActiveMdiChildIconPadding
    {
      get => this.m_oActiveMdiChildIconPadding;
      set
      {
        this.m_oActiveMdiChildIconPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("QAppearance")]
    [Browsable(false)]
    public override bool ExpandOnItemClick
    {
      get => base.ExpandOnItemClick;
      set => base.ExpandOnItemClick = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(typeof (QPadding), "1,1,1,1")]
    public override QPadding ToolBarPadding
    {
      get => base.ToolBarPadding;
      set => base.ToolBarPadding = value;
    }

    [DefaultValue(typeof (QPadding), "1,1,1,1")]
    [Description("Gets or sets the padding between the edge and the contents of the MainMenu")]
    [Category("QBehavior")]
    public QPadding MainMenuPadding
    {
      get => this.ToolBarPadding;
      set => this.ToolBarPadding = value;
    }

    [DefaultValue(QMainMenuMdiButtonsStyle.Custom)]
    [Category("QAppearance")]
    [Description("Gets or sets how the MDI Buttons are shown (The minimize, restore and close buttons)")]
    public QMainMenuMdiButtonsStyle MdiButtonsStyle
    {
      get => this.m_eMdiButtonsStyle;
      set
      {
        this.m_eMdiButtonsStyle = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets whether the MDI close button is visible")]
    [DefaultValue(true)]
    public bool MdiCloseButtonVisible
    {
      get => this.m_bMdiCloseButtonVisible;
      set
      {
        this.m_bMdiCloseButtonVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets whether the MDI close button is visible")]
    [Category("QAppearance")]
    [DefaultValue(true)]
    public bool MdiMinimizeButtonVisible
    {
      get => this.m_bMdiMinimizeButtonVisible;
      set
      {
        this.m_bMdiMinimizeButtonVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets whether the MDI restore button is visible")]
    [DefaultValue(true)]
    public bool MdiRestoreButtonVisible
    {
      get => this.m_bMdiRestoreButtonVisible;
      set
      {
        this.m_bMdiRestoreButtonVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Indicates if the Icon of the ActiveMdiChild should be visible on the MainMenu when maximized")]
    [Category("QAppearance")]
    [DefaultValue(true)]
    public bool ActiveMdiChildIconVisible
    {
      get => this.m_bActiveMdiChildIconVisible;
      set
      {
        this.m_bActiveMdiChildIconVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the Restorebutton mask when custom MDI buttons are drawn. In the Mask the Color Red is replaced by the TextColor.")]
    [Category("QAppearance")]
    [DefaultValue(null)]
    public Image CustomMdiRestoreButtonMask
    {
      get => this.m_oCustomMdiRestoreButtonMask;
      set
      {
        this.m_oCustomMdiRestoreButtonMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the MinimizeButton mask when custom MDI buttons are drawn. In the Mask the Color Red is replaced by the TextColor.")]
    [DefaultValue(null)]
    public Image CustomMdiMinimizeButtonMask
    {
      get => this.m_oCustomMdiMinimizeButtonMask;
      set
      {
        this.m_oCustomMdiMinimizeButtonMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the CloseButton Mask when custom MDI buttons are drawn. In the Mask the Color Red is replaced by the TextColor.")]
    [DefaultValue(null)]
    public Image CustomMdiCloseButtonMask
    {
      get => this.m_oCustomMdiCloseButtonMask;
      set
      {
        this.m_oCustomMdiCloseButtonMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(true)]
    public override bool Stretched
    {
      get => base.Stretched;
      set => base.Stretched = value;
    }

    [Browsable(false)]
    public Image UsedCustomMdiCloseButtonMask => this.m_oCustomMdiCloseButtonMask != null ? this.m_oCustomMdiCloseButtonMask : QMainMenuConfiguration.m_oDefaultCustomMdiCloseButtonMask;

    [Browsable(false)]
    public Image UsedCustomMdiRestoreButtonMask => this.m_oCustomMdiRestoreButtonMask != null ? this.m_oCustomMdiRestoreButtonMask : QMainMenuConfiguration.m_oDefaultCustomMdiRestoreButtonMask;

    [Browsable(false)]
    public Image UsedCustomMdiMinimizeButtonMask => this.m_oCustomMdiMinimizeButtonMask != null ? this.m_oCustomMdiMinimizeButtonMask : QMainMenuConfiguration.m_oDefaultCustomMdiMinimizeButtonMask;

    [DefaultValue(false)]
    public override bool UseExpandingDelay
    {
      get => base.UseExpandingDelay;
      set => base.UseExpandingDelay = value;
    }

    [DefaultValue(false)]
    public override bool IconsVisible
    {
      get => base.IconsVisible;
      set => base.IconsVisible = value;
    }

    [Category("QAppearance")]
    [DefaultValue(false)]
    [Description("Indicates if Shortcuts should be shown")]
    public override bool ShortcutsVisible
    {
      get => base.ShortcutsVisible;
      set => base.ShortcutsVisible = value;
    }

    [DefaultValue(typeof (QSpacing), "0,0")]
    public override QSpacing SeparatorSpacing
    {
      get => base.SeparatorSpacing;
      set => base.SeparatorSpacing = value;
    }

    [Browsable(false)]
    public override Image HasChildItemsMask
    {
      get => base.HasChildItemsMask;
      set => base.HasChildItemsMask = value;
    }

    [Browsable(false)]
    public override QSpacing HasChildItemsSpacing
    {
      get => base.HasChildItemsSpacing;
      set => base.HasChildItemsSpacing = value;
    }

    [DefaultValue(false)]
    [Browsable(false)]
    public override bool HasChildItemsImageVisible
    {
      get => base.HasChildItemsImageVisible;
      set => base.HasChildItemsImageVisible = value;
    }

    [DefaultValue(QToolBarLayoutType.ExpandOnNoFit)]
    public override QToolBarLayoutType LayoutType
    {
      get => base.LayoutType;
      set => base.LayoutType = value;
    }
  }
}
