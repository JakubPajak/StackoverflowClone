using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StackoveflowClone.Entities;

namespace StackoveflowClone.AuthorizationAuthentication
{
	public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, User>
	{
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context
            , ResourceOperationRequirement requirement, User resource)
        {
            if(requirement.ResourceOperation == ResourceOperation.Read
                || requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }


            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (userId == resource.Id)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

