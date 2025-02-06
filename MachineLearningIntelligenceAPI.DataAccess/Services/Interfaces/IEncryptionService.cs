namespace MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Asymetrically encrypt a password with bcrypt. Salts are managed automatically.
        /// </summary>
        public string BcryptEncryptPassword(string password);

        /// <summary>
        /// Asymetrically verify a password with bcrypt. Salts are managed automatically.
        /// </summary>
        public bool BcryptIsPasswordValid(string password, string hash);

        /// <summary>
        /// Generate cryptographically secure salt with a given length
        /// </summary>
        public string GenerateSaltWithLength(int length);
    }
}
