﻿using System;
using System.Collections.Generic;
using ShSoft.Framework2015.Infrastructure.ValueObjects;
using ShSoft.Framework2016.Infrastructure.IDomainEvent;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.IRepositories;
using ShSoft.UAC.Domain.Mediators;
using ShSoft.UAC.DomainEventStore.EventSources.Authority;

namespace ShSoft.UAC.DomainEventHandler.Authority
{
    /// <summary>
    /// 权限创建事件处理者
    /// </summary>
    public class AuthorityCreatedEventHandler : IDomainEventHandler<AuthorityCreatedEvent>
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 领域服务中介者
        /// </summary>
        private readonly DomainServiceMediator _svcMediator;

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 单元事务
        /// </summary>
        private readonly IUnitOfWorkUAC _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="svcMediator">领域服务中介者</param>
        /// <param name="repMediator">仓储中介者</param>
        /// <param name="unitOfWork">单元事务</param>
        public AuthorityCreatedEventHandler(DomainServiceMediator svcMediator, RepositoryMediator repMediator, IUnitOfWorkUAC unitOfWork)
        {
            this._svcMediator = svcMediator;
            this._repMediator = repMediator;
            this._unitOfWork = unitOfWork;
            this.Sort = uint.MaxValue;
        }

        #endregion

        #region # 执行顺序，倒序排列 —— uint Sort
        /// <summary>
        /// 执行顺序，倒序排列
        /// </summary>
        public uint Sort { get; private set; }
        #endregion

        #region # 事件处理方法 —— void Handle(AuthorityCreatedEvent eventSource)
        /// <summary>
        /// 事件处理方法
        /// </summary>
        /// <param name="eventSource">事件源</param>
        public void Handle(AuthorityCreatedEvent eventSource)
        {
            this.AppendAuthorities(eventSource.Data.InfoSystemKindNo, eventSource.Data.AuthorityId);
        }
        #endregion

        #region # 追加权限 —— void AppendAuthorities(string systemKindNo, Guid authorityId)
        /// <summary>
        /// 追加权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        private void AppendAuthorities(string systemKindNo, Guid authorityId)
        {
            //验证参数
            this._svcMediator.InfoSystemKindSvc.AssertAuthorityExists(systemKindNo, authorityId);

            InfoSystemKind currentSystemKind = this._unitOfWork.Resolve<InfoSystemKind>(systemKindNo);
            Domain.Entities.Authority currentAuthority = currentSystemKind.GetAuthority(authorityId);

            //管理中心信息系统类别
            if (systemKindNo == Constants.MCSystemKindNo)
            {
                IEnumerable<string> systemNos = this._repMediator.InfoSystemRep.GetInfoSystemNos(systemKindNo);
                foreach (string systemNo in systemNos)
                {
                    //为超级管理员（admin）追加权限
                    InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);
                    Role adminRole = currentSystem.GetAdminRole();
                    adminRole.AppendAuthorities(new[] { currentAuthority });

                    //注册保存
                    this._unitOfWork.RegisterSave(currentSystem);
                }
            }

            //供应商信息系统类别
            else if (systemKindNo == Constants.SupplierSystemKindNo)
            {
                IEnumerable<string> systemNos = this._repMediator.InfoSystemRep.GetInfoSystemNos(systemKindNo);
                foreach (string systemNo in systemNos)
                {
                    //为系统管理员与供应商代理人追加权限
                    InfoSystem currentSystem = this._unitOfWork.Resolve<InfoSystem>(systemNo);
                    Role managerRole = currentSystem.GetManagerRole();
                    Role agentRole = currentSystem.GetAgentRole();
                    managerRole.AppendAuthorities(new[] { currentAuthority });
                    agentRole.AppendAuthorities(new[] { currentAuthority });

                    //注册保存
                    this._unitOfWork.RegisterSave(currentSystem);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("systemKindNo", @"未知的信息系统类别！");
            }

            //预提交
            this._unitOfWork.Commit();
        }
        #endregion
    }
}
