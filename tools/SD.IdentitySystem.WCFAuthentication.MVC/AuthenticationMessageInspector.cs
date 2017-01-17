﻿using ShSoft.Infrastructure.Constants;
using ShSoft.ValueObjects;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace SD.IdentitySystem.WCFAuthentication.MVC
{
    /// <summary>
    /// WCF/MVC客户端身份认证消息拦截器
    /// </summary>
    internal class AuthenticationMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// 请求发送前事件
        /// </summary>
        /// <param name="request">请求消息</param>
        /// <param name="channel">信道</param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //MVC客户端获取公钥处理
            object loginInfo = HttpContext.Current.Session[CacheConstants.CurrentUserKey];

            if (loginInfo != null)
            {
                Guid publishKey = ((LoginInfo)loginInfo).PublicKey;

                //添加消息头
                MessageHeader header = MessageHeader.CreateHeader(Constants.WcfAuthHeaderName, Constants.WcfAuthHeaderNamespace, publishKey);
                request.Headers.Add(header);
            }

            return null;
        }

        /// <summary>
        /// 请求响应后事件
        /// </summary>
        /// <param name="reply">响应消息</param>
        /// <param name="correlationState">相关状态</param>
        public void AfterReceiveReply(ref Message reply, object correlationState) { }
    }
}