using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace jwttest.MyAuth
{
  public class MyAuthorizationHandler2 : AuthorizationHandler<MyAuthorizationRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MyAuthorizationRequirement requirement)
    {
      Console.WriteLine("MyAuthorizationHandler2 Handle Requirement");
      if(requirement._funcs.Any(f => context.User.HasClaim(c => c.Value == f)))
      {
          context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}