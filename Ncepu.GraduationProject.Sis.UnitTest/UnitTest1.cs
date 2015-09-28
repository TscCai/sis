using System;
using Ncepu.GraduationProject.Sis.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ncepu.GraduationProject.Sis.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSHA256()
        {
            //string ori = "123";
            //string result=PasswordProvider.SHA256Encrypt(ori,false);
            //Console.WriteLine(result);


        }

        [TestMethod]
        public void TestHashPassoword()
        {
            string ori = "123";
            string salt = PasswordProvider.GetSalt();
            Console.WriteLine("Salt is:"+salt);
            string hash = PasswordProvider.EncryptPassword(ori, salt);
            Console.WriteLine(hash);
        }
    }
}
