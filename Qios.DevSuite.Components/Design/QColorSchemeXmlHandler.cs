// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QColorSchemeXmlHandler
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  internal class QColorSchemeXmlHandler
  {
    private ComponentDesigner m_oDesigner;
    private QColorScheme m_oColorScheme;
    private DesignerVerbCollection m_oVerbs;
    private SaveFileDialog m_oSaveDialog;
    private OpenFileDialog m_oOpenDialog;
    private IDesignerHost m_oDesignerHost;
    private IComponentChangeService m_oChangeService;

    public QColorSchemeXmlHandler(
      ComponentDesigner designer,
      IDesignerHost designerHost,
      QColorScheme colorScheme)
    {
      this.m_oDesigner = designer;
      this.m_oDesignerHost = designerHost;
      this.m_oColorScheme = colorScheme;
      this.m_oChangeService = (IComponentChangeService) this.m_oDesignerHost.GetService(typeof (IComponentChangeService));
    }

    public DesignerVerbCollection Verbs
    {
      get
      {
        if (this.m_oVerbs == null)
        {
          this.m_oVerbs = new DesignerVerbCollection();
          this.m_oVerbs.Add(new DesignerVerb("Load colors from XML", new EventHandler(this.LoadColorsFromXml)));
          this.m_oVerbs.Add(new DesignerVerb("Save colors To XML", new EventHandler(this.SaveColorsToXml)));
        }
        return this.m_oVerbs;
      }
    }

    private void LoadColorsFromXml(object sender, EventArgs e)
    {
      if (this.m_oOpenDialog == null)
      {
        this.m_oOpenDialog = new OpenFileDialog();
        this.m_oOpenDialog.Title = QResources.GetGeneral("QColorSchemeDesigner_OpenDialogTitle");
        this.m_oOpenDialog.DefaultExt = QResources.GetGeneral("QColorSchemeDesigner_DialogDefaultExtension");
        this.m_oOpenDialog.Filter = QResources.GetGeneral("QColorSchemeDesigner_DialogFilter");
      }
      if (this.m_oOpenDialog.ShowDialog() != DialogResult.OK)
        return;
      if (this.m_oColorScheme == null)
      {
        int num = (int) MessageBox.Show((IWin32Window) null, "QColorScheme is null", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions) 0);
      }
      DesignerTransaction transaction = this.m_oDesignerHost.CreateTransaction();
      try
      {
        this.m_oChangeService.OnComponentChanging((object) this.m_oColorScheme, (MemberDescriptor) null);
        this.m_oColorScheme.LoadFromXml(this.m_oOpenDialog.FileName);
        this.m_oChangeService.OnComponentChanged((object) this.m_oColorScheme, (MemberDescriptor) null, (object) null, (object) null);
      }
      catch
      {
        transaction.Cancel();
        throw;
      }
      transaction.Commit();
    }

    private void SaveColorsToXml(object sender, EventArgs e)
    {
      if (this.m_oSaveDialog == null)
      {
        this.m_oSaveDialog = new SaveFileDialog();
        this.m_oSaveDialog.Title = QResources.GetGeneral("QColorSchemeDesigner_SaveDialogTitle");
        this.m_oSaveDialog.DefaultExt = QResources.GetGeneral("QColorSchemeDesigner_DialogDefaultExtension");
        this.m_oSaveDialog.Filter = QResources.GetGeneral("QColorSchemeDesigner_DialogFilter");
      }
      if (this.m_oSaveDialog.ShowDialog() != DialogResult.OK)
        return;
      if (this.m_oColorScheme == null)
      {
        int num = (int) MessageBox.Show((IWin32Window) null, "QColorScheme is null", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions) 0);
      }
      this.m_oColorScheme.SaveToXml(this.m_oSaveDialog.FileName, QColorSaveType.ChangedThemeColors);
    }
  }
}
