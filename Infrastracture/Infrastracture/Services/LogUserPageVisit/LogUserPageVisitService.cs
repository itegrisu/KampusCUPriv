namespace Infrastracture.Services.LogUserPageVisit
{
    public class LogUserPageVisitService
    {
        //private readonly IUserReadRepository _userReadRepository;
        //private readonly GetUserInfo _getUserInfo;
        //private readonly ILogUserPageVisitWriteRepository _logUserPageVisitWriteRepository;

        //public LogUserPageVisitService(IUserReadRepository userReadRepository, GetUserInfo getUserInfo, ILogUserPageVisitWriteRepository logUserPageVisitWriteRepository)
        //{
        //    _userReadRepository = userReadRepository;
        //    _getUserInfo = getUserInfo;
        //    _logUserPageVisitWriteRepository = logUserPageVisitWriteRepository;
        //}

        //public async Task LogUserPageVisit()
        //{
        //    var gid = _getUserInfo.GetUserGid();
        //    var pageInfo = _getUserInfo.GetPageInfo();
        //    User user = await _userReadRepository.GetAsync(u => u.Gid.ToString() == gid);

        //    if (gid == null || pageInfo == null || user == null)
        //    {
        //        return;
        //    }

        //    L.LogUserPageVisit logUserPageVisit = new()
        //    {
        //        GidUserFK = user.Gid,
        //        IpAddress = _getUserInfo.GetUserIpAddress(),
        //        PageInfo = pageInfo,
        //        SessionId = _getUserInfo.GetUserSessionID(),
        //    };
        //    _logUserPageVisitWriteRepository.Add(logUserPageVisit);
        //    await _logUserPageVisitWriteRepository.SaveAsync();
        //}
    }
}
