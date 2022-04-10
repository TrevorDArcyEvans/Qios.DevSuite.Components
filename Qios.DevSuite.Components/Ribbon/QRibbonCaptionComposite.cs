// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCaptionComposite
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCaptionComposite : QComposite
  {
    private Font m_oCaptionFont = QRibbonHelper.GetRibbonCaptionFont();
    private QCompositeIcon m_oIconPart;
    private QPart m_oApplicationButtonAreaPart;
    private QPart m_oLaunchBarAreaPart;
    private QCompositeText m_oApplicationTextPart;
    private QCompositeText m_oSeparatorTextPart;
    private QCompositeText m_oDocumentTextPart;
    private QPart m_oTextAreaPart;
    private QPart m_oItemAreaPart;
    private QPart m_oButtonAreaPart;
    private QRibbonCaptionButton m_oMinimizeButton;
    private QRibbonCaptionButton m_oRestoreButton;
    private QRibbonCaptionButton m_oMaximizeButton;
    private QRibbonCaptionButton m_oCloseButton;
    private static Regex m_oMdiChildRegex;

    protected internal QRibbonCaptionComposite(QRibbonCaption ribbonCaption)
      : base((IQPart) null, (QPartCollection) null, (IQCompositeContainer) ribbonCaption, (QCompositeConfiguration) null, ribbonCaption.ColorScheme)
    {
      this.m_oIconPart = new QCompositeIcon(QCompositeItemCreationOptions.None);
      this.m_oIconPart.ItemName = "Icon";
      this.m_oIconPart.Configuration = this.Configuration.IconConfiguration;
      ((QPartIconPainter) this.m_oIconPart.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartIconPainter))).ColorToReplace = Color.Empty;
      this.m_oApplicationButtonAreaPart = new QPart("ApplicationButtonArea", false, new IQPart[0]);
      this.m_oApplicationButtonAreaPart.Properties = (IQPartSharedProperties) this.Configuration.ApplicationButtonAreaConfiguration;
      this.m_oLaunchBarAreaPart = new QPart("LaunchBarArea", false, new IQPart[0]);
      this.m_oLaunchBarAreaPart.Properties = (IQPartSharedProperties) this.Configuration.LaunchBarAreaConfiguration;
      this.m_oApplicationTextPart = new QCompositeText(QCompositeItemCreationOptions.None);
      this.m_oApplicationTextPart.ItemName = nameof (ApplicationText);
      this.m_oApplicationTextPart.PutContentObject((object) new QPartNativeSizedString());
      this.m_oApplicationTextPart.Configuration = this.Configuration.TextAreaConfiguration.ApplicationTextConfiguration;
      this.m_oApplicationTextPart.ColorHost = (IQItemColorHost) this;
      this.m_oSeparatorTextPart = new QCompositeText(QCompositeItemCreationOptions.None);
      this.m_oSeparatorTextPart.ItemName = "SeparatorText";
      this.m_oSeparatorTextPart.PutContentObject((object) new QPartNativeSizedString());
      this.m_oSeparatorTextPart.Configuration = this.Configuration.TextAreaConfiguration.SeparatorTextConfiguration;
      this.m_oSeparatorTextPart.ColorHost = (IQItemColorHost) this;
      this.m_oDocumentTextPart = new QCompositeText(QCompositeItemCreationOptions.None);
      this.m_oDocumentTextPart.ItemName = nameof (DocumentText);
      this.m_oDocumentTextPart.PutContentObject((object) new QPartNativeSizedString());
      this.m_oDocumentTextPart.Configuration = this.Configuration.TextAreaConfiguration.DocumentTextConfiguration;
      this.m_oDocumentTextPart.ColorHost = (IQItemColorHost) this;
      this.m_oTextAreaPart = new QPart(nameof (Text), false, new IQPart[3]
      {
        (IQPart) this.m_oDocumentTextPart,
        (IQPart) this.m_oSeparatorTextPart,
        (IQPart) this.m_oApplicationTextPart
      });
      this.m_oTextAreaPart.Properties = (IQPartSharedProperties) this.Configuration.TextAreaConfiguration;
      this.m_oTextAreaPart.PaintListener = (IQPartPaintListener) this;
      this.m_oTextAreaPart.LayoutListener = (IQPartLayoutListener) this;
      this.m_oItemAreaPart = new QPart("ItemArea", false, new IQPart[0]);
      this.m_oItemAreaPart.Properties = (IQPartSharedProperties) this.Configuration.ItemAreaConfiguration;
      this.m_oMinimizeButton = this.CreateButton(nameof (MinimizeButton), this.Configuration.ButtonAreaConfiguration.MinimizeButtonConfiguration);
      this.m_oRestoreButton = this.CreateButton(nameof (RestoreButton), this.Configuration.ButtonAreaConfiguration.RestoreButtonConfiguration);
      this.m_oMaximizeButton = this.CreateButton(nameof (MaximizeButton), this.Configuration.ButtonAreaConfiguration.MaximizeButtonConfiguration);
      this.m_oCloseButton = this.CreateButton(nameof (CloseButton), this.Configuration.ButtonAreaConfiguration.CloseButtonConfiguration);
      this.m_oRestoreButton.Visible = false;
      this.m_oButtonAreaPart = new QPart("ButtonArea", false, new IQPart[4]
      {
        (IQPart) this.m_oMinimizeButton,
        (IQPart) this.m_oRestoreButton,
        (IQPart) this.m_oMaximizeButton,
        (IQPart) this.m_oCloseButton
      });
      this.m_oButtonAreaPart.Properties = (IQPartSharedProperties) this.Configuration.ButtonAreaConfiguration;
      this.Parts.SuspendChangeNotification();
      this.Parts.Add((IQPart) this.m_oIconPart, false);
      this.Parts.Add((IQPart) this.m_oApplicationButtonAreaPart, false);
      this.Parts.Add((IQPart) this.m_oLaunchBarAreaPart, false);
      this.Parts.Add((IQPart) this.m_oTextAreaPart, false);
      this.Parts.Add((IQPart) this.m_oItemAreaPart, false);
      this.Parts.Add((IQPart) this.m_oButtonAreaPart, false);
      this.Parts.ResumeChangeNotification(false);
    }

    private QRibbonCaptionButton CreateButton(
      string name,
      QRibbonCaptionButtonConfiguration configuration)
    {
      QRibbonCaptionButton button = new QRibbonCaptionButton();
      button.ItemName = name;
      button.Configuration = configuration;
      return button;
    }

    protected override QCompositeConfiguration CreateConfiguration() => (QCompositeConfiguration) new QRibbonCaptionConfiguration();

    protected override QCompositeConfiguration CreateChildCompositeConfiguration() => (QCompositeConfiguration) new QCompositeMenuConfiguration();

    public Font CaptionFont
    {
      get => this.m_oCaptionFont;
      set
      {
        this.m_oCaptionFont = value;
        this.HandleChildObjectChanged(true);
      }
    }

    public QRibbonCaptionConfiguration Configuration => base.Configuration as QRibbonCaptionConfiguration;

    public QRibbonCaption RibbonCaption => this.ParentContainer as QRibbonCaption;

    [Localizable(true)]
    public string ApplicationText
    {
      get => this.m_oApplicationTextPart.Title;
      set => this.m_oApplicationTextPart.Title = value;
    }

    public void SetApplicationText(string value, bool notifyChange) => this.m_oApplicationTextPart.SetTitle(value, notifyChange);

    [Localizable(true)]
    public string DocumentText
    {
      get => this.m_oDocumentTextPart.Title;
      set => this.m_oDocumentTextPart.Title = value;
    }

    public void SetDocumentText(string value, bool notifyChange) => this.m_oDocumentTextPart.SetTitle(value, notifyChange);

    public string ApplicationDocumentSeparator => this.Configuration.TextAreaConfiguration.SeparatorText;

    [Localizable(true)]
    public string Text
    {
      get
      {
        if (this.DocumentText != null && this.DocumentText.Length > 0 && this.ApplicationText != null && this.ApplicationText.Length > 0)
          return this.DocumentText + this.ApplicationDocumentSeparator + this.ApplicationText;
        if (this.DocumentText != null && this.DocumentText.Length > 0)
          return this.DocumentText;
        return this.ApplicationText != null && this.ApplicationText.Length > 0 ? this.ApplicationText : (string) null;
      }
      set
      {
        if (value != null && value.Length > 0)
        {
          int length = value.LastIndexOf(this.ApplicationDocumentSeparator);
          if (length > 0)
          {
            this.SetDocumentText(value.Substring(0, length), false);
            this.SetApplicationText(value.Substring(length + 1), false);
          }
          else
          {
            this.SetDocumentText((string) null, false);
            this.SetApplicationText(value, false);
          }
        }
        else
        {
          this.SetDocumentText((string) null, false);
          this.SetApplicationText((string) null, false);
        }
      }
    }

    public bool SetMDIParentText(string mdiParentText)
    {
      if (QRibbonCaptionComposite.m_oMdiChildRegex == null)
        QRibbonCaptionComposite.m_oMdiChildRegex = new Regex("(.*)( - \\[)(.*)(\\])");
      Match match = QRibbonCaptionComposite.m_oMdiChildRegex.Match(mdiParentText);
      string str1 = (string) null;
      string str2;
      if (match != null && match.Groups.Count >= 3)
      {
        str1 = match.Groups[3].Value;
        str2 = match.Groups[1].Value;
      }
      else
        str2 = mdiParentText;
      bool flag = false;
      if (str2 != this.ApplicationText)
      {
        this.SetApplicationText(str2, false);
        flag = true;
      }
      if (str1 != this.DocumentText)
      {
        this.SetDocumentText(str1, false);
        flag = true;
      }
      if (flag)
        this.HandleChildObjectChanged(true);
      return flag;
    }

    public QRibbonCaptionButton CloseButton => this.m_oCloseButton;

    public QRibbonCaptionButton MinimizeButton => this.m_oMinimizeButton;

    public QRibbonCaptionButton MaximizeButton => this.m_oMaximizeButton;

    public QRibbonCaptionButton RestoreButton => this.m_oRestoreButton;

    public QCompositeIcon IconPart => this.m_oIconPart;

    public QPart ApplicationButtonAreaPart => this.m_oApplicationButtonAreaPart;

    public QPart LaunchBarAreaPart => this.m_oLaunchBarAreaPart;

    public QPart TextAreaPart => this.m_oTextAreaPart;

    public QPart ItemAreaPart => this.m_oItemAreaPart;

    public QPart ButtonAreaPart => this.m_oButtonAreaPart;

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      bool flag = this.RibbonCaption == null || this.RibbonCaption.Active;
      if (destinationObject == this.m_oDocumentTextPart)
      {
        Color foreground = flag ? this.RetrieveFirstDefinedColor("RibbonCaptionDocumentText") : this.RetrieveFirstDefinedColor("RibbonCaptionInactiveDocumentText");
        return new QColorSet(Color.Empty, Color.Empty, Color.Empty, foreground);
      }
      if (destinationObject == this.m_oSeparatorTextPart || destinationObject == this.m_oApplicationTextPart)
      {
        Color foreground = flag ? this.RetrieveFirstDefinedColor("RibbonCaptionApplicationText") : this.RetrieveFirstDefinedColor("RibbonCaptionInactiveApplicationText");
        return new QColorSet(Color.Empty, Color.Empty, Color.Empty, foreground);
      }
      if (destinationObject != this)
        return base.GetItemColorSet(destinationObject, state, additionalProperties);
      QColorSet itemColorSet = new QColorSet();
      if (flag)
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonCaptionBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonCaptionBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonCaptionBorder");
      }
      else
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonCaptionInactiveBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonCaptionInactiveBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonCaptionInactiveBorder");
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
        if (layoutStage == QPartLayoutStage.CalculatingSize)
        {
          this.m_oSeparatorTextPart.SetVisible(this.DocumentText != null && this.DocumentText.Length > 0 && this.ApplicationText != null && this.ApplicationText.Length > 0, false, false);
          this.m_oSeparatorTextPart.Title = this.Configuration.TextAreaConfiguration.SeparatorText;
        }
      }
      else if (part == this.m_oTextAreaPart)
      {
        switch (layoutStage)
        {
          case QPartLayoutStage.CalculatingSize:
            layoutContext.Push();
            layoutContext.Font = this.m_oCaptionFont;
            break;
          case QPartLayoutStage.SizeCalculated:
            layoutContext.Font = (Font) null;
            layoutContext.Pull();
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
      if (part != this && part == this.m_oTextAreaPart)
      {
        switch (paintStage)
        {
          case QPartPaintStage.PaintingContent:
            paintContext.Push();
            paintContext.Font = this.m_oCaptionFont;
            break;
          case QPartPaintStage.ContentPainted:
            paintContext.Font = (Font) null;
            paintContext.Pull();
            break;
        }
      }
      return base.HandlePaintStage(part, paintStage, paintContext);
    }
  }
}
