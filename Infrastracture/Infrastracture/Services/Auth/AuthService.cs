using Application.Abstractions.Auth;

namespace Infrastracture.Services.Auth
{
    public class AuthService : IAuthService
    {
        //    private readonly ITokenHandler _tokenHandler;
        //    private readonly IUserReadRepository _userReadRepository;
        //    private readonly IUserWriteRepository _userWriteRepository;
        //    private readonly ILogSuccessedLoginWriteRepository _logSuccessedLoginWriteRepository;
        //    private readonly ILogFailedLoginWriteRepository _logFailedLoginWriteRepository;
        //    private readonly ILogFailedLoginReadRepository _logFailedLoginReadRepository;
        //    private readonly GetUserInfo _getUserInfo;
        //    private readonly IHttpContextAccessor _httpContextAccessor;
        //    private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
        //    private readonly IUserRefreshTokenReadRepository _userRefreshTokenReadRepository;
        //    private readonly IUserRefreshTokenWriteRepository _userRefreshTokenWriteRepository;
        //    private readonly IMapper _mapper;
        //    private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
        //    private readonly IReservationHotelStaffReadRepository _reservationHotelStaffReadRepository;

        //    public AuthService(ITokenHandler tokenHandler, IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, ILogSuccessedLoginWriteRepository logSuccessedLoginWriteRepository, GetUserInfo getUserInfo, ILogFailedLoginWriteRepository logFailedLoginWriteRepository, ILogFailedLoginReadRepository logFailedLoginReadRepository, IHttpContextAccessor httpContextAccessor, IAuthUserRoleReadRepository authUserRoleReadRepository, IMapper mapper, IUserRefreshTokenReadRepository userRefreshTokenReadRepository, IUserRefreshTokenWriteRepository userRefreshTokenWriteRepository, IPartTimeWorkerReadRepository partTimeWorkerReadRepository, IReservationHotelStaffReadRepository reservationHotelStaffReadRepository)
        //    {
        //        _tokenHandler = tokenHandler;
        //        _userReadRepository = userReadRepository;
        //        _userWriteRepository = userWriteRepository;
        //        _logSuccessedLoginWriteRepository = logSuccessedLoginWriteRepository;
        //        _getUserInfo = getUserInfo;
        //        _logFailedLoginWriteRepository = logFailedLoginWriteRepository;
        //        _logFailedLoginReadRepository = logFailedLoginReadRepository;

        //        _httpContextAccessor = httpContextAccessor;
        //        _authUserRoleReadRepository = authUserRoleReadRepository;
        //        _mapper = mapper;
        //        _userRefreshTokenReadRepository = userRefreshTokenReadRepository;
        //        _userRefreshTokenWriteRepository = userRefreshTokenWriteRepository;
        //        _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
        //        _reservationHotelStaffReadRepository = reservationHotelStaffReadRepository;
        //    }

        //    public async Task<RegisterAuthResponse> Register(RegisterAuthCommand registerAuthCommand)
        //    {
        //        var result = await UserExists(registerAuthCommand.Email);
        //        if (result == true)
        //        {
        //            throw new Exception(AuthBussinessMessages.EmailAlreadyExist);
        //        }
        //        //  await IdNumberCheched(registerAuthCommand.IdNumber);
        //        await PhoneNumberChecked(registerAuthCommand.Gsm);



        //        //þifre güncelleme token ve þifre güncelleme token süresine bakýlacak.

        //        string passwordHash, passwordSalt;
        //        HashingHelperForAuth.CreatePasswordHash(registerAuthCommand.Password, out passwordHash, out passwordSalt);
        //        var user = new User
        //        {
        //            Name = registerAuthCommand.Name,
        //            Surname = registerAuthCommand.Surname,
        //            Email = registerAuthCommand.Email,
        //            Gsm = registerAuthCommand.Gsm,
        //            Password = passwordHash,
        //            PasswordHash = passwordSalt,
        //            IsLoginStatus = true,
        //            IsSystemAdmin = false,
        //            UpdatePasswordToken = "gececi token",
        //        };

        //        await _userWriteRepository.AddAsync(user);

