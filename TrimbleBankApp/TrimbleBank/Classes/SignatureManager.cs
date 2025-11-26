using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TrimbleBank.Classes
{
    /// <summary>
    /// Class handling creation and verifying of signatures
    /// </summary>
    internal class SignatureManager
    {
        private static string privateDSAKey = @"<DSAKeyValue><P>yieLLuDVuwvXmFCWRkmOT2h6QP6zYQUd8A2HTe9DUj+yM4uUEkfe+UWixR5FjCubqYqVm2cj34ZpoivXsGlsgTyYwAY4gEXRnXuNYmQ7t1R85/+EDRtCLYu/268yoQU5gpbDubi/e6BwQQ1vIUGw3h9ZRmO+7eNf+nnn0v5czsk=</P><Q>/ljyTUR7BUKsTY6Eh3IjEcCd3kc=</Q><G>sPVV/RQ+reBO51FaEKOTl/NzXjZ7hi8896jwPlmFl61NZfUhtxLTycUrzIllgkJX9kfh+A7F7S6lYVTgOoZWSW3ERiG8vNqOd3HlzVy+ZMLcCy9gtxJpR+rLfcR42Lby1M7LQcMqtqTqNXGSBejTPSRQi+ZTqXuj6c++ubfIV+c=</G><Y>E9fEAiRjgOsbzgvZ5/gb90fVQRqpqEPFURnTYqk38+rmTRt9RW4KhJw62hY998dzSL8na2inPrA7sJCtpZ4LFJK4KUjMXkzvI+ViqSCFdFUQ7CVTfukIgpSsghwtBu2v+WWJBxjLCPKAbcLj9JWjiqtwzPXwI/s15ntpki4SrX8=</Y><Seed>UQSY7DNNthtDZ8zgEUE80WepFis=</Seed><PgenCounter>AlM=</PgenCounter><X>zi3oT4ilDhcpIc7dzy28DhED0r8=</X></DSAKeyValue>";
        private static string publicDSAKey  = @"<DSAKeyValue><P>yieLLuDVuwvXmFCWRkmOT2h6QP6zYQUd8A2HTe9DUj+yM4uUEkfe+UWixR5FjCubqYqVm2cj34ZpoivXsGlsgTyYwAY4gEXRnXuNYmQ7t1R85/+EDRtCLYu/268yoQU5gpbDubi/e6BwQQ1vIUGw3h9ZRmO+7eNf+nnn0v5czsk=</P><Q>/ljyTUR7BUKsTY6Eh3IjEcCd3kc=</Q><G>sPVV/RQ+reBO51FaEKOTl/NzXjZ7hi8896jwPlmFl61NZfUhtxLTycUrzIllgkJX9kfh+A7F7S6lYVTgOoZWSW3ERiG8vNqOd3HlzVy+ZMLcCy9gtxJpR+rLfcR42Lby1M7LQcMqtqTqNXGSBejTPSRQi+ZTqXuj6c++ubfIV+c=</G><Y>E9fEAiRjgOsbzgvZ5/gb90fVQRqpqEPFURnTYqk38+rmTRt9RW4KhJw62hY998dzSL8na2inPrA7sJCtpZ4LFJK4KUjMXkzvI+ViqSCFdFUQ7CVTfukIgpSsghwtBu2v+WWJBxjLCPKAbcLj9JWjiqtwzPXwI/s15ntpki4SrX8=</Y><Seed>UQSY7DNNthtDZ8zgEUE80WepFis=</Seed><PgenCounter>AlM=</PgenCounter></DSAKeyValue>";


        /// <summary>
        /// Create double signature
        /// </summary>
        /// <param name="value">double value to create signature for</param>
        /// <returns>signature for double</returns>
        public static string Create(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return Create(bytes);
        }

        /// <summary>
        /// Create signature for double, account pair
        /// </summary>
        /// <param name="value">double, account pair value to create signature for</param>
        /// <returns>signature for double, account pair</returns>
        public static string Create(double value, DataRow account)
        {
            return Create($"{value};{account["Number"]}");
        }

        /// <summary>
        /// Create string signature
        /// </summary>
        /// <param name="value">string value to create signature for</param>
        /// <returns>signature for string</returns>
        public static string Create(string text)
        {
            using (DSA dsa = DSA.Create())
            {
                // --- init with private key ---
                dsa.FromXmlString(privateDSAKey);

                // --- generate signature and return as string ----
                byte[] textByte  = Encoding.UTF8.GetBytes(text);
                byte[] signature = dsa.SignData(textByte, HashAlgorithmName.SHA1);
                return Convert.ToBase64String(signature);
            }            
        }

        /// <summary>
        /// Create byte array signature
        /// </summary>
        /// <param name="value">byte array to create signature for</param>
        /// <returns>signature for byte array</returns>
        public static string Create(byte[] bytes)
        {
            using (DSA dsa = DSA.Create())
            {
                // --- init with private key ---
                dsa.FromXmlString(privateDSAKey);

                // --- generate signature and return as string ----
                byte[] signature = dsa.SignData(bytes, HashAlgorithmName.SHA1);
                return Convert.ToBase64String(signature);
            }            
        }

        /// <summary>
        /// Verifiy double, accountRow pair signature
        /// </summary>
        /// <param name="value">double value of pair</param>
        /// <param name="account">DataRow representing the account of pair</param>
        /// <param name="signature">signature to verify</param>
        /// <returns>True, if signature match the double, accountRow pair, otherwise False</returns>
        public static bool Verify(double value, DataRow account, string signature)
        {
            return Verify($"{value};{account["Number"]}", signature);
        }

        /// <summary>
        /// Verifiy double signature
        /// </summary>
        /// <param name="value">double value</param>
        /// <param name="signature">signature to verify</param>
        /// <returns>True, if signature match the double value, otherwise False</returns>
        public static bool Verify(double value, string signature)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return Verify(bytes, signature);
        }

        /// <summary>
        /// Verifiy byte array signature
        /// </summary>
        /// <param name="bytes">byte array value</param>
        /// <param name="signature">signature to verify</param>
        /// <returns>True, if signature match the byte array value, otherwise False</returns>
        public static bool Verify(byte[] bytes, string signature)
        {
            bool verify = false;
            using (DSA dsa = DSA.Create())
            {
                // --- init with private key ---
                dsa.FromXmlString(publicDSAKey);

                // --- generate signature and return as string ----
                byte[] signatureByte = Convert.FromBase64String(signature);

                try
                {
                    verify = dsa.VerifyData(bytes, signatureByte, HashAlgorithmName.SHA1);
                }
                catch (Exception)
                {
                }
            }            
            return verify;
        }

        /// <summary>
        /// Verifiy string signature
        /// </summary>
        /// <param name="text">text value</param>
        /// <param name="signature">signature to verify</param>
        /// <returns>True, if signature match the string value, otherwise False</returns>
        public static bool Verify(string text, string signature)
        {
            using (DSA dsa = DSA.Create())
            {
                // --- init with private key ---
                dsa.FromXmlString(publicDSAKey);

                // --- generate signature and return as string ----
                byte[] textByte      = Encoding.UTF8.GetBytes(text);
                byte[] signatureByte = Convert.FromBase64String(signature);

                return dsa.VerifyData(textByte, signatureByte, HashAlgorithmName.SHA1);
            }            
        }
    }
}
