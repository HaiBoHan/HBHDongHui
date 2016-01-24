namespace U9.VOB.Cus.HBHDongHui
{
	using System;
	using System.Collections.Generic;
	using System.Text; 
	using UFSoft.UBF.AopFrame;	
	using UFSoft.UBF.Util.Context;

	/// <summary>
	/// HBHIssueBP partial 
	/// </summary>	
	public partial class HBHIssueBP 
	{	
		internal BaseStrategy Select()
		{
			return new HBHIssueBPImpementStrategy();	
		}		
	}
	
	#region  implement strategy	
	/// <summary>
	/// Impement Implement
	/// 
	/// </summary>	
	internal partial class HBHIssueBPImpementStrategy : BaseStrategy
	{
		public HBHIssueBPImpementStrategy() { }

		public override object Do(object obj)
		{						
			HBHIssueBP bpObj = (HBHIssueBP)obj;
			
			//get business operation context is as follows
			//IContext context = ContextManager.Context	
			
			//auto generating code end,underside is user custom code
			//and if you Implement replace this Exception Code...
			throw new NotImplementedException();
		}		
	}

	#endregion
	
	
}