        //        await _userWriteRepository.SaveAsync();

        //        return new()
        //        {
        //            Title = AuthBussinessMessages.ProcessCompleted,
        //            IsValid = true,
        //            Message = AuthBussinessMessages.SuccessRegister
        //        };
        //    }

        //    public async Task<LoginAuthResponse> Login(LoginAuthCommand loginAuthCommand)
        //    {

        //        #region Too Many Failed Attempts

        //        List<LogFailedLogin> lstLogFailedLogins = GetFailedLogin(loginAuthCommand.Email, _getUserInfo.GetUserIpAddress(), 2);

        //        if (lstLogFailedLogins.Count > 5)
        //        {
        //            return new LoginAuthResponse()
        //            {
        //                Title = "Incorrect Operation",
        //                ActionType = ActionType.Error.ToString(),
        //                Message = "Too many failed attempts. Please try again 2 hours later",
        //                IsValid = false,
        //                Token = null
        //            };
        //        }

        //        #endregion

        //        var userToCheck = await _userReadRepository.GetSingleAsync(u => u.Email == loginAuthCommand.Email);

        //        #region Is Pasif User

        //        if (userToCheck != null && !userToCheck.IsLoginStatus)
        //        {
        //            return new LoginAuthResponse()
        //            {
        //                Title = "Incorrect Operation",
        //                ActionType = ActionType.Error.ToString(),
        //                Message = "Please contact the system administrator",
        //                IsValid = false,
        //                Token = null
        //            };
        //        }

        //        #endregion

        //        if (userToCheck == null)
        //        {
        //            #region LogFailedLogin

        //            LogFailedLogin logFailedLogin = new LogFailedLogin();
        //            logFailedLogin.Email = loginAuthCommand.Email;
        //            logFailedLogin.Password = loginAuthCommand.Password;
        //            logFailedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //            logFailedLogin.Description = "Email Not Found!";

        //            await _logFailedLoginWriteRepository.AddAsync(logFailedLogin);
        //            await _logFailedLoginWriteRepository.SaveAsync();

        //            #endregion

        //            return new LoginAuthResponse()
        //            {
        //                Title = AuthBussinessMessages.IncorrectOperation,
        //                ActionType = ActionType.Error.ToString(),
        //                Message = AuthBussinessMessages.EmailNotFound,
        //                IsValid = false,
        //                Token = null
        //            };
        //        }



        //        if (!HashingHelperForAuth.VeriFyPasswordHash(loginAuthCommand.Password, userToCheck.Password, userToCheck.PasswordHash))
        //        {
        //            #region LogFailedLogin

        //            LogFailedLogin logFailedLogin = new LogFailedLogin();
        //            logFailedLogin.Email = loginAuthCommand.Email;
        //            logFailedLogin.Password = loginAuthCommand.Password;
        //            logFailedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //            logFailedLogin.Description = "Email found but password is wrong!";

        //            await _logFailedLoginWriteRepository.AddAsync(logFailedLogin);
        //            await _logFailedLoginWriteRepository.SaveAsync();

        //            #endregion

        //            return new LoginAuthResponse()
        //            {
        //                Title = AuthBussinessMessages.IncorrectOperation,
        //                ActionType = ActionType.Error.ToString(),
        //                Message = AuthBussinessMessages.EmailOrPasswordIncorrect,
        //                IsValid = false,
        //                Token = null
        //            };
        //        }

        //        #region token - refresh token
        //        T.Token token = _tokenHandler.CreateAccessToken(userToCheck, 600);
        //        var sessionId = Guid.NewGuid().ToString();
        //        //_httpContextAccessor.HttpContext.Session.SetString("SessionId", sessionId); 
        //        var userRefreshToken = await _userRefreshTokenReadRepository.GetAsync(x => x.GidUserFK == userToCheck.Gid);

        //        if (userRefreshToken == null)
        //        {
        //            UserRefreshToken refreshToken = new UserRefreshToken
        //            {
        //                GidUserFK = userToCheck.Gid,
        //                RefreshToken = token.RefreshToken,
        //                Expiration = token.RefreshTokenExpiration,
        //            };
        //            await _userRefreshTokenWriteRepository.AddAsync(refreshToken);

