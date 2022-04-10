// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMainFormOverride
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal sealed class QMainFormOverride
  {
    private IQMainMenu m_oMainMenu;
    private Form m_oForm;
    private Icon m_oActiveMdiChildIcon;
    private Size m_oMdiIconSize = new Size(16, 16);
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QMdiClientSub m_oMdiClientSub;
    private QMainFormSub m_oFormSub;
    private ArrayList m_aMdiChildSubs;
    private Form m_oActiveMdiChild;
    private EventHandler m_oActiveMdiChildChanged;

    public QMainFormOverride(IQMainMenu mainMenu)
    {
      this.m_oMainMenu = mainMenu;
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oActiveMdiChildChanged = new EventHandler(this.MdiForm_ActiveMdiChildChanged);
    }

    public Form MdiForm => this.m_oForm == null || this.m_oForm.IsDisposed || !this.m_oForm.IsMdiContainer ? (Form) null : this.Form;

    public Form Form
    {
      get => this.m_oForm;
      set => this.SetForm(value);
    }

    public Icon ActiveMdiChildIcon => this.m_oActiveMdiChildIcon;

    public Size MdiChildIconSize
    {
      get => this.m_oMdiIconSize;
      set
      {
        if (this.m_oMdiIconSize == value)
          return;
        this.m_oMdiIconSize = value;
        this.SetActiveMdiChildIcon();
      }
    }

    public Form ActiveMdiChild => this.m_oActiveMdiChild;

    public void TryToAssignMdiClient()
    {
      if (this.m_oForm == null || !this.m_oForm.IsMdiContainer)
        return;
      Control mdiClient = (Control) QControlHelper.GetMdiClient(this.m_oForm);
      if (this.m_oMdiClientSub != null && this.m_oMdiClientSub.MdiClientForm != mdiClient)
        this.m_oMdiClientSub.ReleaseHandle();
      if (mdiClient == null || this.m_oMdiClientSub != null)
        return;
      this.m_oMdiClientSub = new QMdiClientSub(mdiClient);
    }

    private void SetForm(Form form)
    {
      if (this.m_oForm == form)
        return;
      if (this.m_oForm != null)
      {
        if (this.m_oMdiClientSub != null)
          this.m_oMdiClientSub.ReleaseHandle();
        if (this.m_oFormSub != null)
          this.m_oFormSub.ReleaseHandle();
        this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oActiveMdiChildChanged);
      }
      this.m_oForm = form;
      if (form == null)
        return;
      this.m_oFormSub = new QMainFormSub(this.m_oMainMenu, this.m_oForm);
      if (form.IsMdiContainer)
        this.TryToAssignMdiClient();
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oActiveMdiChildChanged, (object) form, "MdiChildActivate"));
      this.SetActiveMdiChild(form.ActiveMdiChild);
    }

    public void SetActiveMdiChildIcon()
    {
      if (this.m_oActiveMdiChild != null)
      {
        if (this.m_oActiveMdiChild.Icon == null)
          return;
        if (this.m_oActiveMdiChild.Icon.Size != this.m_oMdiIconSize)
          this.m_oActiveMdiChildIcon = new Icon(this.m_oActiveMdiChild.Icon, this.m_oMdiIconSize);
        else
          this.m_oActiveMdiChildIcon = this.m_oActiveMdiChild.Icon;
      }
      else
        this.m_oActiveMdiChildIcon = (Icon) null;
    }

    private QMdiChildSub GetSubForChild(Form mdiChild)
    {
      if (this.m_aMdiChildSubs == null)
        return (QMdiChildSub) null;
      for (int index = 0; index < this.m_aMdiChildSubs.Count; ++index)
      {
        QMdiChildSub aMdiChildSub = (QMdiChildSub) this.m_aMdiChildSubs[index];
        if (aMdiChildSub.MdiChild == mdiChild)
          return aMdiChildSub;
      }
      return (QMdiChildSub) null;
    }

    internal bool ContainsSubForMdiChild(Form mdiChild) => this.GetSubForChild(mdiChild) != null;

    internal void AddMdiChildSub(Form mdiChild)
    {
      if (this.ContainsSubForMdiChild(mdiChild))
        return;
      if (this.m_aMdiChildSubs == null)
        this.m_aMdiChildSubs = new ArrayList();
      this.m_aMdiChildSubs.Add((object) new QMdiChildSub(this, this.m_oMainMenu, mdiChild));
    }

    internal void RemoveMdiChildSub(QMdiChildSub sub)
    {
      if (this.m_aMdiChildSubs == null || !this.m_aMdiChildSubs.Contains((object) sub))
        return;
      this.m_aMdiChildSubs.Remove((object) sub);
    }

    private void SetActiveMdiChild(Form activeMdiChild)
    {
      Form oActiveMdiChild = this.m_oActiveMdiChild;
      this.m_oActiveMdiChild = activeMdiChild;
      this.SetActiveMdiChildIcon();
      if (this.m_oActiveMdiChild == null)
        return;
      this.AddMdiChildSub(this.m_oActiveMdiChild);
    }

    private void MdiForm_ActiveMdiChildChanged(object sender, EventArgs e)
    {
      if (this.MdiForm != null)
        this.SetActiveMdiChild(this.MdiForm.ActiveMdiChild);
      this.m_oMainMenu.HandleActiveMdiChildChanged();
    }
  }
}
