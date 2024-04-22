using LibBusiness.Models;
using LibBusiness.Persistence;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace LibBusiness.Business.Core
{
    static class Login
    {
        private static DBContext dbp = new DBContext();
        private static string hashString(string palavra)
        {
            // Salt para aumentar segurança
            string salt = "$%sb3df%/8&df";

            // Combinar a palavra e o salt
            string combinedString = palavra + salt;

            // Aplicar hash SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(combinedString);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Converter o hash em uma string hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
        public static bool validarUser(string username, string senha)
        {
            string hashSenha = hashString(senha);

            var user = dbp.Utilizadores.FirstOrDefault(u => u.Username == username && u.Password == hashSenha);
            return user != null;
        }

        public static void addUser(string username, string senha)
        {
            string hashSenha = hashString(senha);

            Utilizador utilizador = new Utilizador();
            utilizador.Username = username;
            utilizador.Password = hashSenha;

            dbp.Utilizadores.Add(utilizador);

            dbp.SaveChanges();
        }

        public static void removeUser(string Username)
        {
            // Obter o Utilizador com base no ID fornecido
            Utilizador utilizador = dbp.Utilizadores.FirstOrDefault(u => u.Username == Username);

            if (utilizador != null)
            {
                // Remover o Utilizador do DbSet e aplicar as alterações no banco de dados
                dbp.Utilizadores.Remove(utilizador);
                dbp.SaveChanges();
            }
            else
            {
                throw new Exception("Utilizador nao encontrado");
            }
        }

        public static void editUser(int UserId, string username, string senha)
        {
            Utilizador utilizador = dbp.Utilizadores.Find(UserId);
            if (utilizador != null)
            {
                string hashUser = hashString(username);
                string hashSenha = hashString(senha);
                utilizador.Username = hashUser;
                utilizador.Password = hashSenha;
                dbp.SaveChanges();
            }
            else
            {
                throw new Exception("Utilizador nao encontrado");
            }
        }

        public static List<string> getUsernames()
        {
            List<string> usernames = dbp.Utilizadores.Select(u => u.Username).ToList();

            return usernames;
        }

        public static int countUsers()
        {
            return dbp.Utilizadores.Count();
        }

        public static bool IsEqualTo(this SecureString ss1, SecureString ss2)
        {
            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2)
                {
                    for (int x = 0; x < length1; ++x)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, x);
                        byte b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
                }
                else return false;
                return true;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }
        public static void resetPassword(Utilizador utilizador)
        {
            var userBd = dbp.Utilizadores.FirstOrDefault(u => u.Username == utilizador.Username);
            userBd.Password = hashString(utilizador.Password);
            dbp.Utilizadores.Update(userBd);
            dbp.SaveChanges();

        }
        public static void firstInit() {
            if (dbp.Utilizadores.Count() < 1)
            {
                addUser("admin", "123");
            }
        }
    }
}
