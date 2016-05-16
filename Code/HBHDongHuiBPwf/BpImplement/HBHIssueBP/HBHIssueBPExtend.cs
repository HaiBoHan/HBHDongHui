namespace U9.VOB.Cus.HBHDongHui
{
	using System;
	using System.Collections.Generic;
	using System.Text; 
	using UFSoft.UBF.AopFrame;	
	using UFSoft.UBF.Util.Context;
    using HBH.DoNet.DevPlatform.EntityMapping;
    using UFSoft.UBF.Business;
    using System.Collections;
    using UFIDA.U9.CBO.Pub.Controller;
    using UFIDA.U9.Base.Organization;
    using UFIDA.U9.CBO.HR.Person;
    using UFIDA.U9.CBO.HR.Enums;
    using UFIDA.U9.CBO.HR.Operator;
    using UFIDA.U9.CBO.HR.Department;
    using UFSoft.UBF.PL;
    using UFIDA.U9.Base;

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
	
    //#region  implement strategy	
	/// <summary>
	/// Impement Implement
	/// 
	/// </summary>	
	internal partial class HBHIssueBPImpementStrategy : BaseStrategy
    {
        string personEntityFullName = typeof(Person).FullName;
        string entityType = string.Empty;

        private List<string> sysFields;

        public List<string> SysFields
        {
            get
            {
                if(sysFields == null
                    || sysFields.Count == 0
                    )
                {
                    sysFields = new List<string>();
                    sysFields.Add("ID");
                    sysFields.Add("SysVersion");
                    sysFields.Add("CreatedOn");
                    sysFields.Add("CreatedBy");
                    sysFields.Add("ModifiedOn");
                    sysFields.Add("ModifiedBy");
                }

                return sysFields;
            }
        }


		public HBHIssueBPImpementStrategy() { }

		public override object Do(object obj)
		{						
			HBHIssueBP bpObj = (HBHIssueBP)obj;
			
			//get business operation context is as follows
			//IContext context = ContextManager.Context	
			
			//auto generating code end,underside is user custom code
			//and if you Implement replace this Exception Code...
            //throw new NotImplementedException();

            if (PubClass.IsNull(bpObj.EntityType))
            {
                throw new BusinessException("实体类型不可为空。");
            }

            if (bpObj.TargetOrgs == null
                || bpObj.TargetOrgs.Count == 0
                )
            {
                throw new BusinessException("目标组织不可为空。");
            }

            if (bpObj.EntityIDs == null
                || bpObj.EntityIDs.Count == 0
                )
            {
                throw new BusinessException("实体ID不可为空。");
            }

            if (bpObj.UnIssue
                )
            {
                throw new BusinessException("没有实现取消下发。");
            }

            entityType = bpObj.EntityType;

            //UFIDA.U9.CBO.HR.Department.Department dept; dept.ModifiedBy;
            //UFIDA.U9.CBO.HR.Person.Person person;person.EmployeeArchives[0].EmployeeCode;


            using (ISession session = Session.Open())
            {
                bool isCreate = false;

                foreach (long org in bpObj.TargetOrgs)
                {
                    if (org > 0)
                    {
                        Organization current = Organization.Finder.FindByID(org);

                        if (current != null)
                        {
                            foreach (long id in bpObj.EntityIDs)
                            {
                                if (id > 0)
                                {
                                    BusinessEntity.EntityKey key = new BusinessEntity.EntityKey(id, entityType);
                                    if (key != null)
                                    {
                                        BusinessEntity entity = key.GetEntity();

                                        if (entity != null)
                                        {
                                            string codeField = "Code";
                                            string orgField = "Org";

                                            if (entityType == personEntityFullName)
                                            {
                                                codeField = "PersonID";
                                                orgField = "CreateOrg";
                                            }
                                            //else
                                            //{
                                            //    businessEntity2.SetValue("Org", org);
                                            //}

                                            Hashtable hs = new Hashtable();
                                            hs.Add(codeField, "");
                                            hs.Add(orgField, "");

                                            hs[orgField] = org;
                                            //hs[codeField] = GetEntityCode(entity, codeField);
                                            {
                                                object code = entity.GetValue(codeField);

                                                if (entityType == personEntityFullName)
                                                {
                                                    string strPersonID = code.GetString();
                                                    string newOrgCode = current.Code;

                                                    long oldOrgID = entity.GetValue(orgField).GetLong();

                                                    if (oldOrgID > 0)
                                                    {
                                                        Organization oldOrg = Organization.Finder.FindByID(oldOrgID);
                                                        if (oldOrg != null)
                                                        {
                                                            string oldOrgCode = oldOrg.Code;

                                                            strPersonID = newOrgCode + strPersonID.Replace(oldOrgCode, "");
                                                        }
                                                    }

                                                    hs[codeField] = strPersonID;
                                                }
                                                else
                                                {
                                                    hs[codeField] = code;
                                                }
                                            }

                                            //BusinessEntity targetEntity = BusinessEntity.
                                            //entity.CopyTo(

                                            //Entity.EntityFinder entityFinder = new Entity.EntityFinder(fullname);
                                            //result = (BusinessEntity)entityFinder.Find(stringBuilder.ToString(), oqlParamList.ToArray());

                                            BusinessEntity businessEntity2 = UFIDA.U9.CBO.Pub.Controller.Helper.FindByBusinessKey(entityType, hs, org);
                                            if (businessEntity2 != null)
                                            {
                                                throw new CodeExsitsException(entity.MDEntity.DisplayName, (string)entity.GetValue(codeField), current.Name);
                                            }
                                            businessEntity2 = (BusinessEntity)UFSoft.UBF.Business.Entity.Create(entityType, null);


                                            //Dictionary<string, object> dicValues = new Dictionary<string, object>();
                                            //foreach (string field in SysFields)
                                            //{
                                            //    dicValues.Add(field, businessEntity2.GetValue(field));
                                            //}

                                            //entity.CopyTo(businessEntity2);

                                            //foreach (string field in dicValues.Keys)
                                            //{
                                            //    //dicValues.Add(field, businessEntity2.GetValue(field));
                                            //    businessEntity2.SetValue(field, dicValues[field]);
                                            //}

                                            CopyTo(entity, businessEntity2);

                                            if (!isCreate)
                                                isCreate = true;

                                            //businessEntity2.SetValue("MasterOrg", entity.GetValue("Org"));

                                            if (entityType == personEntityFullName)
                                            {
                                                Person person1 = (Person)entity;
                                                Person person2 = (Person)businessEntity2;

                                                person2.AttachOrg = current;
                                                person2.CreateOrg = current;
                                                //person1.LogisticsOrg = OrgTypeEnum.EmployOrg;
                                                //person1.LabourContractOrg = OrgTypeEnum.HROrg;
                                                //person1.PayOrg = OrgTypeEnum.HROrg;
                                                //person1.HRChangeOrg = OrgTypeEnum.HROrg;

                                                person2.LinkMan = null;

                                                CreatePersonChild(person1, person2);
                                            }
                                            else
                                            {
                                                businessEntity2.SetValue("Org", org);
                                            }

                                            if (entityType == typeof(Operators).FullName)
                                            {
                                                Operators src = (Operators)entity;
                                                Operators target = (Operators)businessEntity2;

                                                if (target.Dept != null)
                                                {
                                                    target.Dept = Department.Finder.Find("Org=@Org and Code=@Code"
                                                        , new OqlParam(org)
                                                        , new OqlParam(target.Dept.Code)
                                                        );
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (isCreate)
                {
                    session.Commit();
                }
                else
                {
                    session.Abort();
                }
            }
            return null;
		}

        private void CreatePersonChild(Person person1, Person person2)
        {
            if (person1 != null
                && person2 != null
                )
            {
                // Person_EmployeeArchives
                CopyPersonChild<EmployeeArchive>(person2, person1.EmployeeArchives);

                // Person_DiplomaInfos
                CopyPersonChild<DiplomaInfo>(person2, person1.DiplomaInfos);

                // Person_EducationTrainning
                CopyPersonChild<EducationTrainning>(person2, person1.EducationTrainning);

                // Person_EmpJobTitleLevelRecords
                CopyPersonChild<EmpJobTitleLevelRecord>(person2, person1.EmpJobTitleLevelRecords);

                // Person_EmployeeAssignments
                CopyPersonChild<EmployeeAssignment>(person2, person1.EmployeeAssignments);

                // Person_EmployeeContracts
                CopyPersonChild<EmployeeContract>(person2, person1.EmployeeContracts);

                // Person_EmployeeJobRelations
                CopyPersonChild<EmployeeJobRelation>(person2, person1.EmployeeJobRelations);

                // Person_EmployeeResposibilitys
                CopyPersonChild<EmployeeResposibility>(person2, person1.EmployeeResposibilitys);

                // Person_EmployeeSalaryFiles
                CopyPersonChild<EmployeeSalaryFile>(person2, person1.EmployeeSalaryFiles);

                // Person_EmpolyeeCateAlterRecords
                CopyPersonChild<EmpolyeeCateAlterRecord>(person2, person1.EmpolyeeCateAlterRecords);

                // Person_EmpolyeeEndSalaryRecords
                CopyPersonChild<EmpolyeeEndSalaryRecord>(person2, person1.EmpolyeeEndSalaryRecords);

                // Person_EmpSendOutRecords
                CopyPersonChild<EmpSendOutRecord>(person2, person1.EmpSendOutRecords);

                // Person_FamilySocialRelations
                CopyPersonChild<FamilySocialRelation>(person2, person1.FamilySocialRelations);

                // Person_HRAttachRelations
                CopyPersonChild<HRAttachRelation>(person2, person1.HRAttachRelations);

                // Person_JobHistoryInfos
                CopyPersonChild<JobHistoryInfo>(person2, person1.JobHistoryInfos);

                // Person_JobTitleInfos
                CopyPersonChild<JobTitleInfo>(person2, person1.JobTitleInfos);

                // Person_PersonalCalendars
                CopyPersonChild<PersonalCalendar>(person2, person1.PersonalCalendars);

                // Person_PersonExtend1
                CopyPersonChild<PersonExtend1>(person2, person1.PersonExtend1);

                // Person_PersonExtend2
                CopyPersonChild<PersonExtend2>(person2, person1.PersonExtend2);

                // Person_PersonExtend3
                CopyPersonChild<PersonExtend3>(person2, person1.PersonExtend3);

                // Person_PersonExtend4
                CopyPersonChild<PersonExtend4>(person2, person1.PersonExtend4);

                // Person_PersonExtend5
                CopyPersonChild<PersonExtend5>(person2, person1.PersonExtend5);

                // Person_PersonExtend6
                CopyPersonChild<PersonExtend6>(person2, person1.PersonExtend6);

                // Person_PersonExtend7
                CopyPersonChild<PersonExtend7>(person2, person1.PersonExtend7);

                // Person_PersonExtend8
                CopyPersonChild<PersonExtend8>(person2, person1.PersonExtend8);

                // Person_PersonExtend9
                CopyPersonChild<PersonExtend9>(person2, person1.PersonExtend9);

                // Person_PersonExtend10
                CopyPersonChild<PersonExtend10>(person2, person1.PersonExtend10);

                // Person_PersonExtend11
                CopyPersonChild<PersonExtend11>(person2, person1.PersonExtend11);

                // Person_PersonExtend12
                CopyPersonChild<PersonExtend12>(person2, person1.PersonExtend12);

                // Person_PersonExtend13
                CopyPersonChild<PersonExtend13>(person2, person1.PersonExtend13);

                // Person_PersonExtend14
                CopyPersonChild<PersonExtend14>(person2, person1.PersonExtend14);

                // Person_PersonExtend15
                CopyPersonChild<PersonExtend15>(person2, person1.PersonExtend15);

                // Person_PersonExtend16
                CopyPersonChild<PersonExtend16>(person2, person1.PersonExtend16);

                // Person_PersonExtend17
                CopyPersonChild<PersonExtend17>(person2, person1.PersonExtend17);

                // Person_PersonExtend18
                CopyPersonChild<PersonExtend18>(person2, person1.PersonExtend18);

                // Person_PersonExtend19
                CopyPersonChild<PersonExtend19>(person2, person1.PersonExtend19);

                // Person_PersonExtend20
                CopyPersonChild<PersonExtend20>(person2, person1.PersonExtend20);

                // Person_RetireReturnRecords
                CopyPersonChild<RetireReturnRecord>(person2, person1.RetireReturnRecords);

                // Person_WorkQualificationInfos
                CopyPersonChild<WorkQualificationInfo>(person2, person1.WorkQualificationInfos);


            }
        }

        private void CreatePersonChild_Old(Person person1, Person person2)
        {
            if (person1 != null
                && person2 != null
                )
            {
                // Person_EmployeeArchives
                if (person1.EmployeeArchives != null
                    && person1.EmployeeArchives.Count > 0
                    )
                {
                    foreach (EmployeeArchive arch in person1.EmployeeArchives)
                    {
                        if (arch != null)
                        {
                            //CreateEmployeeArchive(person2, arch);

                            //EmployeeArchive arch2 = EmployeeArchive.Create(person2);
                            //CopyTo(arch, arch2);

                            CreatePersonChild<EmployeeArchive>(person2, arch);
                        }
                    }
                }
                CopyPersonChild<EmployeeArchive>(person2, person1.EmployeeArchives);


                // Person_DiplomaInfos
                if (person1.DiplomaInfos != null
                    && person1.DiplomaInfos.Count > 0
                    )
                {
                    foreach (DiplomaInfo srcChild in person1.DiplomaInfos)
                    {
                        if (srcChild != null)
                        {
                            DiplomaInfo child2 = DiplomaInfo.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EducationTrainning
                if (person1.EducationTrainning != null
                    && person1.EducationTrainning.Count > 0
                    )
                {
                    foreach (EducationTrainning srcChild in person1.EducationTrainning)
                    {
                        if (srcChild != null)
                        {
                            EducationTrainning child2 = EducationTrainning.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmpJobTitleLevelRecords
                if (person1.EmpJobTitleLevelRecords != null
                    && person1.EmpJobTitleLevelRecords.Count > 0
                    )
                {
                    foreach (EmpJobTitleLevelRecord srcChild in person1.EmpJobTitleLevelRecords)
                    {
                        if (srcChild != null)
                        {
                            EmpJobTitleLevelRecord child2 = EmpJobTitleLevelRecord.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmployeeAssignments
                if (person1.EmployeeAssignments != null
                    && person1.EmployeeAssignments.Count > 0
                    )
                {
                    foreach (EmployeeAssignment srcChild in person1.EmployeeAssignments)
                    {
                        if (srcChild != null)
                        {
                            EmployeeAssignment child2 = EmployeeAssignment.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmployeeContracts
                if (person1.EmployeeContracts != null
                    && person1.EmployeeContracts.Count > 0
                    )
                {
                    foreach (EmployeeContract srcChild in person1.EmployeeContracts)
                    {
                        if (srcChild != null)
                        {
                            EmployeeContract child2 = EmployeeContract.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmployeeJobRelations
                if (person1.EmployeeJobRelations != null
                    && person1.EmployeeJobRelations.Count > 0
                    )
                {
                    foreach (EmployeeJobRelation srcChild in person1.EmployeeJobRelations)
                    {
                        if (srcChild != null)
                        {
                            EmployeeJobRelation child2 = EmployeeJobRelation.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmployeeResposibilitys
                if (person1.EmployeeResposibilitys != null
                    && person1.EmployeeResposibilitys.Count > 0
                    )
                {
                    foreach (EmployeeResposibility srcChild in person1.EmployeeResposibilitys)
                    {
                        if (srcChild != null)
                        {
                            EmployeeResposibility child2 = EmployeeResposibility.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmployeeSalaryFiles
                if (person1.EmployeeSalaryFiles != null
                    && person1.EmployeeSalaryFiles.Count > 0
                    )
                {
                    foreach (EmployeeSalaryFile srcChild in person1.EmployeeSalaryFiles)
                    {
                        if (srcChild != null)
                        {
                            EmployeeSalaryFile child2 = EmployeeSalaryFile.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmpolyeeCateAlterRecords
                if (person1.EmpolyeeCateAlterRecords != null
                    && person1.EmpolyeeCateAlterRecords.Count > 0
                    )
                {
                    foreach (EmpolyeeCateAlterRecord srcChild in person1.EmpolyeeCateAlterRecords)
                    {
                        if (srcChild != null)
                        {
                            EmpolyeeCateAlterRecord child2 = EmpolyeeCateAlterRecord.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmpolyeeEndSalaryRecords
                if (person1.EmpolyeeEndSalaryRecords != null
                    && person1.EmpolyeeEndSalaryRecords.Count > 0
                    )
                {
                    foreach (EmpolyeeEndSalaryRecord srcChild in person1.EmpolyeeEndSalaryRecords)
                    {
                        if (srcChild != null)
                        {
                            EmpolyeeEndSalaryRecord child2 = EmpolyeeEndSalaryRecord.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_EmpSendOutRecords
                if (person1.EmpSendOutRecords != null
                    && person1.EmpSendOutRecords.Count > 0
                    )
                {
                    foreach (EmpSendOutRecord srcChild in person1.EmpSendOutRecords)
                    {
                        if (srcChild != null)
                        {
                            EmpSendOutRecord child2 = EmpSendOutRecord.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_FamilySocialRelations
                if (person1.FamilySocialRelations != null
                    && person1.FamilySocialRelations.Count > 0
                    )
                {
                    foreach (FamilySocialRelation srcChild in person1.FamilySocialRelations)
                    {
                        if (srcChild != null)
                        {
                            FamilySocialRelation child2 = FamilySocialRelation.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_HRAttachRelations
                if (person1.HRAttachRelations != null
                    && person1.HRAttachRelations.Count > 0
                    )
                {
                    foreach (HRAttachRelation srcChild in person1.HRAttachRelations)
                    {
                        if (srcChild != null)
                        {
                            HRAttachRelation child2 = HRAttachRelation.Create(person2);
                            CopyTo(srcChild, child2);

                            // ?????
                            // Person_HRAttachRelations_HRActivityRelations
                        }
                    }
                }

                // Person_JobHistoryInfos
                if (person1.JobHistoryInfos != null
                    && person1.JobHistoryInfos.Count > 0
                    )
                {
                    foreach (JobHistoryInfo srcChild in person1.JobHistoryInfos)
                    {
                        if (srcChild != null)
                        {
                            JobHistoryInfo child2 = JobHistoryInfo.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_JobTitleInfos
                if (person1.JobTitleInfos != null
                    && person1.JobTitleInfos.Count > 0
                    )
                {
                    foreach (JobTitleInfo srcChild in person1.JobTitleInfos)
                    {
                        if (srcChild != null)
                        {
                            JobTitleInfo child2 = JobTitleInfo.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonalCalendars
                if (person1.PersonalCalendars != null
                    && person1.PersonalCalendars.Count > 0
                    )
                {
                    foreach (PersonalCalendar srcChild in person1.PersonalCalendars)
                    {
                        if (srcChild != null)
                        {
                            PersonalCalendar child2 = PersonalCalendar.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend1
                if (person1.PersonExtend1 != null
                    && person1.PersonExtend1.Count > 0
                    )
                {
                    foreach (PersonExtend1 srcChild in person1.PersonExtend1)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend1 child2 = PersonExtend1.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }
                // Person_PersonExtend2
                if (person1.PersonExtend2 != null
                    && person1.PersonExtend2.Count > 0
                    )
                {
                    foreach (PersonExtend2 srcChild in person1.PersonExtend2)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend2 child2 = PersonExtend2.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend3
                if (person1.PersonExtend3 != null
                    && person1.PersonExtend3.Count > 0
                    )
                {
                    foreach (PersonExtend3 srcChild in person1.PersonExtend3)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend3 child2 = PersonExtend3.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend4
                if (person1.PersonExtend4 != null
                    && person1.PersonExtend4.Count > 0
                    )
                {
                    foreach (PersonExtend4 srcChild in person1.PersonExtend4)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend4 child2 = PersonExtend4.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend5
                if (person1.PersonExtend5 != null
                    && person1.PersonExtend5.Count > 0
                    )
                {
                    foreach (PersonExtend5 srcChild in person1.PersonExtend5)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend5 child2 = PersonExtend5.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend6
                if (person1.PersonExtend6 != null
                    && person1.PersonExtend6.Count > 0
                    )
                {
                    foreach (PersonExtend6 srcChild in person1.PersonExtend6)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend6 child2 = PersonExtend6.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend7
                if (person1.PersonExtend7 != null
                    && person1.PersonExtend7.Count > 0
                    )
                {
                    foreach (PersonExtend7 srcChild in person1.PersonExtend7)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend7 child2 = PersonExtend7.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend8
                if (person1.PersonExtend8 != null
                    && person1.PersonExtend8.Count > 0
                    )
                {
                    foreach (PersonExtend8 srcChild in person1.PersonExtend8)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend8 child2 = PersonExtend8.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend9
                if (person1.PersonExtend9 != null
                    && person1.PersonExtend9.Count > 0
                    )
                {
                    foreach (PersonExtend9 srcChild in person1.PersonExtend9)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend9 child2 = PersonExtend9.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend10
                if (person1.PersonExtend10 != null
                    && person1.PersonExtend10.Count > 0
                    )
                {
                    foreach (PersonExtend10 srcChild in person1.PersonExtend10)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend10 child2 = PersonExtend10.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend11
                if (person1.PersonExtend11 != null
                    && person1.PersonExtend11.Count > 0
                    )
                {
                    foreach (PersonExtend11 srcChild in person1.PersonExtend11)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend11 child2 = PersonExtend11.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend12
                if (person1.PersonExtend12 != null
                    && person1.PersonExtend12.Count > 0
                    )
                {
                    foreach (PersonExtend12 srcChild in person1.PersonExtend12)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend12 child2 = PersonExtend12.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend13
                if (person1.PersonExtend13 != null
                    && person1.PersonExtend13.Count > 0
                    )
                {
                    foreach (PersonExtend13 srcChild in person1.PersonExtend13)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend13 child2 = PersonExtend13.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend14
                if (person1.PersonExtend14 != null
                    && person1.PersonExtend14.Count > 0
                    )
                {
                    foreach (PersonExtend14 srcChild in person1.PersonExtend14)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend14 child2 = PersonExtend14.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend15
                if (person1.PersonExtend15 != null
                    && person1.PersonExtend15.Count > 0
                    )
                {
                    foreach (PersonExtend15 srcChild in person1.PersonExtend15)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend15 child2 = PersonExtend15.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend16
                if (person1.PersonExtend16 != null
                    && person1.PersonExtend16.Count > 0
                    )
                {
                    foreach (PersonExtend16 srcChild in person1.PersonExtend16)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend16 child2 = PersonExtend16.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend17
                if (person1.PersonExtend17 != null
                    && person1.PersonExtend17.Count > 0
                    )
                {
                    foreach (PersonExtend17 srcChild in person1.PersonExtend17)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend17 child2 = PersonExtend17.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend18
                if (person1.PersonExtend18 != null
                    && person1.PersonExtend18.Count > 0
                    )
                {
                    foreach (PersonExtend18 srcChild in person1.PersonExtend18)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend18 child2 = PersonExtend18.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend19
                if (person1.PersonExtend19 != null
                    && person1.PersonExtend19.Count > 0
                    )
                {
                    foreach (PersonExtend19 srcChild in person1.PersonExtend19)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend19 child2 = PersonExtend19.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_PersonExtend20
                if (person1.PersonExtend20 != null
                    && person1.PersonExtend20.Count > 0
                    )
                {
                    foreach (PersonExtend20 srcChild in person1.PersonExtend20)
                    {
                        if (srcChild != null)
                        {
                            PersonExtend20 child2 = PersonExtend20.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_RetireReturnRecords
                if (person1.RetireReturnRecords != null
                    && person1.RetireReturnRecords.Count > 0
                    )
                {
                    foreach (RetireReturnRecord srcChild in person1.RetireReturnRecords)
                    {
                        if (srcChild != null)
                        {
                            RetireReturnRecord child2 = RetireReturnRecord.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }

                // Person_WorkQualificationInfos
                if (person1.WorkQualificationInfos != null
                    && person1.WorkQualificationInfos.Count > 0
                    )
                {
                    foreach (WorkQualificationInfo srcChild in person1.WorkQualificationInfos)
                    {
                        if (srcChild != null)
                        {
                            WorkQualificationInfo child2 = WorkQualificationInfo.Create(person2);
                            CopyTo(srcChild, child2);
                        }
                    }
                }


            }
        }

        private void CopyPersonChild<T>(Person person2, BusinessEntity.EntityList<T> list) where T : BusinessEntity
        {
            if (list != null
                && list.Count == 0
                )
            {
                foreach (T child in list)
                {
                    CreatePersonChild<T>(person2, child);
                }
            }
        }

        private void CreatePersonChild<T>(Person person2, T arch) where T : BusinessEntity
        {
            //EmployeeArchive arch2 = EmployeeArchive.Create(person2);
            T arch2 = (T)BusinessEntity.Create(typeof(T).FullName, person2);

            //arch.CopyTo(arch2);

            CopyTo(arch, arch2);
        }

        //private void CreateEmployeeArchive(Person person2, EmployeeArchive arch)
        //{
        //    EmployeeArchive arch2 = EmployeeArchive.Create(person2);

        //    //arch.CopyTo(arch2);
            
        //    CopyTo(arch, arch2);
        //}

        private void CopyTo(BusinessEntity srcEntity, BusinessEntity targetEntity)
        {
            Dictionary<string, object> dicValues = new Dictionary<string, object>();
            foreach (string field in SysFields)
            {
                dicValues.Add(field, targetEntity.GetValue(field));
            }

            srcEntity.CopyTo(targetEntity);

            foreach (string field in dicValues.Keys)
            {
                //dicValues.Add(field, businessEntity2.GetValue(field));
                targetEntity.SetValue(field, dicValues[field]);
            }
        }
		



	}

    //#endregion	
}