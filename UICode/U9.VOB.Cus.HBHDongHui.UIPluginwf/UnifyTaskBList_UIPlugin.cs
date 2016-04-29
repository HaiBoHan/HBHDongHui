using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFSoft.UBF.UI.ControlModel;
using UFSoft.UBF.UI.WebControlAdapter;
using UFSoft.UBF.UI.MD.Runtime.Common;
using UFIDA.U9.UI.PDHelper;
using UFIDA.U9.CS.Unify.UnifyTaskUI.UnifyTaskBListUI;

namespace U9.VOB.Cus.HBHDongHui.UIPluginwf
{
    public class UnifyTaskBList_UIPlugin : UFSoft.UBF.UI.Custom.ExtendedPartBase
    {
        //UFSoft.UBF.UI.IView.IPart part;
        UnifyTaskBListUIFormWebPart _strongPart;

        //public const string Const_SaleDeptID = "SaleDept259";
        //IUFDataGrid DataGrid10;
        //IUFFldReferenceColumn itemRef;
        public override void AfterInit(UFSoft.UBF.UI.IView.IPart Part, EventArgs args)
        {
            base.AfterInit(Part, args);

            _strongPart = Part as UnifyTaskBListUIFormWebPart;

            //// Card0
            //string cardName = "Card0";
            //IUFCard card0 = (IUFCard)_strongPart.GetUFControlByName(_strongPart.TopLevelContainer, cardName);

            //{
            //    IUFButton hbh_btnIssue = new UFWebButtonAdapter();
            //    hbh_btnIssue.Text = "下发";
            //    hbh_btnIssue.ID = "hbh_btnIssue";
            //    hbh_btnIssue.AutoPostBack = true;

            //    hbh_btnIssue.Click += new EventHandler(hbh_btnIssue_Click);

            //    //加入Card容器
            //    card0.Controls.Add(hbh_btnIssue);
            //    HBHCommon.HBHCommonUI.UICommonHelper.Layout(card0, hbh_btnIssue, 4, 0);
            //}


            //{
            //    IUFButton hbh_btnUnIssue = new UFWebButtonAdapter();
            //    hbh_btnUnIssue.Text = "取消下发";
            //    hbh_btnUnIssue.ID = "hbh_btnUnIssue";
            //    hbh_btnUnIssue.AutoPostBack = true;

            //    hbh_btnUnIssue.Click += new EventHandler(hbh_btnUnIssue_Click);

            //    //加入Card容器
            //    card0.Controls.Add(hbh_btnUnIssue);
            //    HBHCommon.HBHCommonUI.UICommonHelper.Layout(card0, hbh_btnUnIssue, 6, 0);
            //}
        }

        public override void AfterRender(UFSoft.UBF.UI.IView.IPart Part, EventArgs args)
        {
            base.AfterRender(Part, args);


            // Card0
            string cardName = "Card0";
            IUFCard card0 = (IUFCard)_strongPart.GetUFControlByName(_strongPart.TopLevelContainer, cardName);
            // MenuRelegate
            string btnName = "MenuRelegate";
            IUFMenu btnRelegate = (IUFMenu)_strongPart.GetUFControlByName(card0, btnName);

            if (btnRelegate != null)
            {
                btnRelegate.Enabled = false;
            }
        }
    }
}