        //        }
        //        else
        //        {
        //            userRefreshToken.RefreshToken = token.RefreshToken;
        //            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        //        }
        //        await _userRefreshTokenWriteRepository.SaveAsync();
        //        #endregion



        //        #region LogSuccessedLogin

        //        LogSuccessedLogin logSuccessedLogin = new LogSuccessedLogin();
        //        logSuccessedLogin.GidUserFK = userToCheck.Gid;
        //        logSuccessedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //        logSuccessedLogin.SessionId = sessionId;
        //        logSuccessedLogin.LogOutDate = null;

        //        await _logSuccessedLoginWriteRepository.AddAsync(logSuccessedLogin);


        //        await _logSuccessedLoginWriteRepository.SaveAsync();

        //        #endregion

        //        return new LoginAuthResponse()
        //        {
        //            Message = AuthBussinessMessages.SuccessLogin,
        //            IsValid = true,
        //            Token = token,
        //            SucceededLogGid = logSuccessedLogin.Gid,
        //            SessionId = sessionId,
        //        };
        //    }

        //    public async Task<LoginForPartTimeAuthResponse> LoginForPartTime(LoginForPartTimeAuthCommand loginAuthCommand)
        //    {

        //        #region Too Many Failed Attempts

        //        List<LogFailedLogin> lstLogFailedLogins = GetFailedLogin(loginAuthCommand.Username, _getUserInfo.GetUserIpAddress(), 2); // Email yerine username yazýldý

        //        if (lstLogFailedLogins.Count > 5)
        //        {
        //            return new LoginForPartTimeAuthResponse()
        //            {
        //                Title = "Incorrect Operation",
        //                ActionType = ActionType.Error.ToString(),
        //                Message = "Too many failed attempts. Please try again 2 hours later",
        //                IsValid = false,
        //                Token = null
        //            };
        //        }

        //        #endregion

        //        var userToCheck = await _partTimeWorkerReadRepository.GetSingleAsync(x => x.UserName == loginAuthCommand.Username);

        //        if (userToCheck == null)
        //        {
        //            #region LogFailedLogin

        //            LogFailedLogin logFailedLogin = new LogFailedLogin();
        //            logFailedLogin.Email = loginAuthCommand.Username;
        //            logFailedLogin.Password = loginAuthCommand.Password;
        //            logFailedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //            logFailedLogin.Description = "Kullanýcý Adý Bulunamadý!";

        //            await _logFailedLoginWriteRepository.AddAsync(logFailedLogin);
        //            await _logFailedLoginWriteRepository.SaveAsync();

        //            #endregion

        //            return new LoginForPartTimeAuthResponse()
        //            {
        //                Title = AuthBussinessMessages.IncorrectOperation,
        //                ActionType = ActionType.Error.ToString(),
        //                Message = AuthBussinessMessages.EmailNotFound,
        //                IsValid = false,
        //                Token = null
        //            };
        //        }



        //        if (!HashingHelperForAuth.VeriFyPasswordHash(loginAuthCommand.Password, userToCheck.Password, userToCheck.PasswordHash))
        //        {
        //            #region LogFailedLogin

        //            LogFailedLogin logFailedLogin = new LogFailedLogin();
        //            logFailedLogin.Email = loginAuthCommand.Username;
        //            logFailedLogin.Password = loginAuthCommand.Password;
        //            logFailedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //            logFailedLogin.Description = "Þifre Yanlýþ!";

        //            await _logFailedLoginWriteRepository.AddAsync(logFailedLogin);
        //            await _logFailedLoginWriteRepository.SaveAsync();

        //            #endregion

        //            return new LoginForPartTimeAuthResponse()
        //            {
        //                Title = AuthBussinessMessages.IncorrectOperation,
        //                ActionType = ActionType.Error.ToString(),
        //                Message = AuthBussinessMessages.EmailOrPasswordIncorrect,
        //                IsValid = false,
        //                Token = null
        //            };
        //        }

