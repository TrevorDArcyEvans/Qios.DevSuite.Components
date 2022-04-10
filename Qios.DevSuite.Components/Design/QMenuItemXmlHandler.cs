// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QMenuItemXmlHandler
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  internal class QMenuItemXmlHandler
  {
    private ComponentDesigner m_oDesigner;
    private QMenuItemCollection m_oMenuItems;
    private DesignerVerbCollection m_oVerbs;
    private SaveFileDialog m_oSaveDialog;
    private OpenFileDialog m_oOpenDialog;
    private IDesignerHost m_oDesignerHost;

    public QMenuItemXmlHandler(
      ComponentDesigner designer,
      IDesignerHost designerHost,
      QMenuItemCollection menuItems)
    {
      this.m_oDesigner = designer;
      this.m_oDesignerHost = designerHost;
      this.m_oMenuItems = menuItems;
    }

    public DesignerVerbCollection Verbs
    {
      get
      {
        if (this.m_oVerbs == null)
        {
          this.m_oVerbs = new DesignerVerbCollection();
          this.m_oVerbs.Add(new DesignerVerb("Load items from XML", new EventHandler(this.LoadItemsFromXml)));
          this.m_oVerbs.Add(new DesignerVerb("Save items To XML", new EventHandler(this.SaveItemsToXml)));
        }
        return this.m_oVerbs;
      }
    }

    private void LoadItemsFromXml(object sender, EventArgs e)
    {
      if (this.m_oOpenDialog == null)
      {
        this.m_oOpenDialog = new OpenFileDialog();
        this.m_oOpenDialog.Title = QResources.GetGeneral("QMenuItemContainerDesigner_OpenDialogTitle");
        this.m_oOpenDialog.DefaultExt = QResources.GetGeneral("QMenuItemContainerDesigner_DialogDefaultExtension");
        this.m_oOpenDialog.Filter = QResources.GetGeneral("QMenuItemContainerDesigner_DialogFilter");
      }
      if (this.m_oOpenDialog.ShowDialog() != DialogResult.OK)
        return;
      DesignerTransaction transaction = this.m_oDesignerHost.CreateTransaction();
      try
      {
        this.RemoveMenuItemsFromDesignerHost(this.m_oMenuItems);
        this.m_oMenuItems.LoadFromXml(this.m_oOpenDialog.FileName, QMenuItemLoadType.CreateNewItems);
        this.AddMenuItemsToDesignerHost(this.m_oMenuItems);
      }
      catch
      {
        transaction.Cancel();
        throw;
      }
      transaction.Commit();
    }

    private void SaveItemsToXml(object sender, EventArgs e)
    {
      if (this.m_oSaveDialog == null)
      {
        this.m_oSaveDialog = new SaveFileDialog();
        this.m_oSaveDialog.Title = QResources.GetGeneral("QMenuItemContainerDesigner_SaveDialogTitle");
        this.m_oSaveDialog.DefaultExt = QResources.GetGeneral("QMenuItemContainerDesigner_DialogDefaultExtension");
        this.m_oSaveDialog.Filter = QResources.GetGeneral("QMenuItemContainerDesigner_DialogFilter");
      }
      if (this.m_oSaveDialog.ShowDialog() != DialogResult.OK)
        return;
      if (this.m_oMenuItems == null)
      {
        int num = (int) MessageBox.Show((IWin32Window) null, "Items are null", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions) 0);
      }
      this.m_oMenuItems.SaveToXml(this.m_oSaveDialog.FileName, QMenuItemSaveType.CompleteMenuItem);
    }

    private void RemoveMenuItemsFromDesignerHost(QMenuItemCollection collection)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        IComponent component = (IComponent) collection[index];
        if (collection[index].HasChildItems)
          this.RemoveMenuItemsFromDesignerHost(collection[index].MenuItems);
        this.m_oDesignerHost.DestroyComponent(component);
      }
    }

    private void AddMenuItemsToDesignerHost(QMenuItemCollection collection)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        IComponent component = (IComponent) collection[index];
        if (component != null && component.Site == null)
        {
          if (collection[index].DesignName != null && this.m_oDesignerHost.Container.Components[collection[index].DesignName] == null)
            this.m_oDesignerHost.Container.Add(component, collection[index].DesignName);
          else
            this.m_oDesignerHost.Container.Add(component);
          if (collection[index].HasChildItems)
            this.AddMenuItemsToDesignerHost(collection[index].MenuItems);
        }
      }
    }
  }
}
