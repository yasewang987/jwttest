using Microsoft.AspNetCore.Authorization;

namespace jwttest.MyAuth
{
    public class MyAuthorizationRequirement : IAuthorizationRequirement
    {
        public string[] _funcs { get; }
        public MyAuthorizationRequirement(params string[] funcs)
        {
            _funcs = funcs;
        } 
    }
}