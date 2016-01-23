using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFIDA.U9.CBO.Pub.Department.DepartmentUIModel;

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

        }
    }
}
