// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupText
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Qios.DevSuite.Components
{
  public class QMarkupText : IQWeakEventPublisher, IQPartSizedContent, ICloneable
  {
    public const string DefaultTagAnchor = "a";
    public const string DefaultTagBold = "b";
    public const string DefaultTagBig = "big";
    public const string DefaultTagBR = "br";
    public const string DefaultTagItalic = "i";
    public const string DefaultTagFont = "font";
    public const string DefaultNobreak = "nobr";
    public const string DefaultTagUnderline = "u";
    public const string DefaultTagParagraph = "p";
    public const string DefaultTagSmall = "small";
    private bool m_bWeakEventHandlers = true;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private ArrayList m_oFocusRectangles;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private XmlDocument m_oMarkupDocument;
    private QMarkupTextStyle m_oRootStyle;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QMarkupTextElementRoot m_oRoot;
    private string m_sMarkupText;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QMarkupTextRenderedLineCollection m_oLines;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QMarkupTextElement m_oHotElement;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QMarkupTextElement m_oActiveElement;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QMarkupTextElement m_oFocusedElement;
    private Rectangle m_oBounds = Rectangle.Empty;
    private QColorScheme m_oColorScheme;
    private QMarkupTextConfiguration m_oConfiguration;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QMarkupTextStyleCollection m_oDefaultStyles;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QMarkupTextStyleCollection m_oCustomStyles;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bSuspendProcessing;
    private static Regex m_oWhiteSpaceRegex = new Regex("(\\s)+", RegexOptions.Singleline);
    private static Regex m_oStartWhiteSpaceRegex = new Regex("^(\\s)+", RegexOptions.Singleline);
    private static Regex m_oEndWhiteSpaceRegex = new Regex("$(\\s)+", RegexOptions.Singleline);
    private static Regex m_oReplaceBRWithEnterRegex = new Regex("<br/>", RegexOptions.Singleline);
    private static Regex m_oReplaceEnterWithBRRegex = new Regex("(\\r\\n)|(\\n)", RegexOptions.Singleline);
    private static Regex m_oRemoveTagsRegex = new Regex("(<)([^>]*)(>)", RegexOptions.Singleline);
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oUpdateRequestedDelegate;

    protected internal QMarkupText(object sourceObject, QObjectClonerConstructOptions options)
    {
      QMarkupText sourceObject1 = sourceObject as QMarkupText;
      Font font = sourceObject1.Font;
      this.InternalConstruct(sourceObject1.ColorScheme != null ? sourceObject1.ColorScheme.Clone() as QColorScheme : (QColorScheme) null, font, Color.Black, sourceObject1);
    }

    internal QMarkupText(QColorScheme colorScheme, Font font, Color textColor) => this.InternalConstruct(colorScheme, font, textColor, (QMarkupText) null);

    private void InternalConstruct(
      QColorScheme colorScheme,
      Font font,
      Color textColor,
      QMarkupText sourceObject)
    {
      this.SuspendProcessing();
      this.m_oMarkupDocument = new XmlDocument();
      this.m_oMarkupDocument.AppendChild((XmlNode) this.m_oMarkupDocument.CreateElement("qMarkupText"));
      this.m_oRootStyle = sourceObject == null ? (QMarkupTextStyle) new QMarkupTextStyleRoot() : sourceObject.m_oRootStyle.Clone() as QMarkupTextStyle;
      this.m_oRootStyle.PutParentMarkupText(this);
      this.m_oRoot = this.m_oRootStyle.CreateElement((XmlNode) this.m_oMarkupDocument.DocumentElement) as QMarkupTextElementRoot;
      this.m_oRoot.ProcessMarkup((XmlNode) this.m_oMarkupDocument.DocumentElement);
      this.SetProperties(font, colorScheme, textColor);
      this.m_oColorScheme = colorScheme;
      this.m_oConfiguration = sourceObject == null ? new QMarkupTextConfiguration() : sourceObject.Configuration.Clone() as QMarkupTextConfiguration;
      this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
      this.m_oDefaultStyles = new QMarkupTextStyleCollection(this);
      this.AddDefaultStyles(this.m_oDefaultStyles);
      this.m_oCustomStyles = new QMarkupTextStyleCollection(this);
      if (sourceObject != null)
      {
        for (int index = 0; index < sourceObject.CustomStyles.Count; ++index)
          this.m_oCustomStyles.Add(sourceObject.CustomStyles[index].Clone() as QMarkupTextStyle);
      }
      this.m_oLines = new QMarkupTextRenderedLineCollection(this);
      this.ResumeProcessing(false);
    }

    private void AddDefaultStyles(QMarkupTextStyleCollection styles)
    {
      QMarkupTextStyle style1 = (QMarkupTextStyle) new QMarkupTextStyleText();
      style1.CanBeSourceForEvents = false;
      styles.Add(style1);
      QMarkupTextStyle style2 = new QMarkupTextStyle("b");
      style2.ApplyAllFontDefinitions(new QFontDefinition((string) null, (QFontStyle) FontStyle.Bold, -1f));
      styles.Add(style2);
      QMarkupTextStyle style3 = new QMarkupTextStyle("i");
      style3.ApplyAllFontDefinitions(new QFontDefinition((string) null, (QFontStyle) FontStyle.Italic, -1f));
      styles.Add(style3);
      QMarkupTextStyle style4 = new QMarkupTextStyle("u");
      style4.ApplyAllFontDefinitions(new QFontDefinition((string) null, (QFontStyle) FontStyle.Underline, -1f));
      styles.Add(style4);
      styles.Add(new QMarkupTextStyle("br")
      {
        AddEmptyPart = true,
        NewLineAfter = true
      });
      styles.Add(new QMarkupTextStyle("p")
      {
        NewLineBefore = true,
        NewLineAfter = true
      });
      styles.Add(new QMarkupTextStyle("nobr")
      {
        WrapText = QTristateBool.False
      });
      QMarkupTextStyle style5 = new QMarkupTextStyle("a", typeof (QMarkupTextElementAnchor));
      style5.ApplyAllFontDefinitions(new QFontDefinition((string) null, (QFontStyle) FontStyle.Underline, -1f));
      style5.TextColorProperty = "MarkupTextAnchor";
      style5.TextColorHotProperty = "MarkupTextAnchorHot";
      style5.TextColorActiveProperty = "MarkupTextAnchorActive";
      style5.TextColorDisabledProperty = "MarkupTextAnchorDisabled";
      style5.ReapplyAttributesOnStateChange = true;
      style5.CanFocus = true;
      styles.Add(style5);
      styles.Add(new QMarkupTextStyle("big", typeof (QMarkupTextElementBigSmall))
      {
        AdditionalTags = new string[1]{ "small" }
      });
      QMarkupTextStyle style6 = new QMarkupTextStyle("font", typeof (QMarkupTextElementFont));
      styles.Add(style6);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the QMarkupText requests an update of the UI")]
    public event QCommandUIRequestEventHandler UpdateRequested
    {
      add => this.m_oUpdateRequestedDelegate = QWeakDelegate.Combine(this.m_oUpdateRequestedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oUpdateRequestedDelegate = QWeakDelegate.Remove(this.m_oUpdateRequestedDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    internal ArrayList FocusRectangles
    {
      get
      {
        if (this.m_oFocusRectangles == null)
          this.m_oFocusRectangles = new ArrayList();
        return this.m_oFocusRectangles;
      }
    }

    internal QMarkupTextRenderedLineCollection Lines => this.m_oLines;

    public QMarkupTextElementRoot Root => this.m_oRoot;

    public QMarkupTextStyleCollection CustomStyles => this.m_oCustomStyles;

    public bool Enabled
    {
      get => this.m_oRoot.Enabled;
      set => this.m_oRoot.Enabled = value;
    }

    public Rectangle Bounds => this.m_oBounds;

    internal void PutBounds(Rectangle value) => this.m_oBounds = value;

    public Size Size => this.m_oBounds.Size;

    internal void PutSize(Size value) => this.m_oBounds.Size = value;

    public Point Location => this.m_oBounds.Location;

    internal void PutLocation(Point value) => this.m_oBounds.Location = value;

    public int Left => this.m_oBounds.X;

    public int Top => this.m_oBounds.Y;

    public int Width => this.m_oBounds.Width;

    public int Height => this.m_oBounds.Height;

    public Size LastCalculatedSize => this.Configuration.MarkupPadding.InflateSizeWithPadding(new Size((int) Math.Ceiling((double) this.m_oLines.Width), (int) Math.Ceiling((double) this.m_oLines.Height)), true, true);

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    [Description("Gets the configuration for the QMarkupText.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QMarkupTextConfiguration Configuration => this.m_oConfiguration;

    public Font Font => this.m_oRoot.CurrentFont;

    public Color TextColor => this.m_oRoot.CurrentColor;

    public QColorScheme ColorScheme => this.m_oColorScheme;

    [DefaultValue("")]
    [Description("Gets or sets the Xml for this QMarkupLabel")]
    [Category("QAppearance")]
    public string MarkupText
    {
      get => this.m_sMarkupText;
      set
      {
        try
        {
          this.m_oMarkupDocument.FirstChild.InnerXml = value;
        }
        catch (Exception ex)
        {
          this.m_oMarkupDocument.FirstChild.InnerXml = string.Empty;
          throw new InvalidOperationException(QResources.GetException("QMarkupText_MarkupText_CannotSet"), ex);
        }
        ArrayList nodesToRemove = (ArrayList) null;
        QMarkupText.RemoveExtraWhiteSpace(this.m_oMarkupDocument.FirstChild, false, ref nodesToRemove);
        QMarkupText.RemoveNodes(nodesToRemove);
        this.m_sMarkupText = this.m_oMarkupDocument.FirstChild.InnerXml;
        this.ProcessMarkup();
        this.ApplyAttributes();
        this.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
      }
    }

    public QMarkupTextElement HotElement => this.m_oHotElement;

    internal void PutHotElement(QMarkupTextElement element) => this.m_oHotElement = element;

    public QMarkupTextElement ActiveElement => this.m_oActiveElement;

    internal void PutActiveElement(QMarkupTextElement element) => this.m_oActiveElement = element;

    public QMarkupTextElement FocusedElement => this.m_oFocusedElement;

    internal void PutFocusedElement(QMarkupTextElement element) => this.m_oFocusedElement = element;

    public Rectangle ClippingBounds => this.Configuration.MarkupPadding.InflateRectangleWithPadding(this.Bounds, false, true);

    internal PointF CalculateAbsoluteLocation(PointF childlocation) => new PointF((float) this.m_oBounds.Left + childlocation.X, (float) this.m_oBounds.Top + childlocation.Y);

    internal static string ReplaceEnterWithBR(string value) => QMarkupText.m_oReplaceEnterWithBRRegex.Replace(value, "<br/>");

    internal static string ReplaceBRWithEnter(string value) => QMarkupText.m_oReplaceBRWithEnterRegex.Replace(value, "\r\n");

    internal static string RemoveTags(string value) => QMarkupText.m_oRemoveTagsRegex.Replace(value, "");

    internal static bool RemoveExtraWhiteSpace(
      XmlNode node,
      bool lastTextEndedWithWhiteSpace,
      ref ArrayList nodesToRemove)
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode == null)
        {
          string empty = string.Empty;
        }
        else
        {
          string name = childNode.Name;
        }
        switch (childNode)
        {
          case XmlText _:
            string str;
            if (lastTextEndedWithWhiteSpace)
            {
              string input = QMarkupText.m_oStartWhiteSpaceRegex.Replace(childNode.InnerText, "");
              str = QMarkupText.m_oWhiteSpaceRegex.Replace(input, " ");
            }
            else
              str = QMarkupText.m_oWhiteSpaceRegex.Replace(childNode.InnerText, " ");
            lastTextEndedWithWhiteSpace = str.EndsWith(" ");
            childNode.InnerText = str;
            continue;
          case XmlWhitespace _:
            if (lastTextEndedWithWhiteSpace)
            {
              if (nodesToRemove == null)
                nodesToRemove = new ArrayList();
              nodesToRemove.Add((object) childNode);
              continue;
            }
            childNode.InnerText = " ";
            lastTextEndedWithWhiteSpace = true;
            continue;
          case XmlElement _:
            lastTextEndedWithWhiteSpace = QMarkupText.RemoveExtraWhiteSpace(childNode, lastTextEndedWithWhiteSpace, ref nodesToRemove);
            continue;
          default:
            continue;
        }
      }
      return lastTextEndedWithWhiteSpace;
    }

    internal static void RemoveNodes(ArrayList nodesToRemove)
    {
      if (nodesToRemove == null)
        return;
      for (int index = nodesToRemove.Count - 1; index >= 0; --index)
      {
        if (nodesToRemove[index] is XmlNode oldChild && oldChild.ParentNode != null)
          oldChild.ParentNode.RemoveChild(oldChild);
      }
    }

    internal void DisposeElements()
    {
      this.m_oRoot.Elements.DisposeElements();
      this.m_oRoot.Elements.Clear();
    }

    internal void DisposeRenderedObjects()
    {
      this.m_oRoot.Elements.DisposeRenderedObjects(true);
      this.m_oLines.Clear();
    }

    internal QMarkupTextStyle GetStyleForNode(
      QMarkupTextStyleCollection collection,
      XmlNode node)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index].CanRenderNode(node))
          return collection[index];
      }
      return (QMarkupTextStyle) null;
    }

    public QMarkupTextStyle GetStyleForNode(XmlNode node) => this.GetStyleForNode(this.m_oCustomStyles, node) ?? this.GetStyleForNode(this.m_oDefaultStyles, node);

    internal void SetProperties(Font font, QColorScheme colorScheme, Color textColor)
    {
      this.m_oColorScheme = colorScheme;
      this.m_oRoot.PutCurrentColor(textColor);
      if (font != null)
        this.m_oRoot.PutCurrentFont(font);
      else
        this.m_oRoot.PutCurrentFont((Font) null);
      this.ApplyAttributes();
      this.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    public void SuspendProcessing() => this.m_bSuspendProcessing = true;

    public void ResumeProcessing(bool processNow) => this.ResumeProcessing(processNow, processNow);

    internal void ResumeProcessing(bool processNow, bool raiseEvent)
    {
      this.m_bSuspendProcessing = false;
      if (processNow)
      {
        this.ProcessMarkup();
        this.ApplyAttributes();
      }
      if (!raiseEvent)
        return;
      this.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    internal void ProcessMarkup()
    {
      if (this.m_bSuspendProcessing)
        return;
      this.DisposeRenderedObjects();
      this.DisposeElements();
      this.m_oRoot.ProcessMarkup((XmlNode) this.m_oMarkupDocument.DocumentElement);
    }

    internal void ApplyAttributes()
    {
      if (this.m_bSuspendProcessing)
        return;
      this.m_oRoot.ApplyAttributes();
    }

    internal void RenderMarkupText(QMarkupTextParams markupParams)
    {
      if (this.m_oMarkupDocument == null || this.m_oMarkupDocument.FirstChild == null)
        return;
      this.DisposeRenderedObjects();
      markupParams.MaximumWidth = this.Configuration.MaximumSize.Width < 0 ? (this.Configuration.AdjustWidthToTextSize ? (int) short.MaxValue - this.Configuration.MarkupPadding.Horizontal : this.Width - this.Configuration.MarkupPadding.Horizontal) : this.Configuration.MaximumSize.Width - this.Configuration.MarkupPadding.Horizontal;
      markupParams.MaximumHeight = this.Configuration.MaximumSize.Height < 0 ? (this.Configuration.AdjustHeightToTextSize ? (int) short.MaxValue - this.Configuration.MarkupPadding.Vertical : this.Height - this.Configuration.MarkupPadding.Vertical) : this.Configuration.MaximumSize.Height - this.Configuration.MarkupPadding.Vertical;
      markupParams.Lines = this.m_oLines;
      this.m_oRoot.RenderElement(markupParams);
      double num = (double) markupParams.FinishCurrentLineRendering();
      this.Lines.CalculateSize();
      if (this.Configuration.AdjustWidthToTextSize)
        this.m_oBounds.Width = Math.Min((int) Math.Ceiling((double) this.m_oLines.Bounds.Width) + this.Configuration.MarkupPadding.Horizontal, markupParams.MaximumWidth + this.Configuration.MarkupPadding.Horizontal);
      if (this.Configuration.AdjustHeightToTextSize)
        this.m_oBounds.Height = Math.Min((int) Math.Ceiling((double) this.m_oLines.Bounds.Height) + this.Configuration.MarkupPadding.Vertical, markupParams.MaximumHeight + this.Configuration.MarkupPadding.Vertical);
      this.m_oLines.AlignLineCollection(this.Configuration.TextAlign, this.Configuration.MarkupPadding.InflateRectangleWithPadding(new Rectangle(Point.Empty, this.Size), false, true));
    }

    internal void Draw(Graphics graphics)
    {
      this.FocusRectangles.Clear();
      this.m_oRoot.Draw(graphics);
      if (this.FocusRectangles.Count == 0)
        return;
      Region clip = graphics.Clip;
      Region region = graphics.Clip.Clone();
      for (int index = 0; index < this.FocusRectangles.Count; ++index)
        region.Exclude((Rectangle) this.FocusRectangles[index]);
      graphics.Clip = region;
      for (int index = 0; index < this.FocusRectangles.Count; ++index)
      {
        Rectangle focusRectangle = (Rectangle) this.FocusRectangles[index];
        focusRectangle.Inflate(1, 1);
        ControlPaint.DrawFocusRectangle(graphics, focusRectangle);
      }
      graphics.Clip = clip;
    }

    internal bool ShouldReceiveFocus() => this.Configuration.CanFocus && this.m_oRoot.GetFocusableElement(true, true) != null;

    internal void HandleGotFocus()
    {
      if (this.Configuration != null && !this.Configuration.CanFocus)
      {
        if (this.FocusedElement == null)
          return;
        this.FocusedElement.HandleLostFocus((QMarkupTextElement) null);
        this.PutFocusedElement((QMarkupTextElement) null);
      }
      else
      {
        QMarkupTextElement element = this.m_oRoot.GetFocusableElement(Control.ModifierKeys != Keys.Shift, false);
        if (element != null)
          element = element.GetSourceForEvents();
        if (element != null && !element.IsAccessible)
          element = (QMarkupTextElement) null;
        if (element == this.FocusedElement)
          return;
        this.PutFocusedElement(element);
        if (this.FocusedElement == null)
          return;
        element.HandleGotFocus((QMarkupTextElement) null);
      }
    }

    internal void HandleLostFocus()
    {
      if (this.FocusedElement == null)
        return;
      this.FocusedElement.HandleLostFocus((QMarkupTextElement) null);
      this.PutFocusedElement((QMarkupTextElement) null);
    }

    internal void HandleFocus(bool forward, bool loop)
    {
      if (this.Configuration != null && !this.Configuration.CanFocus)
      {
        if (this.FocusedElement == null)
          return;
        this.FocusedElement.HandleLostFocus((QMarkupTextElement) null);
        this.PutFocusedElement((QMarkupTextElement) null);
      }
      else
      {
        if (this.FocusedElement == null)
          return;
        this.FocusedElement.HandleLostFocus((QMarkupTextElement) null);
        if (this.FocusedElement.HasElements)
        {
          QMarkupTextElement focusableElement = this.FocusedElement.Elements.GetFocusableElement(forward, false);
          if (focusableElement != null)
          {
            this.PutFocusedElement(focusableElement);
            focusableElement.HandleGotFocus((QMarkupTextElement) null);
            return;
          }
        }
        QMarkupTextElement qmarkupTextElement1 = this.FocusedElement;
        QMarkupTextElement qmarkupTextElement2 = this.FocusedElement;
        while (qmarkupTextElement1 != null)
        {
          for (QMarkupTextElement qmarkupTextElement3 = forward ? qmarkupTextElement2.NextSibling : qmarkupTextElement2.PreviousSibling; qmarkupTextElement3 != null; qmarkupTextElement3 = forward ? qmarkupTextElement3.NextSibling : qmarkupTextElement3.PreviousSibling)
          {
            QMarkupTextElement focusableElement = qmarkupTextElement3.GetFocusableElement(forward, false);
            if (focusableElement != null)
            {
              this.PutFocusedElement(focusableElement);
              focusableElement.HandleGotFocus((QMarkupTextElement) null);
              return;
            }
          }
          qmarkupTextElement1 = qmarkupTextElement1.ParentElement;
          qmarkupTextElement2 = qmarkupTextElement1;
        }
        if (loop && forward)
        {
          QMarkupTextElement focusableElement = this.m_oRoot.GetFocusableElement(true, true);
          this.PutFocusedElement(focusableElement);
          focusableElement?.HandleGotFocus((QMarkupTextElement) null);
        }
        else if (loop && !forward)
        {
          QMarkupTextElement focusableElement = this.m_oRoot.GetFocusableElement(false, true);
          this.PutFocusedElement(focusableElement);
          focusableElement?.HandleGotFocus((QMarkupTextElement) null);
        }
        else
          this.PutFocusedElement((QMarkupTextElement) null);
      }
    }

    internal void HandleMouseMove(MouseEventArgs e)
    {
      QMarkupTextElement element = this.m_oRoot.GetElementThatContainsAbsolutePoint((PointF) new Point(e.X, e.Y));
      if (element != null)
        element = element.GetSourceForEvents();
      if (element != null && !element.IsAccessible)
        element = (QMarkupTextElement) null;
      if (element == this.HotElement)
        return;
      if (this.HotElement != null)
        this.HotElement.HandleMouseLeave(e, (QMarkupTextElement) null);
      this.PutHotElement(element);
      if (this.HotElement == null)
        return;
      element.HandleMouseEnter(e, (QMarkupTextElement) null);
    }

    internal void HandleMouseDown(MouseEventArgs e)
    {
      QMarkupTextElement element1 = (QMarkupTextElement) null;
      QMarkupTextElement containsAbsolutePoint = this.m_oRoot.GetElementThatContainsAbsolutePoint((PointF) new Point(e.X, e.Y));
      if (containsAbsolutePoint != null)
        element1 = containsAbsolutePoint.GetSourceForEvents();
      if (element1 != null && !element1.IsAccessible)
        element1 = (QMarkupTextElement) null;
      if (element1 != this.ActiveElement)
      {
        this.PutActiveElement(element1);
        if (this.ActiveElement != null)
          element1.HandleMouseDown(e, (QMarkupTextElement) null);
      }
      if (containsAbsolutePoint == null || this.Configuration == null || !this.Configuration.CanFocus)
        return;
      QMarkupTextElement element2 = containsAbsolutePoint.GetFocusableElement(true, true);
      if (element2 != null && !element2.IsAccessible)
        element2 = (QMarkupTextElement) null;
      if (element2 == this.FocusedElement)
        return;
      if (this.FocusedElement != null)
        this.FocusedElement.HandleLostFocus((QMarkupTextElement) null);
      this.PutFocusedElement(element2);
      if (this.FocusedElement == null)
        return;
      element2.HandleGotFocus((QMarkupTextElement) null);
    }

    internal void HandleMouseUp(MouseEventArgs e)
    {
      if (this.m_oActiveElement == null)
        return;
      this.m_oActiveElement.HandleMouseUp(e, (QMarkupTextElement) null);
      if (e.Button == MouseButtons.Left && this.m_oActiveElement.ContainsAbsolutePoint((PointF) new Point(e.X, e.Y)))
        this.m_oActiveElement.HandleMouseClick((QMarkupTextElement) null);
      this.PutActiveElement((QMarkupTextElement) null);
    }

    internal void HandleMouseLeave()
    {
      if (this.HotElement == null)
        return;
      this.HotElement.HandleMouseLeave((MouseEventArgs) null, (QMarkupTextElement) null);
      this.PutHotElement((QMarkupTextElement) null);
    }

    internal virtual void RaiseUpdateRequested(QCommandUIRequest request)
    {
      if (this.m_bSuspendProcessing)
        return;
      this.OnUpdateRequested(new QCommandUIRequestEventArgs(request, Rectangle.Empty));
    }

    protected virtual void OnUpdateRequested(QCommandUIRequestEventArgs e) => this.m_oUpdateRequestedDelegate = QWeakDelegate.InvokeDelegate(this.m_oUpdateRequestedDelegate, (object) this, (object) e);

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
      this.ApplyAttributes();
      this.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    void IQPartSizedContent.CalculateSize(QPartLayoutContext layoutContext)
    {
      this.PutBounds(new Rectangle(0, 0, (int) short.MaxValue, (int) short.MaxValue));
      this.RenderMarkupText(new QMarkupTextParams(layoutContext.Graphics));
    }

    Size IQPartSizedContent.Size => this.LastCalculatedSize;

    object IQPartSizedContent.ContentObject => (object) null;

    public object Clone()
    {
      QMarkupText qmarkupText = QObjectCloner.CloneObject((object) this) as QMarkupText;
      qmarkupText.MarkupText = qmarkupText.m_sMarkupText;
      return (object) qmarkupText;
    }
  }
}
