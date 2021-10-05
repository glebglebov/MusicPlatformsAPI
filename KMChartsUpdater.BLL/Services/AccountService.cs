using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.BLL.Utils;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;

namespace KMChartsUpdater.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _uow;

        public AccountService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public Account GetAccount(int accountId, string key)
        {
            var account = _uow.Accounts.Get(accountId);

            if (account == null || account.SessionKey != key)
                return null;

            return account;
        }

        public Response Register(string login, string password, string organization)
        {
            if (!AreLoginAndPasswordCorrect(login, password))
                return new ErrorResponse("login or password is incorrect");

            string loginNormalized = NormalizeLogin(login);

            if (IsLoginAlreadyTaken(loginNormalized))
                return new ErrorResponse("login is already taken");

            string salt = RandomString.Generate(8);
            string passwordHash = GenerateMd5Hash(password, salt);

            CreateAccount(login, loginNormalized, organization, salt, passwordHash);

            return new DoneResponse("account created");
        }

        public Response Login(string login, string password)
        {
            string loginNormalized = NormalizeLogin(login);

            var account = GetAccountByLogin(loginNormalized);

            if (account == null || account.PasswordHash == null || account.Salt == null)
                return new ErrorResponse("login or password is incorrect");

            string passwordHash = GenerateMd5Hash(password, account.Salt);

            if (account.PasswordHash != passwordHash)
                return new ErrorResponse("login or password is incorrect");

            string sessionKey = GenerateSessionKey(account);

            account.LastLoginDate = DateTime.Now;
            account.SessionKey = sessionKey;

            _uow.Accounts.Update(account);
            _uow.Save();

            return new SuccessfulLoginResponse
            {
                Id = account.Id,
                Login = account.Login,
                Key = sessionKey
            };
        }

        public Response LoginThroughVk(string id, string username, string token)
        {
            string login = "vk_id" + id;

            var account = GetAccountByLogin(login);

            if (account == null)
            {
                account = new Account
                {
                    Login = login,
                    LoginNormalized = login,
                    Username = username,
                    VkToken = token,
                    VkId = id,
                    AccessLevel = 1,
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now
                };

                _uow.Accounts.Add(account);
                _uow.Save();
            }
            else
            {
                account.VkToken = token;
            }

            string sessionKey = GenerateSessionKey(account);

            account.SessionKey = sessionKey;
            account.LastLoginDate = DateTime.Now;

            _uow.Accounts.Update(account);
            _uow.Save();

            return new SuccessfulLoginResponse
            {
                Id = account.Id,
                Login = account.Login,
                Key = sessionKey
            };
        }

        private void CreateAccount(string login, string loginNormalized, string organization,
            string salt, string passwordHash)
        {
            var account = new Account
            {
                Login = login,
                LoginNormalized = loginNormalized,
                Username = login,
                Organization = organization,
                Salt = salt,
                PasswordHash = passwordHash,
                AccessLevel = 1,
                RegistrationDate = DateTime.Now,
                LastLoginDate = DateTime.Now
            };

            _uow.Accounts.Add(account);
            _uow.Save();
        }

        private bool AreLoginAndPasswordCorrect(string login, string password)
        {
            Regex regex = new Regex("^(?=[a-zA-Z0-9._]{4,20}$)(?!.*[_.]{2})[^_.].*[^_.]$");
            bool isLoginCorrect = regex.IsMatch(login);

            bool isPasswordCorrect = true;

            return isLoginCorrect && isPasswordCorrect;
        }

        private bool IsLoginAlreadyTaken(string login)
        {
            var account = GetAccountByLogin(login);

            return account != null;
        }

        private Account GetAccountByLogin(string login)
        {
            var account = _uow.Accounts.GetAll
                .SingleOrDefault(x => x.LoginNormalized == login);

            return account;
        }

        private string GenerateMd5Hash(string password, string salt)
        {
            string saltedPassword = password + salt;
            string hash = CreateMD5(saltedPassword);

            return hash;
        }

        private string CreateMD5(string input)
        {
            string hash;

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                hash = sb.ToString();
            }

            return hash;
        }

        private string NormalizeLogin(string login)
        {
            return login.ToLower();
        }

        private string GenerateSessionKey(Account account)
        {
            string salt = RandomString.Generate(8);
            string hash = CreateMD5(salt + account.LoginNormalized);

            return hash;
        }
    }
}
