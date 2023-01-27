using Microsoft.EntityFrameworkCore;

namespace Shop.Core.Services
{
    public class UserServics : IUserServics
    {
        readonly  private ShopDbContext _context;
        private readonly IViewRenderService _viewRenderService;
        public UserServics(ShopDbContext Context,IViewRenderService viewRenderService)
        {
            _context = Context;
            _viewRenderService= viewRenderService;
        }
        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PassWordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FexedEmail(login.Email);
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public bool ActiveAccount(string id)
        {
           var user= _context.Users.SingleOrDefault(u => u.ActiveCode == id);
            if (user ==null||user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode =NameGenerator.GenerateUniqCode();
            _context.SaveChanges();
            return true;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId; 
        }

        public bool IsExistEmail(string email)
        {
            return  _context.Users.Any(u => u.Email==email);
        }

        public bool IsExistUsername(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User GetUserByName(string userName)
        {
            return _context.Users.SingleOrDefault(u=>u.UserName==userName);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
           return _context.Users.SingleOrDefault(u=>u.UserId==id);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u=>u.ActiveCode==activeCode);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

        }

        public InformationUserViewModel GetUserInformation(string userName)
        {
            var user = GetUserByUserName(userName);
            InformationUserViewModel information = new();
            information.UserAvatar = user.UserAvatar;
            information.CreateDate = user.RegisterDate;
            information.UserName = user.UserName;
            information.CodeMelli=user.CodeMelli;
            information.Phone = user.PhoneNamber;
            information.Email = user.Email;
            information.Wallet = BalanceUserWallet(userName);
            return information;
        }

        public User GetUserByUserName(string username)
        {
           return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public SlideBserUserPanelViewModel GetSlideBarUserPanelData(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).Select(u => new 
            SlideBserUserPanelViewModel { 
            
             CreateDate=u.RegisterDate,
              UserAvatar=u.UserAvatar,
               UserName=u.UserName
            }).Single();
        }

        public EditProfileUserPanelViewModel GetDataForEditProfileUserPanel(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).Select(u => new EditProfileUserPanelViewModel
            {
                UserName = u.UserName,
                
                Name = u.Name, 
                CodeMelli = u.CodeMelli,
                Email = u.Email,
                Lname = u.Lname,
                PhoneNamber = u.PhoneNamber,
                PostalCode = u.PostalCode,
                
                AvatarName = u.UserAvatar,

            }).Single();
        }

        public int UpdateProfile(string username, EditProfileUserPanelViewModel profile)
        {
            if (profile.UserAvatar !=null)
            {
                string ImagePath = "";
                if (profile.AvatarName!= "Defult.jpg")
                {
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar",profile.AvatarName);
                    if (File.Exists(ImagePath))
                    {
                        File.Delete(ImagePath);
                    }
                    profile.AvatarName = NameGenerator.GenerateNameForImage() + Path.GetExtension(profile.UserAvatar.FileName);
                    ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwRoot/UserAvatar", profile.AvatarName);

                    using var stream = new FileStream(ImagePath, FileMode.Create);
                    profile.UserAvatar.CopyTo(stream);
                    
                }

            }
            int ChangeEmail = 0;
            var user = GetUserByName(username);
            if (user.Email!=profile.Email)
            {
            
                
                user.IsActive = false;
                user.ActiveCode=NameGenerator.GenerateUniqCode();
                ChangeEmail = 1;
            } 
            user.Email = profile.Email;
            user.Lname=profile.Lname;
            user.Name = profile.Name;
            user.CodeMelli = profile.CodeMelli;
            user.PostalCode = profile.PostalCode;
            user.UserAvatar = profile.AvatarName;
            user.PhoneNamber = profile.PhoneNamber;
            UpdateUser(user);
            if (ChangeEmail==1)
            {
                var body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
                SendEmail.Send(user.Email, "فعال سازی ", body);
            }
          

            return ChangeEmail;
        }

        public bool CompareOldPassWord(string userName, string oldpassword)
        {
            string hasholdpassword = PassWordHelper.EncodePasswordMd5(oldpassword);
            return _context.Users.Any(u => u.UserName == userName && u.Password == hasholdpassword);
        }

        public void ChangeUserPasswword(string userName, string newpassword)
        {
          var  user=GetUserByUserName(userName);
            user.Password = PassWordHelper.EncodePasswordMd5(newpassword);
            UpdateUser(user);

        }

        public int BalanceUserWallet(string userName)
        {
           int userId=GetUserIdByUserName(userName);
            var Deposit = _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 1&&w.IsPay)
                .Select(w => w.Amount).ToList();
            var Withdraw= _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 2&&w.IsPay)
                .Select(w => w.Amount).ToList();