        //        #region token - refresh token
        //        T.Token token = _tokenHandler.CreateAccessTokenForPartTime(userToCheck, 600);
        //        var sessionId = Guid.NewGuid().ToString();

        //        //var userRefreshToken = await _userRefreshTokenReadRepository.GetAsync(x => x.GidUserFK == userToCheck.Gid);

        //        //if (userRefreshToken == null)
        //        //{
        //        //    UserRefreshToken refreshToken = new UserRefreshToken
        //        //    {
        //        //        GidUserFK = userToCheck.Gid,
        //        //        RefreshToken = token.RefreshToken,
        //        //        Expiration = token.RefreshTokenExpiration,
        //        //    };
        //        //    await _userRefreshTokenWriteRepository.AddAsync(refreshToken);

        //        //}
        //        //else
        //        //{
        //        //    userRefreshToken.RefreshToken = token.RefreshToken;
        //        //    userRefreshToken.Expiration = token.RefreshTokenExpiration;
        //        //}
        //        //await _userRefreshTokenWriteRepository.SaveAsync();
        //        #endregion



        //        //#region LogSuccessedLogin

        //        //LogSuccessedLogin logSuccessedLogin = new LogSuccessedLogin();
        //        //logSuccessedLogin.GidUserFK = userToCheck.Gid;
        //        //logSuccessedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //        //logSuccessedLogin.SessionId = sessionId;
        //        //logSuccessedLogin.LogOutDate = null;

        //        //await _logSuccessedLoginWriteRepository.AddAsync(logSuccessedLogin);


        //        //await _logSuccessedLoginWriteRepository.SaveAsync();

        //        //#endregion

        //        return new LoginForPartTimeAuthResponse()
        //        {
        //            Message = AuthBussinessMessages.SuccessLogin,
        //            IsValid = true,
        //            Token = token,
        //            //SucceededLogGid = logSuccessedLogin.Gid,
        //            SessionId = sessionId,
        //        };
        //    }

        //    public async Task<LoginForWorkerAuthResponse> LoginForWorker(LoginForWorkerAuthCommand loginAuthCommand)
        //    {
        //        #region Too Many Failed Attempts

        //        List<LogFailedLogin> lstLogFailedLogins = GetFailedLogin(loginAuthCommand.Phone, _getUserInfo.GetUserIpAddress(), 2); // Email yerine phone yazýldý

        //        if (lstLogFailedLogins.Count > 5)
        //        {
        //            return new LoginForWorkerAuthResponse()
        //            {
        //                Title = "Incorrect Operation",
        //                ActionType = ActionType.Error.ToString(),
        //                Message = "Too many failed attempts. Please try again 2 hours later",
        //                IsValid = false,
        //                Token = null
        //            };
        //        }

        //        #endregion

        //        var userToCheck = await _reservationHotelStaffReadRepository.GetSingleAsync(x => x.GsmNo == loginAuthCommand.Phone);

        //        if (userToCheck == null)
        //        {
        //            #region LogFailedLogin

        //            LogFailedLogin logFailedLogin = new LogFailedLogin();
        //            logFailedLogin.Email = loginAuthCommand.Phone;
        //            logFailedLogin.Password = loginAuthCommand.Password;
        //            logFailedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //            logFailedLogin.Description = "Telefon Numarasý Bulunamadý!";

        //            await _logFailedLoginWriteRepository.AddAsync(logFailedLogin);
        //            await _logFailedLoginWriteRepository.SaveAsync();

        //            #endregion

        //            return new LoginForWorkerAuthResponse()
        //            {
        //                Title = AuthBussinessMessages.IncorrectOperation,
        //                ActionType = ActionType.Error.ToString(),
        //                Message = AuthBussinessMessages.EmailNotFound,
        //                IsValid = false,
        //                Token = null
        //            };
        //        }



        //        if (!HashingHelperForAuth.VeriFyPasswordHash(loginAuthCommand.Password, userToCheck.Password, userToCheck.PasswordHash))
        //        {
        //            #region LogFailedLogin

        //            LogFailedLogin logFailedLogin = new LogFailedLogin();
        //            logFailedLogin.Email = loginAuthCommand.Phone;
        //            logFailedLogin.Password = loginAuthCommand.Password;
        //            logFailedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //            logFailedLogin.Description = "Þifre Yanlýþ!";

