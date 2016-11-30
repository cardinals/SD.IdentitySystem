﻿using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IDomainServices;
using SD.IdentitySystem.Domain.Mediators;

namespace SD.IdentitySystem.DomainService.Implements
{
    /// <summary>
    /// 信息系统类别领域服务实现
    /// </summary>
    public class InfoSystemKindService : IInfoSystemKindService
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="repMediator">仓储中介者</param>
        public InfoSystemKindService(RepositoryMediator repMediator)
        {
            this._repMediator = repMediator;
        }

        #endregion

        #region # 获取聚合根实体关键字 —— string GetKeywords(InfoSystemKind entity)
        /// <summary>
        /// 获取聚合根实体关键字
        /// </summary>
        /// <param name="entity">聚合根实体对象</param>
        /// <returns>关键字</returns>
        public string GetKeywords(InfoSystemKind entity)
        {
            return entity.Keywords;
        }
        #endregion
    }
}