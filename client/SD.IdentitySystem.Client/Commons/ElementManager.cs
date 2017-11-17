﻿using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SD.IOC.Core.Mediator;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 元素管理器静态工具类
    /// </summary>
    public static class ElementManager
    {
        #region # 字段及构造器

        /// <summary>
        /// 元素管理器实例
        /// </summary>
        private static readonly IElementManager _Current;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ElementManager()
        {
            _Current = ResolveMediator.Resolve<IElementManager>();
        }

        #endregion

        #region # 文档列表 —— static BindableCollection<DocumentBase> Documents
        /// <summary>
        /// 文档列表
        /// </summary>
        public static BindableCollection<DocumentBase> Documents
        {
            get { return _Current.Documents; }
        }
        #endregion

        #region # 打开文档 —— static void OpenDocument(Type type)
        /// <summary>
        /// 打开文档
        /// </summary>
        /// <param name="type">文档类型</param>
        public static void OpenDocument(Type type)
        {
            _Current.OpenDocument(type);
        }
        #endregion

        #region # 打开文档 —— static void OpenDocument<T>()
        /// <summary>
        /// 打开文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        public static void OpenDocument<T>() where T : DocumentBase
        {
            _Current.OpenDocument<T>();
        }
        #endregion

        #region # 打开飞窗 —— void OpenFlyout(Type type)
        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <param name="type">飞窗类型</param>
        public static void OpenFlyout(Type type)
        {
            _Current.OpenFlyout(type);
        }
        #endregion

        #region # 打开飞窗 —— static void OpenFlyout<T>()
        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <typeparam name="T">飞窗类型</typeparam>
        public static void OpenFlyout<T>() where T : FlyoutBase
        {
            _Current.OpenFlyout<T>();
        }
        #endregion

        #region # 打开消息框 —— static async Task ShowMessage(string title...
        /// <summary>
        /// 打开消息框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="style">样式</param>
        /// <param name="config">配置</param>
        public static async Task ShowMessage(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings config = null)
        {
            MetroWindow currentView = (MetroWindow)Application.Current.MainWindow;
            await currentView.ShowMessageAsync(title, message, style, config);
        }
        #endregion
    }
}
