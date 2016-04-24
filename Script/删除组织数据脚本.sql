		
declare @OrgCode varchar(125) = 'SH001' 		
select * from Base_Organization where Code in (@OrgCode)		
		
delete from  	CBO_ItemMaster	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	CBO_Customer	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	CBO_Supplier	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	CBO_Department	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	CBO_Person	 where AttachOrg in (select org.ID from Base_Organization org where org.Code in (@OrgCode)) or CreateOrg in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	CBO_Operators	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	CBO_BankAccount	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	CBO_Account	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	PR_PR	 where Org in (select org.ID from Base_Organization org where org.Code in(@OrgCode))
delete from  	PM_PurchaseOrder	 where Org in (select org.ID from Base_Organization org where org.Code in(@OrgCode))
delete from  	PM_Receivement	 where Org in (select org.ID from Base_Organization org where org.Code in(@OrgCode))
delete from  	PM_Receivement	 where Org in (select org.ID from Base_Organization org where org.Code in(@OrgCode))
delete from  	AP_APBillHead	 where Org in (select org.ID from Base_Organization org where org.Code in(@OrgCode))
delete from  	AP_PayReqBillHead	 where Org in (select org.ID from Base_Organization org where org.Code in(@OrgCode))
delete from  	AP_PayBillHead	 where Org in (select org.ID from Base_Organization org where org.Code in(@OrgCode))
delete from  	AP_APMatchHead	 where Org in (select org.ID from Base_Organization org where org.Code in(@OrgCode))
delete from  	ER_ExpenseRequest	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	ER_ReimburseBillHead	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	ER_LoanBill	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	ER_LoanPaying	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
delete from  	GL_Voucher	 where Org in (select org.ID from Base_Organization org where org.Code in (@OrgCode))
 delete from	Base_Organization	 where Code = @OrgCode
