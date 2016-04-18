



select *
from UBF_MD_Class
where 
	-- FullName like '%Departmen%'
	-- FullName = 'UFIDA.U9.CBO.HR.Department.Department'
	Local_ID = 1001101157664583


select 
	obj.ID,entity.Local_ID,entity.FullName
	,obj.ObjType
	,entity.FullName
	-- ,obj.BaseObject
	,obj.U_ID,entity.ID
	,obj.*
	,entity.*
from CBO_BaseObject obj
	inner join UBF_MD_Class entity
	on obj.EntityType = entity.Local_ID
where
	-- entity.FullName = 'UFIDA.U9.CBO.HR.Department.Department'

	entity.FullName = 'UFIDA.U9.CBO.SCM.Customer.Customer'


	select *
	from CBO_SendObject

	select *
	from CBO_ObjectAttribute



	select *
	from CBO_AttributeController


--select *
--from CBO_BaseObject_Trl


update CBO_BaseObject
set
	ObjType = 1
	,IsSend = 1
where EntityType in (select Local_ID from UBF_MD_Class where FullName = 'UFIDA.U9.CBO.HR.Department.Department')


if not exists(select 1 from CBO_BaseObject where EntityType in (select Local_ID from UBF_MD_Class where FullName = 'UFIDA.U9.CBO.HR.Department.Department') )
begin

	insert into CBO_BaseObject
	( ID,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,SysVersion,EntityType,ObjType,IsSend,OrgField,U_ID

	)
	--values(
	--1,NULL,'2015-10-04',admin,1,1001101157665606,1,1,'Org','C15A4E15-4787-4382-ABD5-576313B40F17'
	--)
	select
		ID = (select IsNull(max(IsNull(ID,1)),1) + 1 from CBO_BaseObject)
		,GetDate()
		,'hbh'
		,GetDate()
		,'hbh'
		,1
		,EntityType = entity.Local_ID
		,IsSend = 1
		,OrgField = 'Org'
		,U_ID = entity.ID
	from UBF_MD_Class  entity
	where FullName = 'UFIDA.U9.CBO.HR.Department.Department'

end