            return (Deposit.Sum()-Withdraw.Sum());

        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public List<WalletViewModel> GetWalletUser(string userName)
        {
            int userId = GetUserIdByUserName(userName);
            return _context.Wallets.Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new WalletViewModel()

                {
                    Amount = w.Amount,
                    CreateDate = w.CreateDate,
                    Description = w.Description,
                    Type = w.TypeId
                }).ToList();
        }

        public int ChargeWallet(string username,string description, int amount, bool isPay = false)
        {
            Wallet wallet =new ()
            {
                Amount = amount,
                IsPay=isPay,
                Description=description,
                CreateDate=DateTime.Now,
                TypeId=1,
                UserId=GetUserIdByUserName(username),
                  

            };


          return  AddWallet(wallet);



        }

        public int AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public Wallet GetWalletByWalletId(int walletId)
        {
            return _context.Wallets.Find(walletId);

        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

        public UserForAdminViewModel GetUsers(int pageid = 1, string filtercodeMelli = "", string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filtercodeMelli))
            {
                result = result.Where(u => u.CodeMelli.Contains(filtercodeMelli));
            }

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => EF.Functions.Like(u.Email,filtercodeMelli));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => EF.Functions.Like(u.UserName, filterUserName));
            }

            int take = 20;
            int skip = (pageid - 1) * take;

            UserForAdminViewModel list = new();
            list.CurrentPage=pageid;
            list.PageCount=result.Count()/take;
            list.Users=result.OrderBy(u=>u.RegisterDate).Skip(skip).Take(take).ToList();
            return list;
        }

        public int AddUserFromAdmin(CreateUserViewModel user)
        {

            User addUser = new();
            addUser.Email = user.Email;
            addUser.UserName = user.UserName;
            addUser.Password =PassWordHelper.EncodePasswordMd5(user.Password);
            addUser.ActiveCode = NameGenerator.GenerateUniqCode();
            addUser.RegisterDate= DateTime.Now;
            addUser.Name = "";
            addUser.Lname = "";
            addUser.PhoneNamber = "";
            addUser.IsActive = true;
            addUser.PostalCode = "";
            addUser.CodeMelli = "";
            #region Avataer
            if (user.UserAvater != null&& user.UserAvater.IsImage())
            {
                string ImagePass = "";
                addUser.UserAvatar = NameGenerator.GenerateNameForImage() + Path.GetExtension(user.UserAvater.FileName);
                ImagePass = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", addUser.UserAvatar);
                
                using var stream = new FileStream(ImagePass, FileMode.Create);
                user.UserAvater.CopyTo(stream);
            }
            #endregion

           return AddUser(addUser);
        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId).Select(u => new EditUserViewModel()
            {
                Email = u.Email,
                UserId = u.UserId, AvatarName = u.UserAvatar, UserName = u.UserName,
               
                UserRoles = u.UserRoles.Select(r=>r.RoleId).ToList()


            }).Single();
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            var user =GetUserById(editUser.UserId);
            user.Email = editUser.Email;

            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PassWordHelper.EncodePasswordMd5(editUser.Password);
            }
            if (editUser.UserAvater != null)
            {
                //Delete old Image
                if (editUser.AvatarName != "Defult.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GenerateNameForImage() + Path.GetExtension(editUser.UserAvater.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
                using var stream = new FileStream(imagePath, FileMode.Create);
                 editUser.UserAvater.CopyTo(stream);
                
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public InformationUserViewModel GetUserInformation(int userId)
        {
            var user = GetUserById(userId);
            InformationUserViewModel information = new();
            information.UserAvatar = user.UserAvatar;
            information.CreateDate = user.RegisterDate;
            information.UserName = user.UserName;
            information.CodeMelli = user.CodeMelli;
            information.Phone = user.PhoneNamber;
            information.Email = user.Email;
            information.Wallet = BalanceUserWallet(user.UserName);
            return information;
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }

        public UserForAdminViewModel GetUsersForListDeleteUsers(int pageid = 1, string filtercodeMelli = "", string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u=>u.IsDelete);

            if (!string.IsNullOrEmpty(filtercodeMelli))
            {
                result = result.Where(u => u.CodeMelli.Contains(filtercodeMelli));
            }

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => EF.Functions.Like(u.Email, filtercodeMelli));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => EF.Functions.Like(u.UserName, filterUserName));
            }

            int take = 20;
            int skip = (pageid - 1) * take;

            UserForAdminViewModel list = new();
            list.CurrentPage = pageid;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();
            return list;
        }
    }
}
