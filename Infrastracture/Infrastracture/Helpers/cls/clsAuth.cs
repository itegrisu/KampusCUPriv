namespace Infrastracture.Helpers.cls
{
    public class clsAuth
    {
        //private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
        //private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
        //private readonly IAuthPageReadRepository _authPageReadRepository;
        //private readonly ILogAuthorizationErrorWriteRepository _logAuthorizationErrorWriteRepository;
        //private readonly IUserReadRepository _userReadRepository;
        //private readonly GetUserInfo _getUserInfo;

        //public clsAuth(IAuthUserRoleReadRepository authUserRoleReadRepository, IAuthRolePageReadRepository authRolePageReadRepository, IAuthPageReadRepository authPageReadRepository, GetUserInfo getUserInfo, ILogAuthorizationErrorWriteRepository logAuthorizationErrorWriteRepository, IUserReadRepository userReadRepository)
        //{
        //    _authUserRoleReadRepository = authUserRoleReadRepository;
        //    _authRolePageReadRepository = authRolePageReadRepository;
        //    _authPageReadRepository = authPageReadRepository;
        //    _getUserInfo = getUserInfo;
        //    _logAuthorizationErrorWriteRepository = logAuthorizationErrorWriteRepository;
        //    _userReadRepository = userReadRepository;
        //}

        //public async Task<bool> AuthCheck(string gidUser, string url)
        //{
        //    User? user = await _userReadRepository.GetAsync(x => x.Gid.ToString() == gidUser);
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    var result = UserAuthCheck(user.Gid, url);
        //    if (result)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        #region Log Authorization Errors

        //        LogAuthorizationError logAuthorizationError = new LogAuthorizationError();
        //        logAuthorizationError.GidUserFK = user.Gid;
        //        logAuthorizationError.IpAddress = _getUserInfo.GetUserIpAddress();
        //        logAuthorizationError.PageInfo = url;
        //        logAuthorizationError.Operation = "";//Page_Load - IsCallback: " + IsCallback.ToString();
        //        //logAuthorizationError.CreatedUser = gidUser;

        //        await _logAuthorizationErrorWriteRepository.AddAsync(logAuthorizationError);
        //        await _logAuthorizationErrorWriteRepository.SaveAsync();

        //        #endregion

        //        return false;
        //    }
        }

        //private bool UserAuthCheck(Guid gidUser, string url)
        //{
        //    url = url.Replace("//", "");
        //    int slash = url.Count(s => s == '/');
        //    if (slash == 0 || url.IsNullOrEmpty())
        //        return false;
        //    if (slash >= 1)
        //        url = "/" + url.Split('/')[1];

        //    if (IsPageAccessAuthorised(gidUser, url))
        //        return true;

        //    List<AuthUserRole> authUserRole = GetAuthUserNonRolesPages(gidUser);

        //    if (authUserRole.Count > 0)
        //    {
        //        foreach (AuthUserRole nonRolePage in authUserRole)
        //        {
        //            if (nonRolePage.AuthPageFK.PathForAuthCheck == url)
        //                return true;
        //        }
        //    }
        //    return false;
        //}

        //private bool IsPageAccessAuthorised(Guid gidUser, string authPath)
        //{
        //    AuthPage page = GetAuthPageDetailsForAuthCheck(authPath);

        //    if (page == null)
        //        return false;

        //    #region Once Kullaniciya Ait Rolleri Bulalim ve Ilgili sayfa Role AtanmisMi Diye Bakalim

        //    List<AuthUserRole> authUserRoles = GetAuthUserRoles(gidUser);

        //    foreach (AuthUserRole role in authUserRoles)
        //    {
        //        List<AuthRolePage> authRolePage = GetAuthRolePageByRole(role.GidRoleFK.Value);
        //        foreach (AuthRolePage rolePage in authRolePage)
        //        {
        //            if (rolePage.GidPageFK == page.Gid)
        //                return true;
        //        }
        //    }

        //    #endregion

        //    #region Eger Rollerimde Yoksa Sayfa Olarak bana eklenmisMi

        //    foreach (AuthUserRole role in authUserRoles.Where(x => x.GidPageFK.HasValue).ToList())
        //    {
        //        if (role.GidPageFK == page.Gid)
        //            return true;
        //    }

        //    #endregion

        //    //Buraya kadar sayfa yetkisi bulunamadi
        //    return false;
        //}

        //private List<AuthUserRole> GetAuthUserRoles(Guid userGid)
        //{
        //    List<AuthUserRole> authUserRoles = _authUserRoleReadRepository.Table.Where(aur => aur.GidUserFK == userGid && aur.DataState == Core.Enum.DataState.Active)
        //        .Include(i => i.AuthRoleFK).Include(i => i.AuthPageFK).Include(i => i.UserFK)
        //        .OrderBy(x => x.RowNo).ToList();
        //    return authUserRoles;
        //}

        //private List<AuthUserRole> GetAuthUserNonRolesPages(Guid userGid)
        //{
        //    List<AuthUserRole> authUserRoles = _authUserRoleReadRepository.Table.Where(aur => aur.GidUserFK == userGid && aur.DataState == Core.Enum.DataState.Active && aur.GidRoleFK == null && aur.GidPageFK != null)
        //        .Include(i => i.AuthRoleFK).Include(i => i.AuthPageFK).Include(i => i.UserFK)
        //        .OrderBy(x => x.RowNo).ToList();
        //    return authUserRoles;
        //}

        //private List<AuthRolePage> GetAuthRolePageByRole(Guid gidRole)
        //{
        //    List<AuthRolePage> authRolePages = _authRolePageReadRepository.Table.Where(arp => arp.DataState == Core.Enum.DataState.Active && arp.AuthPageFK.DataState == Core.Enum.DataState.Active
        //    && arp.GidRoleFK == gidRole).Include(arp => arp.AuthRoleFK).Include(arp => arp.AuthPageFK)
        //    .OrderBy(x => x.RowNo).ToList();
        //    return authRolePages;
        //}

        //private AuthPage GetAuthPageDetailsForAuthCheck(string pathForAuthCheck)
        //{
        //    AuthPage? authPage = _authPageReadRepository.Table.Where(ap => ap.DataState == Core.Enum.DataState.Active && ap.PathForAuthCheck == pathForAuthCheck).FirstOrDefault();
        //    return authPage;
        //}
    }

