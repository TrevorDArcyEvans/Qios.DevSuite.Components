// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeText
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeTextDesigner), typeof (IDesigner))]
  public class QCompositeText : QCompositeItemBase
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Font m_oCachedFont;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private StringFormat m_oCachedStringFormat;
    private string m_sTitle;
    private string m_sDisplayedTitle;

    protected QCompositeText(object sourceObject, QObjectClonerConstructOptions options)
      : this(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeText()
      : base(QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
    }

    public QCompositeText(QCompositeItemCreationOptions options)
      : base(options)
    {
    }

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = base.CreatePainters(currentPainters);
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Content, (IQPartObjectPainter) new QPartTextPainter());
      return currentPainters;
    }

    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    public override IQPartLayoutEngine LayoutEngine
    {
      get => base.LayoutEngine == QPartLinearLayoutEngine.Default ? (IQPartLayoutEngine) QPartTextLayoutEngine.Default : base.LayoutEngine;
      set => base.LayoutEngine = value;
    }

    private Font GetMeasurementFont(Font baseFont, Graphics graphics)
    {
      Font fontFromCache1 = this.Configuration.FontDefinitionPressed.GetFontFromCache(baseFont);
      Font fontFromCache2 = this.Configuration.FontDefinitionHot.GetFontFromCache(baseFont);
      Font fontFromCache3 = this.Configuration.FontDefinition.GetFontFromCache(baseFont);
      Font fontFromCache4 = this.Configuration.FontDefinitionExpanded.GetFontFromCache(baseFont);
      Font biggestFont1 = QControlPaint.GetBiggestFont(fontFromCache1, fontFromCache3, graphics);
      Font biggestFont2 = QControlPaint.GetBiggestFont(fontFromCache2, biggestFont1, graphics);
      return QControlPaint.GetBiggestFont(fontFromCache4, biggestFont2, graphics);
    }

    [Browsable(false)]
    public QFontDefinition UsedFontDefinition
    {
      get
      {
        QComposite composite = this.Composite;
        QItemStates itemState = this.ItemState;
        if (QItemStatesHelper.IsDisabled(itemState))
          return this.Configuration.FontDefinition;
        if (QItemStatesHelper.IsPressed(itemState) && !QItemStatesHelper.IsExpanded(itemState))
          return this.Configuration.FontDefinitionPressed;
        if (QItemStatesHelper.IsExpanded(itemState) && composite.PaintExpandedItem((object) this))
          return this.Configuration.FontDefinitionExpanded;
        return QItemStatesHelper.IsHot(itemState) ? this.Configuration.FontDefinitionHot : this.Configuration.FontDefinition;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration.")]
    [Category("QAppearance")]
    public QCompositeTextConfiguration Configuration
    {
      get => base.Configuration as QCompositeTextConfiguration;
      set => this.Configuration = (QContentPartConfiguration) value;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeTextConfiguration();

    [Category("QAppearance")]
    [Description("Gets or sets the title of the QRibbonItem")]
    [DefaultValue(null)]
    [QDesignerMainText(true)]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string Title
    {
      get => this.m_sTitle;
      set => this.SetTitle(value, true);
    }

    public void SetTitle(string title, bool notifyParent)
    {
      this.m_sTitle = title;
      this.m_sDisplayedTitle = QHotkeyHelper.RemoveHotkeyPrefix(title);
      if (!notifyParent)
        return;
      this.HandleChange(true);
    }

    [Browsable(false)]
    public string DisplayedTitle => this.m_sDisplayedTitle;

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
          if (this.ContentObject is QPartNativeSizedString contentObject)
            contentObject.Value = this.Title;
          else
            this.PutContentObject((object) this.Title);
          this.m_oCachedFont = this.GetMeasurementFont(layoutContext.Font, layoutContext.Graphics);
          this.m_oCachedStringFormat = QPartTextPainter.CreateStringFormat(layoutContext.StringFormat, this.Configuration.DrawTextOptions, this.Composite == null || this.Composite.HotkeyPrefixVisible, this.Configuration.WrapText);
          layoutContext.Push();
          layoutContext.Font = this.m_oCachedFont;
          layoutContext.StringFormat = this.m_oCachedStringFormat;
          break;
        case QPartLayoutStage.SizeCalculated:
          layoutContext.Font = (Font) null;
          layoutContext.StringFormat = (StringFormat) null;
          layoutContext.Pull();
          break;
        case QPartLayoutStage.ApplyingConstraints:
          layoutContext.Push();
          layoutContext.Font = this.m_oCachedFont;
          layoutContext.StringFormat = this.m_oCachedStringFormat;
          break;
        case QPartLayoutStage.ConstraintsApplied:
          layoutContext.Font = (Font) null;
          layoutContext.StringFormat = (StringFormat) null;
          layoutContext.Pull();
          break;
        case QPartLayoutStage.LayoutFinished:
          if (this.m_oCachedStringFormat != null)
            this.m_oCachedStringFormat.Dispose();
          this.m_oCachedFont = (Font) null;
          this.m_oCachedStringFormat = (StringFormat) null;
          break;
      }
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet1 = (QColorSet) null;
      if (part == this)
      {
        switch (paintStage)
        {
          case QPartPaintStage.PaintingContent:
            qcolorSet1 = this.ColorHost.GetItemColorSet((object) this, this.ItemState, (object) null);
            paintContext.Push();
            paintContext.Font = this.UsedFontDefinition.GetFontFromCache(paintContext.Font);
            paintContext.StringFormat = QPartTextPainter.CreateStringFormat(paintContext.StringFormat, this.Configuration.DrawTextOptions, this.Composite == null || this.Composite.HotkeyPrefixVisible, this.Configuration.WrapText);
            QPartTextPainter objectPainter = part.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartTextPainter)) as QPartTextPainter;
            objectPainter.Orientation = this.Configuration.Orientation;
            objectPainter.TextColor = qcolorSet1.Foreground;
            objectPainter.Font = paintContext.Font;
            objectPainter.StringFormat = paintContext.StringFormat;
            break;
          case QPartPaintStage.ContentPainted:
            paintContext.Pull();
            break;
        }
      }
      QColorSet qcolorSet2 = base.HandlePaintStage(part, paintStage, paintContext);
      return qcolorSet1 ?? qcolorSet2;
    }
  }
}
