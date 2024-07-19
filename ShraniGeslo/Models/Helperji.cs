using System.Text;
using System.Security.Cryptography;
public static class Helper
{
    public static string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                // Concatenate the salt and hashed password for storage
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
            
        }
        public static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16]; // Adjust the size based on your security requirements
                rng.GetBytes(salt);
                return salt;
            }
        }
        // public async Task<string> UserVerify(Uporabnik verify)
        // {

        //     // In a real scenario, you would retrieve these values from your database
        //     var user = _dbContext.Usertests.Where(x => x.Mobile == verify.MobileNo).Select(x => x).FirstOrDefault();

        //     string storedHashedPassword = user.ConfirmPassword;// "hashed_password_from_database";
        //     //string storedSalt = user.Salt; //"salt_from_database";
        //     byte[] storedSaltBytes = user.Salt;
        //     string enteredPassword = verify.ConfirmPassword; //"user_entered_password";

        //     // Convert the stored salt and entered password to byte arrays
        //     // byte[] storedSaltBytes = Convert.FromBase64String(user.Salt);
        //     byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

        //     // Concatenate entered password and stored salt
        //     byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
        //     Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
        //     Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

        //     // Hash the concatenated value
        //     string enteredPasswordHash = HashPassword(enteredPassword, storedSaltBytes);

        //     // Compare the entered password hash with the stored hash
        //     if (enteredPasswordHash == storedHashedPassword)
        //     {
        //         return "Password is correct.";
        //     }
        //     else
        //     {
        //         return "Password is incorrect.";
        //     }
        // }
}