        //            await _logFailedLoginWriteRepository.AddAsync(logFailedLogin);
        //            await _logFailedLoginWriteRepository.SaveAsync();

        //            #endregion

        //            return new LoginForWorkerAuthResponse()
        //            {
        //                Title = AuthBussinessMessages.IncorrectOperation,
        //                ActionType = ActionType.Error.ToString(),
        //                Message = AuthBussinessMessages.EmailOrPasswordIncorrect,
        //                IsValid = false,
        //                Token = null
        //            };
        //        }

        //        #region token - refresh token
        //        T.Token token = _tokenHandler.CreateAccessTokenForWorker(userToCheck, 600);
        //        var sessionId = Guid.NewGuid().ToString();

        //        //var userRefreshToken = await _userRefreshTokenReadRepository.GetAsync(x => x.GidUserFK == userToCheck.Gid);

        //        //if (userRefreshToken == null)
        //        //{
        //        //    UserRefreshToken refreshToken = new UserRefreshToken
        //        //    {
        //        //        GidUserFK = userToCheck.Gid,
        //        //        RefreshToken = token.RefreshToken,
        //        //        Expiration = token.RefreshTokenExpiration,
        //        //    };
        //        //    await _userRefreshTokenWriteRepository.AddAsync(refreshToken);

        //        //}
        //        //else
        //        //{
        //        //    userRefreshToken.RefreshToken = token.RefreshToken;
        //        //    userRefreshToken.Expiration = token.RefreshTokenExpiration;
        //        //}
        //        //await _userRefreshTokenWriteRepository.SaveAsync();
        //        #endregion



        //        //#region LogSuccessedLogin

        //        //LogSuccessedLogin logSuccessedLogin = new LogSuccessedLogin();
        //        //logSuccessedLogin.GidUserFK = userToCheck.Gid;
        //        //logSuccessedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //        //logSuccessedLogin.SessionId = sessionId;
        //        //logSuccessedLogin.LogOutDate = null;

        //        //await _logSuccessedLoginWriteRepository.AddAsync(logSuccessedLogin);


        //        //await _logSuccessedLoginWriteRepository.SaveAsync();

        //        //#endregion

        //        return new LoginForWorkerAuthResponse()
        //        {
        //            Message = AuthBussinessMessages.SuccessLogin,
        //            IsValid = true,
        //            Token = token,
        //            //SucceededLogGid = logSuccessedLogin.Gid,
        //            SessionId = sessionId,
        //        };
        //    }

        //    private async Task<bool> UserExists(string email)
        //    {
        //        var query = await _userReadRepository.GetListAllAsync(user => user.Email == email);
        //        if (query.Count <= 0)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    private async Task IdNumberCheched(string idNumber)
        //    {
        //        var query = await _userReadRepository.GetAll().Where(x => x.IdentityNo == idNumber).ToListAsync();
        //        if (!idNumber.IsNullOrEmpty() && query.Count > 0)
        //        {
        //            throw new Exception(AuthBussinessMessages.IdNumberAlreadyExist);
        //        }
        //    }
        //    private async Task PhoneNumberChecked(string phone)
        //    {
        //        var query = await _userReadRepository.GetAll().Where(x => x.Gsm == phone).ToListAsync();
        //        if (query.Count > 0)
        //        {
        //            throw new Exception(AuthBussinessMessages.PhoneNumberAlreadyExist);
        //        }
        //    }

        //    public async Task<UpdatePasswordAuthResponse> UpdatePassword(UpdatePasswordAuthCommand updatePasswordAuthCommand)
        //    {
        //        User userToCheck = await _userReadRepository.GetSingleAsync(u => u.Email == updatePasswordAuthCommand.Email, false);

        //        if (userToCheck == null)
        //        {
        //            throw new Exception(AuthBussinessMessages.UserNotExists);
        //        }

