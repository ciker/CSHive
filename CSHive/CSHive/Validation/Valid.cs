#region copyright info
//------------------------------------------------------------------------------
// <copyright company="ChaosStudio">
//     Copyright (c) 2002-2010 ��˼������.  All rights reserved.
//     Contact:		Email:atwind@cszi.com , QQ:3329091
//		Link:		 http://www.cszi.com
// </copyright>
//------------------------------------------------------------------------------
#endregion

using System;
using System.Text.RegularExpressions;

namespace CS.Validation
{
    /// <summary>
    /// �ַ����������֤
    /// </summary>
    /// 
    /// <description class = "CS.Utility.Valid">
    /// 
    /// </description>
    /// 
    /// <history>
    ///     Create     :	    Atwind, 2008-6-13 21:39:05;
    ///     Update    :       Atwind , 2010-4-12 ����У���ί�и��ⲿ����
    ///  </history>
    public static class Valid
    {
         /// <summary>
        /// ��ʼ����
        /// <para>��ʼ�� <see cref="Verify"/> ί�� </para>
        /// </summary>
        static Valid()
        {
            Restore();
        }

        /// <summary>
        /// ��֤ί�У���Ҫʱ����ί���ⲿ����
        /// </summary>
        internal static Func<string,string,bool> Verify;

        /// <summary>
        /// �ָ�Ĭ�ϵ�ί�з���
        /// </summary>
        public static void Restore()
        {
            //Verify = (input,pattern) => CheckValue(input,pattern);
            Verify = CheckValue;
        }

        /// <summary>
        /// ��������ַ����Ƿ�Ϊ��, �Ƿ��������
        /// ע��:��ʱһ�����ʧ��
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <param name="pattern">������ʽ</param>
        /// <returns>��Ϊ���ҷ������򷵻� true</returns>
        public static bool CheckValue(string input, string pattern)
        {
            return !string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// ͨ��������֤������Ϊ����Ӣ��3~50������2~25�ַ�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool NameValidator(string input)
        {
            return Verify(input, RegexLib.NAME);
        }
        /// <summary>
        /// ������֤������Ϊ�գ���Ϊ��ʱ Ӣ��5~200������2~100�ַ�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool DescriptionValidator(string input)
        {
            return string.IsNullOrEmpty(input) || Verify(input, RegexLib.DESCRIPTION);
        }

        /// <summary>
        /// �ʻ���֤[Ӣ������]:Ӣ�Ŀ�ͷ,4~32λ
        /// </summary>
        /// <param name="input"></param>
        /// <returns>ͨ����֤Ϊtrue</returns>
        public static bool AccountValidator(string input)
        {
            return Verify(input, RegexLib.ACCOUNT);
        }
        
        /// <summary>
        /// ������֤
        /// Ӣ�Ŀ�ͷ3-16 �ַ�||���Ŀ�ͷ 2-12 �ַ�
        /// </summary>
        /// <param name="input">��Ҫ��֤�� ����</param>
        /// <returns>��֤���</returns>
        public static bool NicknameValidator(string input)
        {
            return Verify(input, RegexLib.NICKNAME);
        }

        /// <summary>
        /// ����������֤[2~8������]
        /// </summary>
        /// <param name="input">��Ҫ��֤�� ����</param>
        /// <returns>��֤���</returns>
        public static bool ChineseNameValidator(string input)
        {
            return Verify(input, RegexLib.CHINESE_NAME);
        }
        
        /// <summary>
        /// ������֤:8~32λ�ַ���
        /// </summary>
        /// <param name="input">��Ҫ��֤�� ����</param>
        /// <returns>��֤���</returns>
        public static bool PasswordValidator(string input)
        {
            return Verify(input, RegexLib.PASSWORD);
        }

        /// <summary>
        /// QQ ��֤
        /// </summary>
        /// <param name="input">��Ҫ��֤�� QQ</param>
        /// <returns>��֤���</returns>
        public static bool QqValidator(string input)
        {
            return Verify(input, RegexLib.QQ);
        }

        /// <summary>
        /// Email ��֤
        /// </summary>
        /// <param name="input">��Ҫ��֤�� Email</param>
        /// <returns>��֤���</returns>
        public static bool EmailValidator(string input)
        {
            return Verify(input, RegexLib.EMAIL);
        }

        /// <summary>
        /// Url ��֤[�����Э��]
        /// </summary>
        /// <param name="p">��Ҫ��֤��Url</param>
        /// <returns>�Ƿ�ͨ����֤</returns>
        public static bool UrlValidator(string p)
        {
            return Verify(p, RegexLib.URL);
        }

        /// <summary>
        /// �ֻ�������֤
        /// </summary>
        /// <param name="input">��Ҫ��֤�� �ֻ�����</param>
        /// <returns>��֤���</returns>
        public static bool MobilephoneValidator(string input)
        {
            return Verify(input, RegexLib.MOBILEPHONE);
        }

        /// <summary>
        /// �绰������֤[�������ʵ绰]
        /// </summary>
        /// <param name="input">��Ҫ��֤�� �绰����</param>
        /// <returns>��֤���</returns>
        public static bool TelephoneValidator(string input)
        {
            return Verify(input, RegexLib.TELEPHONE);
        }

        /// <summary>
        /// �ۺ��Ժ�����֤
        /// </summary>
        /// <param name="input">ƥ���ʽ��11λ�ֻ����� 3-4λ���ţ�7-8λֱ�����룬1��4λ�ֻ��� �磺12345678901��1234-12345678-1234</param>
        /// <returns>���</returns>
        public static bool PhoneValidator(string input)
        {
            return Verify(input, RegexLib.PHONE);
        }

        /// <summary>
        /// �ж��ַ����Ƿ����� _, (_����ո�)�ָ�� id �ִ�.
        /// </summary>
        /// <param name="input">��Ҫ�����ı�</param>
        /// <returns>�����</returns>
        public static bool IdStringValidator(string input)
        {
            return Verify(input.Replace(" ", ""), RegexLib.ID_STRINGS);
        }

      


        #region ���֤��֤

        /// <summary>
        /// ���֤У��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckIDCard(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            if (id.Length == 18) return CheckIDCard18(id);
            return id.Length == 15 && CheckIDCard15(id);
        }

        /// <summary>
        /// 18λ���֤��֤
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckIDCard18(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 18)
                return false;
            long n = 0;
            if (!long.TryParse(id.Remove(17), out n) || n < Math.Pow(10, 16) || !long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n)) return false;//������֤

            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1) return false;//ʡ����֤

            var birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false) return false;//������֤

            var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            var wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            var ai = id.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());

            var y = -1;
            Math.DivRem(sum, 11, out y);
            return arrVarifyCode[y] == id.Substring(17, 1).ToLower();
        }

        /// <summary>
        /// 15λ���֤��֤
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckIDCard15(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 15)
                return false;
            long n;
            if (!long.TryParse(id, out n) || n < Math.Pow(10, 14)) return false;//������֤

            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1) return false;//ʡ����֤

            var birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            return DateTime.TryParse(birth, out time);
        }

        #endregion


    }
}