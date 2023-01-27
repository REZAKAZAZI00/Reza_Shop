
namespace Shop.Core.Services.InterFace
{
    public interface IUserServics
    {

        
        bool IsExistUsername(string userName);
        bool IsExistEmail(string email);

        int AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        bool ActiveAccount(string id);
        User LoginUser(LoginViewModel login);
        User GetUserByName(string userName);
        User GetUserByEmail(string email);
        User GetUserById(int id);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string username);
        int GetUserIdByUserName(string userName);
        #region UserPanel
        InformationUserViewModel GetUserInformation(string userName);
        InformationUserViewModel GetUserInformation(int userId);
        SlideBserUserPanelViewModel GetSlideBarUserPanelData(string userName);
        EditProfileUserPanelViewModel GetDataForEditProfileUserPanel(string userName);
        int UpdateProfile(string username, EditProfileUserPanelViewModel profile);
        bool CompareOldPassWord(string userName, string oldpassword);
        void ChangeUserPasswword(string userName, string newpassword);
        #endregion
        #region Wallet

        int BalanceUserWallet(string userName);
        List<WalletViewModel> GetWalletUser(string userName);
        int ChargeWallet(string username, string description, int amount,bool isPay=false);
        int AddWallet(Wallet wallet);
        void UpdateWallet(Wallet wallet);
        Wallet GetWalletByWalletId(int walletId);
        #endregion
        #region Admin Panel
        UserForAdminViewModel GetUsers(int pageid=1,string filtercodeMelli="", string filterEmail="", string filterUserName= "");
        UserForAdminViewModel GetUsersForListDeleteUsers(int pageid = 1, string filtercodeMelli = "", string filterEmail = "", string filterUserName = "");
        int AddUserFromAdmin(CreateUserViewModel user);
        EditUserViewModel GetUserForShowInEditMode(int userId);
        void EditUserFromAdmin(EditUserViewModel editUser);
        #endregion
    }
}
