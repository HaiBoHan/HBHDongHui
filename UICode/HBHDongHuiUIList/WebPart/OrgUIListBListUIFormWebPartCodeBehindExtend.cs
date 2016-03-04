using System;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Threading;

using Telerik.WebControls;
using UFSoft.UBF.UI.WebControls;
using UFSoft.UBF.UI.Controls;
using UFSoft.UBF.Util.Log;
using UFSoft.UBF.Util.Globalization;

using UFSoft.UBF.UI.IView;
using UFSoft.UBF.UI.Engine;
using UFSoft.UBF.UI.MD.Runtime;
using UFSoft.UBF.UI.ActionProcess;
using UFSoft.UBF.UI.WebControls.ClientCallBack;
using U9.VOB.Cus.HBHDongHui.Proxy;
using System.Collections.Generic;
using HBH.DoNet.DevPlatform.EntityMapping;



/***********************************************************************************************
 * Form ID: 
 * UIFactory Auto Generator 
 ***********************************************************************************************/
namespace OrgUIListBListUIModel
{
    public partial class OrgUIListBListUIFormWebPart
    {
        #region Custome eventBind
	
		 

			

		//DDLCase_TextChanged...
		private void DDLCase_TextChanged_Extend(object sender, EventArgs  e)
		{
			//调用模版提供的默认实现.--默认实现可能会调用相应的Action.
			
		
			DDLCase_TextChanged_DefaultImpl(sender,e);
		}	
		 
				//OnLookCase_Click...
		private void OnLookCase_Click_Extend(object sender, EventArgs  e)
		{
			//调用模版提供的默认实现.--默认实现可能会调用相应的Action.
			
		
			OnLookCase_Click_DefaultImpl(sender,e);
		}	
		 
				//BtnDelete_Click...
        private void BtnDelete_Click_Extend(object sender, EventArgs e)
        {
            //调用模版提供的默认实现.--默认实现可能会调用相应的Action.
            BtnDelete_Click_DefaultImpl(sender, e);
        }
		 
				//BtnOK_Click...
        private void BtnOK_Click_Extend(object sender, EventArgs e)
        {
            //调用模版提供的默认实现.--默认实现可能会调用相应的Action.
            //BtnOK_Click_DefaultImpl(sender,e);

            long[] targetOrgs = this.Model.Organization.GetSelectedRecordIDs();

            if (targetOrgs != null
                && targetOrgs.Length > 0
                )
            {
                HBHIssueBPProxy proxy = new HBHIssueBPProxy();

                proxy.TargetOrgs = new List<long>();
                foreach (long org in targetOrgs)
                {
                    proxy.TargetOrgs.Add(org);
                }

                object objEntityType = this.NameValues["EntityType"];
                if (objEntityType != null)
                {
                    proxy.EntityType = objEntityType.ToString();
                }

                proxy.EntityIDs = new List<long>();
                long entityID = PubClass.GetLong(this.NameValues["EntityID"]);
                if (entityID > 0)
                {
                    proxy.EntityIDs.Add(entityID);
                }
                else
                {
                    U9.VOB.HBHCommon.HBHCommonUI.UICommonHelper.ShowErrorInfo(this, "实体ID不可为空!");
                    return;
                }

                proxy.UnIssue = PubClass.GetBool(this.NameValues["UnIssue"]);

                proxy.Do();

                this.CloseDialog(false);
            }
            else
            {
                U9.VOB.HBHCommon.HBHCommonUI.UICommonHelper.ShowErrorInfo(this, "必须选择下发组织!");
                return;
            }
        }
		 
				//BtnClose_Click...
		private void BtnClose_Click_Extend(object sender, EventArgs  e)
		{
			//调用模版提供的默认实现.--默认实现可能会调用相应的Action.
            //BtnClose_Click_DefaultImpl(sender,e);

            this.CloseDialog(false);
		}	
		 
			
				

		//DataGrid1_GridRowDbClicked...
		private void DataGrid1_GridRowDbClicked_Extend(object sender, GridDBClickEventArgs  e)
		{
			//调用模版提供的默认实现.--默认实现可能会调用相应的Action.
			
		
			DataGrid1_GridRowDbClicked_DefaultImpl(sender,e);
        }

        #endregion

		
            
            

		#region 自定义数据初始化加载和数据收集
		private void OnLoadData_Extend(object sender)
		{
			OnLoadData_DefaultImpl(sender);
		}
		private void OnDataCollect_Extend(object sender)
		{	
			OnDataCollect_DefaultImpl(sender);
		}
		#endregion  

        #region 自己扩展 Extended Event handler 
		public void AfterOnLoad()
		{
			
			AfterOnLoad_Qry_DefaultImpl();//BE列表自动产生的代码
	    
		}

        public void AfterCreateChildControls()
        {
									
			AfterCreateChildControls_Qry_DefaultImpl();//BE列表自动产生的代码

            // 启用页面个性化 
            UFIDA.U9.UI.PDHelper.PersonalizationHelper.SetPersonalizationEnable(this, true);		
        }
        
        public void AfterEventBind()
        {
        }
        
		public void BeforeUIModelBinding()
		{
								
		}

		public void AfterUIModelBinding()
		{
									
			AfterUIModelBinding_Qry_DefaultImpl();//BE列表自动产生的代码


            //U9.VOB.HBHCommon.HBHCommonUI.HBHUIHelper.UIList_SetDocNoTitleClick(this, this.DataGrid1
            //    , "ID"
            //    , "DocNo"
            //    , "d5d270d8-ba8a-4a9d-98c3-c3e7cac03135"
            //    , "售后服务单"
            //    // , param
            //    );
		}


        #endregion
		
    }
}