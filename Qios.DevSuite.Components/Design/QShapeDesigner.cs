// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QShapeDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  public class QShapeDesigner : ComponentDesigner
  {
    private QShape m_oShape;
    private QGeneralDesignerHandler m_oGeneralHandler;
    private QShapeDesignerForm m_oShapeDesignerForm;
    private DesignerVerbCollection m_oVerbs;
    private IDesignerHost m_oDesignerHost;
    private IComponentChangeService m_oComponentChangeService;
    private Image m_oBackgroundImage;
    private int m_iBackgroundImageScale = 100;
    private int m_iBackgroundImageOpacity = 100;
    private Point m_oBackgroundImagePosition = Point.Empty;
    private QSmoothingMode m_oSmoothingMode;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      this.m_oShape = (QShape) component;
      this.m_oComponentChangeService = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
      this.m_oDesignerHost = this.GetService(typeof (IDesignerHost)) as IDesignerHost;
    }

    public Image BackgroundImage
    {
      get => this.m_oBackgroundImage;
      set => this.m_oBackgroundImage = value;
    }

    public int BackgroundImageScale
    {
      get => this.m_iBackgroundImageScale;
      set => this.m_iBackgroundImageScale = value > 0 ? value : throw new InvalidOperationException("Scale must be greater then 0");
    }

    public int BackgroundImageOpacity
    {
      get => this.m_iBackgroundImageOpacity;
      set => this.m_iBackgroundImageOpacity = value > 0 && value <= 100 ? value : throw new InvalidOperationException("Opacity must be between 1 and 100");
    }

    public Point BackgroundImagePosition
    {
      get => this.m_oBackgroundImagePosition;
      set => this.m_oBackgroundImagePosition = value;
    }

    public QSmoothingMode SmoothingMode
    {
      get => this.m_oSmoothingMode;
      set => this.m_oSmoothingMode = value;
    }

    protected override void PreFilterProperties(IDictionary properties)
    {
      base.PreFilterProperties(properties);
      properties.Add((object) "BackgroundImage", (object) TypeDescriptor.CreateProperty(typeof (QShapeDesigner), "BackgroundImage", typeof (Image), (Attribute) new DefaultValueAttribute((string) null), (Attribute) new DescriptionAttribute("Contains a possible background image (design only) that can be used to make designing a custom shape more easy."), (Attribute) CategoryAttribute.Design, (Attribute) DesignOnlyAttribute.Yes));
      properties.Add((object) "BackgroundImageScale", (object) TypeDescriptor.CreateProperty(typeof (QShapeDesigner), "BackgroundImageScale", typeof (int), (Attribute) new DefaultValueAttribute(100), (Attribute) new DescriptionAttribute("Contains the scale of the background image"), (Attribute) CategoryAttribute.Design, (Attribute) DesignOnlyAttribute.Yes));
      properties.Add((object) "BackgroundImageOpacity", (object) TypeDescriptor.CreateProperty(typeof (QShapeDesigner), "BackgroundImageOpacity", typeof (int), (Attribute) new DefaultValueAttribute(100), (Attribute) new DescriptionAttribute("Contains the opacity of the background image"), (Attribute) CategoryAttribute.Design, (Attribute) DesignOnlyAttribute.Yes));
      properties.Add((object) "BackgroundImagePosition", (object) TypeDescriptor.CreateProperty(typeof (QShapeDesigner), "BackgroundImagePosition", typeof (Point), (Attribute) new DefaultValueAttribute(typeof (Point), "0,0"), (Attribute) new DescriptionAttribute("Contains the position of the background image"), (Attribute) CategoryAttribute.Design, (Attribute) DesignOnlyAttribute.Yes));
      properties.Add((object) "SmoothingMode", (object) TypeDescriptor.CreateProperty(typeof (QShapeDesigner), "SmoothingMode", typeof (QSmoothingMode), (Attribute) new DefaultValueAttribute((object) QSmoothingMode.None), (Attribute) new DescriptionAttribute("Contains the smoothing mode that is used to draw the QShape in the designer."), (Attribute) CategoryAttribute.Design, (Attribute) DesignOnlyAttribute.Yes));
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        if (this.m_oVerbs == null)
        {
          this.m_oVerbs = new DesignerVerbCollection();
          this.m_oVerbs.Add(new DesignerVerb(QResources.GetGeneral("QShapeDesigner_DesignShape"), new EventHandler(this.DesignShapeVerbClick)));
        }
        return this.m_oVerbs;
      }
    }

    private void DesignShapeVerbClick(object sender, EventArgs e)
    {
      if (this.m_oShape == null)
        return;
      if (this.m_oShapeDesignerForm == null)
      {
        this.m_oShapeDesignerForm = new QShapeDesignerForm();
        this.m_oShapeDesignerForm.ShowInTaskbar = false;
        this.m_oShapeDesignerForm.PopulateShapeTypes();
        this.m_oShapeDesignerForm.SuspendPopulation = true;
        for (int index = 0; index < QShape.BaseShapes.Count; ++index)
          this.m_oShapeDesignerForm.BaseShapes.Add(QShape.BaseShapes[index]);
        this.m_oShapeDesignerForm.SuspendPopulation = false;
        this.m_oShapeDesignerForm.PopulateBaseShapes();
      }
      if (this.m_oShape.Items.Count == 0)
        this.m_oShapeDesignerForm.SelectedShape.CopyTo(this.m_oShape);
      this.m_oShapeDesignerForm.CurrentShape = this.m_oShape;
      DesignerTransaction transaction = this.m_oDesignerHost.CreateTransaction();
      QShape qshape = (QShape) this.m_oShape.Clone();
      if (this.m_oShapeDesignerForm.ShowDialog() == DialogResult.OK)
      {
        transaction.Commit();
        this.m_oComponentChangeService.OnComponentChanged((object) this.m_oShape, (MemberDescriptor) null, (object) null, (object) null);
        this.m_oShapeDesignerForm.CurrentShape.ResetChanged();
      }
      else
      {
        qshape.CopyTo(this.m_oShape);
        transaction.Cancel();
      }
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.m_oGeneralHandler == null)
        return;
      this.m_oGeneralHandler.Dispose();
    }
  }
}
