using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Ncepu.GraduationProject.Sis.Common
{
    /// <summary>
    /// 模块：PasswordProvider
    /// 摘要：提供用户的密码服务
    /// 作者：Tsccai
    /// 编写日期：2013/5/11 09:43:28
    /// </summary>
    public class PasswordProvider
    {

        /// <summary>
        /// 对字符串进行SHA256加密
        /// </summary>
        /// <param name="origin">原字符串</param>
        /// <param name="separator">是否需要"-"作为分隔符</param>
        /// <returns>加密后的字符串</returns>
        static string SHA256Encrypt(string origin, bool separator)
        {
            SHA256 provider = SHA256CryptoServiceProvider.Create();

            byte[] hash = provider.ComputeHash(Encoding.Default.GetBytes(origin));
            provider.Clear();
            string result = BitConverter.ToString(hash);
            if (!separator)
            {
                result = result.Replace("-", "");
            }
            return result;
        }

        /// <summary>
        /// 加密用户密码
        /// </summary>
        /// <param name="origin">明文密码</param>
        /// <param name="salt">盐值</param>
        /// <returns>两次SHA256加密后的密码</returns>
        public static string EncryptPassword(string origin,string salt)
        {
            string result = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(SHA256Encrypt(origin, false));
            sb.Append(salt);
            result = SHA256Encrypt(sb.ToString(),false);
            return result;
        }

        /// <summary>
        /// 随机生成一个16位的盐值，包含大小写字母、数字、符号
        /// </summary>
        /// <returns></returns>
        public static string GetSalt()
        {
            return GetRnd(16, true, true, true, true, null);
        }


        ///<summary>
        ///生成随机字符串
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        private static string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

    }
}
