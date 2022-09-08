using rapsoSuiteMaster.Data;

namespace rapsoSuiteMaster.interfaces
{
    public interface Iuser
    {
        public Task<taskResponse> createUser(basicNetUserInfo _basicNetUserInfo, string password);
        public Task<taskResponse> updatePassword(string _email, string _oldPassword, string _newPassword);

        public Task<taskResponse> updateEmail(string _oldEmail, string _newEmail);

        public Task<taskResponse> updateNameUser(string _oldName, string _newName);

        public Task<bool> validateEmail(string email);
        public Task<bool> validateUserName(string userName);

    }
}
