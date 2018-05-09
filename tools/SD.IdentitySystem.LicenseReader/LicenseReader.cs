﻿using SD.IdentitySystem.Tookits;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System;
using System.IO;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// 许可证读取器
    /// </summary>
    public static class LicenseReader
    {
        /// <summary>
        /// 许可证路径
        /// </summary>
        private static readonly string _LicencePath = AppDomain.CurrentDomain.BaseDirectory + CommonConstants.LicenseFileName;

        /// <summary>
        /// 获取许可证
        /// </summary>
        /// <returns>许可证</returns>
        public static License? GetLicense()
        {
            if (!File.Exists(_LicencePath))
            {
                return null;
            }

            try
            {
                using (FileStream fileStream = new FileStream(_LicencePath, FileMode.Open))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        byte[] buffer = new byte[fileStream.Length];
                        binaryReader.Read(buffer, 0, (int)fileStream.Length);

                        string aesString = buffer.GetString();
                        string binaryString = aesString.Decrypt();

                        License license = binaryString.ToLicense();

                        return license;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("许可证异常，请联系管理员！", exception);
            }
        }
    }
}