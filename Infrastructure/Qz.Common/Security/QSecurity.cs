using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Qz.Common
{
    public class QSecurity
    {
        private static byte[] m_btIV = { 0x77, 0x70, 0x50, 0xD9, 0xE1, 0x7F, 0x23, 0x13, 0x7A, 0xB3, 0xC7, 0xA7, 0x48, 0x2A, 0x4B, 0x39 };

        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="encryptString">待加密密文</param>
        /// <param name="encryptKey">加密密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string encryptString, string encryptKey)
        {
            if (string.IsNullOrEmpty(encryptString)) { throw (new Exception("密文不得为空")); }

            if (string.IsNullOrEmpty(encryptKey)) { throw (new Exception("密钥不得为空")); }

            string m_strEncrypt = "";

            Rijndael m_AESProvider = Rijndael.Create();

            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(encryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateEncryptor(Encoding.Default.GetBytes(encryptKey), m_btIV), CryptoStreamMode.Write);

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
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public static string AESDecrypt(string decryptString, string decryptKey)
        {
            if (string.IsNullOrEmpty(decryptString)) { throw (new Exception("密文不得为空")); }

            if (string.IsNullOrEmpty(decryptKey)) { throw (new Exception("密钥不得为空")); }

            string m_strDecrypt = "";


            Rijndael m_AESProvider = Rijndael.Create();

            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(decryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateDecryptor(Encoding.Default.GetBytes(decryptKey), m_btIV), CryptoStreamMode.Write);

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

        /// <summary>
        /// DES 加密(数据加密标准，速度较快，适用于加密大量数据的场合)
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">加密的密钥</param>
        /// <returns>returns</returns>
        public static string DESEncrypt(string encryptString, string encryptKey)
        {
            if (string.IsNullOrEmpty(encryptString)) { throw (new Exception("密文不得为空")); }

            if (string.IsNullOrEmpty(encryptKey)) { throw (new Exception("密钥不得为空")); }

            if (encryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }

            string m_strEncrypt = "";

            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();

            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(encryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(encryptKey), m_btIV), CryptoStreamMode.Write);

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
        /// DES 解密(数据加密标准，速度较快，适用于加密大量数据的场合)
        /// </summary>
        /// <param name="decryptString">待解密的密文</param>
        /// <param name="decryptKey">解密的密钥</param>
        /// <returns>returns</returns>
        public static string DESDecrypt(string decryptString, string decryptKey)
        {
            if (string.IsNullOrEmpty(decryptString)) { throw (new Exception("密文不得为空")); }

            if (string.IsNullOrEmpty(decryptKey)) { throw (new Exception("密钥不得为空")); }

            if (decryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }

            string m_strDecrypt = "";

            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();

            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(decryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateDecryptor(Encoding.Default.GetBytes(decryptKey), m_btIV), CryptoStreamMode.Write);

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
        /// RC2 加密(用变长密钥对大量数据进行加密)
        /// </summary>
        /// <param name="encryptString">待加密密文</param>
        /// <param name="encryptKey">加密密钥</param>
        /// <returns>returns</returns>
        public static string RC2Encrypt(string encryptString, string encryptKey)
        {
            if (string.IsNullOrEmpty(encryptString)) { throw (new Exception("密文不得为空")); }

            if (string.IsNullOrEmpty(encryptKey)) { throw (new Exception("密钥不得为空")); }

            if (encryptKey.Length < 5 || encryptKey.Length > 16) { throw (new Exception("密钥必须为5-16位")); }

            string m_strEncrypt = "";

            RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();

            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(encryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateEncryptor(Encoding.Default.GetBytes(encryptKey), m_btIV), CryptoStreamMode.Write);

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
        /// RC2 解密(用变长密钥对大量数据进行加密)
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns>returns</returns>
        public static string RC2Decrypt(string decryptString, string decryptKey)
        {
            if (string.IsNullOrEmpty(decryptString)) { throw (new Exception("密文不得为空")); }

            if (string.IsNullOrEmpty(decryptKey)) { throw (new Exception("密钥不得为空")); }

            if (decryptKey.Length < 5 || decryptKey.Length > 16) { throw (new Exception("密钥必须为5-16位")); }

            string m_strDecrypt = "";

            RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();

            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(decryptString);

                MemoryStream m_stream = new MemoryStream();

                CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateDecryptor(Encoding.Default.GetBytes(decryptKey), m_btIV), CryptoStreamMode.Write);

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
        /// 3DES 加密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
        /// </summary>
        /// <param name="encryptString">待加密密文</param>
        /// <param name="encryptKey1">密钥一</param>
        /// <param name="encryptKey2">密钥二</param>
        /// <param name="encryptKey3">密钥三</param>
        /// <returns>returns</returns>
        public static string DES3Encrypt(string encryptString, string encryptKey1, string encryptKey2, string encryptKey3)
        {
            string m_strEncrypt = "";

            try
            {
                m_strEncrypt = DESEncrypt(encryptString, encryptKey3);

                m_strEncrypt = DESEncrypt(m_strEncrypt, encryptKey2);

                m_strEncrypt = DESEncrypt(m_strEncrypt, encryptKey1);
            }
            catch (Exception ex) { throw ex; }

            return m_strEncrypt;
        }
        /// <summary>
        /// 3DES 解密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey1">密钥一</param>
        /// <param name="decryptKey2">密钥二</param>
        /// <param name="decryptKey3">密钥三</param>
        /// <returns>returns</returns>
        public static string DES3Decrypt(string decryptString, string decryptKey1, string decryptKey2, string decryptKey3)
        {
            string m_strDecrypt = "";

            try
            {
                m_strDecrypt = DESDecrypt(decryptString, decryptKey1);

                m_strDecrypt = DESDecrypt(m_strDecrypt, decryptKey2);

                m_strDecrypt = DESDecrypt(m_strDecrypt, decryptKey3);
            }
            catch (Exception ex) { throw ex; }

            return m_strDecrypt;
        }
    }
}
