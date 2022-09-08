using Microsoft.AspNetCore.Identity;
using rapsoSuiteMaster.Data;
using rapsoSuiteMaster.interfaces;
using System.Text.Json;


namespace rapsoSuiteMaster.Services
{
    public class userServices 
    {
        private readonly UserManager<IdentityUser> _userMgr;

        public userServices(UserManager<IdentityUser> userMgr)
        {
            _userMgr = userMgr;
        }

        public async Task<taskResponse> createUser(basicNetUserInfo _basicNetUserInfo, string password)
        {
            var _validaEmail = await validateEmail(_basicNetUserInfo.email);

            if (_validaEmail.result != "Ok")
            {
                return new taskResponse("createUser", "Fail", "Duplicated Email");
            }

            var _IdentityUser = Activator.CreateInstance<IdentityUser>();

            _IdentityUser.Email = _basicNetUserInfo.email;
            _IdentityUser.PhoneNumber = _basicNetUserInfo.phone;
            _IdentityUser.UserName = _basicNetUserInfo.userName;
            _IdentityUser.NormalizedUserName = _IdentityUser.UserName.ToUpper();
            _IdentityUser.EmailConfirmed = true;

            var _createUser = await _userMgr.CreateAsync(_IdentityUser, _basicNetUserInfo.password);

            if (_createUser.Succeeded == true)
            {
                _basicNetUserInfo.id = _IdentityUser.Id;
                string createUser = JsonSerializer.Serialize(_basicNetUserInfo);
                return new taskResponse("createUser", "Ok", createUser); // *** MOSCA ***
            }
            else
            {
                return new taskResponse("createUser", "Fail", _createUser.ToString());
            }



        }

        public async Task<taskResponse> setRoleUser(basicNetUserInfo _basicNetUserInfo, string password)
        {
            var _validaEmail = await validateEmail(_basicNetUserInfo.email);

            if (_validaEmail.result != "Ok")
            {
                return new taskResponse("createUser", "Fail", "Duplicated Email");
            }

            var _IdentityUser = Activator.CreateInstance<IdentityUser>();

            _IdentityUser.Email = _basicNetUserInfo.email;
            _IdentityUser.PhoneNumber = _basicNetUserInfo.phone;
            _IdentityUser.UserName = _basicNetUserInfo.userName;
            _IdentityUser.NormalizedUserName = _IdentityUser.UserName.ToUpper();
            _IdentityUser.EmailConfirmed = true;

            var _createUser = await _userMgr.CreateAsync(_IdentityUser, _basicNetUserInfo.password);

            if (_createUser.Succeeded == true)
            {
                _basicNetUserInfo.id = _IdentityUser.Id;
                string createUser = JsonSerializer.Serialize(_basicNetUserInfo);
                return new taskResponse("createUser", "Ok", createUser); // *** MOSCA ***
            }
            else
            {
                return new taskResponse("createUser", "Fail", _createUser.ToString());
            }

            


        }

        #region  "Procedures for Update"
        public async Task<taskResponse> updatePassword(string _email, string _oldPassword, string _newPassword)
        {
            var _IdentityUser = await _userMgr.FindByEmailAsync(_email);

            var changePasswordResult = await _userMgr.ChangePasswordAsync(_IdentityUser, _oldPassword, _newPassword);

            // ToDo: colocarle todos sus opciones de resultado
            return new taskResponse("updatePassword", "Ok", changePasswordResult.ToString()); // *** MOSCA ***
        }

        public async Task<taskResponse> updateEmail(string _oldEmail, string _newEmail)
        {
            var _IdentityUser = await _userMgr.FindByEmailAsync(_oldEmail);

            var tokenEmail = await _userMgr.GenerateChangeEmailTokenAsync(_IdentityUser, _newEmail);
            var ChangeEmailResult = await _userMgr.ChangeEmailAsync(_IdentityUser, _newEmail, tokenEmail);


            // ToDo: colocarle todos sus opciones de resultado
            return new taskResponse("updatePassword", "Ok", ChangeEmailResult.ToString()); // *** MOSCA ***
        }

        public async Task<taskResponse> updateTelephone(string _newTelephone, string _name)
        {
            var _IdentityUser = await _userMgr.FindByNameAsync(_name);

            var tokenTelephone = await _userMgr.GenerateChangePhoneNumberTokenAsync(_IdentityUser, _newTelephone);
            var ChangeEmailResult = await _userMgr.ChangePhoneNumberAsync(_IdentityUser, _newTelephone, tokenTelephone);

            // ToDo: colocarle todos sus opciones de resultado
            return new taskResponse("updatePassword", "Ok", ChangeEmailResult.ToString()); // *** MOSCA ***
        }

        public async Task<taskResponse> updateNameUser(string _oldName, string _newName)
        {
            var _IdentityUser = await _userMgr.FindByNameAsync(_oldName);

            _IdentityUser.UserName = _newName;
            _IdentityUser.NormalizedUserName = _IdentityUser.UserName.ToUpper();

            var ChangeUserName = await _userMgr.UpdateAsync(_IdentityUser);


            // ToDo: colocarle todos sus opciones de resultado
            return new taskResponse("updatePassword", "Ok", ChangeUserName.ToString()); // *** MOSCA ***
        }

        #endregion

        #region "Procedudes to validate"
        public async Task<taskResponse> validateEmail(string email)
        {
            var _validateEmail = await _userMgr.FindByEmailAsync(email);

            return _validateEmail == null ? 
                new taskResponse("validateEmail", "Ok", "Email is OK") :
                new taskResponse("validateEmail", "Fail", "Repeated email");
        }

        public async Task<taskResponse> validateUserName(string userName)
        {
            var validateUserName = await _userMgr.FindByNameAsync(userName);

            return validateUserName == null ?
                 new taskResponse("validateUserName", "Ok", "User name is OK") :
                 new taskResponse("validateUserName", "Fail", "Repeated username");
        }
        #endregion



    }




}
