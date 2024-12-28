using System.Text;

namespace Application.Helpers
{
    public class HashingHelperForApplicationLayer
    {
        public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                byte[] passwordSaltByte = hmac.Key;
                byte[] passwordHashByte = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                passwordHash = Convert.ToBase64String(passwordHashByte);
                passwordSalt = Convert.ToBase64String(passwordSaltByte);
            }
        }

        public static bool VeriFyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            byte[] PasswordSaltBytes = Convert.FromBase64String(passwordSalt);

            byte[] passwordHashByte = Convert.FromBase64String(passwordHash);


            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSaltBytes))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHashByte[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}