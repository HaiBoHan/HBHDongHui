using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFSoft.UBF.UI.ControlModel;
using UFSoft.UBF.UI.WebControlAdapter;
using UFSoft.UBF.UI.MD.Runtime.Common;
using UFIDA.U9.UI.PDHelper;
using UFIDA.U9.CBO.Pub.Operator.OperatorUIModel;

namespace U9.VOB.Cus.HBHDongHui.UIPluginwf
{
    public class Operator_UIPlugin : UFSoft.UBF.UI.Custom.ExtendedPartBase
    {
        //UFSoft.UBF.UI.IView.IPart part;
        OperatorMainUIFormWebPart _strongPart;

        //public const string Const_SaleDeptID = "SaleDept259";
        //IUFDataGrid DataGrid10;
        //IUFFldReferenceColumn itemRef;
        public override void AfterInit(UFSoft.UBF.UI.IView.IPart Part, EventArgs args)
        {
            base.AfterInit(Part, args);

            _strongPart = Part as OperatorMainUIFormWebPart;

            // Card0
            string cardName = "Card0";
            IUFCard card0 = (IUFCard)_strongPart.GetUFControlByName(_strongPart.TopLevelContainer, cardName);

            {
                IUFButton hbh_btnIssue = new UFWebButtonAdapter();
                hbh_btnIssue.Text = "下发";
                hbh_btnIssue.ID = "hbh_btnIssue";
                hbh_btnIssue.AutoPostBack = true;

                hbh_btnIssue.Click += new EventHandler(hbh_btnIssue_Click);

                //加入Card容器
                card0.Controls.Add(hbh_btnIssue);
                HBHCommon.HBHCommonUI.UICommonHelper.Layout(card0, hbh_btnIssue, 2, 0);
            }


            //{
            //    IUFButton hbh_btnUnIssue = new UFWebButtonAdapter();
            //    hbh_btnUnIssue.Text = "取消下发";
            //    hbh_btnUnIssue.ID = "hbh_btnUnIssue";
            //    hbh_btnUnIssue.AutoPostBack = true;

            //    hbh_btnUnIssue.Click += new EventHandler(hbh_btnUnIssue_Click);

            //    //加入Card容器
            //    card0.Controls.Add(hbh_btnUnIssue);
            //    HBHCommon.HBHCommonUI.UICommonHelper.Layout(card0, hbh_btnUnIssue, 4, 0);
            //}
        }

        public void hbh_btnIssue_Click(object sender, EventArgs e)
        {
            //    curPart.ShowAtlasModalDialog("8a461af8-d290-4809-ae45-319c199e4232", "归口管理", "992", "504",curPart.TaskId.ToString(),nvs,false,false,false);

            IssueClick(false);
        }

        public void hbh_btnUnIssue_Click(object sender, EventArgs e)
        {
            //    curPart.ShowAtlasModalDialog("8a461af8-d290-4809-ae45-319c199e4232", "归口管理", "992", "504",curPart.TaskId.ToString(),nvs,false,false,false);

            IssueClick(true);
        }


        private void IssueClick(bool isUnIssue)
        {

            HBHCommon.HBHCommonUI.UICommonHelper.ClearErrorInfo(_strongPart);

            OperatorsRecord focused = _strongPart.Model.Operators.FocusedRecord;

            if (focused != null)
            {
                NaviteParamter naviteParamter = new NaviteParamter();
                naviteParamter.NameValues.Add("FromOrg", focused.Org.ToString());
                naviteParamter.NameValues.Add("EntityType", _strongPart.Model.Operators.EntityFullName);
                naviteParamter.NameValues.Add("EntityID", focused.ID.ToString());
                naviteParamter.NameValues.Add("UnIssue", isUnIssue.ToString());
                NavigateManager.ShowModelWebpart(_strongPart, "a03cdb71-d70b-4cf4-96d2-6ea1e7f28ac7", 410, 370, naviteParamter);
            }
            else
            {
                HBHCommon.HBHCommonUI.UICommonHelper.ShowErrorInfo(_strongPart, "必须指定下发实体!");
            }
        }
    }
}
