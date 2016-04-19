﻿using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace ShSoft.UAC.WcfAuthentication.Client
{
    /// <summary>
    /// WCF客户端身份认证消息拦截器
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
            //TODO Windows客户端与Web客户端获取公钥处理

            Guid publishKey = Guid.NewGuid();

            //MessageHeader header = MessageHeader.CreateHeader(CacheConstants.WcfAuthHeaderName, CacheConstants.WcfAuthHeaderNs, publishKey);

            //request.Headers.Add(header);
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