        //        if (!HashingHelperForAuth.VeriFyPasswordHash(updatePasswordAuthCommand.OldPassword, userToCheck.Password, userToCheck.PasswordHash))
        //        {
        //            return new UpdatePasswordAuthResponse()
        //            {
        //                Title = AuthBussinessMessages.IncorrectOperation,
        //                ActionType = ActionType.Error.ToString(),
        //                Message = AuthBussinessMessages.EmailOrPasswordIncorrect,
        //                IsValid = false,
        //            };
        //        }


        //        string passwordHash, passwordSalt;
        //        HashingHelperForAuth.CreatePasswordHash(updatePasswordAuthCommand.NewPassword, out passwordHash, out passwordSalt);

        //        userToCheck.Password = passwordHash;
        //        userToCheck.PasswordHash = passwordSalt;

        //        _userWriteRepository.Update(userToCheck);

        //        await _userWriteRepository.SaveAsync();

        //        return new()
        //        {
        //            Title = AuthBussinessMessages.ProcessCompleted,
        //            IsValid = true,
        //            Message = AuthBussinessMessages.SuccessUpdatedPassword,
        //        };
        //    }

        //    private List<LogFailedLogin> GetFailedLogin(string email, string ipAddress, int hour)
        //    {
        //        return _logFailedLoginReadRepository.Table.Where(a => a.DataState == DataState.Active && (a.Email == email || a.IpAddress == ipAddress) &&
        //                                                    a.CreatedDate.AddHours(hour) > DateTime.Now).ToList();
        //    }

        //    public async Task<T.Token> CreateTokenByRefreshToken(string refreshToken)
        //    {
        //        var existRefreshToken = await _userRefreshTokenReadRepository.GetAsync(x => x.RefreshToken == refreshToken);
        //        if (existRefreshToken == null)
        //        {
        //            return null;
        //            //todo

        //        }
        //        var user = await _userReadRepository.GetByGidAsync(existRefreshToken.GidUserFK);
        //        if (user == null)
        //        {
        //            return null;
        //            //todo
        //        }
        //        var token = _tokenHandler.CreateAccessToken(user, 600);
        //        existRefreshToken.RefreshToken = token.RefreshToken;
        //        existRefreshToken.Expiration = token.AccessTokenExpiration;
        //        await _userRefreshTokenWriteRepository.SaveAsync();

        //        return token;
        //    }

        //    public async Task<bool> RevokeRefreshToken(string refreshToken)
        //    {
        //        var existRefreshToken = await _userRefreshTokenReadRepository.GetAsync(x => x.RefreshToken == refreshToken);
        //        if (existRefreshToken == null)
        //        {
        //            return false;

        //        }
        //        existRefreshToken.DataState = DataState.Deleted;
        //        _userRefreshTokenWriteRepository.Update(existRefreshToken);
        //        await _userRefreshTokenWriteRepository.SaveAsync();
        //        return true;
        //    }

        //    public async Task<LoginAuthWithSystemAdminResponse> LoginWithSystemAdmin(LoginAuthWithSystemAdminCommand loginAuthWithSystemAdminCommand)
        //    {
        //        #region Too Many Failed Attempts

        //        #endregion

        //        var adminToCheck = await _userReadRepository.GetSingleAsync(u => u.Gid == loginAuthWithSystemAdminCommand.Gid);

        //        if (adminToCheck == null)
        //        {
        //            throw new BusinessException("Admin not found, please check");
        //        }

        //        if (adminToCheck.IsSystemAdmin == false)
        //        {
        //            throw new BusinessException("You are not system admin");
        //        }

        //        var userToCheck = await _userReadRepository.GetSingleAsync(u => u.Gid == loginAuthWithSystemAdminCommand.GidUser);



        //        if (userToCheck == null)
        //        {
        //            #region LogFailedLogin

        //            LogFailedLogin logFailedLogin = new LogFailedLogin();
        //            logFailedLogin.Email = "System Admin";
        //            logFailedLogin.Password = "System Admin";
        //            logFailedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //            logFailedLogin.Description = "User Not Found!";

        //            await _logFailedLoginWriteRepository.AddAsync(logFailedLogin);
        //            await _logFailedLoginWriteRepository.SaveAsync();

