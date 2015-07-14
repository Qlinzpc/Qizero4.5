using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Zephyr.Utils;
using System.IO;
namespace Zephyr.Utils
{
    public partial class ZEncypt
    {
        //Ĭ����Կ����
        private static byte[] AESKeys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };
        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ���,ʧ�ܷ���Դ��</returns>
        public static string AESEncrypt(string encryptString, string encryptKey)
        {
            encryptKey = ZString.GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rijndaelProvider.IV = AESKeys;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���,ʧ�ܷ�Դ��</returns>
        public static string AESDecrypt(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = ZString.GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
                rijndaelProvider.IV = AESKeys;
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return "";
            }

        }

        #region ʹ�÷ǶԳ��㷨���ܡ�����

        /// <summary>
        /// �ڷǶԳƼ���ʱ����������Կ���͡�˽Կ��
        /// </summary>
        public static void GeneratePrivateKey()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            //����˽Կ
            string privatekey = RSA.ToXmlString(true);

            //������Կ
            string publicKey = RSA.ToXmlString(false);
        }

        /// <summary>
        /// �ǶԳƼ���
        /// </summary>
        /// <param name="str">Ҫ���ܵ�����</param>
        /// <param name="publicKey">��Կ</param>
        /// <returns>���ܽ��</returns>
        public static string AsymmectricEncrypts(string str, string publicKey)
        {
            string result = string.Empty;

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            //���ù�Կ
            RSA.FromXmlString(publicKey);
            byte[] btResult = RSA.Encrypt(data, true);
            result = Convert.ToBase64String(btResult);
            return result;
        }

        /// <summary>
        /// �ǶԳƼ��ܺ�Ľ���
        /// </summary>
        /// <param name="strcode">���ܺ���ַ���</param>
        /// <param name="privateKey">��Կ��˽Կ��</param>
        /// <returns>���ܺ�Ľ��</returns>
        public static string AsymmectricDecrypts(string strcode, string privateKey)
        {
            string result = string.Empty;
            byte[] bydata = Convert.FromBase64String(strcode);
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            //˽Կ
            RSA.FromXmlString(privateKey);
            byte[] byR = RSA.Decrypt(bydata, true);
            result = System.Text.Encoding.UTF8.GetString(byR);
            return result;
        }

        #endregion

        #region ʹ�öԳƼ��ܡ�����

        /// <summary>
        /// ʹ�öԳ��㷨����
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string SymmetricEncrypts(string str, string encryptKey)
        {
            string result = string.Empty;
            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] IV = { 0x77, 0x70, 0x50, 0xD9, 0xE1, 0x7F, 0x23, 0x13, 0x7A, 0xB3, 0xC7, 0xA7, 0x48, 0x2A, 0x4B, 0x39 };
            try
            {
                byte[] byKey = System.Text.Encoding.UTF8.GetBytes(encryptKey);
                //����ָ�������㷨������Create()������ָ���ַ���
                //Create()�����еĲ��������ǣ�DES��RC2 System��Rijndael��TripleDES 
                //���ò�ͬ��ʵ�����IV������Ҫ��һ��(������GenerateIV()��������)���޲�����ʾ��Rijndael
                SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create();//����һ�ּ����㷨
                MemoryStream msTarget = new MemoryStream();
                //���彫���������ӵ�����ת��������
                CryptoStream encStream = new CryptoStream(msTarget, Algorithm.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                encStream.Write(inputData, 0, inputData.Length);
                encStream.FlushFinalBlock();
                result = Convert.ToBase64String(msTarget.ToArray());
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }


        /// <summary>
        /// ʹ�öԳ��㷨����
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string SymmectricDecrypts(string encryptStr, string encryptKey)
        {
            string result = string.Empty;
            //����ʱʹ�õ���Convert.ToBase64String(),����ʱ����ʹ��Convert.FromBase64String()
            try
            {
                byte[] encryptData = Convert.FromBase64String(encryptStr);
                byte[] byKey = System.Text.Encoding.UTF8.GetBytes(encryptKey);
                byte[] IV = { 0x77, 0x70, 0x50, 0xD9, 0xE1, 0x7F, 0x23, 0x13, 0x7A, 0xB3, 0xC7, 0xA7, 0x48, 0x2A, 0x4B, 0x39 };
                SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create();
                MemoryStream msTarget = new MemoryStream();
                CryptoStream decStream = new CryptoStream(msTarget, Algorithm.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                decStream.Write(encryptData, 0, encryptData.Length);
                decStream.FlushFinalBlock();
                result = System.Text.Encoding.Default.GetString(msTarget.ToArray());
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        #endregion
    }
    public class SDKSecurity
    {
        public SDKSecurity()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// MD5 ���ܾ�̬����
        /// </summary>
        /// <param name="EncryptString">�����ܵ�����</param>
        /// <returns>returns</returns>
        public static string MD5Encrypt(string EncryptString)
        {
            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("���Ĳ���Ϊ��")); }

            MD5 m_ClassMD5 = new MD5CryptoServiceProvider();

            string m_strEncrypt = "";

            try
            {
                m_strEncrypt = BitConverter.ToString(m_ClassMD5.ComputeHash(Encoding.Default.GetBytes(EncryptString))).Replace("-", "");
            }
            catch (ArgumentException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_ClassMD5.Clear(); }

            return m_strEncrypt;
        }

        /// <summary>
        /// DES ����(���ݼ��ܱ�׼���ٶȽϿ죬�����ڼ��ܴ������ݵĳ���)
        /// </summary>
        /// <param name="EncryptString">�����ܵ�����</param>
        /// <param name="EncryptKey">���ܵ���Կ</param>
        /// <returns>returns</returns>
        public static string DESEncrypt(string EncryptString, string EncryptKey)
        {
            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("���Ĳ���Ϊ��")); }

            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("��Կ����Ϊ��")); }

            if (EncryptKey.Length != 8) { throw (new Exception("��Կ����Ϊ8λ")); }

            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            string m_strEncrypt = "";

            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();

            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);

                m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);

                m_cstream.FlushFinalBlock();

                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());

                m_stream.Close(); m_stream.Dispose();

                m_cstream.Close(); m_cstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_DESProvider.Clear(); }

            return m_strEncrypt;
        }
        /// <summary>
        /// DES ����(���ݼ��ܱ�׼���ٶȽϿ죬�����ڼ��ܴ������ݵĳ���)
        /// </summary>
        /// <param name="DecryptString">�����ܵ�����</param>
        /// <param name="DecryptKey">���ܵ���Կ</param>
        /// <returns>returns</returns>
        public static string DESDecrypt(string DecryptString, string DecryptKey)
        {
            if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("���Ĳ���Ϊ��")); }

            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("��Կ����Ϊ��")); }

            if (DecryptKey.Length != 8) { throw (new Exception("��Կ����Ϊ8λ")); }

            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            string m_strDecrypt = "";

            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();

            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);

                m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);

                m_cstream.FlushFinalBlock();

                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());

                m_stream.Close(); m_stream.Dispose();

                m_cstream.Close(); m_cstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_DESProvider.Clear(); }

            return m_strDecrypt;
        }
        /// <summary>
        /// RC2 ����(�ñ䳤��Կ�Դ������ݽ��м���)
        /// </summary>
        /// <param name="EncryptString">����������</param>
        /// <param name="EncryptKey">������Կ</param>
        /// <returns>returns</returns>
        public static string RC2Encrypt(string EncryptString, string EncryptKey)
        {
            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("���Ĳ���Ϊ��")); }

            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("��Կ����Ϊ��")); }

            if (EncryptKey.Length < 5 || EncryptKey.Length > 16) { throw (new Exception("��Կ����Ϊ5-16λ")); }

            string m_strEncrypt = "";

            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();

            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);

                m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);

                m_cstream.FlushFinalBlock();

                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());

                m_stream.Close(); m_stream.Dispose();

                m_cstream.Close(); m_cstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_RC2Provider.Clear(); }

            return m_strEncrypt;
        }
        /// <summary>
        /// RC2 ����(�ñ䳤��Կ�Դ������ݽ��м���)
        /// </summary>
        /// <param name="DecryptString">����������</param>
        /// <param name="DecryptKey">������Կ</param>
        /// <returns>returns</returns>
        public static string RC2Decrypt(string DecryptString, string DecryptKey)
        {
            if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("���Ĳ���Ϊ��")); }

            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("��Կ����Ϊ��")); }

            if (DecryptKey.Length < 5 || DecryptKey.Length > 16) { throw (new Exception("��Կ����Ϊ5-16λ")); }

            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            string m_strDecrypt = "";

            RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();

            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);

                m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);

                m_cstream.FlushFinalBlock();

                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());

                m_stream.Close(); m_stream.Dispose();

                m_cstream.Close(); m_cstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_RC2Provider.Clear(); }
            return m_strDecrypt;
        }
        /// <summary>
        /// 3DES ����(����DES����һ��������������ͬ����Կ�������μ��ܣ�ǿ�ȸ���)
        /// </summary>
        /// <param name="EncryptString">����������</param>
        /// <param name="EncryptKey1">��Կһ</param>
        /// <param name="EncryptKey2">��Կ��</param>
        /// <param name="EncryptKey3">��Կ��</param>
        /// <returns>returns</returns>
        public static string DES3Encrypt(string EncryptString, string EncryptKey1, string EncryptKey2, string EncryptKey3)
        {
            string m_strEncrypt = "";

            try
            {
                m_strEncrypt = DESEncrypt(EncryptString, EncryptKey3);

                m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey2);

                m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey1);
            }
            catch (Exception ex) { throw ex; }

            return m_strEncrypt;
        }
        /// <summary>
        /// 3DES ����(����DES����һ��������������ͬ����Կ�������μ��ܣ�ǿ�ȸ���)
        /// </summary>
        /// <param name="DecryptString">����������</param>
        /// <param name="DecryptKey1">��Կһ</param>
        /// <param name="DecryptKey2">��Կ��</param>
        /// <param name="DecryptKey3">��Կ��</param>
        /// <returns>returns</returns>
        public static string DES3Decrypt(string DecryptString, string DecryptKey1, string DecryptKey2, string DecryptKey3)
        {
            string m_strDecrypt = "";

            try
            {
                m_strDecrypt = DESDecrypt(DecryptString, DecryptKey1);

                m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey2);

                m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey3);
            }
            catch (Exception ex) { throw ex; }

            return m_strDecrypt;
        }
        /// <summary>
        /// AES ����(�߼����ܱ�׼������һ���ļ����㷨��׼���ٶȿ죬��ȫ����ߣ�Ŀǰ AES ��׼��һ��ʵ���� Rijndael �㷨)
        /// </summary>
        /// <param name="EncryptString">����������</param>
        /// <param name="EncryptKey">������Կ</param>
        /// <returns></returns>
        public static string AESEncrypt(string EncryptString, string EncryptKey)
        {
            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("���Ĳ���Ϊ��")); }

            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("��Կ����Ϊ��")); }

            string m_strEncrypt = "";

            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");

            Rijndael m_AESProvider = Rijndael.Create();

            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);

                m_csstream.Write(m_btEncryptString, 0, m_btEncryptString.Length); m_csstream.FlushFinalBlock();

                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());

                m_stream.Close(); m_stream.Dispose();

                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }

            return m_strEncrypt;
        }
        /// <summary>
        /// AES ����(�߼����ܱ�׼������һ���ļ����㷨��׼���ٶȿ죬��ȫ����ߣ�Ŀǰ AES ��׼��һ��ʵ���� Rijndael �㷨)
        /// </summary>
        /// <param name="DecryptString">����������</param>
        /// <param name="DecryptKey">������Կ</param>
        /// <returns></returns>
        public static string AESDecrypt(string DecryptString, string DecryptKey)
        {
            if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("���Ĳ���Ϊ��")); }

            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("��Կ����Ϊ��")); }

            string m_strDecrypt = "";

            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");

            Rijndael m_AESProvider = Rijndael.Create();

            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);

                m_csstream.Write(m_btDecryptString, 0, m_btDecryptString.Length); m_csstream.FlushFinalBlock();

                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());

                m_stream.Close(); m_stream.Dispose();

                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }

            return m_strDecrypt;
        }
    }
}
