using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFIDA.U9.CBO.Pub.Department.DepartmentUIModel;
using UFSoft.UBF.UI.ControlModel;
using UFSoft.UBF.UI.WebControlAdapter;
using UFSoft.UBF.UI.MD.Runtime.Common;
using UFIDA.U9.UI.PDHelper;

namespace U9.VOB.Cus.HBHDongHui.UIPluginwf
{
    public class Dept_UIPlugin : UFSoft.UBF.UI.Custom.ExtendedPartBase
    {
        UFSoft.UBF.UI.IView.IPart part;
        DepartmentMainUIFormWebPart _strongPart;

        //public const string Const_SaleDeptID = "SaleDept259";
        //IUFDataGrid DataGrid10;
        //IUFFldReferenceColumn itemRef;
        public override void AfterInit(UFSoft.UBF.UI.IView.IPart Part, EventArgs args)
        {
            base.AfterInit(Part, args);

            _strongPart = Part as DepartmentMainUIFormWebPart;

            // Card0
            string cardName = "Card0";
            IUFCard card0 = (IUFCard)part.GetUFControlByName(part.TopLevelContainer, cardName);

            {
                IUFButton hbh_btnIssue = new UFWebButtonAdapter();
                hbh_btnIssue.Text = "下发";
                hbh_btnIssue.ID = "hbh_btnIssue";
                hbh_btnIssue.AutoPostBack = true;

                hbh_btnIssue.Click += new EventHandler(hbh_btnIssue_Click);

                //加入Card容器
                card0.Controls.Add(hbh_btnIssue);
                HBHCommon.HBHCommonUI.UICommonHelper.Layout(card0, hbh_btnIssue, 3, 0);
            }


            {
                IUFButton hbh_btnUnIssue = new UFWebButtonAdapter();
                hbh_btnUnIssue.Text = "取消下发";
                hbh_btnUnIssue.ID = "hbh_btnUnIssue";
                hbh_btnUnIssue.AutoPostBack = true;

                hbh_btnUnIssue.Click += new EventHandler(hbh_btnUnIssue_Click);

                //加入Card容器
                card0.Controls.Add(hbh_btnUnIssue);
                HBHCommon.HBHCommonUI.UICommonHelper.Layout(card0, hbh_btnUnIssue, 5, 0);
            }
        }

        public void hbh_btnIssue_Click(object sender, EventArgs e)
        {

            //    curPart.ShowAtlasModalDialog("8a461af8-d290-4809-ae45-319c199e4232", "归口管理", "992", "504",curPart.TaskId.ToString(),nvs,false,false,false);

            NaviteParamter naviteParamter = new NaviteParamter();
            naviteParamter.NameValues.Add("FromOrg", this.CurrentModel.Project.FocusedRecord.Org.ToString());
            naviteParamter.NameValues.Add("EntityType", this.CurrentModel.Project.EntityFullName);
            naviteParamter.NameValues.Add("EntityID", this.CurrentModel.Project.FocusedRecord.ID.ToString());
            NavigateManager.ShowModelWebpart(base.CurrentPart, "e55f8f78-5485-4c74-a758-8aa9904b7476", 320, 288, naviteParamter);
        }


        public void hbh_btnUnIssue_Click(object sender, EventArgs e)
        {

            //    curPart.ShowAtlasModalDialog("8a461af8-d290-4809-ae45-319c199e4232", "归口管理", "992", "504",curPart.TaskId.ToString(),nvs,false,false,false);
        }

    }
}
