


insert into DHERP..Base_Organization_Trl
select *
from SHERP..Base_Organization_Trl
where ID in (select shOrg.ID
			from SHERP..Base_Organization shOrg
			where ID not in (select dh.ID from DHERP..Base_Organization dh)
				and Code not in (select dh.Code from DHERP..Base_Organization dh)

		)



insert into DHERP..Base_Organization
select *
from SHERP..Base_Organization
where ID not in (select dh.ID from DHERP..Base_Organization dh)
	and Code not in (select dh.Code from DHERP..Base_Organization dh)


--insert into DHERP..Base_Organization_Trl
--select *
--from SHERP..Base_Organization_Trl
--where ID not in (select dh.ID from DHERP..Base_Organization_Trl dh)







--select 
--	sh.ID
--	,dh.ID
--	,sh.CreatedOn
--	,dh.CreatedOn
--	,sh.Code
--	,dh.Code
--from SHERP..Base_Organization sh
--	left join DHERP..Base_Organization dh
--	on sh.Code = dh.Code
--where 
--	sh.ID not in (select dh2.ID from DHERP..Base_Organization dh2)
--	and dh.ID is not null




/*


select *
-- into DHERP..Base_Organization_tmp_20160418
from DHERP..Base_Organization
-- order by CreatedOn desc

select *
-- into DHERP..Base_Organization_Trl_tmp_20160418
from DHERP..Base_Organization_Trl
-- where ID not in (select org.ID from DHERP..Base_Organization org)

--delete from DHERP..Base_Organization_Trl
--where ID not in (select org.ID from DHERP..Base_Organization org)


select *
-- into SHERP..Base_Organization_tmp_20160418
from SHERP..Base_Organization
-- order by CreatedOn desc

select *
-- into SHERP..Base_Organization_Trl_tmp_20160418
from SHERP..Base_Organization_Trl




--select *
--from DHERP..Base_Organization


--delete from DHERP..Base_Organization
--where ID not in (select tmp.ID from DHERP..Base_Organization_tmp_20160418 tmp)


*/