        //            #endregion

        //            return new LoginAuthWithSystemAdminResponse()
        //            {
        //                Title = AuthBussinessMessages.IncorrectOperation,
        //                ActionType = ActionType.Error.ToString(),
        //                Message = AuthBussinessMessages.EmailNotFound,
        //                IsValid = false,
        //                Token = null
        //            };
        //        }



        //        #region token - refresh token
        //        T.Token token = _tokenHandler.CreateAccessToken(userToCheck, 600);
        //        var sessionId = Guid.NewGuid().ToString();
        //        var userRefreshToken = await _userRefreshTokenReadRepository.GetAsync(x => x.GidUserFK == userToCheck.Gid);

        //        if (userRefreshToken == null)
        //        {
        //            UserRefreshToken refreshToken = new UserRefreshToken
        //            {
        //                GidUserFK = userToCheck.Gid,
        //                RefreshToken = token.RefreshToken,
        //                Expiration = token.RefreshTokenExpiration,
        //            };
        //            await _userRefreshTokenWriteRepository.AddAsync(refreshToken);

        //        }
        //        else
        //        {
        //            userRefreshToken.RefreshToken = token.RefreshToken;
        //            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        //        }
        //        await _userRefreshTokenWriteRepository.SaveAsync();
        //        #endregion






        //        #region LogSuccessedLogin

        //        LogSuccessedLogin logSuccessedLogin = new LogSuccessedLogin();
        //        logSuccessedLogin.GidUserFK = userToCheck.Gid;
        //        logSuccessedLogin.IpAddress = _getUserInfo.GetUserIpAddress();
        //        logSuccessedLogin.SessionId = sessionId;
        //        logSuccessedLogin.LogOutDate = null;

        //        await _logSuccessedLoginWriteRepository.AddAsync(logSuccessedLogin);

        //        await _logSuccessedLoginWriteRepository.SaveAsync();

        //        #endregion

        //        return new LoginAuthWithSystemAdminResponse()
        //        {
        //            Message = AuthBussinessMessages.SuccessLogin,
        //            IsValid = true,
        //            Token = token,
        //            SessionId = sessionId,
        //            SucceededLogGid = logSuccessedLogin.Gid
        //        };
        //    }

        //    public async Task<UpdatePasswordAuthBySystemAdminResponse> UpdatePasswordBySystemAdmin(UpdatePasswordAuthBySystemAdminCommand updatePasswordAuthBySystemAdminCommand)
        //    {
        //        User systemAdmin = await _userReadRepository.GetSingleAsync(u => u.Gid == updatePasswordAuthBySystemAdminCommand.Gid, false);

        //        if (systemAdmin == null)
        //        {
        //            throw new Exception(AuthBussinessMessages.UserNotExists);
        //        }
        //        if (systemAdmin.IsSystemAdmin == false)
        //        {
        //            throw new BusinessException("You are not allowed to change password");
        //        }

        //        string passwordHash, passwordSalt;

        //        User changedUser = await _userReadRepository.GetSingleAsync(u => u.Gid == updatePasswordAuthBySystemAdminCommand.GidUser, false);

        //        HashingHelperForAuth.CreatePasswordHash(updatePasswordAuthBySystemAdminCommand.NewPassword, out passwordHash, out passwordSalt);

        //        changedUser.Password = passwordHash;
        //        changedUser.PasswordHash = passwordSalt;

        //        _userWriteRepository.Update(changedUser);

        //        await _userWriteRepository.SaveAsync();

        //        return new()
        //        {
        //            Title = AuthBussinessMessages.ProcessCompleted,
        //            IsValid = true,
        //            Message = AuthBussinessMessages.SuccessUpdatedPassword,
        //        };
        //    }




        //    public async Task ChangeForgottenPassword(string email)
        //    {
        //        var userToCheck = await _userReadRepository.GetSingleAsync(u => u.Email == email);
        //        // TODO: lazým olduðunda yazýlacak þuan lazým deðil. RESETTOKEN oluþturulacak ve mail atýlacak.

        //        if (userToCheck != null)
        //        {

        //        }

        //    }


        //}
    }
}
