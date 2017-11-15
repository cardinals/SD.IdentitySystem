﻿using MahApps.Metro.Controls;
using SD.IdentitySystem.Client.Commons;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels
{
    public class TestViewModel : FlyoutBase
    {
        /// <summary>
        /// 创建飞窗
        /// </summary>
        public TestViewModel(string title, Position position, Thickness margin)
            : base(title, position, margin)
        {

        }
    }
}
